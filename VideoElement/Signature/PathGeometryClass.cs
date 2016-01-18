using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace VideoElement.Signature
{
    public static class PathGeometryClass
    {
        public static PathGeometry GetPathGeometry(Point point, double scale)
        {
            point = new Point(point.X - 30 * scale, point.Y - 25 * scale);
            PathFigure pathfigure = new PathFigure();
            pathfigure.StartPoint = new System.Windows.Point(point.X, point.Y);
            //pathfigure.IsClosed = true;
            pathfigure.Segments.Add(new LineSegment(new System.Windows.Point(30 * scale + point.X, 50 * scale + point.Y), true));
            ArcSegment arcseg = new ArcSegment();
            arcseg.Point = new System.Windows.Point(60 * scale + point.X, 20 * scale + point.Y);
            arcseg.Size = new System.Windows.Size(20 * scale, 20 * scale);
            pathfigure.Segments.Add(arcseg);
            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(pathfigure);
            return pathGeometry;
        }
        public static List<Point> GetSeparatePoint(Point beginPoint, Point endPoint)
        {

            List<Point> pL = new List<Point>();

            int dotTime = 0;
            int intervalLen = 2;//步长

            double lineLen = Math.Sqrt(Math.Pow(beginPoint.X - endPoint.X, 2) + Math.Pow(beginPoint.Y - endPoint.Y, 2));//线的长度
            Point currentPoint = new Point(beginPoint.X, beginPoint.Y);

            double relativaRate = Math.Abs(endPoint.Y - beginPoint.Y) * 1.0 / Math.Abs(endPoint.X - beginPoint.X);

            double angle = Math.Atan(relativaRate) * 180 / Math.PI;//直线的角度大小，无需考虑正负

            int xOrientation = endPoint.X > beginPoint.X ? 1 : -1;//判断新生成点的X轴方向

            int yOrientation = endPoint.Y > beginPoint.Y ? 1 : -1;

            if (lineLen >= intervalLen)
            {

                while (dotTime * intervalLen < lineLen)
                {
                    double x = currentPoint.X + dotTime * intervalLen * Math.Cos(angle * Math.PI / 180) * xOrientation;
                    double y = currentPoint.Y + dotTime * intervalLen * Math.Sin(angle * Math.PI / 180) * yOrientation;

                    pL.Add(new Point(x, y));
                    x += intervalLen * Math.Cos(angle * Math.PI / 180) * xOrientation;
                    y += intervalLen * Math.Sin(angle * Math.PI / 180) * yOrientation;
                    pL.Add(new Point(x, y));
                    dotTime += 2;
                }
            }
            return pL;
        }
    }
}
