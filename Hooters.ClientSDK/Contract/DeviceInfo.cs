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

        public List<CounterInfo> Counters { get; set; }
    }

    public class CounterInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Increment { get; set; }
        public long Total { get; set; }
        public DateTime CollectingTime { get; set; }
    }
}
