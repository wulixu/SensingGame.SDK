//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Visifire.Charts;
using Visifire.Commons;
using System.Windows.Markup;
using System.Xml;
using System.IO;

namespace AccessingEmbeddedColorSets
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ColorSets colorSets = LoadVisifireColorSets();
            ColorSet cs = colorSets.GetColorSetByName("VisiRed");
            CreateChart(cs);
        }

        private void CreateChart(ColorSet cs)
        {
            // Create Chart
            Chart chart = new Chart();

            // Set Chart size
            chart.Width = 500;
            chart.Height = 300;

            // Create DataSeries
            DataSeries ds = new DataSeries();
            for (Int32 i = 0; i < 5; i++)
            {
                // Create DataPoint
                DataPoint dp = new DataPoint();

                Brush color = cs.GetNewColorFromColorSet();

                dp.Color = color;

                // Set DataPoint YValue
                dp.YValue = i + 1;

                // Add DataPoint to DataPoints collection of Chart
                ds.DataPoints.Add(dp);
            }

            // Add DataSeries to Series collection of Chart
            chart.Series.Add(ds);

            // Add chart to LayoutRoot
            LayoutRoot.Children.Add(chart);
        }

        private ColorSets LoadVisifireColorSets()
        {
            ColorSets colorSets = null;

            // Resource location for embedded ColorSets
            string resourceName = "Visifire.Charts.ColorSets.xaml";
            using (System.IO.Stream s = typeof(Chart).Assembly.GetManifestResourceStream(resourceName))
            {
                if (s != null)
                {
                    System.IO.StreamReader reader = new System.IO.StreamReader(s);
                    String xaml = reader.ReadToEnd();

                    colorSets = XamlReader.Load(new XmlTextReader(new StringReader(xaml))) as ColorSets;

                    reader.Close();
                    s.Close();
                }
            }
            return colorSets;
        }
    }
}
