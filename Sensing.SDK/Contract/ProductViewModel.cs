using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.SDK.Contract
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public long ItemId { get; set; }

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

        /// <summary>
        /// Thing被专门喜欢的此处.
        /// </summary>
        public int LikeCount { get; set; }

        public string Description { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? LastUpdated { get; set; }

        public int OrderNumber { get; set; }

        public IEnumerable<ProductCategoryViewModel> P_ProductCategories { get; set; }

        public IEnumerable<TagViewModel> ProductTags { get; set; }

        public IEnumerable<SkuViewModel> Skus { get; set; }

        public IEnumerable<ProductFileViewModel> ItemImagesOrVedios { get; set; }

        public bool IsFromBrand { get; set; }
        public string SumPropsName { get; set; }
        public string SellerId { get; set; }
    }

    public class ProductCategoryViewModel
    {
        public int Id { get; set; }

        /// <summary>
        /// 可被理解的分类唯一编码.
        /// </summary>
        public string CategoryCode { get; set; }
        /// <summary>
        /// 分类的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父分类
        /// </summary>
        public int? ParentCategoryId { get; set; }

        public string ParentCategoryName { get; set; }

        /// <summary>
        /// 分类所属组
        /// </summary>
        public int GroupId { get; set; }
        public string GroupName { get; set; }

        /// <summary>
        /// 分类的图片
        /// </summary>
        public string ImageUrl { get; set; }


        public string IconUrl { get; set; }


        public bool IsLocal { get; set; }


        public bool IsSpecial { get; set; }

        public string FromType { get; set; }
        public DateTime Created { get; set; }
    }

    public class ProductFileViewModel
    {
        public int ProductId { get; set; }

        public int ResourceId { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 资源的地址途径
        /// </summary>
        public string FileUrl { get; set; }

        public ExternalEnum FromType { get; set; }
    }

    public enum ExternalEnum
    {
        [Display(Name = "平台分类")]
        Platform,
        [Display(Name = "淘宝分类")]
        Taobao,
        [Display(Name = "Oracle分类")]
        Oracle
    }
}
