using Sensing.SDK.Contract;
using Sensing.SDK.Contract.Faces;
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
        private const string PostFaceRecordQuery = "/BehaviorRecord/PostFaceRecord";
        private string DeviceHeartBeatQuery = "/BehaviorRecord/DeviceHeartBeat";
        private string GetFaceRecordsQuery = "/BehaviorRecord/GetFaceRecords";


        public readonly static string DeviceBigDataServiceRelativePath = "d/";
        public readonly static string DeviceBigDataServiceApiHost = ServerBase + DeviceBigDataServiceRelativePath + Api_Relative_Path;
        public readonly static string DeviceBigDataServiceHost = ServerBase + DeviceBigDataServiceRelativePath;

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

        public async Task<bool> PostFaceRecordAsync(FaceRecord record)
        {
            //api/services/app/BehaviorRecord/PostRecord
            var absolutePath = $"{ServerBase}{DeviceBigDataPath}{PostFaceRecordQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var result = await SendRequestAsync<FaceRecord, AjaxResponse<bool>>(HttpMethod.Post, absolutePath, record);
                return result.Success;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("PostFaceRecordAsync:" + ex.InnerException);
            }
            return false;
        }

        public async Task<bool> DeviceHeartBeatAsync(DeviceHeartBeatInput device)
        {
            var absolutePath = $"{ServerBase}{DeviceBigDataPath}/{DeviceHeartBeatQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var result = await SendRequestAsync<DeviceHeartBeatInput, AjaxResponse<bool>>(HttpMethod.Post, absolutePath, device);
                if (result != null)
                {
                    return result.Success;
                }
                return false;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("RegisterDevice:" + ex.InnerException);
            }
            return false;
        }

        public async Task<PagedResultDto<FaceRecordOutput>> GetFaceRecordsAsync(FaceRecordInput input)
        {
            //api/services/app/BehaviorRecord/PostRecord
            var absolutePath = $"{ServerBase}{DeviceBigDataPath}{GetFaceRecordsQuery}?{GetBasicNameValuesQueryString()}&sorting={input.Sorting}&maxResultCount={input.MaxResultCount}&skipCount={input.SkipCount}&collectionStartTime={input.CollectionStartTime}&collectionEndTime={input.CollectionEndTime}";
            try
            {
                var result = await SendRequestAsync<string, AjaxResponse<PagedResultDto<FaceRecordOutput>>>(HttpMethod.Get, absolutePath, null);
                return result.Result;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("GetFaceRecordsAsync:" + ex.InnerException);
            }
            return null;
        }

    }
}
