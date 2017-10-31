using System.Collections.Generic;
using EnergyTray.Application.AppSettings.Provider;
using EnergyTray.Application.Model;
using EnergyTray.Worker;

namespace EnergyTray.Application.AppSettings.Consumer
{
    public class WorkerSettings : IWorkerSettings
    {
        private readonly IEnergyTraySettings _settings;

        public WorkerSettings(IEnergyTraySettings settings)
        {
            _settings = settings;
        }

        public PowerScheme PowerMode
        {
            get => _settings.Load().PowerMode;
            set
            {
                var settings = _settings.Load();
                settings.PowerMode = value;
                settings.Save();
            }
        }

        public bool IsAutoChangerEnabled
        {
            get => _settings.Load().IsAutoChangerEnabled;
            set
            {
                var settings = _settings.Load();
                settings.IsAutoChangerEnabled = value;
                settings.Save();
            }
        }
    }
}