using System.Diagnostics;

namespace EnergyTray.Application.PowerManagement
{
    public interface IPowerProcessor
    {
        void SwitchScheme(string powerSchemeId);
        void OpenOptions();
        void GetActivePowerScheme(DataReceivedEventHandler handler);
        void GetAllPowerSchemes(DataReceivedEventHandler handler);
    }
}