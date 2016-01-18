using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace VideoElement.Signature
{
    /// <summary>
    /// EffectButton.xaml 的交互逻辑
    /// </summary>
    public partial class EffectButton : UserControl
    {

        public static DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(EffectButton));
        private Storyboard sb = null;
        public event EventHandler<RoutedEventArgs> OnClick;
        private static bool _isPlaying = false;
        public ImageSource Source
        {
            get { return (ImageSource)this.GetValue(SourceProperty); }
            set { this.SetValue(SourceProperty, value); }
        }
        public EffectButton()
        {
            this.DataContext = this;
            InitializeComponent();
            sb = (Storyboard)this.FindResource("myStoryboard");
            sb.Completed += sb_Completed;
        }

        void sb_Completed(object sender, EventArgs e)
        {
            if (OnClick != null)
                OnClick(this, new RoutedEventArgs());
            _isPlaying = false;
        }
        private void img_MouseDown(object sender, EventArgs e)
        {
            if (sb != null && !_isPlaying)
            {
                _isPlaying = true;
                sb.Begin();
            }
        }
        private void img_TouchDown(object sender, TouchEventArgs e)
        {
            if (sb != null && !_isPlaying)
            {
                _isPlaying = true;
                sb.Begin();
            }
            e.Handled = true;
        }
    }
}
