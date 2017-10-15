using System.ComponentModel;

namespace EnergyTray.Worker
{
    public class BackgroundWorkerAdapter : IBackgroundWorkerAdapter
    {
        private readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();
        private IBackgroundWorkerAdapter _backgroundWorkerAdapterImplementation;

        public bool WorkerReportsProgress
        {
            get => _backgroundWorker.WorkerReportsProgress;
            set => _backgroundWorker.WorkerReportsProgress = value;
        }

        public bool WorkerSupportsCancellation
        {
            get => _backgroundWorker.WorkerSupportsCancellation;
            set => _backgroundWorker.WorkerSupportsCancellation = value;
        }

        public bool CancellationPending { get; set; }

        public event DoWorkEventHandler DoWork
        {
            add => _backgroundWorker.DoWork += value;
            remove => _backgroundWorker.DoWork -= value;
        }

        public event RunWorkerCompletedEventHandler RunWorkerCompleted
        {
            add => _backgroundWorker.RunWorkerCompleted += value;
            remove => _backgroundWorker.RunWorkerCompleted -= value;
        }

        public void RunWorkerAsync()
        {
            _backgroundWorker.RunWorkerAsync();
        }
    }
}