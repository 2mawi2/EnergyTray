using System.Collections.Generic;
using EnergyTray.Application.AppSettings.Consumer;
using EnergyTray.Application.Model;
using EnergyTray.Application.PowerManagement;
using EnergyTray.Worker;
using Moq;
using Xunit;

namespace EnergyTrayTests.Worker
{
    public class MonitorCheckWorkerTests
    {
        private readonly Mock<IPowerProcessor> _powerProcessor = new Mock<IPowerProcessor>();
        private readonly Mock<IWorkerSettings> _workerSettings = new Mock<IWorkerSettings>();
        private readonly Mock<IBackgroundWorkerAdapter> _backgroundWorker = new Mock<IBackgroundWorkerAdapter>();

        public MonitorCheckWorkerTests()
        {
            _backgroundWorker.SetupAllProperties();
        }


        private IMonitorCheckWorker CreateMonitorCheckWorker() => new MonitorCheckWorker(
            _powerProcessor.Object,
            _workerSettings.Object,
            _backgroundWorker.Object);

        [Fact]
        public void MonitorCheckWorkerTest_SetupBackgroundWorker()
        {
            _powerProcessor.Setup(i => i.GetAllPowerSchemes()).Returns(new List<PowerScheme>
            {
                new PowerScheme {Name = "Power"}
            });

            CreateMonitorCheckWorker();

            _backgroundWorker.Verify(i => i.RunWorkerAsync());
            _backgroundWorker.VerifySet(i => i.WorkerReportsProgress = true);
            _backgroundWorker.VerifySet(i => i.WorkerSupportsCancellation = true);
        }

        [Fact]
        public void MonitorCheckWorkerTest_GetPowerSchemes()
        {
            _powerProcessor.Setup(i => i.GetAllPowerSchemes()).Returns(new List<PowerScheme>
            {
                new PowerScheme {Name = "Power"}
            });

            CreateMonitorCheckWorker();

            _powerProcessor.Verify(i => i.GetAllPowerSchemes());
        }

        [Fact]
        public void MonitorCheckWorkerTest_DoWorkEvent()
        {
            CreateMonitorCheckWorker();
        }
    }
}