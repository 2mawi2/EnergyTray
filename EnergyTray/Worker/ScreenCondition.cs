using System;
using System.Windows.Forms;

namespace EnergyTray.Worker
{
    [Serializable]
    public class ScreenCondition : Condition
    {
        public override bool Validate()
        {
            return Screen.AllScreens.Length > 1;
        }
    }
}