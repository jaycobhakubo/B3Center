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
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Windows.Threading;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Reports;
using GameTech.Elite.Client.Modules.B3Center.Model.Report;
using GameTech.Elite.UI;
using GameTech.Elite.Client.Modules.B3Center.UI.ReportViews;
using GameTech.Elite.Client.Modules.B3Center.Model;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using SAPBusinessObjects.WPF.Viewer;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{
    public class ReportsViewModel : ViewModelBase
    {
        #region Private Members

        private bool m_isLoading;
        private bool m_isPrinting;
        private B3Controller m_controller;
        private ObservableCollection<Session> m_sessionList;
        private List<B3Report> m_reports;
        private bool m_isRngBallCall;
        private B3CenterSettings Settings
        {
            get { return m_controller.Settings; }
        }

        #endregion

        #region Constructors

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
            m_isRngBallCall = !controller.Settings.IsCommonRngBallCall;

            foreach (var session in controller.Sessions)
            {
                SessionList.Add(session);
            }

            var B3Accounts = new ReportTemplateViewModel(GetReportModel(ReportId.B3Accounts));
            var B3Daily = new ReportTemplateViewModel(GetReportModel(ReportId.B3Daily));
            var B3Detail = new ReportTemplateViewModel(GetReportModel(ReportId.B3Detail));
            var B3Monthly = new ReportTemplateViewModel(GetReportModel(ReportId.B3Monthly));
            var B3Void = new ReportTemplateViewModel(GetReportModel(ReportId.B3Void));
            var B3Drawer = new ReportTemplateViewModel(GetReportModel(ReportId.B3Drawer));
            var B3Jackpot = new ReportTemplateViewModel(GetReportModel(ReportId.B3Jackpot));
            var B3Session = new ReportTemplateViewModel(GetReportModel(ReportId.B3Session));
            var B3BallCallBySession = new ReportTemplateViewModel(GetReportModel(ReportId.B3BallCallBySession));
            var B3BallCallByGame = new ReportTemplateViewModel(GetReportModel(ReportId.B3BallCallByGame));
            var B3SessionTransaction = new ReportTemplateViewModel(GetReportModel(ReportId.B3SessionTransaction));
            var B3SessionSummary = new ReportTemplateViewModel(GetReportModel(ReportId.B3SessionSummary));
            var B3WinnerCards = new ReportTemplateViewModel(GetReportModel(ReportId.B3WinnerCards));
            var B3AccountHistory = new ReportTemplateViewModel(GetReportModel(ReportId.B3AccountHistory));
            var B3BingoCardReport = new ReportTemplateViewModel(GetReportModel(ReportId.B3BingoCardReport));

            m_reportCollection = new ObservableCollection<ReportMain>()
            {
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3Accounts), ReportDisplayName = "Accounts", RptTemplateVM = B3Accounts, RptView = new ReportTemplate(B3Accounts)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3Daily), ReportDisplayName = "Daily", RptTemplateVM = B3Daily, RptView = new ReportTemplate(B3Daily)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3Detail), ReportDisplayName = "Detail", RptTemplateVM = B3Detail, RptView = new ReportTemplate(B3Detail)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3Monthly), ReportDisplayName = "Monthly", RptTemplateVM = B3Monthly, RptView = new ReportTemplate(B3Monthly)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3Void), ReportDisplayName = "Void", RptTemplateVM = B3Void, RptView = new ReportTemplate(B3Void)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3Drawer), ReportDisplayName = "Drawer", RptTemplateVM = B3Drawer, RptView = new ReportTemplate(B3Drawer)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3Jackpot), ReportDisplayName ="Jackpot ", RptTemplateVM = B3Jackpot, RptView = new ReportTemplate(B3Jackpot)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3Session), ReportDisplayName ="Session ", RptTemplateVM = B3Session, RptView = new ReportTemplate(B3Session)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3BallCallBySession), ReportDisplayName = "BallCall by session", RptTemplateVM = B3BallCallBySession, RptView = new ReportTemplate(B3BallCallBySession)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3BallCallByGame), ReportDisplayName = "BallCall by game", RptTemplateVM = B3BallCallByGame, RptView = new ReportTemplate(B3BallCallByGame)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3SessionTransaction), ReportDisplayName = "Session Transaction", RptTemplateVM = B3SessionTransaction, RptView = new ReportTemplate(B3SessionTransaction)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3SessionSummary), ReportDisplayName = "Session Summary", RptTemplateVM = B3SessionSummary, RptView = new ReportTemplate(B3SessionSummary)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3WinnerCards), ReportDisplayName = "Winner Cards", RptTemplateVM = B3WinnerCards, RptView = new ReportTemplate(B3WinnerCards)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3AccountHistory), ReportDisplayName = "Account History", RptTemplateVM = B3AccountHistory, RptView = new ReportTemplate(B3AccountHistory)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3BingoCardReport), ReportDisplayName = "Bingo Card", RptTemplateVM = B3BingoCardReport, RptView = new ReportTemplate(B3BingoCardReport)}, 
            };

            SetBallCallReportBySessionOrByGame(controller.Settings.IsCommonRngBallCall);//Set our ball call report 
            m_reportCollection = new ObservableCollection<ReportMain>(m_reportCollection.OrderBy(l => l.ReportDisplayName));
            SelectedReportColl = m_reportCollection.FirstOrDefault();
            InitCommands();
        }

        #endregion

        #region Static Accessor

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
                        if (m_instance == null) // double-check just in case
                            m_instance = new ReportsViewModel();
                    }
                }
                return m_instance;
            }
        }

        #endregion

        #region Methods

        #region Private

        //Set our data to be popualted to ReportTemplateViewModel then to the viewer.
        //This will be replaced by a observable collection if I got more time. Dont know which is faster.
        /// <summary>
        /// Returns the report for the sent-in report ID
        /// </summary>
        /// <param name="b3rpt"></param>
        /// <returns></returns>
        private ReportTemplateModel GetReportModel(ReportId b3rpt)
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
                        result.ReportTitle = "Accounts Outstanding";
                        break;
                    }
                case ReportId.B3AccountHistory:
                    {
                        result.ReportTitle = "Account History";
                        par.Add("Date");
                        par.Add("Session");
                        par.Add("AccountNumber");
                        break;
                    }
                case ReportId.B3BallCallByGame:
                    {
                        result.ReportTitle = "Ball Call(by game)";
                        par.Add("Date");
                        par.Add("Session");
                        break;
                    }
                case ReportId.B3BallCallBySession:
                    {
                        result.ReportTitle = "Ball Call(by session)";
                        temprptparmodel.StartDate = new Model.Shared.DatePickerM();
                        temprptparmodel.EndDate = new Model.Shared.DatePickerM();
                        par.Add("StartEndDate");
                        break;
                    }
                case ReportId.B3BingoCardReport:
                    {
                        result.ReportTitle = "Bingo Card";
                        par.Add("StartEndCard");
                        break;
                    }
                case ReportId.B3Daily:
                    {
                        result.ReportTitle = "Daily";
                        par.Add("Date");
                        break;
                    }
                case ReportId.B3Detail:
                    {
                        temprptparmodel.StartDate = new Model.Shared.DatePickerM();
                        temprptparmodel.EndDate = new Model.Shared.DatePickerM();
                        result.ReportTitle = "Detail";
                        par.Add("StartEndDatewTime");
                        break;
                    }
                case ReportId.B3Drawer:
                    {
                        result.ReportTitle = "Drawer";
                        break;
                    }
                case ReportId.B3Jackpot:
                    {
                        result.ReportTitle = "Jackpot";
                        par.Add("Date");
                        par.Add("Session");
                        break;
                    }
                case ReportId.B3Monthly:
                    {
                        result.ReportTitle = "Monthly";
                        par.Add("MonthYear");
                        break;
                    }
                case ReportId.B3Session:
                    {
                        result.ReportTitle = "Session";
                        par.Add("Date");
                        par.Add("Session");
                        break;
                    }
                case ReportId.B3SessionSummary:
                    {
                        result.ReportTitle = "Session Summary";
                        par.Add("Date");
                        par.Add("Session");
                        break;
                    }
                case ReportId.B3SessionTransaction:
                    {
                        result.ReportTitle = "Session Transaction";
                        par.Add("Date");
                        par.Add("Session");
                        break;
                    }
                case ReportId.B3Void:
                    {
                        temprptparmodel.StartDate = new Model.Shared.DatePickerM();
                        temprptparmodel.EndDate = new Model.Shared.DatePickerM();
                        result.ReportTitle = "Void";
                        par.Add("StartEndDatewTime");
                        break;
                    }
                case ReportId.B3WinnerCards:
                    {
                        result.ReportTitle = "Winners Card";
                        par.Add("Date");
                        par.Add("Session");
                        break;
                    }
                default:
                    {
                        B3CenterController.Logger.Log(String.Format("Unable to find report for id: '{0}'", b3rpt), LoggerLevel.Warning);
                        break;
                    }
            }

            if (!String.IsNullOrWhiteSpace(result.ReportTitle)) // Moved duplicate case functionality into shared step. Don't set data for invalid reports
            {
                temprptparmodel.rptid = b3rpt;
                result.ReportParameter = par;
            }

            result.ReportViewerm = Visibility.Collapsed;
            result.DefaultViewerm = Visibility.Visible;
            result.RptParamModel = temprptparmodel;
            return result;
        }

        #endregion

        #region Public

        //Check which ball call report to show base on RNGBallCall setting.
        //This code can be simplified even better.
        public void SetBallCallReportBySessionOrByGame(bool SettingRngBallCall)
        {
            if (m_isRngBallCall != SettingRngBallCall)
            {
                Task.Factory.StartNew(() =>
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        //ReportSelectedIndex = -1;
                        if (SettingRngBallCall == true)//ball call game by session
                        {
                            var ReportMainBallCallByGame = m_reportCollection.Single(l => l.B3Reports.Id == ReportId.B3BallCallByGame);
                            var tempresultToList = m_reportCollection.ToList();
                            tempresultToList.Remove(ReportMainBallCallByGame);

                            var exists = tempresultToList.Exists(l => l.B3Reports.Id == ReportId.B3BallCallBySession);       //check if exists 
                            if (exists != true)
                            {
                                ReportTemplateViewModel B3BallCallBySession = new ReportTemplateViewModel(GetReportModel(ReportId.B3BallCallBySession));
                                ReportMain x = new ReportMain() { B3Reports = m_reports.Single(l => l.Id == ReportId.B3BallCallBySession), ReportDisplayName = "BallCall by session", RptTemplateVM = B3BallCallBySession, RptView = new ReportTemplate(B3BallCallBySession) };
                                tempresultToList.Add(x);
                            }
                            ReportListCol = new ObservableCollection<ReportMain>(tempresultToList.OrderBy(l => l.ReportDisplayName));
                        }
                        else
                        {
                            var ReportMainBallCallBySession = m_reportCollection.Single(l => l.B3Reports.Id == ReportId.B3BallCallBySession);
                            var tempresultToList = m_reportCollection.ToList();
                            tempresultToList.Remove(ReportMainBallCallBySession);

                            //check if exists 
                            var exists = tempresultToList.Exists(l => l.B3Reports.Id == ReportId.B3BallCallByGame);
                            if (exists != true)
                            {
                                ReportTemplateViewModel B3BallCallByGame = new ReportTemplateViewModel(GetReportModel(ReportId.B3BallCallByGame));
                                ReportMain xy = new ReportMain() { B3Reports = m_reports.Single(l => l.Id == ReportId.B3BallCallByGame), ReportDisplayName = "BallCall by game", RptTemplateVM = B3BallCallByGame, RptView = new ReportTemplate(B3BallCallByGame) };
                                tempresultToList.Add(xy);
                            }
                            ReportListCol = new ObservableCollection<ReportMain>(tempresultToList.OrderBy(l => l.ReportDisplayName));
                        }
                        m_isRngBallCall = SettingRngBallCall;
                    }));
                });
            }
        }
        
        #endregion

        #region Event Handlers

        /// <summary>
        /// Actions that occur when the selected date changes for the report
        /// </summary>
        /// <param name="updatedDateTime"></param>
        public void UpdateItemDateSelected(DateTime updatedDateTime)
        {
            if (m_selectedReportColl.B3Reports != null)
            {
                var reportID = SelectedReportColl.B3Reports.Id;
                if (reportID == ReportId.B3AccountHistory
                    || reportID == ReportId.B3BallCallByGame
                    || reportID == ReportId.B3Jackpot
                    || reportID == ReportId.B3Session
                    || reportID == ReportId.B3SessionSummary
                    || reportID == ReportId.B3SessionTransaction
                    || reportID == ReportId.B3WinnerCards
                    )
                {
                    SelectedReportColl.RptTemplateVM.ParamVm.UpdateSessionList(updatedDateTime);
                }
                SelectedReportColl.RptTemplateVM.ParamVm.CheckUserValidation(); //Just check user validation no need to filter it shouldnt be that much.      
            }
        }

        /// <summary>
        /// Actions that occur when the selected report changes
        /// </summary>
        public void SelectionChange()
        {
            if (m_reportSelected.Id == ReportId.B3BingoCardReport)
            {
                ViewReportVisibility = false;
            }
            else
            {
                SelectedReportColl.RptTemplateVM.ParamVm.CheckUserValidation();
            }
            SetLabelMessageToUser();
        }

        public void SetLabelMessageToUser()
        {
            //Indicator visibility
            if (m_reportSelected != null)
            {
                var reportID = m_reportSelected.Id;
                if (reportID == ReportId.B3AccountHistory
                    || reportID == ReportId.B3BallCallByGame
                    || reportID == ReportId.B3Jackpot
                    || reportID == ReportId.B3Session
                    || reportID == ReportId.B3SessionSummary
                    || reportID == ReportId.B3SessionTransaction
                    || reportID == ReportId.B3WinnerCards
                    || reportID == ReportId.B3BingoCardReport
                    )
                {
                    IndicatorVisibility = true;
                    if (ReportId.B3BingoCardReport != reportID)
                    {
                        var sessSelected = SelectedReportColl.RptTemplateVM.ParamVm.SelectedSession;
                        if (sessSelected == null)
                        {
                            NoSession = true;
                            NoAccount = true;
                        }
                        else
                        {
                            if (ReportId.B3AccountHistory == reportID)
                            {
                                var accountSelected = SelectedReportColl.RptTemplateVM.ParamVm.AccountSelected;
                                if (accountSelected == null)
                                {
                                    NoSession = false;
                                    NoAccount = true;
                                }
                                else
                                {
                                    IndicatorVisibility = false;
                                }
                            }
                            else
                            {
                                IndicatorVisibility = false;
                            }
                        }
                    }
                    else
                    {
                        IndicatorVisibility = true;
                        NoSession = false;
                        NoAccount = false;
                    }
                }
                else
                {
                    IndicatorVisibility = false;
                }
            }
        }

        #endregion

        #region Displaying Reports

        private void LoadCrystalReport(B3Report report)
        {
            var server = m_controller.Settings.DatabaseServer;
            var name = m_controller.Settings.DatabaseName;
            var user = m_controller.Settings.DatabaseUser;
            var password = m_controller.Settings.DatabasePassword;
            report.LoadCrystalReport(server, name, user, password);
        }

        public void CloseReportAbortOperation()
        {
            if (thread != null)
                thread.Abort();
        }

        System.Threading.Thread thread;
        private DispatcherOperation crRun;

        public void ViewReportRel(ReportId reportID)
        {
            try
            {
                var Rpt = m_reports.FirstOrDefault(r => r.Id == reportID);
                if (Rpt == null) { return; }
                LoadCrystalReport(Rpt);
                var m_rptDoc = m_selectedReportTemplateViewModel.LoadReportDocument(Rpt);
                
                thread = new System.Threading.Thread(
                    new System.Threading.ThreadStart(
                        delegate()
                        {
                            if (!SelectedReportViewCol.CrViewer.ViewerCore.Dispatcher.CheckAccess())
                            {
                                crRun = SelectedReportViewCol.CrViewer.ViewerCore.Dispatcher.BeginInvoke(
                                    DispatcherPriority.Background, new Action(
                                        delegate()
                                        {
                                            SelectedReportViewCol.CrViewer.ViewerCore.ReportSource = m_rptDoc;
                                            SelectedReportViewCol.CrViewer.Owner = Window.GetWindow(SelectedReportViewCol); // DE13491 need a parent window to display errors on top of
                                        }
                                        ));
                                crRun.Wait();
                            }
                            else
                            {
                                SelectedReportViewCol.CrViewer.ViewerCore.ReportSource = m_rptDoc;
                            }
                        }
                        ));
                
                thread.Start();

                DefaultViewMode = Visibility.Collapsed;
                CRViewMode = Visibility.Visible;
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
            finally { IsLoading = false; }
        }
        #endregion
        
        #region Printing Reports

        /// <summary>
        /// NOTE: in order for the report to print to a particular printer name in your network,
        /// you should manually set the page setup -> Printer Options -> No printer name  false(uncheck) .
        /// (It doesn't matter what printer name you pick as long as it is unchecked).
        /// Also check the : Diassociate Formatting Page Size and Adjust Automatically to true(check).
        /// </summary>
        /// <param name="reportID"></param>
        public void StartPrintReport(ReportId reportID)
        {
            var Rpt = m_reports.FirstOrDefault(r => r.Id == reportID);
            if (Rpt == null) { return; }
            LoadCrystalReport(Rpt);
            var report = m_selectedReportTemplateViewModel.LoadReportDocument(Rpt);

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
            catch (Exception ex)
            {
                MessageWindow.Show(
                    string.Format(CultureInfo.CurrentCulture, Properties.Resources.ErrorPrintingReport,
                    ex.Message), Properties.Resources.B3CenterName, MessageWindowType.Close);

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
            catch (Exception ex)
            {
                MessageWindow.Show(
                    string.Format(CultureInfo.CurrentCulture, Properties.Resources.ErrorPrintingReport,
                    ex.Message), Properties.Resources.B3CenterName, MessageWindowType.Close);

                returnValue = false;
            }

            return returnValue;
        }
        #endregion

        #endregion

        #region Public Properties

        private int m_reportSelectedIndex;
        public int ReportSelectedIndex
        {
            get { return m_reportSelectedIndex; }
            set
            {
                if (value != m_reportSelectedIndex)
                {
                    m_reportSelectedIndex = value;
                    RaisePropertyChanged("ReportSelectedIndex");
                }
            }
        }

        private bool m_noSession;
        public bool NoSession
        {
            get { return m_noSession; }
            set
            {
                m_noSession = value;
                RaisePropertyChanged("NoSession");
            }
        }

        private bool m_noAccount;
        public bool NoAccount
        {
            get { return m_noAccount; }
            set
            {
                m_noAccount = value;
                RaisePropertyChanged("NoAccount");
            }
        }

        private bool m_indicatorVisibility;
        public bool IndicatorVisibility
        {
            get { return m_indicatorVisibility; }
            set
            {
                if (m_indicatorVisibility != value)
                {
                    m_indicatorVisibility = value;
                    RaisePropertyChanged("IndicatorVisibility");
                }
            }
        }

        private ObservableCollection<ReportMain> m_reportCollection = new ObservableCollection<ReportMain>();
        public ObservableCollection<ReportMain> ReportListCol
        {
            get { return m_reportCollection; }
            set
            {
                m_reportCollection = value;
                RaisePropertyChanged("ReportListCol");
            }
        }

        private ReportMain m_selectedReportColl;
        public ReportMain SelectedReportColl
        {
            get { return m_selectedReportColl; }
            set
            {
                if (value != null)
                {
                    m_selectedReportColl = value;
                    SelectedReportViewCol = value.RptView;
                    m_selectedReportTemplateViewModel = value.RptTemplateVM;
                    m_reportSelected = value.B3Reports;
                    RaisePropertyChanged("SelectedReportColl");
                }
            }
        }

        private ReportTemplateViewModel m_selectedReportTemplateViewModel { get; set; }

        public ReportTemplate SelectedReportViewCol
        {
            get { return m_selectedReportColl.RptView; }
            set
            {
                m_selectedReportColl.RptView = value;
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
            get { return m_selectedReportTemplateViewModel.ReportViewerVisibility; }
            set
            {
                if (value != m_selectedReportTemplateViewModel.ReportViewerVisibility)
                {
                    m_selectedReportTemplateViewModel.ReportViewerVisibility = value;
                    RaisePropertyChanged("CRViewMode");
                }
            }
        }

        public Visibility DefaultViewMode
        {
            get { return m_selectedReportTemplateViewModel.ReportParameterVisible; }
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

        #endregion

        #region Commands

        public ICommand ViewReportCommand { get; set; }
        public ICommand PrintReportCommand { get; set; }

        /// <summary>
        /// Sets up the Command objects to be bound to
        /// </summary>
        private void InitCommands()
        {
            ViewReportCommand = new RelayCommand(parameter => ViewReportRel(m_reportSelected.Id));
            PrintReportCommand = new RelayCommand(parameter => StartPrintReport(m_reportSelected.Id));
        }


        #endregion
    }
}
