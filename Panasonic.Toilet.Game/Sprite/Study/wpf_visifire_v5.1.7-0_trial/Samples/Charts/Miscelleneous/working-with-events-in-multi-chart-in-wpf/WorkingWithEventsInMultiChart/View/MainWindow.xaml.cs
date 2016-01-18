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

namespace WorkingWithEventsInMultiChart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
            //Attach even to Multi-Series Chart
            MutiChart1.OnDataPointMouseLeftButtonUp += new EventHandler<MouseButtonEventArgs>(MutiChart1_OnDataPointMouseLeftButtonUp);
        }

        void MutiChart1_OnDataPointMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("AxisXLabel : " + (sender as DataPoint).AxisXLabel.ToString() +"\n"+ "YValue : " + (sender as DataPoint).YValue.ToString());
        }
    }
}
