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

namespace WPF_Progressive_Rendering_Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel viewModel = new ViewModel();

            viewModel.FromValue = 17000;
            viewModel.ToValue = 19000;

            this.DataContext = viewModel;

            MyRangeControl.RangeChanged += new EventHandler<Visifire.Controls.RangeBarEventArgs>(MyRangeControl_RangeChanged);
        }

        void MyRangeControl_RangeChanged(object sender, Visifire.Controls.RangeBarEventArgs e)
        {
            (this.DataContext as ViewModel).RefreshPointsCollection();

            if (e.IsFromValueChanged)
                FromText.Text = "From: " + Math.Round((Double)e.FromValue, 2).ToString();

            if (e.IsToValueChanged)
                ToText.Text = "To: " + Math.Round((Double)e.ToValue, 2).ToString();

        }
    }
}
