﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EnergyTray.Application.Model;
using EnergyTray.Application.Utils;

namespace EnergyTray.Application.PowerManagement
{
    public class PowerProcessor : IPowerProcessor
    {
        private readonly ICmd _cmd;

        public PowerProcessor(ICmd cmd)
        {
            _cmd = cmd;
        }

        public string SwitchScheme(string powerSchemeId)
        {
            var result = _cmd.ExecCommand($"powercfg.exe /s {powerSchemeId}");
            OnPowerSchemeChange?.Invoke(null, EventArgs.Empty);
            return result;
        }

        public string OpenOptions()
        {
            return _cmd.ExecCommand(@"%windir%\system32\control.exe /name Microsoft.PowerOptions /page");
        }

        public PowerScheme GetActivePowerScheme() => GetAllPowerSchemes().Single(i => i.IsActive);

        public IEnumerable<PowerScheme> GetAllPowerSchemes()
        {
            return StringUtils.GetAllSchemes(_cmd.ExecCommand(@"powercfg.exe /list"));
        }

        public EventHandler OnPowerSchemeChange { get; set; } = null;
    }
}