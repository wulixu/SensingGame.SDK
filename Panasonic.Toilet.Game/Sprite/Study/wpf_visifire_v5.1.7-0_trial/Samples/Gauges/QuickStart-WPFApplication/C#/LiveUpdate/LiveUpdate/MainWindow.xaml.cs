using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Visifire.Gauges;

namespace LiveUpdate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Create a gauge
            CreateGauge();
        }

        /// <summary>
        /// Function to create a Visifire Gauge
        /// </summary>
        private void CreateGauge()
        {   
            // Create a gauge
            gauge = new Gauge();
            gauge.Width = 300;
            gauge.Height = 300;

            // Create a Needle Indicator
            NeedleIndicator indicator = new NeedleIndicator();
            indicator.Value = 20;

            // Add indicator to Indicators collection of gauge
            gauge.Indicators.Add(indicator);

            // Attach a Loaded event to gauge in order to attach a timer's Tick event
            gauge.Loaded += new RoutedEventHandler(gauge_Loaded);

            // Add gauge to the LayoutRoot for display
            GaugeGrid.Children.Add(gauge);
        }

        /// <summary>
        /// Event handler for loaded event of gauge
        /// </summary>
        /// <param name="sender">Gauge</param>
        /// <param name="e">RoutedEventArgs</param>
        void gauge_Loaded(object sender, RoutedEventArgs e)
        {   
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1500);
        }

        /// <summary>
        /// Event handler for Tick event of Dispatcher Timer
        /// </summary>
        /// <param name="sender">System.Windows.Threading.DispatcherTimer</param>
        /// <param name="e">EventArgs</param>
        void timer_Tick(object sender, EventArgs e)
        {   
            // Update Indicators Value property
            gauge.Indicators[0].Value = rand.Next(10, 80);
        }

        /// <summary>
        /// Event handler for Click event of Update Button
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">RoutedEventArgs</param>
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {   
            // timer starts
            timer.Start();
        }

        /// <summary>
        /// Event handler for Click event of UpdateStop Button
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">RoutedEventArgs</param>
        private void UpdateStopButton_Click(object sender, RoutedEventArgs e)
        {   
            // timer stops
            timer.Stop();
        }
        
        Gauge gauge;                                            // Visifire gauge
        Random rand = new Random(DateTime.Now.Millisecond);     // Create a random class variable
        System.Windows.Threading.DispatcherTimer timer = new    // Create a timer object
            System.Windows.Threading.DispatcherTimer();
    }
}