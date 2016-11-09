using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensingStore.ClientSDK.Contract
{
    public class DeviceResult
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public DeviceInfo Data { get; set; }
    }

    public class DeviceInfo
    {
        public int Id { get; set; }
        public string Mac { get; set; }
        public string Name { get; set; }

        public string Type { get; set; }

        public string IP { get; set; }

        public DeviceStatus DeviceStatus { get; set; }

    }

    public enum DeviceStatus
    {
        Running,
        Stopped
    }
}
