using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensingSite.ClientSDK.Common
{
    public struct AppConfig
    {
        public static string ServiceUrl = ConfigurationManager.AppSettings["GameBaseUrl"];
        public static string ClientName = ConfigurationManager.AppSettings["ClientName"];
        public static string GameName = ConfigurationManager.AppSettings["GameName"];
        public static int WaitPrintTimeout = int.Parse(ConfigurationManager.AppSettings["WaitPrintTimeout"]);

    }
}
