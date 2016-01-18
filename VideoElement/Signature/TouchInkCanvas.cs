using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Data;

namespace VideoElement.Signature
{

    public enum InkCanvasStatus { Ink, Erase }
    public class TouchInkCanvas : InkCanvas
    {
        private double _brushScale = 1d;
        private double _defaultScale = 0.15d;
        private Image eraser = new Image();
        private SolidColorBrush _brush=new SolidColorBrush(Colors.Black);
        private RectangleStylusShape _StylusShape = new RectangleStylusShape(40, 40);
        private Canvas _rootCanvas = new Canvas();
        public TouchInkCanvas()
            : base()
        {
            this.DynamicRenderer = new CustomDynamicRenderer(1);

            eraser.Source = new BitmapImage(new Uri("pack://application:,,,/Images/eraser1.png", UriKind.Absolute));
            eraser.Stretch = Stretch.Uniform;
            this.eraser.Width = 40;
            this.eraser.Height = 40;
            this.eraser.SetBinding(UIElement.VisibilityProperty, new Binding("EditingMode") { Source = this, Converter=new EraserVisibilityConverter() });
            this.EraserShape = new EllipseStylusShape(_StylusShape.Width, _StylusShape.Height,-20);
            //this.eraser.Opacity = 0.5;
            //this.eraser.RenderTransformOrigin = new Point(0.5, 0.5);
            //this.eraser.RenderTransform = new RotateTransform(-10);
            //this.eraser.Fill = new SolidColorBrush(Colors.Gray);
            this.TouchMove += new EventHandler<TouchEventArgs>(TouchInkCanvas_TouchMove);
            //this.eraser.Visibility = Visibility.Hidden;
            this._rootCanvas.Children.Add(this.eraser);
            this.Children.Add(_rootCanvas); 
        }
        void TouchInkCanvas_TouchMove(object sender, TouchEventArgs e)
        {
            if (this.EditingMode == InkCanvasEditingMode.None)
            {
                Canvas.SetLeft(this.eraser, e.TouchDevice.GetTouchPoint(_rootCanvas).Position.X - this.eraser.Width / 2);
                Canvas.SetTop(this.eraser, e.TouchDevice.GetTouchPoint(_rootCanvas).Position.Y - this.eraser.Height / 2);
                EraseByTouchPoint(e);
            }
        }
        private void EraseByTouchPoint(TouchEventArgs contact,Rect rec)
        {
            List<Point> list = this.PrepareErase(contact);
            StrokeCollection col = this.Strokes.HitTest(list, this.EraserShape);
            foreach (Stroke stroke in col)
            {
                //返回剩下的线段
                StrokeCollection eraseResult = stroke.GetEraseResult(rec);
                this.Strokes.Replace(stroke, eraseResult);
            }
        }
        private void EraseByTouchPoint(TouchEventArgs contact)
        {
            List<Point> list = this.PrepareErase(contact);
            
            StrokeCollection col = this.Strokes.HitTest(list, this.EraserShape);
            foreach (Stroke stroke in col)
            {
                //返回剩下的线段
                StrokeCollection eraseResult = stroke.GetEraseResult(list, this.EraserShape);

                this.Strokes.Replace(stroke, eraseResult);
            }
        }
        private List<Point> PrepareErase(TouchEventArgs contact)
        {
            List<Point> points = new List<Point>();
            //foreach (var item in contact.GetIntermediateTouchPoints(this))
            //{
            //    points.Add(item.Position);
            //}
            points.Add(contact.GetTouchPoint(this).Position);
            return points;
        }
        public void SetBrushScale(double scale)
        {
            this._brushScale = scale * _defaultScale;
            (this.DynamicRenderer as CustomDynamicRenderer).RendererScale = this._brushScale;
            //this.EraserShape = new EllipseStylusShape(_StylusShape.Width * scale, _StylusShape.Height * 2 * scale);
            this.EditingMode = InkCanvasEditingMode.Ink;
        }
        public void SetBrush(SolidColorBrush brush)
        {
            if (_brush != null)
            {
                _brush = brush;
                (this.DynamicRenderer as CustomDynamicRenderer).GeometryBrush = _brush;
                SetInkCanvasEditingMode(InkCanvasStatus.Ink);
            }
        }
        public void SetInkCanvasEditingMode(InkCanvasStatus mode)
        {
            if (mode == InkCanvasStatus.Ink)
            {
                this.EditingMode = InkCanvasEditingMode.Ink;
                //this.eraser.Visibility = Visibility.Hidden;
            }
            else if (mode == InkCanvasStatus.Erase)
            {
                //this.EraserShape = new RectangleStylusShape(50, 50,-30);
                if (this.EditingMode == InkCanvasEditingMode.None)
                    this.EditingMode = InkCanvasEditingMode.Ink;
                else
                this.EditingMode = InkCanvasEditingMode.None;
                //this.eraser.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// 清除
        /// </summary>
        public void ClearStrokes()
        {
            this.Strokes.Clear();
            this.EditingMode = InkCanvasEditingMode.Ink;
        }
        public void Reset()
        {
            this.Strokes.Clear();
            this.EditingMode = InkCanvasEditingMode.Ink;
        }
        protected override void OnStrokeCollected(InkCanvasStrokeCollectedEventArgs e)
        {
            // Remove the original stroke and add a custom stroke.
            this.Strokes.Remove(e.Stroke);
            CustomStroke customStroke = new CustomStroke(e.Stroke.StylusPoints, _brushScale);
            customStroke.GeometryBrush = _brush;
            this.Strokes.Add(customStroke);

            // Pass the custom stroke to base class' OnStrokeCollected method.
            InkCanvasStrokeCollectedEventArgs args =
                new InkCanvasStrokeCollectedEventArgs(customStroke);
            base.OnStrokeCollected(args);
        }
    }
    public class EraserVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.Equals(InkCanvasEditingMode.None))
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
           
}
