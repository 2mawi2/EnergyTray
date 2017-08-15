using System;
using System.Windows.Forms;
using EnergyTray.Application;
using EnergyTray.Worker;

namespace EnergyTray.UI
{
    class ContextMenus
    {
        private readonly ProcessIcon _icon;
        public MonitorCheckWorker _monitorCheckWorker;
        public Cmd Cmd { get; }

        public ContextMenus(ProcessIcon processIcon)
        {
            _icon = processIcon;
            Cmd = new Cmd();
            _monitorCheckWorker = new MonitorCheckWorker(PowerMode_Click, Dell_Click);
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
            item = new ToolStripMenuItem { Text = "Automatic Mode" };
            item.Click += Auto_Click;
            menu.Items.Add(item);
            // Separator.
            sep = new ToolStripSeparator();
            menu.Items.Add(sep);
            // Exit.
            item = new ToolStripMenuItem {Text = "Options"};
            item.Click += new EventHandler(Options_Click);
            menu.Items.Add(item);
            return menu;
        }

        private void Auto_Click(object sender, EventArgs e)
        {
            _monitorCheckWorker.AutoEnabled = !_monitorCheckWorker.AutoEnabled;
            _icon.UpdateIcon();
        }


        private void Download_Click(object sender, EventArgs e)
        {
            Cmd.ExecCommand("powercfg.exe /s " + Global.Download);
            _icon.UpdateIcon();
        }

        private void Dell_Click(object sender, EventArgs e)
        {
            Cmd.ExecCommand("powercfg.exe /s " + Global.Dell);
            _icon.UpdateIcon();
        }

        private void EnergySaver_Click(object sender, EventArgs e)
        {
            Cmd.ExecCommand("powercfg.exe /s " + Global.Energysaver);
            _icon.UpdateIcon();
        }

        private void PowerMode_Click(object sender, EventArgs e)
        {
            Cmd.ExecCommand("powercfg.exe /s " + Global.Powermode);
            _icon.UpdateIcon();
        }

        private void Balanced_Click(object sender, EventArgs e)
        {
            Cmd.ExecCommand("powercfg.exe /s " + Global.Balanced);
            _icon.UpdateIcon();
        }

        private void Options_Click(object sender, EventArgs e)
        {
            Cmd.ExecCommand(@"%windir%\system32\control.exe /name Microsoft.PowerOptions /page");
        }
    }
}