using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Media;

namespace VideoElement.Signature
{
    public class CustomDynamicRenderer : DynamicRenderer
    {
        private Point prevPoint;
        private double _rendererScale = 1d;
        private SolidColorBrush _brush = Brushes.Black;
        public SolidColorBrush GeometryBrush
        {
            get { return this._brush; }
            set { this._brush = value; }
        }
        public double RendererScale
        {
            get { return this._rendererScale; }
            set { this._rendererScale = value; }
        }
        //private PathGeometry pathGeometry;
        public CustomDynamicRenderer(double rendererScale)
            : base()
        {
            this._rendererScale = rendererScale;
        }
        protected override void OnStylusDown(RawStylusInput rawStylusInput)
        {
            // Allocate memory to store the previous point to draw from.
            prevPoint = new Point(double.NegativeInfinity, double.NegativeInfinity);
            base.OnStylusDown(rawStylusInput);
        }

        protected override void OnDraw(DrawingContext drawingContext,
                                       StylusPointCollection stylusPoints,
                                       Geometry geometry, Brush fillBrush)
        {
            for (int i = 0; i < stylusPoints.Count; i++)
            {
                Point pt = (Point)stylusPoints[i];
                Vector v = Point.Subtract(prevPoint, pt);
                if (v.Length > 4)
                {
                    if (prevPoint.X == double.NegativeInfinity || prevPoint.X == double.PositiveInfinity)
                    {
                        drawingContext.DrawGeometry(_brush, null, PathGeometryClass.GetPathGeometry(pt, _rendererScale));
                    }
                    else
                        foreach (var item in PathGeometryClass.GetSeparatePoint(prevPoint, pt))
                        {
                            drawingContext.DrawGeometry(_brush, null, PathGeometryClass.GetPathGeometry(item, _rendererScale));
                        }
                    prevPoint = pt;
                }  
            }
        }
    }
}
