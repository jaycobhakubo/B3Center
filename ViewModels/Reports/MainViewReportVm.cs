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
        private static volatile ReportsViewModel m_instance;
        private static readonly object m_syncRoot = new Object();
        private int m_accountNumberSelected;

        //Reports
        private AccountHistoryReportView m_accountHistoryReportView; 
        private AccountsReportView m_accountsReportView;
        private BallCallReportView m_ballCallReportView;
        private BallCallBySessionView m_ballCallBySession;
        private BingoCardView m_bingoCardReportView;
        private DailyReportView m_dailyReportView;
        private DetailReportView m_detailReportView;
        private DrawerReportView m_drawerReportView;
        private JackpotReportView m_jackpotReportView;
        private MonthlyReportView m_monthlyReportView;
        private SessionReportView m_sessionReportView;
        private SessionSummaryView m_sessionsummaryReportView;
        private SessionTransactionReportView m_sessionTranReportView;
        private VoidReportView m_voidReportView;
        private WinnerCardsReportView m_winnerCardsReportView;
           
        #endregion
        #region CONSTRUCTORS

        /// <summary>
        /// Represents the view model for managing reports
        /// </summary>
        public ReportsViewModel()
        {
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

            //set session list
            foreach (var session in controller.Sessions)
            {
                SessionList.Add(session);
            }
      
            LoadReportList();
            SetCommand();           
        }

        //Run once and never again.
        private void LoadReportList()
        {
            m_reportList.Clear();
            foreach (B3Report b3rpt in m_reports)
            {
                B3Report temp = new B3Report();
                temp = b3rpt;
                switch (b3rpt.Id)
                {
                    case ReportId.B3AccountHistory:
                        {
                            temp.DisplayName = "Account History";
                            break;
                        }
                    case ReportId.B3Accounts:
                        {
                            temp.DisplayName = "Accounts";
                            break;
                        }
                
                    case ReportId.B3BallCallByGame:
                        {
                            temp.DisplayName = "Ball Call(by game)";
                            break;
                        }
                    case ReportId.B3BallCallBySession:
                        {
                            temp.DisplayName = "Ball Call(by session)";
                            break;
                        }
                    case ReportId.B3BingoCardReport:
                        {
                            temp.DisplayName = "Bingo Card";
                            break;
                        }
                    case ReportId.B3Daily:
                        {
                            temp.DisplayName = "Daily";
                            break;
                        }
                    case ReportId.B3Detail:
                        {
                            temp.DisplayName = "Detail";
                            break;
                        }
                    case ReportId.B3Drawer:
                        {
                            temp.DisplayName = "Drawer";
                            break;
                        }
                    case ReportId.B3Jackpot:
                        {
                            temp.DisplayName = "Jackpot";
                            break;
                        }
                    case ReportId.B3Monthly:
                        {
                            temp.DisplayName = "Monthly";
                            break;
                        }
                    case ReportId.B3Session:
                        {
                            temp.DisplayName = "Session";
                            break;
                        }

                    case ReportId.B3SessionSummary:
                        {
                            temp.DisplayName = "Session Summary";
                            break;
                        }
                    case ReportId.B3SessionTransaction:
                        {
                            temp.DisplayName = "Session Transaction";
                            break;
                        }
                    case ReportId.B3Void:
                        {
                            temp.DisplayName = "Void";
                            break;
                        }
                    case ReportId.B3WinnerCards:
                        {
                            temp.DisplayName = "Winner Cards";
                            break;
                        }
                }
                if (!string.IsNullOrEmpty(temp.DisplayName))
                {
                    m_reportList.Add(temp);
                }
                m_reportList = m_reportList.OrderBy(l => l.DisplayName).ToList();
            }

            ReportSelected = m_reportList.FirstOrDefault();
        }

        #endregion
        #region ACCESSOR (static)
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

        public void SelectionChanged(string ReportName)
        {
            //Reinitialize every selection changed rather than storing.
            UserControl view = null;
            switch (ReportName)
            {
                case "Accounts":
                    {
                        m_accountsReportView = new AccountsReportView(m_rptBaseVm = new ReportBaseVm(getrtm(ReportId.B3Accounts)));
                        view = m_accountsReportView;
                        break;
                    }
                case "Account History":
                    {
                        m_accountHistoryReportView = new AccountHistoryReportView(m_rptBaseVm = new ReportBaseVm(getrtm(ReportId.B3AccountHistory)));
                        view = m_accountHistoryReportView;
                        break;
                    }
                case "Ball Call(by game)":
                    {
                        m_ballCallReportView = new BallCallReportView(m_rptBaseVm = new ReportBaseVm(getrtm(ReportId.B3BallCallByGame)));
                        view = m_ballCallReportView;
                        break;
                    }
                case "Ball Call(by session)":
                    {
                        m_ballCallBySession = new BallCallBySessionView(m_rptBaseVm = new ReportBaseVm(getrtm(ReportId.B3BallCallBySession)));
                        view = m_ballCallBySession;
                        break;
                    }
                case "Bingo Card":
                    {
                        m_bingoCardReportView = new BingoCardView(m_rptBaseVm = new ReportBaseVm(getrtm(ReportId.B3BingoCardReport)));
                        ViewReportVisibility = false;
                        view = m_bingoCardReportView;
                        break;
                    }
                case "Daily":
                    {
                        m_dailyReportView = new DailyReportView(m_rptBaseVm = new ReportBaseVm(getrtm(ReportId.B3Daily)));
                        view = m_dailyReportView;
                        break;
                    }
                case "Detail":
                    {
                        m_detailReportView = new DetailReportView(m_rptBaseVm = new ReportBaseVm(getrtm(ReportId.B3Detail)));
                        view = m_detailReportView;
                        break;
                    }
                case "Drawer":
                    {
                        m_drawerReportView = new DrawerReportView(m_rptBaseVm = new ReportBaseVm(getrtm(ReportId.B3Drawer)));
                        view = m_drawerReportView;
                        break;
                    }
                case "Jackpot":
                    {
                        m_jackpotReportView = new JackpotReportView(m_rptBaseVm = new ReportBaseVm(getrtm(ReportId.B3Jackpot)));
                        view = m_jackpotReportView;
                        break;
                    }
                case "Monthly":
                    {
                        m_monthlyReportView = new MonthlyReportView(m_rptBaseVm = new ReportBaseVm(getrtm(ReportId.B3Monthly)));
                        view = m_monthlyReportView;
                        break;
                    }
                case "Session":
                    {
                        m_sessionReportView = new SessionReportView(m_rptBaseVm = new ReportBaseVm(getrtm(ReportId.B3Session)));
                        view = m_sessionReportView;
                        break;
                    }
                case "Session Summary":
                    {
                        m_sessionsummaryReportView = new SessionSummaryView(m_rptBaseVm = new ReportBaseVm(getrtm(ReportId.B3SessionSummary)));
                        view = m_sessionsummaryReportView;
                        break;
                    }
                case "Session Transaction":
                    {
                        m_sessionTranReportView = new SessionTransactionReportView(m_rptBaseVm = new ReportBaseVm(getrtm(ReportId.B3SessionTransaction)));
                        view = m_sessionTranReportView;
                        break;
                    }

                case "Void":
                    {
                        m_voidReportView = new VoidReportView(m_rptBaseVm = new ReportBaseVm(getrtm(ReportId.B3Void)));
                        view = m_voidReportView;
                        break;
                    }

                case "Winner Cards":
                    {
                        m_winnerCardsReportView = new WinnerCardsReportView(m_rptBaseVm = new ReportBaseVm(getrtm(ReportId.B3WinnerCards)));
                        view = m_winnerCardsReportView;
                        break;
                    }
            }
            SelectedReportView = view;
        }

        private void LoadCrystalReport(B3Report report)
        {
            var server = "b3-server";//m_controller.Settings.DatabaseServer;
            var name = m_controller.Settings.DatabaseName;
            var user = m_controller.Settings.DatabaseUser;
            var password = "cobalt$45";//m_controller.Settings.DatabasePassword;
            report.LoadCrystalReport(server, name, user, password);
        }


        #endregion
        #region PROPERTIES

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
   
        private ReportBaseVm m_rptBaseVm;
        public ReportBaseVm RptBaseVm
        {
            get { return m_rptBaseVm; }
            set
            {
                m_rptBaseVm = value;
                RaisePropertyChanged("RptBaseVm");
            }
        }

        private B3Report m_reportSelected; 
        public B3Report ReportSelected
        {
            get { return m_reportSelected; }
            set 
            { 
                m_reportSelected = value;
                SelectionChanged(value.DisplayName);
                RaisePropertyChanged("ReportSelected");
            }
        }

        private List<B3Report> m_reportList = new List<B3Report>();
        public List<B3Report> ReportList
        {
            get { return m_reportList; }
        }
        
        private UserControl m_selectedReportView = new UserControl();
        public UserControl SelectedReportView
        {
            get {
                return m_selectedReportView;
            }
            set
            {
                m_selectedReportView = value;
                    RaisePropertyChanged("SelectedReportView");
            }
        }


        public Visibility CRViewMode
        {
            get
            {
                return m_rptBaseVm.ReportViewerVisibility;
            }
            set
            {
                m_rptBaseVm.ReportViewerVisibility = value;
                RaisePropertyChanged("CRViewMode");
            }

        }

        public Visibility DefaultViewMode
        {
            get
            {
                return m_rptBaseVm.ReportParameterVisible;
            }
            set
            {
                m_rptBaseVm.ReportParameterVisible = value;
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
            ViewReportCommand = new RelayCommand(parameter => ViewReportRel(ReportSelected.Id));
            PrintReportCommand = new RelayCommand(parameter => StartPrintReport(ReportSelected.Id)); 
        }
   

        public void ViewReportRel(ReportId reportID)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    tempcr = new CrystalReportsViewer();
                    tempcr.ToggleSidePanel = Constants.SidePanelKind.None;
                    var Rpt = m_reports.FirstOrDefault(r => r.Id == reportID);
                    if (Rpt == null) { return; }
                    LoadCrystalReport(Rpt);
                    var report = m_rptBaseVm.LoadReportDocument(Rpt);

                    if (report == null)
                    {
                        IsLoading = false;
                        return;
                    }

                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
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
            m_rptBaseVm.vReportViewer = tempcr;          
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
            var report = m_rptBaseVm.LoadReportDocument(Rpt);

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