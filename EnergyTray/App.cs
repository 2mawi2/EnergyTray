using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using EnergyTray.Application;
using EnergyTray.Application.Model;
using EnergyTray.Application.Utils;
using EnergyTray.Properties;
using EnergyTray.UI;

namespace EnergyTray
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