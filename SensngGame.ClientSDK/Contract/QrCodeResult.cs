using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensngGame.ClientSDK.Contract
{
    public class QrCodeResult
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public QrCodeData Data { get; set; }
    }


    

    public class QrCodeData
    {
        public string QrCodeUrl { get; set; }
        public string QrCodeId { get; set; }
    }

}
