using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hooters.ClientSDK.Contract
{
    public class DeviceInfo
    {
        public int Id { get; set; }
        public string Mac { get; set; }
        public string Name { get; set; }

        public string Type { get; set; }

        public string LocalIp { get; set; }

        public string Status { get; set; }

        public string LicenseInfo { get; set; }

        public string HardwareCode { get; set; }

        public List<CounterInfo> Counters { get; set; }
    }

    public class CounterInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<CounterTimeInfo> Times { get; set; }
    }

    public class CounterTimeInfo
    {
        public DateTime CollectingTime { get; set; }
        public long Total { get; set; }
        public int Increment { get; set; }
    }
}
