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
        private const string IsFaceMemberQuery = FaceBaseUrl + "/IsFaceMember";

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

        public async Task<FaceMemberOutput> IsFaceMember(FaceInput input)
        {
            input.Subkey = _subKey;
            var absolutePath = $"{MainServiceApiHost}/{IsFaceMemberQuery}";
            try
            {
                var webResult = await SendRequestAsync<FaceInput, AjaxResponse<FaceMemberOutput>>(HttpMethod.Post, absolutePath, input);
                return webResult.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("IsFaceMember:" + ex.InnerException);
            }
            return null;
        }
    }
}
