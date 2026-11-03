using System;
using HallMonitor.Core;

namespace HallMonitor.Devices
{
    public class TemperatureSensor : Sensor
    {
        public TemperatureSensor(string name, double limit) : base(name, limit)
        {
        }

        protected override double MeasureValue()
        {
            return 20 + Random.Shared.NextDouble() * 7;
        }

        public override string GetStatus()
        {
            string state = IsRunning ? "measuring" : "stopped";
            return $"{Name}: {state}";
        }
    }
}