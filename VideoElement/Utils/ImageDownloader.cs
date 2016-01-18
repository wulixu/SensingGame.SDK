using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace VideoElement.Utils
{
    public class ImageDownloader
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static BitmapImage GetImageFromWeb(string url)
        {

            using (HttpClient client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0,0,5);
                try
                {
                    Stream stream = client.GetStreamAsync(url).GetAwaiter().GetResult();
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.EndInit();
                    return image;
                }
                catch (Exception e) 
                {
                    log.Error(e.Message);
                    return null;
                
                }

            }
        }
    }
}
