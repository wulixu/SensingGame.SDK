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

namespace TronCell.Game.Screen
{
    /// <summary>
    /// Interaction logic for ScoreListControl.xaml
    /// </summary>
    public partial class ScoreListControl : UserControl
    {
        public static DependencyProperty ListTypeProperty = DependencyProperty.Register("ListType", 
            typeof(ListType), typeof(ScoreListControl), new PropertyMetadata(OnScoreListTypeChanged));

        public ListType ListType
        {
            get { return (ListType)GetValue(ListTypeProperty); }
            set
            {
                SetValue(ListTypeProperty, value);
            }

        }

        private static void OnScoreListTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ListType listType = (ListType)e.NewValue;
            ScoreListControl self = d as ScoreListControl;
            if(listType == ListType.Score)
            {
               DataTemplate datatemplate = self.FindResource("ScoreItemTemplate") as DataTemplate;
                self.list.ItemTemplate = datatemplate;
            }
            else
            {
                DataTemplate datatemplate = self.FindResource("RankItemTemplate") as DataTemplate;
                self.list.ItemTemplate = datatemplate;
            }
            
        }

        public static DependencyProperty HeadIconProperty = DependencyProperty.Register("HeadIcon",
            typeof(ImageSource), typeof(ScoreListControl));


        public ImageSource HeadIcon
        {
            get { return (ImageSource)GetValue(HeadIconProperty); }
            set
            {
                SetValue(HeadIconProperty, value);
            }

        }



        public ScoreListControl()
        {
            InitializeComponent();
        }

    }

    public enum ListType
    {
        Rank,
        Score
    }

}
