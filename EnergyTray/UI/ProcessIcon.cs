using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using EnergyTray.Application;
using EnergyTray.Properties;
using EnergyTray.Worker;

namespace EnergyTray.UI
{
    public class ProcessIcon : IProcessIcon
    {
        private readonly ICmd _cmd;

        /// <summary>
        /// The NotifyIcon object.
        /// </summary>
        private NotifyIcon Icon { get; }

        private ContextMenus _contextMenus;

        public ProcessIcon(ICmd cmd)
        {
            _cmd = cmd;
            Icon = new NotifyIcon();
        }

        public void Display()
        {
            InitIcon();
            UpdateIcon();
        }

        private void InitIcon()
        {
            Icon.MouseUp += OnClick;
            Icon.MouseDoubleClick += OnDoubleClick;
            Icon.Text = Resources.ProcessIcon_Display_Energy_Tray;
            Icon.Visible = true;
            _contextMenus = new ContextMenus(this, _cmd);
            Icon.ContextMenuStrip = _contextMenus.Create();
            Icon.Icon = Resources.Icon1;
        }

        public void UpdateIcon()
        {
            _cmd.ExecCommand("powercfg.exe /getactivescheme", (sender, args) =>
            {
                var outputLine = args.Data;
                if (!string.IsNullOrEmpty(outputLine))
                {
                    if (StringUtils.IsPowerSchemeOutput(outputLine))
                    {
                        outputLine = StringUtils.GetSchemeId(outputLine);
                        switch (outputLine)
                        {
                            case Global.Download:
                                Icon.Icon = Resources.Download;
                                Icon.Text = outputLine.ToString();
                                break;
                            case Global.Energysaver:
                                Icon.Icon = Resources.EnergySaver;
                                Icon.Text = outputLine.ToString();
                                break;
                            case Global.Balanced:
                                Icon.Icon = Resources.Balanced;
                                Icon.Text = outputLine.ToString();
                                break;
                            case Global.Powermode:
                                Icon.Icon = Resources.Power;
                                Icon.Text = outputLine.ToString();
                                break;
                            case Global.Dell:
                                Icon.Icon = Resources.Dell;
                                Icon.Text = outputLine.ToString();
                                break;
                            default:
                                Icon.Icon = Resources.Dell;
                                Icon.Text = "Error while loading energy setup";
                                break;
                        }

                        if (_contextMenus.MonitorCheckWorker.AutoEnabled)
                        {
                            Icon.Text = Icon.Text + " (Auto)";
                        }
                    }
                }
            });
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
            _cmd.ExecCommand(@"%windir%\system32\control.exe /name Microsoft.PowerOptions /page");
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