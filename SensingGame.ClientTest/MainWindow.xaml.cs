using SensngGame.ClientSDK;
using SensngGame.ClientSDK.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Threading;

namespace SensingGame.ClientTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameServiceClient gameSvc = null;
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
            //timer.Interval = TimeSpan.FromSeconds(2);
            //timer.Tick += Timer_Tick;
            //timer.Start();
        }

        private string firstQrCode;
        private UserData firstUser;

        private string afterQrCode;
        private UserData afterUser;

        private async void Timer_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(firstQrCode))
            {
                var user = await gameSvc.FindScanQrCodeUserAsync(firstQrCode);
                if (user != null && user.Data != null)
                {
                    firstUser = user.Data;
                    avatorImg.Source = new BitmapImage(new Uri(user.Data.Headimgurl));
                    //await gameSvc.PostDataByUserAsync(firstUser.ActionId.ToString(), null, null, 80);
                }
            }
            if (!string.IsNullOrEmpty(afterQrCode))
            {
                var user1 = await gameSvc.FindScanQrCodeUserAsync(afterQrCode);
                if (user1 != null && user1.Data != null)
                {
                    afterUser = user1.Data;
                    avatorImg1.Source = new BitmapImage(new Uri(user1.Data.Headimgurl));
                }
            }
        }


        private DispatcherTimer timer = new DispatcherTimer();
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            Timer_Tick(null,null);
        }

        private async void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            if(firstUser != null)
            {
                await gameSvc.PostDataByUserAsync(firstUser.ActionId.ToString(), null, null, Convert.ToInt16(score.Text));
            }
        }

        private async void acitivityDetails_Click(object sender, RoutedEventArgs e)
        {
            var users = await gameSvc.GetUsersByActivitiy(50);
            var awards = await gameSvc.GetAwardsByActivity();
            var activtiy = await gameSvc.GetActivityInfo();
            var rankUser = await gameSvc.GetRankUsersByActivity("score", 10);
        }

        private async void ActivityWinner_Click(object sender, RoutedEventArgs e)
        {
            var user = await gameSvc.WinAwardByRandom("3");
            if(user != null && user.Data !=null)
            {
                avatorWinnerImg.Source = new BitmapImage(new Uri(user.Data.Headimgurl));
            }
        }

        private async void serviceCreate_Click(object sender, RoutedEventArgs e)
        {
            gameSvc = new GameServiceClient("yourKey", weixinAppId.Text, gameId.Text, activityId.Text);

            var data = await gameSvc.GetQrCode4LoginAsync();
            if (data != null && data.Data != null)
            {
                var qrcode = data.Data;
                firstQrCode = qrcode.QrCodeId;
                image.Source = new BitmapImage(new Uri(qrcode.QrCodeUrl));
            }
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            var postBack = await gameSvc.PostData4ScanAsync(@"E:\TFS\SensingGame\SensingGame\bin\Debug\welcome.png", @"E:\TFS\SensingGame\SensingGame\bin\Debug\welcome.png", Convert.ToInt16(scoreafter.Text));
            if (postBack != null && postBack.Data != null)
            {
                var qrcode = postBack.Data;
                afterQrCode = qrcode.QrCodeId;
                image1.Source = new BitmapImage(new Uri(qrcode.QrCodeUrl));
            }
        }
    }
}
