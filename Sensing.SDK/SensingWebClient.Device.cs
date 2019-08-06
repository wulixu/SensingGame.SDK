using Sensing.SDK.Contract;
using SensingStoreCloud.Devices.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Sensing.SDK
{
    partial class SensingWebClient
    {
        /// <summary>
        /// Get all the things.
        /// </summary>
        private const string DeviceBaseUrl = "SensingDevice";
        private const string RegisterDeviceQuery = DeviceBaseUrl + "/RegisterDevice";

        private const string GroupInfoQuery = DeviceBaseUrl + "/GetTenantAndOrganizationUnitInfo";
        private const string DeviceInfoQuery = DeviceBaseUrl + "/GetDeviceInfo";
        private const string GetDeviceSubkeyByHardwareCodeQuery = DeviceBaseUrl + "/GetDeviceSubkeyByHardwareCode";
        private const string LoginInfoQuery = DeviceBaseUrl + "/Login";
        private const string GetDeviceAppPodVersionQuery = DeviceBaseUrl + "/GetDeviceAppPodVersion";
        private const string ChangeDeviceApppodVersionQuery = DeviceBaseUrl + "/ChangeDeviceApppodVersion";
        private const string GetLastUpdateTimeQuery = DeviceBaseUrl + "/GetLastUpdateTime";
        private const string UploadScreenShotQuery = DeviceBaseUrl + "/UploadScreenShot";



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

        public async Task<TableLastTimeDto> GetLastUpdateTime()
        {
            var absolutePath = $"{MainServiceApiHost}/{GetLastUpdateTimeQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var pagedList = await SendRequestAsync<string, AjaxResponse<TableLastTimeDto>>(HttpMethod.Get, absolutePath, null);
                return pagedList.Result;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("GetDeviceInfo:" + ex.InnerException);
            }
            return null;
        }

        public async Task<string> GetDeviceSubkeyByHardwareCode(string hardwareid)
        {
            var absolutePath = $"{MainServiceApiHost}/{GetDeviceSubkeyByHardwareCodeQuery}?HardwareCode={hardwareid}";
            try
            {
                var pagedList = await SendRequestAsync<string, AjaxResponse<string>>(HttpMethod.Get, absolutePath, null);
                return pagedList.Result;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("GetDeviceInfo:" + ex.InnerException);
            }
            return null;
        }


        public async Task<bool> UploadScreenShot(ScreenshotInput screenshot)
        {
            var absolutePath = $"{ExtenalServiceHost}{UploadScreenShotQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var loginResult = await SendRequestAsync<ScreenshotInput, AjaxResponse<string>>(HttpMethod.Post, absolutePath, screenshot);
                return loginResult.Success;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("DeviceLogin:" + ex.InnerException);
            }
            return false;
        }

        public async Task<CartQrcodeOutput> AddGoodsToCar(List<ShoppingCartItem> shoppingCart)
        {
            var absolutePath = $"https://sync.api.troncell.com/api/services/app/ThingSync/AddGoodsToCar?{GetBasicNameValuesQueryString()}";
            try
            {
                ShoppingCartInput input = new ShoppingCartInput
                {
                    Longterm = true,
                    Item_ids = String.Join(",",shoppingCart.Select(s => $"{s.ItemId}_{s.SkuId}_{s.ItemCount}"))
                };
                var loginResult = await SendRequestAsync<ShoppingCartInput, AjaxResponse<CartQrcodeOutput>>(HttpMethod.Post, absolutePath, input);
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
