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

namespace RealTimeUpdate
{
    /// <summary>
    /// Interaction logic for Controller.xaml
    /// </summary>
    public partial class Controller : UserControl
    {
        public Controller()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Controller_Loaded);
        }

        void Controller_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.MouseLeftButtonUp += new MouseButtonEventHandler(Controller_MouseLeftButtonUp);
                this.MouseMove += new MouseEventHandler(Controller_MouseMove);
                this.MouseLeftButtonDown += new MouseButtonEventHandler(Controller_MouseLeftButtonDown);
                this.LostMouseCapture += new MouseEventHandler(Controller_LostMouseCapture);
            }
            catch { }
        }

        void Controller_LostMouseCapture(object sender, MouseEventArgs e)
        {
            _dragStart = false;
        }

        void Controller_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragStart = true;

            (this.Parent as FrameworkElement).Cursor = Cursors.None;
            _startX = e.GetPosition(this).X;
            _startY = e.GetPosition(this).Y;

            this.CaptureMouse();
        }

        void Controller_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragStart)
            {
                Double height = (sender as FrameworkElement).ActualHeight;

                Double factor = 280 / 50;
                angle = (NobImage.RenderTransform as RotateTransform).Angle;
                Double X = e.GetPosition(this).X;
                Double Y = e.GetPosition(this).Y;

                Int32 direction = Y > _oldY ? -1 : 1;

                if (direction < 0)
                    angle -= factor;

                if (direction > 0)
                    angle += factor;

                if (angle > 280)
                    angle = 280;

                if (angle < 0)
                    angle = 0;

                (NobImage.RenderTransform as RotateTransform).Angle = angle;

                if (angle == 0)
                    Rect.Height = 0;
                else
                    Rect.Height = (angle / 280) * 35;

                _oldY = Y;

                if (ValueChanged != null)
                {
                    ValueChanged(this, new ControllerEventArgs() { Value = (angle / 280) });
                }
            }
        }

        public event EventHandler<ControllerEventArgs> ValueChanged;

        void Controller_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _dragStart = false;
            (this.Parent as FrameworkElement).Cursor = Cursors.Arrow;

            this.ReleaseMouseCapture();
        }

        Double angle = 0;

        Double _startX, _startY;
        Double _oldY;
        Boolean _dragStart = false;
    }

    public class ControllerEventArgs : EventArgs
    {
        public Double Value { get; set; }
    }

}