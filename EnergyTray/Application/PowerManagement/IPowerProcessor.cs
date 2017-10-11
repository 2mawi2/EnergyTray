using System.Diagnostics;

namespace EnergyTray.Application.PowerManagement
{
    public interface IPowerProcessor
    {
        string SwitchScheme(string powerSchemeId);
        string OpenOptions();
        string GetActivePowerScheme();
        string GetAllPowerSchemes();
    }
}