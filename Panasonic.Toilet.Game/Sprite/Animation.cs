using System.ComponentModel;

namespace TronCell.Game.Sprite
{
    public class Animation : INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Variables

        private string _name;
        private TileList _tiles;

        #endregion

        #region Constructors

        public Animation()
        {
            Name = string.Empty;
            Tiles = new TileList();
        }

        #endregion

        #region Properties

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                    }
                }
            }
        }

        public TileList Tiles
        {
            get
            {
                return _tiles;
            }
            set
            {
                if (_tiles != value)
                {
                    _tiles = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Tiles"));
                    }
                }
            }
        }

        #endregion
    }
}