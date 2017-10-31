using EnergyTray.UI;
using Xunit;

namespace EnergyTrayTests.UI
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