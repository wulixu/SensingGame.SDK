using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sensing.SDK.Contract
{
    public enum AuditStatus
    {
        //当前什么状态都不是,如新建一个设备后就是None,为默认值.
        [Display(Name = "未上线")]
        None,
        //当前设备没有入组,处于
        [Display(Name = "下线")]
        Offline,
        //上线审核中...
        [Display(Name = "上线审核中")]
        OnlineAuditing,
        //审核通过,在线
        [Display(Name = "上线")]
        Online,
        //上线审核被拒绝.
        [Display(Name = "上线审核被拒绝")]
        OnlineAuditRejected,
        //下线审核中...
        [Display(Name = "下线审核中")]
        OfflineAuiting,
        //下线审核被拒绝.
        [Display(Name = "下线审核被拒绝")]
        OfflineAuditRejected
    }
    public class SkuSdkModel
    {
        public int Id { get; set; }

        public string ItemId { get; set; }

        public string SkuId { get; set; }

        public long Quantity { get; set; }

        public AuditStatus AuditStatus { get; set; }

        public string PropsName { get; set; }

        /// <summary>
        /// 事物都该有个名字来表示
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 在当今社会,任何事物都是可以明码标价的,难道不是!
        /// </summary>
        public double Price { get; set; }

        public int LikeCount { get; set; }
        /// <summary>
        /// 万物总有属于他自己的关键字,让别人好找到它.
        /// </summary>
        public string Keywords { get; set; }

        public string PicUrl { get; set; }

        public string Description { get; set; }

        public string OuterId { get; set; }

        public int OrderNumber { get; set; }

        public string[] Tags { get; set; }

        public string FromType { get; set; }

        public string ColorName { get; set; }

        public IEnumerable<SkuOnlineStoreInfoSdkModel> OnlineStoreInfos { get; set; }
    }
    public class SkuOnlineStoreInfoSdkModel
    {
        public int Id { get; set; }
        public int SkuId { get; set; }

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
        /// 线上商城的PID
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
}