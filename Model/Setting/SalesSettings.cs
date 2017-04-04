using System.Collections.Generic;

namespace GameTech.Elite.Client.Modules.B3Center.Model.Setting
{
    public class SalesSettings
    {
        public string LogRecycleDays { get; set; }
        public bool ScreenCursor { get; set; }
        public bool AutoPrintSessionReport { get; set; }
        public bool AlowinSessionBall { get; set; }
        public bool PrintLogo { get; set; }
        public bool PagePrinter { get; set; }
        public bool CalibrateTouch { get; set; }
        public bool QuickSales { get; set; }
        public bool LoggingEnable { get; set; }
        public string VolumeSales { get; set; }

        public List<string> MainVolList { get; set; }
    }
}
