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

namespace LiveUpdate
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            // Create a Visifire Chart
            CreateChart();
        }

        /// <summary>
        /// Function to create a chart
        /// </summary>
        public void CreateChart()
        {
            // Create a new instance of Chart
            chart = new Chart();

            // Set the chart width and height
            chart.Width = 500;
            chart.Height = 300;

            // Create a new instance of Title
            Title title = new Title();

            // Set title property
            title.Text = "Visifire Sample With LiveUpdate";

            // Add title to Titles collection
            chart.Titles.Add(title);

            // Create a new instance of DataSeries
            DataSeries dataSeries = new DataSeries();
 
            // Set DataSeries property
            dataSeries.RenderAs = RenderAs.Column;

            // Create a DataPoint
            DataPoint dataPoint;

            for (int i = 0; i < 5; i++)
            {
                // Create a new instance of DataPoint
                dataPoint = new DataPoint(); 

                // Set YValue for a DataPoint
                dataPoint.YValue = rand.Next(-100, 100); 
                
                // Add dataPoint to DataPoints collection
                dataSeries.DataPoints.Add(dataPoint); 
            }

            // Add dataSeries to Series collection.
            chart.Series.Add(dataSeries);

            // Attach a Loaded event to chart in order to attach a timer's Tick event
            chart.Loaded += new RoutedEventHandler(chart_Loaded);

            // Add chart to Chart Grid
            ChartGrid.Children.Add(chart);
        }

        /// <summary>
        /// Event handler for loaded event of chart
        /// </summary>
        /// <param name="sender">Chart</param>
        /// <param name="e">RoutedEventArgs</param>
        void chart_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1500);
        }

        /// <summary>
        /// Event handler for Tick event of Timer
        /// </summary>
        /// <param name="sender"> System.Windows.Threading.DispatcherTimer</param>
        /// <param name="e">EventArgs</param>
        void timer_Tick(object sender, EventArgs e)
        {
            for (Int32 i = 0; i < 5; i++)
            {
                // Update YValue property of the DataPoint
                chart.Series[0].DataPoints[i].YValue = rand.Next(-80, 100); // Changing the dataPoint YValue at runtime
            }
        }

        /// <summary>
        /// Event handler for Click event of UpdateButton
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">RoutedEventArgs</param>
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // timer starts
            timer.Start(); 
        }

        /// <summary>
        /// Event handler for Click event of UpdateStopButton
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">RoutedEventArgs</param>
        private void UpdateStopButton_Click(object sender, RoutedEventArgs e)
        {
            // timer stops
            timer.Stop(); 
        }

        Chart chart;                                            // Chart object
        Random rand = new Random(DateTime.Now.Millisecond);     // Create a random class variable
        System.Windows.Threading.DispatcherTimer timer = new    // Create a new instance of timer object
            System.Windows.Threading.DispatcherTimer();
    }
}
