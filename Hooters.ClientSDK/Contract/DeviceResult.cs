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
}
