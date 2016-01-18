using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TronCell.Game.Utility
{
    public class GUIHelper
    {
        private void CompositeImage2Save(int targetWidth, int targetHeight, Image backDrop, Image foreImage)
        {

            int colorWidth = targetWidth;
            int colorHeight = targetHeight;

            // create a render target that we'll render our controls to
            var renderBitmap = new RenderTargetBitmap(colorWidth, colorHeight, 96.0, 96.0, PixelFormats.Pbgra32);

            var dv = new DrawingVisual();
            using (var dc = dv.RenderOpen())
            {
                // render the backdrop
                var backdropBrush = new VisualBrush(backDrop);
                dc.DrawRectangle(backdropBrush, null, new Rect(new Point(), new Size(colorWidth, colorHeight)));

                // render the color image masked out by players
                var colorBrush = new VisualBrush(foreImage);
                dc.DrawRectangle(colorBrush, null, new Rect(new Point(), new Size(colorWidth, colorHeight)));
            }

            renderBitmap.Render(dv);

            // create a png bitmap encoder which knows how to save a .png file
            BitmapEncoder encoder = new PngBitmapEncoder();

            // create frame from the writable bitmap and add to encoder
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            var time = DateTime.Now.ToString("hh'-'mm'-'ss", CultureInfo.CurrentUICulture.DateTimeFormat);

            var myPhotos = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            var path = Path.Combine(myPhotos, "KinectSnapshot-" + time + ".png");

            // write the new file to disk
            try
            {
                using (var fs = new FileStream(path, FileMode.Create))
                {
                    encoder.Save(fs);
                }

                //this.statusBarText.Text = string.Format(CultureInfo.InvariantCulture, Properties.Resources.ScreenshotWriteSuccess, path);
            }
            catch (IOException)
            {
                //this.statusBarText.Text = string.Format(CultureInfo.InvariantCulture, Properties.Resources.ScreenshotWriteFailed, path);
            }
        }


    }
}
