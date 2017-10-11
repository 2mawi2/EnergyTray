using System;
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

        public PowerScheme GetActivePowerScheme()
        {
            var scheme = GetAllPowerSchemes().SingleOrDefault(i => i.IsActive);
            if (scheme == null)
            {
                throw new EnergyTrayException("No Active power plan found");
            }
            return scheme;
        }

        public IEnumerable<PowerScheme> GetAllPowerSchemes()
        {
            var schemes = StringUtils.GetAllSchemes(_cmd.ExecCommand(@"powercfg.exe /list")).ToList();
            if (!schemes.Any())
            {
                throw new EnergyTrayException("No power plan found");
            }
            return schemes;
        }

        public EventHandler OnPowerSchemeChange { get; set; } = null;
    }
}