using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using EnergyTray;
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
    }
}