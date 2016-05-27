using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensngGame.ClientSDK.Contract
{
    public class UserInfoResult
    {
        public string Status { get; set; }
        public string ErrMessage { get; set; }
        public UserInfoData Data { get; set; }
    }
    public class UserActionResult
    {
        public string Status { get; set; }
        public string ErrMessage { get; set; }
        public UserActionData Data { get; set; }
    }

    public class UserDataResult
    {
        public string Status { get; set; }
        public string ErrMessage { get; set; }
        public UserData Data { get; set; }
    }

    public class UserAwardResult
    {
        public string Status { get; set; }
        public string ErrMessage { get; set; }
        public UserAwardData Data { get; set; }
    }

    public class UserAwardsResult
    {
        public string Status { get; set; }
        public string ErrMessage { get; set; }
        public List<UserAwardData> Data { get; set; }
    }

    public class UserActionsResult
    {
        public string Status { get; set; }
        public string ErrMessage { get; set; }
        public List<UserActionData> Data { get; set; }
    }
    public class UserInfosResult
    {
        public string Status { get; set; }
        public string ErrMessage { get; set; }
        public List<UserInfoData> Data { get; set; }
    }

    public class WhiteUsersResult
    {
        public string Status { get; set; }
        public string ErrMessage { get; set; }
        public List<WhiteUserData> Data { get; set; }
    }

    

    public class UserInfoData
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
    }

    public class WhiteUserData : UserInfoData
    {
        public string AwardSeqs { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string IdentityID { get; set; }

        public string CompanyName { get; set; }
    }
    public class UserData :UserInfoData
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string IdentityID { get; set; }

        public string CompanyName { get; set; }

        public bool IsSigned { get; set; }
    }

    public class UserAwardData:UserInfoData
    {
        public int AwardID { get; set; }

        public bool IsNotified { get; set; }

        public bool IsReceived { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string ReceiverName { get; set; }

        public string ExpressNO { get; set; }

        public string ExpressCompany { get; set; }

        public string ExpressImageUrl { get; set; }
    }

    public class UserActionData : UserInfoData
    {
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

        public DateTime ScanQrCodeTime { get; set; }

        public bool IsSigned { get; set; }
        public int AwardID { get; set; }
    }
}
