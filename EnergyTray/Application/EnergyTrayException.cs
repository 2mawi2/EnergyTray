using System;

namespace EnergyTray.Application.PowerManagement
{
    public class EnergyTrayException : Exception
    {
        public EnergyTrayException(string message) : base(message)
        {
        }

        public EnergyTrayException(Exception exception) : base(exception.Message, exception)
        {
          
        }
    }
}