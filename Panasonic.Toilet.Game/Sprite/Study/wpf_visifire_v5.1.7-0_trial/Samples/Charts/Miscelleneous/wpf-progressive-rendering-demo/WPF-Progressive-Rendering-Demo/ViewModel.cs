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

namespace WPF_Progressive_Rendering_Demo
{   
    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        {
            LoadDataFromWebService();
        }

        public void LoadDataFromWebService()
        {
            ObservableCollection<PointInfo> dataPoints = new ObservableCollection<PointInfo>();

            for (int i = 0; i < 100000; i++)
                dataPoints.Add(new PointInfo() { YValue = random.Next(20 + 15, i * 20 + 60), XValue = i });

            Data = dataPoints;

            FromValue = 0;
            ToValue = 1000;
        }

        public ObservableCollection<PointInfo> Data
        {   
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                FirePropertyChanged("Data");
            }
        }

        public Object FromValue
        {
            get
            {   
                return _fromValue;
            }
            set
            {   
                if (_fromValue != value)
                {   
                    _fromValue = value;
                    FirePropertyChanged("FromValue");
                }
            }
        }

        public Object ToValue
        {   
            get
            {   
                return _toValue;
            }
            set
            {   
                if (_toValue != value)
                {   
                    _toValue = value;
                    FirePropertyChanged("ToValue");
                }
            }
        }

        public void RefreshPointsCollection()
        {   
            if (FromValue != null && ToValue != null)
            {
                var points = (from pointInfo in Data
                                where pointInfo.XValue >= Convert.ToDouble(FromValue)
                                && pointInfo.XValue <= Convert.ToDouble(ToValue)
                                select new DataPoint()
                                {
                                    YValue = pointInfo.YValue,
                                    XValue = pointInfo.XValue
                                });

              
                DataPointsOfSeries1 = new DataPointCollection(points);
            }
        }

        public DataPointCollection DataPointsOfSeries1
        {   
            get
            {   
                return _dataPointsOfSeries1;
            }
            set
            {
                _dataPointsOfSeries1 = value;
                FirePropertyChanged("DataPointsOfSeries1");
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

        DataPointCollection _dataPointsOfSeries1 = new DataPointCollection();
        ObservableCollection<PointInfo> _data = new ObservableCollection<PointInfo>();

        Visibility _showLoadingMsg = Visibility.Collapsed;
        
        Object _fromValue = 0.0;
        Object _toValue = 1000.0;

        // Random Number
        Random random = new Random(DateTime.Now.Second);

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void FirePropertyChanged(String propertyName)
        {   
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
