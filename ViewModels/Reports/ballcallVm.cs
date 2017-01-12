using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Model;
using GameTech.Elite.Reports;
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

        public int StartingCard { get; set; }
        public int EndingCard { get; set; }

        public void SetReportViewerCr()
        {
           // this.ReportTitle = "uuuuu";
            //this.vReportViewer = crv;
            //this.ReportParameterVisible = System.Windows.Visibility.Hidden;
            //this.ReportViewerVisibility = System.Windows.Visibility.Visible;
            //this.CanExecutei = true;
            //NewReportButton.Visibility = Visibility.Visible;
            //ReportViewerBorder.Visibility = Visibility.Visible;
            //SelectDateBorder.Visibility = Visibility.Hidden;
        }



        public ReportDocument LoadReportDocument(B3Report bingoCardReport)
        {
   
            bingoCardReport.CrystalReportDocument.SetParameterValue("@startId", bcvm.parVm.reportParameterModel.b3StartingCard);
            bingoCardReport.CrystalReportDocument.SetParameterValue("@endId", bcvm.parVm.reportParameterModel.b3EndingCard);

            return bingoCardReport.CrystalReportDocument;

        }

       


    }
}
