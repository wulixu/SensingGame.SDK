using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Visifire.Gauges;
using System.Windows.Input;
using System.Windows;

namespace LiveUpdate
{
    public partial class Form1 : Form
    {
        public Form1()
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
            elementHost1.Child = gauge;
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
        
        Gauge gauge;                                            // Visifire gauge
        Random rand = new Random(DateTime.Now.Millisecond);     // Create a random class variable
        System.Windows.Threading.DispatcherTimer timer = new    // Create a timer object
            System.Windows.Threading.DispatcherTimer();

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            // timer starts
            timer.Start();
        }

        private void UpdateStopButton_Click(object sender, EventArgs e)
        {
            // timer stops
            timer.Stop();
        }
    }
}
