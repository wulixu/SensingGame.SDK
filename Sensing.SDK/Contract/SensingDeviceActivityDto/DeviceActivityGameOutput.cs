using System;
using System.Collections.Generic;
using System.Text;

namespace SensingStoreCloud.Activity
{
    public enum SnsActivityAuthorizationType
    {
        Follow,
        Web,
        None
    }

    public class DeviceActivityGameOutput
    {
        public int TenantId { get; set; }
        /// <summary>
        /// 活动游戏名称.
        /// </summary>
        public string Name { get; set; }
        public long? DeviceId { get; set; }
        public long SoftwareId { get; set; }
        public long DispatchedSoftwareId { get; set; }
        //public DispatchedSoftware DispatchedSoftware { get; set; }
        public long ActivityId { get; set; }
        public virtual ActivityOutput Activity { get; set; }
        public bool IsShareAction { get; set; }

        public long? DispatchedActivityId { get; set; }

        /// <summary>
        /// 该活动中游戏最大支持人数
        /// </summary>
        public int SupportedPlayersCount { get; set; }

        /// <summary>
        /// 线上模板的背景图片
        /// </summary>
        public string OnlineBackground { get; set; }

        /// <summary>
        /// 线上模板的前景图片
        /// </summary>
        public string OnlineForeground { get; set; }

        /// <summary>
        /// 线下游戏的背景图片
        /// </summary>
        public string OfflineBackground { get; set; }

        /// <summary>
        /// 线下游戏前景图片
        /// </summary>
        public string OfflineForeground { get; set; }

        /// <summary>
        /// 用户自定义游戏素材zip路径
        /// </summary>
        public string MaterialPacketUrl { get; set; }

        /// <summary>
        /// 一个用户多长时间内最多玩几次
        /// </summary>
        public int MaxPlayCountPerUser { get; set; }
        public int MinPlayTimeDiff { get; set; }

        /// <summary>
        /// 活动关联的游戏是否已经开始
        /// </summary>
        public bool IsGameStarted { get; set; }

        public DateTime? LastGameOverTime { get; set; }

        /// <summary>
        /// 某些特定活动扫码前需要做一些check
        /// </summary>
        public bool IsNeedCheckBeforScanQRCode { get; set; }

        /// <summary>
        /// 用户直接访问特定活动游戏
        /// </summary>
        public string SecurityKey { get; set; }

        public SnsActivityAuthorizationType WeChatAuthorizationType { get; set; }

        public SnsActivityAuthorizationType TaobaoAuthorizationType { get; set; }
    }
    public class ActivityOutput
    {
        /// <summary>
        /// 所属的租户 Id
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 所属的组织 Id
        /// </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 活动图片
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// 活动线下地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 活动的首页url
        /// </summary>
        public string ActivityUrl { get; set; }

        /// <summary>
        /// 活动Logo的url
        /// </summary>
        public string ActivityLogoPath { get; set; }

        /// <summary>
        /// 是否发送中奖消息
        /// </summary>
        public bool IsSendAwardMessage { get; set; }

        public long? BeforeWeChatMessageId { get; set; }

        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime? OpenDate { get; set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 分享后浏览的次数（活动在我们平台的）
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// 分享后点赞的次数（活动在我们平台的）
        /// </summary>
        public int LikeCount { get; set; }

        public long? ActivityShareId { get; set; }

        //public virtual ActivityShare ActivityShare { get; set; }

        /// <summary>
        /// 公众号AppId
        /// </summary>
        public string WeChatAppID { get; set; }

        /// <summary>
        /// Taobao卖家的Id
        /// </summary>
        public string TaobaoSellerID { get; set; }

        /// <summary>
        /// 是否启用白名单
        /// </summary>
        public bool IsEnableWhiteUser { get; set; }

        ///// <summary>
        ///// 活动模板
        ///// </summary>
        //[Display(Name = nameof(rs.ActivityHtmlTemplateID), ResourceType = typeof(rs))]
        //public int? ActivityHtmlTemplateID { get; set; }

        //[Display(Name = nameof(rs.ActivityHtmlTemplate), ResourceType = typeof(rs))]
        //public virtual HtmlTemplate ActivityHtmlTemplate { get; set; }

        /// <summary>
        /// 活动主办方
        /// </summary>
        public string Organizer { get; set; }

        /// <summary>
        /// 是否允许某一个人重复中奖
        /// </summary>
        public bool IsAllowedRepeatAward { get; set; }

        /// <summary>
        /// 是否需要填写中奖信息，如地址姓名电话等。
        /// </summary>
        public bool IsNeedAwardInfo { get; set; }

        /// <summary>
        /// 活动规则
        /// </summary>
        public string Rules { get; set; }

        /// <summary>
        /// 活动执行时间
        /// </summary>
        public DateTime? ActivityExcutedDate { get; set; }

        /// <summary>
        /// 该活动是否是模板
        /// </summary>
        public bool IsTemplate { get; set; }

        /// <summary>
        /// 图标路径 更多用处是在创建活动选模板的时候
        /// </summary>
        public string IconPath { get; set; }

        /// <summary>
        /// 活动描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 次活动需要注册，
        /// todo:william,可以考虑ActivityGame里面增加这个变量.
        /// </summary>
        public bool IsNeedRegister { get; set; }

        /// <summary>
        /// 活动的所有者
        /// </summary>
        public string OwnerName { get; set; }

        /// <summary>
        /// 表明此活动是否是正式使用，或者是试用.
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// 活动联系人
        /// </summary>
        public string Contactor { get; set; }

        public string OuterId { get; set; }

        /// <summary>
        /// 活动联系人电话
        /// </summary>
        public string ContactorPhone { get; set; }
    }
}