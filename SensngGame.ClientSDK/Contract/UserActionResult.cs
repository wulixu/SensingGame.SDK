using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensngGame.ClientSDK.Contract
{
    public class UserActionResult
    {
        public string Status { get; set; }
        public string ErrMessage { get; set; }
        public UserData Data { get; set; }
    }

    public class UserActionsResult
    {
        public string Status { get; set; }
        public string ErrMessage { get; set; }
        public List<UserData> Data { get; set; }
    }

    public class UserData
    {
        public int Id { get; set; }
        public string Openid { get; set; }
        public string Nickname { get; set; }
        public int Sex { get; set; }
        public string Language { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string Headimgurl { get; set; }
        public string Unionid { get; set; }
        public string Remark { get; set; }
        public int WeixinGroupid { get; set; }
        public string qrCodeId { get; set; }
        public int ActionId { get; set; }
        public int Score { get; set; }
        public string PostUrl { get; set; }
        public string GameImage { get; set; }
        public string PlayerImage { get; set; }
        public int PlayerAge { get; set; }
        public int ShareCount { get; set; }
        public int ViewCount { get; set; }
        public int LikeCount { get; set; }

        public bool IsSigned { get; set; }
        public int AwardID { get; set; }
    }
}
