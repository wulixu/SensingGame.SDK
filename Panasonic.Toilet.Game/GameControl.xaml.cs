using log4net;
using Microsoft.Kinect;
using SensngGame.ClientSDK;
using SensngGame.ClientSDK.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using TronCell.Game.KinectController;
using TronCell.Game.Utils;
using TronCell.Game.ViewModel;

namespace TronCell.Game
{
    /// <summary>
    /// Interaction logic for GameControl.xaml
    /// </summary>
    public partial class GameControl : UserControl, INotifyPropertyChanged
    {
        private GamePhrase _gamePhrase = GamePhrase.None;
        private GameMode _gameMode = GameMode.Auto;
        private int gameTimePerPlayer;

        private KinectSensor _kinect = null;
        private ColorFrameReader colorFrameReader = null;
        private BodyFrameReader bodyFrameRender = null;

        private WriteableBitmap colorBitmap = null;
        private ulong currentlyTrackedBodyId = 0;
        private Player player;

        static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private GameServiceClient m_gameServiceClient = null;
        private UserActionResult m_userActionResult = null;
        #region Private State
        private const int TimerResolution = 2;  // ms
        private const int NumIntraFrames = 3;
        private const int MaxShapes = 10;
        private const double MaxFramerate = 70;
        private const double MinFramerate = 15;
        private const double MinShapeSize = 12;
        private const double MaxShapeSize = 90;
        private const double DefaultDropRate = 1.5;
        private const double DefaultDropSize = 32.0;
        private const double DefaultDropGravity = 1.0;

        private readonly SoundPlayer popSound = new SoundPlayer();
        private readonly SoundPlayer hitSound = new SoundPlayer();
        private readonly SoundPlayer squeezeSound = new SoundPlayer();

        private double dropRate = DefaultDropRate;
        private double dropSize = DefaultDropSize;
        private double dropGravity = DefaultDropGravity;
        private DateTime lastFrameDrawn = DateTime.MinValue;
        private DateTime predNextFrame = DateTime.MinValue;
        private double actualFrameTime;

        private Body[] bodies;

        // Player(s) placement in scene (z collapsed):
        private Rect playerBounds;
        private Rect screenRect;

        private double targetFramerate = MaxFramerate;
        private int frameCount;
        private bool runningGameThread;
        private FallingThings gifts;

        private GameViewModel gameViewModel;

        #endregion Private State

        public ImageSource KinectColorSource
        {
            get { return colorBitmap; }
        }


        public GameControl()
        {

            InitializeComponent();
            SensingBase.Utils.Position p = new SensingBase.Utils.Position();

            ReadSetting();
            var giftF = GiftFactory.Instance;
            _kinect = KinectSensor.GetDefault();

            this.bodyFrameRender = this._kinect.BodyFrameSource.OpenReader();
            this.bodyFrameRender.FrameArrived += bodyFrameRender_FrameArrived;
            this.colorFrameReader = this._kinect.ColorFrameSource.OpenReader();
            this.colorFrameReader.FrameArrived += ColorFrameReader_FrameArrived;
            // create the colorFrameDescription from the ColorFrameSource using Bgra format
            FrameDescription colorFrameDescription = this._kinect.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Bgra);

            // create the bitmap to display
            this.colorBitmap = new WriteableBitmap(colorFrameDescription.Width, colorFrameDescription.Height, 96.0, 96.0, PixelFormats.Bgr32, null);

            player = new Player(currentlyTrackedBodyId, _kinect.CoordinateMapper);


            _kinect.Open();


            var path = Environment.CurrentDirectory + "PlayerLog";
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            var appRoot = AppDomain.CurrentDomain.BaseDirectory;
            var headsDir = appRoot + "Heads/";
            if (!Directory.Exists(headsDir))
                Directory.CreateDirectory(headsDir);

            gameViewModel = new GameViewModel();
            gameViewModel.LoadTopFive();
            scoreRankPanel.DataContext = gameViewModel;
            Loaded += OnLoaded;
            Touch.FrameReported += Touch_FrameReported;
            this.DataContext = this;

            this.Loaded += GameControl_Loaded;

        }

