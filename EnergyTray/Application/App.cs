using EnergyTray.UI;

namespace EnergyTray.Application
{
    public class App : IApp
    {
        private readonly IProcessIcon _processIcon;

        public App(IProcessIcon processIcon)
        {
            _processIcon = processIcon;
            GetPowerSchemes();
        }

        private void GetPowerSchemes()
        {
            _processIcon.InitializeIcon();
        }

        public void Dispose()
        {
            _processIcon?.Dispose();
        }
    }
}