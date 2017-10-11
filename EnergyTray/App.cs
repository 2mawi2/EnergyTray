using System;
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
            _powerProcessor.GetAllPowerSchemes((sender, args) =>
            {
                var outputLine = args.Data;
                var powerSchemes = StringUtils.GetAllSchemes(outputLine);
                _processIcon.InitializeIcon(powerSchemes);
                _processIcon.Display();
            });
        }

        public void Dispose()
        {
            _processIcon?.Dispose();
        }
    }
}