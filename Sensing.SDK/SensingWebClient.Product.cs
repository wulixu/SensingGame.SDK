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
        private const string ThingBaseUrl = "StoreSdk";
        private const string GetProductsQuery = ThingBaseUrl + "/Products";


        private const string GetTCategoriesQuery = ThingBaseUrl + "/ProductCategories";

        public async Task<PagedList<ProductSDKModel>> GetProducts(int page = 1,int maxCount=300)
        {
            var absolutePath = $"{ServiceHost}/{GetProductsQuery}?{GetBasicNameValuesQueryString()}&pageSize={maxCount}&page={page}";
            try
            {
                var webResult = await SendRequestAsync<string,WebApiResult<PagedList<ProductSDKModel>>>(HttpMethod.Get, absolutePath,null);
                if(webResult.status == ApiStatus.OK)
                {
                    return webResult.data;
                }
                Console.WriteLine("GetProducts:" + webResult.message);
                return null;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("GetThings:" + ex.InnerException);
            }
            return null;
        }



        

        public async Task<IEnumerable<ProductCategorySDKModel>> GetProductCategories(int maxCount = 300)
        {
            var absolutePath = $"{ServiceHost}/{GetTCategoriesQuery}?{GetBasicNameValuesQueryString()}&pageSize={maxCount}";
            try
            {
                var webResult = await SendRequestAsync<string, WebApiResult<PagedList<ProductCategorySDKModel>>>(HttpMethod.Get, absolutePath, null);
                if (webResult.status == ApiStatus.OK)
                {
                    return webResult?.data.Data;
                }
                return null;
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
