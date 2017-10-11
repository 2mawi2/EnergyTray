using System.Windows.Forms;

namespace EnergyTray.UI
{
    public interface IContextMenu
    {
        ContextMenuStrip Create();
    }
}