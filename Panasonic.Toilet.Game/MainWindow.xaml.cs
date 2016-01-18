
using System;
using System.ComponentModel;
using System.Windows;

namespace TronCell.Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            this.Content = new GameControl();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Environment.Exit(0);
        }


    }


}
