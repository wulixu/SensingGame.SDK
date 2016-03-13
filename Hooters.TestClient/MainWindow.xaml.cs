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
            device.Counters = new List<CounterInfo>();
            device.Counters.Add(new CounterInfo { Name = Counter1Name.Text });
            device.Counters.Add(new CounterInfo { Name = Counter2Name.Text });
            var result = await hooterService.CreateCountersByDevice(device);
            if (result != null)
            {
                MsgBlock.Text = $"Status:{result.Status}, Message = {result.Message}, Data ={result.Data}";
            }
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            device.Name = DeviceName.Text;
            device.Mac = DeviceMac.Text;
            hooterService = new HooterServiceClient(SubKey.Text);
        }

        private async void GetCounters_Click(object sender, RoutedEventArgs e)
        {
            var result = await hooterService.GetCountersByDevice(device.Mac);
            if (result != null && result.Status == HooterServiceClient.ApiOK)
            {
                MsgBlock.Text = $"Status:{result.Status}, Message = {result.Message}, Data ={result.Data}";
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
                counter.Increment = new Random().Next(100);
                counter.Total += counter.Increment;
                counter.CollectingTime = DateTime.Now;
            }
            var result = await hooterService.PostCountersByDevice(device);
            if(result != null)
            {
                MsgBlock.Text += $"Status:{result.Status}, Message = {result.Message}, Data ={result.Data}";
                isPosting = false;
            }
        }

        private DispatcherTimer timer;

        private async void PostHeatmap_Click(object sender, RoutedEventArgs e)
        {
            var result = await hooterService.PostHeatmapByDevice(device.Mac, System.IO.Path.Combine(Environment.CurrentDirectory, "heapmap.png"), System.IO.Path.Combine(Environment.CurrentDirectory, "camera.png"));
            if (result != null)
            {
                MsgBlock.Text += $"Status:{result.Status}, Message = {result.Message}, Data ={result.Data}";
            }
        }
    }
}
