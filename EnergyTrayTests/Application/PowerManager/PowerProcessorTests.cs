using AutoFixture.Xunit2;
using EnergyTray.Application.Exceptions;
using EnergyTray.Application.PowerManagement;
using Moq;
using Xunit;

namespace EnergyTrayTests.Application.PowerManager
{
    public class PowerProcessorTests
    {
        private readonly Mock<ICmd> _cmd = new Mock<ICmd>();


        [Theory, AutoData]
        public void SwitchSchemeTest(string schemeId)
        {
            var processor = new PowerProcessor(_cmd.Object);

            processor.SwitchScheme(schemeId);

            _cmd.Verify(i => i.ExecCommand(
                It.Is<string>(j => j == $"powercfg.exe /s {schemeId}")));
        }

        [Theory, AutoData]
        public void OpenOptionsTest()
        {
            var processor = new PowerProcessor(_cmd.Object);

            processor.OpenOptions();

            _cmd.Verify(i => i.ExecCommand(
                It.Is<string>(j => j == @"%windir%\system32\control.exe /name Microsoft.PowerOptions /page")));
        }

        [Fact]
        public void GetAllPowerSchemesTest_ThrowsIfNoActiveSchemeFound()
        {
            var processor = new PowerProcessor(_cmd.Object);

            Assert.Throws<EnergyTrayException>(() => processor.GetAllPowerSchemes());

            _cmd.Verify(i => i.ExecCommand(
                It.Is<string>(j => j == @"powercfg.exe /list")));
        }

        [Fact]
        public void GetActivePowerSchemeTest_Throws()
        {
            var processor = new PowerProcessor(_cmd.Object);

            Assert.Throws<EnergyTrayException>(() => processor.GetActivePowerScheme());
        }


        [Fact]
        public void GetActivePowerSchemeTest()
        {
            var processor = new PowerProcessor(_cmd.Object);

            var inputString = @"
            Existing Power Schemes (* Active)
            -----------------------------------
            Power Scheme GUID: 0688d228-2803-44ee-917d-2e544c763797  (Download)
            Power Scheme GUID: 381b4222-f694-41f0-9685-ff5bb260df2e  (Balanced)
            Power Scheme GUID: 49ef8fc0-bb7f-488e-b6a0-f1fc77ec649b  (Dell) *
            Power Scheme GUID: 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c  (High performance)
            Power Scheme GUID: a1841308-3541-4fab-bc81-f71556f20b4a  (Power saver)
            ";

            _cmd.Setup(i => i.ExecCommand(It.Is<string>(j => j == @"powercfg.exe /list")))
                .Returns(inputString);

            var schemes = processor.GetActivePowerScheme();

            Assert.Equal(schemes.Name.Trim(), "Dell");
        }
    }
}