using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EnergyTray.Application.AppSettings.Consumer;
using EnergyTray.Application.Model;
using EnergyTray.Application.PowerManagement;
using EnergyTray.UI;
using EnergyTray.Worker;
using Moq;
using Xunit;
using ContextMenu = EnergyTray.UI.ContextMenu;

namespace EnergyTrayTests.UI
{
    public class ContextMenuTests
    {
        private Mock<IMonitorCheckWorker> _monitorCheckWorker = new Mock<IMonitorCheckWorker>();
        private Mock<IPowerProcessor> _powerProcessor = new Mock<IPowerProcessor>();
        private Mock<IIconSettings> _iconSettings = new Mock<IIconSettings>();
        private Mock<IWorkerSettings> _workerSettings = new Mock<IWorkerSettings>();

        public IContextMenu CreateContextMenu()
        {
            return new ContextMenu(
                //_monitorCheckWorker.Object, 
                _powerProcessor.Object,
                _iconSettings.Object,
                _workerSettings.Object);
        }

        private static IEnumerable<PowerScheme> CreateTestSchemes()
        {
            return new List<PowerScheme>
            {
                new PowerScheme
                {
                    Id = "Id",
                    IsActive = true,
                    Name = "Name"
                },
                new PowerScheme
                {
                    Id = "Id2",
                    IsActive = false,
                    Name = "Name2"
                },
            };
        }

        [Theory]
        [InlineData("Exit")]
        [InlineData("Power Options")]
        [InlineData("Automatic Mode")]
        [InlineData("Name")]
        [InlineData("Name2")]
        [InlineData("Select Icons")]
        public void CreateTest_ContainsAllExpectedItems(string expectedItemText)
        {
            _powerProcessor.Setup(i => i.GetAllPowerSchemes()).Returns(CreateTestSchemes());

            var result = CreateContextMenu().Create();

            Assert.True(result.Items.Cast<ToolStripItem>().ToArray().Any(i => i.Text == expectedItemText));
        }
    }
}