using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Visifire.Gauges;

namespace LinearGauge
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Call function to create gauge
            CreateGauge();
        }
        
        private void CreateGauge()
        {
            // Create a gauge
            Gauge gauge = new Gauge();
            gauge.Width = 100;
            gauge.Height = 300;

            // Set Type property in Gauge
            gauge.Type = GaugeTypes.Linear;

            // Create a Bar Indicator
            BarIndicator indicator = new BarIndicator();
            indicator.Value = 20;

            // Add indicator to Indicators collection of gauge
            gauge.Indicators.Add(indicator);

            // Add gauge to the LayoutRoot for display
            elementHost1.Child = gauge;
        }
    }
}
