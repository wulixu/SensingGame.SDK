using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.SDK.Contract
{
    public class DeviceAdsViewModel
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public string Name { get; set; }
        public double Size { get; set; }
        public string ResourceName { get; set; }
        public string Content { get; set; }

        public int DeviceId { get; set; }

        public int AdsId { get; set; }

        public string AuditStatus { get; set; }

        public string Pixel { get; set; }

        public DateTime? Created { get; set; }
        public string ResourType { get; set; }

        public string Fileurl { get; set; }

        public string ThumbnailUrl { get; set; }

        public DateTime? StartedTime { get; set; }

        public DateTime? EndTime { get; set; }

        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// 页面切换的动画.
        /// </summary>
        public string Transition { get; set; }

        public string ProductAttributes { get; set; }
        public string Tags { get; set; }
    }
}
