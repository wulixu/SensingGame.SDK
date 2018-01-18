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
        private const string DeviceBaseUrl = "SensingDevice";
        private const string RegisterDeviceQuery = DeviceBaseUrl + "/RegisterDevice";
        private const string DeviceHeartBeatQuery = DeviceBaseUrl + "/DeviceHeartBeat";

        private const string GroupInfoQuery = DeviceBaseUrl + "/GroupInfo";
        private const string DeviceInfoQuery = DeviceBaseUrl + "/DeviceInfo";
        private const string LoginInfoQuery = DeviceBaseUrl + "/Login";

        public async Task<WebApiResult<DeviceSdkModel>> RegisterDeviceAsyn(RegisterDeviceViewModel device)
        {
            var absolutePath = $"{ServiceHost}/{RegisterDeviceQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var pagedList = await SendRequestAsync<RegisterDeviceViewModel, WebApiResult<DeviceSdkModel>>(HttpMethod.Post, absolutePath, device);
                return pagedList;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("RegisterDevice:" + ex.InnerException);
            }
            return null;
        }

        public async Task<WebApiResult<DeviceSdkModel>> GetDeviceInfo()
        {
            var absolutePath = $"{ServiceHost}/{DeviceInfoQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var pagedList = await SendRequestAsync<string, WebApiResult<DeviceSdkModel>>(HttpMethod.Get, absolutePath, null);
                return pagedList;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("GetDeviceInfo:" + ex.InnerException);
            }
            return null;
        }

        public async Task<WebApiResult<GroupViewModel>> GetGroupInfo()
        {
            var absolutePath = $"{ServiceHost}/{GroupInfoQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var groupResult = await SendRequestAsync<string, WebApiResult<GroupViewModel>>(HttpMethod.Get, absolutePath, null);
                return groupResult;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("GetGroupInfo:" + ex.InnerException);
            }
            return null;
        }

        public async Task<WebApiResult<DeviceStaffLoginResultViewModel>> DeviceLogin(DeviceSdkLoginViewModel loginVM)
        {
            var absolutePath = $"{ServiceHost}/{LoginInfoQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var loginResult = await SendRequestAsync<string, WebApiResult<DeviceStaffLoginResultViewModel>>(HttpMethod.Post, absolutePath, null);
                return loginResult;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("DeviceLogin:" + ex.InnerException);
            }
            return null;
        }
    }
}
