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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TronCell.Game.Screen
{
    /// <summary>
    /// Interaction logic for GameOverScreen.xaml
    /// </summary>
    public partial class GameOverScreen : UserControl
    {
        public static readonly RoutedEvent NextScreenedEvent =
            EventManager.RegisterRoutedEvent("NextScreened", RoutingStrategy.Direct,
            typeof(RoutedEventHandler), typeof(GameOverScreen));

        // .NET wrapper
        public event RoutedEventHandler NextScreened
        {
            add { AddHandler(NextScreenedEvent, value); }
            remove { RemoveHandler(NextScreenedEvent, value); }
        }

        public GameOverScreen()
        {
            InitializeComponent();
            
        }

        private Timer timer = null;
        public void Start()
        {
            this.Visibility = Visibility.Visible;
            timer = new Timer(state =>
            {
                this.Dispatcher.BeginInvoke(new Action(() => RaiseEvent(new RoutedEventArgs(NextScreenedEvent))));
                timer.Dispose();
            },null,5000,30000);
        }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(NextScreenedEvent));
        }
    }
}
