using System.Speech.Synthesis;

namespace TronCell.Game.Utility
{
    public class SpeechHelper
    {
        private static SpeechSynthesizer _speechSynthesizer = new SpeechSynthesizer();
        public static void Speech(string str)
        {
            _speechSynthesizer.SpeakAsync(str);
        }
    }
}
