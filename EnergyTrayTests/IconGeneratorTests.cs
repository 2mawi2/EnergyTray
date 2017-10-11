using System.Runtime.InteropServices.ComTypes;
using EnergyTray.Application.IconGenerator;
using Xunit;

namespace EnergyTrayTests
{
    public class IconGeneratorTests
    {
        [Fact]
        public void GenerateTest()
        {
            var icon = IconGenerator.GetIcon("A");

            Assert.NotNull(icon);
        }
    }
}