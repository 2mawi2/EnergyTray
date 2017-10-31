using System;

namespace EnergyTray.Application.Exceptions
{
    public class EnergyTrayException : Exception
    {
        public EnergyTrayException(string message) : base(message)
        {
        }

        public EnergyTrayException(Exception exception) : base(exception.Message, exception)
        {
          
        }

        public EnergyTrayException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}