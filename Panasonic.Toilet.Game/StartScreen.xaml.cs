using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TronCell.Game
{
    /// <summary>
    /// Interaction logic for StartScreen.xaml
    /// </summary>
    public partial class StartScreen : UserControl
    {
        private Storyboard animation;

        public static readonly RoutedEvent AnimationFinishedEvent = EventManager.RegisterRoutedEvent(
    "AnimationFinished", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(StartScreen));

        // Provide CLR accessors for the event 
        public event RoutedEventHandler AnimationFinished
        {
            add { AddHandler(AnimationFinishedEvent, value); }
            remove { RemoveHandler(AnimationFinishedEvent, value); }
        }


        public StartScreen()
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
            RaiseEvent(new RoutedEventArgs(StartScreen.AnimationFinishedEvent,this));
        }
        public void Start()
        {
            this.Visibility = System.Windows.Visibility.Visible;
            if (animation != null)
            {
                SoundPlayer action = new SoundPlayer();
                action.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Resources\\go.wav";
                action.Play();
                animation.Begin();
            }
        }

        public void Normalize()
        {
            
        }
    }
}
