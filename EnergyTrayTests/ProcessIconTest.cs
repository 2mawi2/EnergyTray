using EnergyTray.Application.AppSettings;
using EnergyTray.Application.Model;
using EnergyTray.Application.PowerManagement;
using EnergyTray.UI;
using EnergyTray.Worker;
using Moq;
using Xunit;

namespace EnergyTrayTests
{
    public class ProcessIconTest
    {
        private readonly Mock<IMonitorCheckWorker> _monitorCheckWorker = new Mock<IMonitorCheckWorker>();
        private readonly Mock<IContextMenu> _contextMenu = new Mock<IContextMenu>();
        private readonly Mock<IPowerProcessor> _powerProcessor = new Mock<IPowerProcessor>();
        private readonly Mock<IIconSettings> _iconSettings = new Mock<IIconSettings>();

        public ProcessIconTest()
        {
            _powerProcessor.Setup(i => i.GetActivePowerScheme())
                           .Returns(new PowerScheme {Id = "Id", IsActive = true, Name = "Name"});
            _powerProcessor.SetupAllProperties();
            _monitorCheckWorker.SetupAllProperties();

            CreateProcessIcon().InitializeIcon();
        }

        private IProcessIcon CreateProcessIcon()
        {
            return new ProcessIcon(
                _monitorCheckWorker.Object,
                _contextMenu.Object,
                _powerProcessor.Object,
                _iconSettings.Object);
        }

        [Fact]
        public void InitializeIconTest_UpdateGetsActivePowerScheme()
        {
            _powerProcessor.Verify(i => i.GetActivePowerScheme());
        }

        [Fact]
        public void InitializeIconTest_UpdateIconTextChecksForAutoEnabled()
        {
            _monitorCheckWorker.VerifyGet(i => i.AutoEnabled);
        }

        [Fact]
        public void InitializeIconTest_UpdateIconImageGetsIconById()
        {
            _iconSettings.Verify(i => i.GetIconById("Id"));
        }

        [Fact]
        public void InitializeIconTest_ContextMenuCreate()
        {
            _contextMenu.Verify(i => i.Create());
        }
    }
}