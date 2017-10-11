using System.Diagnostics;

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
            return _cmd.ExecCommand($"powercfg.exe /s {powerSchemeId}");
        }

        public string OpenOptions()
        {
            return _cmd.ExecCommand(@"%windir%\system32\control.exe /name Microsoft.PowerOptions /page");
        }

        public string GetActivePowerScheme()
        {
            return _cmd.ExecCommand(@"powercfg.exe /getactivescheme");
        }
        
        public string GetAllPowerSchemes()
        {
            return _cmd.ExecCommand(@"powercfg.exe /list");
        }
    }
}