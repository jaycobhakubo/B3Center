using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTech.Elite.Client.Modules.B3Center.Model;
using SAPBusinessObjects.WPF.Viewer;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Reports
{
    public class ballcallVm : ReportTemplateViewModel
    {
        public ballcallVm(ReportTemplateModel reportTemplateModel) : base(reportTemplateModel)
        {
            bcvm = this;
        }

        public ReportTemplateViewModel bcvm
        {
            get;set;
        }

        public void SetReportViewerCr(CrystalReportsViewer crv)
        {
            this.vReportViewer = crv;
            this.ReportParameterVisible = System.Windows.Visibility.Hidden;
            this.ReportViewerVisibility = System.Windows.Visibility.Visible;
            //NewReportButton.Visibility = Visibility.Visible;
            //ReportViewerBorder.Visibility = Visibility.Visible;
            //SelectDateBorder.Visibility = Visibility.Hidden;

        }
        

    }
}
