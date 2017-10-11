using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using EnergyTray.Properties;
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
            var applicationManager = container.GetInstance<IApp>();
            Run();
        }

        private static Container CreateContainer() => new Container(i => i.Scan(_ =>
        {
            _.TheCallingAssembly();
            _.WithDefaultConventions();
        }));
    }
}