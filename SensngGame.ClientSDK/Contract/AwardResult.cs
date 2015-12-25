using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensngGame.ClientSDK.Contract
{
    public class AwardsResult
    {
        public string Status { get; set; }
        public string ErrMessage { get; set; }
        public List<AwardData> Data { get; set; }
    }

    public class AwardData
    {
        public int Id;
        /// <summary>
        /// 活动id
        /// </summary>
        public int ActivityID { get; set; }

        /// <summary>
        /// 计划奖品数
        /// </summary>
        public int PlanQty { get; set; }

        /// <summary>
        /// 已发奖品数
        /// </summary>
        public int ActualQty { get; set; }

        /// <summary>
        /// 奖项名称 如 1等奖，2等奖
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 奖品等级 如 1，2，对应上面的 1等奖，2等奖，以后排序用
        /// </summary>
        public int AwardSeq { get; set; }

        /// <summary>
        /// 奖品名称，如 ipad，索尼照相机
        /// 
        /// </summary>
        public string AwardProduct { get; set; }

        /// <summary>
        /// 奖品图片
        /// </summary>
        public string AwardImagePath { get; set; }
    }
}
