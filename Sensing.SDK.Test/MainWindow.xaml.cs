using Newtonsoft.Json;
using Sensing.SDK;
using Sensing.SDK.Contract;
using SensingSite.ClientSDK.Common;
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

namespace Sensing.SDK.Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SensingWebClient _sensingWebClient;
        
        public MainWindow()
        {
            InitializeComponent();
            this.ClientNoTB.Text = MacIPHelper.GetClientMac();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var subKey = SubKeyTB.Text;
            var mac = ClientNoTB.Text;
            var dAG = SecurityKey.Text;
            //subKey = "hahaha";
            //mac = "aa:bb:cc:dd:ee:f0";
            _sensingWebClient = new SensingWebClient(subKey, "123456", mac,dAG);
            CreateBtn.Background = Brushes.Green;
            tabControl.IsEnabled = true;
        }

        private async void UploadBehaviorDataBtn_Click(object sender, RoutedEventArgs e)
        {
            var action = ActionComboBox.SelectedValue.ToString();
            var category = ThingType.SelectedValue.ToString();
            var sku = ThingNoTB.Text;
            var name = ThingNameTB.Text;
            var increment = int.Parse(IncrementTB.Text);
            var records = new List<BehaviorRecord>();
            records.Add(new BehaviorRecord
            {
                Action = action,
                ThingId = sku,
                CollectionTime = DateTime.Now,
                CollectEndTime = DateTime.Now,
                Increment = increment,
                Category = category,
                Name = name,
                SoftwareName = SoftwareNameTB.Text,
                PageName = PageNameTB.Text
            });
            var result = await _sensingWebClient.PostBehaviorRecordsAsync(records);
            BMessage.Text += result + Environment.NewLine;
        }




        private async void GetThings_Click(object sender, RoutedEventArgs e)
        {
            ThingMsg.Text = "loading..." + Environment.NewLine;
            var data = await _sensingWebClient.GetProducts();
            var brand = await _sensingWebClient.GetBrands();
            if(data != null && data.Items != null)
            {
                ThingMsg.Text = "Thing Successfully" + Environment.NewLine;
                ThingMsg.Text += $"Total Count = {data.TotalCount}, Current Count {data.Items.Count()}" + Environment.NewLine;
            }
            else
            {
                ThingMsg.Text = "failed" + Environment.NewLine;
            }
        }

        private async void GetCategoriesBtn_Click(object sender, RoutedEventArgs e)
        {
            ThingMsg.Text = "loading..." + Environment.NewLine;
            var data = await _sensingWebClient.GetProductCategories();
            if (data != null)
            {
                ThingMsg.Text = "Categories Successfully" + Environment.NewLine;
                ThingMsg.Text += $"Total Count = {data.TotalCount}, Current Count {data.Items.Count()}" + Environment.NewLine;
            }
            else
            {
                ThingMsg.Text = "failed" + Environment.NewLine;
            }
        }

        private async void CouponBtn_Click(object sender, RoutedEventArgs e)
        {
            //todo:Zaric
            CouponMsg.Text = "loading..." + Environment.NewLine;
            var data = await _sensingWebClient.GetCoupons();
            if (data != null)
            {
                CouponMsg.Text = "Coupon Successfully" + Environment.NewLine;
                CouponMsg.Text += $"Total Count = {data.TotalCount}, Current Count {data.Items.Count()}" + Environment.NewLine;
            }
            else
            {
                CouponMsg.Text = "failed" + Environment.NewLine;
            }
        }

        private async void GetAds_Click(object sender, RoutedEventArgs e)
        {
            var data = await _sensingWebClient.GetAds();
            if (data != null)
            {
                AdsMessage.Text = "Ads Successfully" + Environment.NewLine;
                AdsMessage.Text += $"Total Count = {data.TotalCount}, Current Count {data.Items.Count()}" + Environment.NewLine;
            }
            else
            {
                AdsMessage.Text = "failed" + Environment.NewLine;
            }
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void LikesBtn_Click(object sender, RoutedEventArgs e)
        {
            var data = await _sensingWebClient.GetLikeInfos();
            if (data != null)
            {
                LikeMsg.Text = "Likes Successfully" + Environment.NewLine;
                LikeMsg.Text += $"Total Count = {data.TotalCount}, Current Count {data.Items.Count()}" + Environment.NewLine;
            }
            else
            {
                LikeMsg.Text = "failed" + Environment.NewLine;
            }
        }

        private async void MatchBtn_Click(object sender, RoutedEventArgs e)
        {
            var data = await _sensingWebClient.GetMatchInfos();
            if (data != null)
            {
                MatchMsg.Text = "Match Successfully" + Environment.NewLine;
                MatchMsg.Text += $"Total Count = {data.TotalCount}, Current Count {data.Items.Count()}" + Environment.NewLine;
            }
            else
            {
                MatchMsg.Text = "failed" + Environment.NewLine;
            }
        }

        private async void GoodsByFacesBtn_Click(object sender, RoutedEventArgs e)
        {
            var faces = new FacesRecommendsInput();
            var face = new FaceImage();
            var fileName = genderCBox.SelectedIndex == 0 ? "face-m.jpg" : "face-f.jpg";
            var faceImagePath = System.IO.Path.Combine(Environment.CurrentDirectory, fileName);

            var bytes = File.ReadAllBytes(faceImagePath);
            face.Image = bytes;
            face.Type = "head";
            faces.Faces = new FaceImage[] { face };
            var data = await _sensingWebClient.GetRecommendsByFaces(faces);
            if (data != null)
            {
                MatchMsg.Text = "GoodsByFaces Successfully" + Environment.NewLine;
                MatchMsg.Text += data;
            }
            else
            {
                MatchMsg.Text = "failed" + Environment.NewLine;
            }
        }

        private async void DeviceStatus_Click(object sender, RoutedEventArgs e)
        {
            var records = new List<DeviceStatusInput>();
            records.Add(new DeviceStatusInput
            {
                StartTime = DateTime.Now.Subtract(TimeSpan.FromMinutes(5)),
                EndTime = DateTime.Now,
                Memory = 0.5,
                Cpu = 0.5
            });
            var result = await _sensingWebClient.PostDeviceStatusRecordAsync(records);
            BMessage.Text += result + Environment.NewLine;
        }

        private async void DeviceInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            var data = await _sensingWebClient.GetDeviceInfo();
            if (data != null)
            {
                BMessage.Text = "Match Successfully" + Environment.NewLine;
                BMessage.Text += JsonConvert.SerializeObject(data);
            }
            else
            {
                BMessage.Text = "failed" + Environment.NewLine;
            }
        }

        private async void MetaphysicsList_Click(object sender, RoutedEventArgs e)
        {
            var data = await _sensingWebClient.GetMetaphysicsList();
            if (data != null)
            {
                MetaphysicsStatusMessage.Text = $"Metaphysics Successfully with count = {data.Items.Count}" + Environment.NewLine;
            }
            else
            {
                MetaphysicsStatusMessage.Text = "failed" + Environment.NewLine;
            }
        }

        private async void DateMetaphysicsList_Click(object sender, RoutedEventArgs e)
        {
            var data = await _sensingWebClient.GetDateMetaPhysics(DateTime.Now, DateTime.Now.AddDays(30));
            if (data != null)
            {
                MetaphysicsStatusMessage.Text = $"GetDateMetaPhysics Successfully with count = {data.Items.Count}" + Environment.NewLine;
            }
            else
            {
                MetaphysicsStatusMessage.Text = "failed" + Environment.NewLine;
            }
        }

        private async void MemberFaceBtn_Click(object sender, RoutedEventArgs e)
        {
            var faces = new FaceInput();
            var fileName = genderCBox.SelectedIndex == 0 ? "face-m.jpg" : "face-f.jpg";
            var faceImagePath = System.IO.Path.Combine(Environment.CurrentDirectory, fileName);

            var bytes = File.ReadAllBytes(faceImagePath);
            faces.Face = bytes;
            var data = await _sensingWebClient.IsFaceMember(faces);
            if (data != null)
            {
                MatchMsg.Text = "IsFaceMember Successfully" + Environment.NewLine;
                MatchMsg.Text += data;
            }
            else
            {
                MatchMsg.Text = "failed" + Environment.NewLine;
            }
        }

        #region Activity

        private void ScannedAvator_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateQrcode_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ActivityWinner_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GetRankUsers_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateWinUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StartActivity_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StopAcitivy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PostDataByUserClick(object sender, RoutedEventArgs e)
        {

        }

        private void TaoUploadBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        private async void GetDeviceActivityGameInfos_Click(object sender, RoutedEventArgs e)
        {
            var deviceActivityGame = await _sensingWebClient.GetDeviceActivityGameInfoAsync();
            if (deviceActivityGame != null)
            {
                if (deviceActivityGame.Activity != null)
                {
                    activityDetails.Text = $"Activity Name:{deviceActivityGame.Activity.Name}" + Environment.NewLine;
                    activityDetails.Text += $"Wechat Public Id:{deviceActivityGame.Activity.WeChatAppID}" + Environment.NewLine;
                    activityDetails.Text += $"Taobao Seller Id:{deviceActivityGame.Activity.TaobaoSellerID}" + Environment.NewLine;
                    activityDetails.Text += $"Enable Special User:{deviceActivityGame.Activity.IsEnableWhiteUser}" + Environment.NewLine;
                }

                //game details
                if (deviceActivityGame.Software != null)
                {
                    gameInfoDetails.Text = $"Game Name:{deviceActivityGame.Software.Name}" + Environment.NewLine;
                    gameInfoDetails.Text += $"Game Language:{deviceActivityGame.Software.Language}" + Environment.NewLine;
                    gameInfoDetails.Text += $"Game EnvType:{deviceActivityGame.Software.EnvType}" + Environment.NewLine;
                    gameInfoDetails.Text += $"Game Version:{deviceActivityGame.Software.VersionNumber}" + Environment.NewLine;
                }

                //game details
                //gameInfoDetails.Text = $"ActivityGame Name:{deviceActivityGame.Name}" + Environment.NewLine;
                activityGameDetails.Text += $"WeChatAuthorizationType:{deviceActivityGame.WeChatAuthorizationType.ToString()}" + Environment.NewLine;
                activityGameDetails.Text += $"TaobaoAuthorizationType:{deviceActivityGame.TaobaoAuthorizationType}" + Environment.NewLine;
                activityGameDetails.Text += $"DeviceId:{deviceActivityGame.DeviceId}" + Environment.NewLine;
                activityGameDetails.Text += $"DeviceName:{deviceActivityGame.DeviceId}" + Environment.NewLine;
            }

            var awards = await _sensingWebClient.GetAwardsAsync();
            if (awards != null)
            {
                awardDetails.Text += $"Awards Count :{awards.Count}" + Environment.NewLine;
                foreach (var award in awards)
                {
                    awardDetails.Text += $"Award:{award.Name}--AwardId:{award.Id}----{award.AwardProduct}" + Environment.NewLine;
                }
            }
        }
    }
}
