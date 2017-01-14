using System;
using System.Collections.Generic;
using System.Globalization;
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



        public ReportDocument LoadReportDocument(B3Report Report/*, int UserId, int StationId*/)
        {
            DateTime tempdate = new DateTime();
            //tempdate = DateTime.Parse("1/13/2017 00:00:00");

             DateTime endtempdate = new DateTime();
            //endtempdate = DateTime.Parse("1/14/2017 00:00:00");


            switch (Report.Id)
            {
                case ReportId.B3AccountHistory:
                    {
                      tempdate = DateTime.Parse(bcvm.parVm.datepickerVm.datepicker.DateFull.ToString());
                      Report.CrystalReportDocument.SetParameterValue("@P_Date_", tempdate.Date.ToString(CultureInfo.InvariantCulture)); //tempdate.Date.ToString(CultureInfo.InvariantCulture)); /*bcvm.parVm.reportParameterModel.Date_*/
                        Report.CrystalReportDocument.SetParameterValue("@SessionID_", bcvm.parVm.reportParameterModel.b3Session.Number);
                        Report.CrystalReportDocument.SetParameterValue("@AccountNumber", bcvm.parVm.reportParameterModel.b3AccountNumber);
                        break;
                    }
                case ReportId.B3Accounts:
                    {
                        string tempyear = "2017";
                        Report.CrystalReportDocument.SetParameterValue("@nMonth", /*bcvm.parVm.reportParameterModel.dateMonth*/1);
                        Report.CrystalReportDocument.SetParameterValue("@nYear", tempyear.ToString()); /*bcvm.parVm.reportParameterModel.dateYear*/ 
                        break;
                    }
                case ReportId.B3BallCallByGame://OK
                    {
                       
                      Report.CrystalReportDocument.SetParameterValue("@session", bcvm.parVm.reportParameterModel.b3Session.Number);
                       Report.CrystalReportDocument.SetParameterValue("@DateParameter", tempdate.Date.ToString(CultureInfo.InvariantCulture));
                        
                       
                        break;
                    
                    }
                case ReportId.B3BallCallBySession://OK
                    {
                        Report.CrystalReportDocument.SetParameterValue("@StartDate", tempdate.Date.ToString(CultureInfo.InvariantCulture));
                        Report.CrystalReportDocument.SetParameterValue("@EndDate", endtempdate.Date.ToString(CultureInfo.InvariantCulture));
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
                        Report.CrystalReportDocument.SetParameterValue("@SessionNum", null);
                        Report.CrystalReportDocument.SetParameterValue("@UserId", 0);
                        Report.CrystalReportDocument.SetParameterValue("@Station", 0);
                        Report.CrystalReportDocument.SetParameterValue("@DateTime", /*bcvm.parVm.reportParameterModel*/tempdate.Date.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3Detail:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@dtStartDateTime", tempdate.Date.ToString(CultureInfo.InvariantCulture));
                       Report.CrystalReportDocument.SetParameterValue("@dtEndDateTime", endtempdate.Date.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3Drawer://No data: Issue on clientmac
                    {
                     Report.CrystalReportDocument.SetParameterValue("@MachineID", 15);
                        Report.CrystalReportDocument.SetParameterValue("@Station", 15);
                       Report.CrystalReportDocument.SetParameterValue("@nDate", tempdate.Date.ToString(CultureInfo.InvariantCulture));
                        Report.CrystalReportDocument.SetParameterValue("@UserId", 2);
                        break;
                    }
                case ReportId.B3Jackpot:
                    {
                     Report.CrystalReportDocument.SetParameterValue("@nSessNum", bcvm.parVm.reportParameterModel.b3Session.Number);
                     Report.CrystalReportDocument.SetParameterValue("@UserId", 2);
                    Report.CrystalReportDocument.SetParameterValue("@Station", 0);
                   Report.CrystalReportDocument.SetParameterValue("@nDate", tempdate.Date.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3Monthly:
                    {

                        string tempyear = "2017";
                        Report.CrystalReportDocument.SetParameterValue("@nMonth", /*bcvm.parVm.reportParameterModel.dateMonth*/1);
                        Report.CrystalReportDocument.SetParameterValue("@nYear", tempyear.ToString()); /*bcvm.parVm.reportParameterModel.dateYear*/
                        break;
                       
                    }
                case ReportId.B3Session:
                    {
                        
                        Report.CrystalReportDocument.SetParameterValue("@SessionID", bcvm.parVm.reportParameterModel.b3Session.Number);
                        Report.CrystalReportDocument.SetParameterValue("@DateN", tempdate.Date.ToString(CultureInfo.InvariantCulture));
                Report.CrystalReportDocument.SetParameterValue("@UserID", 2);
                 Report.CrystalReportDocument.SetParameterValue("@Station", 0);//15

                        break;
                 
                    }

                case ReportId.B3SessionSummary:
                    {

                       Report.CrystalReportDocument.SetParameterValue("@SessionN", bcvm.parVm.reportParameterModel.b3Session.Number);
                       Report.CrystalReportDocument.SetParameterValue("@DateTime", tempdate.Date.ToString(CultureInfo.InvariantCulture));
                       Report.CrystalReportDocument.SetParameterValue("@UserID", 2);
                   Report.CrystalReportDocument.SetParameterValue("@Station", 0);


                        break;
                    }
                case ReportId.B3SessionTransaction:
                    {

                       Report.CrystalReportDocument.SetParameterValue("@SessionNumber", bcvm.parVm.reportParameterModel.b3Session.Number);
                        Report.CrystalReportDocument.SetParameterValue("@DateTime", tempdate.Date.ToString(CultureInfo.InvariantCulture));


                        break;
                    }
                case ReportId.B3Void:
                    {

                        Report.CrystalReportDocument.SetParameterValue("@dtStartDateTime", tempdate.Date.ToString(CultureInfo.InvariantCulture));
                        Report.CrystalReportDocument.SetParameterValue("@dtEndDateTime", endtempdate.Date.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3WinnerCards:
                    {

                        Report.CrystalReportDocument.SetParameterValue("@SessionNum", bcvm.parVm.reportParameterModel.b3Session.Number);
                       Report.CrystalReportDocument.SetParameterValue("@DateRun", tempdate.Date.ToString(CultureInfo.InvariantCulture));

                        break;
                    }

            }

           
        return Report.CrystalReportDocument;
        }




    }
}
