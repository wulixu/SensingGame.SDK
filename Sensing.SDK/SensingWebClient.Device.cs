using Sensing.SDK.Contract;
using SensingStoreCloud.Devices.Dto;
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

        public async Task<DeviceOutput> RegisterDeviceAsyn(RegisterDeviceInput device)
        {
            var absolutePath = $"{ServiceHost}/{RegisterDeviceQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var pagedList = await SendRequestAsync<RegisterDeviceInput, AjaxResponse<DeviceOutput>>(HttpMethod.Post, absolutePath, device);
                if(pagedList.Success)
                {
                    return pagedList.Result;
                }
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("RegisterDevice:" + ex.InnerException);
            }
            return null;
        }

        public async Task<bool> DeviceHeartBeatAsync(DeviceHeartBeatInput device)
        {
            var absolutePath = $"{ServiceHost}/{DeviceHeartBeatQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var result = await SendRequestAsync<DeviceHeartBeatInput, AjaxResponse<bool>>(HttpMethod.Post, absolutePath, device);
                if(result != null)
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

        public async Task<DeviceOutput> GetDeviceInfo()
        {
            var absolutePath = $"{ServiceHost}/{DeviceInfoQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var pagedList = await SendRequestAsync<string, AjaxResponse<DeviceOutput>>(HttpMethod.Get, absolutePath, null);
                return pagedList.Result;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("GetDeviceInfo:" + ex.InnerException);
            }
            return null;
        }

        public async Task<TenantAndOrganizationUnitOutput> GetTenantAndOrganizationUnitInfo()
        {
            var absolutePath = $"{ServiceHost}/{GroupInfoQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var groupResult = await SendRequestAsync<string, AjaxResponse<TenantAndOrganizationUnitOutput>>(HttpMethod.Get, absolutePath, null);
                return groupResult.Result;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("GetGroupInfo:" + ex.InnerException);
            }
            return null;
        }

        public async Task<DeviceStaffLoginResultViewModel> DeviceLogin(DeviceSdkLoginViewModel loginVM)
        {
            var absolutePath = $"{ServiceHost}/{LoginInfoQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var loginResult = await SendRequestAsync<string, AjaxResponse<DeviceStaffLoginResultViewModel>>(HttpMethod.Post, absolutePath, null);
                return loginResult.Result;
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
