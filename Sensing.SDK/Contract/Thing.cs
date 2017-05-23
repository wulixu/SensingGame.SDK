using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.SDK.Contract
{
    public class ThingViewModel
    {
        // Properties
        public string AuditStatus { get; set; }

        public string Categories { get; set; }

        public IEnumerable<int> CategoryIds { get; set; }

        public DateTime? Created { get; set; }

        public string Description { get; set; }

        public IEnumerable<FileViewModel> Files { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public string HumanCode { get; set; }

        public int Id { get; set; }

        public string IdentifyingId { get; set; }

        public string ImageUrl { get; set; }

        public string Keywords { get; set; }

        public DateTime? LastUpdated { get; set; }

        public int LikeCount { get; set; }

        public string Name { get; set; }

        public string Owner { get; set; }

        public double Price { get; set; }

        public string QRCodeUrl { get; set; }

        public TaobaoThingDetailViewModel TaobaoThing { get; set; }

        public IEnumerable<TagViewModel> Tas { get; set; }

        public IEnumerable<TCategoryViewModel> TCategories { get; set; }
    }



    public class FileViewModel
    {
        // Methods

        // Properties
        public ResourceFileViewModel ResourceFileViewModel { get; set; }
        public string Type { get; set; }
    }




    public class ResourceFileViewModel
    {
        // Properties
        public string Content { get; set; }
        public DateTime? Created { get; set; }
        public string FileUrl { get; set; }
        public int GroupId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tags { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Type { get; set; }
    }





    public class TCategoryViewModel
    {
        public int Id { get; set; }

        /// <summary>
        /// 可被理解的分类唯一编码.
        /// </summary>
        public string HumanCode { get; set; }
        /// <summary>
        /// 分类的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父分类
        /// </summary>
        public int? ParentCategoryId { get; set; }

        public string ParentCategory { get; set; }

        /// <summary>
        /// 分类所属组
        /// </summary>
        public int GroupId { get; set; }
        public string GroupName { get; set; }

        /// <summary>
        /// 分类的图片
        /// </summary>
        public string ImageUrl { get; set; }
        public string ImageUrl2 { get; set; }


        public string IconUrl { get; set; }

        public string DescriptionImageUrl { get; set; }

        public bool IsLocal { get; set; }


        public bool IsSpecial { get; set; }
    }

    public class TagViewModel
    {
        public int Id { get; set; }

        public string Value { get; set; }
    }

    public class TaobaoThingDetailViewModel
    {
        public int Id { get; set; }


        /// <summary>
        /// 事物都该有个名字来表示
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 淘宝商品SPU唯一Id
        /// </summary>
        public string ItemId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SkuId { get; set; }
        /// <summary>
        /// 数量 库存
        /// </summary>
        public int TaobaoQuantity { get; set; }

        public string TaobaoStatus { get; set; }
        /// <summary>
        /// 淘宝名称
        /// </summary>
        public string TaobaoName { get; set; }
        /// <summary>
        /// 淘宝价格
        /// </summary>
        public double TaobaoPrice { get; set; }
        /// <summary>
        /// 淘宝二维码
        /// </summary>
        public string TaobaoQRCodeUrl { get; set; }
        /// <summary>
        /// 淘宝主图
        /// </summary>

        public string TaobaoPic_Url { get; set; }
        /// <summary>
        /// 淘宝描述
        /// </summary>

        public string TaobaoDescription { get; set; }

        /// <summary>
        /// 主图片集/属性图片集
        /// </summary>
        public virtual List<ResourceFileViewModel> ItemImages { get; set; }
    }


    public class FinalThingViewModel
    {
        public int Id { get; set; }

        /// <summary>
        /// 事物都该有个名字来表示
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 在当今社会,任何事物都是可以明码标价的,难道不是!
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 二维码这么流行,没个这玩意都不好意思说我在编程.
        /// </summary>
        public string QRCodeUrl { get; set; }


        /// <summary>
        /// 数量 库存
        /// </summary>
        public int Quantity { get; set; }


        /// <summary>
        /// 万物总有属于他自己的关键字,让别人好找到它.
        /// </summary>
        public string Keywords { get; set; }


        public string ImageUrl { get; set; }

        /// <summary>
        /// Thing被专门喜欢的此处.
        /// </summary>
        public int LikeCount { get; set; }

        //public IEnumerable<ResourceFileViewModel> Files { get; set; }

        public string Description { get; set; }


        public IEnumerable<TagViewModel> Tags { get; set; }

        public IEnumerable<int> CategoryIds { get; set; }

        /// <summary>
        /// 淘宝商品SPU唯一Id
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SkuId { get; set; }


    }
}
