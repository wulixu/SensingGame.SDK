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
        private const string FaceBaseUrl = "SensingDevice";
        private const string GetGoodsByFacesQuery = FaceBaseUrl + "/GetRecommendsByFaces";

        public async Task<FaceRecommnedsOutput> GetRecommendsByFaces(FacesRecommendsInput input)
        {
            input.Subkey = _subKey;
            var absolutePath = $"{MainServiceApiHost}/{GetGoodsByFacesQuery}";
            try
            {
                var webResult = await SendRequestAsync<FacesRecommendsInput, AjaxResponse<FaceRecommnedsOutput>>(HttpMethod.Post, absolutePath, input);
                return webResult.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetRecommendsByFaces:" + ex.InnerException);
            }
            return null;
        }
    }
}
