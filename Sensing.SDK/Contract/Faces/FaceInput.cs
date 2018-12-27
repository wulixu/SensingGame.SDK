using SensingStoreCloud.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.SDK.Contract.Faces
{
    public class FaceDataInput : SensingDeviceGameInputBase
    {
        /// <summary>
        /// 
        /// </summary>
        public byte[] FaceBytes { get; set; }

        public string FaceUrl { get; set; }
    }


    public class UserFaceDataInput : FaceDataInput
    {
        public string OpenId { get; set; }
        public EnumSnsType SnsType { get; set; }
    }
}
