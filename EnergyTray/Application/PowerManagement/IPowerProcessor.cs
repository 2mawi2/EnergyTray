using System.Diagnostics;

namespace EnergyTray.Application.PowerManagement
{
    public interface IPowerProcessor
    {
        void SwitchScheme(string powerSchemeId);
        void OpenOptions();
        void GetPowerScheme(DataReceivedEventHandler handler);
    }
}