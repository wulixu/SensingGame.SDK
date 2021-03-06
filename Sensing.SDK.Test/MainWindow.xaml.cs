﻿using Sensing.SDK;
using Sensing.SDK.Contract;
using SensingSite.ClientSDK.Common;
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
            if(data != null && data.Data != null)
            {
                ThingMsg.Text = "Successfully" + Environment.NewLine;
                ThingMsg.Text += $"Thing Count {data.Data.Count()}" + Environment.NewLine;
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
                ThingMsg.Text = "Successfully" + Environment.NewLine;
                ThingMsg.Text += $"Categories Count {data.Count()}" + Environment.NewLine;
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
                CouponMsg.Text = "Successfully" + Environment.NewLine;
                CouponMsg.Text += $"Coupon Count {data.Data.Count()}" + Environment.NewLine;
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
                AdsMessage.Text = "Successfully" + Environment.NewLine;
                AdsMessage.Text += $"Ads Count {data.Data.Count()}" + Environment.NewLine;
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
                LikeMsg.Text = "Successfully" + Environment.NewLine;
                LikeMsg.Text += $"Likes Count {data.Data.Count()}" + Environment.NewLine;
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
                MatchMsg.Text = "Successfully" + Environment.NewLine;
                MatchMsg.Text += $"Matches Count {data.Data.Count()}" + Environment.NewLine;
            }
            else
            {
                MatchMsg.Text = "failed" + Environment.NewLine;
            }
        }
    }
}
