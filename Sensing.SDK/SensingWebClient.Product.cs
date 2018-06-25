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
        private const string GetProductsQuery = ThingBaseUrl + "/GetProducts";

        private const string GetBrandsQuery = ThingBaseUrl + "/GetBrands";
        private const string GetTCategoriesQuery = ThingBaseUrl + "/GetProductCategories";
        private const string GetMatchesQuery = ThingBaseUrl + "/GetMatchInfos";
        private const string GetLikesQuery = ThingBaseUrl + "/GetLikeInfos";
        private const string GetCommentsQuery = ThingBaseUrl + "/GetProductComments";

        private const string MaxResultCount = "MaxResultCount";
        private const string SkipCount = "SkipCount";

        public async Task<PagedResultDto<ProductSdkModel>> GetProducts(int skipCount = 0,int maxCount=300)
        {
            var absolutePath = $"{MainServiceApiHost}/{GetProductsQuery}?{GetBasicNameValuesQueryString()}&{MaxResultCount}={maxCount}&{SkipCount}={skipCount}";
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

        public async Task<PagedResultDto<ProductCategorySDKModel>> GetProductCategories(int skipCount = 0,int maxCount = 500)
        {
            var absolutePath = $"{MainServiceApiHost}/{GetTCategoriesQuery}?{GetBasicNameValuesQueryString()}&{MaxResultCount}={maxCount}&{SkipCount}={skipCount}";
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


        public async Task<PagedResultDto<MatchInfoViewModel>> GetMatchInfos(int skipCount = 0, int maxCount = 1000)
        {
            var absolutePath = $"{MainServiceApiHost}/{GetMatchesQuery}?{GetBasicNameValuesQueryString()}&{MaxResultCount}={maxCount}&{SkipCount}={skipCount}";
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

        public async Task<PagedResultDto<LikeInfoViewModel>> GetLikeInfos(int skipCount = 0, int maxCount = 1000)
        {
            var absolutePath = $"{MainServiceApiHost}/{GetLikesQuery}?{GetBasicNameValuesQueryString()}&{MaxResultCount}={maxCount}&{SkipCount}={skipCount}";
            try
            {
                var webResult = await SendRequestAsync<string, AjaxResponse<PagedResultDto<LikeInfoViewModel>>>(HttpMethod.Get, absolutePath, null);

                    return webResult.Result;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetLikeInfos:" + ex.InnerException);
            }
            return null;
        }

        public async Task<PagedResultDto<ProductCommentModel>> GetProductComments(int skipCount = 0, int maxCount = 1000)
        {
            var absolutePath = $"{MainServiceApiHost}/{GetCommentsQuery}?{GetBasicNameValuesQueryString()}&{MaxResultCount}={maxCount}&{SkipCount}={skipCount}";
            try
            {
                var webResult = await SendRequestAsync<string, AjaxResponse<PagedResultDto<ProductCommentModel>>>(HttpMethod.Get, absolutePath, null);
                return webResult.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetProductComments:" + ex.InnerException);
            }
            return null;
        }

        public async Task<PagedResultDto<BrandDto>> GetBrands(int skipCount =0, int maxCount = 100)
        {
            var absolutePath = $"{MainServiceApiHost}/{GetBrandsQuery}?{GetBasicNameValuesQueryString()}&{MaxResultCount}={maxCount}&{SkipCount}={skipCount}";
            try
            {
                var webResult = await SendRequestAsync<string, AjaxResponse<PagedResultDto<BrandDto>>>(HttpMethod.Get, absolutePath, null);
                return webResult.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetBrands:" + ex.InnerException);
            }
            return null;
        }
    }
}
