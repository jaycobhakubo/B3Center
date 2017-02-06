using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Reports;

namespace GameTech.Elite.Client.Modules.B3Center.Model.Report
{
    public class ReportMain
    {
        public B3Report B3Reports { get; set; }
        public string ReportDisplayName { get; set; }
        public ReportBaseVm ReportBasevm { get; set; }
    }
}
