using System.Collections.Generic;
using EnergyTray.Application.Model;
using EnergyTray.Worker;

namespace EnergyTray.Application.AppSettings
{
    public interface IWorkerSettings
    {
        bool IsAutoChangerEnabled { get; set; }
        PowerScheme PowerMode { get; set; }
        void SetConditions(IEnumerable<Condition> conditions);
        IEnumerable<Condition> GetConditions();
    }
}