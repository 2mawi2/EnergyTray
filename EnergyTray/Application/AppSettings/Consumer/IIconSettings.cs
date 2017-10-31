namespace EnergyTray.Application.AppSettings.Consumer
{
    public interface IIconSettings
    {
        string GetIconById(string powerSchemeId);
        void SaveIcon(string powerSchemeId, string iconFileLocation);
    }
}