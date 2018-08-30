using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.SDK
{
    partial class SensingWebClient
    {
        /// <summary>
        /// Post device data record.
        /// </summary>
        private const string CreateQrcode4LoginQuery = "/BehaviorRecord/PostRecord";
        //private const string PostDeviceStatusQuery = "/BehaviorRecord/PostDeviceStatusRecord";

        public readonly static string ActivityServiceRelativePath = "g/";
        public readonly static string ActivityServiceApiHost = ServerBase + ActivityDataPath + Api_Relative_Path;
        public readonly static string ActivityServiceHost = ServerBase + ActivityDataPath;

        private const string ActivityDataPath = "g/api/";
        //public async Task<FaceRecommnedsOutput> CreateQrcode4Login(FacesRecommendsInput input)
        //{
        //    input.Subkey = _subKey;
        //    var absolutePath = $"{MainServiceApiHost}/{GetGoodsByFacesQuery}";
        //    try
        //    {
        //        var webResult = await SendRequestAsync<FacesRecommendsInput, AjaxResponse<FaceRecommnedsOutput>>(HttpMethod.Post, absolutePath, input);
        //        return webResult.Result;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("GetRecommendsByFaces:" + ex.InnerException);
        //    }
        //    return null;
        //}


        //public async Task<FaceRecommnedsOutput> PostData4ActionQrcode(FacesRecommendsInput input)
        //{
        //    input.Subkey = _subKey;
        //    var absolutePath = $"{MainServiceApiHost}/{GetGoodsByFacesQuery}";
        //    try
        //    {
        //        var webResult = await SendRequestAsync<FacesRecommendsInput, AjaxResponse<FaceRecommnedsOutput>>(HttpMethod.Post, absolutePath, input);
        //        return webResult.Result;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("GetRecommendsByFaces:" + ex.InnerException);
        //    }
        //    return null;
        //}
    }
}
