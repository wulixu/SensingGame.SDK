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
        private const string CouponBaseUrl = "things";
        private const string GetCouponsQuery = ThingBaseUrl + "/getall";

        public async Task<IEnumerable<ThingViewModel>> GetCoupons(int maxCount=300)
        {
            var absolutePath = $"{ServiceHost}/{GetCouponsQuery}?{GetBasicNameValuesQueryString()}&pageSize={maxCount}";
            try
            {
                var pagedList = await SendRequestAsync<string,List<ThingViewModel>>(HttpMethod.Get, absolutePath,null);
                return pagedList;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("GetThings:" + ex.InnerException);
            }
            return null;
        }
    }
}
