using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace VideoElement.Utils
{
    public static class CommonHelper
    {
        public static bool _isPlaying;

        /// <summary>
        /// 缩小放大动画
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        public static void ScaleElement(FrameworkElement element, Action action)
        {
            if (_isPlaying)
                return;

            element.RenderTransformOrigin = new Point(0.5, 0.5);
            element.RenderTransform = new ScaleTransform();

            var storyboard = new Storyboard();
            var scaleSmallX = new DoubleAnimation { From = 1, To = 0.8, Duration = TimeSpan.FromSeconds(0.2) };
            var scaleSmallY = new DoubleAnimation { From = 1, To = 0.8, Duration = TimeSpan.FromSeconds(0.2) };
            var scaleBigX = new DoubleAnimation { From = 0.8, To = 1, BeginTime = TimeSpan.FromSeconds(0.2), Duration = TimeSpan.FromSeconds(0.1) };
            var scaleBigY = new DoubleAnimation { From = 0.8, To = 1, BeginTime = TimeSpan.FromSeconds(0.2), Duration = TimeSpan.FromSeconds(0.1) };

            Storyboard.SetTarget(scaleSmallX, element);
            Storyboard.SetTargetProperty(scaleSmallX, new PropertyPath("(FrameworkElement.RenderTransform).(ScaleTransform.ScaleX)"));
            Storyboard.SetTarget(scaleSmallY, element);
            Storyboard.SetTargetProperty(scaleSmallY, new PropertyPath("(FrameworkElement.RenderTransform).(ScaleTransform.ScaleY)"));

            Storyboard.SetTarget(scaleBigX, element);
            Storyboard.SetTargetProperty(scaleBigX, new PropertyPath("(FrameworkElement.RenderTransform).(ScaleTransform.ScaleX)"));
            Storyboard.SetTarget(scaleBigY, element);
            Storyboard.SetTargetProperty(scaleBigY, new PropertyPath("(FrameworkElement.RenderTransform).(ScaleTransform.ScaleY)"));

            storyboard.Children.Add(scaleSmallX);
            storyboard.Children.Add(scaleSmallY);
            storyboard.Children.Add(scaleBigY);
            storyboard.Children.Add(scaleBigX);

            storyboard.Completed += (sender, args) =>
            {
                if (action != null)
                    action.Invoke();
                _isPlaying = false;
            };

            storyboard.Begin();
            _isPlaying = true;
        }

        /// <summary>
        /// web脚本禁用
        /// </summary>
        /// <param name="webBrowser"></param>
        /// <param name="hide"></param>
        public static void SuppressScriptErrors(this WebBrowser webBrowser, bool hide)
        {
            webBrowser.Navigating += (s, e) =>
            {
                var fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                if (fiComWebBrowser == null)
                    return;

                object objComWebBrowser = fiComWebBrowser.GetValue(webBrowser);
                if (objComWebBrowser == null)
                    return;

                objComWebBrowser.GetType().InvokeMember("Silent", System.Reflection.BindingFlags.SetProperty, null, objComWebBrowser, new object[] { hide });
            };
        }

        /// <summary>
        /// 设置物体的index为最高
        /// </summary>
        public static void SetIndexOfElement<T>(Panel control, FrameworkElement children)
        {
            if (control != null)
            {
                var scaleItems = control.Children.OfType<T>();
                var max = scaleItems.OfType<FrameworkElement>().Max(x => Panel.GetZIndex(x));
                Panel.SetZIndex(children, max + 1);
            }
        }
    }
}