        private void GameControl_Loaded(object sender, RoutedEventArgs e)
        {
            m_gameServiceClient = new GameServiceClient("j;lajdf;jaiuefjf", ConfigurationManager.AppSettings["WeixinAppId"], "12", ConfigurationManager.AppSettings["ActivityId"]);
            RefreshQrcode();
        }

        private void RefreshQrcode()
        {
            m_userActionResult = null;
            Task<QrCode> qrTask = Task.Factory.StartNew<QrCode>(() =>
            {
                while (true)
                {
                    var qrCodeResult = m_gameServiceClient.GetQrCode4LoginAsync().Result;
                    if (qrCodeResult != null)
                    {
                        QrCode qrCode = new QrCode();
                        WebClient webClient = new WebClient();
                        byte[] data = webClient.DownloadData(qrCodeResult.Data.QrCodeUrl);
                        MemoryStream memoryStream = new MemoryStream(data);
                        qrCode.QrcodeId = qrCodeResult.Data.QrCodeId;
                        qrCode.Stream = memoryStream;
                        return qrCode;
                    }
                    else
                    {
                        Thread.Sleep(500);
                    }
                }
            });

            Task<QrCode> setImageTask = qrTask.ContinueWith<QrCode>((t) =>
            {
                QrCode qrCode = t.Result;
                if (qrCode == null)
                {
                    MessageBox.Show("二维码下戴失败");
                    return null;
                }

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = qrCode.Stream;
                bitmap.EndInit();
                WelcomeScreen.qrCodeStart.Source = bitmap;

                return qrCode;

            }, TaskScheduler.FromCurrentSynchronizationContext());

            Task<UserActionResult> longPullTask = setImageTask.ContinueWith<UserActionResult>((t) =>
            {
                QrCode qrCode = t.Result;
                if (qrCode != null)
                {
                    DateTime expiredTime = DateTime.Now.AddMinutes(3.0);

                    while (true)
                    {
                        try
                        {
                            UserActionResult actionReuslt = m_gameServiceClient.FindScanQrCodeUserAsync(qrCode.QrcodeId)
                                                                                .Result;
                            if (actionReuslt.Data != null)
                            {
                                try
                                {
                                    var headDir = AppDomain.CurrentDomain.BaseDirectory + "weixinheads/";
                                    if (!Directory.Exists(headDir)) Directory.CreateDirectory(headDir);

                                    if (string.IsNullOrEmpty(actionReuslt.Data.Headimgurl))
                                    {
                                        actionReuslt.Data.Headimgurl = "nohead.jpg";
                                        return actionReuslt;
                                    }

                                    var headPath = headDir + actionReuslt.Data.Headimgurl.GetHashCode() + ".jpg";
                                    if (File.Exists(headPath))
                                    {
                                        actionReuslt.Data.Headimgurl = actionReuslt.Data.Headimgurl.GetHashCode() + ".jpg";
                                        return actionReuslt;
                                    }

                                    var downloadPath = headDir + Guid.NewGuid() + ".downloading";
                                    WebClient webClient = new WebClient();
                                    webClient.DownloadFile(actionReuslt.Data.Headimgurl, headPath);
                                    File.Move(downloadPath, headPath);
                                    actionReuslt.Data.Headimgurl = actionReuslt.Data.Headimgurl.GetHashCode() + ".jpg";
                                    
                                }
                                catch (Exception)
                                { }

                                return actionReuslt;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        Thread.Sleep(800);
                        if (DateTime.Now > expiredTime)
                            break;
                    }
                }
                return null;
            }, TaskScheduler.Default);

            longPullTask.ContinueWith((t) =>
            {
                UserActionResult actionResult = t.Result;
                if (actionResult != null)
                {
                    m_userActionResult = actionResult;
                    WelcomePage_OnNextScreened(null, null);
                }
                else
                {
                    Console.WriteLine("Expired Cancel Longpull Task");
                    RefreshQrcode();
                }

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }


        // Since the timer resolution defaults to about 10ms precisely, we need to
        // increase the resolution to get framerates above between 50fps with any
        // consistency.
        [DllImport("Winmm.dll", EntryPoint = "timeBeginPeriod")]
        private static extern int TimeBeginPeriod(uint period);


        private DateTime _idleTouchSpan;
        void Touch_FrameReported(object sender, TouchFrameEventArgs e)
        {
            _idleTouchSpan = DateTime.Now;
        }

        private bool isSendMail = false;
        private int defaultElevationAngle = 0;
        private void ReadSetting()
        {
            var time = ConfigurationManager.AppSettings["GameTimePerPlayer"];
            gameTimePerPlayer = int.Parse(time);
            defaultElevationAngle = int.Parse(ConfigurationManager.AppSettings["ElevationAngle"]);
        }


        private void On_Close()
        {
            Environment.Exit(0);

        }

        private void ColorFrameReader_FrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            // ColorFrame is IDisposable
            using (ColorFrame colorFrame = e.FrameReference.AcquireFrame())
            {
                if (colorFrame != null)
                {
                    FrameDescription colorFrameDescription = colorFrame.FrameDescription;

                    using (KinectBuffer colorBuffer = colorFrame.LockRawImageBuffer())
                    {
                        this.colorBitmap.Lock();

                        // verify data and write the new color frame data to the display bitmap
                        if ((colorFrameDescription.Width == this.colorBitmap.PixelWidth) && (colorFrameDescription.Height == this.colorBitmap.PixelHeight))
                        {
                            colorFrame.CopyConvertedFrameDataToIntPtr(
                                this.colorBitmap.BackBuffer,
                                (uint)(colorFrameDescription.Width * colorFrameDescription.Height * 4),
                                ColorImageFormat.Bgra);

                            this.colorBitmap.AddDirtyRect(new Int32Rect(0, 0, this.colorBitmap.PixelWidth, this.colorBitmap.PixelHeight));
                        }

                        this.colorBitmap.Unlock();
                    }
                }
            }
        }

        void bodyFrameRender_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            if (CurrentGameMode == GameMode.Auto)
            {
                return;
            }

            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame != null)
                {
                    if (this.bodies == null)
                    {
                        this.bodies = new Body[bodyFrame.BodyCount];
                    }

                    bodyFrame.GetAndRefreshBodyData(this.bodies);
                    ChooseSkeleton();
                    PlayerUpdate();
                }
            }
        }

