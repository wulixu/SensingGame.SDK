using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace VideoElement.Utils
{
    public enum ImageType
    {
        Bmp,
        Gif,
        Jpeg,
        Png,
        Tiff,
        Wdp
    }

    public static class ImageSaver
    {
        public static int Width=1024;
        public static int Height=768;
        /// <summary>
        /// Convert any control to a PngBitmapEncoder
        /// </summary>
        /// <param name="controlToConvert">The control to convert to an ImageSource</param>
        /// <param name="imageType">The image type will indicate the type of return bitmap encoder</param>
        /// <returns>The returned ImageSource of the controlToConvert</returns>
        private static BitmapEncoder GetImageFromControl(Visual controlToConvert, ImageType imageType, double width, double height)
        {
            
            //var bounds = controlToConvert.GetBounds(controlToConvert.Parent as Visual);

            var renderBitmap = new RenderTargetBitmap((Int32)width, (Int32)height, 96d,
                                                      96d, PixelFormats.Pbgra32);
            renderBitmap.Render(controlToConvert);

            var encoder = GetBitmapEncoderByImageType(imageType);

            // puch rendered bitmap into it
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
            return encoder;
        }



        private static Rect GetBounds(this FrameworkElement element, Visual from)
        {
            Rect rect = Rect.Empty;
            try
            {
                GeneralTransform transform = element.TransformToVisual(from);
                rect = transform.TransformBounds(new Rect(0, 0, element.Width, element.Height));
            }
            catch
            {

            }
            return rect;
        }


        /// <summary>
        /// Get an encoder by a specify image type
        /// </summary>
        /// <param name="type">the image type</param>
        /// <returns>return an eccoder</returns>
        private static BitmapEncoder GetBitmapEncoderByImageType(ImageType type)
        {
            switch (type)
            {
                case ImageType.Bmp:
                    return new BmpBitmapEncoder();
                case ImageType.Gif:
                    return new GifBitmapEncoder();
                case ImageType.Jpeg:
                    return new JpegBitmapEncoder();
                case ImageType.Png:
                    return new PngBitmapEncoder();
                case ImageType.Tiff:
                    return new TiffBitmapEncoder();
                case ImageType.Wdp:
                    return new WmpBitmapEncoder();
                default:
                    return new PngBitmapEncoder();
            }
        }

        /// <summary>
        /// Get the iamge type by image file name
        /// </summary>
        /// <param name="fileName">the file name of an image</param>
        /// <returns>the iamge type</returns>
        private static ImageType GetImageTypeByFileName(string fileName)
        {
            ImageType returnType = ImageType.Png;

            var extension = Path.GetExtension(fileName);
            if (!String.IsNullOrEmpty(extension))
            {
                switch (extension.ToLower())
                {
                    case ".bmp":
                        returnType = ImageType.Bmp;
                        break;
                    case ".gif":
                        returnType = ImageType.Gif;
                        break;
                    case ".jpeg":
                    case ".jpg":
                    case ".jpe":
                    case "jfif":
                        returnType = ImageType.Jpeg;
                        break;
                    case ".png":
                        returnType = ImageType.Png;
                        break;
                    case ".tiff":
                    case ".tif":
                        returnType = ImageType.Tiff;
                        break;
                    case ".wdp":
                        returnType = ImageType.Wdp;
                        break;
                    default:
                        returnType = ImageType.Png;
                        break;
                }
            }

            return returnType;
        }

        /// <summary>
        /// Get an ImageSource of a control
        /// </summary>
        /// <param name="controlToConvert">The control to convert to an ImageSource</param>
        /// <param name="imageType">the image type</param>
        /// <returns>The returned ImageSource of the controlToConvert</returns>
        public static BitmapSource GetImageFromFrameworkElement(Visual controlToConvert, ImageType imageType, double width, double height)
        {
            // return first frame of image 
            var encoder = GetImageFromControl(controlToConvert, imageType, width, height);
            if (encoder != null && encoder.Frames.Count > 0)
            {
                return encoder.Frames[0];
            }

            return new BitmapImage();
        }


        /// <summary>
        /// Get an ImageSource of a control(Jpeg as default type)
        /// </summary>
        /// <param name="controlToConvert">The control to convert to an ImageSource</param>
        /// <returns>The returned ImageSource of the controlToConvert</returns>
        public static BitmapSource GetImageOfControl(Visual controlToConvert, double width, double height)
        {
            return GetImageFromFrameworkElement(controlToConvert, ImageType.Png,width,height);
        }


        /// <summary>
        /// Save an image of a control
        /// </summary>
        /// <param name="controlToConvert">The control to convert to an ImageSource</param>
        /// <param name="fileName">The location to save the image to</param>
        /// <returns>The returned ImageSource of the controlToConvert</returns>
        public static bool SaveImage4FrameworkElement(Visual controlToConvert, String fileName, double width, double height)
        {
            try
            {
                var imageType = GetImageTypeByFileName(fileName);

                
                var encoder = GetImageFromControl(controlToConvert, imageType, width, height);
                
                using (var outStream = new FileStream(fileName, FileMode.Create))
                {
                    //BitmapEncoder encoder1 = GetBitmapEncoderByImageType(imageType);
                    if (encoder != null && encoder.Frames.Count > 0)
                    {
                        //BitmapSource bs= encoder.Frames[0] as BitmapSource;

                        //Bitmap bp = GetBitmap(bs);
                        //bp = ResizeImage(bp, Width, Height);
                        //bs = convertBitmapToBitmapSource(bp);
                        //encoder1 = GetBitmapEncoderByImageType(imageType);
                        // puch rendered bitmap into it
                        //encoder1.Frames.Add(BitmapFrame.Create(bs));
                        encoder.Save(outStream);
                    }
                    //encoder1.Save(outStream);
                }
            }
            catch (Exception e)
            {
                #if DEBUG
                Console.WriteLine("Exception caught saving stream: {0}", e.Message);
                #endif
                return false;
            }

            return true;
        }


        public static Boolean SaveImage4FrameworkElement(Visual controlToConvert, String fileName, double width, double height,double sclae)
        {
            try
            {
                var imageType = GetImageTypeByFileName(fileName);

                using (var outStream = new FileStream(fileName, FileMode.Create))
                {
                    if (width > 0)
                    {
                        DrawingVisual dv = new DrawingVisual();
                        using (DrawingContext ctx = dv.RenderOpen())
                        {
                            VisualBrush vb = new VisualBrush(controlToConvert);
                            vb.Stretch = Stretch.UniformToFill;
                            vb.AlignmentX = AlignmentX.Left;
                            vb.AlignmentY = AlignmentY.Top;
                            ctx.DrawRectangle(vb, null, new Rect(0, 0, (double)width * sclae, (double)height * sclae));
                        }
                        RenderTargetBitmap rtb = new RenderTargetBitmap((Int32)(width * sclae), (Int32)(height * sclae), 96.0, 96.0, PixelFormats.Pbgra32);
                        rtb.Render(dv);
                        var encoder = GetBitmapEncoderByImageType(imageType);

                        // puch rendered bitmap into it
                        encoder.Frames.Add(BitmapFrame.Create(rtb));
                        //BitmapEncoder encoder1 = GetBitmapEncoderByImageType(imageType);
                        if (encoder != null && encoder.Frames.Count > 0)
                        {
                            //BitmapSource bs= encoder.Frames[0] as BitmapSource;

                            //Bitmap bp = GetBitmap(bs);
                            //bp = ResizeImage(bp, Width, Height);
                            //bs = convertBitmapToBitmapSource(bp);
                            //encoder1 = GetBitmapEncoderByImageType(imageType);
                            // puch rendered bitmap into it
                            //encoder1.Frames.Add(BitmapFrame.Create(bs));
                            encoder.Save(outStream);
                        }
                        //encoder1.Save(outStream);
                    }

                    
                }
            }
            catch (Exception e)
            {
#if DEBUG
                Console.WriteLine("Exception caught saving stream: {0}", e.Message);
#endif
                return false;
            }

            return true;
        }


        private static System.Drawing.Bitmap GetBitmap(BitmapSource bitmapSource)
        {
            int width = bitmapSource.PixelWidth;
            int height = bitmapSource.PixelHeight;
            int stride = width * ((bitmapSource.Format.BitsPerPixel + 7) / 8);

            byte[] bits = new byte[height * stride];

            bitmapSource.CopyPixels(bits, stride, 0);

            unsafe
            {
                fixed (byte* pBits = bits)
                {
                    IntPtr ptr = new IntPtr(pBits);

                    System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(
                        width,
                        height,
                        stride,
                        System.Drawing.Imaging.PixelFormat.Format32bppPArgb,
                        ptr);

                    return bitmap;
                }
            }
        }

        public static System.Drawing.Bitmap ResizeImage(System.Drawing.Bitmap bmp, int newW, int newH)
        {
            try
            {
                System.Drawing.Bitmap b = new System.Drawing.Bitmap(newW, newH);
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(b);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new System.Drawing.Rectangle(0, 0, newW, newH), new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        } 

        private static BitmapSource convertBitmapToBitmapSource(System.Drawing.Bitmap bitmap)
        {
            using (bitmap)
            {
                System.Windows.Media.Imaging.BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    bitmap.GetHbitmap(),
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
                return bitmapSource;
            }
        }


    }
}
