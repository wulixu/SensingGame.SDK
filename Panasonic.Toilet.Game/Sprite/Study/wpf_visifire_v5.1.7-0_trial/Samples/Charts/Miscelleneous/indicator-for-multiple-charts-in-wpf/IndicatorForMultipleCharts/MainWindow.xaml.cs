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

namespace IndicatorForMultipleCharts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateChart(Chart1, RenderAs.Line, 1, false);
            CreateChart(Chart2, RenderAs.Column, 1, true);

            Chart1.PlotArea.MouseMove += new EventHandler<PlotAreaMouseEventArgs>(PlotArea_MouseMove);
            Chart1.PlotArea.MouseLeave += new EventHandler<MouseEventArgs>(PlotArea_MouseLeave);

            Chart2.PlotArea.MouseMove += new EventHandler<PlotAreaMouseEventArgs>(PlotArea2_MouseMove);
            Chart2.PlotArea.MouseLeave += new EventHandler<MouseEventArgs>(PlotArea2_MouseLeave);
        }

        /// <summary>
        /// MouseMove event handler, it will enabled ShowIndicator for Chart1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PlotArea2_MouseMove(object sender, PlotAreaMouseEventArgs e)
        {
            Chart1.ShowIndicator(e.XValue, e.YValue);
        }

        /// <summary>
        ///  MouseLeave  event handler, it will disabled ShowIndicator for Chart1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>           
        void PlotArea2_MouseLeave(object sender, MouseEventArgs e)
        {
            Chart1.HideIndicator();
        }

        /// <summary>
        /// MouseMove event handler, it will enabled ShowIndicator for Chart2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PlotArea_MouseMove(object sender, PlotAreaMouseEventArgs e)
        {
            Chart2.ShowIndicator(e.XValue, e.YValue);
        }

        /// <summary>
        /// MouseLeave event handler, it will disabled ShowIndicator for Chart2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PlotArea_MouseLeave(object sender, MouseEventArgs e)
        {
            Chart2.HideIndicator();
        }

        /// <summary>
        /// Function to create a chart
        /// </summary>
        public void CreateChart(Chart chart, RenderAs renderAs, Double opacity, Boolean isRondom)
        {
            chart.ScrollingEnabled = false;

            // Create a new instance of DataSeries
            DataSeries dataSeries1 = new DataSeries();
            dataSeries1.ToolTipText = "XValue=#XValue\nYValue=#YValue";
            dataSeries1.MarkerEnabled = false;

            if (renderAs == RenderAs.Line)
            {
                dataSeries1.ShadowEnabled = true;
                dataSeries1.LineThickness = 1;
            }
            else
            {
                dataSeries1.ShadowEnabled = true;
                dataSeries1.RadiusX = new CornerRadius(2);
                dataSeries1.RadiusY = new CornerRadius(2);
                dataSeries1.Bevel = true;
            }

            // Set DataSeries property
            dataSeries1.RenderAs = renderAs;
            dataSeries1.Opacity = opacity;
            dataSeries1.Bevel = false;

            // Create a DataPoint
            DataPoint dataPoint1;

            Double degree = 0;

            for (int i = 0; i < 30; i++)
            {
                // Create a new instance of DataPoint
                dataPoint1 = new DataPoint();

                // Set YValue for a DataPoint
                if (isRondom)
                {
                    dataPoint1.YValue = rand.Next(1, 100);
                }
                else
                {
                    if (i == 0)
                        dataPoint1.YValue = 0;
                    else
                        dataPoint1.YValue = (Int32)(100 * Math.Sin(degree % Math.PI));
                }

                degree += 0.06;

                if (i == 0 || i == 4 || i == 10 || i == 18 || i == 29 || i == 35 || i == 45 || i == 50)
                {
                    if (renderAs == RenderAs.Line)
                    {
                        dataPoint1.MarkerEnabled = true;
                        dataPoint1.MarkerSize = 8;
                        dataPoint1.MarkerColor = dataPoint1.Color;
                    }
                }


                // Add dataPoint to DataPoints collection
                dataSeries1.DataPoints.Add(dataPoint1);
            }

            // Add dataSeries to Series collection.
            chart.Series.Add(dataSeries1);
            chart.IndicatorEnabled = true;
        }

        Random rand = new Random(DateTime.Now.Millisecond);
    }
}
