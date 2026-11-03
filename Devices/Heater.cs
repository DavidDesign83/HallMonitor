using HallMonitor.Core;

namespace HallMonitor.Devices
{
    public class Heater : Device
    {
        public double PowerKw { get; set; }

        public Heater(string name, double powerKw = 2.5) : base(name)
        {
            PowerKw = powerKw;
        }

        public override string GetStatus()
        {
            string state = IsRunning ? $"heating, {PowerKw} kW" : "stopped";
            return $"{Name}: {state}";
        }
    }
}