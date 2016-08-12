using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensngGame.ClientSDK.Contract
{

    public class ActivityResult
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public ActivityData Data { get; set; }
    }

    public class ActivityData
    {
        public int Id { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 活动图片
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int GroupID { get; set; }


        /// <summary>
        /// 活动的首页url
        /// </summary>
        public string ActivityUrl { get; set; }


        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime OpenDate { get; set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 分享后浏览的次数（活动在我们平台的）
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// 分享后点赞的次数（活动在我们平台的）
        /// </summary>
        public int LikeCount { get; set; }

        /// <summary>
        /// 微信分享次数，活动在我们平台的，可提供次数据。
        /// </summary>
        public int ShareCount { get; set; }

        /// <summary>
        /// WeixinUserAction 表里的记录数 根据当前activityid
        /// </summary>
        public int UserActionCount { get; set; }

        public bool IsEnableWhiteUser { get; set; }


    }
}
