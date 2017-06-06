using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.SDK.Contract
{
    public class MatchInfoViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 展示图
        /// </summary>
        public string ShowImage { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }

        public List<MatchItemViewModel> MatchItems { get; set; }
    }

    public class MatchItemViewModel
    {
        public int Id { get; set; }
        public int? SkuId { get; set; }
        public int ProductId { get; set; }

        public string SkuPicUrl { get; set; }
        public string SkuTitle { get; set; }
    }
}
