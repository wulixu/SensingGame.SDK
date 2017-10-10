using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.SDK.Contract
{
    public class RegisterDeviceViewModel
    {
        public string Name { get; set; }

        public string IntranetIP { get; set; }

        public string InternetIP { get; set; }

        public string Mac { get; set; }

        public string Address { get; set; }

        public int DeviceTypeId { get; set; }

        public string LicenseInfo { get; set; }

        public string HardwareCode { get; set; }

        public double Resolution_Width { get; set; }

        public double Resolution_Height { get; set; }

        public string OS { get; set; }
    }

    public class DeviceSdkModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Mac { get; set; }

        public int GroupId { get; set; }

        public GroupSdkModel Group { get; set; }

        public bool IsLocked { get; set; }

        public int DeviceTypeId { get; set; }

        public string DeviceTypeName { get; set; }

        public string Address { get; set; }

        public string IntranetIP { get; set; }

        public string InternetIP { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// 设备是否已被注册，如果已注册，另外的机器不能再注册. 同时管理员可以清空，允许其它设备进行注册.
        /// </summary>
        public bool IsRegistered { get; set; }

        public string AuditStatus { get; set; }

        public string LicenseInfo { get; set; }

        public string HardwareCode { get; set; }

        public DateTime? ShutdownTime { get; set; }

        public double Resolution_Width { get; set; }

        public double Resolution_Height { get; set; }

        public DateTime? HeartBeatTime { get; set; }

        /// <summary>
        /// 访问此设备的唯一凭证，通过Subkey可调用平台api，拿到设备相关的信息，如,ads,apps,products等.同时可用户注册.
        /// </summary>
        public string SubKey { get; set; }
        public string OS { get; set; }

        public OnlineStore OnlineTrafficTarget { get; set; }
    }

    public class GroupSdkModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string Address { get; set; }
        public string GroupType { get; set; }
        public int GroupTypeValue { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
        public string OmniStoreId { get; set; }
    }
}