        private void ChooseSkeleton()
        {
            var isTrackedSkeltonVisible = false;
            var nearestDistance = float.MaxValue;
            ulong nearestSkeleton = 0;
            int nearestBodyIndex = 0;
            for (int i = 0; i < bodies.Count(); i++)
            {
                var skel = bodies[i];
                if (null == skel)
                {
                    continue;
                }

                if (!skel.IsTracked)
                {
                    continue;
                }

                if (skel.TrackingId == this.currentlyTrackedBodyId)
                {
                    isTrackedSkeltonVisible = true;
                    break;
                }

                if (skel.Joints[JointType.Head].Position.Z < nearestDistance)
                {
                    nearestDistance = skel.Joints[JointType.Head].Position.Z;
                    nearestSkeleton = skel.TrackingId;
                    nearestBodyIndex = i;
                }
            }

            if (!isTrackedSkeltonVisible && nearestSkeleton != 0)
            {
                this.currentlyTrackedBodyId = nearestSkeleton;

            }

        }

        private void PlayerUpdate()
        {
            var body = bodies.SingleOrDefault(b => b.IsTracked && b.TrackingId == currentlyTrackedBodyId);
            if (body != null)
            {
                player.Id = currentlyTrackedBodyId;
                player.LastUpdated = DateTime.Now;
                player.IsAlive = true;
                CameraSpacePoint position = body.Joints[JointType.SpineBase].Position;
                DepthSpacePoint depthSpacePoint = _kinect.CoordinateMapper.MapCameraPointToDepthSpace(position);
                ColorSpacePoint colorSpacePoint = _kinect.CoordinateMapper.MapCameraPointToColorSpace(position);

                player.UpdatePlayerLocation(body.Joints);

                if (player.HeadLocation.X > 0 && player.HeadLocation.X < 1920
                    && player.HeadLocation.Y > 0 && player.HeadLocation.Y < 1080)
                {
                    Canvas.SetLeft(ellipseHead, player.HeadLocation.X - ellipseHead.Width / 2);
                    Canvas.SetTop(ellipseHead, player.HeadLocation.Y - ellipseHead.Height / 2);
                }
            }
            else
            {

                player.IsAlive = false;
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {

            this.gifts = new FallingThings(MaxShapes, this.targetFramerate, NumIntraFrames);
            this.UpdatePlayfieldSize();

            this.gifts.SetGravity(this.dropGravity);
            this.gifts.SetDropRate(this.dropRate);
            this.gifts.SetSize(this.dropSize);
            this.gifts.SetPolies(PolyType.All);
            this.gifts.SetGameMode(GameMode.Off);

            try
            {
                this.popSound.Stream = TronCell.Game.Properties.Resources.Pop_5;
                this.hitSound.Stream = TronCell.Game.Properties.Resources.Hit_2;
                this.squeezeSound.Stream = TronCell.Game.Properties.Resources.Squeeze;
                this.BGMusic.Source = new Uri(Environment.CurrentDirectory + "bg.mp3");
                this.BGMusic.Play();
            }
            catch (Exception ex)
            {
                logger.Error("The background music cannot be loaded successfully, please check the bg.mp3 file in the current application directory.");
            }

            GamePhrase = GamePhrase.EnterGame;

            TimeBeginPeriod(TimerResolution);
            var myGameThread = new Thread(this.GameThread);
            myGameThread.SetApartmentState(ApartmentState.STA);
            myGameThread.Start();

            BeginScreen.AnimationFinished += BeginGame;

        }


        private void UpdatePlayfieldSize()
        {
            // Size of player wrt size of playfield, putting ourselves low on the screen.
            this.screenRect.X = 0;
            this.screenRect.Y = 0;
            this.screenRect.Width = this.Carrier.ActualWidth;
            this.screenRect.Height = this.Carrier.ActualHeight;

            BannerText.UpdateBounds(this.screenRect);

            this.playerBounds.X = 0;
            this.playerBounds.Width = this.Carrier.ActualWidth;
            this.playerBounds.Y = this.Carrier.ActualHeight * 0.2;
            this.playerBounds.Height = this.Carrier.ActualHeight * 0.75;


            player.SetBounds(this.playerBounds);


            Rect fallingBounds = this.playerBounds;
            fallingBounds.Y = 0;
            fallingBounds.Height = Carrier.ActualHeight;
            if (this.gifts != null)
            {
                this.gifts.SetBoundaries(fallingBounds);
            }
        }


        private void BeginGame(object sender, RoutedEventArgs e)
        {
            this.GamePhrase = Game.GamePhrase.Gaming;
        }

        private void GameThread()
        {
            this.runningGameThread = true;
            this.predNextFrame = DateTime.Now;
            this.actualFrameTime = 1000.0 / this.targetFramerate;
            // Try to dispatch at as constant of a framerate as possible by sleeping just enough since
            // the last time we dispatched.
            while (this.runningGameThread)
            {
                // Calculate average framerate.  
                DateTime now = DateTime.Now;
                if (this.lastFrameDrawn == DateTime.MinValue)
                {
                    this.lastFrameDrawn = now;
                }

                double ms = now.Subtract(this.lastFrameDrawn).TotalMilliseconds;
                this.actualFrameTime = (this.actualFrameTime * 0.95) + (0.05 * ms);
                this.lastFrameDrawn = now;

                // Adjust target framerate down if we're not achieving that rate
                this.frameCount++;
                if ((this.frameCount % 100 == 0) && (1000.0 / this.actualFrameTime < this.targetFramerate * 0.92))
                {
                    this.targetFramerate = Math.Max(MinFramerate, (this.targetFramerate + (1000.0 / this.actualFrameTime)) / 2);
                }

                if (now > this.predNextFrame)
                {
                    this.predNextFrame = now;
                }
                else
                {
                    double milliseconds = this.predNextFrame.Subtract(now).TotalMilliseconds;
                    if (milliseconds >= TimerResolution)
                    {
                        Thread.Sleep((int)(milliseconds + 0.5));
                    }
                }

                this.predNextFrame += TimeSpan.FromMilliseconds(1000.0 / this.targetFramerate);

                this.Dispatcher.Invoke(DispatcherPriority.Send, new Action<int>(this.HandleGameTimer), 0);
            }
        }

        private int autoPaddleMoveFactor = -1;
        private void HandleGameTimer(int param)
        {
            if (!runningGameThread) return;
            // Every so often, notify what our actual framerate is
            if ((this.frameCount % 100) == 0)
            {
                this.gifts.SetFramerate(1000.0 / this.actualFrameTime);
            }
            frameRateMsg.Content = "Frame Rate " + 1000.0 / this.actualFrameTime;

            if (GamePhrase == GamePhrase.InputProfile)
            {
                TimeSpan idleTouchSpan = DateTime.Now.Subtract(_idleTouchSpan);

                if (idleTouchSpan.TotalSeconds > 90)
                {
                    this.GamePhrase = GamePhrase.EnterGame;
                }
            }

            // Advance animations, and do hit testing.
            for (int i = 0; i < NumIntraFrames; ++i)
            {

                CollisionPaddle(player);

                if (CurrentGameMode == GameMode.Off)
                {
                    CollisionPaddle(new Player(0, null));
                }
                if (CurrentGameMode == GameMode.Auto)
                {
                    CollisionPaddle(new Player(0, null));
                }
                this.gifts.AdvanceFrame();
            }

            // Draw new Wpf scene by adding all objects to canvas
            Carrier.Children.Clear();
            this.gifts.DrawFrame(this.Carrier.Children);
            Carrier.Children.Add(PaddleController);
            FlyingText.Draw(this.Carrier.Children);

            if (CurrentGameMode == GameMode.Auto)
            {
                //random set increment.
                var original = Canvas.GetLeft(PaddleController);
                var increment = 3 * autoPaddleMoveFactor;
                var paddleX = original + increment;
                if (paddleX < 0)
                {
                    paddleX = 0;
                    autoPaddleMoveFactor = 1;
                }
                if (paddleX > Carrier.Width - PaddleController.Width)
                {
                    paddleX = Carrier.Width - PaddleController.Width;
                    autoPaddleMoveFactor = -1;
                }
                Canvas.SetLeft(PaddleController, paddleX);
            }
            else
            {
                if (player != null && player.IsAlive)
                {
                    SetPaddleControllerLocation(player.PlayerLocation.X);
                }

                if (this.GamePhrase == Game.GamePhrase.Gaming)
                {
                    TimeSpan span = gifts.GetGameDurationTime();
                    if (span != TimeSpan.Zero)
                    {
                        if (span.TotalSeconds > gameTimePerPlayer)
                        {
                            //Game is over;
                            this.GamePhrase = GamePhrase.GameOver;
                        }
                        else
                        {
                            var remaining = "00:" + (gameTimePerPlayer - span.TotalSeconds).ToString("00");
                            Time.Content = remaining;
                        }
                    }
                    else
                    {
                        Time.Content = "00:" + gameTimePerPlayer.ToString("00");
                    }
                    Score.Content = gifts.GetFirstPlayerScore();

                }
            }

        }

        private void CollisionPaddle(Player player)
        {

            HitType hit = this.gifts.LookForHits(PaddleController.CallisionRect, player.Id);
            if ((hit & HitType.Squeezed) != 0)
            {
                this.hitSound.Play();
            }
            else if ((hit & HitType.Popped) != 0)
            {
                this.popSound.Play();
            }
            else if ((hit & HitType.Hand) != 0)
            {

                this.squeezeSound.Play();
            }
        }

        private void DoGameOver()
        {
            var score = gifts.GetFirstPlayerScore();

            ScaleTransform scale = new ScaleTransform(0.5, 0.5, 1920 / 2, 1080 / 2);
            TransformedBitmap scaleBitmap = new TransformedBitmap(colorBitmap, scale);
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(scaleBitmap));

            var uploadPath = AppDomain.CurrentDomain.BaseDirectory + "Heads/" + DateTime.Now.ToString("yyyyMMdd_mmhhss") + ".jpg";
            using (FileStream fs = File.Open(uploadPath, FileMode.Create))
            {
                encoder.Save(fs);
            }
            m_gameServiceClient.PostDataByUserAsync(m_userActionResult.Data.ActionId + "",
                                               null, uploadPath, score);



            var headImagePath = "weixinheads/" + m_userActionResult.Data.Headimgurl;

            gameViewModel.AddNewScore(score, headImagePath, m_userActionResult.Data.Id);

            RefreshQrcode();

            this.RankListScreen.ScoreMsg.Text = score.ToString();
            this.RankListScreen.SetHeadImage(headImagePath);


            this.gifts.CanStartTime = false;
            CurrentGameMode = GameMode.Auto;
            this.gifts.SetDropRate(DefaultDropRate * 0.5);
            this.RankListScreen.Opacity = 0;

            this.RankListScreen.Visibility = Visibility.Visible;
            this.RankListScreen.BeginAnimation(UIElement.OpacityProperty, CreateAnimation(0, 1, 600, (s, args) =>
            {
                this.RankListScreen.Visibility = Visibility.Visible;
            }));
            CurrentGameMode = GameMode.Auto;


            this.RankListScreen.Start();
        }

        private void SetPaddleControllerLocation(double playerOffset)
        {

            double center = (Carrier.Width - PaddleController.Width) / 2;
            double paddleX = center + playerOffset;
            if (paddleX < 0)
            {
                paddleX = 0;
            }
            if (paddleX > Carrier.Width - PaddleController.Width)
            {
                paddleX = Carrier.Width - PaddleController.Width;
            }
            Canvas.SetLeft(PaddleController, paddleX);

        }

        private DateTime gameStartTime;
        private void Carrier_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Size of player wrt size of playfield, putting ourselves low on the screen.
            this.screenRect.X = 0;
            this.screenRect.Y = 0;
            this.screenRect.Width = this.Carrier.ActualWidth;
            this.screenRect.Height = this.Carrier.ActualHeight;

            //BannerText.UpdateBounds(this.screenRect);

            this.playerBounds.X = 0;
            this.playerBounds.Width = this.Carrier.ActualWidth;
            this.playerBounds.Y = 100;
            this.playerBounds.Height = 810;


            player.SetBounds(this.playerBounds);


            Rect fallingBounds = this.playerBounds;
            fallingBounds.Y = 0;
            fallingBounds.Height = Carrier.ActualHeight;
            //if (this.myFallingThings != null)
            //{
            //    this.myFallingThings.SetBoundaries(fallingBounds);
            //}
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            logger.Error("WindowClosing was invoked, sensorChooser.Stop()");
            this.runningGameThread = false;
            if (this.colorFrameReader != null)
            {
                this.colorFrameReader.Dispose();
                this.colorFrameReader = null;
            }
            if (this.bodyFrameRender != null)
            {
                this.bodyFrameRender.Dispose();
                this.bodyFrameRender = null;
            }
            if (this._kinect != null)
            {
                this._kinect.Close();
                this._kinect = null;
            }
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            logger.Error("WindowClosed was invoked, KinectSensor.Dispose()");
            //this.KinectSensorManager.KinectSensor = null;
            Application.Current.Shutdown();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

            //ImageSaver.SaveImage4FrameworkElement(this.Carrier, targetPath);
        }

