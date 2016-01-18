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

namespace WorkingWithImagesInChart
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
            chart.Width = 600;
            chart.Height = 400;

            //// Create an ImageBrush
            ImageBrush brush = new ImageBrush();

            // Set image source
            brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/WorkingWithImagesInChart;component/chart_background.jpg", UriKind.RelativeOrAbsolute));

            // Set ImageBrush's Stretch property
            brush.Stretch = Stretch.Fill;

            // Set image brush to Chart Background
            chart.Background = brush;

            // Create DataSeries
            DataSeries ds = new DataSeries();
            ds.Bevel = false;
            ds.LabelEnabled = true;

            for (Int32 i = 0; i < 5; i++)
            {
                // Create DataPoint
                DataPoint dp = new DataPoint();

                dp.Color = GetBrush(i + 1);

                // Set DataPoint YValue
                if (i == 0)
                    dp.YValue = 150;
                else if (i == 1)
                    dp.YValue = 80;
                else if (i == 2)
                    dp.YValue = 100;
                else if (i == 3)
                    dp.YValue = 120;
                else if (i == 4)
                    dp.YValue = 122;

                // Add DataPoint to DataPoints collection of Chart
                ds.DataPoints.Add(dp);
            }

            // Add DataSeries to Series collection of Chart
            chart.Series.Add(ds);

            // Add chart to LayoutRoot
            LayoutRoot.Children.Add(chart);
        }


        public Brush GetBrush(Int32 index)
        {
            // Create an ImageBrush
            ImageBrush brush = new ImageBrush();

            // Set image source
            brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/WorkingWithImagesInChart;component/img" + index.ToString() + ".jpg", UriKind.RelativeOrAbsolute));

            // Set ImageBrush's Stretch property
            brush.Stretch = Stretch.Fill;

            return brush;
        }

    }
}
