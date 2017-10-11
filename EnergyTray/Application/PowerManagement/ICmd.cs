using System.Diagnostics;

namespace EnergyTray.Application.PowerManagement
{
    public interface ICmd
    {
        void ExecCommand(string command, DataReceivedEventHandler callback = null);
    }
}