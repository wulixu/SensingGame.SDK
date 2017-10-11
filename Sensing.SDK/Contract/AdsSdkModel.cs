using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.SDK.Contract
{
    public class AdsSdkModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Pixel { get; set; }
        public double Size { get; set; }
        public string FileUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Tags { get; set; }
        public string ProductAttributes { get; set; }
        public string Content { get; set; }
        public int Postion { get; set; }
        public string Transition { get; set; }
        public TimeSpan? Duration { get; set; }
    }
}
