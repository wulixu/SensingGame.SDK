using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace VideoElement
{
    /// <summary>
    /// QrCodePopup.xaml 的交互逻辑
    /// </summary>
    public partial class QrCodePopup : Canvas
    {
        private DispatcherTimer autoCloseTimer;
        private int totalTime;
        private Storyboard shakeShake;
        private Storyboard showError;
        public static readonly DependencyProperty QrCodeProperty = DependencyProperty
             .Register("QrCode", typeof(BitmapSource), typeof(QrCodePopup), new FrameworkPropertyMetadata(OnCurrentQrCodeChanged));

        private static void OnCurrentQrCodeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            QrCodePopup q = (QrCodePopup)d;
            BitmapSource qrCode = e.NewValue as BitmapSource;
            q.AppCode.Source = qrCode;
            q.shakeShake.Begin();
        }

        public BitmapSource QrCode
        {
            get { return (BitmapSource)GetValue(QrCodeProperty); }
            set { SetValue(QrCodeProperty, value); }
        }

        public QrCodePopup()
        {
            InitializeComponent();
            InitTimer();
            shakeShake = FindResource("ShakeShake") as Storyboard;
            showError = FindResource("ShowError") as Storyboard;
            showError.Completed += showError_Completed;
        }


        private void InitTimer()
        {
            totalTime = AppConfig.AutoCloseDuration;
            countdown.Text = string.Format("00:{0:00}", totalTime);
            autoCloseTimer = new DispatcherTimer();
            autoCloseTimer.Interval =TimeSpan.FromSeconds(1);
            autoCloseTimer.Tick += (s, e) =>
            {
                totalTime--;
                if (totalTime <= 0)
                {
                    ResetTime();
                    this.Visibility = Visibility.Hidden;
                }
                else
                {
                    countdown.Text = string.Format("00:{0:00}", totalTime);
                }
            };
        }

        public void Show()
        {
            this.Visibility = Visibility.Visible;
        }

        public void ShowError()
        {
            showError.Begin();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            ResetTime();
        }

        private void ResetTime()
        {
            totalTime = AppConfig.AutoCloseDuration;
            countdown.Text = string.Format("00:{0:00}", totalTime);
            autoCloseTimer.Stop();
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            autoCloseTimer.Start();
        }

        void showError_Completed(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
