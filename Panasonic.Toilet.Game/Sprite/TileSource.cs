using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TronCell.Game.Sprite
{
    public class TileSource : Image, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public TileSource()
            : base()
        {
            Loaded += new RoutedEventHandler(TileSource_Loaded);
        }

        #endregion

        #region Properties
        public int RenderTile
        {
            get
            {
                return (int)GetValue(RenderTileProperty);
            }
            set
            {
                if (RenderTile != value)
                {
                    SetValue(RenderTileProperty, value);
                    PropertyHasChanged("RenderTile");
                    SetTransforms();
                }
            }
        }

        public static readonly DependencyProperty RenderTileProperty =
            DependencyProperty.Register("RenderTile", typeof(int), typeof(TileSource), new PropertyMetadata(0));

        public int TileWidth
        {
            get
            {
                return (int)GetValue(TileWidthProperty);
            }
            set
            {
                if (TileWidth != value)
                {
                    SetValue(TileWidthProperty, value);
                    PropertyHasChanged("TileWidth");

                    TilesPerRow = (int)(Source.Width / TileWidth);
                    WidthRatio = TileWidth / ScaleFactor;
                }
            }
        }

        public static readonly DependencyProperty TileWidthProperty =
            DependencyProperty.Register("TileWidth", typeof(int), typeof(TileSource), new PropertyMetadata(0));

        public int TileHeight
        {
            get
            {
                return (int)GetValue(TileHeightProperty);
            }
            set
            {
                if (TileHeight != value)
                {
                    SetValue(TileHeightProperty, value);
                    PropertyHasChanged("TileHeight");
                    
                    HeightRatio = TileHeight / ScaleFactor;
                }
            }
        }

        public static readonly DependencyProperty TileHeightProperty =
            DependencyProperty.Register("TileHeight", typeof(int), typeof(TileSource), new PropertyMetadata(0));

        private int TilesPerRow { get; set; }

        public int TileCount
        {
            get
            {
                return TilesPerRow * (int)(Source.Height / TileHeight);
            }
        }

        private int ScaleFactor
        {
            get
            {
                return
                    (int)((Source.Width > Source.Height) ? Source.Width : Source.Height) /
                    ((TileWidth > TileHeight) ? TileWidth : TileHeight);
            }
        }

        private RectangleGeometry RectangleClip
        {
            get
            {
                return Clip as RectangleGeometry;
            }
            set
            {
                Clip = value;
            }
        }

        private TranslateTransform TranslateTransform { get; set; }

        private int WidthRatio { get; set; }
        private int HeightRatio { get; set; }

        #endregion

        #region Methods

        protected void PropertyHasChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void SetTransforms()
        {
            int x = (RenderTile % TilesPerRow) * WidthRatio;
            int y = (RenderTile / TilesPerRow) * HeightRatio;

            RectangleClip.Rect = new Rect(x, y, WidthRatio, HeightRatio);
            TranslateTransform.X = -x - 1;
            TranslateTransform.Y = -y;
        }

        #endregion

        #region Event Handlers

        private void TileSource_Loaded(object sender, RoutedEventArgs e)
        {
            Width = TileWidth;
            Height = TileHeight;
            TilesPerRow = (int)(Source.Width / TileWidth);
            WidthRatio = TileWidth / ScaleFactor;
            HeightRatio = TileHeight / ScaleFactor;

            RectangleClip = new RectangleGeometry();
            TranslateTransform = new TranslateTransform();
            
            SetTransforms();

            RenderTransform = new TransformGroup();
            (RenderTransform as TransformGroup).Children.Add(TranslateTransform);
            (RenderTransform as TransformGroup).Children.Add(new ScaleTransform(ScaleFactor, ScaleFactor));
        }

        #endregion
    }
}