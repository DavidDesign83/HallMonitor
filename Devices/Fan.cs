using HallMonitor.Core;

namespace HallMonitor.Devices
{
    public class Fan : Device
    {
        public int Rpm { get; set; }

        public Fan(string name, int rpm = 1430) : base(name)
        {
            Rpm = rpm;
        }

        public override string GetStatus()
        {
            string state = IsRunning ? $"running, {Rpm} rpm" : "stopped";
            return $"{Name}: {state}";
        }
    }
}