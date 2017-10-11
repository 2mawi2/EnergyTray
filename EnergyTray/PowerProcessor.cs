using System;
using System.Diagnostics;
using System.Threading;
using EnergyTray.Application;
using EnergyTray.Worker;

namespace EnergyTray
{
    public class PowerProcessor : IPowerProcessor
    {
        private readonly ICmd _cmd;

        public PowerProcessor(ICmd cmd)
        {
            _cmd = cmd;
        }

        public void SwitchScheme(string powerSchemeId)
        {
            _cmd.ExecCommand($"powercfg.exe /s {powerSchemeId}");
        }
    }
}