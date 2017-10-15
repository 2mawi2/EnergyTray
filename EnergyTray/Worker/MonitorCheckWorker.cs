using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using EnergyTray.Application;
using EnergyTray.Application.AppSettings;
using EnergyTray.Application.PowerManagement;
using EnergyTray.UI;

namespace EnergyTray.Worker
{
    public class MonitorCheckWorker : IMonitorCheckWorker
    {
        private readonly IPowerProcessor _powerProcessor;
        private readonly IWorkerSettings _workerSettings;
        private readonly BackgroundWorker _bw = new BackgroundWorker();

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

        public MonitorCheckWorker(IPowerProcessor powerProcessor, IWorkerSettings workerSettings)
        {
            _powerProcessor = powerProcessor;
            _workerSettings = workerSettings;
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
                    if (IsExternalMonitorSetup() && IsPowerPluggedIn())
                    {
                        _powerProcessor.SwitchScheme(Global.Powermode);
                    }
                    else
                    {
                        _powerProcessor.SwitchScheme(_powerProcessor.GetActivePowerScheme().Id);
                    }
                }
                System.Threading.Thread.Sleep(4000);
            }
        }

        private static bool IsPowerPluggedIn()
        {
            return SystemInformation.PowerStatus.PowerLineStatus ==
                   PowerLineStatus.Online;
        }

        private static bool IsExternalMonitorSetup()
        {
            return Screen.AllScreens.Length > 1;
        }

        private void RestartWorkerOnCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _bw.RunWorkerAsync();
        }
    }
}