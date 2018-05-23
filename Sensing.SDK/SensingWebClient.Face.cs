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
        private const string GetGoodsByFacesQuery = FaceBaseUrl + "/GetRecommendedGoodsByFaces";

        public async Task<string> GetRecommendedGoodsByFaces(FacesRecommendGoodsInput input)
        {
            input.Subkey = _subKey;
            var absolutePath = $"{ServiceHost}/{GetGoodsByFacesQuery}";
            try
            {
                var webResult = await SendRequestAsync<FacesRecommendGoodsInput, AjaxResponse<string>>(HttpMethod.Post, absolutePath, input);
                return webResult.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetRecommendedGoodsByFaces:" + ex.InnerException);
            }
            return null;
        }
    }
}
