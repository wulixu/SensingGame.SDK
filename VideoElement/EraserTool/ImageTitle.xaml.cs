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

namespace VideoElement.EraserTool
{
    /// <summary>
    /// Interaction logic for ImageTitle.xaml
    /// </summary>
    public partial class ImageTitle : UserControl
    {
        public ImageTitle()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        public ImageBrush ImgBrush
        {
            get { return (ImageBrush)GetValue(ImgBrushProperty); }
            set { SetValue(ImgBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImgBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImgBrushProperty =
            DependencyProperty.Register("ImgBrush", typeof(ImageBrush), typeof(ImageTitle), new PropertyMetadata(null));

        public string TitleName
        {
            get { return (string)GetValue(TitleNameProperty); }
            set { SetValue(TitleNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleNameProperty =
            DependencyProperty.Register("TitleName", typeof(string), typeof(ImageTitle), new PropertyMetadata(string.Empty));


    }
}
