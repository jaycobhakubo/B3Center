#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
//US1618: B3 Session Report
//US4300: B3 Daily Report
//US4301: B3 Monthly Report
//US4317: B3 Void Report
//US4316: B3 Detail Report
//US4315: B3 Drawer Report
//US4314: B3 Jackpot Report
//US4302: B3 Accounts Report
//US4369: B3 Center: Option to print reports without previewing the report.
//US4377: B3 Center: Generate the Monthly report by month and year.
//US4373: B3 Center: Generate the Accounts Report by month and year.
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using CrystalDecisions.CrystalReports.Engine;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Reports;
using GameTech.Elite.Client.Modules.B3Center.UI.ReportViews;
using System.Windows.Controls;
using System.Windows.Input;
using GameTech.Elite.Client.Modules.B3Center.Model;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Reports;
using GameTech.Elite.Client.Modules.B3Center.Helper;
using System.Threading.Tasks;
using SAPBusinessObjects.WPF.Viewer;
using GameTech.Elite.UI;
using System.Windows.Threading;
using CrystalDecisions.Shared;
using GameTech.Elite.Client.Modules.B3Center.Model.Report;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{
    public class ReportsViewModel : ViewModelBase
    {
        #region MEMBERS
    
        private bool m_isLoading;
        private bool m_isPrinting;
        private B3Controller m_controller;
        private ObservableCollection<Session> m_sessionList;
        private List<B3Report> m_reports;
        private CrystalReportsViewer tempcr;
       

        #endregion
        #region CONSTRUCTORS

        /// <summary>
        /// Represents the view model for managing reports
        /// </summary>
        public ReportsViewModel()
        {
            m_selectedReportColl = new ReportMain();
            SessionList = new ObservableCollection<Session>();         
            IsLoading = false;
            IsPrinting = false;
        }
              

        /// <summary>
        /// Initializes the ViewModel with the specified controller.
        /// </summary>
        /// <param name="controller">The controller.</param>
        internal void Initialize(B3Controller controller)
        {
            m_controller = controller;
            m_reports = controller.Reports;         
            m_controller.SessionInfoCompleted += OnListInfoDone;
            tempcr = new CrystalReportsViewer();
            //set session list
            foreach (var session in controller.Sessions)
            {
                SessionList.Add(session);
            }

            b3AccountVm = new ReportTemplateViewModel(getrtm(ReportId.B3Accounts));
            b3DailyVm = new ReportTemplateViewModel(getrtm(ReportId.B3Daily));
            b3DetailVm = new ReportTemplateViewModel(getrtm(ReportId.B3Detail));
            b3AccountHistVm = new ReportTemplateViewModel(getrtm(ReportId.B3AccountHistory));
            b3CardVm = new ReportTemplateViewModel(getrtm(ReportId.B3BingoCardReport));

            m_reportCollection = new ObservableCollection<ReportMain>()
            {
                new ReportMain(){B3Reports = m_reports[0], ReportDisplayName = "Accounts",rpttemplatevm = b3AccountVm  , rptView = new ReportTemplate(b3AccountVm)},
                new ReportMain(){B3Reports = m_reports[1], ReportDisplayName = "Daily", rpttemplatevm = b3DailyVm ,rptView = new ReportTemplate(b3DailyVm)},
                new ReportMain(){B3Reports = m_reports[2], ReportDisplayName = "Detail",rpttemplatevm = b3DetailVm ,rptView = new ReportTemplate(b3DetailVm)},
                 new ReportMain(){B3Reports = m_reports[13], ReportDisplayName = "Account History", rpttemplatevm= b3AccountHistVm ,rptView = new ReportTemplate(b3AccountHistVm)},
                 new ReportMain(){B3Reports = m_reports[14], ReportDisplayName = "Bingo Card", rpttemplatevm= b3AccountHistVm ,rptView = new ReportTemplate(b3CardVm)},
            };
            m_selectedReportColl = m_reportCollection.FirstOrDefault();
            SetCommand();           
        }

        #endregion
        #region ACCESSOR (static)

        private static volatile ReportsViewModel m_instance;
        private static readonly object m_syncRoot = new Object();
        /// <summary>
        /// Gets the singleton instance of ReportsViewModel.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static ReportsViewModel Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_syncRoot)
                    {
                        if (m_instance == null)
                            m_instance = new ReportsViewModel();
                    }
                }

                return m_instance;
            }
        }

        #endregion
        #region METHOD

        //Set our data to be popualted to ReportTemplateViewModel then to the viewer.
        //This will be replaced by a observable collection if I got more time. Dont know which is faster.
        private ReportTemplateModel getrtm(ReportId b3rpt)
        {
           var result = new ReportTemplateModel();
           var temprptparmodel = new ReportParameterModel();
           var par = new List<string>();
            
            temprptparmodel.b3DateData = new Model.Shared.DatePickerM();
            result.CurrentUser = m_controller.Parent.StaffId;
            result.CurrentMachine = m_controller.Parent.MachineId;

            switch (b3rpt)
            {
                case ReportId.B3Accounts:
                    {
                        par.Add("MonthYear");
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Accounts Outstanding";              
                        result.ReportParameter = par;                   
                        break;
                    }
                case ReportId.B3AccountHistory:
                    {
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Account History";                     
                        par.Add("Date");
                        par.Add("Session");
                        par.Add("AccountNumber");                
                        result.ReportParameter = par;                  
                        break;
                    }
                case ReportId.B3BallCallByGame:
                    {
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Ball Call(by game)";
                        par.Add("Date");
                        par.Add("Session");
                        result.ReportParameter = par;                      
                        break;
                    }
                case ReportId.B3BallCallBySession:
                    {
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Ball Call(by session)";
                        temprptparmodel.StartDate = new Model.Shared.DatePickerM();
                        temprptparmodel.EndDate = new Model.Shared.DatePickerM();
                        par.Add("StartEndDate");
                        result.ReportParameter = par;
                        break;
                    }
                case ReportId.B3BingoCardReport:
                    {
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Bingo Card";
                        par.Add("StartEndCard");     
                        result.ReportParameter = par;                                        
                        break;
                    }
                case ReportId.B3Daily:
                    {                      
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Daily";
                        par.Add("Date");
                        result.ReportParameter = par;                      
                        break;
                    }
                case ReportId.B3Detail:
                    {                        
                        temprptparmodel.rptid = b3rpt;
                        temprptparmodel.StartDate = new Model.Shared.DatePickerM();
                        temprptparmodel.EndDate = new Model.Shared.DatePickerM();
                        result.ReportTitle = "Detail";
                        par.Add("StartEndDatewTime");
                        result.ReportParameter = par;                      
                        break;
                    }
                case ReportId.B3Drawer:
                    {
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Drawer";                  
                        result.ReportParameter = par;
                        break;
                    }
                case ReportId.B3Jackpot:
                    {                    
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Jackpot";
                        par.Add("Date");
                        par.Add("Session");
                        result.ReportParameter = par;                       
                        break;
                    }
                case ReportId.B3Monthly:
                    {                     
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Monthly";
                        par.Add("MonthYear");
                        result.ReportParameter = par;
                        break;
                    }
                case ReportId.B3Session:
                    {                    
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Session";
                        par.Add("Date");
                        par.Add("Session");
                        result.ReportParameter = par;
                        break;
                    }
                case ReportId.B3SessionSummary:
                    {
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Session Summary";
                        par.Add("Date");
                        par.Add("Session");
                        result.ReportParameter = par;
                        break;
                    }
                case ReportId.B3SessionTransaction:
                    {
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Session Transaction";
                        par.Add("Date");
                        par.Add("Session");
                        result.ReportParameter = par;
                        break;
                    }
                case ReportId.B3Void:
                    {
                        temprptparmodel.rptid = b3rpt;
                        temprptparmodel.StartDate = new Model.Shared.DatePickerM();
                        temprptparmodel.EndDate = new Model.Shared.DatePickerM();
                        result.ReportTitle = "Void";
                        par.Add("StartEndDatewTime");
                        result.ReportParameter = par;
                        break;
                    }
                case ReportId.B3WinnerCards:
                    {
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Winners Card";
                        par.Add("Date");
                        par.Add("Session");
                        result.ReportParameter = par;
                        break;
                    }
            }
            result.ReportViewerm = Visibility.Collapsed;
            result.DefaultViewerm = Visibility.Visible;
            result.rptParModel = temprptparmodel;
            return result;
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

        #region (on event)

        //Date change
        public void updateItemDateSelected(DateTime i)
        {
            var y = SelectedReportColl.B3Reports.Id;
            if
                (
                y == ReportId.B3AccountHistory
                || y == ReportId.B3BallCallByGame
                || y == ReportId.B3Jackpot
                || y == ReportId.B3Session
                        || y == ReportId.B3SessionSummary
                        || y == ReportId.B3SessionTransaction
                        || y == ReportId.B3WinnerCards
                )
            {
                SelectedReportColl.rpttemplatevm.parVm.UpdateSessionList(i);

            }
            SelectedReportColl.rpttemplatevm.parVm.CheckUserValidation(); //Just check user validation no need to filter it shouldnt be that much.      
        }


        //Report change
        public void SelectionChange()
        {
            SelectedReportColl.rpttemplatevm.parVm.CheckUserValidation();
        }

        #endregion
        #region (view and print report)

        public ReportDocument LoadReportDocument(B3Report Report)
        {
            //Station is the machine Description of the machine. 
            //E.g machine ID 22 Description POS SALES
            //Since I cant find it. Ill just send the ID and just use subreport to get the machine description on Crystal report.

            var userId = m_selectedReportTemplateViewModel.ReportTemplate_Model.CurrentUser;
            var machineId = m_selectedReportTemplateViewModel.ReportTemplate_Model.CurrentMachine;

            switch (Report.Id)
            {
                case ReportId.B3AccountHistory:
                    {

                        Report.CrystalReportDocument.SetParameterValue("@P_Date_", m_selectedReportTemplateViewModel.parVm.GetDate().Date.ToString(CultureInfo.InvariantCulture)); //tempdate.Date.ToString(CultureInfo.InvariantCulture)); /*bcvm.parVm.RptParameterDataHandler.Date_*/
                        Report.CrystalReportDocument.SetParameterValue("@SessionID_", m_selectedReportTemplateViewModel.parVm.RptParameterDataHandler.b3Session.Number);
                        Report.CrystalReportDocument.SetParameterValue("@AccountNumber", m_selectedReportTemplateViewModel.parVm.RptParameterDataHandler.b3AccountNumber);
                        break;
                    }
                case ReportId.B3Accounts:
                    {

                        var testr = GetMonthEquivValue(m_selectedReportTemplateViewModel.parVm.MonthSelected) + 1;
                        Report.CrystalReportDocument.SetParameterValue("@nMonth", GetMonthEquivValue(m_selectedReportTemplateViewModel.parVm.MonthSelected) + 1);
                        Report.CrystalReportDocument.SetParameterValue("@nYear", m_selectedReportTemplateViewModel.parVm.YearSelected.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3BallCallByGame:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@session", m_selectedReportTemplateViewModel.parVm.RptParameterDataHandler.b3Session.Number);
                        Report.CrystalReportDocument.SetParameterValue("@DateParameter", m_selectedReportTemplateViewModel.parVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        break;

                    }
                case ReportId.B3BallCallBySession:
                    {
                        var startdate = m_selectedReportTemplateViewModel.parVm.StartDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        var enddate = m_selectedReportTemplateViewModel.parVm.EndDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        Report.CrystalReportDocument.SetParameterValue("@StartDate", startdate);
                        Report.CrystalReportDocument.SetParameterValue("@EndDate", enddate);
                        break;
                    }
                case ReportId.B3BingoCardReport:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@startId", m_selectedReportTemplateViewModel.parVm.RptParameterDataHandler.b3StartingCard);
                        Report.CrystalReportDocument.SetParameterValue("@endId", m_selectedReportTemplateViewModel.parVm.RptParameterDataHandler.b3EndingCard);
                        break;
                    }
                case ReportId.B3Daily:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@SessionNum", null);
                        Report.CrystalReportDocument.SetParameterValue("@UserId", m_selectedReportTemplateViewModel.ReportTemplate_Model.CurrentUser);
                        Report.CrystalReportDocument.SetParameterValue("@Station", m_selectedReportTemplateViewModel.ReportTemplate_Model.CurrentMachine);
                        Report.CrystalReportDocument.SetParameterValue("@DateTime", m_selectedReportTemplateViewModel.parVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3Detail:
                    {
                        var startdate = m_selectedReportTemplateViewModel.parVm.StartDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        var enddate = m_selectedReportTemplateViewModel.parVm.EndDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        Report.CrystalReportDocument.SetParameterValue("@dtStartDateTime", startdate);
                        Report.CrystalReportDocument.SetParameterValue("@dtEndDateTime", enddate);
                        break;
                    }
                case ReportId.B3Drawer://No data: Issue on clientmac(This report need fix)
                    {
                        Report.CrystalReportDocument.SetParameterValue("@MachineID", machineId);
                        Report.CrystalReportDocument.SetParameterValue("@Station", machineId);
                        Report.CrystalReportDocument.SetParameterValue("@nDate", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                        Report.CrystalReportDocument.SetParameterValue("@UserId", m_selectedReportTemplateViewModel.ReportTemplate_Model.CurrentUser);
                        break;
                    }
                case ReportId.B3Jackpot:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@nSessNum", m_selectedReportTemplateViewModel.parVm.RptParameterDataHandler.b3Session.Number);
                        Report.CrystalReportDocument.SetParameterValue("@UserId", userId);
                        Report.CrystalReportDocument.SetParameterValue("@Station", machineId);
                        Report.CrystalReportDocument.SetParameterValue("@nDate", m_selectedReportTemplateViewModel.parVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3Monthly:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@nMonth", GetMonthEquivValue(m_selectedReportTemplateViewModel.parVm.MonthSelected));//bcvm.parVm.RptParameterDataHandler.dateMonth);
                        Report.CrystalReportDocument.SetParameterValue("@nYear", m_selectedReportTemplateViewModel.parVm.YearSelected.ToString(CultureInfo.InvariantCulture)); //bcvm.parVm.RptParameterDataHandler.dateYear);                      
                        break;
                    }
                case ReportId.B3Session:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@SessionID", m_selectedReportTemplateViewModel.parVm.RptParameterDataHandler.b3Session.Number);
                        Report.CrystalReportDocument.SetParameterValue("@DateN", m_selectedReportTemplateViewModel.parVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        Report.CrystalReportDocument.SetParameterValue("@UserID", userId);
                        Report.CrystalReportDocument.SetParameterValue("@Station", machineId);
                        break;
                    }

                case ReportId.B3SessionSummary:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@SessionN", m_selectedReportTemplateViewModel.parVm.RptParameterDataHandler.b3Session.Number);
                        Report.CrystalReportDocument.SetParameterValue("@DateTime", m_selectedReportTemplateViewModel.parVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        Report.CrystalReportDocument.SetParameterValue("@UserID", userId);
                        Report.CrystalReportDocument.SetParameterValue("@Station", machineId);
                        break;
                    }
                case ReportId.B3SessionTransaction:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@SessionNumber", m_selectedReportTemplateViewModel.parVm.RptParameterDataHandler.b3Session.Number);
                        Report.CrystalReportDocument.SetParameterValue("@DateTime", m_selectedReportTemplateViewModel.parVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                case ReportId.B3Void:
                    {
                        var startdate = m_selectedReportTemplateViewModel.parVm.StartDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        var enddate = m_selectedReportTemplateViewModel.parVm.EndDatePickerVm.DatepickerModel.DateFullwTime.ToString(CultureInfo.InvariantCulture);
                        Report.CrystalReportDocument.SetParameterValue("@dtStartDateTime", startdate);
                        Report.CrystalReportDocument.SetParameterValue("@dtEndDateTime", enddate);
                        break;
                    }
                case ReportId.B3WinnerCards:
                    {
                        Report.CrystalReportDocument.SetParameterValue("@SessionNum", m_selectedReportTemplateViewModel.parVm.RptParameterDataHandler.b3Session.Number);
                        Report.CrystalReportDocument.SetParameterValue("@DateRun", m_selectedReportTemplateViewModel.parVm.GetDate().Date.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
            }
            return Report.CrystalReportDocument;
        }

        private void LoadCrystalReport(B3Report report)
        {
            var server = "b3-server";//m_controller.Settings.DatabaseServer;
            var name = m_controller.Settings.DatabaseName;
            var user = m_controller.Settings.DatabaseUser;
            var password = "cobalt$45";//m_controller.Settings.DatabasePassword;
            report.LoadCrystalReport(server, name, user, password);
        }

        public void ViewReportRel(ReportId reportID)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {            
                    var Rpt = m_reports.FirstOrDefault(r => r.Id == reportID);
                    if (Rpt == null) { return; }
                    LoadCrystalReport(Rpt);
                    var report = LoadReportDocument(Rpt);

                    if (report == null)
                    {
                        IsLoading = false;
                        return;
                    }

                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                       
                        tempcr.ToggleSidePanel = Constants.SidePanelKind.None;
                        tempcr.ViewerCore.ReportSource = report;
                        tempcr.Focusable = true;
                        tempcr.Focus();
                    }));
                }
                catch (Exception ex)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        MessageWindow.Show(
                            string.Format(CultureInfo.CurrentCulture, Properties.Resources.ErrorLoadingReport,
                                ex.Message), Properties.Resources.B3CenterName, MessageWindowType.Close);
                    }));
                }
                finally
                {
                    IsLoading = false;
               
                }
            });

            DefaultViewMode = Visibility.Collapsed;
            CRViewMode = Visibility.Visible;
            m_selectedReportTemplateViewModel.vReportViewer = tempcr;
        }

        //NOTE: in order for the report to print in a particular printer name in your network,
        //you should manually set the page setup -> Printer Options -> No printer name  false(uncheck) .
        //(It dont matter what printer name you pick as long as it is uncheck).
        //Also check the : Diassociate Formatting Page Size and Adjust Automatically to true(check).
        public void StartPrintReport(ReportId reportID)
        {
            var Rpt = m_reports.FirstOrDefault(r => r.Id == reportID);
            if (Rpt == null) { return; }
            LoadCrystalReport(Rpt);
            var report = LoadReportDocument(Rpt);

            if (!PrintReport(reportID, report))//Try printing the report
            {
                {
                    PrintDialog printDialog = new PrintDialog();

                    if (printDialog.ShowDialog() == true)
                    {
                        report.PrintOptions.PrinterName = printDialog.PrintQueue.Name;
                        report.PrintToPrinter(1, true, 0, 0);
                    }
                }
            }
        }

        internal bool PrintReport(ReportId reportId, ReportDocument report)
        {
            var returnValue = false;
            switch (reportId)
            {
                case ReportId.B3AccountHistory:
                case ReportId.B3Accounts:
                case ReportId.B3Detail:
                case ReportId.B3Monthly:
                case ReportId.B3Void:
                case ReportId.B3BingoCardReport:
                case ReportId.B3BallCallByGame:
                case ReportId.B3BallCallBySession:
                case ReportId.B3WinnerCards:
                case ReportId.B3SessionTransaction:
                    {
                        returnValue = TryPrintGlobalPrinter(report);
                    }
                    break;

                case ReportId.B3Daily:
                case ReportId.B3Drawer:
                case ReportId.B3Jackpot:
                case ReportId.B3Session:
                case ReportId.B3SessionSummary:

                    {
                        returnValue = TryPrintReceiptPrinter(report); //try to print to receipt printer
                        if (!returnValue)  //if unsuccessful then try to print to report printer
                        {
                            returnValue = TryPrintReceiptPrinter(report);
                        }
                    }
                    break;
            }

            return returnValue;
        }

        //Epson
        private bool TryPrintReceiptPrinter(ReportDocument report)
        {
            var returnValue = true;
            try
            {
                PageMargins margins;
                margins = report.PrintOptions.PageMargins;
                margins.bottomMargin = 0;
                margins.leftMargin = 0;
                margins.rightMargin = 0;
                margins.topMargin = 0;
                report.PrintOptions.PaperSource = PaperSource.Auto;
                report.PrintOptions.ApplyPageMargins(margins);
                report.PrintOptions.DissociatePageSizeAndPrinterPaperSize = false;
                report.PrintOptions.PrinterName = Settings.ReceiptPrinterName;
                report.PrintToPrinter(1, true, 0, 0);
            }
            catch (Exception)
            {
                returnValue = false;
            }

            return returnValue;
        }

        private bool TryPrintGlobalPrinter(ReportDocument report)
        {
            var returnValue = true;
            try
            {
                report.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true;
                report.PrintOptions.PrinterName = Settings.PrinterName;
                report.PrintToPrinter(1, true, 0, 0);
            }
            catch (Exception)
            {
                returnValue = false;
            }

            return returnValue;
        }
        #endregion

        #endregion
        #region PROPERTIES

        public ReportTemplateViewModel b3AccountVm { get; set; }
        public ReportTemplateViewModel b3DailyVm { get; set; }
        public ReportTemplateViewModel b3DetailVm { get; set; }
        public ReportTemplateViewModel b3AccountHistVm { get; set; }
        public ReportTemplateViewModel b3CardVm { get; set; }
        public ReportTemplateViewModel b3BallCallbyGameVm { get; set; }
        public ReportTemplateViewModel b3BallCallbySessionVm { get; set; }
        public ReportTemplateViewModel b3DrawerVm { get; set; }
        public ReportTemplateViewModel b3JackpotVm { get; set; }
        public ReportTemplateViewModel b3MonthlyVm { get; set; }
        public ReportTemplateViewModel b3SessionVm { get; set; }
        public ReportTemplateViewModel b3SessionSummaryVm { get; set; }
        public ReportTemplateViewModel b3SessionTransactionVm { get; set; }
        public ReportTemplateViewModel b3VoidVm { get; set; }
        public ReportTemplateViewModel b3WinnersCardVm { get; set; }

        private ObservableCollection<ReportMain> m_reportCollection = new ObservableCollection<ReportMain>();
        public ObservableCollection<ReportMain> ReportListCol
        {
            get { return m_reportCollection; }
            set { m_reportCollection = value; }
        }

        private ReportMain m_selectedReportColl;
        public ReportMain SelectedReportColl
        {
            get { return m_selectedReportColl; }
            set
            {
                m_selectedReportColl = value;
                SelectedReportViewCol = value.rptView;
                m_selectedReportTemplateViewModel = value.rpttemplatevm;
                m_reportSelected = value.B3Reports;
                RaisePropertyChanged("SelectedReportColl");
            }
        }

        private ReportTemplateViewModel m_selectedReportTemplateViewModel { get; set; }

        public ReportTemplate SelectedReportViewCol
        {
            get { return m_selectedReportColl.rptView; }
            set
            {
                m_selectedReportColl.rptView = value;
                RaisePropertyChanged("SelectedReportViewCol");
            }
        }

        private B3Report m_reportSelected;
        public B3Report ReportSelected
        {
            get { return m_reportSelected; }
            set
            {
                m_reportSelected = value;
                RaisePropertyChanged("ReportSelected");
            }
        }

        private bool m_viewReportVisibility;
        public bool ViewReportVisibility
        {
            get { return m_viewReportVisibility; }
            set
            {
                m_viewReportVisibility = value;
                RaisePropertyChanged("ViewReportVisibility");
            }
        }
   
        
        public Visibility CRViewMode
        {
            get
            {
                return m_selectedReportTemplateViewModel.ReportViewerVisibility;
            }
            set
            {
                m_selectedReportTemplateViewModel.ReportViewerVisibility = value;
                RaisePropertyChanged("CRViewMode");
            }
        }

        public Visibility DefaultViewMode
        {
            get
            {
                return Visibility.Visible; //m_rptBaseVm.ReportParameterVisible;
            }
            set
            {
                m_selectedReportTemplateViewModel.ReportParameterVisible = value;
                RaisePropertyChanged("DefaultViewMode");
            }

        }

        /// <summary>
        /// Gets or sets the session list.
        /// </summary>
        /// <value>
        /// The session list.
        /// </value>
        public ObservableCollection<Session> SessionList
        {
            get
            {
                return m_sessionList;
            }
            set
            {
                m_sessionList = value;
                RaisePropertyChanged("SessionList");
            }
        }



        /// <summary>
        /// Gets or sets a value indicating whether a report is loading.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is loading; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoading
        {
            get
            {
                return m_isLoading;
            }
            set
            {
                m_isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a report is loading.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is loading; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrinting
        {
            get
            {
                return m_isPrinting;
            }
            set
            {
                m_isPrinting = value;
                RaisePropertyChanged("IsPrinting");
            }
        }

        internal B3CenterSettings Settings
        {
            get { return m_controller.Settings; }
        }

        #endregion
        #region EVENTS
        /// <summary>
        /// Occurs when [full screen event].
        /// </summary>
        public event EventHandler<EventArgs> FullScreenEvent;

        /// <summary>
        /// Occurs when [exit screen event].
        /// </summary>
        public event EventHandler<EventArgs> ExitScreenEvent;
        

        /// <summary>
        /// Called when [full screen event].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnFullScreenEvent(object sender, EventArgs eventArgs)
        {
  
            var handler = FullScreenEvent;
            if (handler != null)
            {
                handler(sender, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Called when [exit screen event].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnExitScreenEvent(object sender, EventArgs eventArgs)
        {

            var handler = ExitScreenEvent;
            if (handler != null)
            {
                handler(sender, EventArgs.Empty);
            }
        }

    
    /// <summary>
        /// Called when [list information done].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="AsyncCompletedEventArgs"/> instance containing the event data.</param>
        private void OnListInfoDone(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (m_controller.Sessions.Count != SessionList.Count)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        SessionList.Clear();
                        foreach (var session in m_controller.Sessions)
                        {
                            SessionList.Add(session);
                        }

                        //SessionReportSessionSelected = SessionList.LastOrDefault();
                        //JackpotReportSessionSelected = SessionList.LastOrDefault();
                    }));
                }
            }
        }
        #endregion
        #region COMMAND

        public ICommand ViewReportCommand { get; set; }
        public ICommand PrintReportCommand { get; set; }

        private void SetCommand()
        {
            ViewReportCommand = new RelayCommand(parameter => ViewReportRel(m_reportSelected.Id));
            PrintReportCommand = new RelayCommand(parameter => StartPrintReport(m_reportSelected.Id)); 
        }

    
        #endregion
    }
}


