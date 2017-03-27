using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GameTech.Elite.Client.Modules.B3Center.Business;

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

        //public bool IsEnableCRAZYBOUT { get; set; }
        //public bool IsEnableJAILBREAK { get; set; }
        //public bool IsEnableMAYAMONEY { get; set; }
        //public bool IsEnableSPIRIT76 { get; set; }
        //public bool IsEnableTIMEBOMB { get; set; }
        //public bool IsEnableUKICKEM { get; set; }
        //public bool IsEnableWILDBALL { get; set; }
        //public bool IsEnableWILDFIRE { get; set; }

        public B3GameSetting IsEnableCRAZYBOUT { get; set; }
        public B3GameSetting IsEnableJAILBREAK { get; set; }
        public B3GameSetting IsEnableMAYAMONEY { get; set; }
        public B3GameSetting IsEnableSPIRIT76 { get; set; }
        public B3GameSetting IsEnableTIMEBOMB { get; set; }
        public B3GameSetting IsEnableUKICKEM { get; set; }
        public B3GameSetting IsEnableWILDBALL { get; set; }
        public B3GameSetting IsEnableWILDFIRE { get; set; }

        //public List<B3GameSetting> B3SettingEnableDisablePreviousValue { get; set; }


    }
}
