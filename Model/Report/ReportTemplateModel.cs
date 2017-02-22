using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTech.Elite.Base;
using SAPBusinessObjects.WPF.Viewer;
using System.Windows;

namespace GameTech.Elite.Client.Modules.B3Center.Model
{
  
        public class ReportTemplateModel
        {
            public string ReportTitle{ get; set; }       
            public List<string> ReportParameter { get; set; }
            public CrystalReportsViewer CrystalReportViewer_{ get; set; }                  
            public ReportParameterModel rptParModel { get; set; }
            public Visibility ReportViewerm     { get; set; }
            public Visibility DefaultViewerm { get; set; }
            public int CurrentUser { get; set; }
            public int CurrentMachine { get; set; }
    }
}