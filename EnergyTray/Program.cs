using System;
using System.Diagnostics;
using EnergyTray.UI;
using StructureMap;
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

            var container = CreateContainer();

            using (var tray = container.GetInstance<IProcessIcon>())
            {
                tray.Display();
                Run();
            }
        }

        private static Container CreateContainer() => new Container(i => i.Scan(_ =>
        {
            _.TheCallingAssembly();
            _.WithDefaultConventions();
        }));
    }
}