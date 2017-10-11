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
        public string Code { get; set; }
        public string RCode { get; set; }
        public string AvatarUrl { get; set; }
        public string Gender { get; set; }
        public IEnumerable<OnlineStoreProfileViewModel> OnlineStoreProfiles { get; set; }
        public string Comments { get; set; }
    }

    public class OnlineStoreProfileViewModel
    {
        public int Id { get; set; }
        public string OnlineStoreType { get; set; }
        public int StaffId { get; set; }
        public string StaffCode { get; set; }
        public string StaffName { get; set; }
        public string Code { get; set; }
        public string Comments { get; set; }
    }
}
