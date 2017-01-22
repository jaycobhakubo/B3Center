using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameTech.Elite.Client.Modules.B3Center.Model.Setting
{
    public class PlayerSettings
    {
        public string TimeToCollect { get; set; }
        public bool ScreenCursor { get; set; }
        public bool CalibrateTouch { get; set; }
        public bool AnnounceCall { get; set; }
        public bool PressToCollect { get; set; }
        public bool Disclaimer { get; set; }
        public List<string> MainVolList { get; set; }
        public string MainVolSelected { get; set; }
    }
}
