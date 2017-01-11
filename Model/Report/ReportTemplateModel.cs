using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTech.Elite.Base;
using SAPBusinessObjects.WPF.Viewer;

namespace GameTech.Elite.Client.Modules.B3Center.Model
{
  
        public class ReportTemplateModel
        {
            public string ReportTitle
            {
                get;
                set;
            }

            public List<string> ReportParameter
            {
                get;
                set;
            }


            public CrystalReportsViewer CrystalReportViewer
            {
                get;
                set;
            }
        
    }
}