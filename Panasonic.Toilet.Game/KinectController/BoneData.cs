using System;

namespace TronCell.Game.KinectController
{
    public struct BoneData
    {
        public Segment Segment;
        public Segment LastSegment;
        public double XVelocity;
        public double YVelocity;
        public double XVelocity2;
        public double YVelocity2;
        public DateTime TimeLastUpdated;

        private const double Smoothing = 0.8;

        public BoneData(Segment s)
        {
            this.Segment = this.LastSegment = s;
            this.XVelocity = this.YVelocity = 0;
            this.XVelocity2 = this.YVelocity2 = 0;
            this.TimeLastUpdated = DateTime.Now;
        }

        // Update the segment's position and compute a smoothed velocity for the circle or the
        // endpoints of the segment based on  the time it took it to move from the last position
        // to the current one.  The velocity is in pixels per second.
        public void UpdateSegment(Segment s)
        {
            this.LastSegment = this.Segment;
            this.Segment = s;

            DateTime cur = DateTime.Now;
            double fMs = cur.Subtract(this.TimeLastUpdated).TotalMilliseconds;
            if (fMs < 10.0)
            {
                fMs = 10.0;
            }

            double fps = 1000.0 / fMs;
            this.TimeLastUpdated = cur;

            if (this.Segment.IsCircle())
            {
                this.XVelocity = (this.XVelocity * Smoothing) + ((1.0 - Smoothing) * (this.Segment.X1 - this.LastSegment.X1) * fps);
                this.YVelocity = (this.YVelocity * Smoothing) + ((1.0 - Smoothing) * (this.Segment.Y1 - this.LastSegment.Y1) * fps);
            }
            else
            {
                this.XVelocity = (this.XVelocity * Smoothing) + ((1.0 - Smoothing) * (this.Segment.X1 - this.LastSegment.X1) * fps);
                this.YVelocity = (this.YVelocity * Smoothing) + ((1.0 - Smoothing) * (this.Segment.Y1 - this.LastSegment.Y1) * fps);
                this.XVelocity2 = (this.XVelocity2 * Smoothing) + ((1.0 - Smoothing) * (this.Segment.X2 - this.LastSegment.X2) * fps);
                this.YVelocity2 = (this.YVelocity2 * Smoothing) + ((1.0 - Smoothing) * (this.Segment.Y2 - this.LastSegment.Y2) * fps);
            }
        }

        // Using the velocity calculated above, estimate where the segment is right now.
        public Segment GetEstimatedSegment(DateTime cur)
        {
            Segment estimate = this.Segment;
            double fMs = cur.Subtract(this.TimeLastUpdated).TotalMilliseconds;
            estimate.X1 += fMs * this.XVelocity / 1000.0;
            estimate.Y1 += fMs * this.YVelocity / 1000.0;
            if (this.Segment.IsCircle())
            {
                estimate.X2 = estimate.X1;
                estimate.Y2 = estimate.Y1;
            }
            else
            {
                estimate.X2 += fMs * this.XVelocity2 / 1000.0;
                estimate.Y2 += fMs * this.YVelocity2 / 1000.0;
            }

            return estimate;
        }
    }
}
