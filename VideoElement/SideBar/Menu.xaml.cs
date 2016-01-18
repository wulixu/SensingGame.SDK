using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace VideoElement.SideBar
{
    /// <summary>
    /// MainMenu.xaml 的交互逻辑
    /// </summary>
    public partial class MainMenu : Grid
    {
        public Action ShowForegoundAction;
        public Action TakeShotAction;
        public Action SignNameAction;
        public Action DisplayPicturesAction;

        private DispatcherTimer autoCollapseTimer;
        public MainMenu()
        {
            InitializeComponent();
            autoCollapseTimer = new DispatcherTimer 
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            autoCollapseTimer.Tick += (sender, e) => 
            {
                this.Visibility = Visibility.Collapsed;
            };
        }

        public void Show()
        {
            this.Visibility = Visibility.Visible;
            autoCollapseTimer.Start();
        }

        public void Hide()
        {
            this.Visibility = Visibility.Collapsed;
            autoCollapseTimer.Stop();
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            autoCollapseTimer.Stop();
            autoCollapseTimer.Start();
        }

        Point p;
        bool isPressed = false;
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            p = e.GetPosition(this);
            isPressed = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            
            base.OnMouseUp(e);
            if (isPressed)
            {
                double offset = e.GetPosition(this).X - p.X;
                if (offset >= 20)
                {
                    this.Visibility = Visibility.Collapsed;
                }
                isPressed = false;
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
        }

        private void fgButton_Click(object sender, RoutedEventArgs e)
        {
            if (ShowForegoundAction != null)
            {
                ShowForegoundAction.Invoke();
            }
        }

        private void shotButton_Click(object sender, RoutedEventArgs e)
        {

            System.Media.SystemSounds.Hand.Play();
            if (TakeShotAction != null)
            {
                TakeShotAction.Invoke();
            }
        }

        private void signButton_Click(object sender, RoutedEventArgs e)
        {
            if (SignNameAction != null)
            {
                SignNameAction.Invoke();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DisplayPicturesAction != null)
            {
                DisplayPicturesAction.Invoke();
            }
        }


        public void SwitchToHome()
        {
            fgButton.Visibility = Visibility.Visible;
            shotButton.Visibility = Visibility.Visible;
            signButton.Visibility = Visibility.Visible;
            pvButton.Visibility = Visibility.Visible;
        }

        public void SwitchToPriview()
        {
            this.Visibility = Visibility.Collapsed;
            fgButton.Visibility = Visibility.Collapsed;
            shotButton.Visibility = Visibility.Collapsed;
            signButton.Visibility = Visibility.Collapsed;
            pvButton.Visibility = Visibility.Collapsed;
        }
    }
}
