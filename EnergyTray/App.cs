using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using EnergyTray.Application;
using EnergyTray.Application.Model;
using EnergyTray.Application.PowerManagement;
using EnergyTray.Application.Utils;
using EnergyTray.Properties;
using EnergyTray.UI;

namespace EnergyTray
{
    public class App : IApp
    {
        private readonly IProcessIcon _processIcon;
        private readonly IPowerProcessor _powerProcessor;

        public App(IProcessIcon processIcon, IPowerProcessor powerProcessor)
        {
            _processIcon = processIcon;
            _powerProcessor = powerProcessor;
            GetPowerSchemes();
        }

        private void GetPowerSchemes()
        {
            var powerSchemes = StringUtils.GetAllSchemes(_powerProcessor.GetAllPowerSchemes());
            _processIcon.InitializeIcon(powerSchemes);
        }

        public void Dispose()
        {
            _processIcon?.Dispose();
        }
    }
}