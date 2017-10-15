using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EnergyTray.Application.Model;
using EnergyTray.Worker;

namespace EnergyTray.Application.AppSettings
{
    public class EnergyTraySetttings : AppSettings<EnergyTraySetttings>
    {
        public readonly Dictionary<string, string> PowerSchemeIconMap = new Dictionary<string, string>();
        public bool IsAutoChangerEnabled { get; set; }
        public List<Condition> Conditions { get; set; } = new List<Condition>();
        public PowerScheme PowerMode { get; set; }
    }
}