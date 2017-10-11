using System.Diagnostics;

namespace EnergyTray.Worker
{
    public interface ICmd
    {
        void ExecCommand(string command, DataReceivedEventHandler callback = null);
    }
}