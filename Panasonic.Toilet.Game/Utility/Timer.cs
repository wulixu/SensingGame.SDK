using System;
using System.Collections.Generic;
using TronCell.Game.Utility.WindowsAPI;

namespace TronCell.Game.Utility
{
    public class Timer
    {
        #region Classes

        public class TimedFunction
        {
            public string Name { get; set; }
            public TimedFunctionHandler Handler { get; set; }
            public int TimeUntilCall { get; set; }
            public int CallTime { get; set; }
        }

        #endregion

        #region Events

        public delegate bool TimedFunctionHandler();

        #endregion

        #region Constructors

        static Timer()
        {
            long freq;
            Kernel32.QueryPerformanceFrequency(out freq);
            Frequency = (int)(freq / 1000);
        }

        /// <summary>
        /// The timer will start from this time value.
        /// </summary>
        /// <param name="startTime">Time in milliseconds.</param>
        public Timer(int startTime)
        {
            StartTime = startTime;
            InitialTime = GetSystemTimeMS();
            Registry = new Dictionary<string, TimedFunction>();
        }

        public Timer()
            : this(0)
        {
        }

        #endregion

        #region Properties

        private static int Frequency { get; set; }
        private int StartTime { get; set; }
        private int InitialTime { get; set; }
        private Dictionary<string, TimedFunction> Registry { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Add a timed event to this timer's register.
        /// </summary>
        /// <param name="key">Name used to identify this event.</param>
        /// <param name="timeUntilCall">Time in milliseconds until the function is called.</param>
        /// <param name="handler">The function to call for this event.</param>
        public void AddTimedEvent(string key, int timeUntilCall, TimedFunctionHandler handler)
        {
            if (Registry.ContainsKey(key))
            {
                throw new ArgumentException("Timer registery already contains event: " + key, "key");
            }
            TimedFunction tf = new TimedFunction();
            tf.Name = key;
            tf.Handler = handler;
            tf.TimeUntilCall = timeUntilCall;
            tf.CallTime = GetTimeMS() + timeUntilCall;
            Registry.Add(key, tf);
        }

        /// <summary>
        /// Add a timed event to this timer's register.
        /// </summary>
        /// <param name="key">Name used to identify this event.</param>
        /// <param name="callTime">The time at which to call this function for the first time.</param>
        /// <param name="timeUntilCall">Time in milliseconds until the function is called.</param>
        /// <param name="handler">The function to call for this event.</param>
        public void AddTimedEvent(string key, int callTime, int timeUntilCall, TimedFunctionHandler handler)
        {
            if (Registry.ContainsKey(key))
            {
                throw new ArgumentException("Timer registery already contains event: " + key, "key");
            }
            TimedFunction tf = new TimedFunction();
            tf.Name = key;
            tf.Handler = handler;
            tf.TimeUntilCall = timeUntilCall;
            tf.CallTime = callTime; ;
            Registry.Add(key, tf);
        }

        public TimedFunction GetTimedFunctionInfo(string key)
        {
            if (Registry.ContainsKey(key))
            {
                return (TimedFunction)Registry[key];
            }
            return null;
        }

        /// <summary>
        /// Check the timed event registry, and call any functions whose time is up.
        /// </summary>
        public void HandleTimedEvents()
        {
            foreach (TimedFunction tf in Registry.Values)
            {
                if (tf.Handler != null)
                {
                    if (GetTimeMS() >= tf.CallTime)
                    {
                        if (tf.Handler())
                        {
                            tf.CallTime += tf.TimeUntilCall;
                        }
                        else
                        {
                            Registry.Remove(tf.Name);
                        }
                    }
                }
            }
        }

        public void Reset(int startTime)
        {
            StartTime = startTime;
            InitialTime = GetSystemTimeMS();
        }

        public void Reset()
        {
            Reset(0);
        }

        /// <summary>
        /// Convert seconds to milliseconds.
        /// </summary>
        public static int SToMS(int s)
        {
            return s * 1000;
        }

        /// <summary>
        /// Convert minutes to milliseconds.
        /// </summary>
        public static int MToMS(int m)
        {
            return SToMS(m * 60);
        }

        public static int GetSystemTimeMS()
        {
            long count;
            Kernel32.QueryPerformanceCounter(out count);
            return (int)(count / Frequency);
        }

        public static int GetSystemTimeS()
        {
            return GetSystemTimeMS() / 1000;
        }

        /// <summary>
        /// Get the current system time in milliseconds.
        /// </summary>
        public int GetTimeMS()
        {
            long count;
            Kernel32.QueryPerformanceCounter(out count);
            return StartTime + (GetSystemTimeMS() - InitialTime);
        }

        /// <summary>
        /// Get the current system time in seconds.
        /// </summary>
        public int GetTimeS()
        {
            return GetTimeMS() / 1000;
        }

        /// <summary>
        /// Get the current system time in minutes.
        /// </summary>
        public int GetTimeM()
        {
            return GetTimeS() / 60;
        }

        /// <summary>
        /// Get the current system time in hours.
        /// </summary>
        public int GetTimeH()
        {
            return GetTimeM() / 60;
        }

        /// <summary>
        /// Get the current system time in days.
        /// </summary>
        public int GetTimeD()
        {
            return GetTimeH() / 24;
        }

        /// <summary>
        /// Get the current system time in years.
        /// </summary>
        public int GetTimeY()
        {
            return GetTimeD() / 365;
        }

        public override string ToString()
        {
            return GetTimeH() + ":" + (GetTimeM() % 60).ToString().PadLeft(2, '0') + ":" + (GetTimeS() % 60).ToString().PadLeft(2, '0');
        }

        public static string GetTimeStamp()
        {
            string hour = DateTime.Now.Hour.ToString();
            string minute = (DateTime.Now.Minute % 60).ToString().PadLeft(2, '0');
            string second = (DateTime.Now.Second % 60).ToString().PadLeft(2, '0');
            return hour + ":" + minute + ":" + second;
        }

        public static string GetDateStamp()
        {
            string year = DateTime.Now.Year.ToString().PadLeft(2, '0');
            string month = DateTime.Now.Month.ToString().PadLeft(2, '0');
            string day = DateTime.Now.Day.ToString().PadLeft(2, '0');
            return year + "." + month + "." + day;
        }

        #endregion
    }
}