using System;

namespace EnergyTray.Worker
{
    [Serializable]
    public abstract class Condition
    {
        public abstract bool Validate();
    }
}