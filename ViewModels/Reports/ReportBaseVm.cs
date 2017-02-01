using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
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
        #region CONSTRUCTOR
        public ReportBaseVm(ReportTemplateModel reportTemplateModel)
            : base(reportTemplateModel)
        {
            bcvm = this;
        }
        #endregion
        #region METHOD
        private int GetMonthEquivValue(string monthName)
        {
            string monthname = "Jan";
           string[] m_months =
            {
                "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct",
                "Nov", "Dec"
            };
           return  Array.IndexOf(m_months, monthname) + 1;          
        }

        public ReportDocument LoadReportDocument(B3Report Report)
        {     
            //Station is the machine Description of the machine. 
            //E.g machine ID 22 Description POS SALES
            //Since I cant find it. Ill just send the ID and just use subreport to get the machine description on Crystal report.
            var tempdate = new DateTime();
            var endtempdate = new DateTime();
            var userId = bcvm.ReportTemplate_Model.CurrentUser;
            var machineId = bcvm.ReportTemplate_Model.CurrentMachine; 
    
            switch (Report.Id)
            {
                case ReportId.B3AccountHistory:
                    {
                  
                        Report.CrystalReportDocument.SetParameterValue("@P_Date_", bcvm.parVm.GetDate().Date.ToString(CultureInfo.InvariantCulture)); //tempdate.Date.ToString(CultureInfo.InvariantCulture)); /*bcvm.parVm.RptParameterDataHandler.Date_*/
                        Report.CrystalReportDocument.SetParameterValue("@SessionID_", bcvm.parVm.RptParameterDataHandler.b3Session.Number);
                        Report.CrystalReportDocument.SetParameterValue("@AccountNumber", bcvm.parVm.RptParameterDataHandler.b3AccountNumber);
                        break;
                    }
                case ReportId.B3Accounts:
                    {
                        
                        Report.CrystalReportDocument.SetParameterValue("@nMonth", GetMonthEquivValue(bcvm.parVm.MonthSelected) + 1);
                        Report.CrystalReportDocument.SetParameterValue("@nYear", bcvm.parVm.YearSelected.ToString(CultureInfo.InvariantCulture)); 
                        break;
                    }
                case ReportId.B3BallCallByGame:
                    {                      
                       Report.CrystalReportDocument.SetParameterValue("@session", bcvm.parVm.RptParameterDataHandler.b3Session.Number);
                       Report.CrystalReportDocument.SetParameterValue("@DateParameter", bcvm.parVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        break;
                    
                    }
                case ReportId.B3BallCallBySession:
                    {
                        var startdate = bcvm.parVm.StartDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        var enddate =   bcvm.parVm.EndDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        Report.CrystalReportDocument.SetParameterValue("@StartDate", startdate);
                        Report.CrystalReportDocument.SetParameterValue("@EndDate", enddate);
                        break;
                    }
                case ReportId.B3BingoCardReport:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@startId", bcvm.parVm.RptParameterDataHandler.b3StartingCard);
                        Report.CrystalReportDocument.SetParameterValue("@endId", bcvm.parVm.RptParameterDataHandler.b3EndingCard);
                        break;
                    }
                case ReportId.B3Daily:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@SessionNum", null);
                        Report.CrystalReportDocument.SetParameterValue("@UserId", bcvm.ReportTemplate_Model.CurrentUser); 
                        Report.CrystalReportDocument.SetParameterValue("@Station", bcvm.ReportTemplate_Model.CurrentMachine);
                        Report.CrystalReportDocument.SetParameterValue("@DateTime", bcvm.parVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3Detail:
                    {
                        var startdate = bcvm.parVm.StartDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        var enddate = bcvm.parVm.EndDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        Report.CrystalReportDocument.SetParameterValue("@dtStartDateTime", startdate);
                        Report.CrystalReportDocument.SetParameterValue("@dtEndDateTime", enddate);
                        break;
                    }
                case ReportId.B3Drawer://No data: Issue on clientmac(This report need fix)
                    {
                     Report.CrystalReportDocument.SetParameterValue("@MachineID", machineId);
                     Report.CrystalReportDocument.SetParameterValue("@Station", machineId); 
                        Report.CrystalReportDocument.SetParameterValue("@nDate", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                        Report.CrystalReportDocument.SetParameterValue("@UserId", bcvm.ReportTemplate_Model.CurrentUser);
                        break;
                    }
                case ReportId.B3Jackpot:
                    {
                    Report.CrystalReportDocument.SetParameterValue("@nSessNum", bcvm.parVm.RptParameterDataHandler.b3Session.Number);
                    Report.CrystalReportDocument.SetParameterValue("@UserId", userId);
                    Report.CrystalReportDocument.SetParameterValue("@Station", machineId);
                    Report.CrystalReportDocument.SetParameterValue("@nDate", bcvm.parVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3Monthly:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@nMonth", GetMonthEquivValue(bcvm.parVm.MonthSelected) + 1);//bcvm.parVm.RptParameterDataHandler.dateMonth);
                        Report.CrystalReportDocument.SetParameterValue("@nYear", bcvm.parVm.YearSelected.ToString(CultureInfo.InvariantCulture)); //bcvm.parVm.RptParameterDataHandler.dateYear);                      
                        break;                       
                    }
                case ReportId.B3Session:
                    {
                       Report.CrystalReportDocument.SetParameterValue("@SessionID", bcvm.parVm.RptParameterDataHandler.b3Session.Number);
                       Report.CrystalReportDocument.SetParameterValue("@DateN", bcvm.parVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                       Report.CrystalReportDocument.SetParameterValue("@UserID", userId);
                       Report.CrystalReportDocument.SetParameterValue("@Station", machineId);
                       break;                 
                    }

                case ReportId.B3SessionSummary:
                    {
                       Report.CrystalReportDocument.SetParameterValue("@SessionN", bcvm.parVm.RptParameterDataHandler.b3Session.Number);
                       Report.CrystalReportDocument.SetParameterValue("@DateTime", bcvm.parVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                       Report.CrystalReportDocument.SetParameterValue("@UserID", userId);
                       Report.CrystalReportDocument.SetParameterValue("@Station", machineId);
                      break;
                    }
                case ReportId.B3SessionTransaction:
                    {
                       Report.CrystalReportDocument.SetParameterValue("@SessionNumber", bcvm.parVm.RptParameterDataHandler.b3Session.Number);
                       Report.CrystalReportDocument.SetParameterValue("@DateTime", bcvm.parVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                      break;
                    }
                case ReportId.B3Void:
                    {
                        var startdate = bcvm.parVm.StartDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        var enddate = bcvm.parVm.EndDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        Report.CrystalReportDocument.SetParameterValue("@dtStartDateTime",startdate);
                        Report.CrystalReportDocument.SetParameterValue("@dtEndDateTime", enddate);
                        break;
                    }
                case ReportId.B3WinnerCards:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@SessionNum", bcvm.parVm.RptParameterDataHandler.b3Session.Number);
                        Report.CrystalReportDocument.SetParameterValue("@DateRun", bcvm.parVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
            }          
        return Report.CrystalReportDocument;
        }

#endregion
        #region PROPERTIES
        public ReportTemplateViewModel bcvm
        {
            get;
            set;
        }
        #endregion
    }
}
