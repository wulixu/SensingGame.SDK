using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TronCell.Game.Screen
{
    /// <summary>
    /// Interaction logic for BillboardScreen.xaml
    /// </summary>
    public partial class BillboardScreen : UserControl
    {
        public static readonly RoutedEvent NextScreenedEvent =
            EventManager.RegisterRoutedEvent("NextScreened", RoutingStrategy.Direct,
            typeof(RoutedEventHandler), typeof(BillboardScreen));

        // .NET wrapper
        public event RoutedEventHandler NextScreened
        {
            add { AddHandler(NextScreenedEvent, value); }
            remove { RemoveHandler(NextScreenedEvent, value); }
        }

        public BillboardScreen()
        {
            InitializeComponent();
        }

        public void SetHeadImage(string relatviePath)
        {
            var appRoot = AppDomain.CurrentDomain.BaseDirectory;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(appRoot + relatviePath, UriKind.Absolute);
            bitmap.EndInit();
            int width = (int)bitmap.Width; //force refresh 
            headImage.Source = bitmap;
        }

        private Timer timer = null;
        public void Start()
        {
            this.Visibility = Visibility.Visible;
            var storyboard = Resources["UpAnimation"] as Storyboard;
            if(storyboard != null)
            {
                storyboard.Begin();
            }
            timer = new Timer(state =>
            {
                this.Dispatcher.BeginInvoke(new Action(() => RaiseEvent(new RoutedEventArgs(NextScreenedEvent))));
                timer.Dispose();
            }, null, 5000, 30000);
        }
    }
}
