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

namespace DateTimeAxis
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            // Create a new DateTime object
            dt = new List<DateTime>();

            // Populate DateTime collection
            dt.Add(new DateTime(2008, 1, 2));
            dt.Add(new DateTime(2008, 2, 4));
            dt.Add(new DateTime(2008, 3, 2));
            dt.Add(new DateTime(2008, 4, 11));
            dt.Add(new DateTime(2008, 5, 1));
            dt.Add(new DateTime(2008, 6, 10));

            // Create a new Visifire Chart
            CreateChart();
        }

        /// <summary>
        /// Function to create a chart
        /// </summary>
        public void CreateChart()
        {
            // Create a new instance of Chart
            Chart chart = new Chart();

            // Set chart properties
            chart.ScrollingEnabled = false;
            chart.View3D = true;

            // Create a new instance of Title
            Title title = new Title();

            // Set title property
            title.Text = "Visifire DateTime Axis Chart";

            // Add title to Titles collection
            chart.Titles.Add(title);

            // Create a new Axis
            Axis axis = new Axis();

            // Set axis properties
            axis.IntervalType = IntervalTypes.Days;
            axis.Interval = 20;


            // Add axis to AxesX collection
            chart.AxesX.Add(axis);

            for (Int32 j = 0; j < 2; j++)
            {
                // Create a new instance of DataSeries
                DataSeries dataSeries = new DataSeries();

                // Set DataSeries properties
                dataSeries.RenderAs = RenderAs.Column;
                dataSeries.XValueType = ChartValueTypes.DateTime;

                // Create a DataPoint
                DataPoint dataPoint;

                for (int i = 0; i < 6; i++)
                {
                    // Create a new instance of DataPoint
                    dataPoint = new DataPoint();

                    // Set XValue for a DataPoint
                    dataPoint.XValue = dt[i];

                    // Set YValue for a DataPoint
                    dataPoint.YValue = rand.Next(10, 100);

                    // Add dataPoint to DataPoints collection
                    dataSeries.DataPoints.Add(dataPoint);
                }

                // Add dataSeries to Series collection.
                chart.Series.Add(dataSeries);
            }

            // Add chart to LayoutRoot
            LayoutRoot.Children.Add(chart);
        }

        /// <summary>
        /// Create a random class variable
        /// </summary>
        Random rand = new Random(DateTime.Now.Millisecond);
        List<DateTime> dt;
    }
}
