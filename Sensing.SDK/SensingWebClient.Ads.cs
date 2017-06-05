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
        private const string AdsBaseUrl = "StoreSdk";
        private const string GetAdsQuery = AdsBaseUrl + "/Ads";

        public async Task<PagedList<DeviceAdsViewModel>> GetAds(int page = 1,int maxCount=300)
        {
            var absolutePath = $"{ServiceHost}/{GetAdsQuery}?{GetBasicNameValuesQueryString()}&pageSize={maxCount}&page={page}";
            try
            {
                var webResult = await SendRequestAsync<string,WebApiResult<PagedList<DeviceAdsViewModel>>>(HttpMethod.Get, absolutePath,null);
                if(webResult.status == ApiStatus.OK)
                {
                    return webResult.data;
                }
                Console.WriteLine("GetAds:" + webResult.message);
                return null;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("GetAds:" + ex.InnerException);
            }
            return null;
        }
    }
}
