using EnergyTray.Application.AppSettings.Provider;
using EnergyTray.Application.Exceptions;
using EnergyTray.Application.Utils;
using EnergyTrayTests.TestHelper;
using Moq;
using Xunit;

namespace EnergyTrayTests.Application.AppSettings.Provider
{
    public class AppSettingsTest
    {
        [Theory, AutoMoqData]
        public void TestSave(Mock<IFileAdapter> fileDelegate)
        {
            var settings = new EnergyTraySettings();

            settings.Save();

            fileDelegate.Verify(i => i.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));
        }

        [Fact]
        public void LoadSave()
        {
            var fileDelegate = new Mock<IFileAdapter>();
            fileDelegate.Setup(i => i.Exists(It.IsAny<string>())).Returns(true);
            fileDelegate.Setup(i => i.ReadAllText(It.IsAny<string>())).Returns("");

            var settings = new EnergyTraySettings();

            settings.Load();

            fileDelegate.Verify(i => i.Exists(It.IsAny<string>()));
            fileDelegate.Verify(i => i.ReadAllText(It.IsAny<string>()));
        }

        [Fact]
        public void LoadSave_DoesNotReadIfNotExists()
        {
            var fileDelegate = new Mock<IFileAdapter>();
            fileDelegate.Setup(i => i.Exists(It.IsAny<string>())).Returns(false);

            var settings = new EnergyTraySettings();

            settings.Load();

            fileDelegate.Verify(i => i.Exists(It.IsAny<string>()));
            fileDelegate.Verify(i => i.ReadAllText(It.IsAny<string>()), Times.Never);
        }
    }
}