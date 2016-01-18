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
using Visifire.Charts;

namespace BulletChart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateChart();
        }

        private void CreateChart()
        {
            // Create Chart 
            Chart chart = new Chart();

            // Set Chart size
            chart.Width = 500;
            chart.Height = 110;
            chart.DataPointWidth = 25;

            // Create Chart Title
            Title title = new Title();
            title.Text = "2005 YTD";
            title.Margin = new Thickness(0, 10, 0, 0);
            chart.Titles.Add(title);

            // Create AxisX for Chart
            Axis axis1 = new Axis();
            axis1.AxisLabels = new AxisLabels();
            axis1.AxisLabels.Enabled = false;
            chart.AxesX.Add(axis1);

            // Create AxisY for Chart
            Axis axis2 = new Axis();
            axis2.AxisMaximum = 300;
            axis2.Interval = 50;
            chart.AxesY.Add(axis2);

            // Create Qualitatative range1 
            TrendLine trendLine1 = new TrendLine();
            trendLine1.StartValue = 0;
            trendLine1.EndValue = 150;
            trendLine1.LineColor = new SolidColorBrush(Colors.Red);
            trendLine1.Orientation = Orientation.Vertical;

            // Create Qualitatative range2
            TrendLine trendLine2 = new TrendLine();
            trendLine2.StartValue = 150;
            trendLine2.EndValue = 230;
            trendLine2.LineColor = new SolidColorBrush(Colors.Yellow);
            trendLine2.Orientation = Orientation.Vertical;

            // Create Qualitatative range3
            TrendLine trendLine3 = new TrendLine();
            trendLine3.StartValue = 230;
            trendLine3.EndValue = 300;
            trendLine3.LineColor = new SolidColorBrush(Colors.Green);
            trendLine3.Orientation = Orientation.Vertical;

            // Create Text Label 
            TrendLine trendLine4 = new TrendLine();
            trendLine4.Value = 1;
            trendLine4.LabelText = "Revenue \nU.S. $(1,000s)";
            trendLine4.LabelFontColor = new SolidColorBrush(Colors.Black);
            trendLine4.Orientation = Orientation.Horizontal;

            // Create Symbol marker that encodes Comparative measure 
            TrendLine trendLine5 = new TrendLine();
            trendLine5.Value = 250;
            trendLine5.LineColor = new SolidColorBrush(Colors.Black);
            trendLine5.Orientation = Orientation.Vertical;

            // Add TrendLine to Chart
            chart.TrendLines.Add(trendLine1);
            chart.TrendLines.Add(trendLine2);
            chart.TrendLines.Add(trendLine3);
            chart.TrendLines.Add(trendLine4);
            chart.TrendLines.Add(trendLine5);

            // Create Bar that encodes the Performance measure  
            DataSeries dataSeries = new DataSeries();
            dataSeries.RenderAs = RenderAs.Bar;

            // Create a DataPoint
            DataPoint dataPoint = new DataPoint();
            dataPoint.Color = new SolidColorBrush(Colors.Black);

            // Set the YValue 
            dataPoint.YValue = 280;

            // Add DataPoint to DataSeries
            dataSeries.DataPoints.Add(dataPoint);

            // Add DataSeries to Chart
            chart.Series.Add(dataSeries);

            // Add chart to the LayoutRoot for display
            LayoutRoot.Children.Add(chart);
        }
    }
}