        private void WelcomePage_OnNextScreened(object sender, RoutedEventArgs e)
        {
            GamePhrase = GamePhrase.WaitingPlayer;
        }


        private AnimationTimeline CreateAnimation(double from, double to, int milliSeconds, EventHandler whenDone = null)
        {
            IEasingFunction ease = new ElasticEase() { EasingMode = EasingMode.EaseIn };
            //IEasingFunction ease = new BackEase { Amplitude = 0.5, EasingMode = EasingMode.EaseOut };
            //var duration = new Duration(TimeSpan.FromSeconds(0.5));
            //var anim = new DoubleAnimation(from, to, duration) { EasingFunction = ease };
            var duration = new Duration(TimeSpan.FromMilliseconds(milliSeconds));
            var anim = new DoubleAnimation(from, to, duration) { EasingFunction = ease };
            if (whenDone != null)
                anim.Completed += whenDone;
            anim.Freeze();
            return anim;
        }

        private AnimationTimeline CreateDropAnimation(double from, double to, int milliSeconds, EventHandler whenDone = null)
        {
            IEasingFunction ease = new CircleEase() { EasingMode = EasingMode.EaseIn };
            //IEasingFunction ease = new BackEase { Amplitude = 0.5, EasingMode = EasingMode.EaseOut };
            //var duration = new Duration(TimeSpan.FromSeconds(0.5));
            //var anim = new DoubleAnimation(from, to, duration) { EasingFunction = ease };
            var duration = new Duration(TimeSpan.FromMilliseconds(milliSeconds));
            var anim = new DoubleAnimation(from, to, duration) { EasingFunction = ease };
            if (whenDone != null)
                anim.Completed += whenDone;
            anim.Freeze();
            return anim;
        }

