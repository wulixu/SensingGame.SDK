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

namespace TronCell.Game
{
    /// <summary>
    /// Interaction logic for Paddle.xaml
    /// </summary>
    public partial class Paddle : UserControl
    {
        public Paddle()
        {
            InitializeComponent();
        }

        public Rect CallisionRect
        {
            get { return new Rect(Canvas.GetLeft(this),Canvas.GetTop(this)+ 40,this.Width,this.Height/4); }
            //set { callisionRect = value; }
        }
    }
}
