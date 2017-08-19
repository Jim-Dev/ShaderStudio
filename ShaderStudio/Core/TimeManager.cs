using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaderStudio.Core
{
    public class TimeManager
    {
        private static object syncRoot = new Object();
        private static volatile TimeManager instance;
        public static TimeManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new TimeManager();
                    }
                }

                return instance;
            }
        }

        private Stopwatch globalStopwatch;

        public TimeManager()
        {
            globalStopwatch = new Stopwatch();
        }

        public void Start()
        {
            Instance.globalStopwatch.Start();
        }

        public float GetElapsedMilliseconds()
        {
            return Instance.globalStopwatch.ElapsedMilliseconds;
        }
        public float GetElapsedSeconds()
        {
            return Instance.globalStopwatch.ElapsedMilliseconds / 1000f;
        }

        public bool IsStarted
        {
            get { return globalStopwatch.IsRunning; }
        }

    }
}
