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

    public class UserData
    {
        public string AppID { get; set; }
        public string City { get; set; }
        public string HeadImagUrl { get; set; }
        public string NickName { get; set; }
        public string OpenId { get; set; }

        public string QrCodeId { get; set; }
    }
}
