using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TronCell.Game.ViewModel
{
    public class ScoreRankViewModel : INotifyPropertyChanged
    {
        private int _rank;
        public int Rank
        {
            get { return _rank; }
            set
            {
                if(_rank != value)
                {
                    _rank = value;
                    RasiePropertyChangedEvent();
                }
            }
        }

        private int _score;

        public int Score
        {
            get { return _score; }
            set
            {
                if(_score != value)
                {
                    _score = value;
                    RasiePropertyChangedEvent();
                }
            }
        }

        private string _headImage;
        public string HeadImage
        {
            get { return _headImage; }
            set
            {
                if(_headImage != value)
                {
                    _headImage = value;
                    RasiePropertyChangedEvent();
                }
            }
        }

        public string CreateTime { get; set; }

        private void RasiePropertyChangedEvent([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
