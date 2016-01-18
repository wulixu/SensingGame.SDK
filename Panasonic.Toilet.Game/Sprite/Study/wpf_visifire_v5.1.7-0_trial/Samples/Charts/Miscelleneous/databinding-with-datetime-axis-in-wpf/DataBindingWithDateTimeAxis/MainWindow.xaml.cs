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

namespace DataBindingWithDateTimeAxis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime dt1 = new DateTime(2010, 8, 1);
        Random rn = new Random();

        public MainWindow()
        {
            InitializeComponent();

            // Populate collection of Values
            for (int i = 0; i < 1000; i++)
            {
                values.Add(new Value() { XValue = dt1, YValue = rn.Next(1, 50) });
                dt1 = dt1.AddMinutes(1);
            }

            // Setting values collection as source
            MyGrid.ItemsSource = values;
        }

        ObservableCollection<Value> values = new ObservableCollection<Value>();
    }
}
