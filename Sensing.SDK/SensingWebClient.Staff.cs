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
        private const string StaffBaseUrl = "SensingDevice";
        private const string GetStaffsQuery = StaffBaseUrl + "/GetStaffs";

        public async Task<PagedResultDto<StaffSdkModel>> GetStaffs(int skipCount = 0,int maxCount=300)
        {
            var absolutePath = $"{MainServiceApiHost}/{GetStaffsQuery}?{GetBasicNameValuesQueryString()}&{MaxResultCount}={maxCount}&{SkipCount}={skipCount}";
            try
            {
                var webResult = await SendRequestAsync<string, AjaxResponse<PagedResultDto<StaffSdkModel>>>(HttpMethod.Get, absolutePath, null);
                return webResult.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAds:" + ex.InnerException);
            }
            return null;
        }
    }
}
