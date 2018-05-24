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
            //subKey = "hahaha";
            //mac = "aa:bb:cc:dd:ee:f0";
            _sensingWebClient = new SensingWebClient(subKey, "123456", mac);
            CreateBtn.Background = Brushes.Green;
            tabControl.IsEnabled = true;
        }

        private async void UploadBehaviorDataBtn_Click(object sender, RoutedEventArgs e)
        {
            var action = ActionComboBox.SelectedValue.ToString();
            var category = ThingType.SelectedValue.ToString();
            var sku = ThingNoTB.Text;
            var increment = int.Parse(IncrementTB.Text);
            var records = new List<BehaviorRecord>();
            records.Add(new BehaviorRecord { Action = action,ThingId = sku, CollectTime = DateTime.Now, Increment = increment,Category = category });
            var result = await _sensingWebClient.PostBehaviorRecordsAsync(records);
            BMessage.Text += result + Environment.NewLine;
        }




        private async void GetThings_Click(object sender, RoutedEventArgs e)
        {
            ThingMsg.Text = "loading..." + Environment.NewLine;
            var data = await _sensingWebClient.GetProducts();
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
                MatchMsg.Text = "Match Successfully" + Environment.NewLine;
                MatchMsg.Text += data;
            }
            else
            {
                MatchMsg.Text = "failed" + Environment.NewLine;
            }
        }
    }
}
