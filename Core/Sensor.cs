using System;
using System.Timers;

namespace HallMonitor.Core
{
    public class Reading
    {
        public Sensor Sensor { get; set; }
        public double Value { get; set; }
        public DateTime Time { get; set; }

        public Reading(Sensor sensor, double value, DateTime time)
        {
            Sensor = sensor;
            Value = value;
            Time = time;
        }
    }

    public abstract class Sensor : Device
    {
        public double Limit { get; set; }
        
        public event EventHandler<Reading>? LimitExceeded;

        private System.Timers.Timer _timer;

        protected Sensor(string name, double limit) : base(name)
        {
            Limit = limit;
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += OnTick;
        }

        public override void Start()
        {
            base.Start();
            _timer.Start();
        }

        public override void Stop()
        {
            base.Stop();
            _timer.Stop();
        }
    
    private void OnTick(object? sender, ElapsedEventArgs e)
    {
        if (!IsRunning) return;

        double val = MeasureValue();
        DateTime now = DateTime.Now;

        if (val > Limit)
        {
          Reading reading = new Reading(this, val, now);
          LimitExceeded?.Invoke(this, reading);
        }
    }

        protected abstract double MeasureValue();
    }
}