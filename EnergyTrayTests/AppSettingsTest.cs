using EnergyTray.Application.AppSettings.Provider;
using EnergyTray.Application.Exceptions;
using EnergyTray.Application.Utils;
using Moq;
using Xunit;

namespace EnergyTrayTests
{
    public class WrongFooSettings
    {
        public WrongFooSettings(string wrong)
        {
        }
    }

    public class FooSettings : AppSettings<EnergyTraySettings>
    {
        public FooSettings(IFileDelegate file) : base(file)
        {
        }
    }

    public class AppSettingsTest
    {
        [Theory, AutoMoqData]
        public void TestSave(Mock<IFileDelegate> fileDelegate)
        {
            var settings = new AppSettings<FooSettings>(fileDelegate.Object);

            settings.Save();

            fileDelegate.Verify(i => i.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));
        }

        [Fact]
        public void LoadSave()
        {
            var fileDelegate = new Mock<IFileDelegate>();
            fileDelegate.Setup(i => i.Exists(It.IsAny<string>())).Returns(true);
            fileDelegate.Setup(i => i.ReadAllText(It.IsAny<string>())).Returns("");

            var settings = new AppSettings<FooSettings>(fileDelegate.Object);

            settings.Load();

            fileDelegate.Verify(i => i.Exists(It.IsAny<string>()));
            fileDelegate.Verify(i => i.ReadAllText(It.IsAny<string>()));
        }

        [Fact]
        public void LoadSave_DoesNotReadIfNotExists()
        {
            var fileDelegate = new Mock<IFileDelegate>();
            fileDelegate.Setup(i => i.Exists(It.IsAny<string>())).Returns(false);

            var settings = new AppSettings<FooSettings>(fileDelegate.Object);

            settings.Load();

            fileDelegate.Verify(i => i.Exists(It.IsAny<string>()));
            fileDelegate.Verify(i => i.ReadAllText(It.IsAny<string>()), Times.Never);
        }
        
        [Fact]
        public void LoadSave_HandlesWrongSettings()
        {
            var fileDelegate = new Mock<IFileDelegate>();
            fileDelegate.Setup(i => i.Exists(It.IsAny<string>())).Returns(true);
            fileDelegate.Setup(i => i.ReadAllText(It.IsAny<string>())).Returns("");

            var settings = new AppSettings<WrongFooSettings>(fileDelegate.Object);

            Assert.Throws<EnergyTrayException>(() => settings.Load());
        }
    }
}