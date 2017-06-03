using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.SDK.Contract
{
    public class ApiStatus
    {
        public static string OK = "OK";
        public static string NG = "NG";
    }
    public class WebApiResult<T>
    {
        public string status { get; set; }
        public string message { get; set; }
        public string code { get; set; }
        public T data { get; set; }
    }
}
