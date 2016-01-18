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

namespace CustomizingToolTipFromManagedCode
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Attach MouseEnter and MouseMove events to the Series
            MyChart.Series[0].MouseEnter += new EventHandler<MouseEventArgs>(Page_MouseEnter);
            MyChart.Series[0].MouseMove += new EventHandler<MouseEventArgs>(MainPage_MouseMove);
        }

        //Definition for Image_Loaded() function
        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            _toolTipImage = sender as Image;
        }
        Image _toolTipImage;

        //MouseMove Event Handler
        void MainPage_MouseMove(object sender, MouseEventArgs e)
        {
            DataPoint dp = sender as DataPoint;
            UpdateDataPointToolTipImage(dp);
        }

        //MouseEnter Event Handler
        void Page_MouseEnter(object sender, MouseEventArgs e)
        {
            DataPoint dp = sender as DataPoint;
            UpdateDataPointToolTipImage(dp);
        }

        //Definition for UpdateDataPointToolTipImage() function
        private void UpdateDataPointToolTipImage(DataPoint dataPoint)
        {
            if (_toolTipImage != null)
            {
                Uri imageUri = new Uri(dataPoint.AxisXLabel + ".jpg", UriKind.RelativeOrAbsolute);
                ImageSource imgSource = new BitmapImage(imageUri);
                _toolTipImage.Stretch = Stretch.Fill;
                _toolTipImage.SetValue(Image.SourceProperty, imgSource);
            }
        }
    }
}
