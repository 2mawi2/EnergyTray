using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Windows.Forms;
using EnergyTray.Application;
using EnergyTray.Application.AppSettings;
using EnergyTray.Application.AppSettings.Consumer;
using EnergyTray.Application.Model;
using EnergyTray.Application.PowerManagement;
using EnergyTray.UI;
using StructureMap.Pipeline;

namespace EnergyTray.Worker
{
    public class MonitorCheckWorker : IMonitorCheckWorker
    {
        private readonly IPowerProcessor _powerProcessor;
        private readonly IWorkerSettings _workerSettings;
        private IBackgroundWorkerAdapter _bw;

        public MonitorCheckWorker()
        {
        }

        public bool AutoEnabled
        {
            get => _workerSettings.IsAutoChangerEnabled;
            set
            {
                _workerSettings.IsAutoChangerEnabled = value;
                OnAutoChanged.Invoke(null, EventArgs.Empty);
            }
        }

        public EventHandler OnAutoChanged { get; set; }

        public MonitorCheckWorker(
            IPowerProcessor powerProcessor,
            IWorkerSettings workerSettings,
            IBackgroundWorkerAdapter bw)
        {
            _powerProcessor = powerProcessor;
            _workerSettings = workerSettings;
            SetupBackgroundWorker(bw);
        }

        private void SetupBackgroundWorker(IBackgroundWorkerAdapter bw)
        {
            var powerMode = _powerProcessor.GetAllPowerSchemes()
                                           .SingleOrDefault(i => i.Name.Contains("Power"));

            if (powerMode == default(PowerScheme))
            {
                _workerSettings.IsAutoChangerEnabled = false;
                return;
            }

            _bw = bw;
            _bw.WorkerReportsProgress = true;
            _bw.WorkerSupportsCancellation = true;
            _bw.DoWork += Run;
            _bw.RunWorkerCompleted += RestartWorkerOnCompleted;
            _bw.RunWorkerAsync();
        }


        private void Run(object sender, DoWorkEventArgs e)
        {
            if (sender is BackgroundWorker worker && worker.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                if (AutoEnabled)
                {
                    var allConditionsTrue = true; // TODO get conditions from config and check

                    _powerProcessor.SwitchScheme(allConditionsTrue
                        ? _workerSettings.PowerMode.Id
                        : _powerProcessor.GetActivePowerScheme().Id);
                }
                System.Threading.Thread.Sleep(4000);
            }
        }

        private void RestartWorkerOnCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _bw.RunWorkerAsync();
        }
    }
}