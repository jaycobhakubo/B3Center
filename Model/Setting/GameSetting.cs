﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameTech.Elite.Client.Modules.B3Center.Model.Setting
{
    public class GameSettings
    {
        public string SiteName { get; set; }
        public string HandPayTrigger { get; set; }
        public string VIPPointMultiplier{ get; set; }
        public string MagCardSentinelStart { get; set; }
        public string MagCardSentinelEnd { get; set; }
        public string PlayerPINLength{ get; set; }
        public string RNGBallCallTime { get; set; }
        public string MinimunPlayers{ get; set; }
        public string MainVol { get; set; }
        public string Currency { get; set; }
        public string AutoSessionEnd { get; set; }
        public bool NorthDakotaMode { get; set; }
        public bool MultiOperator { get; set; }
        public bool CommonRNGBallCall { get; set; }
        public bool EnableUK { get; set; }
        public bool DualAccount { get; set; }
    }
}
 