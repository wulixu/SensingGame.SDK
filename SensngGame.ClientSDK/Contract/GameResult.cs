using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensngGame.ClientSDK.Contract
{

    public class GameResult
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public GameData Data { get; set; }
    }


    public class GameData
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string Owner { get; set; }

        public string IntroductionUrl { get; set; }

        public string LogoUrl { get; set; }

        public string GameType { get; set; }

        public string EnvType { get; set; }

        public string MaterialPacketUrl { get; set; }
    }
}
