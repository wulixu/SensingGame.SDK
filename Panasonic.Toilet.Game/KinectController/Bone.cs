using Microsoft.Kinect;

namespace TronCell.Game.KinectController
{
    // For hit testing, a dictionary of BoneData items, keyed off the endpoints
    // of a segment (Bone) is used.  The velocity of these endpoints is estimated
    // and used during hit testing and updating velocity vectors after a hit.
    public struct Bone
    {
        public JointType Joint1;
        public JointType Joint2;

        public Bone(JointType j1, JointType j2)
        {
            this.Joint1 = j1;
            this.Joint2 = j2;
        }
    }
}
