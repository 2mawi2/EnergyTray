using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using EnergyTray.Application;
using EnergyTray.Application.AppSettings;
using EnergyTray.Application.AppSettings.Consumer;
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
        private readonly IIconSettings _iconSettings;

        private NotifyIcon Icon { get; }

        public ProcessIcon(
            IMonitorCheckWorker monitorCheckWorker,
            IContextMenu contextMenu,
            IPowerProcessor powerProcessor,
            IIconSettings iconSettings)
        {
            _monitorCheckWorker = monitorCheckWorker;
            _contextMenu = contextMenu;
            _powerProcessor = powerProcessor;
            _iconSettings = iconSettings;
            _powerProcessor.OnPowerSchemeChange += (sender, e) => Update();
            monitorCheckWorker.OnAutoChanged = (sender, e) => Update();
            Icon = new NotifyIcon();
        }


        public void InitializeIcon()
        {
            Icon.MouseUp += OnClick;
            Icon.MouseDoubleClick += OnDoubleClick;
            Icon.Text = Resources.ProcessIcon_Display_Energy_Tray;
            Icon.Visible = true;
            Icon.ContextMenuStrip = _contextMenu.Create();
            Icon.Icon = Resources.Icon1;
            Update();
        }

        private void Update()
        {
            var activeScheme = _powerProcessor.GetActivePowerScheme();
            UpdateIconText(activeScheme);
            UpdateIconImage(activeScheme);
        }

        private void UpdateIconText(PowerScheme activeScheme)
        {
            Icon.Text = activeScheme.Name;
            if (_monitorCheckWorker.AutoEnabled)
            {
                Icon.Text = Icon.Text + " (Auto)";
            }
        }

        private void UpdateIconImage(PowerScheme activeScheme)
        {
            var icon = _iconSettings.GetIconById(activeScheme.Id);
            Icon.Icon = icon != null ? new Icon(icon) : Resources.Icon1;
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