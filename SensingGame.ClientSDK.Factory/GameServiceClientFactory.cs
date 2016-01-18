using SensngGame.ClientSDK;
using SensngGame.ClientSDK.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensingGame.ClientSDK.Factory
{
    public class GameServiceClientFactory
    {

        private static GameServiceClient _sington;
        public static QrCodeResult CurrentQrCodeResult { get; set; }

        public static UserActionResult CurrentUserActionResult { get; set; }

        public static GameServiceClient GetInstance()
        {
            if (_sington == null)
                throw new Exception("GameServiceClient is not initialized!!!");
            return _sington;
        }

        public static void Initialize(string weixinAppId, string gameId,string activityId)
        {
            _sington = new GameServiceClient("j;lajdf;jaiuefjf", weixinAppId, gameId, activityId);

        }
    }
}
