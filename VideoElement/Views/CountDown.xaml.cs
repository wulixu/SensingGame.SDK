using System;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace VideoElement
{
    /// <summary>
    /// Interaction logic for StartScreen.xaml
    /// </summary>
    public partial class CountDown : UserControl
    {
        private Storyboard animation;
        private bool _isRunning = false;

        public static readonly RoutedEvent AnimationFinishedEvent = EventManager.RegisterRoutedEvent(
    "AnimationFinished", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CountDown));

        // Provide CLR accessors for the event 
        public event RoutedEventHandler AnimationFinished
        {
            add { AddHandler(AnimationFinishedEvent, value); }
            remove { RemoveHandler(AnimationFinishedEvent, value); }
        }


        public CountDown()
        {
            InitializeComponent();

            animation = this.Resources["Animation"] as Storyboard;
            
            if (animation != null)
            {
                animation.Completed += AnimationOnCompleted;
            }
        }

        private void AnimationOnCompleted(object sender, EventArgs eventArgs)
        {
            _isRunning = false;
            RaiseEvent(new RoutedEventArgs(CountDown.AnimationFinishedEvent,this));
        }
        

        public void Start()
        {
            this.Visibility = System.Windows.Visibility.Visible;
            if (animation != null && !_isRunning)
            {
                _isRunning = true;
                //SoundPlayer action = new SoundPlayer();
                //action.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\go.wav";
                //action.Play();
                animation.Begin();
            }
        }

        public void Normalize()
        {
            
        }
    }
}
