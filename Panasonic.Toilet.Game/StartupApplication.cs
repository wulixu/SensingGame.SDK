using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;

namespace TronCell.Game
{
    public partial class StartupApplication : Application
    {
       static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            XmlConfigurator.Configure();
            logger.Error("version: 2015-3-25 ");
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            DispatcherUnhandledException += StartupApplication_DispatcherUnhandledException;
        }

        void StartupApplication_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            logger.Error("the dispather has gotten some unhandled exceptions.", e.Exception);
            e.Handled = true;
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            logger.Error(e.ExceptionObject);
        }

        protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
        {
            logger.Error("session ending.");
            base.OnSessionEnding(e);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            logger.Error("the gaming is exiting");
            base.OnExit(e);
        }

        /// <summary>
        /// Entry Point of Cobon Intelligent Sensing Platform.
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        public static int Main(string[] args)
        {
            foreach (string arg in args)
            {
                logger.Debug("With an argument:" + arg);
            }

            if (IsAlreadyRunning)
            {
                Environment.Exit(0);
            }



            StartupApplication mainApp = new StartupApplication();
            mainApp.InitializeComponent();
            //mainApp.splashScreen = new Splash();
            //mainApp.splashScreen.ShowDialog();
            mainApp.Run();

            return 0;
        }
        public void InitializeComponent()
        {
            this.StartupUri = new System.Uri("MainWindow.xaml", System.UriKind.Relative);
        }
        //this is used for control that there is just one named process exists in one machine.
        private static Mutex mutex;
        private static bool IsAlreadyRunning
        {
            get
            {
                string strLoc = Assembly.GetExecutingAssembly().Location;
                FileSystemInfo fileInfo = new FileInfo(strLoc);
                string sExeName = fileInfo.Name;
                bool bCreatedNew = false;

                mutex = new Mutex(true, "Global\\" + sExeName, out bCreatedNew);
                return !bCreatedNew;
            }
        }
    }
}
