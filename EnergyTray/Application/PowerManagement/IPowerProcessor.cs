using System;
using System.Collections.Generic;
using System.Diagnostics;
using EnergyTray.Application.Model;

namespace EnergyTray.Application.PowerManagement
{
    public interface IPowerProcessor
    {
        string SwitchScheme(string powerSchemeId);
        string OpenOptions();
        PowerScheme GetActivePowerScheme();
        IEnumerable<PowerScheme> GetAllPowerSchemes();
        EventHandler OnPowerSchemeChange { get; set; }
    }
}