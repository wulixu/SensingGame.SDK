using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensngGame.ClientSDK.Contract
{
    public class GameActivityResult
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public ActivityGameInfo Data { get; set; }
    }

    public class ActivityGameInfo
    {
        /// <summary>
        /// 活动游戏名称.
        /// </summary>
        public string Name { get; set; }

        public int GameID { get; set; }

        public int ActivityID { get; set; }

        public int SupportedPlayersCount { get; set; }

        public string OnlineBackground { get; set; }

        public string OnlineForeground { get; set; }

        public string OfflineBackground { get; set; }

        public string OfflineForeground { get; set; }

        public string MaterialPacketUrl { get; set; }

        public int MaxPlayCountPerUser { get; set; }
        public int MinPlayTimeDiff { get; set; }

        public bool IsGameStarted { get; set; }

        public bool IsNeedCheckBeforScanQRCode { get; set; }

        public string ActivityAuthorizationType { get; set; }
    }
}
