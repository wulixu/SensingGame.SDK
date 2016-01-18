
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

namespace TronCell.Game.Screen
{
    /// <summary>
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : UserControl
    {
        // Register the routed event

        // Register the routed event
        public static readonly RoutedEvent NextScreenedEvent =
            EventManager.RegisterRoutedEvent("NextScreened", RoutingStrategy.Direct,
            typeof(RoutedEventHandler), typeof(WelcomePage));

        // .NET wrapper
        public event RoutedEventHandler NextScreened
        {
            add { AddHandler(NextScreenedEvent, value); }
            remove { RemoveHandler(NextScreenedEvent, value); }
        }

        public WelcomePage():base()
        {
            InitializeComponent();
            //this.KeyDown += WelcomePage_KeyDown;
          
            
        }

        void WelcomePage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ImageButton_OnOnClick(null, null);
            }
        }

        public void ImageButton_OnOnClick(object sender, EventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(NextScreenedEvent));
        }
    }
}
