using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Visifire.Charts;
using System.Windows.Data;

using System.Diagnostics;
using System.ComponentModel;

namespace Visifire.Controls
{   
    public partial class RangeBar : UserControl
    {
        public RangeBar()
        {
            InitializeComponent();

            this.SizeChanged += new SizeChangedEventHandler(RangeBar_SizeChanged);
            this.Loaded += new RoutedEventHandler(RangeBar_Loaded);
        }

        void RangeBar_Loaded(object sender, RoutedEventArgs e)
        {
            _renderBlock = true;

            Binding backgroundBinding = new Binding("Background");
            backgroundBinding.Source = this;
            backgroundBinding.Mode = BindingMode.TwoWay;
            CenterThumbBorder.SetBinding(Border.BackgroundProperty, backgroundBinding);

            if (Chart == null)
            {
                Dispatcher.BeginInvoke(new Action(delegate
                {
                    PositionLeftAndRightThumb();
                    RenderLabels();
                    UpdateLeftThumbVisual();
                    UpdateRightThumbVisual();
                }));
            }
        }

        #region Public Methods

        #endregion

        #region Public Properties

        /// <summary>
        /// Range Start Value
        /// </summary>
        public Object FromValue
        {
            get
            {
                return (Object)GetValue(FromValueProperty);
            }
            set
            {
                SetValue(FromValueProperty, value);
            }
        }

        /// <summary>
        /// Range End Value
        /// </summary>
        public Object ToValue
        {
            get
            {
                return (Object)GetValue(ToValueProperty);
            }
            set
            {
                SetValue(ToValueProperty, value);
            }
        }

        /// <summary>
        /// Range End Value
        /// </summary>
        public Boolean ResizeEnabled
        {
            get
            {
                return (Boolean)GetValue(ResizeEnabledProperty);
            }
            set
            {
                SetValue(ResizeEnabledProperty, value);
            }
        }

        /// <summary>
        /// Scale Maximum Value
        /// </summary>
        public Object Maximum
        {
            get
            {
                return (Object)GetValue(MaximumProperty);
            }
            set
            {
                SetValue(MaximumProperty, value);
            }
        }

        /// <summary>
        /// Scale Minumum Value
        /// </summary>
        public Object Minimum
        {
            get
            {
                return (Object)GetValue(MinimumProperty);
            }
            set
            {
                SetValue(MinimumProperty, value);
            }
        }

        /// <summary>
        /// NUmber of Minor Ticks
        /// </summary>
        public Int32 NumberOfMinorTicks
        {
            get
            {
                return (Int32)GetValue(NumberOfMinorTicksProperty);
            }
            set
            {
                SetValue(NumberOfMinorTicksProperty, value);
            }
        }

        /// <summary>
        /// Number Of Labels to be displaied in RangeBar
        /// </summary>
        public Int32 NumberOfLabels
        {
            get
            {
                return (Int32)GetValue(NumberOfLabelsProperty);
            }
            set
            {
                SetValue(NumberOfLabelsProperty, value);
            }
        }

        /// <summary>
        /// XValue format String
        /// </summary>
        public String XValueFormatString
        {
            get
            {
                String val = (String)GetValue(XValueFormatStringProperty);

                if (String.IsNullOrEmpty(val))
                {
                    if (Minimum != null && Minimum.GetType().Equals(typeof(DateTime)))
                        return "MM-yyyy";
                    else
                        return "#0.00";
                }

                return val;
            }
            set
            {
                SetValue(XValueFormatStringProperty, value);
            }
        }

        /// <summary>
        /// Visifire Chart
        /// </summary>
        public Chart Chart
        {
            get
            {
                return (Chart)GetValue(ChartProperty);
            }
            set
            {
                SetValue(ChartProperty, value);
            }
        }

        /// <summary>
        /// Animation Enabled
        /// </summary>
        public Boolean AnimationEnabled
        {
            get
            {
                return (Boolean)GetValue(AnimationEnabledProperty);
            }
            set
            {
                SetValue(AnimationEnabledProperty, value);
            }
        }

        #region DependencyProperties Def

        /// <summary>
        /// Identifies the Visifire.Controls.RangeBar.Maximum dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the Visifire.Controls.RangeBar.Maximum dependency property.
        /// </returns>
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register
            ("Maximum",
            typeof(Object),
            typeof(RangeBar), new PropertyMetadata(null, MaximumValueChanged));

