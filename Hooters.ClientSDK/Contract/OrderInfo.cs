using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hooters.ClientSDK.Contract
{
    public class OrderInfo
    {
        public DateTime CollectingTime { get; set; }

        public double Total { get; set; }

        public int BuyerAge { get; set; }

        public string BuyerGender { get; set; }

        public string Details { get; set; }

        public string PosOrderId { get; set; }
    }
}
