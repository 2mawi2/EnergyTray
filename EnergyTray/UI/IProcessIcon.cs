using System;

namespace EnergyTray.UI
{
    public interface IProcessIcon : IDisposable
    {
        /// <summary>
        /// gets called by Program.cs/>
        /// </summary>
        void Display();

        void UpdateIcon();
    }
}