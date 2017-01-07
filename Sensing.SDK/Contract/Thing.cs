using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.SDK.Contract
{
    public class ThingViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 人可识别的唯一ID号码,如 Product-Xl-8890
        /// </summary>
        public string IdentifyingId { get; set; }

        /// <summary>
        /// 如SKU,90121020910231203
        /// </summary>
        public string HumanCode { get; set; }

        /// <summary>
        /// 事物所属的所有者,也许它是别人的共有财产呢?
        /// </summary>
        public string Owner { get; set; }

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
        /// 万物总有属于他自己的关键字,让别人好找到它.
        /// </summary>
        public string Keywords { get; set; }

        /// <summary>
        /// 有了分类是不是可以按照树形结构查找呢,可能,先留着吧。
        /// </summary>
        public string Categories { get; set; }

        /// <summary>
        /// Thing(商品/服务/需要上下架)
        /// </summary>
        public string AuditStatus { get; set; }

        public int GroupId { get; set; }
        public string GroupName { get; set; }


        public string ImageUrl { get; set; }

        /// <summary>
        /// Thing被专门喜欢的此处.
        /// </summary>
        public int LikeCount { get; set; }

        public IEnumerable<ResourceFileViewModel> Files { get; set; }

        public string Description { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? LastUpdated { get; set; }

        public IEnumerable<TCategoryViewModel> TCategories { get; set; }

        public IEnumerable<TagViewModel> Tas { get; set; }
    }


    public class ResourceFileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// 资源的地址途径
        /// </summary>
        public string FileUrl { get; set; }

        public string Content { get; set; }

        public string Type { get; set; }

        public string Tags { get; set; }

        public DateTime? Created { get; set; }

        public int GroupId { get; set; }
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


        public bool IsLocal { get; set; }


        public bool IsSpecial { get; set; }
    }

    public class TagViewModel
    {
        public int Id { get; set; }

        public string Value { get; set; }
    }
}