        private void PlayerInfoLogin_OnNextScreened(object sender, RoutedEventArgs e)
        {
            GamePhrase = GamePhrase.WaitingPlayer;
        }

        public GameMode CurrentGameMode
        {
            get { return this._gameMode; }
            set
            {
                if (this._gameMode != value)
                {
                    this._gameMode = value;
                    this.gifts.SetGameMode(value);
                }
            }
        }

        public GamePhrase GamePhrase
        {
            get { return _gamePhrase; }
            set
            {
                if (_gamePhrase != value)
                {
                    //this.DogImg.Visibility = Visibility.Visible;
                    _gamePhrase = value;
                    if (_gamePhrase == GamePhrase.EnterGame)
                    {
                        logger.Debug("The current phrase is EnterGame Screen.");
                        CurrentGameMode = GameMode.Auto;
                        this.gifts.SetDropRate(DefaultDropRate * 0.5);
                        this.WelcomeScreen.Opacity = 0;
                        this.WelcomeScreen.Visibility = Visibility.Visible;
                        this.WelcomeScreen.BeginAnimation(UIElement.OpacityProperty, CreateAnimation(0, 1, 400, (s, args) =>
                        {
                            //this.PlayerInfoLogin.ClearPlayer();
                            this.Score.Content = "0";
                            this.Time.Content = "00:00";
                            this.WelcomeScreen.Visibility = Visibility.Visible;
                        }));
                        //if (PlayerInfoLogin.Visibility == Visibility.Visible)
                        //{
                        //    this.PlayerInfoLogin.BeginAnimation(UIElement.OpacityProperty, CreateAnimation(1, 0, 600, (s, args) => PlayerInfoLogin.Visibility = Visibility.Hidden));
                        //}
                    }
                    if (_gamePhrase == GamePhrase.InputProfile)
                    {
                        logger.Debug("The current phrase is InputProfile Screen.");
                        _idleTouchSpan = DateTime.Now;
                        //this.PlayerInfoLogin.Opacity = 0;
                        //this.PlayerInfoLogin.Visibility = Visibility.Visible;
                        this.WelcomeScreen.BeginAnimation(UIElement.OpacityProperty, CreateAnimation(1, 0, 300, (s, args) =>
                        {
                            WelcomeScreen.Visibility = Visibility.Hidden;
                        }));
                        //this.PlayerInfoLogin.BeginAnimation(UIElement.OpacityProperty, CreateAnimation(0, 1, 600, (s, args) =>
                        //{
                        //    PlayerInfoLogin.Visibility = Visibility.Visible;
                        //    PlayerInfoLogin.Phone.Focus();
                        //}));
                    }
                    if (_gamePhrase == GamePhrase.WaitingPlayer)
                    {
                        logger.Debug("The current phrase is WaitingPlayer Screen.");
                        CurrentGameMode = GameMode.Off;
                        gifts.Clear();
                        gifts.SetDropRate(0);
                        this.WelcomeScreen.Visibility = Visibility.Hidden;
                        //this.PlayerInfoLogin.BeginAnimation(UIElement.OpacityProperty, CreateAnimation(1, 0, 600, (s, args) =>
                        //{
                        //    PlayerInfoLogin.Visibility = Visibility.Hidden;

                        //}));

                        this.Score.Content = "0";
                        this.Time.Content = "00:" + gameTimePerPlayer.ToString("00");


                        //Begin the hint message.
                        this.HintImage.Visibility = Visibility.Visible;
                        this.HintImage.BeginAnimation(UIElement.OpacityProperty, CreateAnimation(1, 0, 3000, (s, args) =>
                        {
                            this.HintImage.Visibility = Visibility.Hidden;
                            BeginScreen.Start();
                        }));
                    }
                    if (_gamePhrase == GamePhrase.Gaming)
                    {

                        this.gifts.SetDropRate(DefaultDropRate);
                        this.gifts.SetGravity(DefaultDropGravity);
                        this.gifts.CanStartTime = true;
                        CurrentGameMode = GameMode.Off;

                    }
                    if (_gamePhrase == GamePhrase.GameOver)
                    {

                        DoGameOver();
                    }
                }
            }
        }


