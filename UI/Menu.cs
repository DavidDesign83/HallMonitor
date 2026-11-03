using System;
using HallMonitor.Core;

namespace HallMonitor.UI
{
    public class Menu
    {
        private Hall _hall;

        public Menu(Hall hall)
        {
            _hall = hall;
            _hall.AlarmRaised += OnAlarmRaised;
        }

        private void OnAlarmRaised(object? sender, AlarmRecord record)
        {
            Console.WriteLine($"\n> !! ALARM {record}");
            PrintPrompt();
        }

        private void PrintPrompt()
        {
            Console.Write("> ");
        }

        public void Show()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("=== Hall monitor ===");
                Console.WriteLine("1 Devices");
                Console.WriteLine("2 Start or stop a device");
                Console.WriteLine("3 Alarm history");
                Console.WriteLine("0 Exit");
                PrintPrompt();

                string? input = Console.ReadLine();
                if (string.IsNullOrEmpty(input) || !int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Invalid input, try again.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        ShowDevices();
                        break;
                    case 2:
                        ToggleDevice();
                        break;
                    case 3:
                        ShowAlarmHistory();
                        break;
                    case 0:
                        running = false;
                        break;
                    default:
                        Console.WriteLine("no such option, again");
                        break;
                }
            }
        }

        private void ShowDevices()
        {
            for (int i = 0; i < _hall.Devices.Count; i++)
            {
                var dev = _hall.Devices[i];
                Console.WriteLine($"{i + 1} {dev.GetStatus()}");
            }
        }

        private void ToggleDevice()
        {
            ShowDevices();
            Console.Write("device: ");
            string? input = Console.ReadLine();
            
            if (int.TryParse(input, out int index) && index >= 1 && index <= _hall.Devices.Count)
            {
                var dev = _hall.Devices[index - 1];
                if (dev.IsRunning)
                {
                    dev.Stop();
                    Console.WriteLine($"{dev.Name}: stopped");
                }
                else
                {
                    dev.Start();
                    Console.WriteLine($"{dev.Name}: started");
                }
            }
            else
            {
                Console.WriteLine("no such device, again");
            }
        }

        private void ShowAlarmHistory()
    {
        Console.WriteLine($"Total alarms: {_hall.AlarmHistory.Count}");
    
        foreach (var alarm in _hall.AlarmHistory)
        {
            Console.WriteLine(alarm);
        }
    }

    }
}