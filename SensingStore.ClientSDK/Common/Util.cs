using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensingSite.ClientSDK.Common
{
    public class MediaType
    {
        public static string Json = "application/json";
        public static string Xml = "application/xml";
        public static string Html = "text/html";
    }

    public class Paths
    {
        public static string Base = AppConfig.ServiceUrl;
        public static string GameInfoBase = Base + "/gameinfos/";
        public static string GameInfos = Base + "/gameinfos/addrange";
        public static string AddInteractiveLogs = Base + "/InteractiveLogs/AddInteractiveLogList/";
        public static string AddInteractiveUserLogs = Base + "/InteractiveUserLogs/AddInteractiveUserLogs/";
        public static string AddFace = Base + "/faces";
        public static string AddVipData = Base + "/faces/vip";
    }
}
