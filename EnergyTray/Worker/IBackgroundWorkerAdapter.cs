using System.ComponentModel;

namespace EnergyTray.Worker
{
    public interface IBackgroundWorkerAdapter
    {
        bool WorkerReportsProgress { get; set; }
        bool WorkerSupportsCancellation { get; set; }
        event DoWorkEventHandler DoWork;
        event RunWorkerCompletedEventHandler RunWorkerCompleted;
        void RunWorkerAsync();
    }
}