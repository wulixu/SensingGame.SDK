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

namespace ProductAndSales
{   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Logo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.visifire.com");
        }

        private void Logo_MouseEnter(object sender, MouseEventArgs e)
        {
            FrameworkElement fe = sender as FrameworkElement;
            fe.Opacity = 0.6;
        }

        private void Logo_MouseLeave(object sender, MouseEventArgs e)
        {
            FrameworkElement fe = sender as FrameworkElement;
            fe.Opacity = 1;
        }
    }
}
