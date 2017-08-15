using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using EnergyTray.UI;

namespace EnergyTray.Worker
{
    internal class ProcessWorker
    {
        private readonly BackgroundWorker _bw = new BackgroundWorker();

        public ProcessWorker()
        {
            _bw.WorkerReportsProgress = true;
            _bw.WorkerSupportsCancellation = true;
            _bw.DoWork += bw_DoWork;
            _bw.RunWorkerCompleted += RestartWorkerOnCompleted;
            _bw.RunWorkerAsync();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;

            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                
                var processRecommendation = CheckProcesses();
                //Check CPU State and wait for specific amount of time


                //wait for 4 seconds
                System.Threading.Thread.Sleep(4000);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>shall power mode be activated</returns>
        private bool CheckProcesses()
        {
            var isDevelopment = Process.GetProcesses().ToList().Any(IsDevelopmentProcess);
            var isCharging = SystemInformation.PowerStatus.BatteryChargeStatus == BatteryChargeStatus.Charging;
            return isCharging && isDevelopment;
        }

        private static bool IsDevelopmentProcess(Process i) => i.ProcessName.Contains("devenv")
                                                               || i.ProcessName.Contains("Unity")
                                                               || i.ProcessName.Contains("Photoshop")
                                                               || i.ProcessName.Contains("vmware-vmx")
                                                               || i.ProcessName.Contains("idea");

        /// <summary>
        /// Checks if CPU state triggers energy mode change
        /// </summary>
        public ProcessIcon.CurrentMode CheckCpuState()
        {
            throw new NotImplementedException();
            return ProcessIcon.CurrentMode.Download;
        }

        private void RestartWorkerOnCompleted(object sender, RunWorkerCompletedEventArgs e) => _bw.RunWorkerAsync();
    }
}