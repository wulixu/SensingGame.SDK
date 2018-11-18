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

        private const string GroupInfoQuery = DeviceBaseUrl + "/GetTenantAndOrganizationUnitInfo";
        private const string DeviceInfoQuery = DeviceBaseUrl + "/GetDeviceInfo";
        private const string LoginInfoQuery = DeviceBaseUrl + "/Login";
        private const string GetDeviceAppPodVersionQuery = DeviceBaseUrl + "/GetDeviceAppPodVersion";
        private const string ChangeDeviceApppodVersionQuery = DeviceBaseUrl + "/ChangeDeviceApppodVersion";



        public async Task<AjaxResponse<DeviceOutput>> RegisterDeviceAsyn(RegisterDeviceInput device)
        {
            var absolutePath = $"{MainServiceApiHost}/{RegisterDeviceQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var pagedList = await SendRequestAsync<RegisterDeviceInput, AjaxResponse<DeviceOutput>>(HttpMethod.Post, absolutePath, device);
                if(pagedList.Success)
                {
                    return pagedList;
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
            var absolutePath = $"{MainServiceApiHost}/{DeviceHeartBeatQuery}?{GetBasicNameValuesQueryString()}";
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

        public async Task<AjaxResponse<DeviceOutput>> GetDeviceInfo()
        {
            var absolutePath = $"{MainServiceApiHost}/{DeviceInfoQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var pagedList = await SendRequestAsync<string, AjaxResponse<DeviceOutput>>(HttpMethod.Get, absolutePath, null);
                return pagedList;
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
            var absolutePath = $"{MainServiceApiHost}/{GroupInfoQuery}?{GetBasicNameValuesQueryString()}";
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
            var absolutePath = $"{MainServiceApiHost}/{LoginInfoQuery}?{GetBasicNameValuesQueryString()}";
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

        public async Task<DeviceAppPodVersionModel> GetDeviceAppPodVersion()
        {
            var absolutePath = $"{ExtenalServiceHost}{GetDeviceAppPodVersionQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var appPodVersion = await SendRequestAsync<string, AjaxResponse<DeviceAppPodVersionModel>>(HttpMethod.Get, absolutePath, null);
                return appPodVersion.Result;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("GetDeviceAppPodVersion:" + ex.InnerException);
            }
            return null;
        }

        public async Task<bool> ChangeDeviceApppodVersion(ChangeDeviceApppodVersionInput input)
        {
            input.Subkey = _subKey;
            var absolutePath = $"{ExtenalServiceHost}{ChangeDeviceApppodVersionQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var loginResult = await SendRequestAsync<ChangeDeviceApppodVersionInput, AjaxResponse<string>>(HttpMethod.Post, absolutePath, input);
                return loginResult.Success;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("DeviceLogin:" + ex.InnerException);
            }
            return false;
        }


    }
}
