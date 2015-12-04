using SensngGame.ClientSDK;
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

namespace SensingGame.ClientTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GameServiceClient gameSvc = new GameServiceClient("williamwu");
            var data = await gameSvc.GetQrCode4LoginAsyn("123", "123", "clientUniqueId");
            var qrcode = data.Data;

            var user = await gameSvc.FindScanQrCodeUserAsyn("13");

            var postBack = await gameSvc.PostData4ScanAsyn("1", "2", "3", 5);
            var pData = postBack.Data;
            //var qrcode = data.Result;
            //data.Result
            //var qrCode = data.Data;
        }

        public async Task Doit()
        {

        }
    }
}