        private void RankListScreen_OnNextScreened(object sender, RoutedEventArgs e)
        {

            this.RankListScreen.BeginAnimation(UIElement.OpacityProperty, CreateAnimation(1, 0, 400, (s, args) =>
            {
                this.RankListScreen.Visibility = Visibility.Hidden;
            }));
            GamePhrase = GamePhrase.EnterGame;
        }


        private void BGMusic_OnMediaEnded(object sender, RoutedEventArgs e)
        {
            this.BGMusic.Position = TimeSpan.FromSeconds(0);
            this.BGMusic.Play();
        }


        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        int _clickCount = 0;
        DateTime _lastPressed = DateTime.Now;
        Key _lastKey;


        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (_clickCount > 0)
            {
                _clickCount = 0;
                if (DateTime.Now.Subtract(_lastPressed).TotalMilliseconds < 500 && _lastKey == e.Key)
                {
                    if (e.Key == Key.H)
                    {
                        debugCanvas.Visibility = debugCanvas.Visibility == Visibility.Visible ?
                                                 Visibility.Collapsed : Visibility.Visible;
                    }

                }
            }
            _lastPressed = DateTime.Now;
            _clickCount++;
            _lastKey = e.Key;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void btnTurnOff_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var fetchRankUsersTask  = Task.Factory.StartNew<string>(() =>
            {
                int tryCount = 0;
                List<UserActionData> userlist = null;
                while (tryCount < 3)
                {
                    var userActionsResult = m_gameServiceClient.GetRankUsersByActivity("score", 7).Result;
                    if(userActionsResult.Data != null)
                    {
                        userlist = userActionsResult.Data;
                        break;
                    }
                    tryCount++;
                }
                tryCount = 0;
                List<AwardData> awardList = null;
                while(tryCount < 3)
                {
                    var  awardResult = m_gameServiceClient.GetAwardsByActivity().Result;
                    if(awardResult.Data != null)
                    {
                        awardList = awardResult.Data.OrderBy(a => a.AwardSeq).ToList();
                        break;
                    }
                }
                tryCount = 0;

                if (userlist == null)
                    return "获取玩家列表为空";
                if (awardList == null)
                    return "获取奖品列表为空";

                int awardCount = 0;
                string errors = null;
                for (int i = 0; i < awardList.Count; i++)
                {
                    var award = awardList[i];
                    for (int j = 0; j < award.PlanQty - award.ActualQty; j++)
                    {
                        if (userlist.Count <= awardCount)
                            break;
                        var user = userlist[awardCount];
                        var userAwardResult = m_gameServiceClient.WinAwardByUser(award.Id.ToString(), user.Id.ToString()).Result;
                        if(userAwardResult.Data == null)
                        {
                            if (errors == null) errors = "";
                            errors += " " + award.Id + " " + user.Id + " failed";
                        }
                        awardCount++;
                    }
                }
                return null;
            });

            fetchRankUsersTask.ContinueWith((t) =>
            {
                string errors = t.Result;
                if(errors != null)
                {
                    MessageBox.Show(errors);
                }
                MessageBox.Show("奖品发放完毕。");
            }, TaskScheduler.FromCurrentSynchronizationContext());



        }
    }

    public class QrCode
    {
        public string QrcodeId { get; set; }
        public MemoryStream Stream { get; set; }
    }

    public enum GamePhrase
    {
        None,
        EnterGame,
        InputProfile,
        WaitingPlayer,
        Gaming,
        GameOver
    }
}
