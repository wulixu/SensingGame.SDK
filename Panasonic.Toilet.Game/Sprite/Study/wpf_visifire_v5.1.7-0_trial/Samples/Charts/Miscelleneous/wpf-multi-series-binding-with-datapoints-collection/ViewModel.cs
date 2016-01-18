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
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using Visifire.Charts;

namespace WPF_MultiSeriesBindingWithDataPointsCollection
{
    public class ViewModel : INotifyPropertyChanged
    {   
        public ViewModel()
        {   
            //// Adding 2 empty DataPoints collection
            //for (int index = 0; index < MAX_NUMBER_OF_SERIES; index++)
            //    DataPointsOfSeries.Add(new DataPointCollection());

            UpdateChartData();
        }

        public void UpdateChartData()
        {   
            RefreshDataSeries(0);
            RefreshDataSeries(1);
        }
        
        private void RefreshDataSeries(Int32 index)
        {   
            DateTime renderStartTime = DateTime.Now;

            // Show Loading DataSeries Msg..
            if (App.Current.Windows[0] != null)
            {
                App.Current.Windows[0].Dispatcher.BeginInvoke(new Action(delegate
                    {
                        ShowLoadingMsg = Visibility.Visible;
                    }));
            }

            // Update DataPointCollection
            DataPointCollection dataPoints = new DataPointCollection();

            Int32 index1 = index + 1;

            for (int i = 0; i < 100; i++)
                dataPoints.Add(new DataPoint() { YValue = rn.Next(index1 * 20 + 15, index1 * 20 + 30), AxisXLabel = "AxisXLabel" + i.ToString() });
            
            if (DataPointsOfSeries.Count < 2)
                DataPointsOfSeries.Add(dataPoints);
            else
                DataPointsOfSeries[index] = dataPoints;
            
            // Hide Loading DataSeries Msg..
            if (App.Current.Windows[0] != null)
            {
                App.Current.Windows[0].Dispatcher.BeginInvoke(new Action(delegate
                {
                    ShowLoadingMsg = Visibility.Collapsed;
                }));
            }
        }

        public ObservableCollection<DataPointCollection> DataPointsOfSeries
        {
            get
            {   
                return _dataPointsOfSeries;
            }
            set
            {
                _dataPointsOfSeries = value;
                FirePropertyChanged("DataPointsOfSeries");
            }
        }

        public Visibility ShowLoadingMsg
        {
            get
            {
                return _showLoadingMsg;
            }
            set
            {
                _showLoadingMsg = value;
                FirePropertyChanged("ShowLoadingMsg");
            }
        }

        ObservableCollection<DataPointCollection> _dataPointsOfSeries = new ObservableCollection<DataPointCollection>();

        Visibility _showLoadingMsg = Visibility.Collapsed;

        Int32 MAX_NUMBER_OF_SERIES = 2;

        Random rn = new Random(DateTime.Now.Second);

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void FirePropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
