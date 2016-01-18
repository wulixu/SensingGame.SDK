using System;
using System.Collections.Generic;
using System.IO;
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
using SensingBase.Utils;

namespace VideoElement.SideBar
{
    /// <summary>
    /// ForegroundList.xaml 的交互逻辑
    /// </summary>
    public partial class ForegroundList : Grid
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Action ShowMenu;
        public Action<BitmapImage> ForegoundSelected;

        public ForegroundList()
        {
            InitializeComponent();
            backButton.OnClick += rtnButton_Click;
        }

        private void rtnButton_Click(object sender, RoutedEventArgs e)
        {
            if (ShowMenu != null)
            {
                ShowMenu.Invoke();
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        public void LoadForegoundImages(string path)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            var pics = Directory.GetFiles(path).Where(f => f.EndsWith(".png") || f.EndsWith("jpg")).ToArray();
            BitmapImage[] images = new BitmapImage[pics.Count()];

            for (int i=0;i<images.Length;i++)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(System.IO.Path.Combine(basePath, pics[i]));
                bitmap.EndInit();
                images[i] = bitmap;
            }
            list.ItemsSource = images;

        }

        public void Next()
        {
            if (list.Items.Count > 0)
            {
                int selectedIndex = list.SelectedIndex;
                selectedIndex++;
                if (selectedIndex == list.Items.Count)
                {
                    selectedIndex = 0;
                }
                list.SelectedIndex = selectedIndex;
                
            }
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (list.SelectedIndex != -1)
            {
                BitmapImage bitmap = list.SelectedItem as BitmapImage;
                log.Debug(bitmap.UriSource);
                if (ForegoundSelected != null)
                {
                    ForegoundSelected.Invoke(bitmap);
                } 
                
            }
        }

        


    }
}
