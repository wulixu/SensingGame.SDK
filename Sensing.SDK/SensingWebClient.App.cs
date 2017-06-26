using Sensing.SDK.Contract;
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
        private const string AppBaseUrl = "StoreSdk";
        private const string GetAppsQuery = AppBaseUrl + "/Apps";

        public async Task<List<DeviceSoftwareViewModel>> GetApps()
        {
            var absolutePath = $"{ServiceHost}/{GetAppsQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var webResult = await SendRequestAsync<string,WebApiResult<List<DeviceSoftwareViewModel>>>(HttpMethod.Get, absolutePath,null);
                if(webResult.status == ApiStatus.OK)
                {
                    return webResult.data;
                }
                Console.WriteLine("GetApps:" + webResult.message);
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
