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
        private const string CouponBaseUrl = "taobao";
        private const string GetCouponsQuery = CouponBaseUrl + "/All";

        public async Task<IEnumerable<CouponViewModel>> GetCoupons(int maxCount=300)
        {
            var absolutePath = $"{ServiceHost}/{GetCouponsQuery}?{GetBasicNameValuesQueryString()}&pageSize={maxCount}";
            try
            {
                var couponList = await SendRequestAsync<string,List<CouponViewModel>>(HttpMethod.Get, absolutePath,null);
                return couponList;
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
