using System.Collections.Generic;
using EnergyTray.Application.Model;
using EnergyTray.Worker;

namespace EnergyTray.Application.AppSettings.Consumer
{
    public interface IWorkerSettings
    {
        bool IsAutoChangerEnabled { get; set; }
        PowerScheme PowerMode { get; set; }
    }
}