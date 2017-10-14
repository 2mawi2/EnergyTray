using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Windows.Forms;
using EnergyTray.Application.Model;
using EnergyTray.Application.PowerManagement;
using EnergyTray.UI;
using EnergyTray.Worker;
using Moq;
using Xunit;
using ContextMenu = EnergyTray.UI.ContextMenu;

namespace EnergyTrayTests
{
    public class ContextMenuTests
    {
        private Mock<IMonitorCheckWorker> _monitorCheckWorker = new Mock<IMonitorCheckWorker>();
        private Mock<IPowerProcessor> _powerProcessor = new Mock<IPowerProcessor>();

        public IContextMenu CreateContextMenu()
        {
            return new ContextMenu(_monitorCheckWorker.Object, _powerProcessor.Object);
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
        [InlineData("Options")]
        [InlineData("Automatic Mode")]
        [InlineData("Name")]
        [InlineData("Name2")]
        public void CreateTest_ContainsAllExpectedItems(string expectedItemText)
        {
            var schemes = CreateTestSchemes();

            var result = CreateContextMenu().Create(schemes);

            Assert.True(result.Items.Cast<ToolStripItem>().ToArray().Any(i => i.Text == expectedItemText));
        }
    }
}