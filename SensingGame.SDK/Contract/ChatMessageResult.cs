using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensngGame.ClientSDK.Contract
{
    public class ChatMessageResult
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<ChartMessage> Data { get; set; }
    }

    public class ChartMessage
    {
        public int Id { get; set; }
        public string Message { get; set; }
    }

   
}
