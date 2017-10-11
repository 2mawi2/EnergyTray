using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using EnergyTray.Application;
using EnergyTray.UI;

namespace EnergyTray.Worker
{
    public class MonitorCheckWorker
    {
        private readonly EventHandler _powerModeEventHandler;
        private readonly EventHandler _dellModeEventHandler;
        private readonly ICmd _cmd;
        private readonly BackgroundWorker _bw = new BackgroundWorker();
        public bool AutoEnabled = true;

        public MonitorCheckWorker(EventHandler powerModeEventHandler, EventHandler dellModeEventHandler, ICmd cmd)
        {
            _powerModeEventHandler = powerModeEventHandler;
            _dellModeEventHandler = dellModeEventHandler;
            _cmd = cmd;
            _bw.WorkerReportsProgress = true;
            _bw.WorkerSupportsCancellation = true;
            _bw.DoWork += bw_DoWork;
            _bw.RunWorkerCompleted += RestartWorkerOnCompleted;
            _bw.RunWorkerAsync();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;

            if (worker != null && worker.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                if (AutoEnabled)
                {
                    if (IsExternalMonitorSetup() && IsPowerPluggedIn())
                    {
                        _powerModeEventHandler.Invoke(this, new EventArgs());
                    }
                    else
                    {
                        _cmd.ExecCommand("powercfg.exe /getactivescheme", SwitchToDellModeHandler);
                    }
                }
                System.Threading.Thread.Sleep(4000);
            }
        }

        private void SwitchToDellModeHandler(object sender, DataReceivedEventArgs e)
        {
            var outputLine = e.Data;
            if (!string.IsNullOrEmpty(outputLine))
            {
                if (outputLine.Contains("Power Scheme GUID"))
                {
                    outputLine = outputLine.Replace("Power Scheme GUID: ", "");
                    outputLine = outputLine.Remove(36, outputLine.ToCharArray().Length - 36);
                    if (outputLine == Global.Powermode)
                    {
                        _dellModeEventHandler.Invoke(this, new EventArgs());
                    }
                }
            }
        }

        private static bool IsPowerPluggedIn() => SystemInformation.PowerStatus.PowerLineStatus ==
                                                  PowerLineStatus.Online;

        private static bool IsExternalMonitorSetup() => Screen.AllScreens.Length > 1;

        private void RestartWorkerOnCompleted(object sender, RunWorkerCompletedEventArgs e) => _bw.RunWorkerAsync();
    }
}