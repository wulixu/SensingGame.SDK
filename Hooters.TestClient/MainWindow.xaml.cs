using Hooters.ClientSDK;
using Hooters.ClientSDK.Contract;
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

namespace Hooters.TestClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        HooterServiceClient hooterService = null;

        DeviceInfo device = new DeviceInfo();

        public MainWindow()
        {
            InitializeComponent();
        }


        private async void CreteDeviceBtn_Click(object sender, RoutedEventArgs e)
        {
            if (device.Counters == null)
            {
                device.Counters = new List<CounterInfo>();
                device.Counters.Add(new CounterInfo { Name = Counter1Name.Text });
                device.Counters.Add(new CounterInfo { Name = Counter2Name.Text });
            }
            var result = await hooterService.CreateCountersByDevice(device);
            if (result != null)
            {
                var data = result.Data;

                MsgBlock.Text  += $"Status:{result.Status}, Message = {result.Message}, Data ={result.Data}" + Environment.NewLine;
                
            }
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            device.Name = DeviceName.Text;
            device.Mac = DeviceMac.Text;
            device.Type = TypeKey.Text;
            device.Status = "Running";
            hooterService = new HooterServiceClient(SubKey.Text);
        }

        private async void GetCounters_Click(object sender, RoutedEventArgs e)
        {
            var result = await hooterService.GetCountersByDevice(device.Mac);
            if (result != null)
            {

                var data = result.Data;
                MsgBlock.Text = $"Status:{result.Status}, Message = {result.Message}, Data ={result.Data}" + Environment.NewLine;

                MsgBlock.Text += $"Device Name:{data.Name}, Device Mac = {data.Mac}" + Environment.NewLine;

                if (data != null && data.Counters != null)
                {
                    device = data;
                    foreach (var c in data.Counters)
                    {
                        MsgBlock.Text += $"Counter Name:{c.Name}" + Environment.NewLine;
                    }
                }
            }
        }

        private void PostCountersData_Click(object sender, RoutedEventArgs e)
        {
            if (PostCountersData.Content.ToString() == "Stop Post Data")
            {
                PostCountersData.Content = "Post Counters Data(auto)";
                StopPostCounter();
            }
            else
            {
                StartPostCounters();
                PostCountersData.Content = "Stop Post Data";
            }
        }
        private void StopPostCounter()
        {
            if (timer != null)
            {
                timer.Stop();
            }
        }
        private void StartPostCounters()
        {
            if (timer == null)
            {
                timer = new DispatcherTimer(DispatcherPriority.Background);
                timer.Tick += Timer_Tick;
                timer.Interval = TimeSpan.FromSeconds(double.Parse(TimeTick.Text));
            }
            timer.Start();
        }
        private static bool isPosting = false;
        private async void Timer_Tick(object sender, EventArgs e)
        {
            if (isPosting) return;
            isPosting = true;
            foreach (var counter in device.Counters)
            {
                var timesLength = new Random().Next(50);
                List<CounterTimeInfo> times = new List<CounterTimeInfo>();
                for (int index = 0; index < timesLength; index++)
                {
                    var timeInfo = new CounterTimeInfo();
                    timeInfo.Increment = new Random().Next(100);
                    timeInfo.Total += timeInfo.Increment;
                    timeInfo.CollectingTime = DateTime.Now.AddDays(new Random().Next(5));
                    times.Add(timeInfo);
                }
                counter.Times = times;
            }
            var result = await hooterService.PostCountersByDevice(device);
            if(result != null)
            {
                MsgBlock.Text += $"Status:{result.Status}, Message = {result.Message}, Data ={result.Data}" + Environment.NewLine;
                isPosting = false;
            }
        }

        private DispatcherTimer timer;

        private async void PostHeatmap_Click(object sender, RoutedEventArgs e)
        {
            var result = await hooterService.PostHeatmapByDevice(device.Mac, System.IO.Path.Combine(Environment.CurrentDirectory, "heapmap.png"), DateTime.Now);
            if (result != null)
            {
                MsgBlock.Text += $"Status:{result.Status}, Message = {result.Message}, Data ={result.Data}";
            }
        }

        private void MultiData_Click(object sender, RoutedEventArgs e)
        {

        }

        public void PostMultiCounters()
        {

        }

        private async void DeleteOne_Click(object sender, RoutedEventArgs e)
        {
            if(device.Counters.Count > 0)
            {
                device.Counters.RemoveAt(0);
            }
            var result = await hooterService.CreateCountersByDevice(device);
        }

        private async void GroupInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = await hooterService.GetGroupInfoAsync();
        }

        private DispatcherTimer uploadSales_timer;

        private async void UploadSalesDataBtn_Click(object sender, RoutedEventArgs e)
        {
            var datas = new List<OrderInfo>();
            for (int index = 0; index < 10; index++)
            {
                datas.Add(new OrderInfo { CollectingTime = DateTime.Now, Total = 500, Details = "鸡蛋,2,59", BuyerAge = 15, BuyerGender = "Female" });
            }
            var result = await hooterService.PostSalesData(datas);
        }
    }
}
