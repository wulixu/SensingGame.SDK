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
        private const string CouponsBaseUrl = "SensingDevice";

        private const string GetCouponsQuery = CouponsBaseUrl + "/Coupons";

        public async Task<PagedList<CouponViewModel>> GetCoupons(int page = 1,int maxCount=300)
        {
            var absolutePath = $"{ServiceHost}/{GetCouponsQuery}?{GetBasicNameValuesQueryString()}&pageSize={maxCount}&page={page}";
            try
            {
                var webResult = await SendRequestAsync<string, WebApiResult<PagedList<CouponViewModel>>>(HttpMethod.Get, absolutePath, null);
                if (webResult.status == ApiStatus.OK)
                {
                    return webResult.data;
                }
                Console.WriteLine("GetCoupons:" + webResult.message);
                return null;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("GetCoupons:" + ex.InnerException);
            }
            return null;
        }
    }
}
