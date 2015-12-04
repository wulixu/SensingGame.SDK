using SensngGame.ClientSDK.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensngGame.ClientSDK
{
    /// <summary>
    /// The game service client proxy interface.
    /// </summary>
    public interface IGameServiceClient
    {
        Task<QrCodeResult> GetQrCode4LoginAsyn(string gameId, string appId, string clientUniqueId);

        Task<UserActionResult> FindScanQrCodeUserAsyn(string qrCodeId);

        Task<PostBackResult> PostDataByUserAsyn(string userId, string qrCodeId, string playerImage, string gameImage, int score);

        Task<QrCodeResult> PostData4ScanAsyn(string gameId, string playerImage, string gameImage, int score);

    }
}
