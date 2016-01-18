using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace VideoElement
{
    public class Player
    {
        public static Dictionary<string, BitmapImage> hats = new Dictionary<string, BitmapImage>();
        const int threshold = 5;

        private int count;
        public ulong TrackingId { get; set; }

        public ColorSpacePoint HatPosition { get; set; }
        public ColorSpacePoint HeadPosition { get; set; }

        private float scale = 1;

        public ColorSpacePoint HandLeftPosition { get; set; }

        public ColorSpacePoint HandRightPosition { get; set; }

        public string HatName { get; set; }

        private bool LeftHand;

        private bool RightHand;

        public Rect FaceBox { get; set; }

        public Vector4 FaceRotationQuaternion { get; set; }

        public bool IsLasso()
        {
            return count > 0;
        }

        public void LostTrack()
        {
            TrackingId = 0;
            count = 0;
        }

        internal void Draw(System.Windows.Media.DrawingContext dc, DrawingContext handDc)
        {
            if (TrackingId != 0)
            {
                //dc.DrawEllipse(Brushes.Red, null, HatPosition, 50.0, 50.0);
                BitmapImage bitmap;
                if(hats.ContainsKey(HatName))
                {
                    bitmap = hats[HatName];
                }
                else
                {
                    bitmap = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory +  HatName, UriKind.Absolute));
                    hats.Add(HatName, bitmap);
                }

                RotateTransform rotate = new RotateTransform(15);
                rotate.CenterX = HeadPosition.X;
                rotate.CenterY = HeadPosition.Y;
                dc.PushTransform(rotate);

                dc.DrawImage(bitmap, new Rect(HatPosition.X - 100 * scale, HatPosition.Y - 100 * scale, 200 * scale, 200 * scale));
                dc.Pop();
                if (count > 0 && count < threshold)
                {
                    if (LeftHand)
                    {
                        handDc.DrawEllipse(Brushes.Red, null, new Point(HandLeftPosition.X, HandLeftPosition.Y - 40), 20, 20);
                    }
                    if (RightHand)
                    {
                        handDc.DrawEllipse(Brushes.Red, null, new Point(HandRightPosition.X, HandRightPosition.Y - 40), 20, 20);
                    }
                }
            }
        }

        internal void Update(Body body, IReadOnlyDictionary<JointType, Joint> joints, CoordinateMapper coordinateMapper)
        {

            var head = joints[JointType.Head];
            var hatPosition = head.Position;
            hatPosition.Y += 0.2f;
            scale = 1 / hatPosition.Z;
            HatPosition = coordinateMapper.MapCameraPointToColorSpace(hatPosition);
            HeadPosition = coordinateMapper.MapCameraPointToColorSpace(head.Position);
            HandLeftPosition = coordinateMapper.MapCameraPointToColorSpace(joints[JointType.HandLeft].Position);
            HandRightPosition = coordinateMapper.MapCameraPointToColorSpace(joints[JointType.HandRight].Position);

            var handRight = joints[Microsoft.Kinect.JointType.HandRight];
            var elbowRight = joints[JointType.ElbowRight];
            var handLeft = joints[Microsoft.Kinect.JointType.HandLeft];
            var elbowLeft = joints[JointType.ElbowLeft];

            if ((body.HandRightState == HandState.Lasso && handRight.Position.Y > elbowRight.Position.Y) || (body.HandLeftState == HandState.Lasso && handLeft.Position.Y > elbowLeft.Position.Y))
            {
                count++;
                if (body.HandRightState == HandState.Lasso)
                    RightHand = true;
                if (body.HandLeftState == HandState.Lasso)
                    LeftHand = true;
            }
            else
            {
                count = 0;
                RightHand = false;
                LeftHand = false;
            }


        }

        public void Reset()
        {
            RightHand = false;
            LeftHand = false;
            count = 0;
        }

        public bool IsReady
        {
            get { return count > threshold; }
        }

    }
}
