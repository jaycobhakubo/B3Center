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
    public class ReportBaseVm : ReportTemplateViewModel
    {
        public ReportBaseVm(ReportTemplateModel reportTemplateModel)
            : base(reportTemplateModel)
        {
            bcvm = this;
        }

        public ReportTemplateViewModel bcvm
        {
            get;set;
        }



        public ReportDocument LoadReportDocument(B3Report Report)
        {
            switch (Report.Id)
            {
                case ReportId.B3AccountHistory:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@P_Date_", bcvm.parVm.reportParameterModel.Date_);
                        Report.CrystalReportDocument.SetParameterValue("@SessionID_", bcvm.parVm.reportParameterModel.b3Session);
                        Report.CrystalReportDocument.SetParameterValue("@AccountNumber", bcvm.parVm.reportParameterModel.b3AccountNumber);
                        break;
                    }
                case ReportId.B3Accounts:
                    {
                       
                        break;
                    }
                case ReportId.B3BallCallByGame:
                    {
                       
                        break;
                    }
                case ReportId.B3BingoCardReport:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@startId", bcvm.parVm.reportParameterModel.b3StartingCard);
                        Report.CrystalReportDocument.SetParameterValue("@endId", bcvm.parVm.reportParameterModel.b3EndingCard);
                        break;
                    }
                case ReportId.B3Daily:
                    {
                      
                        break;
                    }
                case ReportId.B3Detail:
                    {
                       
                        break;
                    }
                case ReportId.B3Drawer:
                    {
                        
                        break;
                    }
                case ReportId.B3Jackpot:
                    {
                       
                        break;
                    }
                case ReportId.B3Monthly:
                    {
                       
                        break;
                    }
                case ReportId.B3Session:
                    {
                       
                        break;
                    }

                case ReportId.B3SessionSummary:
                    {
                       
                        break;
                    }
                case ReportId.B3SessionTransaction:
                    {
                       
                        break;
                    }
                case ReportId.B3Void:
                    {
                       
                        break;
                    }
                case ReportId.B3WinnerCards:
                    {
               
                        break;
                    }

            }

           
        return Report.CrystalReportDocument;
        }




    }
}
