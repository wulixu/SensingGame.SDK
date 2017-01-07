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
        private const string ThingBaseUrl = "things";
        private const string GetThingsQuery = ThingBaseUrl + "/getall";

        public async Task<IEnumerable<ThingViewModel>> GetThings()
        {
            var absolutePath = $"{ServiceHost}/{GetThingsQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var pagedList = await SendRequestAsync<string,PagedList<ThingViewModel>>(HttpMethod.Get, absolutePath,null);
                return pagedList.Data;
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
