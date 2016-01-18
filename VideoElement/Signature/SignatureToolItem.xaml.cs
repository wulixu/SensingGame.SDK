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

namespace VideoElement.Signature
{
    /// <summary>
    /// Interaction logic for SignatureToolItem.xaml
    /// </summary>
    public partial class SignatureToolItem : UserControl
    {
        public static DependencyProperty SignatureContentProperty = DependencyProperty.Register("SignatureContent", typeof(string), typeof(SignatureToolItem));

        public string SignatureContent
        {
            get { 
                return (string)this.GetValue(SignatureContentProperty); 
            }
            set { 
                this.SetValue(SignatureContentProperty, value);

            }
        }
        public static DependencyProperty SignatureSourceProperty = DependencyProperty.Register("SignatureSource", typeof(ImageSource), typeof(SignatureToolItem));
        public ImageSource SignatureSource
        {
            get { 
                return (ImageSource)this.GetValue(SignatureSourceProperty); 
            }
            set { 
                this.SetValue(SignatureSourceProperty, value); 
            }
        }
        public static  readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register("IsChecked", typeof(bool), typeof(SignatureToolItem));

        public bool IsChecked
        {
            get
            {
                return (bool)this.GetValue(IsCheckedProperty);
            }
            set
            {
                this.SetValue(IsCheckedProperty, value);

            }
        }
        public event EventHandler<RoutedEventArgs> OnClick;
        public SignatureToolItem()
        {
            InitializeComponent();
            //this.DataContext = this;
        }

        private void EffectButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (OnClick != null)
            {
                OnClick(this, new RoutedEventArgs());
            }
        }

        private void us_TouchDown(object sender, TouchEventArgs e)
        {
            effect.RaiseEvent(e);
        }
    }
    public class ImageResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.Equals(InkCanvasEditingMode.EraseByPoint))
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
