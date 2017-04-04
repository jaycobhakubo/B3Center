#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System;
using System.Windows;
using System.Windows.Input;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Model;
using GameTech.Elite.UI;
using CrystalDecisions.CrystalReports.Engine;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Reports;
using System.Globalization;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{
    public class ReportTemplateViewModel : ViewModelBase
    {
        private ReportTemplateModel m_reportTemplateModel;
        public ICommand CloseViewReportCommand
        {
            get;
            set;
        }

        public ReportTemplateViewModel(ReportTemplateModel reportTemplateModel)
        {
            ReportTemplateModel = reportTemplateModel;
            var reportParameterList = ReportTemplateModel.ReportParameter;
            ParamVm = new ReportParameterViewModel(reportParameterList, ReportTemplateModel.RptParamModel);
            CloseViewReportCommand = new RelayCommand(parameter => CloseViewReport());
        }

        public ReportTemplateModel ReportTemplateModel
        {
            get { return m_reportTemplateModel; }
            set
            {
                m_reportTemplateModel = value;
                RaisePropertyChanged("ReportTemplateModel");
            }
        }

        public string ReportTitle
        {
            get
            {
                return ReportTemplateModel.ReportTitle;
            }
        }

        public ReportParameterViewModel ParamVm
        {
            get;
            set;
        }

        public Visibility ReportParameterVisible
        {
            get { return ReportTemplateModel.DefaultViewerm; }//knc
            set
            {
                var testdd = ReportTemplateModel;
                testdd.DefaultViewerm = value;
                ReportTemplateModel = testdd;
                RaisePropertyChanged("ReportParameterVisible");
            }
        }

        public Visibility ReportViewerVisibility
        {
            get { return ReportTemplateModel.ReportViewerm; }
            set
            {
                ReportTemplateModel.ReportViewerm = value;
                RaisePropertyChanged("ReportViewerVisibility");
            }
        }

        public void CloseViewReport()
        {
            var x = ReportsViewModel.Instance;
            x.DefaultViewMode = Visibility.Visible;
            x.CrystalReportViewMode = Visibility.Collapsed;
            x.CloseReportAbortOperation();
        }

        private int GetMonthEquivValue(string monthName)
        {
            string monthname = monthName;
            string[] months =
             {
                "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct",
                "Nov", "Dec"
            };
            return Array.IndexOf(months, monthname) + 1;
        }

        public ReportDocument LoadReportDocument(B3Report report)
        {
            //Station is the machine Description of the machine. 
            //E.g machine ID 22 Description POS SALES
            //Since I cant find it. Ill just send the ID and just use subreport to get the machine description on Crystal report.

            var userId = ReportTemplateModel.CurrentUser;
            var machineId = ReportTemplateModel.CurrentMachine;

            switch (report.Id)
            {
                case ReportId.B3AccountHistory:
                    {

                        report.CrystalReportDocument.SetParameterValue("@P_Date_", ParamVm.GetDate().Date.ToString(CultureInfo.InvariantCulture)); //tempdate.Date.ToString(CultureInfo.InvariantCulture)); /*bcvm.parVm.RptParameterDataHandler.Date_*/
                        report.CrystalReportDocument.SetParameterValue("@SessionID_", ParamVm.RptParameterDataHandler.B3Session.Number);
                        report.CrystalReportDocument.SetParameterValue("@AccountNumber", ParamVm.RptParameterDataHandler.B3AccountNumber);
                        break;
                    }
                case ReportId.B3Accounts:
                    {

                        GetMonthEquivValue(ParamVm.MonthSelected);
                        report.CrystalReportDocument.SetParameterValue("@nMonth", GetMonthEquivValue(ParamVm.MonthSelected));
                        report.CrystalReportDocument.SetParameterValue("@nYear", ParamVm.YearSelected.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3BallCallByGame:
                    {
                        report.CrystalReportDocument.SetParameterValue("@session", ParamVm.RptParameterDataHandler.B3Session.Number);
                        report.CrystalReportDocument.SetParameterValue("@DateParameter", ParamVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        break;

                    }
                case ReportId.B3BallCallBySession:
                    {
                        var startdate = ParamVm.StartDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        var enddate = ParamVm.EndDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        report.CrystalReportDocument.SetParameterValue("@StartDate", startdate);
                        report.CrystalReportDocument.SetParameterValue("@EndDate", enddate);
                        break;
                    }
                case ReportId.B3BingoCardReport:
                    {
                        report.CrystalReportDocument.SetParameterValue("@startId", ParamVm.StartingCard);
                        report.CrystalReportDocument.SetParameterValue("@endId", ParamVm.EndingCard);
                        break;
                    }
                case ReportId.B3Daily:
                    {
                        report.CrystalReportDocument.SetParameterValue("@SessionNum", null);
                        report.CrystalReportDocument.SetParameterValue("@UserId", ReportTemplateModel.CurrentUser);
                        report.CrystalReportDocument.SetParameterValue("@Station", ReportTemplateModel.CurrentMachine);
                        report.CrystalReportDocument.SetParameterValue("@DateTime", ParamVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3Detail:
                    {
                        var startdate = ParamVm.StartDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        var enddate = ParamVm.EndDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        report.CrystalReportDocument.SetParameterValue("@dtStartDateTime", startdate);
                        report.CrystalReportDocument.SetParameterValue("@dtEndDateTime", enddate);
                        break;
                    }
                case ReportId.B3Drawer://No data: Issue on clientmac(This report need fix)
                    {
                        report.CrystalReportDocument.SetParameterValue("@MachineID", machineId);
                        report.CrystalReportDocument.SetParameterValue("@Station", machineId);
                        report.CrystalReportDocument.SetParameterValue("@nDate", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                        report.CrystalReportDocument.SetParameterValue("@UserId", ReportTemplateModel.CurrentUser);
                        break;
                    }
                case ReportId.B3Jackpot:
                    {
                        report.CrystalReportDocument.SetParameterValue("@nSessNum", ParamVm.RptParameterDataHandler.B3Session.Number);
                        report.CrystalReportDocument.SetParameterValue("@UserId", userId);
                        report.CrystalReportDocument.SetParameterValue("@Station", machineId);
                        report.CrystalReportDocument.SetParameterValue("@nDate", ParamVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3Monthly:
                    {
                        report.CrystalReportDocument.SetParameterValue("@nMonth", GetMonthEquivValue(ParamVm.MonthSelected));
                        report.CrystalReportDocument.SetParameterValue("@nYear", ParamVm.YearSelected.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3Session:
                    {
                        report.CrystalReportDocument.SetParameterValue("@SessionID", ParamVm.RptParameterDataHandler.B3Session.Number);
                        report.CrystalReportDocument.SetParameterValue("@DateN", ParamVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        report.CrystalReportDocument.SetParameterValue("@UserID", userId);
                        report.CrystalReportDocument.SetParameterValue("@Station", machineId);
                        break;
                    }

                case ReportId.B3SessionSummary:
                    {
                        report.CrystalReportDocument.SetParameterValue("@SessionN", ParamVm.RptParameterDataHandler.B3Session.Number);
                        report.CrystalReportDocument.SetParameterValue("@DateTime", ParamVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        report.CrystalReportDocument.SetParameterValue("@UserID", userId);
                        report.CrystalReportDocument.SetParameterValue("@Station", machineId);
                        break;
                    }
                case ReportId.B3SessionTransaction:
                    {
                        report.CrystalReportDocument.SetParameterValue("@SessionNumber", ParamVm.RptParameterDataHandler.B3Session.Number);
                        report.CrystalReportDocument.SetParameterValue("@DateTime", ParamVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3Void:
                    {
                        var startdate = ParamVm.StartDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        var enddate = ParamVm.EndDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        report.CrystalReportDocument.SetParameterValue("@dtStartDateTime", startdate);
                        report.CrystalReportDocument.SetParameterValue("@dtEndDateTime", enddate);
                        break;
                    }
                case ReportId.B3WinnerCards:
                    {
                        report.CrystalReportDocument.SetParameterValue("@SessionNum", ParamVm.RptParameterDataHandler.B3Session.Number);
                        report.CrystalReportDocument.SetParameterValue("@DateRun", ParamVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                default:
                    {
                        B3CenterController.Logger.Log(string.Format("Unable to find report for id: '{0}'", report.Id), LoggerLevel.Warning);
                        break;
                    }
            }
            return report.CrystalReportDocument;
        }
    }
}
