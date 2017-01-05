using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTech.Elite.Client.Modules.B3Center.UI.ReportViews;

namespace GameTech.Elite.Client.Modules.B3Center.Model
{
    class ReportModel
    {
        public ReportTemplate reportTemplate
        {
            get;
            set;
        }

        public string reportTitle
        { get; set; }
    }
}
