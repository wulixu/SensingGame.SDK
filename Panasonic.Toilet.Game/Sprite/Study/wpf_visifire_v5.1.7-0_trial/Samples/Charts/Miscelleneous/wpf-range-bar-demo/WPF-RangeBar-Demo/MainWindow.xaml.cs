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
using Visifire.Controls;

namespace WPF_RangeBar_Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MyRangeControl.RangeChanged += new EventHandler<RangeBarEventArgs>(MyRangeControl_RangeChanged);
        }

        void MyRangeControl_RangeChanged(object sender, RangeBarEventArgs e)
        {
            RangeBar rangeBar = sender as RangeBar;

            if (e.IsFromValueChanged)
                FromTextBlock.Text = e.FromValue.ToString();
                
            if (e.IsToValueChanged)
                ToTextBlock.Text = e.ToValue.ToString();
        }

        void RangeChart_Loaded(object sender, RoutedEventArgs e)
        {
            AddDataPointsInRangeChart(sender as Chart);
        }

        public void AddDataPointsInRangeChart(Chart chart)
        {   
            DataPointCollection collection = new DataPointCollection();

            DateTime dateTime = DateTime.Now;

            for (Int32 index = 0; index < 1000; index++)
            {
                dateTime = dateTime.AddSeconds(index);
                collection.Add(new DataPoint() { XValue = dateTime, YValue = rand.Next(50, 100) });
            }

            chart.Series[0].DataPoints = collection;
        }

        Random rand = new Random(DateTime.Now.Millisecond);
    }
}