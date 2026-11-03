using System;
using HallMonitor.Core;
using HallMonitor.Devices;
using HallMonitor.UI;

namespace HallMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Hall hall = new Hall();

            hall.AddDevice(new TemperatureSensor("T-hall", 26.0));
            hall.AddDevice(new Fan("Fan-1", 1430));
            hall.AddDevice(new Heater("Heater-1", 2.5));

            hall.StartAll();

            Menu menu = new Menu(hall);
            menu.Show();

            hall.StopAll();
        }
    }
}
