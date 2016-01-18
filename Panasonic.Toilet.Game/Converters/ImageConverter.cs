using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace TronCell.Game
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var approot = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = (string)value;
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(approot + relativePath, UriKind.Absolute);
            image.EndInit();
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
