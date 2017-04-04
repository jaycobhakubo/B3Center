#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Model;
using GameTech.Elite.Reports;
using GameTech.Elite.Base;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Reports
{
    public class ReportBaseVm : ViewModelBase
    {
        #region Private Members

        #endregion

        #region Constructors

        #endregion

        internal void Initialize(ReportTemplateModel rptTemplateModel)
        {
        }

        private static volatile ReportBaseVm m_instance;
        private static readonly object m_syncRoot = new object();

        public static ReportBaseVm Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_syncRoot)
                    {
                        if (m_instance == null) // double-check just in case
                            m_instance = new ReportBaseVm();
                    }
                }

                return m_instance;
            }
        }

        #region Methods
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

            var userId = ReportVm.ReportTemplateModel.CurrentUser;
            var machineId = ReportVm.ReportTemplateModel.CurrentMachine;

            switch (report.Id)
            {
                case ReportId.B3AccountHistory:
                    {

                        report.CrystalReportDocument.SetParameterValue("@P_Date_", ReportVm.ParamVm.GetDate().Date.ToString(CultureInfo.InvariantCulture)); //tempdate.Date.ToString(CultureInfo.InvariantCulture)); /*bcvm.parVm.RptParameterDataHandler.Date_*/
                        report.CrystalReportDocument.SetParameterValue("@SessionID_", ReportVm.ParamVm.RptParameterDataHandler.B3Session.Number);
                        report.CrystalReportDocument.SetParameterValue("@AccountNumber", ReportVm.ParamVm.RptParameterDataHandler.B3AccountNumber);
                        break;
                    }
                case ReportId.B3Accounts:
                    {

                        GetMonthEquivValue(ReportVm.ParamVm.MonthSelected);
                        report.CrystalReportDocument.SetParameterValue("@nMonth", GetMonthEquivValue(ReportVm.ParamVm.MonthSelected));
                        report.CrystalReportDocument.SetParameterValue("@nYear", ReportVm.ParamVm.YearSelected.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3BallCallByGame:
                    {
                        report.CrystalReportDocument.SetParameterValue("@session", ReportVm.ParamVm.RptParameterDataHandler.B3Session.Number);
                        report.CrystalReportDocument.SetParameterValue("@DateParameter", ReportVm.ParamVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        break;

                    }
                case ReportId.B3BallCallBySession:
                    {
                        var startdate = ReportVm.ParamVm.StartDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        var enddate = ReportVm.ParamVm.EndDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        report.CrystalReportDocument.SetParameterValue("@StartDate", startdate);
                        report.CrystalReportDocument.SetParameterValue("@EndDate", enddate);
                        break;
                    }
                case ReportId.B3BingoCardReport:
                    {
                        report.CrystalReportDocument.SetParameterValue("@startId", ReportVm.ParamVm.RptParameterDataHandler.B3StartingCard);
                        report.CrystalReportDocument.SetParameterValue("@endId", ReportVm.ParamVm.RptParameterDataHandler.B3EndingCard);
                        break;
                    }
                case ReportId.B3Daily:
                    {
                        report.CrystalReportDocument.SetParameterValue("@SessionNum", null);
                        report.CrystalReportDocument.SetParameterValue("@UserId", ReportVm.ReportTemplateModel.CurrentUser);
                        report.CrystalReportDocument.SetParameterValue("@Station", ReportVm.ReportTemplateModel.CurrentMachine);
                        report.CrystalReportDocument.SetParameterValue("@DateTime", ReportVm.ParamVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3Detail:
                    {
                        var startdate = ReportVm.ParamVm.StartDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        var enddate = ReportVm.ParamVm.EndDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        report.CrystalReportDocument.SetParameterValue("@dtStartDateTime", startdate);
                        report.CrystalReportDocument.SetParameterValue("@dtEndDateTime", enddate);
                        break;
                    }
                case ReportId.B3Drawer://No data: Issue on clientmac(This report need fix)
                    {
                        report.CrystalReportDocument.SetParameterValue("@MachineID", machineId);
                        report.CrystalReportDocument.SetParameterValue("@Station", machineId);
                        report.CrystalReportDocument.SetParameterValue("@nDate", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                        report.CrystalReportDocument.SetParameterValue("@UserId", ReportVm.ReportTemplateModel.CurrentUser);
                        break;
                    }
                case ReportId.B3Jackpot:
                    {
                        report.CrystalReportDocument.SetParameterValue("@nSessNum", ReportVm.ParamVm.RptParameterDataHandler.B3Session.Number);
                        report.CrystalReportDocument.SetParameterValue("@UserId", userId);
                        report.CrystalReportDocument.SetParameterValue("@Station", machineId);
                        report.CrystalReportDocument.SetParameterValue("@nDate", ReportVm.ParamVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3Monthly:
                    {
                        report.CrystalReportDocument.SetParameterValue("@nMonth", GetMonthEquivValue(ReportVm.ParamVm.MonthSelected));
                        report.CrystalReportDocument.SetParameterValue("@nYear", ReportVm.ParamVm.YearSelected.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3Session:
                    {
                        report.CrystalReportDocument.SetParameterValue("@SessionID", ReportVm.ParamVm.RptParameterDataHandler.B3Session.Number);
                        report.CrystalReportDocument.SetParameterValue("@DateN", ReportVm.ParamVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        report.CrystalReportDocument.SetParameterValue("@UserID", userId);
                        report.CrystalReportDocument.SetParameterValue("@Station", machineId);
                        break;
                    }

                case ReportId.B3SessionSummary:
                    {
                        report.CrystalReportDocument.SetParameterValue("@SessionN", ReportVm.ParamVm.RptParameterDataHandler.B3Session.Number);
                        report.CrystalReportDocument.SetParameterValue("@DateTime", ReportVm.ParamVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        report.CrystalReportDocument.SetParameterValue("@UserID", userId);
                        report.CrystalReportDocument.SetParameterValue("@Station", machineId);
                        break;
                    }
                case ReportId.B3SessionTransaction:
                    {
                        report.CrystalReportDocument.SetParameterValue("@SessionNumber", ReportVm.ParamVm.RptParameterDataHandler.B3Session.Number);
                        report.CrystalReportDocument.SetParameterValue("@DateTime", ReportVm.ParamVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3Void:
                    {
                        var startdate = ReportVm.ParamVm.StartDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        var enddate = ReportVm.ParamVm.EndDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        report.CrystalReportDocument.SetParameterValue("@dtStartDateTime", startdate);
                        report.CrystalReportDocument.SetParameterValue("@dtEndDateTime", enddate);
                        break;
                    }
                case ReportId.B3WinnerCards:
                    {
                        report.CrystalReportDocument.SetParameterValue("@SessionNum", ReportVm.ParamVm.RptParameterDataHandler.B3Session.Number);
                        report.CrystalReportDocument.SetParameterValue("@DateRun", ReportVm.ParamVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
            }
            return report.CrystalReportDocument;
        }

        #endregion

        #region Public Properties

        public ReportTemplateViewModel ReportVm
        {
            get;
            set;
        }

        #endregion
    }
}
