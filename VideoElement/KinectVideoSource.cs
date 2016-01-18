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
    public class KinectVideoSource
    {
        public Action HandUpAction;

        public Action PlayerLeave;

        public Action PlayerEnter;

        private int bodyCount = 6;
        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensor kinectSensor = null;

        /// <summary>
        /// Size of the RGB pixel in the bitmap
        /// </summary>
        private readonly int bytesPerPixel = (PixelFormats.Bgr32.BitsPerPixel + 7) / 8;

        /// <summary>
        /// Coordinate mapper to map one type of point to another
        /// </summary>
        private CoordinateMapper coordinateMapper = null;

        /// <summary>
        /// Reader for depth/color/body index frames
        /// </summary>
        private MultiSourceFrameReader multiFrameSourceReader = null;

        /// <summary>
        /// Bitmap to display
        /// </summary>
        private WriteableBitmap bitmap = null;

        /// <summary>
        /// The size in bytes of the bitmap back buffer
        /// </summary>
        private uint bitmapBackBufferSize = 0;

        /// <summary> Array for the bodies (Kinect will track up to 6 people simultaneously) </summary>
        private Body[] bodies = null;


        private Player[] players = null;

        private DrawingImage hatImage;

        private DrawingGroup hatCanvas;

        private DrawingImage handPointImage;

        private DrawingGroup handCanvas;

        private VideoControl _videoControl;

        public bool Pause { get; set; }


        private const double FaceRotationIncrementInDegrees = 5.0;


        /// <summary>
        /// Width of display (color space)
        /// </summary>
        private int displayWidth;

        /// <summary>
        /// Height of display (color space)
        /// </summary>
        private int displayHeight;

        /// <summary>
        /// Display rectangle
        /// </summary>
        private Rect displayRect;

        private DateTime? lastIdleTime = DateTime.Now;

        public KinectVideoSource(VideoControl videoControl)
        {
            _videoControl = videoControl;
            // get the kinectSensor object
            this.kinectSensor = KinectSensor.GetDefault();
            this.multiFrameSourceReader = this.kinectSensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Body);

            this.multiFrameSourceReader.MultiSourceFrameArrived += this.Reader_MultiSourceFrameArrived;

            this.coordinateMapper = this.kinectSensor.CoordinateMapper;


            FrameDescription colorFrameDescription = this.kinectSensor.ColorFrameSource.FrameDescription;

            displayWidth = colorFrameDescription.Width;
            displayHeight = colorFrameDescription.Height;
            this.displayRect = new Rect(0.0, 0.0, this.displayWidth, this.displayHeight);

            this.bitmap = new WriteableBitmap(displayWidth, displayHeight, 96.0, 96.0, PixelFormats.Bgra32, null);

            // Calculate the WriteableBitmap back buffer size
            this.bitmapBackBufferSize = (uint)((this.bitmap.BackBufferStride * (this.bitmap.PixelHeight - 1)) + (this.bitmap.PixelWidth * this.bytesPerPixel));
            // open the sensor
            this.kinectSensor.Open();

            hatCanvas = new DrawingGroup();
            hatImage = new DrawingImage(hatCanvas);

            handCanvas = new DrawingGroup();
            handPointImage = new DrawingImage(this.handCanvas);


        }

        private void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            if (Pause) return;

            ColorFrame colorFrame = null;
            BodyFrame bodyFrame = null;
            bool isBitmapLocked = false;

            MultiSourceFrame multiSourceFrame = e.FrameReference.AcquireFrame();

            // If the Frame has expired by the time we process this event, return.
            if (multiSourceFrame == null)
            {
                return;
            }

            // We use a try/finally to ensure that we clean up before we exit the function.  
            // This includes calling Dispose on any Frame objects that we may have and unlocking the bitmap back buffer.
            try
            {
                colorFrame = multiSourceFrame.ColorFrameReference.AcquireFrame();
                bodyFrame = multiSourceFrame.BodyFrameReference.AcquireFrame();

                // If any frame has expired by the time we process this event, return.
                // The "finally" statement will Dispose any that are not null.
                if (colorFrame == null || bodyFrame == null)
                {
                    return;
                }

                if (colorFrame != null)
                {
                    FrameDescription colorFrameDescription = colorFrame.FrameDescription;

                    using (KinectBuffer colorBuffer = colorFrame.LockRawImageBuffer())
                    {
                        this.bitmap.Lock();

                        // verify data and write the new color frame data to the display bitmap
                        if ((colorFrameDescription.Width == this.bitmap.PixelWidth) && (colorFrameDescription.Height == this.bitmap.PixelHeight))
                        {
                            colorFrame.CopyConvertedFrameDataToIntPtr(
                                this.bitmap.BackBuffer,
                                (uint)(colorFrameDescription.Width * colorFrameDescription.Height * 4),
                                ColorImageFormat.Bgra);

                            this.bitmap.AddDirtyRect(new Int32Rect(0, 0, this.bitmap.PixelWidth, this.bitmap.PixelHeight));
                        }

                        this.bitmap.Unlock();
                    }
                }

                if (bodyFrame != null)
                {
                    if (bodies == null)
                    {
                        bodies = new Body[bodyFrame.BodyCount];
                        players = new Player[bodies.Length];
                        for (var i = 0; i < bodies.Length; i++)
                        {
                            players[i] = new Player();
                        }
                    }

                    bodyFrame.GetAndRefreshBodyData(bodies);

                    bool playAlive = bodies.Any(b => b.IsTracked); 

                    if(playAlive)
                    {
                        if (lastIdleTime != null)
                        {
                            if (PlayerEnter != null)
                                PlayerEnter.Invoke();
                            lastIdleTime = null;
                        }
                    }
                    else
                    {
                        if(lastIdleTime == null)
                        {
                            lastIdleTime = DateTime.Now;
                        }


                        if (DateTime.Now.Subtract(lastIdleTime.Value).TotalSeconds > AppConfig.ScreenSave)
                        {
                            lastIdleTime = DateTime.Now;
                            if (PlayerLeave != null)
                            {
                                PlayerLeave.Invoke();
                            }
                        }
                        
                    }

                    using (var handDc = handCanvas.Open())
                    using (var dc = hatCanvas.Open())
                    {
                        dc.DrawRectangle(Brushes.Transparent, null, new Rect(0, 0, bitmap.Width, bitmap.Height));
                        handDc.DrawRectangle(Brushes.Transparent, null, new Rect(0, 0, bitmap.Width, bitmap.Height));
                        for (var i = 0; i < bodies.Length; i++)
                        {
                            var body = bodies[i];
                            var player = players[i];
                            if (body.IsTracked && body.Joints[JointType.SpineBase].Position.Z < AppConfig.Distance)
                            {
                                if (body.TrackingId == player.TrackingId)
                                {
                                    player.Update(body, body.Joints, coordinateMapper);
                                }
                                else
                                {
                                    player.LostTrack();
                                    player.TrackingId = body.TrackingId;
                                    player.HatName = string.Format("hats/hat{0}.png", i + 1);
                                }
                            }
                            else
                            {
                                player.LostTrack();
                            }
                            if (player.TrackingId != 0)
                            {
                                _videoControl.UpdateLasso(player.IsLasso());

                                player.Draw(dc, handDc);

                            }
                        }
                        this.hatCanvas.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, bitmap.Width, bitmap.Height));
                        this.handCanvas.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, bitmap.Width, bitmap.Height));
                    }
                    bool ready = false;
                    foreach (var p in players)
                    {
                        if (p.IsReady)
                        {
                            ready = true;
                            break;
                        }
                    }
                    if (ready)
                    {
                        foreach (var p in players) p.Reset();
                        if (HandUpAction != null)
                        {
                            HandUpAction.Invoke();
                        }
                    }
                }

            }
            finally
            {
                if (isBitmapLocked)
                {
                    this.bitmap.Unlock();
                }

                if (colorFrame != null)
                {
                    colorFrame.Dispose();
                }
                if (bodyFrame != null)
                {
                    bodyFrame.Dispose();
                }
            }
        }

        public ImageSource ImageSource
        {
            get
            {
                return this.bitmap;
            }
        }

        public ImageSource HatImageSource
        {
            get
            {
                return this.hatImage;
            }
        }

        public ImageSource HandImageSource
        {
            get
            {
                return this.handPointImage;
            }
        }

        private void GestureFired(string gestureName)
        {
            if (gestureName == "Lasso")
            {
                HandUpAction.Invoke();
            }
        }


        /// <summary>
        /// Handles the event which the sensor becomes unavailable (E.g. paused, closed, unplugged).
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Sensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e)
        {
            Console.WriteLine(this.kinectSensor.IsAvailable ? "Running" : "Kinect not available!");
        }

        public void Dispose()
        {


            if (this.kinectSensor != null)
            {
                this.kinectSensor.Close();
                this.kinectSensor = null;
            }
        }
    }
}
