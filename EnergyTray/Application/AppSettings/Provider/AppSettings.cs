using System;
using EnergyTray.Application.Exceptions;
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
            var settings = TryCreateSettings();
            if (_file.Exists(fileName))
            {
                settings = JsonConvert.DeserializeObject<T>(_file.ReadAllText(fileName));
            }
            return settings;
        }

        private T TryCreateSettings()
        {
            T settings;
            try
            {
                settings = (T) Activator.CreateInstance(typeof(T), _file);
            }
            catch (Exception e)
            {
                throw new EnergyTrayException($"Generic object needs to inherit from {nameof(AppSettings)}", e);
            }
            return settings;
        }
    }
}