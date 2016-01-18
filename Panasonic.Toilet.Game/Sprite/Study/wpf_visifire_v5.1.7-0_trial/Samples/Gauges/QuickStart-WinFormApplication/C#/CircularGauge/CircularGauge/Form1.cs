using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Visifire.Gauges;

namespace CircularGauge
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

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
            elementHost1.Child =gauge;
        }
    }
}
