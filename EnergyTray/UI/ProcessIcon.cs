using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using EnergyTray.Application;
using EnergyTray.Application.Model;
using EnergyTray.Application.PowerManagement;
using EnergyTray.Application.Utils;
using EnergyTray.Properties;
using EnergyTray.Worker;

namespace EnergyTray.UI
{
    public class ProcessIcon : IProcessIcon
    {
        private readonly IMonitorCheckWorker _monitorCheckWorker;
        private readonly IContextMenu _contextMenu;
        private readonly IPowerProcessor _powerProcessor;

        private NotifyIcon Icon { get; }

        public ProcessIcon(
            IMonitorCheckWorker monitorCheckWorker,
            IContextMenu contextMenu,
            IPowerProcessor powerProcessor)
        {
            _monitorCheckWorker = monitorCheckWorker;
            _contextMenu = contextMenu;
            _powerProcessor = powerProcessor;
            _powerProcessor.OnPowerSchemeChange += (sender, e) => Update();
            monitorCheckWorker.OnAutoChanged = (sender, e) => Update();
            Icon = new NotifyIcon();
        }


        public void InitializeIcon(IEnumerable<PowerScheme> schemes)
        {
            Icon.MouseUp += OnClick;
            Icon.MouseDoubleClick += OnDoubleClick;
            Icon.Text = Resources.ProcessIcon_Display_Energy_Tray;
            Icon.Visible = true;
            Icon.ContextMenuStrip = _contextMenu.Create(schemes);
            Icon.Icon = Resources.Icon1;
            Update();
        }

        public void Update()
        {
            var activeScheme = _powerProcessor.GetActivePowerScheme();
            Icon.Text = activeScheme.Name;

            if (_monitorCheckWorker.AutoEnabled)
            {
                Icon.Text = Icon.Text + " (Auto)";
            }
        }

        public void Dispose() => Icon.Dispose();

        private void OnClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var mi = typeof(NotifyIcon).GetMethod(
                    "ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                mi.Invoke(Icon, null);
            }
        }

        private void OnDoubleClick(object sender, MouseEventArgs e)
        {
            _powerProcessor.OpenOptions();
            ForceMenuClose();
        }

        private void ForceMenuClose()
        {
            var mi = typeof(NotifyIcon).GetMethod("UpdateIcon", BindingFlags.Instance | BindingFlags.NonPublic);
            var contextMenuStripSave = Icon.ContextMenuStrip;
            Icon.ContextMenuStrip = null;
            mi.Invoke(Icon, new object[] {true});
            Icon.ContextMenuStrip = contextMenuStripSave;
        }
    }
}