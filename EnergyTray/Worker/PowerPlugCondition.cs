using System;
using System.Windows.Forms;

namespace EnergyTray.Worker
{
    [Serializable]
    public class PowerPlugCondition : Condition
    {
        public override bool Validate()
        {
            return SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Online;
        }
    }
}