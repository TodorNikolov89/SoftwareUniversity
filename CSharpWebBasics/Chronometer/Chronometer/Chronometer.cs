﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Chronometer
{
    public class Chronometer : IChronometer
    {
        private long miliseconds = 0;
        private bool isRunning;

        public Chronometer()
        {
            this.Reset();
        }

        public string GetTime => $"{(miliseconds / 60000):D2}:{miliseconds / 1000:D2}.{miliseconds % 1000:D4}";

        public List<string> Laps { get; private set; }

        public string Lap()
        {
            var lap = this.GetTime;
            this.Laps.Add(lap);

            return lap;
        }

        public void Reset()
        {
            Stop();
            this.miliseconds = 0;
            this.Laps = new List<string>();
        }

        public void Start()
        {
            this.isRunning = true;
            Task.Run(() =>
            {
                while (this.isRunning)
                {
                    Thread.Sleep(1);
                    this.miliseconds++;
                }
            });

        }

        public void Stop()
        {
            isRunning = false;
        }
    }
}
