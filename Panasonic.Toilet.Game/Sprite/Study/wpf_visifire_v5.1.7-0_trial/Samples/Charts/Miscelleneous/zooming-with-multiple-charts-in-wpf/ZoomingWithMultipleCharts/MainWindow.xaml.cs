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

namespace ZoomingWithMultipleCharts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MyChart1.AxesX[0].OnZoom += new EventHandler<AxisZoomEventArgs>(MyChart1_OnZoom);
            MyChart2.AxesX[0].OnZoom += new EventHandler<AxisZoomEventArgs>(MyChart2_OnZoom);

            MyChart1.AxesX[0].Scroll += new EventHandler<AxisScrollEventArgs>(MyChart1_Scroll);
            MyChart2.AxesX[0].Scroll += new EventHandler<AxisScrollEventArgs>(MyChart2_Scroll);
        }

        void MyChart1_Scroll(object sender, AxisScrollEventArgs e)
        {
            Axis axis = sender as Axis;
            MyChart2.AxesX[0].ScrollBarOffset = axis.ScrollBarOffset;
        }

        void MyChart2_Scroll(object sender, AxisScrollEventArgs e)
        {
            Axis axis = sender as Axis;
            MyChart1.AxesX[0].ScrollBarOffset = axis.ScrollBarOffset;
        }

        void MyChart1_OnZoom(object sender, AxisZoomEventArgs e)
        {
            Axis axis = sender as Axis;
            MyChart2.AxesX[0].Zoom(e.MinValue, e.MaxValue);
            MyChart2.AxesX[0].ScrollBarOffset = axis.ScrollBarOffset;
        }

        void MyChart2_OnZoom(object sender, AxisZoomEventArgs e)
        {
            Axis axis = sender as Axis;
            MyChart1.AxesX[0].Zoom(e.MinValue, e.MaxValue);
            MyChart1.AxesX[0].ScrollBarOffset = axis.ScrollBarOffset;
        }

        Random rand = new Random(DateTime.Now.Millisecond);
    }
}
