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
        /// The qrcode4login.
        /// </summary>
        private const string PostBehaviorQuery = "/Behavior";

        //public async Task<bool> PostBehaviorRecordsAsync(IEnumerable<BehaviorRecord> records)
        //{
        //    var absolutePath = $"{ServiceHost}/{PostBehaviorQuery}?{GetBasicNameValuesQueryString()}";
        //    try
        //    {
        //        var successed = await SendRequestAsync<IEnumerable<BehaviorRecord>, bool>(HttpMethod.Post, absolutePath, records);
        //        return successed;
        //    }
        //    catch (Exception ex)
        //    {
        //        //logger.Error("PostBehaviorRecordsAsync", ex);
        //        Console.WriteLine("PostBehaviorRecordsAsync:" + ex.InnerException);
        //    }
        //    return false;
        //}
    }
}
