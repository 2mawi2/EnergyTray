using System;
using EnergyTray.Application.Utils;
using Newtonsoft.Json;

namespace EnergyTray.Application.AppSettings.Provider
{
    public class AppSettings<T> : IAppSettings<T>
    {
        private readonly IFileDelegate _file;

        public AppSettings(IFileDelegate file)
        {
            _file = file;
        }

        public void Save(string fileName = Global.DefaultFilename)
        {
            var jset = new JsonSerializerSettings() {TypeNameHandling = TypeNameHandling.All};
            _file.WriteAllText(fileName, JsonConvert.SerializeObject(this, jset));
        }

        public T Load(string fileName = Global.DefaultFilename)
        {
            var settings = (T) Activator.CreateInstance(typeof(T), _file);
            if (_file.Exists(fileName))
            {
                settings = JsonConvert.DeserializeObject<T>(_file.ReadAllText(fileName));
            }
            return settings;
        }
    }
}