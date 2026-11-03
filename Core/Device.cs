namespace HallMonitor.Core
{
    public abstract class Device
    {
        public string Name { get; set; }
        public bool IsRunning { get; protected set; }

        protected Device(string name)
        {
            Name = name;
            IsRunning = false;
        }

        public virtual void Start()
        {
            IsRunning = true;
        }

        public virtual void Stop()
        {
            IsRunning = false;
        }

        public abstract string GetStatus();
    }
}