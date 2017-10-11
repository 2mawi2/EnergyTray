using System;

namespace EnergyTray.Worker
{
    public interface IMonitorCheckWorker
    {
        bool AutoEnabled { get; set; }
        EventHandler OnAutoChanged { get; set; }
    }
}