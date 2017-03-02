using Sensing.SDK;
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
            subKey = "12345";
            mac = "aa:bb:cc:dd:ee:f0";
            _sensingWebClient = new SensingWebClient(subKey, "123456", mac);
            CreateBtn.Background = Brushes.Green;
        }

        private async void UploadBehaviorDataBtn_Click(object sender, RoutedEventArgs e)
        {
            var action = ActionComboBox.SelectedValue.ToString();
            var sku = ThingNoTB.Text;
            var increment = int.Parse(IncrementTB.Text);
            var records = new List<BehaviorRecord>();
            records.Add(new BehaviorRecord { Action = action, Sku = sku, CollectTime = DateTime.Now, From = ClientNoTB.Text, Increment = increment });
            var result = await _sensingWebClient.PostBehaviorRecordsAsync(records);
            BMessage.Text += result + Environment.NewLine;
        }




        private async void GetThings_Click(object sender, RoutedEventArgs e)
        {
            ThingMsg.Text = "loading..." + Environment.NewLine;
            var data = await _sensingWebClient.GetThings();
            if(data != null)
            {
                ThingMsg.Text = "Successfully" + Environment.NewLine;
                ThingMsg.Text += $"Thing Count {data.Count()}" + Environment.NewLine;
            }
            else
            {
                ThingMsg.Text = "failed" + Environment.NewLine;
            }
        }

        private async void GetCategoriesBtn_Click(object sender, RoutedEventArgs e)
        {
            ThingMsg.Text = "loading..." + Environment.NewLine;
            var data = await _sensingWebClient.GetTCategories();
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

        private async void GetFinalThings_Click(object sender, RoutedEventArgs e)
        {
            ThingMsg.Text = "loading..." + Environment.NewLine;
            var data = await _sensingWebClient.GetFinalThings();
            if (data != null)
            {
                ThingMsg.Text = "Successfully" + Environment.NewLine;
                ThingMsg.Text += $"Thing Count {data.Count()}" + Environment.NewLine;
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
                ThingMsg.Text = "Successfully" + Environment.NewLine;
                ThingMsg.Text += $"Coupon Count {data.Count()}" + Environment.NewLine;
            }
            else
            {
                ThingMsg.Text = "failed" + Environment.NewLine;
            }
        }
    }
}
