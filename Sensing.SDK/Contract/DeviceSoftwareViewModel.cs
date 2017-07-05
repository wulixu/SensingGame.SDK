using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.SDK.Contract
{
    public class DeviceSoftwareViewModel
    {
        public int Id { get; set; }

        public int SoftwareId { get; set; }
        public int DeviceId { get; set; }
        public bool IsDefault { get; set; }
        public string CustomizeSetting { get; set; }
        public string GroupSoftwareSetting { get; set; }
        public string MaterialPacketUrl { get; set; }
        public SoftwareViewModel Software { get; set; }
    }

    public class SoftwareViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Owner { get; set; }
        public string Url { get; set; }
        public string LogoUrl { get; set; }
        public string TargetResolution { get; set; }
        public string LargeImageUrl { get; set; }
        public string VersionNumber { get; set; }
        public string PackageUrl { get; set; }
        public string FullLink { get; set; }

        public string Type { get; set; }
        public string GameType { get; set; }
        public string EnvType { get; set; }
        public string Setting { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string ExePath { get; set; }
    }
}
