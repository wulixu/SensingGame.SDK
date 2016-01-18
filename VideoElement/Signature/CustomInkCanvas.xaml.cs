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
using System.Windows.Ink;

namespace VideoElement.Signature
{
    /// <summary>
    /// Interaction logic for MyInkCanvas.xaml
    /// </summary>
    public partial class CustomInkCanvas : UserControl
    {
        //private InkCanvas inkCanvas = new InkCanvas();
        private Dictionary<int, Stroke> TouchStrokes = new Dictionary<int, Stroke>();
        private Dictionary<int, StylusPointCollection> TrackStrokes = new Dictionary<int, StylusPointCollection>();
        private SolidColorBrush _brush = new SolidColorBrush(Colors.Blue);
        private double _defaultWeight = 5d;
        private double _currentWeight = 5d;
        private Image eraser = new Image();
        public event Action<bool> ToolBarEvent;

        //private Dictionary<int, PointInfo> LastPoints = new Dictionary<int, PointInfo>();
        public CustomInkCanvas()
        {
            InitializeComponent();
            this.TouchDown += new EventHandler<TouchEventArgs>(TouchInkCanvas_TouchDown);
            this.TouchMove += new EventHandler<TouchEventArgs>(TouchInkCanvas_TouchMove);
            this.TouchUp += new EventHandler<TouchEventArgs>(TouchInkCanvas_TouchUp);
            this.TouchLeave += new EventHandler<TouchEventArgs>(TouchInkCanvas_TouchUp);
            //eraser.Source = new BitmapImage(new Uri("pack://application:,,,/Images/eraser1.png", UriKind.Absolute));
            eraser.Source = new BitmapImage(new Uri("pack://application:,,,/VideoElement;component/Images/eraser1.png", UriKind.Absolute));

            eraser.Stretch = Stretch.Uniform;
            this.eraser.Width = 20;
            this.eraser.Height = 20;
            this.eraser.SetBinding(UIElement.VisibilityProperty, new Binding("EditingMode") { Source = this.inkCanvas, Converter = new MyEraserVisibilityConverter() });
            this._rootCanvas.Children.Add(this.eraser);
            this.inkCanvas.IsEnabled = false;
        }
        void TouchInkCanvas_TouchDown(object sender, TouchEventArgs e)
        {
   
            e.Handled = true;
            
            if (ToolBarEvent != null)
                ToolBarEvent.Invoke(false);
            if (this.inkCanvas.EditingMode == InkCanvasEditingMode.Ink)
            {
                StartDraw(e);
            }
        }
        void TouchInkCanvas_TouchMove(object sender, TouchEventArgs e)
        {

            e.Handled = true;
            switch (this.EditingMode)
            {
                case InkCanvasEditingMode.Ink:
                    Drawing(e);
                    break;
                case InkCanvasEditingMode.EraseByPoint:
                    {
                        Canvas.SetLeft(this.eraser, e.TouchDevice.GetTouchPoint(this).Position.X - this.eraser.Width / 2);
                        Canvas.SetTop(this.eraser, e.TouchDevice.GetTouchPoint(this).Position.Y - this.eraser.Height / 2);
                        this.EraseByTouchPoint(e);
                    }
                    break;
                //case InkCanvasEditingMode.EraseByStroke:
                //    this.EraseByStroke(e);
                //    break;
                default:
                    return;
            }
        }

        void TouchInkCanvas_TouchUp(object sender, TouchEventArgs e)
        {

            e.Handled = true;

            if (this.inkCanvas.EditingMode == InkCanvasEditingMode.Ink)
            {
                FinishDraw(e);
            }
            if (TouchStrokes.Count == 0)
            {
                if (ToolBarEvent != null)
                    ToolBarEvent.Invoke(true);
            }
        }

        #region Public Method
        public void SetBrushScale(double scale)
        {
            this._currentWeight = scale * _defaultWeight;
            this.inkCanvas.EraserShape = new EllipseStylusShape(15 * scale, 15 * scale);
            this.eraser.Width = 20 * scale;
            this.eraser.Height = 20 * scale;
            //(this.DynamicRenderer as CustomDynamicRenderer).RendererScale = this._brushScale;
            //this.EraserShape = new EllipseStylusShape(_StylusShape.Width * scale, _StylusShape.Height * 2 * scale);
            //this.EditingMode = InkCanvasEditingMode.Ink;
        }
        public void SetBrush(SolidColorBrush brush)
        {
            _brush = brush;
            //this.DefaultDrawingAttributes.Color = brush.Color;
            this.EditingMode = InkCanvasEditingMode.Ink;
        }
        public void SetInkCanvasEditingMode(InkCanvasEditingMode mode)
        {
            this.inkCanvas.EditingMode = mode;
        }
        public void ClearStrokes()
        {
            this.inkCanvas.Strokes.Clear();
        }
        public void Reset()
        {
            this.Strokes.Clear();
            this.EditingMode = InkCanvasEditingMode.Ink;
        }
        #endregion

        #region Method

