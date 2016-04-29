using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hooters.ClientSDK.Contract
{
    public class DeviceResult
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public DeviceInfo Data { get; set; }
    }

    public class GroupResult
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
    }

    public class OrderResult
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }

}
