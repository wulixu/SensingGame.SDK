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
using System.Collections.ObjectModel;

namespace DataBindingInWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            values.Add(new Value() { Label = "Sony", YValue = 50 });
            values.Add(new Value() { Label = "Dell", YValue = 35 });
            values.Add(new Value() { Label = "HP", YValue = 27 });
            values.Add(new Value() { Label = "HCL", YValue = 17 });
            values.Add(new Value() { Label = "Toshiba", YValue = 16 });

            MyGrid.ItemsSource = values;
        }

        ObservableCollection<Value> values = new ObservableCollection<Value>();
    }
}
