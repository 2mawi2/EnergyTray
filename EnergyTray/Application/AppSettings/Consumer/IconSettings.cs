using EnergyTray.Application.AppSettings.Provider;

namespace EnergyTray.Application.AppSettings.Consumer
{
    public class IconSettings : IIconSettings
    {
        private readonly IEnergyTraySettings _settings;

        public IconSettings(IEnergyTraySettings settings)
        {
            _settings = settings;
        }

        public string GetIconById(string powerSchemeId)
        {
            var settings = _settings.Load();
            settings.PowerSchemeIconMap.TryGetValue(powerSchemeId, out var file);
            return file;
        }

        public void SaveIcon(string powerSchemeId, string iconFileLocation)
        {
            var settings = _settings.Load();
            settings.PowerSchemeIconMap[powerSchemeId] = iconFileLocation;
            settings.Save();
        }
    }
}