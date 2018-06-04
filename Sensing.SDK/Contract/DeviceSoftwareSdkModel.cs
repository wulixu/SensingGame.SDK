using System;
using System.Collections.Generic;
using System.Text;

namespace Sensing.SDK.Contract
{
    public class DeviceSoftwareSdkModel
    {
        public int Id { get; set; }
        public bool IsDefault { get; set; }
        public string CustomizeSetting { get; set; }
        public string GroupSoftwareSetting { get; set; }
        public string MaterialPacketUrl { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Owner { get; set; }
        public string LogoUrl { get; set; }
        public string LargeImageUrl { get; set; }
        public string VersionNumber { get; set; }
        public string PackageUrl { get; set; }
        public string Type { get; set; }
        public string EnvType { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string ExePath { get; set; }
        public string ExtensionData { get; set; }
        public string Alias { get; set; }

    }
}
