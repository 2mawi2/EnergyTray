using System.Collections.Generic;
using EnergyTray.Application.Model;
using EnergyTray.Application.Utils;
using EnergyTray.Worker;

namespace EnergyTray.Application.AppSettings.Provider
{
    public class EnergyTraySettings : AppSettings<EnergyTraySettings>, IEnergyTraySettings
    {
        public EnergyTraySettings(IFileAdapter file) : base(file)
        {
        }

        public readonly Dictionary<string, string> PowerSchemeIconMap = new Dictionary<string, string>();
        public bool IsAutoChangerEnabled { get; set; }
        public PowerScheme PowerMode { get; set; }
        public bool IsMonitorConditionEnabled { get; set; }
        public bool IsPowerConditionEnabled { get; set; }
    }
}