#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

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
using GameTech.Elite.Base;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Reports
{
    public class ReportBaseVm : ViewModelBase
    {
        #region Private Members

        private ReportTemplateModel m_rptTemplateModel = new ReportTemplateModel();

        #endregion

        #region Constructors

        public ReportBaseVm()
        {
        }

        internal void Initialize(ReportTemplateModel rptTemplateModel)
        {
            m_rptTemplateModel = rptTemplateModel;
        }

        private static volatile ReportBaseVm m_instance;
        private static readonly object m_syncRoot = new Object();

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
        #endregion

        #region Methods
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

            var userId = ReportVM.ReportTemplate_Model.CurrentUser;
            var machineId = ReportVM.ReportTemplate_Model.CurrentMachine;

            switch (Report.Id)
            {
                case ReportId.B3AccountHistory:
                    {

                        Report.CrystalReportDocument.SetParameterValue("@P_Date_", ReportVM.ParamVm.GetDate().Date.ToString(CultureInfo.InvariantCulture)); //tempdate.Date.ToString(CultureInfo.InvariantCulture)); /*bcvm.parVm.RptParameterDataHandler.Date_*/
                        Report.CrystalReportDocument.SetParameterValue("@SessionID_", ReportVM.ParamVm.RptParameterDataHandler.b3Session.Number);
                        Report.CrystalReportDocument.SetParameterValue("@AccountNumber", ReportVM.ParamVm.RptParameterDataHandler.b3AccountNumber);
                        break;
                    }
                case ReportId.B3Accounts:
                    {

                        var testr = GetMonthEquivValue(ReportVM.ParamVm.MonthSelected) + 1;
                        Report.CrystalReportDocument.SetParameterValue("@nMonth", GetMonthEquivValue(ReportVM.ParamVm.MonthSelected));
                        Report.CrystalReportDocument.SetParameterValue("@nYear", ReportVM.ParamVm.YearSelected.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3BallCallByGame:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@session", ReportVM.ParamVm.RptParameterDataHandler.b3Session.Number);
                        Report.CrystalReportDocument.SetParameterValue("@DateParameter", ReportVM.ParamVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        break;

                    }
                case ReportId.B3BallCallBySession:
                    {
                        var startdate = ReportVM.ParamVm.StartDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        var enddate = ReportVM.ParamVm.EndDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        Report.CrystalReportDocument.SetParameterValue("@StartDate", startdate);
                        Report.CrystalReportDocument.SetParameterValue("@EndDate", enddate);
                        break;
                    }
                case ReportId.B3BingoCardReport:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@startId", ReportVM.ParamVm.RptParameterDataHandler.b3StartingCard);
                        Report.CrystalReportDocument.SetParameterValue("@endId", ReportVM.ParamVm.RptParameterDataHandler.b3EndingCard);
                        break;
                    }
                case ReportId.B3Daily:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@SessionNum", null);
                        Report.CrystalReportDocument.SetParameterValue("@UserId", ReportVM.ReportTemplate_Model.CurrentUser);
                        Report.CrystalReportDocument.SetParameterValue("@Station", ReportVM.ReportTemplate_Model.CurrentMachine);
                        Report.CrystalReportDocument.SetParameterValue("@DateTime", ReportVM.ParamVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3Detail:
                    {
                        var startdate = ReportVM.ParamVm.StartDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        var enddate = ReportVM.ParamVm.EndDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        Report.CrystalReportDocument.SetParameterValue("@dtStartDateTime", startdate);
                        Report.CrystalReportDocument.SetParameterValue("@dtEndDateTime", enddate);
                        break;
                    }
                case ReportId.B3Drawer://No data: Issue on clientmac(This report need fix)
                    {
                        Report.CrystalReportDocument.SetParameterValue("@MachineID", machineId);
                        Report.CrystalReportDocument.SetParameterValue("@Station", machineId);
                        Report.CrystalReportDocument.SetParameterValue("@nDate", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                        Report.CrystalReportDocument.SetParameterValue("@UserId", ReportVM.ReportTemplate_Model.CurrentUser);
                        break;
                    }
                case ReportId.B3Jackpot:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@nSessNum", ReportVM.ParamVm.RptParameterDataHandler.b3Session.Number);
                        Report.CrystalReportDocument.SetParameterValue("@UserId", userId);
                        Report.CrystalReportDocument.SetParameterValue("@Station", machineId);
                        Report.CrystalReportDocument.SetParameterValue("@nDate", ReportVM.ParamVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3Monthly:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@nMonth", GetMonthEquivValue(ReportVM.ParamVm.MonthSelected));
                        Report.CrystalReportDocument.SetParameterValue("@nYear", ReportVM.ParamVm.YearSelected.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3Session:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@SessionID", ReportVM.ParamVm.RptParameterDataHandler.b3Session.Number);
                        Report.CrystalReportDocument.SetParameterValue("@DateN", ReportVM.ParamVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        Report.CrystalReportDocument.SetParameterValue("@UserID", userId);
                        Report.CrystalReportDocument.SetParameterValue("@Station", machineId);
                        break;
                    }

                case ReportId.B3SessionSummary:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@SessionN", ReportVM.ParamVm.RptParameterDataHandler.b3Session.Number);
                        Report.CrystalReportDocument.SetParameterValue("@DateTime", ReportVM.ParamVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        Report.CrystalReportDocument.SetParameterValue("@UserID", userId);
                        Report.CrystalReportDocument.SetParameterValue("@Station", machineId);
                        break;
                    }
                case ReportId.B3SessionTransaction:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@SessionNumber", ReportVM.ParamVm.RptParameterDataHandler.b3Session.Number);
                        Report.CrystalReportDocument.SetParameterValue("@DateTime", ReportVM.ParamVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3Void:
                    {
                        var startdate = ReportVM.ParamVm.StartDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        var enddate = ReportVM.ParamVm.EndDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        Report.CrystalReportDocument.SetParameterValue("@dtStartDateTime", startdate);
                        Report.CrystalReportDocument.SetParameterValue("@dtEndDateTime", enddate);
                        break;
                    }
                case ReportId.B3WinnerCards:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@SessionNum", ReportVM.ParamVm.RptParameterDataHandler.b3Session.Number);
                        Report.CrystalReportDocument.SetParameterValue("@DateRun", ReportVM.ParamVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
            }
            return Report.CrystalReportDocument;
        }

        #endregion

        #region Public Properties

        public ReportTemplateViewModel ReportVM
        {
            get;
            set;
        }

        #endregion
    }
}
