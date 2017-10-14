using System;
using System.Collections.Generic;
using EnergyTray.Application.Model;

namespace EnergyTray.UI
{
    public interface IProcessIcon : IDisposable
    {
        void InitializeIcon();
    }
}