using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoElement
{
    public class AppConfig
    {
        public static int AutoCloseDuration 
        {
            get
            {
                int time = 20;
                int.TryParse(ConfigurationManager.AppSettings["AutoCloseDuration"],out time);
                return time;
            }
        }

        public static int Distance
        {
            get
            {
                int distance;
                int.TryParse(ConfigurationManager.AppSettings["Distance"], out distance);
                if (distance == 0) distance = 3;
                return distance;
            }
        }

        public static string WeixinAppId
        {
            get
            {
                return ConfigurationManager.AppSettings["WeixinAppId"];
            }
        }

        public static string ActivityId
        {
            get
            {
                return ConfigurationManager.AppSettings["ActivityId"];
                
            }
        }

        public static int ScreenSave
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["ScreenSave"]);
            }
        }
    }
}
