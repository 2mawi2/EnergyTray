using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using EnergyTray.Application;
using EnergyTray.Properties;
using EnergyTray.Worker;

namespace EnergyTray.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessIcon : IDisposable
    {
        /// <summary>
        /// The NotifyIcon object.
        /// </summary>
        private NotifyIcon Icon { get; }

        private ContextMenus _contextMenus;

        public ProcessIcon()
        {
            Icon = new NotifyIcon();
        }

        /// <summary>
        /// gets called by Program.cs/>
        /// </summary>
        public void Display()
        {
            Icon.MouseUp += OnClick;
            Icon.MouseDoubleClick += OnDoubleClick;
            Icon.Text = Resources.ProcessIcon_Display_Energy_Tray;
            Icon.Visible = true;
            _contextMenus = new ContextMenus(this);
            Icon.ContextMenuStrip = _contextMenus.Create();
            Icon.Icon = Resources.Icon1;
            UpdateIcon();
        }


        private void RefreshIcon(CurrentMode mode)
        {
            switch (mode)
            {
                case CurrentMode.Download:
                    Icon.Icon = Resources.Download;
                    Icon.Text = mode.ToString();
                    break;
                case CurrentMode.Power:
                    Icon.Icon = Resources.Power;
                    Icon.Text = mode.ToString();
                    break;
                case CurrentMode.Balanced:
                    Icon.Icon = Resources.Balanced;
                    Icon.Text = mode.ToString();
                    break;
                case CurrentMode.Energysaver:
                    Icon.Icon = Resources.EnergySaver;
                    Icon.Text = mode.ToString();
                    break;
                case CurrentMode.Dell:
                    Icon.Icon = Resources.Dell;
                    Icon.Text = mode.ToString();
                    break;
                default:
                    Icon.Icon = Resources.Dell;
                    Icon.Text = "Error while loading energy setup";
                    break;
            }
            if(_contextMenus._monitorCheckWorker.AutoEnabled)
            {
                Icon.Text = Icon.Text + " (Auto)";
            }
        }


        public void UpdateIcon()
        {
            Cmd.ExecCommand("powercfg.exe /getactivescheme", OnDataReceived);
        }

        /// <summary>
        /// CallbackHandler for CheckCurrentState
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            var outputLine = e.Data;
            if (!string.IsNullOrEmpty(outputLine))
            {
                if (outputLine.Contains("Power Scheme GUID"))
                {
                    outputLine = outputLine.Replace("Power Scheme GUID: ", "");
                    outputLine = outputLine.Remove(36, outputLine.ToCharArray().Length - 36);
                    switch (outputLine)
                    {
                        case Global.Download:
                            RefreshIcon(CurrentMode.Download);
                            break;
                        case Global.Energysaver:
                            RefreshIcon(CurrentMode.Energysaver);
                            break;
                        case Global.Balanced:
                            RefreshIcon(CurrentMode.Balanced);
                            break;
                        case Global.Powermode:
                            RefreshIcon(CurrentMode.Power);
                            break;
                        case Global.Dell:
                            RefreshIcon(CurrentMode.Dell);
                            break;
                    }
                }
            }
        }

        public void Dispose() => Icon.Dispose();

        private void OnClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var mi = typeof(NotifyIcon).GetMethod("ShowContextMenu",
                    BindingFlags.Instance | BindingFlags.NonPublic);
                mi.Invoke(Icon, null);
            }
        }

        private ContextMenuStrip _contextMenuStripSave;

        private void OnDoubleClick(object sender, MouseEventArgs e)
        {
            Cmd.ExecCommand(@"%windir%\system32\control.exe /name Microsoft.PowerOptions /page");
            ForceMenuClose();
        }

        private void ForceMenuClose()
        {
            var mi = typeof(NotifyIcon).GetMethod("UpdateIcon", BindingFlags.Instance | BindingFlags.NonPublic);
            _contextMenuStripSave = Icon.ContextMenuStrip;
            Icon.ContextMenuStrip = null;
            mi.Invoke(Icon, new object[] {true});
            Icon.ContextMenuStrip = _contextMenuStripSave;
        }


        public enum CurrentMode
        {
            Download,
            Energysaver,
            Power,
            Balanced,
            Dell
        }
    }
}