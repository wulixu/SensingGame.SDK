using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.SDK.Contract
{
    public class LikeInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }

        public List<LikeItemViewModel> LikeItems { get; set; }
    }

    public class LikeItemViewModel
    {
        public int Id { get; set; }
        public int? SkuId { get; set; }
        public int LikeInfoId { get; set; }
        public string Reason { get; set; }
        public string SkuPicUrl { get; set; }
        public string SkuTitle { get; set; }
    }
}
