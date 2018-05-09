using Sensing.SDK.Contract;
using SensingStoreCloud.Devices.Dto.SensingDevice;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.SDK
{
    partial class SensingWebClient
    {
        /// <summary>
        /// Get all the things.
        /// </summary>
        private const string AppBaseUrl = "SensingDevice";
        private const string GetAppsQuery = AppBaseUrl + "/Apps";

        public async Task<List<DeviceSoftwareSdkModel>> GetApps()
        {
            var absolutePath = $"{ServiceHost}/{GetAppsQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var webResult = await SendRequestAsync<string, AjaxResponse<List<DeviceSoftwareSdkModel>>>(HttpMethod.Get, absolutePath,null);
                if(webResult.Success)
                {
                    return webResult.Result;
                }
                Console.WriteLine("GetApps:" + webResult.Error.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetApps:" + ex.InnerException);
            }
            return null;
        }
    }
}
