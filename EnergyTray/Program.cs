using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using EnergyTray.Application;
using EnergyTray.Application.AppSettings.Consumer;
using EnergyTray.Application.AppSettings.Provider;
using EnergyTray.Application.Exceptions;
using EnergyTray.Application.Utils;
using EnergyTray.Properties;
using EnergyTray.UI;
using EnergyTray.Worker;
using StructureMap;
using static System.Windows.Forms.Application;

namespace EnergyTray
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            EnableVisualStyles();
            SetCompatibleTextRenderingDefault(false);
            ConfigureErrorHandling();
            CreateContainer().GetInstance<IApp>();
            Run();
        }

        private static void ConfigureErrorHandling()
        {
            ThreadException += ExceptionHandler.ErrorHandler;
            SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += ExceptionHandler.UnhandledException;
        }

        private static Container CreateContainer()
        {
            var container = new Container(i =>
            {
                i.Scan(_ =>
                {
                    _.TheCallingAssembly();
                    _.WithDefaultConventions();
                });

                i.ForConcreteType<MonitorCheckWorker>().Configure.Singleton();
                i.ForConcreteType<ProcessIcon>().Configure.Singleton();
                i.For<IMonitorCheckWorker>().Use(c => c.GetInstance<MonitorCheckWorker>());
                i.For<IProcessIcon>().Use(c => c.GetInstance<ProcessIcon>());
            });

            return container;
        }
    }
}