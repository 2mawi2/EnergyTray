using System.Collections.Generic;
using System.Windows.Forms;
using EnergyTray.Application.Model;

namespace EnergyTray.UI
{
    public interface IContextMenu
    {
        ContextMenuStrip Create();
    }
}