        /// <summary>
        /// Identifies the Visifire.Controls.RangeBar.Maximum dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the Visifire.Controls.RangeBar.Maximum dependency property.
        /// </returns>
        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register
            ("Minimum",
            typeof(Object),
            typeof(RangeBar), new PropertyMetadata(null, MinimumValueChanged));

        /// <summary>
        /// Identifies the Visifire.Controls.RangeBar.ResizeEnabled dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the Visifire.Controls.RangeBar.ResizeEnabled dependency property.
        /// </returns>
        public static readonly DependencyProperty ResizeEnabledProperty = DependencyProperty.Register
            ("ResizeEnabled",
            typeof(Boolean),
            typeof(RangeBar), new PropertyMetadata(true, ResizeEnabledPropertyChanged));

        /// <summary>
        /// Identifies the Visifire.Controls.RangeBar.NumberOfMinorTicks dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the Visifire.Controls.RangeBar.NumberOfMinorTicks dependency property.
        /// </returns>
        public static readonly DependencyProperty NumberOfMinorTicksProperty = DependencyProperty.Register
            ("NumberOfMinorTicks",
            typeof(Int32),
            typeof(RangeBar),
            new PropertyMetadata(6, NumberOfMinorTicksChanged));

        /// <summary>
        /// Identifies the Visifire.Controls.RangeBar.AnimationEnabled dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the Visifire.Controls.RangeBar.AnimationEnabled dependency property.
        /// </returns>
        public static readonly DependencyProperty AnimationEnabledProperty = DependencyProperty.Register
            ("AnimationEnabled",
            typeof(Boolean),
            typeof(RangeBar), new PropertyMetadata(true, null));


        /// <summary>
        /// Identifies the Visifire.Controls.RangeBar.ToValue dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the Visifire.Controls.RangeBar.ToValue dependency property.
        /// </returns>
        public static readonly DependencyProperty ToValueProperty = DependencyProperty.Register
            ("ToValue",
            typeof(Object),
            typeof(RangeBar), null);

        /// <summary>
        /// Identifies the Visifire.Controls.RangeBar.FromValue dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the Visifire.Controls.RangeBar.FromValue dependency property.
        /// </returns>
        public static readonly DependencyProperty FromValueProperty = DependencyProperty.Register
            ("FromValue",
            typeof(Object),
            typeof(RangeBar), null);

        /// <summary>
        /// Identifies the Visifire.Controls.RangeBar.NumberOfLabels dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the Visifire.Controls.RangeBar.NumberOfLabels dependency property.
        /// </returns>
        public static readonly DependencyProperty NumberOfLabelsProperty = DependencyProperty.Register
            ("NumberOfLabels",
            typeof(Int32),
            typeof(RangeBar),
            new PropertyMetadata(6, NumberOfLabelsChanged));

        /// <summary>
        /// Identifies the Visifire.Controls.RangeBar.XValueFormatString dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the Visifire.Controls.RangeBar.XValueFormatString dependency property.
        /// </returns>
        public static readonly DependencyProperty XValueFormatStringProperty = DependencyProperty.Register
            ("XValueFormatString",
            typeof(String),
            typeof(RangeBar),
            new PropertyMetadata(XValueFormatStringChanged));

        /// <summary>
        /// Identifies the Visifire.Controls.RangeBar.Chart dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the Visifire.Controls.RangeBar.Chart dependency property.
        /// </returns>
        public static readonly DependencyProperty ChartProperty = DependencyProperty.Register
            ("Chart",
            typeof(Chart),
            typeof(RangeBar), new PropertyMetadata(ChartChanged));

        #endregion

        #endregion

        #region Public Events And Delegates

        /// <summary>
        /// Range changed Event
        /// </summary>
        public event EventHandler<RangeBarEventArgs> RangeChanged;

        #endregion

        #region Protected Methods

        #endregion

        #region Internal Properties

        #endregion

        #region Private Properties

        /// <summary>
        /// Whether the chart is in design mode or application mode
        /// </summary>
        private Boolean IsInDesignMode
        {
            get
            {
                return System.ComponentModel.DesignerProperties.GetIsInDesignMode(this);
            }
        }

        #endregion

        #region Private Delegates

