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
        private const string ThingBaseUrl = "SensingDevice";
        private const string GetProductsQuery = ThingBaseUrl + "/Products";


        private const string GetTCategoriesQuery = ThingBaseUrl + "/ProductCategories";
        private const string GetMatchesQuery = ThingBaseUrl + "/MatchInfos";
        private const string GetLikesQuery = ThingBaseUrl + "/LikeInfos";

        public async Task<PagedResultDto<ProductSdkModel>> GetProducts(int page = 1,int maxCount=300)
        {
            var absolutePath = $"{ServiceHost}/{GetProductsQuery}?{GetBasicNameValuesQueryString()}&pageSize={maxCount}&page={page}";
            try
            {
                var webResult = await SendRequestAsync<string,AjaxResponse<PagedResultDto<ProductSdkModel>>>(HttpMethod.Get, absolutePath,null);
                return webResult.Result;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("GetProducts:" + ex.InnerException);
            }
            return null;
        }

        public async Task<PagedResultDto<ProductCategorySDKModel>> GetProductCategories(int page = 1,int maxCount = 500)
        {
            var absolutePath = $"{ServiceHost}/{GetTCategoriesQuery}?{GetBasicNameValuesQueryString()}&pageSize={maxCount}&page={page}";
            try
            {
                var webResult = await SendRequestAsync<string, AjaxResponse<PagedResultDto<ProductCategorySDKModel>>>(HttpMethod.Get, absolutePath, null);

                return webResult.Result;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("GetProductCategories:" + ex.InnerException);
            }
            return null;
        }


        public async Task<PagedResultDto<MatchInfoViewModel>> GetMatchInfos(int page = 1, int maxCount = 1000)
        {
            var absolutePath = $"{ServiceHost}/{GetMatchesQuery}?{GetBasicNameValuesQueryString()}&pageSize={maxCount}&page={page}";
            try
            {
                var webResult = await SendRequestAsync<string, AjaxResponse<PagedResultDto<MatchInfoViewModel>>>(HttpMethod.Get, absolutePath, null);

                return webResult.Result;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("GetMatchInfos:" + ex.InnerException);
            }
            return null;
        }

        public async Task<PagedResultDto<LikeInfoViewModel>> GetLikeInfos(int page = 1, int maxCount = 1000)
        {
            var absolutePath = $"{ServiceHost}/{GetLikesQuery}?{GetBasicNameValuesQueryString()}&pageSize={maxCount}&page={page}";
            try
            {
                var webResult = await SendRequestAsync<string, AjaxResponse<PagedResultDto<LikeInfoViewModel>>>(HttpMethod.Get, absolutePath, null);

                    return webResult.Result;
                
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("GetLikeInfos:" + ex.InnerException);
            }
            return null;
        }
    }
}
