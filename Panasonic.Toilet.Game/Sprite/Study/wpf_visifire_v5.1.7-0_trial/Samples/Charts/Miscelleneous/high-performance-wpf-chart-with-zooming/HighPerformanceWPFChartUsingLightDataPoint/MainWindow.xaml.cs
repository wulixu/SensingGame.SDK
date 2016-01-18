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
using Visifire.Commons;

namespace HighPerformanceWPFChartUsingLightDataPoint
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

        public void CreateChart()
        {
            _dt1 = DateTime.UtcNow;

            _chart = new Chart();
            _chart.ZoomingEnabled = true;
            _chart.Width = 800;
            _chart.Height = 400;

            Title title = new Title();
            title.Text = "Line Chart with " + _numberofDataPoints + " DataPoints";
            _chart.Titles.Add(title);

            Axis axisX = new Axis();
            axisX.ValueFormatString = "hh:mm:ss tt";
            axisX.AxisLabels = new AxisLabels();
            axisX.AxisLabels.Angle = -45;
            _chart.AxesX.Add(axisX);

            Axis axisY = new Axis();
            axisY.ViewportRangeEnabled = true;
            _chart.AxesY.Add(axisY);

            for (Int32 j = 0; j < 1; j++)
            {
                values = new List<KeyValuePair<DateTime, double>>();

                DataSeries dataSeries = new DataSeries();
                dataSeries.RenderAs = RenderAs.QuickLine;
                dataSeries.XValueType = ChartValueTypes.Time;

                _chart.Series.Add(dataSeries);

                DataPointCollection coll = new DataPointCollection();

                for (int i = 0; i < _numberofDataPoints; i++)
                {
                    coll.Add(CreateDataPoint());
                }

                dataSeries.DataPoints = coll;
            }

            _chart.Rendered += chart_Rendered;
            MyChartGrid.Children.Add(_chart);
        }

        void chart_Rendered(object sender, EventArgs e)
        {
            MsgBox.Text = "Total time taken to render the chart : ";
            DateTime dt2 = DateTime.UtcNow;
            ResultBox.Text = Math.Round((dt2 - _dt1).TotalSeconds, 2) + " seconds";
        }

        List<KeyValuePair<DateTime, Double>> values;

        private IDataPoint CreateDataPoint()
        {
            KeyValuePair<DateTime, Double> prevValue;

            if (values.Count > 0)
                prevValue = values[values.Count - 1];
            else
                prevValue = new KeyValuePair<DateTime, double>();

            DateTime date = values.Count > 0 ? ((DateTime)prevValue.Key).AddMilliseconds(1) : _startDate.AddMilliseconds(values.Count);

            // LightDataPoint is used as an alternative for DataPoint to create high performance charts.
            LightDataPoint dataPoint = new LightDataPoint();

            dataPoint.XValue = date;

            Double value;

            if (values.Count > 0)
                value = prevValue.Value;
            else
                value = 10;

            if (_rand.NextDouble() > 0.5)
                value = value + _rand.NextDouble();
            else
                value = value - _rand.NextDouble();

            dataPoint.YValue = value;

            values.Add(new KeyValuePair<DateTime, double>(date, value));

            return dataPoint;
        }

        Chart _chart;
        Random _rand = new Random();
        DateTime _startDate = DateTime.Now;
        Int32 _numberofDataPoints = 100000;
        DateTime _dt1;
    }
}
