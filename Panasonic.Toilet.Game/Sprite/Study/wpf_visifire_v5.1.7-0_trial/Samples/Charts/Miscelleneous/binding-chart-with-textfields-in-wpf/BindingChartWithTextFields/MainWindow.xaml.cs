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
using System.ComponentModel;
using Visifire.Charts;

namespace BindingChartWithTextFields
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LayoutRoot.DataContext = bindValues;
            SetBinding();
        }

        BindValues bindValues = new BindValues();
        private void SetBinding()
        {
            var yValue = new Binding { Path = new PropertyPath("YValue"), Source = bindValues };
            (MyChart.Series[0].DataPoints[0] as DataPoint).SetBinding(DataPoint.YValueProperty, yValue);

            Binding axisLabelsAngle = new Binding { Path = new PropertyPath("Angle"), Source = bindValues };
            MyChart.AxesX[0].AxisLabels.SetBinding(AxisLabels.AngleProperty, axisLabelsAngle);

            var titleFontSize = new Binding { Path = new PropertyPath("FontSize"), Source = bindValues };
            MyChart.Titles[0].SetBinding(Visifire.Charts.Title.FontSizeProperty, titleFontSize);
        }
    }


    public class BindValues : INotifyPropertyChanged
    {
        public Double YValue
        {
            get
            {
                return _yValue;
            }
            set
            {
                _yValue = value;
                FirePropertyChanged("YValue");
            }
        }
        public Double Angle
        {
            get
            {
                return _angle;
            }
            set
            {
                _angle = value;
                FirePropertyChanged("Angle");
            }
        }
        public Double FontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                _fontSize = value;
                FirePropertyChanged("FontSize");
            }
        }
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        public void FirePropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
        private Double _angle;
        private Double _fontSize = 10;
        private Double _yValue;
    }
}