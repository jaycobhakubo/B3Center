﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameTech.Elite.Client.Modules.B3Center.Model.Setting
{
    public class PlayerSettings
    {
        public bool PlayerCalibrateTouch { get; set; }
        public bool PresstoCollect { get; set; }
        public bool AnnounceCall { get; set; }
        public bool PlayerScreenCursor { get; set; }
        public string TimeToCollect { get; set; }
        public bool Disclaimer { get; set; }
        public bool DisclaimerTextID { get; set; }
        public string PlayerMainVolume { get; set; }

        public bool IsEnableCRAZYBOUT { get; set; }
        public bool IsEnableJAILBREAK { get; set; }
        public bool IsEnableMAYAMONEY { get; set; }
        public bool IsEnableSPIRIT76 { get; set; }
        public bool IsEnableTIMEBOMB { get; set; }
        public bool IsEnableUKICKEM { get; set; }
        public bool IsEnableWILDBALL { get; set; }
        public bool IsEnableWILDFIRE { get; set; }

    }
}
