using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections;
using Visifire.Charts;
using Visifire.Commons;

namespace WorkingWithEventsInMultiChart
{   
    
    public class MultiChart : Visifire.Charts.Chart
    {   
        #region SeriesSource (DependencyProperty)

        public IEnumerable SeriesSource
        {
            get { return (IEnumerable)GetValue(SeriesSourceProperty); }
            set { SetValue(SeriesSourceProperty, value); }
        }
        public static readonly DependencyProperty SeriesSourceProperty = DependencyProperty.Register("SeriesSource", typeof(IEnumerable), typeof(MultiChart), new PropertyMetadata(default(IEnumerable), new PropertyChangedCallback(OnSeriesSourceChanged)));

        private static void OnSeriesSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IEnumerable oldValue = (IEnumerable)e.OldValue;
            IEnumerable newValue = (IEnumerable)e.NewValue;
            MultiChart source = (MultiChart)d;
            source.OnSeriesSourceChanged(oldValue, newValue);
        }

        public event EventHandler<System.Windows.Input.MouseButtonEventArgs> OnDataPointMouseLeftButtonUp;

        void series_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (OnDataPointMouseLeftButtonUp != null)
                OnDataPointMouseLeftButtonUp(sender, e); //sender will be a DataPoint           
        }
        
        protected virtual void OnSeriesSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            this.Series.Clear();

            if (newValue != null)
            {
                foreach (object item in newValue)
                {
                    DataTemplate dataTemplate = null;

                    // get data template
                    if (this.SeriesTemplateSelector != null)
                    {
                        dataTemplate = this.SeriesTemplateSelector.SelectTemplate(item, this);
                    }
                    if (dataTemplate == null && this.SeriesTemplate != null)
                    {
                        dataTemplate = this.SeriesTemplate;
                    }

                    // load data template content
                    if (dataTemplate != null)
                    {
                        // Series series = dataTemplate.LoadContent() as Series;
                        DataSeries series = dataTemplate.LoadContent() as DataSeries;

                        if (series != null)
                        {
                            // set data context
                            series.DataContext = item;

                            //  Attach event with DataSeries
                            series.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(series_MouseLeftButtonUp);

                            this.Series.Add(series);
                        }
                    }
                }
            }
        }

        #endregion

        #region SeriesTemplate (DependencyProperty)

        public DataTemplate SeriesTemplate
        {
            get { return (DataTemplate)GetValue(SeriesTemplateProperty); }
            set { SetValue(SeriesTemplateProperty, value); }
        }
        public static readonly DependencyProperty SeriesTemplateProperty = DependencyProperty.Register("SeriesTemplate", typeof(DataTemplate), typeof(MultiChart), new PropertyMetadata(default(DataTemplate)));

        #endregion

        #region SeriesTemplateSelector (DependencyProperty)

        public DataTemplateSelector SeriesTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(SeriesTemplateSelectorProperty); }
            set { SetValue(SeriesTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty SeriesTemplateSelectorProperty = DependencyProperty.Register("SeriesTemplateSelector", typeof(DataTemplateSelector), typeof(MultiChart), new PropertyMetadata(default(DataTemplateSelector)));

        #endregion
    }
}
