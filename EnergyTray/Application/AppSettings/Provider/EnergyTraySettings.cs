using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using EnergyTray.Application.Exceptions;
using EnergyTray.Application.Model;
using EnergyTray.Worker;
using Newtonsoft.Json;

namespace EnergyTray.Application.AppSettings.Provider
{
    public class EnergyTraySettings : IEnergyTraySettings
    {
        #region settings

        public readonly Dictionary<string, string> PowerSchemeIconMap = new Dictionary<string, string>();
        public bool IsAutoChangerEnabled { get; set; }
        public PowerScheme PowerMode { get; set; }
        public bool IsMonitorConditionEnabled { get; set; }
        public bool IsPowerConditionEnabled { get; set; }

        #endregion

        #region Serialization

        private readonly object _fileLock = new object();

        public void Save(string fileName = Global.DefaultFilename)
        {
            var jset = new JsonSerializerSettings() {TypeNameHandling = TypeNameHandling.All};
            var settings = JsonConvert.SerializeObject(this, jset);

            lock (_fileLock)
            {
                try
                {
                    File.WriteAllText(fileName, settings);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }
        }

        public EnergyTraySettings Load(string fileName = Global.DefaultFilename)
        {
            lock (_fileLock)
            {
                try
                {
                    return !File.Exists(fileName)
                        ? new EnergyTraySettings()
                        : JsonConvert.DeserializeObject<EnergyTraySettings>(File.ReadAllText(fileName));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                    return new EnergyTraySettings();
                }
            }
        }

        #endregion
    }
}