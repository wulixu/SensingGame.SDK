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

namespace CircularGauge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Call function to create gauge
            CreateGauge();
        }

        private void CreateGauge()
        {
            // Create a gauge
            Gauge gauge = new Gauge();
            gauge.Width = 300;
            gauge.Height = 300;

            // Create a Needle Indicator
            NeedleIndicator indicator = new NeedleIndicator();
            indicator.Value = 20;

            // Add indicator to Indicators collection of gauge
            gauge.Indicators.Add(indicator);

            // Add gauge to the LayoutRoot for display
            LayoutRoot.Children.Add(gauge);
        }
    }
}