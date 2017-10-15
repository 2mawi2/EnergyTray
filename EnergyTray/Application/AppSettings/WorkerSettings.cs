using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EnergyTray.Application.Model;
using EnergyTray.Application.PowerManagement;
using EnergyTray.Worker;

namespace EnergyTray.Application.AppSettings
{
    public class WorkerSettings : IWorkerSettings
    {
        public void SetConditions(IEnumerable<Condition> conditions)
        {
            var settings = EnergyTraySetttings.Load();
            settings.Conditions.AddRange(conditions);
            settings.Save();
        }

        public IEnumerable<Condition> GetConditions()
        {
            return EnergyTraySetttings.Load().Conditions;
        }

        public PowerScheme PowerMode
        {
            get => EnergyTraySetttings.Load().PowerMode;
            set
            {
                var settings = EnergyTraySetttings.Load();
                settings.PowerMode = value;
                settings.Save();
            }
        }

        public bool IsAutoChangerEnabled
        {
            get => EnergyTraySetttings.Load().IsAutoChangerEnabled;
            set
            {
                var settings = EnergyTraySetttings.Load();
                settings.IsAutoChangerEnabled = value;
                settings.Save();
            }
        }
    }
}