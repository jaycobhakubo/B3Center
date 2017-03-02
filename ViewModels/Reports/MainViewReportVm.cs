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
        #region MEMBERS
    
        private bool m_isLoading;
        private bool m_isPrinting;
        private B3Controller m_controller;
        private ObservableCollection<Session> m_sessionList;
        private List<B3Report> m_reports;
        //private CrystalReportsViewer tempcr = new CrystalReportsViewer();
        private bool m_isRngBallCall;

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
            //m_controller.SessionInfoCompleted += OnListInfoDone;
            m_isRngBallCall = !controller.Settings.IsCommonRngBallCall;

            //set session list
            foreach (var session in controller.Sessions)
            {
                SessionList.Add(session);
            }

            var B3Accounts = new ReportTemplateViewModel(getrtm(ReportId.B3Accounts));
            var B3Daily = new ReportTemplateViewModel(getrtm(ReportId.B3Daily));
            var B3Detail = new ReportTemplateViewModel(getrtm(ReportId.B3Detail));
            var B3Monthly = new ReportTemplateViewModel(getrtm(ReportId.B3Monthly));
            var B3Void = new ReportTemplateViewModel(getrtm(ReportId.B3Void));
            var B3Drawer = new ReportTemplateViewModel(getrtm(ReportId.B3Drawer));
            var B3Jackpot = new ReportTemplateViewModel(getrtm(ReportId.B3Jackpot));
            var B3Session = new ReportTemplateViewModel(getrtm(ReportId.B3Session));
            var B3BallCallBySession = new ReportTemplateViewModel(getrtm(ReportId.B3BallCallBySession));
            var B3BallCallByGame = new ReportTemplateViewModel(getrtm(ReportId.B3BallCallByGame));
            var B3SessionTransaction = new ReportTemplateViewModel(getrtm(ReportId.B3SessionTransaction));
            var B3SessionSummary = new ReportTemplateViewModel(getrtm(ReportId.B3SessionSummary));
            var B3WinnerCards = new ReportTemplateViewModel(getrtm(ReportId.B3WinnerCards));
            var B3AccountHistory = new ReportTemplateViewModel(getrtm(ReportId.B3AccountHistory));
            var B3BingoCardReport = new ReportTemplateViewModel(getrtm(ReportId.B3BingoCardReport));

            m_reportCollection = new ObservableCollection<ReportMain>()
            {
              new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3Accounts), ReportDisplayName = "Accounts",rpttemplatevm =   B3Accounts  , rptView = new ReportTemplate(B3Accounts)},
              new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3Daily ), ReportDisplayName = "Daily",rpttemplatevm =   B3Daily   , rptView = new ReportTemplate(  B3Daily )},
              new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3Detail ), ReportDisplayName = "Detail",rpttemplatevm =   B3Detail   , rptView = new ReportTemplate(  B3Detail )},
              new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3Monthly), ReportDisplayName = "Monthly",rpttemplatevm =   B3Monthly  , rptView = new ReportTemplate(  B3Monthly)},
              new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3Void ), ReportDisplayName = "Void",rpttemplatevm =   B3Void   , rptView = new ReportTemplate(  B3Void )},
              new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3Drawer ), ReportDisplayName = "Drawer",rpttemplatevm =   B3Drawer   , rptView = new ReportTemplate(  B3Drawer )},
              new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3Jackpot ), ReportDisplayName ="Jackpot ",rpttemplatevm =   B3Jackpot   , rptView = new ReportTemplate(  B3Jackpot )},
              new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3Session ), ReportDisplayName ="Session ",rpttemplatevm =   B3Session   , rptView = new ReportTemplate(  B3Session )},
              new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3BallCallBySession), ReportDisplayName = "BallCall by session",rpttemplatevm =   B3BallCallBySession  , rptView = new ReportTemplate(  B3BallCallBySession)},
              new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3BallCallByGame ), ReportDisplayName = "BallCall by game",rpttemplatevm =   B3BallCallByGame   , rptView = new ReportTemplate(  B3BallCallByGame )},
              new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3SessionTransaction ), ReportDisplayName = "Session Transaction",rpttemplatevm =   B3SessionTransaction   , rptView = new ReportTemplate(  B3SessionTransaction )},
              new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3SessionSummary ), ReportDisplayName = "Session Summary",rpttemplatevm =   B3SessionSummary   , rptView = new ReportTemplate(  B3SessionSummary )},
              new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3WinnerCards ), ReportDisplayName = "Winner Cards",rpttemplatevm =   B3WinnerCards   , rptView = new ReportTemplate(  B3WinnerCards )},
              new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3AccountHistory ), ReportDisplayName = "Account History",rpttemplatevm =   B3AccountHistory   , rptView = new ReportTemplate(  B3AccountHistory )},
              new ReportMain(){B3Reports = m_reports.Single(l => l.Id == ReportId.B3BingoCardReport ), ReportDisplayName = "Bingo Card",rpttemplatevm =   B3BingoCardReport   , rptView = new ReportTemplate(  B3BingoCardReport )}, 
            };

            SetBallCallReportBySessionOrByGame(controller.Settings.IsCommonRngBallCall);//Set our ball call report 
            m_reportCollection = new ObservableCollection<ReportMain>(m_reportCollection.OrderBy(l => l.ReportDisplayName));
            SelectedReportColl = m_reportCollection.FirstOrDefault();
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
        #region (private)

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
            //CrystalReportsViewer ReportViewer_ = new CrystalReportsViewer();
            //ReportViewer_.ToggleSidePanel = Constants.SidePanelKind.None;
            //ReportViewer_.Focusable = true;
            //ReportViewer_.Focus();
            //result.CrystalReportViewer_ = ReportViewer_;
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
    
        #endregion
        #region (public)

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
                                   ReportTemplateViewModel B3BallCallBySession = new ReportTemplateViewModel(getrtm(ReportId.B3BallCallBySession));
                                   ReportMain x = new ReportMain() { B3Reports = m_reports.Single(l => l.Id == ReportId.B3BallCallBySession), ReportDisplayName = "BallCall by session", rpttemplatevm = B3BallCallBySession, rptView = new ReportTemplate(B3BallCallBySession) };
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
                                   ReportTemplateViewModel B3BallCallByGame = new ReportTemplateViewModel(getrtm(ReportId.B3BallCallByGame));
                                   ReportMain xy = new ReportMain() { B3Reports = m_reports.Single(l => l.Id == ReportId.B3BallCallByGame), ReportDisplayName = "BallCall by game", rpttemplatevm = B3BallCallByGame, rptView = new ReportTemplate(B3BallCallByGame) };
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
        #region (on event)

        //Dates change event
        public void updateItemDateSelected(DateTime i)
        {
            if (m_selectedReportColl.B3Reports != null)
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
        }


        //Report change event
        public void SelectionChange()
        {
            if (m_reportSelected.Id == ReportId.B3BingoCardReport)
            {
                ViewReportVisibility = false;
            }
            else
            {
                SelectedReportColl.rpttemplatevm.parVm.CheckUserValidation();
            }
            SetLabelMessageToUser();
        }

        public void SetLabelMessageToUser()
        {
            //Indicator visibility
            if (m_reportSelected != null)
            {
                var y = m_reportSelected.Id;
                if
                    (
                    y == ReportId.B3AccountHistory
                    || y == ReportId.B3BallCallByGame
                    || y == ReportId.B3Jackpot
                    || y == ReportId.B3Session
                            || y == ReportId.B3SessionSummary
                            || y == ReportId.B3SessionTransaction
                            || y == ReportId.B3WinnerCards
                            || y == ReportId.B3BingoCardReport
                    )
                {
                    IndicatorVisibility = true;
                    if (ReportId.B3BingoCardReport != y)
                    {
                        var sessSelected = SelectedReportColl.rpttemplatevm.parVm.SelectedSession;
                        if (sessSelected == null)
                        {
                            NoSession = true;
                            NoAccount = true;
                        }
                        else
                        {
                            if (ReportId.B3AccountHistory == y)
                            {
                                var accountSelected = SelectedReportColl.rpttemplatevm.parVm.AccountSelected;
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
        #region (view and print report)

        private void LoadCrystalReport(B3Report report)
        {
            var server = m_controller.Settings.DatabaseServer;
            var name = m_controller.Settings.DatabaseName;
            var user = m_controller.Settings.DatabaseUser;
            var password = m_controller.Settings.DatabasePassword;
            report.LoadCrystalReport(server, name, user, password);
        }


        public void ViewReportRel(ReportId reportID)
        {
            try
            {
                var Rpt = m_reports.FirstOrDefault(r => r.Id == reportID);
                if (Rpt == null) { return; }
                LoadCrystalReport(Rpt);
                var m_rptDoc = m_selectedReportTemplateViewModel.LoadReportDocument(Rpt);
                SelectedReportViewCol.CrViewer.ViewerCore.ReportSource = m_rptDoc;
                SelectedReportViewCol.CrViewer.Owner = Window.GetWindow(SelectedReportViewCol); // DE13491 need a parent window to display errors on top of
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

        #region print
        
        /// <summary>
        /// NOTE: in order for the report to print in a particular printer name in your network,
        /// you should manually set the page setup -> Printer Options -> No printer name  false(uncheck) .
        /// (It dont matter what printer name you pick as long as it is uncheck).
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

        private bool TryPrintReceiptPrinter(ReportDocument report)     //Epson
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
        #endregion
        #region PROPERTIES

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
            set {
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
                    SelectedReportViewCol = value.rptView;
                    m_selectedReportTemplateViewModel = value.rpttemplatevm;
                    m_reportSelected = value.B3Reports;
                    RaisePropertyChanged("SelectedReportColl");
                }
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

        internal B3CenterSettings Settings
        {
            get { return m_controller.Settings; }
        }

        #endregion
        #region EVENTS
    
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

