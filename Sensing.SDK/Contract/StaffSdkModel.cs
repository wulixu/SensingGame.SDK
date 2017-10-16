using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.SDK.Contract
{
    public class StaffSdkModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RFIDCode { get; set; }
        public string Code { get; set; }
        public string RCode { get; set; }
        public string AvatarUrl { get; set; }
        public string Gender { get; set; }
        public IEnumerable<OnlineStoreProfileSdkModel> OnlineStoreProfiles { get; set; }
    }

    public class OnlineStoreProfileSdkModel
    {
        public string OnlineStoreType { get; set; }
        public string Code { get; set; }
    }
}
