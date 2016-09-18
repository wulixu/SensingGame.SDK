

using SensngGame.ClientSDK;
using SensngGame.ClientSDK.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VideoElement.Utils;

namespace VideoElement
{
    /// <summary>
    /// VideoControl.xaml 的交互逻辑
    /// </summary>
    public partial class VideoControl : UserControl
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private VideoControlViewModel _model;
        private KinectVideoSource _kinectVideoSource;
        private AppPage _currentPage = AppPage.Home;
        private PostHelper _postHelper;

        GameServiceClient gameSvc;

        public VideoControl()
        {
            _model = new VideoControlViewModel();
            gameSvc = new GameServiceClient(AppConfig.WeixinAppId, "OFF-K-Camera-001");
           // SensingBase.Utils.Position p = new SensingBase.Utils.Position();

            InitializeComponent();
            InitPlayer();
            InitListener();
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //合成图片
            outputTemplate.ComposePicture(canvas);

            // Check for design mode. 
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                InitData();
            }
            _postHelper = new PostHelper();
        }

        public void Pause()
        {
            _kinectVideoSource.Pause = true;
        }

        public void Resume()
        {
            _kinectVideoSource.Pause = false;
        }

        private void InitPlayer()
        {
            InitKinect();
        }

        private void InitKinect()
        {
            kinectPlayer.Visibility = Visibility.Visible;
            _kinectVideoSource = new KinectVideoSource(this);
            kinectPlayer.Source = _kinectVideoSource.ImageSource;
            hatCanvas.Source = _kinectVideoSource.HatImageSource;
            handPointCanvas.Source = _kinectVideoSource.HandImageSource;
        }

        public void UpdateLasso(bool isLasso)
        {
            lasso.Visibility = isLasso ? Visibility.Hidden : Visibility.Visible;
        }

        private void InitListener()
        {
            gsTriger.ShowMenu += () =>
            {
                if (_currentPage == AppPage.Home)
                {
                    mainMenu.Show();
                }
            };
            mainMenu.ShowForegoundAction += () =>
            {
                fgList.Next();
            };
            mainMenu.TakeShotAction += () => 
            {
                countDown.Start();
            };
            mainMenu.SignNameAction += () => 
            {
                ElementToolBar.Visibility = Visibility.Visible;
                ElementInkCanvas.IsEnabled = true;
            };
            mainMenu.DisplayPicturesAction += ()=>
            {
                GoPreviewPage();
            };

            fgList.ShowMenu += () =>
            {
                fgList.Visibility = Visibility.Collapsed;
                mainMenu.Show();
            };
            fgList.ForegoundSelected += (bitmap) =>
            {
               // fgImage.Source = bitmap;
            };
            homeButton.OnClick += (s, e) => 
            {
                GoHomePage();
            };
            if (_kinectVideoSource != null)
            {
                _kinectVideoSource.HandUpAction += () =>
                {
                    
                    if (popup.Visibility != Visibility.Visible && photo.Visibility != Visibility.Visible)
                    {
                        countDown.Start();
                    }
                };
                _kinectVideoSource.PlayerEnter += () =>
                 {
                     Console.WriteLine("PlayerEnter");
                     Panel.SetZIndex(adsVideo, 0);
                     adsVideo.Stop();
                 };
                _kinectVideoSource.PlayerLeave += () =>
                 {
                     Console.WriteLine("PlayerLeave");
                     if (popup.Visibility != Visibility.Visible)
                     {
                         Panel.SetZIndex(adsVideo, 10);
                         adsVideo.Play();
                     }

                 };
            }
            countDown.AnimationFinished += (sender, e) => 
            {
                CaptureImage();
            };

        }

      
        private void GoHomePage()
        {
            _currentPage = AppPage.Home;
            mainMenu.SwitchToHome();
            homeButton.Visibility = Visibility.Collapsed;
            photo.Visibility = Visibility.Collapsed;
        }

        private void GoPreviewPage()
        {
            _currentPage = AppPage.Preview;
            homeButton.Visibility = Visibility.Visible;
            photo.Visibility = Visibility.Visible;
            mainMenu.SwitchToPriview();
        }

        bool capturingImage = false;
        private async void CaptureImage()
        {
            if (capturingImage) return;
            capturingImage = true;
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            var time = DateTime.Now.ToString("yyyyMMddhhmmss");
            var fileName = time + ".jpg";
            var targetPath = System.IO.Path.Combine(basePath, _model.SaveDirectory, fileName);
            // ImageSaver.SaveImage4FrameworkElement(outputTemplate, targetPath, outputTemplate.Width, outputTemplate.Height, 1);
            ImageSaver.SaveImage4FrameworkElement(canvas, targetPath, 1920, 1080, 0.5);

            popup.previewImage.Source = ImageSaver.GetImageFromFrameworkElement(canvas, ImageType.Png, 1920, 1080);
            popup.Show();
            StartStopWait();
            try
            {
                QrCodeResult result = await gameSvc.PostData4ScanAsync("", targetPath, 0);
                using (HttpClient httpclient = new HttpClient())
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = await httpclient.GetStreamAsync(result.Data.QrCodeUrl);
                    bitmap.EndInit();
                    popup.QrCode = bitmap;
                }
            }
            catch (Exception e)
            {
                log.Error(e);
                popup.ShowError();
            }
            capturingImage = false;
            StartStopWait();
            StopSign();
            
           
        }

        //拍照后，清空画笔
        private void StopSign()
        {
            ElementInkCanvas.ClearStrokes();
            ElementToolBar.Visibility = Visibility.Collapsed;
            ElementInkCanvas.IsEnabled = false;
        }

        private void InitData()
        {

        }

        private void StartStopWait()
        {
           LoadingAdorner.IsAdornerVisible = !LoadingAdorner.IsAdornerVisible;
           _kinectVideoSource.Pause = !_kinectVideoSource.Pause;
        }

        public void Dispose()
        {
            kinectPlayer.Source = null;
            if (_kinectVideoSource != null)
            {
                _kinectVideoSource.Dispose();
            }
        }

        private void EraserControl_OnClearEvent()
        {
            ElementInkCanvas.ClearStrokes();
        }

        private void EraserControl_OnCloseEraserToolEvent()
        {
            ElementToolBar.Visibility = Visibility.Collapsed;
            ElementInkCanvas.IsEnabled = false;
            ElementInkCanvas.SetInkCanvasEditingMode(InkCanvasEditingMode.Ink);
        }

        private void EraserControl_OnEraserEvent(bool isEraser)
        {
            if (isEraser)
            {
                ElementInkCanvas.SetInkCanvasEditingMode(InkCanvasEditingMode.EraseByPoint);
            }
            else
            {
                ElementInkCanvas.SetInkCanvasEditingMode(InkCanvasEditingMode.Ink);
            }
        }

        private void EraserControl_OnPenColorEvent(SolidColorBrush brush)
        {
            ElementInkCanvas.SetBrush(brush);
        }

        private void EraserControl_OnPenSizeEvent(int obj)
        {
            ElementInkCanvas.SetBrushScale(obj);
        }
        
    }

    public enum AppPage
    {
        Home, ScanWeiXin,Preview
    }
}
