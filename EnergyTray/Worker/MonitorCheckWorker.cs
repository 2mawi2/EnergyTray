using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using EnergyTray.Application;
using EnergyTray.Application.PowerManagement;
using EnergyTray.UI;

namespace EnergyTray.Worker
{
    public class MonitorCheckWorker : IMonitorCheckWorker
    {
        private readonly IPowerProcessor _powerProcessor;
        private readonly BackgroundWorker _bw = new BackgroundWorker();
        private bool _autoEnabled = true;

        public bool AutoEnabled { get; set; }

        public MonitorCheckWorker(IPowerProcessor powerProcessor)
        {
            _powerProcessor = powerProcessor;
            _bw.WorkerReportsProgress = true;
            _bw.WorkerSupportsCancellation = true;
            _bw.DoWork += bw_DoWork;
            _bw.RunWorkerCompleted += RestartWorkerOnCompleted;
            _bw.RunWorkerAsync();
        }


        private void bw_DoWork(object sender, DoWorkEventArgs e)
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
                        _powerProcessor.GetActivePowerScheme();
                    }
                }
                System.Threading.Thread.Sleep(4000);
            }
        }

        private static bool IsPowerPluggedIn() => SystemInformation.PowerStatus.PowerLineStatus ==
                                                  PowerLineStatus.Online;

        private static bool IsExternalMonitorSetup() => Screen.AllScreens.Length > 1;
        
        private void RestartWorkerOnCompleted(object sender, RunWorkerCompletedEventArgs e) => _bw.RunWorkerAsync();
    }
}