        /// <summary>
        /// Center Thumb DragStarted
        /// </summary>
        private void Thumb_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            _leftPanelWidth = ContentGrid.ColumnDefinitions[0].ActualWidth;
            _rightPanelWidth = ContentGrid.ColumnDefinitions[2].ActualWidth;
        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            if (e.HorizontalChange < 0)
            {
                if (_leftPanelWidth > 0)
                {
                    Double newLeftBarWidth = _leftPanelWidth - Math.Abs(e.HorizontalChange);
                    Double newRightBarWidth = _rightPanelWidth + Math.Abs(e.HorizontalChange);

                    if (newLeftBarWidth < 0)
                    {
                        Double diff = _leftPanelWidth - 0;
                        newRightBarWidth = _rightPanelWidth + diff;
                        newLeftBarWidth = 0;
                    }

                    _leftPanelWidth = newLeftBarWidth;
                    _rightPanelWidth = newRightBarWidth;

                    ContentGrid.ColumnDefinitions[0].Width = new GridLength(_leftPanelWidth, GridUnitType.Pixel);
                    ContentGrid.ColumnDefinitions[2].Width = new GridLength(_rightPanelWidth, GridUnitType.Pixel);
                }
            }

            if (e.HorizontalChange > 0)
            {
                if (_rightPanelWidth > 0)
                {
                    Double newLeftBarWidth = _leftPanelWidth + Math.Abs(e.HorizontalChange);
                    Double newRightBarWidth = _rightPanelWidth - Math.Abs(e.HorizontalChange);

                    if (newRightBarWidth < 0)
                    {
                        Double diff = _rightPanelWidth - 0;
                        newLeftBarWidth = _leftPanelWidth + diff;
                        newRightBarWidth = 0;
                    }

                    _leftPanelWidth = newLeftBarWidth;
                    _rightPanelWidth = newRightBarWidth;

                    ContentGrid.ColumnDefinitions[0].Width = new GridLength(_leftPanelWidth, GridUnitType.Pixel);
                    ContentGrid.ColumnDefinitions[2].Width = new GridLength(_rightPanelWidth, GridUnitType.Pixel);
                }
            }
        }