        private void StartDraw(TouchEventArgs e)
        {
            TouchPoint tp = e.GetTouchPoint(this.inkCanvas);
            StylusPointCollection collection = new StylusPointCollection();
            collection.Add(new StylusPoint(tp.Position.X, tp.Position.Y));
            StylusPointCollection trackCollection = new StylusPointCollection();
            trackCollection.Add(new StylusPoint(tp.Position.X, tp.Position.Y));
            Stroke currentStroke = new Stroke(collection, new DrawingAttributes { Color = _brush.Color, Height = _currentWeight, Width = _currentWeight });
            this.inkCanvas.Strokes.Add(currentStroke);
            if (TouchStrokes.ContainsKey(e.TouchDevice.Id))
            {
                TouchStrokes.Remove(e.TouchDevice.Id);
                TrackStrokes.Remove(e.TouchDevice.Id);
            }
            TouchStrokes.Add(e.TouchDevice.Id, currentStroke);
            TrackStrokes.Add(e.TouchDevice.Id, trackCollection);
        }
        private void Drawing(TouchEventArgs e)
        {
            if (TouchStrokes.ContainsKey(e.TouchDevice.Id))
            {
                TouchPoint tp = e.GetTouchPoint(this.inkCanvas);
                Stroke currentStroke = TouchStrokes[e.TouchDevice.Id];
                currentStroke.StylusPoints.Add(new StylusPoint(tp.Position.X, tp.Position.Y));
                TrackStrokes[e.TouchDevice.Id].Add(new StylusPoint(tp.Position.X, tp.Position.Y));
            }
        }
        private void FinishDraw(TouchEventArgs e)
        {
            if (TouchStrokes.ContainsKey(e.TouchDevice.Id))
            {
                //Stroke currentStroke = TouchStrokes[e.TouchDevice.Id];
                //StylusPointCollection tracCollection = TrackStrokes[e.TouchDevice.Id];
                //StylusPointCollection drawCollection = new StylusPointCollection();
                //int i = 0;
                //drawCollection.Add(tracCollection[i]);
                //for (; i < tracCollection.Count - 1; i++)
                //{
                //    StylusPoint[] points = InsertPoints(tracCollection[i], tracCollection[i + 1], 2);
                //    for (int j = 1; j < points.Length; j++)
                //    {
                //        drawCollection.Add(points[j]);
                //    }
                //}

                //this.inkCanvas.Strokes.Remove(TouchStrokes[e.TouchDevice.Id]);
                TouchStrokes.Remove(e.TouchDevice.Id);
                TrackStrokes.Remove(e.TouchDevice.Id);
                //currentStroke = new Stroke(drawCollection, this.inkCanvas.DefaultDrawingAttributes.Clone());
                //this.inkCanvas.Strokes.Add(currentStroke);
            }
        }
        private void EraseByTouchPoint(TouchEventArgs contact)
        {
            List<Point> list = this.PrepareErase(contact);
            foreach (Stroke stroke in this.inkCanvas.Strokes.HitTest(list, this.inkCanvas.EraserShape))
            {
                StrokeCollection eraseResult = stroke.GetEraseResult(list, this.inkCanvas.EraserShape);
                this.inkCanvas.Strokes.Replace(stroke, eraseResult);
            }
        }
        private List<Point> PrepareErase(TouchEventArgs contact)
        {

            List<Point> points = new List<Point>();
            points.Add(contact.GetTouchPoint(this).Position);
            return points;

            //List<Point> points = new List<Point>();
            //foreach (var item in contact.GetIntermediateTouchPoints(this.inkCanvas))
            //{
            //    points.Add(item.Position);
            //}
            //return points;
        }
        #endregion

        #region Virtual

        void inkCanvas_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            this.OnStrokeCollected(e);
        }

        protected virtual void OnStrokeCollected(InkCanvasStrokeCollectedEventArgs args)
        {
            base.RaiseEvent(args);
        }

        #endregion

        #region Override

        //protected override void OnInitialized(EventArgs e)
        //{
        //    base.OnInitialized(e);
        //    base.AddVisualChild(this.inkCanvas);
        //}

        //protected override Visual GetVisualChild(int index)
        //{
        //    return this.inkCanvas;
        //}

        //protected override Size MeasureOverride(Size availableSize)
        //{
        //    this.inkCanvas.Measure(availableSize);
        //    return this.inkCanvas.DesiredSize;
        //}

        //protected override int VisualChildrenCount
        //{
        //    get
        //    {
        //        return 1;
        //    }
        //}

        #endregion

        #region Property
        public InkCanvasEditingMode EditingMode
        {
            get
            {
                return this.inkCanvas.EditingMode;
            }
            set
            {
                this.inkCanvas.EditingMode = value;
            }
        }

        public StrokeCollection Strokes
        {
            get
            {
                return this.inkCanvas.Strokes;
            }
            set
            {
                this.inkCanvas.Strokes = value;
            }
        }

        //public StylusShape EraserShape
        //{
        //    get
        //    {
        //        return this.inkCanvas.EraserShape;
        //    }
        //    set
        //    {
        //        this.inkCanvas.EraserShape = value;
        //    }
        //}
        #endregion
    }
    public class MyEraserVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.Equals(InkCanvasEditingMode.EraseByPoint))
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
