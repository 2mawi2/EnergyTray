using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EnergyTray.Application;
using EnergyTray.Application.Extensions;
using EnergyTray.Application.Model;
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

        public ContextMenuStrip Create(IEnumerable<PowerScheme> powerSchemes)
        {
            var items = CreateItems(powerSchemes);
            return CreateMenu(items);
        }

        private IEnumerable<ToolStripDropDownItem> CreateItems(IEnumerable<PowerScheme> powerSchemes)
        {
            var items = new List<ToolStripDropDownItem>();
            items.AddRange(GetPowerSchemeItems(powerSchemes));
            items.Add(GetAutomaticModeItem());
            items.Add(GetOptionsItem());
            items.Add(GetExitItem());
            return items;
        }

        private static ContextMenuStrip CreateMenu(IEnumerable<ToolStripDropDownItem> items)
        {
            var menu = new ContextMenuStrip();
            items.ForEach(i => menu.Items.Add(i));
            return menu;
        }

        private IEnumerable<ToolStripMenuItem> GetPowerSchemeItems(IEnumerable<PowerScheme> powerSchemes)
        {
            return powerSchemes.Select(i =>
            {
                var it = new ToolStripMenuItem {Text = i.Name};
                it.Click += (sender, e) => { _powerProcessor.SwitchScheme(i.Id); };
                return it;
            });
        }

        private ToolStripMenuItem GetAutomaticModeItem()
        {
            var item = new ToolStripMenuItem {Text = "Automatic Mode"};
            item.Click += (sender, e) => { _monitorCheckWorker.AutoEnabled = !_monitorCheckWorker.AutoEnabled; };
            return item;
        }

        private ToolStripMenuItem GetOptionsItem()
        {
            var item = new ToolStripMenuItem {Text = "Options"};
            item.Click += (sender, e) => _powerProcessor.OpenOptions();
            return item;
        }

        private ToolStripDropDownItem GetExitItem()
        {
            var item = new ToolStripMenuItem {Text = "Exit"};
            item.Click += (sender, e) => System.Windows.Forms.Application.Exit();
            return item;
        }
    }
}