        /// <summary>
        /// Center Thumb DragCompleted
        /// </summary>
        private void Thumb_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            UpdateFromToValue();
        }

        #region LeftThumb Delegates

        private void LeftThumb_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            if (ResizeEnabled)
            {
                _leftPanelWidth = ContentGrid.ColumnDefinitions[0].ActualWidth;
                _rightPanelWidth = ContentGrid.ColumnDefinitions[2].ActualWidth;

                _leftPanelStartWidth = ContentGrid.ColumnDefinitions[0].ActualWidth;
            }
        }

        private void LeftThumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            if (ResizeEnabled)
            {
                Double newWidth = _leftPanelWidth + e.HorizontalChange;

                if (newWidth < 0)
                    newWidth = 0;
                else if (newWidth > this.ActualWidth)
                    newWidth = this.ActualWidth;

                if (newWidth + _rightPanelWidth > this.ActualWidth)
                    return;

                _leftPanelWidth = newWidth;

                Debug.WriteLine("Width=" + _leftPanelWidth.ToString());

                ContentGrid.ColumnDefinitions[0].Width = new GridLength(_leftPanelWidth, GridUnitType.Pixel);
            }
        }

        private void LeftThumb_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            if (ResizeEnabled)
            {
                RightThumb.SetValue(Canvas.ZIndexProperty, 4);
                LeftThumb.SetValue(Canvas.ZIndexProperty, 5);

                AnimateLeftPanel();
            }
        }

        #endregion

        #region RightThumb Delegates

        private void RightThumb_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            if (ResizeEnabled)
            {
                _leftPanelWidth = ContentGrid.ColumnDefinitions[0].ActualWidth;
                _rightPanelWidth = ContentGrid.ColumnDefinitions[2].ActualWidth;

                _rightPanelStartWidth = ContentGrid.ColumnDefinitions[2].ActualWidth;
            }
        }

        private void RightThumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            if (ResizeEnabled)
            {
                Double newWidth = _rightPanelWidth - e.HorizontalChange;

                if (newWidth < 0)
                    newWidth = 0;
                else if (newWidth > this.ActualWidth)
                    newWidth = this.ActualWidth;

                if (newWidth + _leftPanelWidth > this.ActualWidth)
                    return;

                _rightPanelWidth = newWidth;
                Debug.WriteLine("Width=" + _leftPanelWidth.ToString());

                ContentGrid.ColumnDefinitions[2].Width = new GridLength(_rightPanelWidth, GridUnitType.Pixel);
            }
        }

        private void RightThumb_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            if (ResizeEnabled)
            {
                LeftThumb.SetValue(Canvas.ZIndexProperty, 4);
                RightThumb.SetValue(Canvas.ZIndexProperty, 5);

                AnimateRightPanel();
            }
        }

        #endregion

        private void LeftPanelAnimationStoryboard_Completed(object sender, EventArgs e)
        {
            UpdateFromValue();
        }

        private void RightPanelAnimationStoryboard_Completed(object sender, EventArgs e)
        {
            UpdateToValue();
        }

        private void UpdateFromValue()
        {
            FromValue = PixelToValue(_leftPanelWidth);

            if (RangeChanged != null)
                RangeChanged(this, new RangeBarEventArgs()
                {
                    IsFromValueChanged = true,
                    IsToValueChanged = false,
                    FromValue = FromValue
                });
        }

        private void UpdateToValue()
        {
            ToValue = PixelToValue(this.ActualWidth - _rightPanelWidth);

            if (RangeChanged != null)
                RangeChanged(this, new RangeBarEventArgs()
                {
                    IsFromValueChanged = false,
                    IsToValueChanged = true,
                    ToValue = ToValue
                });
        }

        private void UpdateFromToValue()
        {
            FromValue = PixelToValue(_leftPanelWidth);
            ToValue = PixelToValue(this.ActualWidth - _rightPanelWidth);

            if (RangeChanged != null)
                RangeChanged(this, new RangeBarEventArgs()
                {
                    IsFromValueChanged = true,
                    IsToValueChanged = true,
                    FromValue = FromValue,
                    ToValue = ToValue
                });
        }

        private Double ValueToPixel(Object value)
        {
            Double retVal;

            if (Chart != null)
                retVal = Chart.AxesX[0].XValueToPixelXPosition(value);
            else if (Minimum.GetType().Equals(typeof(DateTime))) // Is a DateTime Scale
            {
                Double min = ((DateTime)Minimum).ToOADate();
                Double max = ((DateTime)Maximum).ToOADate();
                Double valueOADate = ((DateTime)value).ToOADate();
                retVal = Visifire.Commons.Graphics.ValueToPixelPosition(0, this.ActualWidth, min, max, valueOADate);
            }
            else
            {
                Double min = Convert.ToDouble(Minimum);
                Double max = Convert.ToDouble(Maximum);
                retVal = Visifire.Commons.Graphics.ValueToPixelPosition(0, this.ActualWidth, min, max, Convert.ToDouble(value));
            }

            if (retVal < 0)
                retVal = 0;

            return retVal;
        }

        private Object PixelToValue(Double pixelWidth)
        {
            if (Chart != null)
                return (DateTime)Chart.AxesX[0].PixelPositionToXValue(pixelWidth);
            else if (Minimum.GetType().Equals(typeof(DateTime))) // Is a DateTime Scale
            {
                Double min = ((DateTime)Minimum).ToOADate();
                Double max = ((DateTime)Maximum).ToOADate();
                return DateTime.FromOADate(Visifire.Commons.Graphics.PixelPositionToValue(0, this.ActualWidth, min, max, pixelWidth));
            }
            else // Numeric Scale
            {
                Double min = Convert.ToDouble(Minimum);
                Double max = Convert.ToDouble(Maximum);
                return Visifire.Commons.Graphics.PixelPositionToValue(0, this.ActualWidth, min, max, pixelWidth);
            }
        }

        void RangeBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Chart == null || _chartRendered)
            {
                RenderLabels();

                Dispatcher.BeginInvoke(new Action(delegate
                {
                    UpdateLeftThumbVisual();
                    UpdateLeftThumbVisual();
                }));
            }

            this.Clip = new RectangleGeometry()
            {
                Rect = new Rect(new Point(-4.5, 0), new Point(e.NewSize.Width + 4.5, e.NewSize.Height))
            };
        }

        #endregion

        #region Private Methods

        private void AnimateRightPanel()
        {
            if (AnimationEnabled && !IsInDesignMode)
            {
                Storyboard rightStoryBoard = ContentGrid.Resources["RightPanelAnimationStoryboard"] as Storyboard;
                DoubleAnimation RightAnimation = rightStoryBoard.Children[0] as DoubleAnimation;
                DoubleAnimation RightGripAnimation = rightStoryBoard.Children[1] as DoubleAnimation;

                if (_rightPanelStartWidth == 0 || _rightPanelWidth == 0)
                    RightAnimation.From = 0;
                else
                {
                    RightAnimation.From = _rightPanelStartWidth / _rightPanelWidth;
                }

                if (_rightPanelStartWidth < _rightPanelWidth)
                    RightGripAnimation.From = Math.Abs(_rightPanelWidth - _rightPanelStartWidth);
                else
                    RightGripAnimation.From = -Math.Abs(_rightPanelWidth - _rightPanelStartWidth);

                rightStoryBoard.Begin();
            }
            else
            {
                UpdateToValue();
            }
        }

        private void AnimateLeftPanel()
        {
            if (AnimationEnabled && !IsInDesignMode)
            {
                Storyboard leftStoryBoard = ContentGrid.Resources["LeftPanelAnimationStoryboard"] as Storyboard;
                DoubleAnimation LeftAnimation = leftStoryBoard.Children[0] as DoubleAnimation;
                DoubleAnimation LeftGripAnimation = leftStoryBoard.Children[1] as DoubleAnimation;

                if (_leftPanelStartWidth == 0 || _leftPanelWidth == 0)
                    LeftAnimation.From = 0;
                else
                {
                    LeftAnimation.From = _leftPanelStartWidth / _leftPanelWidth;
                }

                if (_leftPanelStartWidth < _leftPanelWidth)
                    LeftGripAnimation.From = -Math.Abs(_leftPanelWidth - _leftPanelStartWidth);
                else
                    LeftGripAnimation.From = Math.Abs(_leftPanelWidth - _leftPanelStartWidth);

                leftStoryBoard.Begin();
            }
            else
            {
                UpdateFromValue();
            }
        }


        private static void XValueFormatStringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RangeBar rangeBar = d as RangeBar;
            rangeBar.RenderLabels();
        }

        private static void FromValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RangeBar rangeBar = d as RangeBar;
            // rangeBar.BuildLabels();
        }

        private static void ToValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RangeBar rangeBar = d as RangeBar;
            // rangeBar.BuildLabels();
        }

        private static void NumberOfLabelsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RangeBar rangeBar = d as RangeBar;
            rangeBar.RenderLabels();
        }

        private static void ResizeEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RangeBar rangeBar = d as RangeBar;
            if ((Boolean)e.NewValue)
            {
                rangeBar.LeftThumb.Visibility = Visibility.Visible;
                rangeBar.RightThumb.Visibility = Visibility.Visible;
            }
            else
            {
                rangeBar.LeftThumb.Visibility = Visibility.Collapsed;
                rangeBar.RightThumb.Visibility = Visibility.Collapsed;
            }
        }

        private static void NumberOfMinorTicksChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RangeBar rangeBar = d as RangeBar;
            rangeBar.RenderLabels();
        }

        private static void MaximumValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RangeBar rangeBar = d as RangeBar;

            if (!rangeBar._renderBlock)
                rangeBar.RenderLabels();
        }

        private static void MinimumValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RangeBar rangeBar = d as RangeBar;

            if (!rangeBar._renderBlock)
                rangeBar.RenderLabels();
        }

        private static void ChartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RangeBar rangeBar = d as RangeBar;
            rangeBar.ChartContainer.Children.Clear();

            if (e.NewValue != null)
            {
                Chart chart = e.NewValue as Chart;
                chart.Rendered += new EventHandler(chart_Rendered);

                rangeBar.ChartContainer.Children.Add(chart);
            }
        }

        private static void chart_Rendered(object sender, EventArgs e)
        {
            Chart chart = sender as Chart;

            Grid contentGrid = (chart.Parent as Grid).Parent as Grid;
            RangeBar rangeBar = contentGrid.Parent as RangeBar;

            rangeBar.Dispatcher.BeginInvoke(new Action(delegate
            {
                rangeBar.PositionLeftAndRightThumb();

                contentGrid.RowDefinitions[1].Height = new GridLength(chart.AxesX[0].Height, GridUnitType.Pixel);

                rangeBar._renderBlock = true;

                rangeBar.Minimum = (DateTime)chart.AxesX[0].ActualAxisMinimum;
                rangeBar.Maximum = (DateTime)chart.AxesX[0].ActualAxisMaximum;

                rangeBar._renderBlock = false;

                rangeBar.RenderLabels();

                if (rangeBar.FromValue == null)
                {
                    rangeBar.FromValue = (DateTime)chart.AxesX[0].ActualAxisMinimum;

                    if (rangeBar.RangeChanged != null)
                        rangeBar.RangeChanged(rangeBar, new RangeBarEventArgs()
                        {
                            IsFromValueChanged = true,
                            IsToValueChanged = false,
                            FromValue = rangeBar.FromValue
                        });
                }
                else
                    rangeBar.UpdateLeftThumbVisual();

                if (rangeBar.ToValue == null)
                {
                    rangeBar.ToValue = (DateTime)chart.AxesX[0].ActualAxisMaximum;

                    if (rangeBar.RangeChanged != null)
                        rangeBar.RangeChanged(rangeBar, new RangeBarEventArgs()
                        {
                            IsFromValueChanged = false,
                            IsToValueChanged = true,
                            ToValue = rangeBar.ToValue
                        });
                }
                else
                    rangeBar.UpdateRightThumbVisual();
            }));

            rangeBar._chartRendered = true;
        }

        private void PositionLeftAndRightThumb()
        {
            if (Chart != null)
            {
                LeftLine.Height = ActualHeight - Chart.AxesX[0].Height;
                RightLine.Height = ActualHeight - Chart.AxesX[0].Height;
            }
            else
            {
                LeftLine.Height = this.ActualHeight;
                RightLine.Height = this.ActualHeight;
            }

            LeftLine.SetValue(Canvas.TopProperty, -(LeftLine.Height / 2));
            RightLine.SetValue(Canvas.TopProperty, -(RightLine.Height / 2));
        }

        private void UpdateLeftThumbVisual()
        {
            _leftPanelStartWidth = _leftPanelWidth;
            _leftPanelWidth = ValueToPixel(FromValue);
            ContentGrid.ColumnDefinitions[0].Width = new GridLength(_leftPanelWidth, GridUnitType.Pixel);

            AnimateLeftPanel();
        }

        private void UpdateRightThumbVisual()
        {
            _rightPanelStartWidth = _rightPanelWidth;
            _rightPanelWidth = this.ActualWidth - ValueToPixel(ToValue);

            if (Double.IsNaN(_rightPanelWidth) || _rightPanelWidth < 0)
                _rightPanelWidth = 0;

            ContentGrid.ColumnDefinitions[2].Width = new GridLength(_rightPanelWidth, GridUnitType.Pixel);

            AnimateRightPanel();
        }

        private void RenderLabels()
        {
            if (Minimum == null || Maximum == null || this.ActualWidth == 0)
                return;

            LabelsCanvas.Children.Clear();

            // If DateTime Axis
            if (Minimum.GetType().Equals(typeof(DateTime)))
            {
                CreateLabelsForDateTimeAxis();
            }
            else
            {
                CreateLabelsForNumericAxis(); /* AxisXLables are not considered */
            }
        }

        private void CreateLabelsForNumericAxis()
        {
            if (Minimum == null || Maximum == null)
                return;

            TextBlock textblock;

            Double min = Convert.ToDouble(Minimum);
            Double max = Convert.ToDouble(Maximum);

            /* If you want to display last label */
            /* textblock = new TextBlock()
               {
                  Text = min.ToString(XValueFormatString),
                  VerticalAlignment = VerticalAlignment.Bottom,
                  HorizontalAlignment = HorizontalAlignment.Right,
                  Foreground = new SolidColorBrush(Colors.Black)
               };
            
               LabelsCanvas.Children.Add(textblock);
            */

            Double length = max - min;
            Double interval = length / NumberOfLabels;
            Double widthInterval = this.ActualWidth / NumberOfLabels;

            Double tempPosition = min;
            Double tempLeft = 0;
            for (Int32 index = 0; index < NumberOfLabels; index++)
            {
                textblock = new TextBlock()
                {
                    Margin = new Thickness(tempLeft + 2, 0, 0, 0),
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Text = tempPosition.ToString(XValueFormatString),
                    Foreground = new SolidColorBrush(Colors.Black)
                };

                tempPosition = tempPosition + interval;

                Rectangle rect = new Rectangle()
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Width = 1,
                    Margin = new Thickness(tempLeft, 0, 0, 0),
                    Fill = new SolidColorBrush(Colors.DarkGray)
                };

                LabelsCanvas.Children.Add(rect);
                LabelsCanvas.Children.Add(textblock);

                Double nextMajorTicksPos = tempPosition + interval;
                Double minorInterval = (nextMajorTicksPos - tempPosition) / NumberOfMinorTicks;
                Double minorIntervalWidth = widthInterval / NumberOfMinorTicks;

                for (Int32 minorTicksIndex = 0; minorTicksIndex < NumberOfMinorTicks; minorTicksIndex++)
                {
                    Rectangle rectangle = new Rectangle()
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = System.Windows.VerticalAlignment.Top,
                        Height = 4,
                        Width = 1,
                        Margin = new Thickness(tempLeft + minorIntervalWidth * minorTicksIndex, 0, 0, 0),
                        Fill = new SolidColorBrush(Colors.DarkGray)
                    };

                    LabelsCanvas.Children.Add(rectangle);
                }

                tempLeft = tempLeft + widthInterval;
            }
        }

        private void CreateLabelsForDateTimeAxis()
        {
            if (Minimum == null || Maximum == null)
                return;

            TextBlock textblock;

            DateTime min = (DateTime)Minimum;
            DateTime max = (DateTime)Maximum;

            /* If you want to display last label */
            /* textblock = new TextBlock()
               {
                  Text = min.ToString(XValueFormatString),
                  VerticalAlignment = VerticalAlignment.Bottom,
                  HorizontalAlignment = HorizontalAlignment.Right,
                  Foreground = new SolidColorBrush(Colors.Black)
               };
            
               LabelsCanvas.Children.Add(textblock);
            */

            TimeSpan timeSpan = max - min;
            Double interval = timeSpan.Ticks / NumberOfLabels;
            Double widthInterval = this.ActualWidth / NumberOfLabels;

            DateTime tempDateTime = min;
            Double tempLeft = 0;

            for (Int32 index = 0; index < NumberOfLabels; index++)
            {
                textblock = new TextBlock()
                {
                    Margin = new Thickness(tempLeft + 2, 0, 0, 0),
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Text = tempDateTime.ToString(XValueFormatString),
                    Foreground = new SolidColorBrush(Colors.Black)
                };

                tempDateTime = tempDateTime.AddTicks((long)(interval));

                Rectangle rect = new Rectangle()
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Width = 1,
                    Margin = new Thickness(tempLeft, 0, 0, 0),
                    Fill = new SolidColorBrush(Colors.DarkGray)
                };

                LabelsCanvas.Children.Add(rect);
                LabelsCanvas.Children.Add(textblock);

                DateTime nextDateTime = tempDateTime.AddTicks((long)interval);
                long minorInterval = (nextDateTime - tempDateTime).Ticks / NumberOfMinorTicks;
                Double minorIntervalWidth = widthInterval / NumberOfMinorTicks;

                for (Int32 minorTicksIndex = 0; minorTicksIndex < NumberOfMinorTicks; minorTicksIndex++)
                {
                    Rectangle rectangle = new Rectangle()
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = System.Windows.VerticalAlignment.Top,
                        Height = 4,
                        Width = 1,
                        Margin = new Thickness(tempLeft + minorIntervalWidth * minorTicksIndex, 0, 0, 0),
                        Fill = new SolidColorBrush(Colors.DarkGray)
                    };

                    LabelsCanvas.Children.Add(rectangle);
                }

                tempLeft = tempLeft + widthInterval;
            }
        }
        #endregion

        #region Internal Methods

        #endregion

        #region Internal Events And Delegates

        #endregion

        #region Data

        Double _leftPanelStartWidth = 0;
        Double _leftPanelWidth = 0;

        Double _rightPanelStartWidth = 0;
        Double _rightPanelWidth = 0;
        Boolean _chartRendered = false;

        Boolean _renderBlock = true;

        #endregion
    }

    /// <summary>
    /// Visifire.Controls.RangeBarEventArgs
    /// </summary>
    public class RangeBarEventArgs : EventArgs
    {
        public Boolean IsFromValueChanged
        {
            get;
            internal set;
        }

        public Boolean IsToValueChanged
        {
            get;
            internal set;
        }

        public Object FromValue
        {
            get;
            internal set;
        }

        public Object ToValue
        {
            get;
            internal set;
        }
    }
}