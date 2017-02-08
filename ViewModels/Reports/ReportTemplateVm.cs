using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Model;
using SAPBusinessObjects.WPF.Viewer;
using System.ComponentModel;
using GameTech.Elite.UI;
using CrystalDecisions.CrystalReports.Engine;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Reports;
using System.Globalization;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{

    

    public partial class ReportTemplateViewModel :ViewModelBase
    {
        private ReportTemplateModel m_reportTemplateModel;
        public ICommand CloseViewReportCommand { get; set; }
        private List<string> reportParameterList;


        public ReportTemplateViewModel(ReportTemplateModel reportTemplateModel)
        {         
            ReportTemplate_Model = reportTemplateModel;
            reportParameterList = ReportTemplate_Model.ReportParameter;
            parVm = new ReportParameterViewModel(reportParameterList, ReportTemplate_Model.rptParModel);//.Instance;
            CloseViewReportCommand = new RelayCommand(parameter => CloseViewReport());
        }

        public ReportTemplateModel ReportTemplate_Model
        {
            get { return m_reportTemplateModel; }
            set
            {
                m_reportTemplateModel = value;
                RaisePropertyChanged("ReportTemplate_Model");
            }
        }

        public string ReportTitle
        {
            get
            {
                return ReportTemplate_Model.ReportTitle;
            }     
        }

        public ReportParameterViewModel parVm
        {
            get;
            set;
        }

        public Visibility ReportParameterVisible
        {
            get { return ReportTemplate_Model.DefaultViewerm; }//knc
            set
            {
                var testdd = ReportTemplate_Model;
                testdd.DefaultViewerm = value;
                ReportTemplate_Model = testdd;
                RaisePropertyChanged("ReportParameterVisible");
            }
        }

        public Visibility ReportViewerVisibility
        {        
            get { return ReportTemplate_Model.ReportViewerm; }
            set
            {
                ReportTemplate_Model.ReportViewerm = value;
                RaisePropertyChanged("ReportViewerVisibility");
            }
        }

           public void CloseViewReport()
           {
               var x = ReportsViewModel.Instance;
               x.DefaultViewMode = Visibility.Visible;
               x.CRViewMode = Visibility.Collapsed;
           }


           private int GetMonthEquivValue(string monthName)
           {
               string monthname = monthName;
               string[] m_months =
             {
                "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct",
                "Nov", "Dec"
            };
               return Array.IndexOf(m_months, monthname) + 1;
           }


           public ReportDocument LoadReportDocument(B3Report Report)
           {
               //Station is the machine Description of the machine. 
               //E.g machine ID 22 Description POS SALES
               //Since I cant find it. Ill just send the ID and just use subreport to get the machine description on Crystal report.

               var userId = ReportTemplate_Model.CurrentUser;
               var machineId = ReportTemplate_Model.CurrentMachine;

               switch (Report.Id)
               {
                   case ReportId.B3AccountHistory:
                       {

                           Report.CrystalReportDocument.SetParameterValue("@P_Date_", parVm.GetDate().Date.ToString(CultureInfo.InvariantCulture)); //tempdate.Date.ToString(CultureInfo.InvariantCulture)); /*bcvm.parVm.RptParameterDataHandler.Date_*/
                           Report.CrystalReportDocument.SetParameterValue("@SessionID_", parVm.RptParameterDataHandler.b3Session.Number);
                           Report.CrystalReportDocument.SetParameterValue("@AccountNumber", parVm.RptParameterDataHandler.b3AccountNumber);
                           break;
                       }
                   case ReportId.B3Accounts:
                       {

                           var testr = GetMonthEquivValue(parVm.MonthSelected) + 1;
                           Report.CrystalReportDocument.SetParameterValue("@nMonth", GetMonthEquivValue(parVm.MonthSelected) + 1);
                           Report.CrystalReportDocument.SetParameterValue("@nYear", parVm.YearSelected.ToString(CultureInfo.InvariantCulture));
                           break;
                       }
                   case ReportId.B3BallCallByGame:
                       {
                           Report.CrystalReportDocument.SetParameterValue("@session", parVm.RptParameterDataHandler.b3Session.Number);
                           Report.CrystalReportDocument.SetParameterValue("@DateParameter", parVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                           break;

                       }
                   case ReportId.B3BallCallBySession:
                       {
                           var startdate = parVm.StartDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                           var enddate = parVm.EndDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                           Report.CrystalReportDocument.SetParameterValue("@StartDate", startdate);
                           Report.CrystalReportDocument.SetParameterValue("@EndDate", enddate);
                           break;
                       }
                   case ReportId.B3BingoCardReport:
                       {
                           Report.CrystalReportDocument.SetParameterValue("@startId", parVm.StartingCard);
                           Report.CrystalReportDocument.SetParameterValue("@endId", parVm.EndingCard);
                           break;
                       }
                   case ReportId.B3Daily:
                       {
                           Report.CrystalReportDocument.SetParameterValue("@SessionNum", null);
                           Report.CrystalReportDocument.SetParameterValue("@UserId", ReportTemplate_Model.CurrentUser);
                           Report.CrystalReportDocument.SetParameterValue("@Station", ReportTemplate_Model.CurrentMachine);
                           Report.CrystalReportDocument.SetParameterValue("@DateTime", parVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                           break;
                       }
                   case ReportId.B3Detail:
                       {
                           var startdate = parVm.StartDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                           var enddate = parVm.EndDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                           Report.CrystalReportDocument.SetParameterValue("@dtStartDateTime", startdate);
                           Report.CrystalReportDocument.SetParameterValue("@dtEndDateTime", enddate);
                           break;
                       }
                   case ReportId.B3Drawer://No data: Issue on clientmac(This report need fix)
                       {
                           Report.CrystalReportDocument.SetParameterValue("@MachineID", machineId);
                           Report.CrystalReportDocument.SetParameterValue("@Station", machineId);
                           Report.CrystalReportDocument.SetParameterValue("@nDate", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                           Report.CrystalReportDocument.SetParameterValue("@UserId", ReportTemplate_Model.CurrentUser);
                           break;
                       }
                   case ReportId.B3Jackpot:
                       {
                           Report.CrystalReportDocument.SetParameterValue("@nSessNum", parVm.RptParameterDataHandler.b3Session.Number);
                           Report.CrystalReportDocument.SetParameterValue("@UserId", userId);
                           Report.CrystalReportDocument.SetParameterValue("@Station", machineId);
                           Report.CrystalReportDocument.SetParameterValue("@nDate", parVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                           break;
                       }
                   case ReportId.B3Monthly:
                       {
                           Report.CrystalReportDocument.SetParameterValue("@nMonth", GetMonthEquivValue(parVm.MonthSelected));//bcvm.parVm.RptParameterDataHandler.dateMonth);
                           Report.CrystalReportDocument.SetParameterValue("@nYear", parVm.YearSelected.ToString(CultureInfo.InvariantCulture)); //bcvm.parVm.RptParameterDataHandler.dateYear);                      
                           break;
                       }
                   case ReportId.B3Session:
                       {
                           Report.CrystalReportDocument.SetParameterValue("@SessionID", parVm.RptParameterDataHandler.b3Session.Number);
                           Report.CrystalReportDocument.SetParameterValue("@DateN", parVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                           Report.CrystalReportDocument.SetParameterValue("@UserID", userId);
                           Report.CrystalReportDocument.SetParameterValue("@Station", machineId);
                           break;
                       }

                   case ReportId.B3SessionSummary:
                       {
                           Report.CrystalReportDocument.SetParameterValue("@SessionN", parVm.RptParameterDataHandler.b3Session.Number);
                           Report.CrystalReportDocument.SetParameterValue("@DateTime", parVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                           Report.CrystalReportDocument.SetParameterValue("@UserID", userId);
                           Report.CrystalReportDocument.SetParameterValue("@Station", machineId);
                           break;
                       }
                   case ReportId.B3SessionTransaction:
                       {
                           Report.CrystalReportDocument.SetParameterValue("@SessionNumber", parVm.RptParameterDataHandler.b3Session.Number);
                           Report.CrystalReportDocument.SetParameterValue("@DateTime", parVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                           break;
                       }
                   case ReportId.B3Void:
                       {
                           var startdate = parVm.StartDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                           var enddate = parVm.EndDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                           Report.CrystalReportDocument.SetParameterValue("@dtStartDateTime", startdate);
                           Report.CrystalReportDocument.SetParameterValue("@dtEndDateTime", enddate);
                           break;
                       }
                   case ReportId.B3WinnerCards:
                       {
                           Report.CrystalReportDocument.SetParameterValue("@SessionNum", parVm.RptParameterDataHandler.b3Session.Number);
                           Report.CrystalReportDocument.SetParameterValue("@DateRun", parVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                           break;
                       }
               }
               return Report.CrystalReportDocument;
           }

          
    }
}
