﻿using System.Collections.Generic;

namespace EnergyTray.Application.AppSettings
{
    public class EnergyTraySetttings : AppSettings<EnergyTraySetttings>
    {
        public Dictionary<string, string> PowerSchemeIconMap = new Dictionary<string, string>();
    }
}