using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using TronCell.Game.Utility;

namespace TronCell.Game.Sprite
{
    public class Sprite : TileSource, INotifyPropertyChanged
    {
        #region Constructors

        public Sprite()
            : base()
        {
            Animations = new List<Animation>();
            Loaded += new RoutedEventHandler(Sprite_Loaded);
        }

        #endregion

        #region Properties

        public int Speed
        {
            get
            {
                return (int)GetValue(SpeedProperty);
            }
            set
            {
                if (Speed != value)
                {
                    SetValue(SpeedProperty, value);
                    PropertyHasChanged("Speed");
                    if (AnimationTimer != null)
                    {
                        AnimationTimer.Interval = new TimeSpan(0, 0, 0, 0, Speed);
                    }
                }
            }
        }

        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(int), typeof(Sprite), new PropertyMetadata(0));

        public string CurrentAnimation
        {
            get
            {
                return (string)GetValue(CurrentAnimationProperty);
            }
            set
            {
                if (CurrentAnimation != value)
                {
                    SetValue(CurrentAnimationProperty, value);
                    PropertyHasChanged("CurrentAnimation");

                    SetCurrentAnimation();
                }
            }
        }

        public static readonly DependencyProperty CurrentAnimationProperty =
            DependencyProperty.Register("CurrentAnimation", typeof(string), typeof(Sprite), new PropertyMetadata(string.Empty));

        private Animation Animation { get; set; }

        private int AnimationIndex { get; set; }

        public List<Animation> Animations { get; set; }

        private int StartTime { get; set; }

        protected DispatcherTimer AnimationTimer { get; set; }

        #endregion

        #region Methods

        private void SetCurrentAnimation()
        {
            Animation = Animations[0];
            foreach (Animation anim in Animations)
            {
                if (anim.Name == CurrentAnimation)
                {
                    Animation = anim;
                    break;
                }
            }
        }

        #endregion

        #region Event Handlers

        private void Sprite_Loaded(object sender, RoutedEventArgs e)
        {
            StartTime = Timer.GetSystemTimeMS();
            SetCurrentAnimation();
            //AnimationTimer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, Speed), DispatcherPriority.Render, new EventHandler(Sprite_Animate), Dispatcher);
            Sprite_Animate(null, null);
            Unloaded += Sprite_Unloaded;
            Loaded -= Sprite_Loaded;
        }

        void Sprite_Unloaded(object sender, RoutedEventArgs e)
        {
            Unloaded -= Sprite_Unloaded;
            AnimationTimer = null;
        }

        private void Sprite_Animate(object sender, EventArgs e)
        {
            AnimationIndex = (Timer.GetSystemTimeMS() - StartTime) / Speed;
            RenderTile = Animation.Tiles[AnimationIndex % Animation.Tiles.Count];
        }

        #endregion
    }
}