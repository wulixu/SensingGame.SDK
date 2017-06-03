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
    }

    public class DeviceViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Mac { get; set; }

        public int GroupId { get; set; }

        public bool IsLocked { get; set; }

        public int DeviceTypeId { get; set; }

        public string DeviceTypeName { get; set; }

        public string Address { get; set; }

        public string IntranetIP { get; set; }

        public string InternetIP { get; set; }

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
    }
}
