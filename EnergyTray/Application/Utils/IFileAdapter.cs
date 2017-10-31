namespace EnergyTray.Application.Utils
{
    public interface IFileAdapter
    {
        void WriteAllText(string fileName, string contents);
        bool Exists(string fileName);
        string ReadAllText(string fileName);
    }
}