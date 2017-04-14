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
        
        System.Threading.Thread m_thread;
        private DispatcherOperation m_crRun;
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
            m_isRngBallCall = controller.Settings.IsCommonRngBallCall;

            foreach (var session in controller.Sessions)
            {
                SessionList.Add(session);
            }

            var b3Accounts = new ReportTemplateViewModel(GetReportModel(ReportId.B3Accounts));
            var b3Daily = new ReportTemplateViewModel(GetReportModel(ReportId.B3Daily));
            var b3Detail = new ReportTemplateViewModel(GetReportModel(ReportId.B3Detail));
            var b3Monthly = new ReportTemplateViewModel(GetReportModel(ReportId.B3Monthly));
            var b3Void = new ReportTemplateViewModel(GetReportModel(ReportId.B3Void));
            var b3Drawer = new ReportTemplateViewModel(GetReportModel(ReportId.B3Drawer));
            var b3Jackpot = new ReportTemplateViewModel(GetReportModel(ReportId.B3Jackpot));
            var b3Session = new ReportTemplateViewModel(GetReportModel(ReportId.B3Session));
            var b3BallCallBySession = new ReportTemplateViewModel(GetReportModel(ReportId.B3BallCallBySession));
            var b3BallCallByGame = new ReportTemplateViewModel(GetReportModel(ReportId.B3BallCallByGame));
            var b3SessionTransaction = new ReportTemplateViewModel(GetReportModel(ReportId.B3SessionTransaction));
            var b3SessionSummary = new ReportTemplateViewModel(GetReportModel(ReportId.B3SessionSummary));
            var b3WinnerCards = new ReportTemplateViewModel(GetReportModel(ReportId.B3WinnerCards));
            var b3AccountHistory = new ReportTemplateViewModel(GetReportModel(ReportId.B3AccountHistory));
            var b3BingoCardReport = new ReportTemplateViewModel(GetReportModel(ReportId.B3BingoCardReport));

            m_reportCollection = new ObservableCollection<ReportMain>()
            {
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3Accounts), ReportDisplayName = "Accounts", RptTemplateVm = b3Accounts, RptView = new ReportTemplate(b3Accounts)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3Daily), ReportDisplayName = "Daily", RptTemplateVm = b3Daily, RptView = new ReportTemplate(b3Daily)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3Detail), ReportDisplayName = "Detail", RptTemplateVm = b3Detail, RptView = new ReportTemplate(b3Detail)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3Monthly), ReportDisplayName = "Monthly", RptTemplateVm = b3Monthly, RptView = new ReportTemplate(b3Monthly)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3Void), ReportDisplayName = "Void", RptTemplateVm = b3Void, RptView = new ReportTemplate(b3Void)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3Drawer), ReportDisplayName = "Drawer", RptTemplateVm = b3Drawer, RptView = new ReportTemplate(b3Drawer)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3Jackpot), ReportDisplayName ="Jackpot ", RptTemplateVm = b3Jackpot, RptView = new ReportTemplate(b3Jackpot)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3Session), ReportDisplayName ="Session ", RptTemplateVm = b3Session, RptView = new ReportTemplate(b3Session)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3BallCallBySession), ReportDisplayName = "BallCall by session", RptTemplateVm = b3BallCallBySession, RptView = new ReportTemplate(b3BallCallBySession)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3BallCallByGame), ReportDisplayName = "BallCall by game", RptTemplateVm = b3BallCallByGame, RptView = new ReportTemplate(b3BallCallByGame)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3SessionTransaction), ReportDisplayName = "Session Transaction", RptTemplateVm = b3SessionTransaction, RptView = new ReportTemplate(b3SessionTransaction)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3SessionSummary), ReportDisplayName = "Session Summary", RptTemplateVm = b3SessionSummary, RptView = new ReportTemplate(b3SessionSummary)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3WinnerCards), ReportDisplayName = "Winner Cards", RptTemplateVm = b3WinnerCards, RptView = new ReportTemplate(b3WinnerCards)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3AccountHistory), ReportDisplayName = "Account History", RptTemplateVm = b3AccountHistory, RptView = new ReportTemplate(b3AccountHistory)},
                new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3BingoCardReport), ReportDisplayName = "Bingo Card", RptTemplateVm = b3BingoCardReport, RptView = new ReportTemplate(b3BingoCardReport)}, 
            };

            SetBallCallReportBySessionOrByGame(m_isRngBallCall);//Set our ball call report 
            m_reportCollection = new ObservableCollection<ReportMain>(m_reportCollection.OrderBy(l => l.ReportDisplayName));
            SelectedReportColl = m_reportCollection.FirstOrDefault();
            InitCommands();
        }

        #endregion

        #region Static Accessor

        private static volatile ReportsViewModel m_instance;
        private static readonly object m_syncRoot = new object();
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
        /// <param name="b3Report"></param>
        /// <returns></returns>
        private ReportTemplateModel GetReportModel(ReportId b3Report)
        {
            var result = new ReportTemplateModel();
            var temprptparmodel = new ReportParameterModel();
            var par = new List<string>();

            temprptparmodel.B3DateData = new Model.Shared.DatePickerM();
            result.CurrentUser = m_controller.Parent.StaffId;
            result.CurrentMachine = m_controller.Parent.MachineId;

            switch (b3Report)
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
                        B3CenterController.Logger.Log(string.Format("Unable to find report for id: '{0}'", b3Report), LoggerLevel.Warning);
                        break;
                    }
            }

            if (!string.IsNullOrWhiteSpace(result.ReportTitle)) // Moved duplicate case functionality into shared step. Don't set data for invalid reports
            {
                temprptparmodel.Rptid = b3Report;
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
        public void SetBallCallReportBySessionOrByGame(bool settingRngBallCall)
        {
            if (settingRngBallCall)//true
            {
                //var reportMainBallCallByGame = m_reportCollection.Single(l => l.B3Reports.Id == ReportId.B3BallCallByGame);
                //m_reportCollection.Remove(reportMainBallCallByGame);
                //ReportTemplateViewModel b3BallCallBySession = new ReportTemplateViewModel(GetReportModel(ReportId.B3BallCallBySession));
                //ReportMain x = new ReportMain() { B3Reports = m_reports.Single(l => l.Id == ReportId.B3BallCallBySession), ReportDisplayName = "BallCall by session", RptTemplateVm = b3BallCallBySession, RptView = new ReportTemplate(b3BallCallBySession) };
                //ReportListCol.Insert();
                //m_reportCollection.Contains();

                //Task.Factory.StartNew(() =>
                //{
                //    Application.Current.Dispatcher.Invoke(new Action(() =>
                //    {
                //ReportSelectedIndex = -1;
                //if (settingRngBallCall)//ball call game by session
                //        {
                //            var reportMainBallCallByGame = m_reportCollection.Single(l => l.B3Reports.Id == ReportId.B3BallCallByGame);
                //            var tempresultToList = m_reportCollection.ToList();
                //            tempresultToList.Remove(reportMainBallCallByGame);

                //            var exists = tempresultToList.Exists(l => l.B3Reports.Id == ReportId.B3BallCallBySession);       //check if exists 
                //            if (exists != true)
                //            {
                //                ReportTemplateViewModel b3BallCallBySession = new ReportTemplateViewModel(GetReportModel(ReportId.B3BallCallBySession));
                //                ReportMain x = new ReportMain() { B3Reports = m_reports.Single(l => l.Id == ReportId.B3BallCallBySession), ReportDisplayName = "BallCall by session", RptTemplateVm = b3BallCallBySession, RptView = new ReportTemplate(b3BallCallBySession) };
                //                tempresultToList.Add(x);
                //            }
                //            ReportListCol = new ObservableCollection<ReportMain>(tempresultToList.OrderBy(l => l.ReportDisplayName));
                //        }
                //        else
                //        {
                //            var reportMainBallCallBySession = m_reportCollection.Single(l => l.B3Reports.Id == ReportId.B3BallCallBySession);
                //            var tempresultToList = m_reportCollection.ToList();
                //            tempresultToList.Remove(reportMainBallCallBySession);

                //            //check if exists 
                //            var exists = tempresultToList.Exists(l => l.B3Reports.Id == ReportId.B3BallCallByGame);
                //            if (exists != true)
                //            {
                //                ReportTemplateViewModel b3BallCallByGame = new ReportTemplateViewModel(GetReportModel(ReportId.B3BallCallByGame));
                //                ReportMain xy = new ReportMain() { B3Reports = m_reports.Single(l => l.Id == ReportId.B3BallCallByGame), ReportDisplayName = "BallCall by game", RptTemplateVm = b3BallCallByGame, RptView = new ReportTemplate(b3BallCallByGame) };
                //                tempresultToList.Add(xy);
                //            }
                //            ReportListCol = new ObservableCollection<ReportMain>(tempresultToList.OrderBy(l => l.ReportDisplayName));
                //        }
                //        m_isRngBallCall = settingRngBallCall;
                //    }));
                //});
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
                var reportId = SelectedReportColl.B3Reports.Id;
                if (reportId == ReportId.B3AccountHistory
                    || reportId == ReportId.B3BallCallByGame
                    || reportId == ReportId.B3Jackpot
                    || reportId == ReportId.B3Session
                    || reportId == ReportId.B3SessionSummary
                    || reportId == ReportId.B3SessionTransaction
                    || reportId == ReportId.B3WinnerCards
                    )
                {
                    SelectedReportColl.RptTemplateVm.ParamVm.UpdateSessionList(updatedDateTime);
                }
                SelectedReportColl.RptTemplateVm.ParamVm.CheckUserValidation(); //Just check user validation no need to filter it shouldnt be that much.      
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
                SelectedReportColl.RptTemplateVm.ParamVm.CheckUserValidation();
            }
            SetLabelMessageToUser();
        }

        public void SetLabelMessageToUser()
        {
            //Indicator visibility
            if (m_reportSelected != null)
            {
                var reportId = m_reportSelected.Id;
                if (reportId == ReportId.B3AccountHistory
                    || reportId == ReportId.B3BallCallByGame
                    || reportId == ReportId.B3Jackpot
                    || reportId == ReportId.B3Session
                    || reportId == ReportId.B3SessionSummary
                    || reportId == ReportId.B3SessionTransaction
                    || reportId == ReportId.B3WinnerCards
                    || reportId == ReportId.B3BingoCardReport
                    )
                {
                    IndicatorVisibility = true;
                    if (ReportId.B3BingoCardReport != reportId)
                    {
                        var sessSelected = SelectedReportColl.RptTemplateVm.ParamVm.SelectedSession;
                        if (sessSelected == null)
                        {
                            NoSession = true;
                            NoAccount = true;
                        }
                        else
                        {
                            if (ReportId.B3AccountHistory == reportId)
                            {
                                var accountSelected = SelectedReportColl.RptTemplateVm.ParamVm.AccountSelected;
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
            if (m_thread != null)
                m_thread.Abort();
        }

        public void ViewReportRel(ReportId reportId)
        {
            try
            {
                var report = m_reports.FirstOrDefault(r => r.Id == reportId);
                if (report == null) { return; }
                LoadCrystalReport(report);
                var reporttDoc = m_selectedReportTemplateViewModel.LoadReportDocument(report);
                
                m_thread = new System.Threading.Thread(
                    delegate()
                    {
                        if (!SelectedReportViewCol.CrViewer.ViewerCore.Dispatcher.CheckAccess())
                        {
                            m_crRun = SelectedReportViewCol.CrViewer.ViewerCore.Dispatcher.BeginInvoke(
                                DispatcherPriority.Background, new Action(
                                    delegate
                                    {
                                        SelectedReportViewCol.CrViewer.ViewerCore.ReportSource = reporttDoc;
                                        SelectedReportViewCol.CrViewer.Owner = Window.GetWindow(SelectedReportViewCol); // DE13491 need a parent window to display errors on top of
                                    }
                                    ));
                            m_crRun.Wait();
                        }
                        else
                        {
                            SelectedReportViewCol.CrViewer.ViewerCore.ReportSource = reporttDoc;
                        }
                    });
                
                m_thread.Start();

                DefaultViewMode = Visibility.Collapsed;
                CrystalReportViewMode = Visibility.Visible;
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
        /// <param name="reportId"></param>
        public void StartPrintReport(ReportId reportId)
        {
            var rpt = m_reports.FirstOrDefault(r => r.Id == reportId);
            if (rpt == null) { return; }
            LoadCrystalReport(rpt);
            var report = m_selectedReportTemplateViewModel.LoadReportDocument(rpt);

            if (!PrintReport(reportId, report))//Try printing the report
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
                var margins = report.PrintOptions.PageMargins;
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
                    m_selectedReportTemplateViewModel = value.RptTemplateVm;
                    m_reportSelected = value.B3Reports;
                    RaisePropertyChanged("SelectedReportColl");
                }
            }
        }

        private ReportTemplateViewModel m_selectedReportTemplateViewModel;

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
        
        public Visibility CrystalReportViewMode
        {
            get { return m_selectedReportTemplateViewModel.ReportViewerVisibility; }
            set
            {
                if (value != m_selectedReportTemplateViewModel.ReportViewerVisibility)
                {
                    m_selectedReportTemplateViewModel.ReportViewerVisibility = value;
                    RaisePropertyChanged("CrystalReportViewMode");
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
