using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.SDK.Contract
{
    public class ProductSdkModel
    {
        public int Id { get; set; }
        public string ItemId { get; set; }
        public long Num { get; set; }
        public int GroupId { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// 在当今社会,任何事物都是可以明码标价的,难道不是!
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 万物总有属于他自己的关键字,让别人好找到它.
        /// </summary>
        public string Keywords { get; set; }
        public string PicUrl { get; set; }

        /// <summary>
        /// Thing被专门喜欢的此处.
        /// </summary>
        public int LikeCount { get; set; }

        public string Description { get; set; }

        public int OrderNumber { get; set; }

        public bool IsFromBrand { get; set; }

        public bool HasRealSkus { get; set; }

        public string SellerId { get; set; }

        public string OuterId { get; set; }

        public string FromType { get; set; }

        public string GroupQrCodeInfo { get; set; }

        public IEnumerable<ProductDecideImageViewModel> PropImgs { get; set; }

        public int[] CategoryIds { get; set; }

        public string[] Tags { get; set; }
        public IEnumerable<SkuSdkModel> Skus { get; set; }

        public IEnumerable<ProductFileSdkModel> ItemImagesOrVedios { get; set; }

        public IEnumerable<ProductOnlineStoreInfoViewModel> OnlineStoreInfos { get; set; }
    }

    public class ProductDecideImageViewModel
    {
        public string PropertyName { get; set; }
        public string ImageUrl { get; set; }
    }

    public class ProductCategorySDKModel
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


        public ProductCategorySDKModel ParentCategory { get; set; }

        /// <summary>
        /// 分类所属组
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// 分类的图片
        /// </summary>
        public string ImageUrl { get; set; }


        public string IconUrl { get; set; }


        public bool IsLocal { get; set; }


        public bool IsSpecial { get; set; }

        public string FromType { get; set; }
    }

    public class ProductFileSdkModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileUrl { get; set; }
        public string FromType { get; set; }
        public string ResourType { get; set; }
        public string Content { get; set; }
    }

    public class ProductOnlineStoreInfoViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long ItemId { get; set; }

        /// <summary>
        /// 线上商场的Id.
        /// </summary>
        public string StoreId { get; set; }

        /// <summary>
        /// 线上商城的类型.
        /// </summary>
        public OnlineStore Type { get; set; }
        public string OnlineStoreType { get; set; }

        /// <summary>
        /// 线上商品的PID
        /// </summary>
        public string PID { get; set; }

        /// <summary>
        /// 线上商场Product的价格.
        /// </summary>
        public double? Price { get; set; }

        /// <summary>
        /// 线上商城的库存.
        /// </summary>
        public int? Inventory { get; set; }

        /// <summary>
        /// 线上商城的二维码.
        /// </summary>
        public string Qrcode { get; set; }
    }

    public enum OnlineStore
    {
        [Display(Name = "官方电商")]
        MyStore,
        [Display(Name = "淘宝电商")]
        Taobao,
        [Display(Name = "京东电商")]
        JD,
        [Display(Name = "苏宁电商")]
        Suning,
        [Display(Name = "商派电商")]
        Shangpai,
        [Display(Name = "百胜")]
        Baisheng_Openshop,
        [Display(Name = "亚马逊")]
        Amazon,
        [Display(Name = "亚马逊EDI")]
        Amazonedi,
        [Display(Name = "一号店")]
        Yihaodian,
        [Display(Name = "当当网")]
        Dangdang,
        [Display(Name = "百胜网络分销")]
        Baisheng_Encm,
        [Display(Name = "凡客V+")]
        Vjia,
        [Display(Name = "优购")]
        Yougou,
        [Display(Name = "银泰")]
        Yintai,
        [Display(Name = "聚美优品")]
        Jumei,
        [Display(Name = "微购物")]
        Weigou,
        [Display(Name = "麦考林平台")]
        M18,
        [Display(Name = "百胜ISHOP")]
        Baisheng_Ishop,
        [Display(Name = "淘宝分销")]
        Taobao_Fenxiao,
        [Display(Name = "百胜ICRM")]
        Baisheng_Icrm,
        [Display(Name = "百胜M6")]
        Baisheng_M6,
        [Display(Name = "有赞")]
        Koudaitong,
        [Display(Name = "飞牛网商城商家")]
        Feiniu,
        [Display(Name = "飞牛网转单厂家")]
        Feiniu_zd,
        [Display(Name = "苏宁海外购")]
        Suning_hwg,
        [Display(Name = "京东全球购")]
        Jingdong_qqg,
        [Display(Name = "微盟旺铺")]
        Weimeng,
        [Display(Name = "麦进斗magento商城")]
        Magento,
        [Display(Name = "工行融易购")]
        Icbc,
        [Display(Name = "卷皮网")]
        Juanpi,
        [Display(Name = "蘑菇街小店")]
        Xiaodian,
        [Display(Name = "折800")]
        Zhe800,
        [Display(Name = "网易考拉")]
        Kaola,
        [Display(Name = "贝贝网")]
        Beibei,
        [Display(Name = "拍拍")]
        Paipai,
        [Display(Name = "百度mall")]
        Baidu,
        [Display(Name = "楚楚街")]
        Chuchujie,
        [Display(Name = "国美在线")]
        Guomei,
        [Display(Name = "蘑菇街")]
        Mogujie,
        [Display(Name = "阿里巴巴")]
        Alibaba,
        [Display(Name = "建行善融商城")]
        Cbc,
        [Display(Name = "明星衣橱")]
        Hichao,
        [Display(Name = "萌店")]
        Mengdian,
        [Display(Name = "蜜芽宝贝")]
        Mia,
        [Display(Name = "人人店")]
        Renren,
        [Display(Name = "速卖通平台")]
        Aliexpress,
        [Display(Name = "尚品网")]
        Shangpin,
        [Display(Name = "好乐买")]
        Okbuy,
        [Display(Name = "拼多多")]
        Pinduoduo,
        [Display(Name = "洽客")]
        Qiake,
        [Display(Name = "网易秀品")]
        Wangyi,
        [Display(Name = "乐峰")]
        Lefeng,
    }

    public enum ExternalEnum
    {
        [Display(Name = "平台分类")]
        Platform,
        [Display(Name = "淘宝分类")]
        Taobao,
        [Display(Name = "Oracle分类")]//
        Oracle,
        [Display(Name = "京东")]
        JD,
        [Display(Name = "百盛ERP")]
        BaiSheng,
        [Display(Name = "商派ERP")]
        Shangpai,
    }
}
