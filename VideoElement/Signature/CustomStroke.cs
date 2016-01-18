using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;

namespace VideoElement.Signature
{
    public class CustomStroke : Stroke
    {
        Brush brush;
        Pen pen;
        private double _strokeScale = 1d;
        private SolidColorBrush _brushes = Brushes.Black;
        public SolidColorBrush GeometryBrush
        {
            get { return this._brushes; }
            set { this._brushes = value; }
        }
        public double StrokeScale
        {
            get { return this._strokeScale; }
            set { this._strokeScale = value; }
        }
        public CustomStroke(StylusPointCollection stylusPoints, double strokeScale)
            : base(stylusPoints)
        {
            // Create the Brush and Pen used for drawing.
            brush = new LinearGradientBrush(Colors.Red, Colors.Blue, 20d);
            pen = new Pen(brush, 2d);
            this._strokeScale = strokeScale;
        } 
        protected override void DrawCore(DrawingContext drawingContext,
                                         DrawingAttributes drawingAttributes)
        {
            // Allocate memory to store the previous point to draw from.
            Point prevPoint = new Point(double.NegativeInfinity,
                                        double.NegativeInfinity);
            // Draw linear gradient ellipses between 
            // all the StylusPoints in the Stroke.
            for (int i = 0; i < this.StylusPoints.Count; i++)
            {
                Point pt = (Point)this.StylusPoints[i];
                Vector v = Point.Subtract(prevPoint, pt);

                // Only draw if we are at least 4 units away 
                // from the end of the last ellipse. Otherwise, 
                // we're just redrawing and wasting cycles.
                if (v.Length > 4)
                {
                    // Set the thickness of the stroke 
                    // based on how hard the user pressed.
                    //double radius = this.StylusPoints[i].PressureFactor * 10d;
                    if (prevPoint.X == double.NegativeInfinity || prevPoint.X == double.PositiveInfinity)
                    {
                        drawingContext.DrawGeometry(_brushes, null, PathGeometryClass.GetPathGeometry(pt, _strokeScale));
                    }
                    else
                        foreach (var item in PathGeometryClass.GetSeparatePoint(prevPoint, pt))
                        {
                            drawingContext.DrawGeometry(_brushes, null, PathGeometryClass.GetPathGeometry(item, _strokeScale));
                        }
                    //drawingContext.DrawGeometry(Brushes.Black, null, PathGeometryClass.GetPathGeometry(pt));
                    //drawingContext.DrawEllipse(brush, pen, pt, radius, radius);
                    prevPoint = pt;
                }
            }
        }
    }
}
