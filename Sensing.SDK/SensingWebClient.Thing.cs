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
        private const string GetFinalThingsQuery = ThingBaseUrl + "/finals";


        private const string GetTCategoriesQuery = "tcategory/getall";

        public async Task<IEnumerable<ThingViewModel>> GetThings(int maxCount=300)
        {
            var absolutePath = $"{ServiceHost}/{GetThingsQuery}?{GetBasicNameValuesQueryString()}&pageSize={maxCount}";
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


        public async Task<IEnumerable<FinalThingViewModel>> GetFinalThings(int maxCount = 500)
        {
            var absolutePath = $"{ServiceHost}/{GetFinalThingsQuery}?{GetBasicNameValuesQueryString()}&pageSize={maxCount}";
            try
            {
                var pagedList = await SendRequestAsync<string, PagedList<FinalThingViewModel>>(HttpMethod.Get, absolutePath, null);
                return pagedList.Data;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("GetFinalThings:" + ex.InnerException);
            }
            return null;
        }



        

        public async Task<IEnumerable<TCategoryViewModel>> GetTCategories(int maxCount = 100)
        {
            var absolutePath = $"{ServiceHost}/{GetTCategoriesQuery}?{GetBasicNameValuesQueryString()}&pageSize={maxCount}";
            try
            {
                var pagedList = await SendRequestAsync<string, PagedList<TCategoryViewModel>>(HttpMethod.Get, absolutePath, null);
                return pagedList.Data;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("GetTCategories:" + ex.InnerException);
            }
            return null;
        }
    }
}
