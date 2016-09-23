using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SensingSite.ClientSDK.Common
{
    public class MacIPHelper
    {
        /// <summary>
        /// 获取本机MAC地址
        /// </summary>
        /// <returns>MAC数据 string</returns>
        public static string GetClientMac()
        {
            string macAddresses = "";
#if !WINDOWS_UWP
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
#endif
            return macAddresses;
        }


    }
}
