using System.Windows;

namespace SensingAds.ViewBanner
{
    public interface PageTransformer
    {
        void OnAnimationStart(FrameworkElement view, double position);
        void OnAnimationEnd(FrameworkElement view, double position);
        void OnAnimationUpdate(FrameworkElement view, double position);



    }
}
