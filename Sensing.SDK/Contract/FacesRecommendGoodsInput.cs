using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.SDK.Contract
{
    public class FaceImage
    {
        public byte[] Image { get; set; }
        public string ImageUrl { get; set; }
        public string Type { get; set; }
    }
    public class FacesRecommendGoodsInput
    {
        public FaceImage[] Faces { get; set; }
        public string Subkey { get; set; }
    }
}