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

        public void SwitchScheme(string powerSchemeId)
        {
            _cmd.ExecCommand($"powercfg.exe /s {powerSchemeId}");
        }

        public void OpenOptions()
        {
            _cmd.ExecCommand(@"%windir%\system32\control.exe /name Microsoft.PowerOptions /page");
        }

        public void GetPowerScheme(DataReceivedEventHandler handler)
        {
            _cmd.ExecCommand(@"powercfg.exe /getactivescheme", handler);
        }
    }
}