using EnergyTray.Application.Utils;
using EnergyTray.UI;
using Xunit;

namespace EnergyTrayTests
{
    public class ToolStripItemFactoryTest
    {
        [Theory]
        [InlineData("text")]
        public void CreateTest(string text)
        {
            var result = ToolStripItemFactory.Create(text, (sender, args) => { });
            Assert.Equal(text, result.Text);
        }
    }
}