using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using VideoElement.Utils;

namespace VideoElement.EraserTool
{
    /// <summary>
    /// Interaction logic for EraserControl.xaml
    /// </summary>
    public partial class EraserControl : UserControl
    {
        #region Events

        public event Action CloseEraserToolEvent;
        public event Action<SolidColorBrush> PenColorEvent;
        public event Action<int> PenSizeEvent;
        public event Action<bool> EraserEvent;
        public event Action ClearEvent;

        #endregion

        #region Fields

        private Brush _selectedBrush;
        private Brush _unSelectedBrush = null;
        private Storyboard _storyboard;
        #endregion
        public EraserControl()
        {
            InitializeComponent();

            _selectedBrush = this.FindResource("SelectedBrush") as ImageBrush;
            ElementColor.ImgBrush = this.FindResource("BlueBrush") as ImageBrush;
            _storyboard = this.FindResource("DisplayStoryboard") as Storyboard;
        }

        private void UIElement_OnTouchDown(object sender, TouchEventArgs e)
        {
            if (CloseEraserToolEvent != null)
                CloseEraserToolEvent.Invoke();
        }

        /// <summary>
        /// 获取选中的颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIElement_OnTouchDownColor(object sender, TouchEventArgs e)
        {
            var border = sender as Border;
            CommonHelper.ScaleElement(border, () =>
            {
                var color = border.Tag.ToString();
                var brush = Brushes.Black;// Colors.Black;

                switch (color)
                {
                    case "Red":
                        brush = (SolidColorBrush)new BrushConverter().ConvertFromString("#c8FF0000");
                        ElementColor.ImgBrush = this.FindResource("RedBrush") as ImageBrush;
                        break;
                    case "Yellow":
                        brush = (SolidColorBrush)new BrushConverter().ConvertFromString("#c8FFFF00");
                        ElementColor.ImgBrush = this.FindResource("YellowBrush") as ImageBrush;
                        break;
                    case "Blue":
                        brush = (SolidColorBrush)new BrushConverter().ConvertFromString("#c80000FF");
                        ElementColor.ImgBrush = this.FindResource("BlueBrush") as ImageBrush;
                        break;
                    case "Green":
                        brush = (SolidColorBrush)new BrushConverter().ConvertFromString("#c8006400");
                        ElementColor.ImgBrush = this.FindResource("GreenBrush") as ImageBrush;
                        break;
                    case "White":
                        brush = (SolidColorBrush)new BrushConverter().ConvertFromString("#c8ffffff");
                        ElementColor.ImgBrush = this.FindResource("WhiteBrush") as ImageBrush;
                        break;
                    case "Black":
                        brush = (SolidColorBrush)new BrushConverter().ConvertFromString("#c8000000");
                        ElementColor.ImgBrush = this.FindResource("BlackBrush") as ImageBrush;
                        break;
                }
                if (PenColorEvent != null)
                    PenColorEvent.Invoke(brush);
                CancelEraser();

                ColorBorder.Visibility = Visibility.Hidden;
            });
        }

        public void InitialPen()
        {
            ElementColor.ImgBrush = this.FindResource("BlackBrush") as ImageBrush;
        }

        private void ElementColor_OnTouchDown(object sender, TouchEventArgs e)
        {
            CommonHelper.ScaleElement(sender as FrameworkElement, () =>
            {
                ColorBorder.Visibility = Visibility.Visible;
                ColroPanel.Visibility = Visibility.Visible;
                PenPanel.Visibility = Visibility.Collapsed;
                CancelEraser();
                _storyboard.Begin();
            });

        }

        private void ElementEraser_OnTouchDown(object sender, TouchEventArgs e)
        {
            CommonHelper.ScaleElement(sender as FrameworkElement, () =>
            {
                if (EraserEvent != null)
                    EraserEvent.Invoke(true);
            });
        }
        /// <summary>
        /// 取消橡皮擦
        /// </summary>
        private void CancelEraser()
        {
            if (EraserEvent != null)
                EraserEvent.Invoke(false);
        }

        private void ElementPen_OnTouchDown(object sender, TouchEventArgs e)
        {
            CommonHelper.ScaleElement(sender as FrameworkElement, () =>
            {
                ColorBorder.Visibility = Visibility.Visible;
                ColroPanel.Visibility = Visibility.Collapsed;
                PenPanel.Visibility = Visibility.Visible;
                CancelEraser();

                _storyboard.Begin();
            });

        }

        private void ElementClear_OnTouchDown(object sender, TouchEventArgs e)
        {
            CommonHelper.ScaleElement(sender as FrameworkElement, () =>
            {
                if (ClearEvent != null)
                    ClearEvent.Invoke();
                CancelEraser();
            });

        }

        /// <summary>
        /// 获取笔的粗细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIElement_OnTouchDownPen(object sender, TouchEventArgs e)
        {
            CommonHelper.ScaleElement(sender as FrameworkElement, () =>
            {
                var border = sender as Border;
                var size = int.Parse(border.Tag.ToString());

                if (PenSizeEvent != null)
                    PenSizeEvent.Invoke(size);
                CancelEraser();

                ColorBorder.Visibility = Visibility.Hidden;
            });


        }


        private void UIElement_OnTouchDown1(object sender, TouchEventArgs e)
        {
            var imageTitle = e.Source as ImageTitle;
            var stackPanel = sender as StackPanel;
            if (stackPanel != null)
                stackPanel.Children.OfType<ImageTitle>().ToList().ForEach(x =>
                {
                    x.Background = _unSelectedBrush;
                });

            if (imageTitle != null && imageTitle.Name == "ElementEraser")
                imageTitle.Background = _selectedBrush;
        }
    }
}
