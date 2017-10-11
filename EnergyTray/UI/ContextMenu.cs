using System;
using System.Windows.Forms;
using EnergyTray.Application;
using EnergyTray.Application.PowerManagement;
using EnergyTray.Worker;

namespace EnergyTray.UI
{
    public class ContextMenu : IContextMenu
    {
        private readonly IMonitorCheckWorker _monitorCheckWorker;
        private readonly IPowerProcessor _powerProcessor;

        public ContextMenu(IMonitorCheckWorker monitorCheckWorker, IPowerProcessor powerProcessor)
        {
            _monitorCheckWorker = monitorCheckWorker;
            _powerProcessor = powerProcessor;
        }

        public ContextMenuStrip Create()
        {
            // Add the default menu options.
            var menu = new ContextMenuStrip();
            ToolStripSeparator sep;
            // Windows Explorer.
            var item = new ToolStripMenuItem {Text = "Download"};
            item.Click += Download_Click;
            menu.Items.Add(item);
            // Windows Explorer.
            item = new ToolStripMenuItem {Text = "Energy Saver"};
            item.Click += EnergySaver_Click;
            menu.Items.Add(item);
            // About.
            item = new ToolStripMenuItem {Text = "Power Mode"};
            item.Click += PowerMode_Click;
            menu.Items.Add(item);
            item = new ToolStripMenuItem {Text = "Balanced"};
            item.Click += Balanced_Click;
            menu.Items.Add(item);
            item = new ToolStripMenuItem {Text = "Dell"};
            item.Click += Dell_Click;
            menu.Items.Add(item);
            item = new ToolStripMenuItem {Text = "Automatic Mode"};
            item.Click += Auto_Click;
            menu.Items.Add(item);
            // Separator.
            sep = new ToolStripSeparator();
            menu.Items.Add(sep);
            // Exit.
            item = new ToolStripMenuItem {Text = "Options"};
            item.Click += Options_Click;
            menu.Items.Add(item);
            return menu;
        }

        private void Auto_Click(object sender, EventArgs e) => _monitorCheckWorker.ToggleAutoEnabled();

        private void Download_Click(object sender, EventArgs e) => _powerProcessor.SwitchScheme(Global.Download);

        private void EnergySaver_Click(object sender, EventArgs e) => _powerProcessor.SwitchScheme(Global.Energysaver);

        private void PowerMode_Click(object sender, EventArgs e) => _powerProcessor.SwitchScheme(Global.Powermode);

        private void Dell_Click(object sender, EventArgs e) => _powerProcessor.SwitchScheme(Global.Dell);

        private void Balanced_Click(object sender, EventArgs e) => _powerProcessor.SwitchScheme(Global.Balanced);

        private void Options_Click(object sender, EventArgs e) => _powerProcessor.OpenOptions();
    }
}