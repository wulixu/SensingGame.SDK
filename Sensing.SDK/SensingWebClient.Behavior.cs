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
        private const string PostBehaviorQuery = "/BehaviorRecord/PostRecord";
        private const string PostDeviceStatusQuery = "/BehaviorRecord/PostDeviceStatusRecord";
        private const string DeviceBigDataPath = "d/api/services/app";

        public async Task<bool> PostBehaviorRecordsAsync(IEnumerable<BehaviorRecord> records)
        {
            //api/services/app/BehaviorRecord/PostRecord
            var absolutePath = $"{ServerBase}{DeviceBigDataPath}{PostBehaviorQuery}?{GetBasicNameValuesQueryString()}";
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

        public async Task<bool> PostDeviceStatusRecordAsync(IEnumerable<DeviceStatusInput> status)
        {
            var absolutePath = $"{ServerBase}{DeviceBigDataPath}{PostDeviceStatusQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var result = await SendRequestAsync<IEnumerable<DeviceStatusInput>, AjaxResponse<bool>>(HttpMethod.Post, absolutePath, status);
                return result.Success;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("PostDeviceStatusRecordAsync:" + ex.InnerException);
            }
            return false;
        }
    }
}
