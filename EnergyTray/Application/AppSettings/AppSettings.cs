using System.IO;
using Newtonsoft.Json;

namespace EnergyTray.Application.AppSettings
{
    public class AppSettings<T> where T : new()
    {
        public void Save(string fileName = Global.DefaultFilename)
        {
            File.WriteAllText(fileName, JsonConvert.SerializeObject(this));
        }

        public static void Save(T settings, string fileName = Global.DefaultFilename)
        {
            File.WriteAllText(fileName, JsonConvert.SerializeObject(settings));
        }

        public static T Load(string fileName = Global.DefaultFilename)
        {
            var settings = new T();
            if (File.Exists(fileName))
            {
                settings = JsonConvert.DeserializeObject<T>(File.ReadAllText(fileName));
            }
            return settings;
        }
    }
}