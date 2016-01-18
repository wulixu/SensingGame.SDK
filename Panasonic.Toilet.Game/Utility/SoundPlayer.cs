using System;
using System.Media;

namespace TronCell.Game.Utility
{
    public class ClickSounder
    {
        private static string  SoundPath =AppDomain.CurrentDomain.BaseDirectory + "Resources\\Click.wav";
        private static SoundPlayer _soundPlayer;

        static ClickSounder()
        {
            if (_soundPlayer == null)
            {
                _soundPlayer = new SoundPlayer();
                _soundPlayer.SoundLocation = SoundPath;
                
            }
        }
        /// <summary>
        /// 播放声音
        /// </summary>
        public static void PaySound()
        {
            _soundPlayer.Play();
        }

        /// <summary>
        /// 停止播放声音
        /// </summary>
        public static void StopSound()
        {
            //if (_soundPlayer != null)
            //    _soundPlayer.Stop();
        }
    }
}
