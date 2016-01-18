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

namespace VideoElement
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class OutputTemplate : Canvas
    {
        private DrawingImage drawingImage;
        private DrawingGroup drawingGroup;
        public OutputTemplate()
        {
            InitializeComponent();
            drawingGroup = new DrawingGroup();
            drawingImage = new DrawingImage(drawingGroup);
            this.DataContext = this;
          
        }

        public ImageSource ImageSource
        {
            get{
                return drawingImage;
            }

        }
        
        public void ComposePicture(Visual visual)
        {
            using (var dc = drawingGroup.Open())
            {
                VisualBrush vb = new VisualBrush(visual);
                vb.Stretch = Stretch.UniformToFill;
                dc.DrawRectangle(vb, null, new Rect(0, 0, image.Width, image.Height));
            }
        }
    }
}
