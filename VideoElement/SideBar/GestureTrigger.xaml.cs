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

namespace VideoElement.SideBar
{
    /// <summary>
    /// GestureTrigger.xaml 的交互逻辑
    /// </summary>
    public partial class GestureTrigger : Canvas
    {
        public Action ShowMenu;


        public GestureTrigger()
        {
            InitializeComponent();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (ShowMenu != null)
            {
                ShowMenu.Invoke();
            }
        }
    }
}
