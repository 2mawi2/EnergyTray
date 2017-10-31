namespace EnergyTray.Application.AppSettings.Provider
{
    public interface IAppSettings<out T>
    {
        void Save(string fileName = Global.DefaultFilename);
        T Load(string fileName = Global.DefaultFilename);
    }
}