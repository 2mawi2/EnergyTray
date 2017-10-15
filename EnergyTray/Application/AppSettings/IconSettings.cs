using EnergyTray.Application.PowerManagement;

namespace EnergyTray.Application.AppSettings
{
    public class IconSettings : IIconSettings
    {
        public string GetIconById(string powerSchemeId)
        {
            var settings = EnergyTraySetttings.Load();
            settings.PowerSchemeIconMap.TryGetValue(powerSchemeId, out var file);
            return file;
        }

        public void SaveIcon(string powerSchemeId, string iconFileLocation)
        {
            var settings = EnergyTraySetttings.Load();
            settings.PowerSchemeIconMap[powerSchemeId] = iconFileLocation;
            settings.Save();
        }
    }
}