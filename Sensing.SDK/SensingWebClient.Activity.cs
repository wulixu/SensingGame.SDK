using Sensing.SDK.Contract;
using SensingStoreCloud.Activity;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
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
        private const string CreateQrcode4LoginQuery = "/BehaviorRecord/PostRecord";
        //private const string PostDeviceStatusQuery = "/BehaviorRecord/PostDeviceStatusRecord";
        private const string GetDeviceActivityGameInfoQuery = "services/app/SensingDeviceActivity/GetDeviceActivityGameInfo";
        private const string GetAwardsByActivityQuery =       "services/app/SensingDeviceActivity/GetAwardsByActivity";
        private const string GetQrcode4LoginQuery = "services/app/SensingDeviceActivity/CreateQrCode4Login";
        private const string PostPlayerData4ActionQrcodeQuery = "UserAction/PostPlayerData4ActionQrcode";
        private const string PostPlayerDataByActionQuery = "UserAction/PostPlayerDataByAction";
        private const string GetScanQrCodeUsersQuery = "services/app/SensingDeviceActivity/GetScanQrCodeUsers";
        private const string GetScanQrCodeUserActionsQuery = "services/app/SensingDeviceActivity/GetScanQrCodeUserActions";

        private const string DoLotteryUserByAwardIdQuery = "services/app/SensingDeviceActivity/DoLotteryUserByAwardId";
        private const string DoLotteryAwardByActionQuery = "services/app/SensingDeviceActivity/DoLotteryAwardByAction";
        private const string GetUserActionByIdQuery = "services/app/SensingDeviceActivity/GetUserActionById";

        public readonly static string ActivityServiceRelativePath = "g/";
        public readonly static string ActivityServiceApiHost = ServerBase + ActivityDataPath + Api_Relative_Path;
        public readonly static string ActivityServiceHost = ServerBase + ActivityDataPath;
        private const string ActivityDataPath = "g/api/";

        public async Task<QrcodeOutput> CreateQrCode4LoginAsync(Qrcode4LoginInput input)
        {
            input.SecurityKey = _deviceActivityGameSecurityKey;
            var absolutePath = $"{ServerBase}{ActivityDataPath}{GetQrcode4LoginQuery}";
            try
            {
                var webResult = await SendRequestAsync<Qrcode4LoginInput, AjaxResponse<QrcodeOutput>>(HttpMethod.Post, absolutePath, input);
                return webResult.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetRecommendsByFaces:" + ex.InnerException);
            }
            return null;
        }

        public async Task<DeviceActivityGameOutput> GetDeviceActivityGameInfoAsync()
        {
            //input.Subkey = _subKey;
            var absolutePath = $"{ServerBase}{ActivityDataPath}{GetDeviceActivityGameInfoQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var webResult = await SendRequestAsync<string, AjaxResponse<DeviceActivityGameOutput>>(HttpMethod.Get, absolutePath, null);
                return webResult.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetDeviceActivityGameInfoAsync:" + ex.InnerException);
            }
            return null;
        }

        public async Task<UserActionInfoOutput> PostPlayerDataByAction(PlayerActionDataInput playerActionData)
        {
            var absolutePath = $"{ServerBase}{ActivityDataPath}{PostPlayerDataByActionQuery}";
            //var absolutePath = "http://localhost:13654/api/UserAction/PostPlayerDataByAction";
            var nameValues = new NameValueCollection();
            AddBasicNameValues(nameValues);
            nameValues.Add("score", playerActionData.Score.ToString());
            nameValues.Add("params", playerActionData.Params);
            nameValues.Add("qrType", playerActionData.QrType.ToString());
            nameValues.Add("snsType", playerActionData.SnsType.ToString());
            nameValues.Add("IsSendWeChatMsg", playerActionData.IsSendWeChatMsg.ToString());
            nameValues.Add("TargetUrl", playerActionData.TargetUrl);
            nameValues.Add("ActionId", playerActionData.ActionId.ToString());

            try
            {
                var files = new List<string>();
                var names = new List<string>();
                if (!string.IsNullOrEmpty(playerActionData.PlayerImage) && File.Exists(playerActionData.PlayerImage))
                {
                    files.Add(playerActionData.PlayerImage);
                    names.Add("playerimage");
                }
                if (!string.IsNullOrEmpty(playerActionData.PlayingImage) && File.Exists(playerActionData.PlayingImage))
                {
                    files.Add(playerActionData.PlayingImage);
                    names.Add("PlayingImage");
                }
                return await SendMultipartFormRequestAsync<UserActionInfoOutput>(absolutePath, files.ToArray(), names.ToArray(), nameValues);
            }
            catch (Exception ex)
            {
                logger.Error("PostData4ScanAsync", ex);
            }
            return default(UserActionInfoOutput);
        }
        public async Task<QrcodeActionOutput> PostPlayerData4ActionQrcodeAsync(PlayerDataInput playerData)
        {
            var absolutePath = $"{ServerBase}{ActivityDataPath}{PostPlayerData4ActionQrcodeQuery}";
            var nameValues = new NameValueCollection();
            AddBasicNameValues(nameValues);
            nameValues.Add("score", playerData.Score.ToString());
            if (!string.IsNullOrEmpty(playerData.Params))
                nameValues.Add("params", playerData.Params);
            nameValues.Add("qrType", playerData.QrType.ToString());
            nameValues.Add("snsType", playerData.SnsType.ToString());
            nameValues.Add("IsSendWeChatMsg", playerData.IsSendWeChatMsg.ToString());
            if (!string.IsNullOrEmpty(playerData.TargetUrl)) nameValues.Add("TargetUrl", playerData.TargetUrl);

            try
            {
                var files = new List<string>();
                var names = new List<string>();
                if (!string.IsNullOrEmpty(playerData.PlayerImage) && File.Exists(playerData.PlayerImage))
                {
                    files.Add(playerData.PlayerImage);
                    names.Add("playerimage");
                }
                if (!string.IsNullOrEmpty(playerData.PlayingImage) && File.Exists(playerData.PlayingImage))
                {
                    files.Add(playerData.PlayingImage);
                    names.Add("PlayingImage");
                }
                var data =  await SendMultipartFormRequestAsync<AjaxResponse<QrcodeActionOutput>>(absolutePath, files.ToArray(), names.ToArray(), nameValues);
                return data.Result;
            }
            catch (Exception ex)
            {
                logger.Error("PostData4ScanAsync", ex);
            }
            return default(QrcodeActionOutput);
        }
        public async Task<PagedResultDto<SnsUserInfoOutput>> GetScanQrCodeUsers(Qrcode4UsersInput input)
        {
            input.SecurityKey = _deviceActivityGameSecurityKey;
            var absolutePath = $"{ServerBase}{ActivityDataPath}{GetScanQrCodeUsersQuery}?{GetBasicNameValuesQueryString()}&qrcodeId={input.QrcodeId}&{MaxResultCount}={input.MaxResultCount}&{SkipCount}={input.SkipCount}&{Query}={input.Filter}";
            try
            {
                var webResult = await SendRequestAsync<string, AjaxResponse<PagedResultDto<SnsUserInfoOutput>>>(HttpMethod.Get, absolutePath, null);
                return webResult.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetScanQrCodeUsers:" + ex.InnerException);
            }
            return null;
        }

        public async Task<PagedResultDto<UserActionInfoOutput>> GetScanQrCodeUserActions(Qrcode4UsersInput input)
        {
            input.SecurityKey = _deviceActivityGameSecurityKey;
            var absolutePath = $"{ServerBase}{ActivityDataPath}{GetScanQrCodeUserActionsQuery}?{GetBasicNameValuesQueryString()}&qrcodeId={input.QrcodeId}&{MaxResultCount}={input.MaxResultCount}&{SkipCount}={input.SkipCount}&{Query}={input.Filter}";
            try
            {
                var webResult = await SendRequestAsync<string, AjaxResponse<PagedResultDto<UserActionInfoOutput>>>(HttpMethod.Get, absolutePath, null);
                return webResult.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetScanQrCodeUsers:" + ex.InnerException);
            }
            return null;
        }

        public async Task<SnsUserAwardOuput> DoLotteryUserByAwardId(AwardDataInput input)
        {
            input.SecurityKey = _deviceActivityGameSecurityKey;
            var absolutePath = $"{ServerBase}{ActivityDataPath}{DoLotteryUserByAwardIdQuery}";
            try
            {
                var webResult = await SendRequestAsync<AwardDataInput, AjaxResponse<SnsUserAwardOuput>>(HttpMethod.Post, absolutePath, input);
                return webResult.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DoLotteryByAwardId:" + ex.InnerException);
            }
            return null;
        }

        public async Task<SnsUserAwardOuput> DoLotteryAwardByAction(ActionDataInput input)
        {
            input.SecurityKey = _deviceActivityGameSecurityKey;
            var absolutePath = $"{ServerBase}{ActivityDataPath}{DoLotteryAwardByActionQuery}";
            //var absolutePath = "http://localhost:13654/api/services/app/SensingDeviceActivity/DoLotteryAwardByAction";

            var webResult = await SendRequestAsync<ActionDataInput, AjaxResponse<SnsUserAwardOuput>>(HttpMethod.Post, absolutePath, input);
            return webResult.Result;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("DoLotteryByAwardId:" + ex.InnerException);
            //}
            //return null;
        }

        public async Task<List<AwardOutput>> GetAwardsAsync()
        {
            var absolutePath = $"{ServerBase}{ActivityDataPath}{GetAwardsByActivityQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var webResult = await SendRequestAsync<string, AjaxResponse<List<AwardOutput>>>(HttpMethod.Get, absolutePath, null);
                return webResult.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetRecommendsByFaces:" + ex.InnerException);
            }
            return null;
        }


        public async Task<UserActionInfoOutput> GetUserActionByIdAsync(ActionDataInput input)
        {
            input.SecurityKey = _deviceActivityGameSecurityKey;
            var absolutePath = $"{ServerBase}{ActivityDataPath}{GetUserActionByIdQuery}?{GetBasicNameValuesQueryString()}&actionId={input.ActionId}";
            try
            {
                var webResult = await SendRequestAsync<string, AjaxResponse<UserActionInfoOutput>>(HttpMethod.Get, absolutePath, null);
                return webResult.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetUserActionById:" + ex.InnerException);
            }
            return null;
        }


    }
}
