using System.Diagnostics;

namespace EnergyTray.Application.PowerManagement
{
    public interface ICmd
    {
        string ExecCommand(string command);
    }
}