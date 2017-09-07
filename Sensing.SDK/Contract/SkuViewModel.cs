using System;
using System.Collections.Generic;

namespace Sensing.SDK.Contract
{
    public class SkuSDKModel
    {
        public int Id { get; set; }

        public string ItemId { get; set; }

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

        public int LikeCount { get; set; }
        /// <summary>
        /// 万物总有属于他自己的关键字,让别人好找到它.
        /// </summary>
        public string Keywords { get; set; }

        public int GroupId { get; set; }

        public string PicUrl { get; set; }

        public bool HasSelfImage { get; set; }

        public string Description { get; set; }

        public int OrderNumber { get; set; }

        public string OuterId { get; set; }

        public string ColorName { get; set; }

        public string[] Tags { get; set; }

        public string FromType { get; set; }

        public IEnumerable<SkuOnlineStoreInfoViewModel> OnlineStoreInfos { get; set; }
    }

    public class SkuOnlineStoreInfoViewModel
    {
        public int Id { get; set; }

        public int SkuId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// 线上商场的Id.
        /// </summary>
        public string StoreId { get; set; }

        /// <summary>
        /// 线上商城的类型.
        /// </summary>
        public OnlineStore Type { get; set; }

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