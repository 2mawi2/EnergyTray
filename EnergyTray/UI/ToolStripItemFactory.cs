using System;
using System.Windows.Forms;

namespace EnergyTray.UI
{
    public static class ToolStripItemFactory
    {
        public static ToolStripMenuItem Create(string text, EventHandler clickHandler)
        {
            var item = new ToolStripMenuItem {Text = text};
            item.Click += clickHandler;
            return item;
        }
    }
}