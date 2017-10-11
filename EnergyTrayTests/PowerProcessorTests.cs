using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using EnergyTray;
using EnergyTray.Application.PowerManagement;
using EnergyTray.Worker;
using Moq;
using Ploeh.AutoFixture.Kernel;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace EnergyTrayTests
{
    public class PowerProcessorTests
    {
        [Theory, AutoData]
        public void SwitchSchemeTest(string schemeId)
        {
            var cmd = new Mock<ICmd>();
            var processor = new PowerProcessor(cmd.Object);

            processor.SwitchScheme(schemeId);

            cmd.Verify(i => i.ExecCommand(
                It.Is<string>(j => j == $"powercfg.exe /s {schemeId}"),
                It.IsAny<DataReceivedEventHandler>()));
        }

        [Theory, AutoData]
        public void OpenOptionsTest()
        {
            var cmd = new Mock<ICmd>();
            var processor = new PowerProcessor(cmd.Object);

            processor.OpenOptions();

            cmd.Verify(i => i.ExecCommand(
                It.Is<string>(j => j == @"%windir%\system32\control.exe /name Microsoft.PowerOptions /page"),
                It.IsAny<DataReceivedEventHandler>()));
        }

        [Fact]
        public void GetPowerSchemeTest()
        {
            var cmd = new Mock<ICmd>();
            var processor = new PowerProcessor(cmd.Object);
            var testHandler = new DataReceivedEventHandler(delegate { });
            processor.GetPowerScheme(testHandler);

            cmd.Verify(i => i.ExecCommand(
                It.Is<string>(j => j == @"powercfg.exe /getactivescheme"),
                It.Is<DataReceivedEventHandler>(j => j == testHandler)));
        }
    }
}