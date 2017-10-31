using System.Collections.Generic;
using EnergyTray.Application.Model;
using EnergyTray.Worker;

namespace EnergyTray.Application.AppSettings.Provider
{
    public interface IEnergyTraySettings
    {
        bool IsAutoChangerEnabled { get; set; }
        PowerScheme PowerMode { get; set; }
        void Save(string fileName = Global.DefaultFilename);
        EnergyTraySettings Load(string fileName = Global.DefaultFilename);
    }
}