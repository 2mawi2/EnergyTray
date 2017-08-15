using System;
using EnergyTray.UI;
using static System.Windows.Forms.Application;

namespace EnergyTray
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            EnableVisualStyles();
            SetCompatibleTextRenderingDefault(false);
            using (var tray = new ProcessIcon())
            {
                tray.Display();
                Run();
            }
        }
    }
}