namespace EnergyTray.Worker
{
    public interface IMonitorCheckWorker
    {
        bool AutoEnabled { get; set; }
        void ToggleAutoEnabled();
    }
}