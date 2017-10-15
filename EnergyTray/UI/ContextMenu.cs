using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EnergyTray.Application;
using EnergyTray.Application.AppSettings;
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
        private readonly IIconSettings _iconSettings;

        public ContextMenu(IMonitorCheckWorker monitorCheckWorker, IPowerProcessor powerProcessor, IIconSettings iconSettings)
        {
            _monitorCheckWorker = monitorCheckWorker;
            _powerProcessor = powerProcessor;
            _iconSettings = iconSettings;
        }

        public ContextMenuStrip Create()
        {
            var items = CreateItems();
            return CreateMenu(items);
        }

        private IEnumerable<ToolStripDropDownItem> CreateItems()
        {
            var items = new List<ToolStripDropDownItem>();
            items.AddRange(GetPowerSchemeItems(_powerProcessor.GetAllPowerSchemes()));
            items.Add(GetAutomaticModeItem());
            items.Add(GetOptionsItem());
            items.Add(GetIconSectionItem());
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
                return ToolStripItemFactory.Create(i.Name, (sender, e) => _powerProcessor.SwitchScheme(i.Id));
            });
        }

        private ToolStripMenuItem GetAutomaticModeItem()
        {
            return ToolStripItemFactory.Create("Automatic Mode",
                (sender, e) => _monitorCheckWorker.AutoEnabled = !_monitorCheckWorker.AutoEnabled);
        }

        private ToolStripMenuItem GetOptionsItem()
        {
            return ToolStripItemFactory.Create("Power Options", (sender, e) => _powerProcessor.OpenOptions());
        }

        private ToolStripDropDownItem GetIconSectionItem()
        {
            return ToolStripItemFactory.Create("Select Icons", (sender, e) =>
            {
                var form = new SelectIconsForm(_powerProcessor, _iconSettings);
                form.Show();
            });
        }

        private static ToolStripMenuItem GetExitItem()
        {
            return ToolStripItemFactory.Create("Exit", (sender, e) => System.Windows.Forms.Application.Exit());
        }
    }
}