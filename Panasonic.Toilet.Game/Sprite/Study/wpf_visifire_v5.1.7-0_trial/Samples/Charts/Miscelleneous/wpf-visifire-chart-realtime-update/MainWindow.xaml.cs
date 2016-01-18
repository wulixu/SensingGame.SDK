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
using System.Windows.Threading;

namespace RealTimeUpdate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SpeedController.ValueChanged += new EventHandler<ControllerEventArgs>(SpeedController_ValueChanged);
            _timer.Tick += new EventHandler(timer_Tick);

            DataPoints = new DataPointCollection();

            ChartControl.Series[0].DataPoints = DataPoints;

            GraphList.DataContext = this;
            GraphList.SelectionChanged += new SelectionChangedEventHandler(GraphList_SelectionChanged);
            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);

            RadioButtonRenderTechnique1.IsChecked = true;
            RenderTechnique1.MouseLeftButtonUp += new MouseButtonEventHandler(RenderTechnique1_MouseLeftButtonUp);
            RenderTechnique2.MouseLeftButtonUp += new MouseButtonEventHandler(RenderTechnique2_MouseLeftButtonUp);

            StartStopButton.Click += new RoutedEventHandler(StartStopButton_Click);
        }

        void StartStopButton_Click(object sender, RoutedEventArgs e)
        {
            if (_timer.IsEnabled)
                _timer.Stop();
            else
                _timer.Start();
        }

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                _frameCount++;

                DateTime now = DateTime.Now;
                if ((now - _currentDateTime).TotalSeconds >= 1)
                {
                    FrameRate.Text = _frameCount.ToString() + " fps";
                    _currentDateTime = now;
                    _frameCount = 0;
                }

            }));
        }

        public DataPointCollection DataPoints
        {
            get;
            set;
        }


        public RenderAs[] RenderAsList
        {
            get
            {
                return new RenderAs[] {
                        RenderAs.Column,
                        RenderAs.Line,
                        RenderAs.QuickLine,
                        RenderAs.Bar,
                        RenderAs.Area,
                        RenderAs.Bubble,
                        RenderAs.Point,
                        RenderAs.StepLine,
                        RenderAs.Spline
                };
            }
        }

        void GraphList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChartControl.Series[0].RenderAs = (RenderAs)RenderAsList[GraphList.SelectedIndex];

            if (ChartControl.Series[0].RenderAs == RenderAs.Bar)
            {
                ChartControl.AxesX[0].AxisLabels.Angle = 0;
            }
            else
            {
                ChartControl.AxesX[0].AxisLabels.Angle = -45;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if ((Boolean)RadioButtonRenderTechnique1.IsChecked)
            {
                if (DataPoints.Count > 100)
                    DataPoints.RemoveAt(0);

                TimeSpan time = DateTime.Now.TimeOfDay;
                String axisLabel = time.Hours.ToString() + ":" + time.Minutes.ToString() + ":" + time.Seconds.ToString() + ":" + time.Milliseconds.ToString("000");

                DataPoints.Add(new DataPoint() { AxisXLabel = axisLabel, YValue = _rand.Next(0, time.Minutes + time.Seconds + time.Milliseconds) });
            }
            else if ((Boolean)RadioButtonRenderTechnique2.IsChecked)
            {
                DataPointCollection dpc = new DataPointCollection();
                TimeSpan time;
                String axisLabel;

                for (Int32 index = 0; index < 100; index++)
                {
                    time = DateTime.Now.TimeOfDay;
                    axisLabel = time.Hours.ToString() + ":" + time.Minutes.ToString() + ":" + time.Seconds.ToString() + ":" + time.Milliseconds.ToString();

                    dpc.Add(new DataPoint() { AxisXLabel = axisLabel, YValue = _rand.Next(0, time.Minutes + time.Seconds + time.Milliseconds) });
                }

                ChartControl.Series[0].DataPoints = dpc;
            }
        }

        void SpeedController_ValueChanged(object sender, ControllerEventArgs e)
        {
            Double miliSec = (1 - e.Value) * 100;
            miliSec = Math.Round(miliSec, 1);

            _timer.Interval = new TimeSpan(0, 0, 0, 0, (Int32)miliSec);
        }


        void RenderTechnique1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RadioButtonRenderTechnique1.IsChecked = true;
        }

        void RenderTechnique2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RadioButtonRenderTechnique2.IsChecked = true;
        }

        private void RadioButtonRenderTechnique1_Checked(object sender, RoutedEventArgs e)
        {
            if ((Boolean)RadioButtonRenderTechnique1.IsChecked)
            {
                ChartControl.Series[0].DataPoints = DataPoints;
                Info.Text = "FIFO: This render technique adds one new DataPoint at the end of the collection and removes the first one from the beginning. This render technique called as FIFO (First in First out). It creates visual effect of a graph moving from right to left. This technique is used mostly used to simulate 'Streaming Process'.";
                RadioButtonRenderTechnique2.IsChecked = false;
            }
        }

        private void RadioButtonRenderTechnique2_Checked(object sender, RoutedEventArgs e)
        {
            if ((Boolean)RadioButtonRenderTechnique2.IsChecked)
            {
                RadioButtonRenderTechnique1.IsChecked = false;
                Info.Text = "Update DataSet: This render technique updates the dataset directly at one shot. It always creates a new points collection and directly set it as new dataset of graph.";
            }
        }

        Random _rand = new Random(DateTime.Now.Millisecond);
        DispatcherTimer _timer = new DispatcherTimer();
        DateTime _currentDateTime = DateTime.Now;
        Int32 _frameCount;
    }
}
