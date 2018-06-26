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
        /// Post device data record.
        /// </summary>
        private const string GetDateMetaPhysicsQuery = "/DateMetaPhysics/GetDateMetaphysicsBySubkey";
        private const string GetMetaphysicsListQuery = "/MetaPhysics/GetMetaphysicsListBySubkey";

        public readonly static string RecommendServiceRelativePath = "r/";
        public readonly static string RecommendServiceApiHost = ServerBase + DeviceBigDataServiceRelativePath + Api_Relative_Path;
        public readonly static string RecommendServiceHost = ServerBase + DeviceBigDataServiceRelativePath;

        private const string RecommendPath = "r/api/services/app";

        public async Task<bool> GetDateMetaPhysics(IEnumerable<BehaviorRecord> records)
        {
            //api/services/app/BehaviorRecord/PostRecord
            var absolutePath = $"{ServerBase}{RecommendPath}{GetDateMetaPhysicsQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var result = await SendRequestAsync<IEnumerable<BehaviorRecord>, AjaxResponse<bool>>(HttpMethod.Post, absolutePath, records);
                return result.Success;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("PostBehaviorRecordsAsync:" + ex.InnerException);
            }
            return false;
        }


        public async Task<bool> GetMetaphysicsList(long? typeId)
        {
            //api/services/app/BehaviorRecord/PostRecord
            var absolutePath = $"{ServerBase}{RecommendPath}{GetMetaphysicsListQuery}?{GetBasicNameValuesQueryString()}&typeId={typeId}";
            try
            {
                var result = await SendRequestAsync<IEnumerable<BehaviorRecord>, AjaxResponse<bool>>(HttpMethod.Get, absolutePath, null);
                return result.Success;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("GetMetaphysicsList:" + ex.InnerException);
            }
            return false;
        }

    }
}
