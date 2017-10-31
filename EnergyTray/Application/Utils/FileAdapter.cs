using System.IO;

namespace EnergyTray.Application.Utils
{
    public class FileAdapter : IFileAdapter
    {
        public void WriteAllText(string fileName, string contents)
        {
            File.WriteAllText(fileName, contents);
        }

        public bool Exists(string fileName) => File.Exists(fileName);
        public string ReadAllText(string fileName) => File.ReadAllText(fileName);
    }
}