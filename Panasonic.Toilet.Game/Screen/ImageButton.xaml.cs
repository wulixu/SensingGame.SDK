using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using TronCell.Game.Utility;

namespace TronCell.Game.Screen
{
    /// <summary>
    /// ImageButton.xaml 的交互逻辑
    /// </summary>
    public partial class ImageButton : UserControl
    {
        public event EventHandler<EventArgs> Clicked;

        public ImageSource DefaultImage
        {
            get { return (ImageSource)GetValue(DefaultImageProperty); }
            set { SetValue(DefaultImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultImageProperty =
            DependencyProperty.Register("DefaultImage", typeof(ImageSource), typeof(ImageButton), new UIPropertyMetadata(null));

        public ImageSource FocusedImage
        {
            get { return (ImageSource)GetValue(FocusedImageProperty); }
            set { SetValue(FocusedImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FocusedImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FocusedImageProperty =
            DependencyProperty.Register("FocusedImage", typeof(ImageSource), typeof(ImageButton), new UIPropertyMetadata(null));

        Storyboard sbDown = null;
        Storyboard sbUp = null;
        public ImageButton()
        {
            InitializeComponent();
            Stylus.SetIsPressAndHoldEnabled(this, false);

            sbDown = this.Resources["sb1"] as Storyboard;
            sbUp = this.Resources["sb2"] as Storyboard;

            //this.TouchDown += (s, e) => { DealTouchDown(); };
            //this.TouchUp += (s, e) => { DealTouchUp(); };

            this.MouseLeftButtonDown += (s, e) => { DealTouchDown(); };
            this.MouseLeftButtonUp += (s, e) => { DealTouchUp(); };

            sbUp.Completed += (s, e) =>
            {
                if (Clicked != null)
                {
                    Clicked(this, null);
                }
            };
        }

        private void DealTouchUp()
        {
            ClickSounder.StopSound();
            sbUp.Begin();
        }

        private void DealTouchDown()
        {
            ClickSounder.PaySound();
            sbDown.Begin();
        }
       
    }
}
