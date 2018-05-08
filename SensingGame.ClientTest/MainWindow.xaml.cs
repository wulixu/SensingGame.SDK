using SensngGame.ClientSDK;
using SensngGame.ClientSDK.Contract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace SensingGame.ClientTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameServiceClient gameSvc = null;

        private Dictionary<string, EnumQRStatus> qrcodeTypes = new Dictionary<string, EnumQRStatus>();
        public MainWindow()
        {

            qrcodeTypes.Add("游戏之前", EnumQRStatus.BeforeGame);
            qrcodeTypes.Add("游戏之后", EnumQRStatus.AfterGame);
            qrcodeTypes.Add("活动介绍", EnumQRStatus.ActivityIntroduction);
            qrcodeTypes.Add("活动注册", EnumQRStatus.ActivityRegister);
            qrcodeTypes.Add("在线游戏", EnumQRStatus.OnlineGame);
            qrcodeTypes.Add("游戏结果", EnumQRStatus.GameResult);
            qrcodeTypes.Add("中奖信息", EnumQRStatus.Award);

            InitializeComponent();
            Loaded += MainWindow_Loaded;
            AssembleGames();
            platformCBox.Items.Add("WeChat");
            platformCBox.Items.Add("Taobao");

            foreach (var pair in qrcodeTypes)
            {
                qrCodeCBox.Items.Add(pair.Key);
            }
            qrCodeCBox.SelectedIndex = 1;

            orderByCBox.Items.Add("score");
            orderByCBox.Items.Add("likecount");
            orderByCBox.Items.Add("viewcount");

            orderByCBox.SelectedIndex = 1;
        }

        private void AssembleGames()
        {
            gameNoCBox.Items.Add("OFF-M-FindDiff-001");
            gameNoCBox.Items.Add("ON-M-AirSpace-001");
            gameNoCBox.Items.Add("OFF-M-Ballon-001");

            gameNoCBox.Items.Add("OFF-K-Car-001");
            gameNoCBox.Items.Add("OFF-K-Camera-001");
            gameNoCBox.Items.Add("OFF-K-Video-001");
            gameNoCBox.Items.Add("OFF-R-Camera-001");
            gameNoCBox.Items.Add("OFF-R-Video-001");
            gameNoCBox.Items.Add("OFF-K-GreenCamera-001");
            gameNoCBox.Items.Add("OFF-K-GreenVideo-001");
            gameNoCBox.Items.Add("OFF-R-GreenCamera-001");
            gameNoCBox.Items.Add("OFF-R-GreenVideo-001");
            gameNoCBox.Items.Add("OFF-K-Catch-001");
            gameNoCBox.Items.Add("OFF-T-Linlink-001");
            gameNoCBox.Items.Add("OFF-T-Bird-001");
            gameNoCBox.Items.Add("OFF-K-Fruit-001");
            gameNoCBox.Items.Add("OFF-M-Lottery-001");
            gameNoCBox.Items.Add("ON-M-Love-001");
            gameNoCBox.Items.Add("OFF-K-Dart-001");
            gameNoCBox.Items.Add("OFF-K-Jurassic-001");
            gameNoCBox.Items.Add("OFF-R-Jurassic-001");
            gameNoCBox.Items.Add("OFF-K-Brick-001");
            gameNoCBox.Items.Add("OFF-K-TimeSpace-001");
            gameNoCBox.Items.Add("OFF-R-TimeSpace-001");
            gameNoCBox.Items.Add("OFF-K-Prism-001");
            gameNoCBox.Items.Add("OFF-R-Prism-001");
            gameNoCBox.Items.Add("OFF-K-TrWorld-001");
            gameNoCBox.Items.Add("OFF-R-TrWorld-001");
            gameNoCBox.Items.Add("OFF-T-Arrow-001");
            gameNoCBox.Items.Add("OFF-M-Sign-001");
            gameNoCBox.Items.Add("OFF-T-Mirror-001");

            gameNoCBox.SelectedIndex = 0;
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
                    //avatorImg.Source = new BitmapImage(new Uri(user.Data.Headimgurl));
                    avatorImg.Source = UriToImage(user.Data.Headimgurl);
                    //await gameSvc.PostDataByUserAsync(firstUser.ActionId.ToString(), null, null, 80);
                }

                var users = await gameSvc.FindScanQrCodeUsersAsync(firstQrCode);
                if (users != null && users.Data != null)
                {
                    scanCountBefore.Content = $"{users.Data.Count} 人";
                }

            }
        }


        private DispatcherTimer timer = new DispatcherTimer();
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {


        }

        public void ClearData()
        {
            qrCodeImg.Source = null;
            avatorImg.Source = null;
            avatorRank.Source = null;
            avatorWinnerImg.Source = null;
            awardUserImg.Source = null;
        }

        private async void ScannedAvator_Click(object sender, RoutedEventArgs e)
        {
            Timer_Tick(null, null);
        }

        private async void PostDataByUserClick(object sender, RoutedEventArgs e)
        {
            if (firstUser != null)
            {
                await gameSvc.PostDataByUserAsync(firstUser.ActionId.ToString(), System.IO.Path.Combine(Environment.CurrentDirectory, "player.png"), System.IO.Path.Combine(Environment.CurrentDirectory, "playing.png"), Convert.ToInt16(score.Text));
            }
        }

        AwardData awardInfo = null;

        

        private async void ActivityWinner_Click(object sender, RoutedEventArgs e)
        {
            if (awardInfo != null)
            {
                var winner = await gameSvc.WinAwardByRandom(awardInfo.Id.ToString());
                if (winner != null && winner.Data != null)
                {
                    //avatorWinnerImg.Source = new BitmapImage(new Uri(winner.Data.Headimgurl));
                    avatorWinnerImg.Source = UriToImage(winner.Data.Headimgurl);
                }
            }
        }

        ActivityData activityInfo = null;
        private async void ServiceCreate_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
            var gameNo = gameNoCBox.SelectedValue as string;
            gameSvc = new GameServiceClient(subKey.Text, gameNo);
            FillActivityAndGameInfo();
        }

        public static BitmapSource UriToImage(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl)) return null;
            if (imageUrl.Contains("mp.weixin.qq.com/cgi-bin/showqrcode") || imageUrl.Contains("wx.qlogo.cn"))
            {
                return WebImageToImage(imageUrl);
            }
            else
            {
                return ValueToImage(imageUrl);
            }
        }

        public static BitmapSource WebImageToImage(string imageUrl)
        {
            var imageBytes = new WebClient().DownloadData(imageUrl);
            MemoryStream ms = new MemoryStream(imageBytes);
            BitmapImage bmImage = new BitmapImage();
            bmImage.BeginInit();
            bmImage.StreamSource = ms;
            bmImage.EndInit();
            return bmImage;
        }

        public static BitmapSource ValueToImage(string qrcode)
        {

            IBarcodeWriter writer = new BarcodeWriter{ Format = BarcodeFormat.QR_CODE };
            var bitmap = writer.Write(qrcode);

            var hbmp = bitmap.GetHbitmap();
            BitmapSource source;
            try
            {
                source = Imaging.CreateBitmapSourceFromHBitmap(hbmp, IntPtr.Zero, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
                
            }
            finally
            {
                //DeleteObject(hbmp);
            }
            return source;
        }

        public static BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }


        private async void CreateQrcode_Click(object sender, RoutedEventArgs e)
        {
            var platformType = platformCBox.SelectedValue as string;
            if (platformType == "WeChat")
            {
                var qrcodeTypeString = qrCodeCBox.SelectedValue as string;
                var qrcodeType = qrcodeTypes[qrcodeTypeString];

                if (qrcodeType == EnumQRStatus.AfterGame)
                {
                    var postBack = await gameSvc.PostData4ScanAsync(System.IO.Path.Combine(Environment.CurrentDirectory, "player.png"), System.IO.Path.Combine(Environment.CurrentDirectory, "playing.png"), Convert.ToInt16(scoreafter.Text));
                    if (postBack != null && postBack.Data != null)
                    {
                        var qrcode = postBack.Data;
                        afterQrCode = qrcode.QrCodeId;
                        qrCodeImg.Source = UriToImage(qrcode.QrCodeUrl);
                    }
                }
                else
                {
                    var data = await gameSvc.GetQrCode4LoginAsync(qrcodeType);
                    if (data != null && data.Data != null)
                    {
                        var qrcode = data.Data;
                        firstQrCode = qrcode.QrCodeId;
                        qrCodeImg.Source = UriToImage(qrcode.QrCodeUrl);
                    }
                }
            }
            else
            {
                var postBack = await gameSvc.PostActionToTao4Qrcode(System.IO.Path.Combine(Environment.CurrentDirectory, "player.png"), "TestGame", 100, ActionStatus.Register);
                qrCodeImg.Source = UriToImage(postBack);
            }
        }

        private async void GetRankUsers_Click(object sender, RoutedEventArgs e)
        {
            var rankMax = int.Parse(rankPos.Text);
            var orderby = orderByCBox.SelectedValue as string;
            var rankUser = await gameSvc.GetRankUsersByActivity(orderby, rankMax);

            if (rankUser != null && rankUser.Data != null)
            {
                if (rankUser.Data.Count >= rankMax)
                {
                    var topUser = rankUser.Data[rankMax - 1];
                    //avatorRank.Source = new BitmapImage(new Uri(topUser.Headimgurl));
                    avatorRank.Source = UriToImage(topUser.Headimgurl);
                    rankMsg.Content = $"Id:{topUser.Id}--OpenId:{topUser.Openid}--Nickname:{topUser.Nickname}";
                }
                else
                {
                    rankMsg.Content = "没有这么多人";
                }
            }
        }

        private async void CreateWinUser_Click(object sender, RoutedEventArgs e)
        {
            var awardId = awardIDBox.Text;
            var userId = winnerIDBox.Text;
            var awardUser = await gameSvc.WinAwardByUser(awardId, userId);
            if(awardUser != null && awardUser.Data != null)
            {
                var user = awardUser.Data;
                //awardUserImg.Source = new BitmapImage(new Uri(user.Headimgurl));
                awardUserImg.Source = UriToImage(user.Headimgurl);
            }
        }

        private void stopAcitivy_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void startActivity_Click(object sender, RoutedEventArgs e)
        {
            var awardUser = await gameSvc.GetUsersByActivityAndGame(100);
        }

        #region basic info
        private void FillActivityAndGameInfo()
        {
            AssembleActivityDetails();
            AssembleActivityGameDetails();
            AssembleGameDetails();
            AssembleAwardDetails();
        }

        private async void AssembleActivityGameDetails()
        {
            activityGameDetails.Text = "";
            var activityGame = await gameSvc.GetActivityGameInfo();
            if (activityGame != null && activityGame.Data != null)
            {
                activityGameDetails.Text += $"Activity Name:{activityGame.Data.Name}" + Environment.NewLine;
                activityGameDetails.Text += $"Package Zip:{activityGame.Data.MaterialPacketUrl}" + Environment.NewLine;
                activityGameDetails.Text += $"AuthorizationType:{activityGame.Data.ActivityAuthorizationType}" + Environment.NewLine;
            }
        }

        private async void AssembleGameDetails()
        {
            gameInfoDetails.Text = "";
            var gameData = await gameSvc.GetGameInfo();
            if (gameData != null && gameData.Data != null)
            {
                gameInfoDetails.Text += $"Game Name:{gameData.Data.Name}" + Environment.NewLine;
                gameInfoDetails.Text += $"Game Code:{gameData.Data.Code}" + Environment.NewLine;
                gameInfoDetails.Text += $"GameType:{gameData.Data.GameType}" + Environment.NewLine;
                gameInfoDetails.Text += $"EnvType:{gameData.Data.EnvType}" + Environment.NewLine;
                //gameInfoDetails.Text += $"Owner:{gameData.Data.Owner}" + Environment.NewLine;
            }
        }


        private async void AssembleActivityDetails()
        {
            activityDetails.Text = "";
            var activityInfo = await gameSvc.GetActivityInfo();
            if (activityInfo != null && activityInfo.Data != null)
            {
                activityDetails.Text += $"Activity Name:{activityInfo.Data.Name}" + Environment.NewLine;
                activityDetails.Text += $"Enable WhiteList:{activityInfo.Data.IsEnableWhiteUser}" + Environment.NewLine;
            }

            var users = await gameSvc.GetUsersByActivitiy(50);
            if (users != null && users.Data != null)
            {
                activityDetails.Text += $"Users:{users.Data.Count}" + Environment.NewLine;
            }

            var rankUser = await gameSvc.GetRankUsersByActivity("Score", 20);
            if (rankUser != null && rankUser.Data != null)
            {
                activityDetails.Text += $"RankUsers Count:{rankUser.Data.Count}" + Environment.NewLine;
            }
            var whiteUsers = await gameSvc.GetActivityWhiteListUsers();
            if (whiteUsers != null && whiteUsers.Data != null)
            {
                activityDetails.Text += $"WhiteUsers Count:{whiteUsers.Data.Count}" + Environment.NewLine;
            }
        }


        private async void AssembleAwardDetails()
        {
            awardDetails.Text = "";
            var awards = await gameSvc.GetAwardsByActivity();

            if (awards != null && awards.Data != null)
            {
                if (awards.Data.Count > 0)
                {
                    awardInfo = awards.Data[0];
                }
                foreach (var award in awards.Data)
                {
                    awardDetails.Text += $"Award:{award.Name}--AwardId:{award.Id}----{award.AwardProduct}" + Environment.NewLine;
                }
            }
        }

        #endregion

        private async void TaoUploadBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = await gameSvc.PostActionToTao4Qrcode(System.IO.Path.Combine(Environment.CurrentDirectory, "player.png"),"TestGame", 10, ActionStatus.Register);
        }
    }
}