#region SCRATCH

/// <summary>
/// Loads the ball call report document.
/// </summary>
/// <returns></returns>
//internal ReportDocument LoadBallCallReportDocument(DateTime startDate, DateTime endDate, int ballCalldefID)
//{
//    var BallCallReport = m_reports.FirstOrDefault(r => r.Id == ReportId.B3BallCallByGame);

//    if (ballCalldefID == 1)
//    {
//        if (BallCallReport == null)
//        {
//            return null;
//        }

//        LoadCrystalReport(BallCallReport);

//        if (BallCallReportSessionSelected == null)
//        {
//            return null;
//        }

//        var sessionId = BallCallReportSessionSelected.Number;

//        BallCallReport.CrystalReportDocument.SetParameterValue("@session", sessionId);
//        BallCallReport.CrystalReportDocument.SetParameterValue("@DateParameter", startDate.Date.ToString(CultureInfo.InvariantCulture));


//    }
//    else
//    {
//        BallCallReport = m_reports.FirstOrDefault(r => r.Id == ReportId.B3BallCallBySession);

//        if (BallCallReport == null)
//        {
//            return null;
//        }

//        LoadCrystalReport(BallCallReport);
//        BallCallReport.CrystalReportDocument.SetParameterValue("@StartDate", startDate.Date.ToString(CultureInfo.InvariantCulture));
//        BallCallReport.CrystalReportDocument.SetParameterValue("@EndDate",  endDate.Date.ToString(CultureInfo.InvariantCulture));

//    }

//    return BallCallReport.CrystalReportDocument;
//}



#endregion