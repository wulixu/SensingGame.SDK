using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using TronCell.Game;

namespace BrookStoneGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        GameControl game1;
        VideoElement.VideoControl game2;

        public MainWindow()
        {
            InitializeComponent();
           
            game1 = new GameControl();
            game2 = new VideoElement.VideoControl();
            
            root.Children.Add(game1);
            root.Children.Add(game2);
            game1.Pause();

        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            
            if(e.Key == Key.D1)
            {
                game2.Pause();
                game1.Resume();
                Panel.SetZIndex(game1, 3);
            }
            else if(e.Key == Key.D2)
            {
                game1.Pause();
                game2.Resume();
                Panel.SetZIndex(game1, 0);
            }

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Environment.Exit(0);
        }
    }
}
