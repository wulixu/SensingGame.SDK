using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.SDK.Contract
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public string Address { get; set; }

        public string SubscriptionKey { get; set; }

        public string ParentGroup { get; set; }
        public int ParentGroupId { get; set; }

        public string GroupType { get; set; }

        public int GroupTypeValue { get; set; }

        public DateTime Created { get; set; }
        public string Description { get; set; }

        public string EmailService { get; set; }

        public string EmailUserName { get; set; }
        public string EmailPassword { get; set; }
        public string WebAddressUrl { get; set; }
        public string Declaration { get; set; }
        public string QRCodeUrl { get; set; }
    }
}
