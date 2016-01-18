using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoElement
{

   

    public class VideoControlViewModel : INotifyPropertyChanged
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
        public VideoControlViewModel()
        {
            log.Debug("Avaliable Devices:");
            DeviceName = ConfigurationManager.AppSettings["DeviceName"];
            
           //DeviceName = "Logitech HD Webcam C310";
            //DeviceName = "Kinect";
            ForegoundPicDirectory = "foregound";
            SaveDirectory = "save/pic";
        }


        public string DeviceName { get; set; }

        public string ForegoundPicDirectory { get; set; }

        public string SaveDirectory { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }

    }
}
