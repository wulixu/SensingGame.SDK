using SesingStore.ClientSDK;
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

namespace SensingStore.ClientTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SensingStoreServiceClient storeService = null;
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            storeService = new SensingStoreServiceClient(SubKey.Text, DeviceMac.Text);

            var result = await storeService.GetDevice();
            if (result != null)
            {
                var data = result.Data;
                MsgBlock.Text += $"Status:{result.Status}, Message = {result.Message}, Data ={result.Data}" + Environment.NewLine;
            }
        }

        private async void CreteDeviceBtn_Click(object sender, RoutedEventArgs e)
        {
            storeService = new SensingStoreServiceClient(SubKey.Text, DeviceMac.Text);

            var result = await storeService.GetProducts();
            if (result != null)
            {
                var data = result.Data;
                MsgBlock.Text += $"Status:{result.Status}, Message = {result.Message}, Data ={result.Data}" + Environment.NewLine;
            }
        }
    }
}
