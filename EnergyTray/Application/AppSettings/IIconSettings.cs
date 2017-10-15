namespace EnergyTray.Application.AppSettings
{
    public interface IIconSettings
    {
        string GetIconById(string powerSchemeId);
        void SaveIcon(string powerSchemeId, string iconFileLocation);
    }
}