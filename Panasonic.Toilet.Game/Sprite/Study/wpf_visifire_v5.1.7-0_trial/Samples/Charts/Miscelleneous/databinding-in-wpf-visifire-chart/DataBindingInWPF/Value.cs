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

namespace DataBindingInWPF
{
    public class ValuesCollection : ObservableCollection<Value> { };

    public class Value : INotifyPropertyChanged
    {
        public String Label
        {
            get
            {
                return _label;
            }
            set
            {
                _label = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Label"));
            }
        }

        public Double YValue
        {
            get
            {
                return _yValue;
            }
            set
            {
                _yValue = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("YValue"));
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        Double _yValue;
        String _label;
    }
}
