using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EnergyTray.Application;
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

        private IEnumerable<ToolStripMenuItem> CreateItems(IEnumerable<PowerScheme> powerSchemes)
        {
            var items = new List<ToolStripMenuItem>();
            items.AddRange(GetPowerSchemeItems(powerSchemes));
            items.Add(GetAutomaticModeItem());
            items.Add(new ToolStripMenuItem());
            items.Add(GetOptionsItem());
            return items;
        }

        private static ContextMenuStrip CreateMenu(IEnumerable<ToolStripMenuItem> items)
        {
            var menu = new ContextMenuStrip();
            foreach (var i in items)
            {
                menu.Items.Add(i);
            }
            return menu;
        }

        private ToolStripMenuItem GetAutomaticModeItem()
        {
            var item = new ToolStripMenuItem {Text = "Automatic Mode"};
            item.Click += (sender, e) => _monitorCheckWorker.ToggleAutoEnabled();
            return item;
        }

        private ToolStripMenuItem GetOptionsItem()
        {
            var item = new ToolStripMenuItem {Text = "Options"};
            item.Click += (sender, e) => _powerProcessor.OpenOptions();
            return item;
        }

        private IEnumerable<ToolStripMenuItem> GetPowerSchemeItems(IEnumerable<PowerScheme> powerSchemes)
        {
            return powerSchemes.Select(i =>
            {
                var it = new ToolStripMenuItem {Text = i.Name};
                it.Click += (sender, e) => _powerProcessor.SwitchScheme(i.Id);
                return it;
            });
        }
    }
}