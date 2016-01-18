using SensngGame.ClientSDK;
using SensngGame.ClientSDK.Contract;
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
        private UserActionData firstUser;

        private string afterQrCode;
        private UserActionData afterUser;

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

                var users = await gameSvc.FindScanQrCodeUsersAsync("5322");
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
                await gameSvc.PostDataByUserAsync(firstUser.ActionId.ToString(), System.IO.Path.Combine(Environment.CurrentDirectory, "player.png"), System.IO.Path.Combine(Environment.CurrentDirectory, "playing.png"), Convert.ToInt16(score.Text));
            }
        }

        AwardData awardInfo = null;
        
        private async void acitivityDetails_Click(object sender, RoutedEventArgs e)
        {
            activityDetails.Text = "";
            var users = await gameSvc.GetUsersByActivitiy(50);
            if(users != null && users.Data != null)
            {
                activityDetails.Text += $"Users:{users.Data.Count}" + Environment.NewLine;   
            }
            var awards = await gameSvc.GetAwardsByActivity();

            if (awards != null && awards.Data != null)
            {
                if (awards.Data.Count > 0)
                {
                    awardInfo = awards.Data[0];
                }
                foreach (var award in awards.Data)
                {
                    activityDetails.Text += $"Award:{award.Name}----{award.AwardProduct}" + Environment.NewLine;
                }
            }
            
            var rankUser = await gameSvc.GetRankUsersByActivity("score", 10);
                
            if(rankUser != null && rankUser.Data != null)
            {
                activityDetails.Text += $"RankUsers Count:{rankUser.Data.Count}" + Environment.NewLine;
            }

            var whiteUsers = await gameSvc.GetActivityWhiteListUser();
            if (whiteUsers != null && whiteUsers.Data != null)
            {
                activityDetails.Text += $"WhiteUsers Count:{whiteUsers.Data.Count}" + Environment.NewLine;
            }
        }

        private async void ActivityWinner_Click(object sender, RoutedEventArgs e)
        {
            if (awardInfo != null)
            {
                var winner = await gameSvc.WinAwardByRandom(awardInfo.Id.ToString());
                if(winner != null && winner.Data != null)
                {
                    avatorWinnerImg.Source = new BitmapImage(new Uri(winner.Data.Headimgurl));
                }
            }
        }

        ActivityData activityInfo = null;
        private async void serviceCreate_Click(object sender, RoutedEventArgs e)
        {
            
            //gameSvc = new GameServiceClient("yourKey", weixinAppId.Text, gameId.Text, activityId.Text);
            var data = await gameSvc.GetQrCode4LoginAsync();
            if (data != null && data.Data != null)
            {
                var qrcode = data.Data;
                firstQrCode = qrcode.QrCodeId;
                image.Source = new BitmapImage(new Uri(qrcode.QrCodeUrl));
            }

            var acitivityInfo = await gameSvc.GetActivityInfo();
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            var postBack = await gameSvc.PostData4ScanAsync(System.IO.Path.Combine(Environment.CurrentDirectory, "player.png"),System.IO.Path.Combine(Environment.CurrentDirectory,"playing.png"), Convert.ToInt16(scoreafter.Text));
            if (postBack != null && postBack.Data != null)
            {
                var qrcode = postBack.Data;
                afterQrCode = qrcode.QrCodeId;
                image1.Source = new BitmapImage(new Uri(qrcode.QrCodeUrl));
            }
        }

        private async void GetRankUsers_Click(object sender, RoutedEventArgs e)
        {
            var rankUser = await gameSvc.GetRankUsersByActivity(orderby.Text, 10);

            if (rankUser != null && rankUser.Data != null)
            {
                if(rankUser.Data.Count > 0)
                {
                    var topUser = rankUser.Data[0];
                    avatorRank.Source = new BitmapImage(new Uri(topUser.Headimgurl));
                }
            }
        }
    }
}
