using System.IO;
using Newtonsoft.Json;

namespace EnergyTray.Application.AppSettings
{
    public class AppSettings<T> where T : new()
    {
        public void Save(string fileName = Global.DefaultFilename)
        {
            var jset = new JsonSerializerSettings() {TypeNameHandling = TypeNameHandling.All};
            File.WriteAllText(fileName, JsonConvert.SerializeObject(this, jset));
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