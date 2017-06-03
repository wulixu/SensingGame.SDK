using System;

namespace Sensing.SDK.Contract
{
    public class SkuViewModel
    {
        public int Id { get; set; }

        public long ItemId { get; set; }

        public string SkuId { get; set; }

        public long Num { get; set; }

        public string PropsName { get; set; }
        /// <summary>
        /// 事物都该有个名字来表示
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 在当今社会,任何事物都是可以明码标价的,难道不是!
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 二维码这么流行,没个这玩意都不好意思说我在编程.
        /// </summary>
        public string QRCodeUrl { get; set; }

        /// <summary>
        /// 万物总有属于他自己的关键字,让别人好找到它.
        /// </summary>
        public string Keywords { get; set; }

        /// <summary>
        /// Thing(商品/服务/需要上下架)
        /// </summary>
        public string AuditStatus { get; set; }

        public int GroupId { get; set; }
        public string GroupName { get; set; }


        public string PicUrl { get; set; }

        public bool HasSelfImage { get; set; }
        ///// <summary>
        ///// Thing被专门喜欢的此处.
        ///// </summary>
        //public int LikeCount { get; set; }

        public string Description { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? LastUpdated { get; set; }

        public int OrderNumber { get; set; }


        //public IEnumerable<ProductCategoryViewModel> P_ProductCategories { get; set; }

        //public IEnumerable<TagViewModel> ProductTags { get; set; }


    }
}