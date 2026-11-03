using System;
using System.Collections.Generic;

namespace HallMonitor.Core
{
    public class AlarmRecord
    {
        public string Message { get; set; }
        public DateTime Time { get; set; }

        public AlarmRecord(string message, DateTime time)
        {
            Message = message;
            Time = time;
        }

        public override string ToString()
        {
            return $"{Time:HH:mm:ss} {Message}";
        }
    }

    public class Hall
    {
        public List<Device> Devices { get; private set; }
        public List<AlarmRecord> AlarmHistory { get; private set; }
        public event EventHandler<AlarmRecord>? AlarmRaised;

        public Hall()
        {
            Devices = new List<Device>();
            AlarmHistory = new List<AlarmRecord>();
        }

        public void AddDevice(Device device)
        {
            Devices.Add(device);
            if (device is Sensor sensor)
            {
                sensor.LimitExceeded += OnSensorLimitExceeded;
            }
        }

        private void OnSensorLimitExceeded(object? sender, Reading reading)
        {
            string msg = $"{reading.Sensor.Name} {reading.Value:0.0} C, limit {reading.Sensor.Limit} C";
            AlarmRecord record = new AlarmRecord(msg, reading.Time);
    
            AlarmHistory.Add(record);
            AlarmRaised?.Invoke(this, record);
        }

        public void StartAll()
        {
            foreach (var d in Devices)
            {
                d.Start();
            }
        }

        public void StopAll()
        {
            foreach (var d in Devices)
            {
                d.Stop();
            }
        }
    }
}