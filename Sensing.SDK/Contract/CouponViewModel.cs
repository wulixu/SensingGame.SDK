using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.SDK.Contract
{
    public class CouponViewModel
    {
        public int Id { get; set; }

        /// <summary>
        /// 淘宝数据库里的唯一id
        /// </summary>
        public int Coupon_id { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        /// <summary>
        /// 面额
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 优惠券活动ID 应该是模板id
        /// </summary>
        public string spread_id { get; set; }

        /// <summary>
        /// 优惠券名称
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 优惠券图片
        /// </summary>
        public string pictures { get; set; }


        /// <summary>
        /// 优惠券生效时间
        /// </summary>
        public DateTime start_time { get; set; }

        /// <summary>
        /// 优惠券的截止日期
        /// </summary>
        public DateTime end_time { get; set; }

        /// <summary>
        /// 优惠券地址
        /// </summary>
        public string Url { get; set; }
    }
}
