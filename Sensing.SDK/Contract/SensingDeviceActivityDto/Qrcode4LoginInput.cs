using Sensing.SDK.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SensingStoreCloud.Activity
{
    public enum ActionStatus
    {
        Start,
        Waitting,
        Playing,
        Done,
        AwardEdit,
        //未中奖，因为奖品已发完
        NotGetPrizeWithNoAward,
        //未中奖
        NotGetPrize
    }

    public interface ISensingDeviceGameInput
    {
         string SecurityKey { get; set; }
    }
    public class SensingDeviceGameInputBase : ISensingDeviceGameInput
    {
        [Required]
        public string SecurityKey { get; set; }
    }

    public class SensingDeviceGamePagedSortedFilteredInputBase: PagedSortedAndFilteredInputDto, ISensingDeviceGameInput
    {
        [Required]
        public string SecurityKey { get; set; }

        public string SoftwareCode { get; set; }

        public string ClientId { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "LastModificationTime";
            }
        }
    }

    public class Qrcode4LoginInput: SensingDeviceGameInputBase
    {
        public bool IsSendWeChatMsg { get; set; }
        public string Params { get; set; }
        public EnumQRStatus QrType { get; set; }
        public EnumSnsType SnsType { get; set; }
    }

    public enum EnumSnsType
    {
        Taobao,
        WeChat,
        Others,
    }

    public enum EnumQRStatus
    {
        /// <summary>
        /// 玩游戏之前生成此二维码
        /// </summary>
        [Display(Name = "游戏前扫码")]
        BeforeGame = 1,

        /// <summary>
        /// 玩游戏之后生成
        /// </summary>
        [Display(Name = "游戏后扫码")]
        AfterGame = 2,

        [Display(Name = "活动主页")]
        ActivityIntroduction,

        [Display(Name = "活动注册")]
        ActivityRegister,

        [Display(Name = "在线游戏")]
        OnlineGame,

        [Display(Name = "游戏结果")]
        GameResult,

        [Display(Name = "中奖主页")]
        Award
    }
    public class Qrcode4UsersInput : PagedSortedAndFilteredInputDto
    {
        [Required]
        [Range(0,double.MaxValue)]
        public long QrcodeId { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "LastModificationTime";
            }
        }
    }

    public class ActivityInput: PagedSortedAndFilteredInputDto
    {

    }


    public class PlayerDataInput: Qrcode4LoginInput
    { 
        //public IFormFile PlayerImage { get; set; }
        //public IFormFile PlayingImage { get; set; }
        public double? Score { get; set; }
        public ActionStatus? Status { get; set; }
    }

    public class PlayerActionDataInput : PlayerDataInput
    {
        [Required]
        [Range(0, double.MaxValue)]
        public long ActionId { get; set; }
    }

    public class SnsUserActionDataInput: PlayerActionDataInput
    {
        [Required]
        [Range(0, double.MaxValue)]
        public string OpenId { get; set; }
    }

    public class ActionDataInput : SensingDeviceGameInputBase
    {
        [Required]
        [Range(0, double.MaxValue)]
        public long ActionId { get; set; }
    }

    public class LotteryByAwardDataInput : SensingDeviceGameInputBase
    {
        [Required]
        [Range(0, double.MaxValue)]
        public long AwardId { get; set; }
    }
}
