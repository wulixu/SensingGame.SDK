using System.Windows;

namespace SensingAds.ViewBanner.Transitions
{
    public class NoneTransformer : PageTransformer
    {
        public void OnAnimationEnd(FrameworkElement view, double position)
        {
            if (position > 0)
            {
                view.Opacity = 1;
            }
            else
            {
                view.Opacity = 0;
            }
        }

        public void OnAnimationStart(FrameworkElement view, double position)
        {
        }

        public void OnAnimationUpdate(FrameworkElement view, double position)
        {
            
        }
    }
}


