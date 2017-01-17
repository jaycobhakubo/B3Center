﻿#region Copyright
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

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{
    public class ReportsViewModel : ViewModelBase
    {
        #region Local variables

        private bool m_enableJackpotReportButtons;
        private bool m_enableSessionReportButtons;
        private bool m_enableAccountHistoryReportButtons;
        private bool m_enableBingoCardReportButton;
        private bool m_isLoading;
        private bool m_isPrinting;
        private B3Controller m_controller;
        private ObservableCollection<Session> m_sessionList;
        private ObservableCollection<int> m_accountList;
        private ObservableCollection<Session> m_jackpotReportSessionList;
        private ObservableCollection<Session> m_sessionReportSessionList;
        private ObservableCollection<Session> m_AccountHistoryReportSessionList;
        private ObservableCollection<Session> m_winnercardsReportSessionList;
        private ObservableCollection<Session> m_ballCallReportSessionList;
        private ObservableCollection<Session> m_SessTranReportSessionList;
        private ObservableCollection<int> m_accountHistoryReportAccountList;
        private ObservableCollection<string> m_ballCallReportDefList;

        private IEnumerable<string> m_months;
        private IEnumerable<string> m_years;

        private List<B3Report> m_reports;    
        private Session m_sessionReportSessionSelected;
        private Session m_jackpotReportSessionSelected;
        private Session m_accountHistoryReportSessionSelected;
        private Session m_winnercardsReportSessionSelected;
        private Session m_ballCallReportSessionSelected;
        private Session m_sessionTransReportSessionSelected;
        private string m_ballCallReportDefSelected;
        private int m_accountHistoryReportAccountSelected;
        private string m_monthlyReportMonthSelected;
        private string m_monthlyReportYearSelected;
        private string m_accountReportMonthSelected;
        private string m_accountReportYearSelected;
        private static volatile ReportsViewModel m_instance;
        private static readonly object m_syncRoot = new Object();
        private int m_accountNumberSelected;

    

        //Reports
        private AccountsReportView m_accountsReportView;// = new AccountsReportView();// = new AccountsReportView;
        private DailyReportView m_dailyReportView;//= new DailyReportView();
        private DetailReportView m_detailReportView;//= new DetailReportView();
        private DrawerReportView m_drawerReportView;//= new DrawerReportView();
        private JackpotReportView m_jackpotReportView;// = new JackpotReportView();
        private MonthlyReportView m_monthlyReportView;//= new MonthlyReportView();
        private SessionReportView m_sessionReportView;//= new SessionReportView();
        private VoidReportView m_voidReportView;// = new VoidReportView();
        private SessionSummaryView m_sessionsummaryReportView;// = new SessionSummaryView();
        private AccountHistoryReportView m_accountHistoryReportView;
        private WinnerCardsReportView m_winnerCardsReportView;// = new WinnerCardsReportView();
        private BallCallReportView m_ballCallReportView;
        private SessionTransactionReportView m_sessionTranReportView;// = new SessionTransactionReportView();
        //private BingoCardReportView m_bingoCardReportView = new BingoCardReportView();
        private BingoCardView m_bingoCardReportView;

        CrystalReportsViewer tempcr = new CrystalReportsViewer();
        

  
        #endregion

        #region Constructors

        /// <summary>
        /// Represents the view model for managing reports
        /// </summary>
        public ReportsViewModel()
        {
            SessionList = new ObservableCollection<Session>();
            JackpotReportSessionList = new ObservableCollection<Session>();
            SessionReportSessionList = new ObservableCollection<Session>();
            AccountHistoryReportSessionList = new ObservableCollection<Session>();
            AccountHistoryReportAccountList = new ObservableCollection<int>();
            WinnerCardsReportSessionList = new ObservableCollection<Session>();
            BallCallReportDefList = new ObservableCollection<string>();
            BallCallReportSessionList = new ObservableCollection<Session>();
            SessionTranReportSessionList = new ObservableCollection<Session>();
            AccountList = new ObservableCollection<int>();

            Months = Enum.GetNames(typeof(Month)).Where(m => m != Month.NotSet.ToString());

            var years = new List<string>();
            for (var i = DateTime.Now.Year; i > DateTime.Now.Year - 50; i--)
            {
                years.Add(i.ToString());
            }

            Years = years;

            //Get current month months
            var currentMonth = Months.FirstOrDefault(m => m == ((Month)DateTime.Now.Month).ToString());
            var currentYear = DateTime.Now.Year;

            //Set MonthlyReport
            MonthlyReportMonthSelected = currentMonth;
            MonthlyReportYearSelected = currentYear.ToString();

            //Set Account Report
            AccountReportMonthSelected = currentMonth;
            AccountReportYearSelected = currentYear.ToString();

            IsLoading = false;
            IsPrinting = false;

            LoadBallCallReportDefList();
     
        }

       

       
        /// <summary>
        /// Initializes the ViewModel with the specified controller.
        /// </summary>
        /// <param name="controller">The controller.</param>
        internal void Initialize(B3Controller controller)
        {
            m_controller = controller;
            m_reports = controller.Reports;
            tempcr.ToggleSidePanel = Constants.SidePanelKind.None;
            m_controller.SessionInfoCompleted += OnListInfoDone;

            //set session list
            foreach (var session in controller.Sessions)
            {
                SessionList.Add(session);//knc
            }
      
            LoadReportList();
            SetCommand();
         
            
        }


        private ReportTemplateModel getrtm(ReportId b3rpt)
        {
            ReportTemplateModel result = new ReportTemplateModel();
            List<string> par = new List<string>();
            ReportParameterModel temprptparmodel = new ReportParameterModel();
            temprptparmodel.DatePickerModel = new Model.Shared.DatePickerM();
            

            switch (b3rpt)
            {
                case ReportId.B3Accounts:
                    {
                        par.Add("MonthYear");
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Accounts Outstanding";              
                        result.ReportParameter = par;
                        //result.CrystalReportViewer = new SAPBusinessObjects.WPF.Viewer.CrystalReportsViewer();
                        result.ReportViewerm = Visibility.Hidden;
                        result.DefaultViewerm = Visibility.Visible;
                        break;
                    }
                case ReportId.B3AccountHistory:
                    {

                        //temprptparmodel.SessionList = SessionList;
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Account History";
                     
                        par.Add("Date");
                        par.Add("Session");
                        par.Add("AccountNumber");
                
                        result.ReportParameter = par;
                        
                        //result.CrystalReportViewer = new SAPBusinessObjects.WPF.Viewer.CrystalReportsViewer();
                        result.ReportViewerm = Visibility.Hidden;
                        result.DefaultViewerm = Visibility.Visible;
                        break;
                    }
                case ReportId.B3BallCallByGame:
                    {
                       
                        temprptparmodel.SessionList = SessionList.ToList();
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Ball Call";
                        par.Add("Category");
                        par.Add("Date");
                        par.Add("Session");
                        result.ReportParameter = par;
                        //result.CrystalReportViewer = new SAPBusinessObjects.WPF.Viewer.CrystalReportsViewer();
                        result.ReportViewerm = Visibility.Hidden;
                        result.DefaultViewerm = Visibility.Visible;
                        break;
                    }
                case ReportId.B3BingoCardReport:
                    {
                        //temprptparmodel.SessionList = SessionList;
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Bingo Card";
                        par.Add("StartEndCard");     
                        result.ReportParameter = par;
                        //result.CrystalReportViewer = new SAPBusinessObjects.WPF.Viewer.CrystalReportsViewer();
                        result.ReportViewerm = Visibility.Hidden;
                        result.DefaultViewerm = Visibility.Visible;
                        break;
                    }
                case ReportId.B3Daily:
                    {
                        //temprptparmodel.SessionList = SessionList;
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Daily";
                        par.Add("Date");
                        result.ReportParameter = par;
                        //result.CrystalReportViewer = new SAPBusinessObjects.WPF.Viewer.CrystalReportsViewer();
                        result.ReportViewerm = Visibility.Hidden;
                        result.DefaultViewerm = Visibility.Visible;
                        break;
                    }
                case ReportId.B3Detail:
                    {
                        //temprptparmodel.SessionList = SessionList;
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Detail";
                        par.Add("StartEndDatewTime");
                        result.ReportParameter = par;
                        //result.CrystalReportViewer = new SAPBusinessObjects.WPF.Viewer.CrystalReportsViewer();
                        result.ReportViewerm = Visibility.Hidden;
                        result.DefaultViewerm = Visibility.Visible;
                        break;
                    }
                case ReportId.B3Drawer:
                    {
                        //temprptparmodel.SessionList = SessionList;
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Drawer";
                        //par.Add("StartEndCard");
                        result.ReportParameter = par;
                        //result.CrystalReportViewer = new SAPBusinessObjects.WPF.Viewer.CrystalReportsViewer();
                        result.ReportViewerm = Visibility.Hidden;
                        result.DefaultViewerm = Visibility.Visible;
                        break;
                    }
                case ReportId.B3Jackpot:
                    {
                        temprptparmodel.SessionList = SessionList.ToList();
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Jackpot";
                        par.Add("Date");
                        par.Add("Session");
                        result.ReportParameter = par;
                        //result.CrystalReportViewer = new SAPBusinessObjects.WPF.Viewer.CrystalReportsViewer();
                        result.ReportViewerm = Visibility.Hidden;
                        result.DefaultViewerm = Visibility.Visible;
                        break;
                    }
                case ReportId.B3Monthly:
                    {
                        //temprptparmodel.SessionList = SessionList;
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Monthly";
                        par.Add("MonthYear");
                        result.ReportParameter = par;
                        //result.CrystalReportViewer = new SAPBusinessObjects.WPF.Viewer.CrystalReportsViewer();
                        result.ReportViewerm = Visibility.Hidden;
                        result.DefaultViewerm = Visibility.Visible;
                        break;
                    }
                case ReportId.B3Session:
                    {

                        temprptparmodel.SessionList = SessionList.ToList();
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Session";
                        par.Add("Date");
                        par.Add("Session");
                        result.ReportParameter = par;
                        //result.CrystalReportViewer = new SAPBusinessObjects.WPF.Viewer.CrystalReportsViewer();
                        result.ReportViewerm = Visibility.Hidden;
                        result.DefaultViewerm = Visibility.Visible;
                        break;
                    }
                case ReportId.B3SessionSummary:
                    {

                        temprptparmodel.SessionList = SessionList.ToList();
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Session Summary";
                        par.Add("Date");
                        par.Add("Session");
                        result.ReportParameter = par;
                        //result.CrystalReportViewer = new SAPBusinessObjects.WPF.Viewer.CrystalReportsViewer();
                        result.ReportViewerm = Visibility.Hidden;
                        result.DefaultViewerm = Visibility.Visible;
                        break;
                    }
                case ReportId.B3SessionTransaction:
                    {

                        temprptparmodel.SessionList = SessionList.ToList();
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Session Transaction";
                        par.Add("Date");
                        par.Add("Session");
                        result.ReportParameter = par;
                        //result.CrystalReportViewer = new SAPBusinessObjects.WPF.Viewer.CrystalReportsViewer();
                        result.ReportViewerm = Visibility.Hidden;
                        result.DefaultViewerm = Visibility.Visible;
                        break;
                    }
                case ReportId.B3Void:
                    {
                        //temprptparmodel.SessionList = SessionList;
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Void";
                        par.Add("StartEndDatewTime");
                        result.ReportParameter = par;
                        //result.CrystalReportViewer = new SAPBusinessObjects.WPF.Viewer.CrystalReportsViewer();
                        result.ReportViewerm = Visibility.Hidden;
                        result.DefaultViewerm = Visibility.Visible;
                        break;
                    }
                case ReportId.B3WinnerCards:
                    {

                        temprptparmodel.SessionList = SessionList.ToList();
                        temprptparmodel.rptid = b3rpt;
                        result.ReportTitle = "Winners Card";
                        par.Add("Date");
                        par.Add("Session");
                        result.ReportParameter = par;
                        //result.CrystalReportViewer = new SAPBusinessObjects.WPF.Viewer.CrystalReportsViewer();
                        result.ReportViewerm = Visibility.Hidden;
                        result.DefaultViewerm = Visibility.Visible;
                        break;
                    }
            }
            result.rptParModel = temprptparmodel;
            return result;
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
                            temp.DisplayName = "Ball Call";
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
            //m_reportList.Add("Accounts");
            //m_reportList.Add("Account History");
            //m_reportList.Add("Ball Call");
            //m_reportList.Add("Bingo Card");
            //m_reportList.Add("Daily");
            //m_reportList.Add("Detail");
            //m_reportList.Add("Drawer");
            //m_reportList.Add("Jackpot");
            //m_reportList.Add("Monthly");
            //m_reportList.Add("Session");
            //m_reportList.Add("Session Summary");
            //m_reportList.Add("Session Transaction");
            //m_reportList.Add("Void");
            //m_reportList.Add("Winner Cards");
            ReportSelected = m_reportList.FirstOrDefault();
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
                case "Ball Call":
                    {
                        m_ballCallReportView = new BallCallReportView(m_rptBaseVm = new ReportBaseVm(getrtm(ReportId.B3BallCallByGame)));
                        view = m_ballCallReportView;
                        break;
                    }
                case "Bingo Card":
                    {
                        m_bingoCardReportView = new BingoCardView(m_rptBaseVm = new ReportBaseVm(getrtm(ReportId.B3BingoCardReport)));
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

      
         
        #endregion

        #region Properties

      
        /// <summary>
        /// Gets or sets the months.
        /// </summary>
        /// <value>
        /// The months.
        /// </value>
        public IEnumerable<string> Months
        {
            get
            {
                return m_months;
            }
            set
            {
                m_months = value;
                RaisePropertyChanged("Months");
            }
        }

        public IEnumerable<string> Years
        {
            get
            {
                return m_years;
            }
            set
            {
                m_years = value;
                RaisePropertyChanged("Years");
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
        /// Gets or sets the account list.
        /// </summary>
        /// <value>
        /// The account list.
        /// </value>
        public ObservableCollection<int> AccountList
        {
            get
            {
                return m_accountList;
            }
            set
            {
               m_accountList = value;
                RaisePropertyChanged("AccountList");
            }
        }

        /// <summary>
        /// Gets or sets the jackpot report session list.
        /// </summary>
        /// <value>
        /// The jackpot report session list.
        /// </value>
        public ObservableCollection<Session> JackpotReportSessionList
        {
            get
            {
                return m_jackpotReportSessionList;
            }
            set
            {
                m_jackpotReportSessionList = value;
                RaisePropertyChanged("JackpotReportSessionList");
            }
        }


        /// <summary>
        /// Gets or sets the session report session list.
        /// </summary>
        /// <value>
        /// The session report session list.
        /// </value>
        public ObservableCollection<Session> SessionReportSessionList
        {
            get
            {
                return m_sessionReportSessionList;
            }
            set
            {
                m_sessionReportSessionList = value;
                RaisePropertyChanged("SessionReportSessionList");
            }
        }

        /// <summary>
        /// Gets or sets the account history report session list.
        /// </summary>
        /// <value>
        /// The account history report session list.
        /// </value>
        public ObservableCollection<Session> AccountHistoryReportSessionList
        {
            get
            {
                return m_AccountHistoryReportSessionList;
            }
            set
            {
                m_AccountHistoryReportSessionList = value;
                RaisePropertyChanged("AccountHistoryReportSessionList");
            }
        }

        /// <summary>
        /// Gets or sets the Account History report account number list.
        /// </summary>
        /// <value>
        /// The account history report account number list.
        /// </value>
        public ObservableCollection<int> AccountHistoryReportAccountList
        {
            get
            {
                return m_accountHistoryReportAccountList;
            }
            set
            {
                m_accountHistoryReportAccountList = value;
                RaisePropertyChanged("AccountHistoryReportAccountList");
            }
        }

        /// <summary>
        /// Gets or sets the winner cards report session list.
        /// </summary>
        /// <value>
        /// The winner cards report session list.
        /// </value>
        public ObservableCollection<Session> WinnerCardsReportSessionList
        {
            get
            {
                return m_winnercardsReportSessionList;
            }
            set
            {
                m_winnercardsReportSessionList = value;
                RaisePropertyChanged("WinnerCardsReportSessionList");
            }
        }

        /// <summary>
        /// Gets or sets the winner cards report session list.
        /// </summary>
        /// <value>
        /// The winner cards report session list.
        /// </value>
        public ObservableCollection<Session> BallCallReportSessionList
        {
            get
            {
                return m_ballCallReportSessionList;
            }
            set
            {
                m_ballCallReportSessionList = value;
                RaisePropertyChanged("BallCallReportSessionList");
            }
        }

        /// <summary>
        /// Gets or sets the session transaction report session list.
        /// </summary>
        /// <value>
        /// The session transaction report session list.
        /// </value>
        public ObservableCollection<Session> SessionTranReportSessionList
        {
            get
            {
                return m_SessTranReportSessionList;
            }
            set
            {
                m_SessTranReportSessionList = value;
                RaisePropertyChanged("SessionTranReportSessionList");
            }
        }


        /// <summary>
        /// Gets or sets the session selected for session report.
        /// </summary>
        /// <value>
        /// The session report session selected.
        /// </value>
        public Session SessionReportSessionSelected
        {
            get
            {
                return m_sessionReportSessionSelected;
            }
            set
            {
                if (m_sessionReportSessionSelected != value)
                {
                    m_sessionReportSessionSelected = value;
                    RaisePropertyChanged("SessionReportSessionSelected");
                }
            }
        }

        /// <summary>
        /// Gets or sets the jackpot report session selected.
        /// </summary>
        /// <value>
        /// The jackpot report session selected.
        /// </value>
        public Session JackpotReportSessionSelected
        {
            get
            {
                return m_jackpotReportSessionSelected;
            }
            set
            {
                if (m_jackpotReportSessionSelected != value)
                {
                    m_jackpotReportSessionSelected = value;
                    RaisePropertyChanged("JackpotReportSessionSelected");
                }
            }
        }

        /// <summary>
        /// Gets or sets the account history report session selected.
        /// </summary>
        /// <value>
        /// The account history report session selected.
        /// </value>
        public Session AccountHistoryReportSessionSelected
        {
            get
            {
                return m_accountHistoryReportSessionSelected;
            }
            set
            {
                if (m_accountHistoryReportSessionSelected != value)
                {
                    m_accountHistoryReportSessionSelected = value;
                    if (value != null)
                    {
                        UpdateAccountHistoryReportAccountBySession(m_accountHistoryReportSessionSelected.Number);
                    }
                    else
                    {
                        AccountHistoryReportAccountList.Clear();
                        AccountHistoryReportAccountSelected = new int();
                    }
                    RaisePropertyChanged("AccountHistoryReportSessionSelected");
                   
                }
            }
        }



        /// <summary>
        /// Gets or sets the winner cards report session selected.
        /// </summary>
        /// <value>
        /// The winner cards report session selected.
        /// </value>
        public Session WinnerCardsReportSessionSelected
        {
            get
            {
                return m_winnercardsReportSessionSelected;
            }
            set
            {
                if (m_winnercardsReportSessionSelected != value)
                {
                    m_winnercardsReportSessionSelected = value;
                    RaisePropertyChanged("WinnerCardsReportSessionSelected");
                }
            }
        }

        /// <summary>
        /// Gets or sets the ball call report session selected.
        /// </summary>
        /// <value>
        /// The ball call report session selected.
        /// </value>
        public Session BallCallReportSessionSelected
        {
            get
            {
                return m_ballCallReportSessionSelected;
            }
            set
            {
                if (m_ballCallReportSessionSelected != value)
                {
                    m_ballCallReportSessionSelected = value;
                    RaisePropertyChanged("BallCallReportSessionSelected");
                }
            }
        }

        /// <summary>
        /// Gets or sets the session transaction report session selected.
        /// </summary>
        /// <value>
        /// The session transaction report session selected.
        /// </value>
        public Session SessionTranReportSessionSelected
        {
            get
            {
                return m_sessionTransReportSessionSelected;
            }
            set
            {
                if (m_sessionTransReportSessionSelected != value)
                {
                    m_sessionTransReportSessionSelected = value;
                    RaisePropertyChanged("SessionTranReportSessionSelected");
                }
            }
        }

        /// <summary>
        /// Gets or sets the account history report account selected.
        /// </summary>
        /// <value>
        /// The account history report account selected.
        /// </value>
        public int AccountHistoryReportAccountSelected
        {
            get
            {
                return m_accountHistoryReportAccountSelected;
            }
            set
            {
                if (m_accountHistoryReportAccountSelected != value)
                {
                    m_accountHistoryReportAccountSelected = value;
                    RaisePropertyChanged("AccountHistoryReportAccountSelected");
                }
            }
        }

        /// <summary>
        /// Gets or sets the ball call report definition selected.
        /// </summary>
        /// <value>
        /// The ball call report definition selected.
        /// </value>
        public string BallCallReportDefSelected
        {
            get
            {
                return m_ballCallReportDefSelected;
            }
            set
            {
                if (m_ballCallReportDefSelected != value)
                {
                    m_ballCallReportDefSelected = value;
                    RaisePropertyChanged("BallCallReportDefSelected");
                }
            }
        }

        /// <summary>
        /// Gets or sets the ball call report definition list.
        /// </summary>
        /// <value>
        /// The ball call report definition list.
        /// </value>
        public ObservableCollection<string> BallCallReportDefList
        {
            get
            {
                return m_ballCallReportDefList;
            }
            set
            {
                m_ballCallReportDefList = value;
                RaisePropertyChanged("BallCallReportDefList");
            }
        }

        /// <summary>
        /// Gets or sets the Account selected for account history report.
        /// </summary>
        /// <value>
        /// The account history report account selected.
        /// </value>
        public int AccountNumberSelected
        {
            get
            {
                return m_accountNumberSelected;
            }
            set
            {
                if (m_accountNumberSelected != value)
                {
                    m_accountNumberSelected = value;
                    RaisePropertyChanged("AccountNumberSelected");
                }
            }
        }

        /// <summary>
        /// Gets or sets the monthly report month selected.
        /// </summary>
        /// <value>
        /// The monthly report month selected.
        /// </value>
        public string MonthlyReportMonthSelected
        {
            get
            {
                return m_monthlyReportMonthSelected;
            }
            set
            {

                if (m_monthlyReportMonthSelected != value)
                {
                    m_monthlyReportMonthSelected = value;
                    RaisePropertyChanged("MonthlyReportMonthSelected");
                }
            }
        }

        /// <summary>
        /// Gets or sets the monthly report year selected.
        /// </summary>
        /// <value>
        /// The monthly report year selected.
        /// </value>
        public string MonthlyReportYearSelected
        {
            get
            {
                return m_monthlyReportYearSelected;
            }
            set
            {

                if (m_monthlyReportYearSelected != value)
                {
                    m_monthlyReportYearSelected = value;
                    RaisePropertyChanged("MonthlyReportYearSelected");
                }
            }
        }

        /// <summary>
        /// Gets or sets the account report month selected.
        /// </summary>
        /// <value>
        /// The account report month selected.
        /// </value>
        public string AccountReportMonthSelected
        {
            get
            {
                return m_accountReportMonthSelected;
            }
            set
            {
                if (m_accountReportMonthSelected != value)
                {
                    m_accountReportMonthSelected = value;
                    RaisePropertyChanged("AccountReportMonthSelected");
                }
            }
        }

        /// <summary>
        /// Gets or sets the account report year selected.
        /// </summary>
        /// <value>
        /// The account report year selected.
        /// </value>
        public string AccountReportYearSelected
        {
            get
            {
                return m_accountReportYearSelected;
            }
            set
            {

                if (m_accountReportYearSelected != value)
                {
                    m_accountReportYearSelected = value;
                    RaisePropertyChanged("AccountReportYearSelected");
                }
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

        /// <summary>
        /// Gets or sets a value indicating whether [enable daily report buttons].
        /// </summary>
        /// <value>
        /// <c>true</c> if [enable daily report buttons]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableJackpotReportButtons 
        {
            get { return m_enableJackpotReportButtons; }
            set
            {
                m_enableJackpotReportButtons = value;
                RaisePropertyChanged("EnableJackpotReportButtons");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [enable drawer report buttons].
        /// </summary>
        /// <value>
        /// <c>true</c> if [enable drawer report buttons]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableSessionReportButtons
        {
            get { return m_enableSessionReportButtons; }
            set
            {
                m_enableSessionReportButtons = value;
                RaisePropertyChanged("EnableSessionReportButtons");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [enable Account History buttons].
        /// </summary>
        /// <value>
        /// <c>true</c> if [enable account history report buttons]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableAccountHistoryReportButtons
        {
            get { return  m_enableAccountHistoryReportButtons; }
            set
            {
                m_enableAccountHistoryReportButtons = value;
                RaisePropertyChanged("EnableAccountHistoryReportButtons");
            }
        }




        /// <summary>
        /// Gets or sets a value indicating whether [enable Bingo Card buttons].
        /// </summary>
        /// <value>
        /// <c>true</c> if [enableBingo Card  report buttons]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableBingoCardReportButtons
        {
            get { return m_enableBingoCardReportButton; }
            set
            {
                m_enableBingoCardReportButton = value;
                RaisePropertyChanged("EnableBingoCardReportButtons");
            }
        }


        internal B3CenterSettings Settings
        {
            get { return m_controller.Settings; }
        }

        #endregion

        #region Member Methods

      
        /// <summary>
        /// Loads the account report document.
        /// </summary>
        /// <returns></returns>
        internal ReportDocument LoadAccountReportDocument()
        {
            var accountReport = m_reports.FirstOrDefault(r => r.Id == ReportId.B3Accounts);

            if (accountReport == null)
            {
                return null;
            }

            LoadCrystalReport(accountReport);

            Month month;
            if (!Enum.TryParse(AccountReportMonthSelected, out month))
            {
                return null;
            }

            accountReport.CrystalReportDocument.SetParameterValue("@nMonth", (int)month);
            accountReport.CrystalReportDocument.SetParameterValue("@nYear", AccountReportYearSelected);

            return accountReport.CrystalReportDocument;
        }

        /// <summary>
        /// Loads the daily report document.
        /// </summary>
        /// <returns></returns>
        internal ReportDocument LoadDailyReportDocument(DateTime dateTime, int StaffId, int MachineId)
        {
            var dailyReport = m_reports.FirstOrDefault(r => r.Id == ReportId.B3Daily);

            if (dailyReport == null)
            {
                return null;
            }

            LoadCrystalReport(dailyReport);

            dailyReport.CrystalReportDocument.SetParameterValue("@SessionNum", null);
            dailyReport.CrystalReportDocument.SetParameterValue("@UserId", StaffId);
            dailyReport.CrystalReportDocument.SetParameterValue("@Station", MachineId);
            dailyReport.CrystalReportDocument.SetParameterValue("@DateTime", dateTime.Date.ToString(CultureInfo.InvariantCulture));

            return dailyReport.CrystalReportDocument;
        }

        /// <summary>
        /// Loads the drawer report document.
        /// </summary>
        /// <returns></returns>
        internal ReportDocument LoadDrawerReportDocument(DateTime dateTime, int MachineID, int StaffId)
        {
            var drawerReport = m_reports.FirstOrDefault(r => r.Id == ReportId.B3Drawer);

            if (drawerReport == null)
            {
                return null;
            }

            LoadCrystalReport(drawerReport);

            drawerReport.CrystalReportDocument.SetParameterValue("@MachineID", MachineID);
            drawerReport.CrystalReportDocument.SetParameterValue("@Station", MachineID);
            drawerReport.CrystalReportDocument.SetParameterValue("@nDate", dateTime.Date.ToString(CultureInfo.InvariantCulture));

            drawerReport.CrystalReportDocument.SetParameterValue("@UserId", StaffId);
          
            return drawerReport.CrystalReportDocument;
        }

        /// <summary>
        /// Loads the detail report document.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        internal ReportDocument LoadDetailReportDocument(DateTime start, DateTime end)
        {
            var detailReport = m_reports.FirstOrDefault(r => r.Id == ReportId.B3Detail);

            if (detailReport == null)
            {
                return null;
            }

            LoadCrystalReport(detailReport);

            detailReport.CrystalReportDocument.SetParameterValue("@dtStartDateTime", start);
            detailReport.CrystalReportDocument.SetParameterValue("@dtEndDateTime", end);


            return detailReport.CrystalReportDocument;
        }

     

        /// <summary>
        /// Loads the jackpot report document.
        /// </summary>
        /// <returns></returns>
        internal ReportDocument LoadJackpotReportDocument(DateTime dateTime, int staffId, int machineId)
        {
            var jackpotReport = m_reports.FirstOrDefault(r => r.Id == ReportId.B3Jackpot);

            if (jackpotReport == null)
            {
                return null;
            }

            LoadCrystalReport(jackpotReport);

            var sessionId = JackpotReportSessionSelected.Number;

            jackpotReport.CrystalReportDocument.SetParameterValue("@nSessNum", sessionId);
            jackpotReport.CrystalReportDocument.SetParameterValue("@UserId", staffId);
            jackpotReport.CrystalReportDocument.SetParameterValue("@Station", machineId);
            jackpotReport.CrystalReportDocument.SetParameterValue("@nDate", dateTime.Date.ToString(CultureInfo.InvariantCulture));

            return jackpotReport.CrystalReportDocument;
        }

        /// <summary>
        /// Loads the monthly report document.
        /// </summary>
        /// <returns></returns>
        internal ReportDocument LoadMonthlyReportDocument()
        {
            var monthlyReport = m_reports.FirstOrDefault(r => r.Id == ReportId.B3Monthly);

            if (monthlyReport == null)
            {
                return null;
            }

            LoadCrystalReport(monthlyReport);

            Month month;
            if (!Enum.TryParse(MonthlyReportMonthSelected, out month))
            {
                return null;
            }

            monthlyReport.CrystalReportDocument.SetParameterValue("@nMonth", (int)month);
            monthlyReport.CrystalReportDocument.SetParameterValue("@nYear", MonthlyReportYearSelected);

            return monthlyReport.CrystalReportDocument;
        }

        /// <summary>
        /// Loads the session transaction report document.
        /// </summary>
        /// <returns></returns>
        internal ReportDocument LoadSessionTranReportDocument(DateTime dateTime)
        {
            var sessionTranReport = m_reports.FirstOrDefault(r => r.Id == ReportId.B3SessionTransaction);

            if (sessionTranReport == null)
            {
                return null;
            }

            LoadCrystalReport(sessionTranReport);

            if (SessionReportSessionSelected == null)
            {
                return null;
            }

            var sessionId = SessionTranReportSessionSelected.Number;

            sessionTranReport.CrystalReportDocument.SetParameterValue("@SessionNumber", sessionId);
            sessionTranReport.CrystalReportDocument.SetParameterValue("@DateTime", dateTime);

            return sessionTranReport.CrystalReportDocument;
        }


        /// <summary>
        /// Loads the session report document.
        /// </summary>
        /// <returns></returns>
        internal ReportDocument LoadSessionReportDocument(DateTime dateTime, int StaffId, int MachineId)
        {
            var sessionReport = m_reports.FirstOrDefault(r => r.Id == ReportId.B3Session);

            if (sessionReport == null)
            {
                return null;
            }

            LoadCrystalReport(sessionReport);

            if (SessionReportSessionSelected == null)
            {
                return null;
            }

            var sessionId = SessionReportSessionSelected.Number;

            sessionReport.CrystalReportDocument.SetParameterValue("@SessionID", sessionId);
            sessionReport.CrystalReportDocument.SetParameterValue("@DateN", dateTime.Date.ToString(CultureInfo.InvariantCulture));
            sessionReport.CrystalReportDocument.SetParameterValue("@UserID", StaffId);
            sessionReport.CrystalReportDocument.SetParameterValue("@Station", MachineId);
        

            return sessionReport.CrystalReportDocument;
        }


        /// <summary>
        /// Loads the session summary report document.
        /// </summary>
        /// <returns></returns>
        internal ReportDocument LoadSessionSummaryReportDocument(DateTime dateTime, int StaffId, int MachineId)
        {
            var sessionReport = m_reports.FirstOrDefault(r => r.Id == ReportId.B3SessionSummary);

            if (sessionReport == null)
            {
                return null;
            }

            LoadCrystalReport(sessionReport);

            if (SessionReportSessionSelected == null)
            {
                return null;
            }

            var sessionId = SessionReportSessionSelected.Number;

            sessionReport.CrystalReportDocument.SetParameterValue("@SessionN", sessionId);
            sessionReport.CrystalReportDocument.SetParameterValue("@DateTime", dateTime.Date.ToString(CultureInfo.InvariantCulture));
            sessionReport.CrystalReportDocument.SetParameterValue("@UserID", StaffId);
            sessionReport.CrystalReportDocument.SetParameterValue("@Station", MachineId);


            return sessionReport.CrystalReportDocument;
        }

        /// <summary>
        /// Loads the Account History report document.
        /// </summary>
        /// <returns></returns>
        internal ReportDocument LoadAccountHistoryReportDocument(DateTime dateTime)
        {
            var accountHistoryReport = m_reports.FirstOrDefault(r => r.Id == ReportId.B3AccountHistory);

            if (accountHistoryReport == null)
            {
                return null;
            }

            LoadCrystalReport(accountHistoryReport);

            if (AccountHistoryReportSessionSelected == null)
            {
                return null;
            }

            var sessionId = AccountHistoryReportSessionSelected.Number;

            accountHistoryReport.CrystalReportDocument.SetParameterValue("@P_Date_", dateTime.Date.ToString(CultureInfo.InvariantCulture));
            accountHistoryReport.CrystalReportDocument.SetParameterValue("@SessionID_", sessionId);
            accountHistoryReport.CrystalReportDocument.SetParameterValue("@AccountNumber", AccountHistoryReportAccountSelected);

            return accountHistoryReport.CrystalReportDocument;
        }

        /// <summary>
        /// Loads the winner cards report document.
        /// </summary>
        /// <returns></returns>
        internal ReportDocument LoadWinnerCardsReportDocument(DateTime dateTime)
        {
            var WinnerCardsReport = m_reports.FirstOrDefault(r => r.Id == ReportId.B3WinnerCards);

            if (WinnerCardsReport == null)
            {
                return null;
            }

            LoadCrystalReport(WinnerCardsReport);

            if (WinnerCardsReportSessionSelected  == null)
            {
                return null;
            }

            var sessionId = WinnerCardsReportSessionSelected.Number;

            WinnerCardsReport.CrystalReportDocument.SetParameterValue("@SessionNum", sessionId);
            WinnerCardsReport.CrystalReportDocument.SetParameterValue("@DateRun", dateTime.Date.ToString(CultureInfo.InvariantCulture));

            return WinnerCardsReport.CrystalReportDocument;
        }

        /// <summary>
        /// Loads the ball call report document.
        /// </summary>
        /// <returns></returns>
        internal ReportDocument LoadBallCallReportDocument(DateTime startDate, DateTime endDate, int ballCalldefID)
        {
            var BallCallReport = m_reports.FirstOrDefault(r => r.Id == ReportId.B3BallCallByGame);

            if (ballCalldefID == 1)
            {
                if (BallCallReport == null)
                {
                    return null;
                }

                LoadCrystalReport(BallCallReport);

                if (BallCallReportSessionSelected == null)
                {
                    return null;
                }

                var sessionId = BallCallReportSessionSelected.Number;

                BallCallReport.CrystalReportDocument.SetParameterValue("@session", sessionId);
                BallCallReport.CrystalReportDocument.SetParameterValue("@DateParameter", startDate.Date.ToString(CultureInfo.InvariantCulture));


            }
            else
            {
                BallCallReport = m_reports.FirstOrDefault(r => r.Id == ReportId.B3BallCallBySession);

                if (BallCallReport == null)
                {
                    return null;
                }

                LoadCrystalReport(BallCallReport);
                BallCallReport.CrystalReportDocument.SetParameterValue("@StartDate", startDate.Date.ToString(CultureInfo.InvariantCulture));
                BallCallReport.CrystalReportDocument.SetParameterValue("@EndDate",  endDate.Date.ToString(CultureInfo.InvariantCulture));

            }

            return BallCallReport.CrystalReportDocument;
        }

        /// <summary>
        /// Loads the void report document.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        internal ReportDocument LoadVoidReportDocument(DateTime start, DateTime end)
        {
            var voidReport = m_reports.FirstOrDefault(r => r.Id == ReportId.B3Void);

            if (voidReport == null)
            {
                return null;
            }

            LoadCrystalReport(voidReport);

            voidReport.CrystalReportDocument.SetParameterValue("@dtStartDateTime", start);
            voidReport.CrystalReportDocument.SetParameterValue("@dtEndDateTime", end);

            return voidReport.CrystalReportDocument;
        }


        /// <summary>
        /// Loads the bingo card report document.
        /// </summary>
        /// <returns></returns>
        internal ReportDocument LoadBingoCardReportDocument(int startCard, int endCard)
        {
            var bingoCardReport = m_reports.FirstOrDefault(r => r.Id == ReportId.B3BingoCardReport);

            if (bingoCardReport == null)
            {
                return null;
            }

            LoadCrystalReport(bingoCardReport);



            bingoCardReport.CrystalReportDocument.SetParameterValue("@startId", startCard);
            bingoCardReport.CrystalReportDocument.SetParameterValue("@endId", endCard);

            //bingoCardReport.CrystalReportDocument.SetParameterValue("@SessionNum", 1);
            //bingoCardReport.CrystalReportDocument.SetParameterValue("@GamingDate", "1/1/2000 00:00:00");


            return bingoCardReport.CrystalReportDocument;
        }

        /// <summary>
        /// Loads the crystal report.
        /// </summary>
        /// <param name="report">The report.</param>
        private void LoadCrystalReport(B3Report report)
        {
            var server = "b3-server";//m_controller.Settings.DatabaseServer;
            var name = m_controller.Settings.DatabaseName;
            var user = m_controller.Settings.DatabaseUser;
            var password = "cobalt$45";//m_controller.Settings.DatabasePassword;
            //report.LoadCrystalReport(string.Empty, string.Empty, string.Empty, string.Empty);
            report.LoadCrystalReport(server, name, user, password);
        }

        /// <summary>
        /// Updates the jackpot report sessions by date.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        internal void UpdateJackpotReportSessionsByDate(DateTime datetime)
        {
            JackpotReportSessionList.Clear();
            foreach (var session in SessionList)
            {
                var sessionStartDateTime = DateTime.Parse(session.SessionStartTime);
                  var sessionEndDateTime = DateTime.Parse(session.SessionEndTime);

                  if (sessionStartDateTime == sessionEndDateTime)
                  {
                      sessionEndDateTime = DateTime.Today;
                  }

                if (sessionStartDateTime.Year == datetime.Year &&
                    sessionStartDateTime.Month == datetime.Month &&
                    (sessionStartDateTime.Day <= datetime.Day && sessionEndDateTime.Day >= datetime.Day))
                {
                    JackpotReportSessionList.Add(session);
                }
            }

            JackpotReportSessionSelected = JackpotReportSessionList.LastOrDefault();

            if (JackpotReportSessionList.Count == 0)
            {
                EnableJackpotReportButtons = false;
            }
            else
            {
                EnableJackpotReportButtons = true;
            }
        }

          /// <summary>
        /// Updates the session transaction report sessions by date.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        internal void UpdateSessionTranReportSessionsByDate(DateTime datetime)
        {          
            SessionTranReportSessionList.Clear();

            foreach (var session in SessionList)
            {
                var sessionStartDateTime = DateTime.Parse(session.SessionStartTime);
                var sessionEndDateTime = DateTime.Parse(session.SessionEndTime);

                if (sessionStartDateTime == sessionEndDateTime)
                {
                    sessionEndDateTime = DateTime.Today;
                }

                if (sessionStartDateTime.Year == datetime.Year &&
                    sessionStartDateTime.Month == datetime.Month &&
                    (sessionStartDateTime.Day <= datetime.Day && sessionEndDateTime.Day >= datetime.Day))

                {
                    SessionTranReportSessionList.Add(session);
                }
              
            }

            SessionTranReportSessionSelected = SessionTranReportSessionList.LastOrDefault();

            if (SessionTranReportSessionList.Count == 0)
            {
                EnableSessionReportButtons = false;
            }
            else
            {
                EnableSessionReportButtons = true;
            }
        }

        /// <summary>
        /// Updates the session report sessions by date.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        internal void UpdateSessionReportSessionsByDate(DateTime datetime)
        {
            SessionReportSessionList.Clear();

            foreach (var session in SessionList)
            {
                var sessionStartDateTime = DateTime.Parse(session.SessionStartTime);
                var sessionEndDateTime = DateTime.Parse(session.SessionEndTime);

                if (sessionStartDateTime == sessionEndDateTime)
                {
                    sessionEndDateTime = DateTime.Today;
                }

                if (sessionStartDateTime.Year == datetime.Year &&
                    sessionStartDateTime.Month == datetime.Month &&
                    (sessionStartDateTime.Day <= datetime.Day && sessionEndDateTime.Day >= datetime.Day))

                {
                    SessionReportSessionList.Add(session);
                }
              
            }

            SessionReportSessionSelected = SessionReportSessionList.LastOrDefault();

            if (SessionReportSessionList.Count == 0)
            {
                EnableSessionReportButtons = false;
            }
            else
            {
                EnableSessionReportButtons = true;
            }

        }

        /// <summary>
        /// Updates the session report sessions by date.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        internal void UpdateWinnerCardsSessionsByDate(DateTime datetime)
        {
            WinnerCardsReportSessionList.Clear();

            foreach (var session in SessionList)
            {
                var sessionStartDateTime = DateTime.Parse(session.SessionStartTime);
                var sessionEndDateTime = DateTime.Parse(session.SessionEndTime);

                if (sessionStartDateTime == sessionEndDateTime)
                {
                    sessionEndDateTime = DateTime.Today;
                }

                if (sessionStartDateTime.Year == datetime.Year &&
                    sessionStartDateTime.Month == datetime.Month &&
                    (sessionStartDateTime.Day <= datetime.Day && sessionEndDateTime.Day >= datetime.Day))

                {
                    WinnerCardsReportSessionList.Add(session);
                }
              
            }

            WinnerCardsReportSessionSelected = WinnerCardsReportSessionList.LastOrDefault();

            if (WinnerCardsReportSessionList.Count == 0)
            {
                EnableSessionReportButtons = false;
            }
            else
            {
                EnableSessionReportButtons = true;
            }

        }

        /// <summary>
        /// Updates the ball call report sessions by date.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        internal bool UpdateBallCallReportSessionsByDate(DateTime datetime)
        {

            BallCallReportSessionList.Clear();

            foreach (var session in SessionList)
            {
                var sessionStartDateTime = DateTime.Parse(session.SessionStartTime);
                var sessionEndDateTime = DateTime.Parse(session.SessionEndTime);

                if (sessionStartDateTime == sessionEndDateTime)
                {
                    sessionEndDateTime = DateTime.Today;
                }

                if (sessionStartDateTime.Year == datetime.Year &&
                    sessionStartDateTime.Month == datetime.Month &&
                    (sessionStartDateTime.Day <= datetime.Day && sessionEndDateTime.Day >= datetime.Day))

                {
                    BallCallReportSessionList.Add(session);
                }      
            }

            BallCallReportSessionSelected = BallCallReportSessionList.LastOrDefault();

            if (BallCallReportSessionList.Count == 0)
            {
                EnableSessionReportButtons = false;
            }
            else
            {
                EnableSessionReportButtons = true;
            }

            return EnableSessionReportButtons;
        }

        /// <summary>
        /// Updates the account history report sessions by date.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        internal void UpdateAccountHistoryReportSessionsByDate(DateTime datetime)
        {
            AccountHistoryReportSessionList.Clear();
            EnableAccountHistoryReportButtons = false;

            foreach (var session in SessionList)
            {
                var sessionStartDateTime = DateTime.Parse(session.SessionStartTime);
                var sessionEndDateTime = DateTime.Parse(session.SessionEndTime);

                if (sessionStartDateTime == sessionEndDateTime)
                {
                    sessionEndDateTime = DateTime.Today;
                }

                if (sessionStartDateTime.Year == datetime.Year &&
                    sessionStartDateTime.Month == datetime.Month &&
                    (sessionStartDateTime.Day <= datetime.Day && sessionEndDateTime.Day >= datetime.Day))
                {
                    AccountHistoryReportSessionList.Add(session);
                }
              
            }

            if (AccountHistoryReportSessionList.Count != 0)
            {
                AccountHistoryReportSessionSelected = AccountHistoryReportSessionList.LastOrDefault();
            }
            else
            {
                AccountHistoryReportAccountList.Clear();
                AccountHistoryReportAccountSelected = new int();
            }
        }

        /// <summary>
        /// Updates the Account History report sessions by date.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        internal void UpdateAccountHistoryReportAccountBySession(int SessionNumber)
        {
            AccountHistoryReportAccountList.Clear();
            AccountHistoryReportAccountSelected = new int();
           Messages.GetB3AccountNumber msg = new Messages.GetB3AccountNumber(SessionNumber);
           msg.Send();
           AccountList = new ObservableCollection<int>();
           //AccountList = msg.AccountNumberList;

            foreach (int account in AccountList)
            {
                AccountHistoryReportAccountList.Add(account);
            }

            if (AccountHistoryReportAccountList.Count != 0)
            {
                AccountHistoryReportAccountSelected = AccountHistoryReportAccountList.LastOrDefault();
                EnableAccountHistoryReportButtons = true;
            }
            else
            {
                EnableAccountHistoryReportButtons = false;
            }
        }

        /// <summary>
        /// Load item for ball call report definition list.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        internal void LoadBallCallReportDefList()
        {
            BallCallReportDefList.Add("By Session");
            BallCallReportDefList.Add("By Game");

             BallCallReportDefSelected  = BallCallReportDefList.LastOrDefault();
        }

        internal bool ValidateUserInputForBingoCardReport(int startingCardN, int EndingCardN)
        {
            bool IsValid = false;
            if (startingCardN != 0 && EndingCardN  != 0)
            {
                if (startingCardN > EndingCardN)
                {

                }
                else
                {
                    IsValid = true;
                }
            }
            else
            {
               // IsValid = false;
            }

            EnableBingoCardReportButtons = IsValid;
            return IsValid;

        }



        /// <summary>
        /// Prints the report.
        /// </summary>
        /// <param name="reportId">The report identifier.</param>
        /// <param name="report">The report.</param>
        /// <returns></returns>
        internal bool PrintReport(ReportId reportId, ReportDocument report)
        {
            var returnValue = false;
            switch (reportId)
            {
                case ReportId.B3Accounts:
                case ReportId.B3Detail:
                case ReportId.B3Monthly:
                case ReportId.B3Void:
                    {
                        //try to print to report printer
                        returnValue = TryPrintGlobalPrinter(report);
                    }
                    break;

                case ReportId.B3Daily:
                case ReportId.B3Drawer:
                case ReportId.B3Jackpot:
                case ReportId.B3Session:
                    {
                        //try to print to receipt printer
                        returnValue = TryPrintReceiptPrinter(report);

                        //if unsuccessful then try to print to report printer
                        if (!returnValue)
                        {
                            returnValue = TryPrintReceiptPrinter(report);
                        }
                    }
                    break;
            }

            return returnValue;
        }

        /// <summary>
        /// Tries the print receipt printer.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <returns></returns>
        private bool TryPrintReceiptPrinter(ReportDocument report)
        {
            var returnValue = true;
            try
            {
                report.PrintOptions.PrinterName = Settings.ReceiptPrinterName;
                report.PrintToPrinter(1, true, 0, 0);
            }
            catch (Exception)
            {
                //if throw exception then set flag to false
                returnValue = false;
            }

            return returnValue;
        }

        /// <summary>
        /// Tries the print global printer.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <returns></returns>
        private bool TryPrintGlobalPrinter(ReportDocument report)
        {
            var returnValue = true;
            try
            {
                report.PrintOptions.PrinterName = Settings.PrinterName;
                report.PrintToPrinter(1, true, 0, 0);
            }
            catch (Exception)
            {
                //if throw exception then set flag to false
                returnValue = false;
            }

            return returnValue;
        }



        
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
        /// Occurs when [full screen event].
        /// </summary>
        public event EventHandler<EventArgs> FullScreenEvent;

        /// <summary>
        /// Occurs when [exit screen event].
        /// </summary>
        public event EventHandler<EventArgs> ExitScreenEvent;
        




      

       

        /// <summary>
        /// Called when [list information done].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="AsyncCompletedEventArgs"/> instance containing the event data.</param>
        private void OnListInfoDone(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                //update session list
                if (m_controller.Sessions.Count != SessionList.Count)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        //clear and redefine list
                        SessionList.Clear();
                        foreach (var session in m_controller.Sessions)
                        {
                            SessionList.Add(session);
                        }

                        SessionReportSessionSelected = SessionList.LastOrDefault();
                        JackpotReportSessionSelected = SessionList.LastOrDefault();
                    }));
                }
            }
        }

        
        #endregion




      
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


        #region Command



        private void SetCommand()
        {
            ViewReportCommand = new RelayCommand(parameter => ViewReportRel(ReportSelected.Id));
        }
        public ICommand ViewReportCommand { get; set; }

        public void ViewReportRel(ReportId reportID)
        {            
            //ViewPrintButtonVisibility = Visibility.Collapsed;

            //Task.Factory.StartNew(() =>
            //{
            //    IsLoading = true;
            //reportID = ReportId.B3BallCallBySession;
            try
            {
                var bingoCardReport = m_reports.FirstOrDefault(r => r.Id == /*ReportId.B3BingoCardReport*/ reportID);


                if (bingoCardReport == null)
                {
                    return;
                }
                LoadCrystalReport(bingoCardReport);

                //var report = LoadBingoCardReportDocument(1, 1);
                var report = m_rptBaseVm.LoadReportDocument(bingoCardReport);


                if (report == null)
                {
                    IsLoading = false;
                    return;
                }

                //Dispatcher.Invoke(new Action(() =>
                //{
                tempcr.ViewerCore.ReportSource = report;
                tempcr.Focusable = true;
                tempcr.Focus();
                //}));
            }
            catch (Exception ex)
            {
                //display message box on UI thread
                //Dispatcher.Invoke(new Action(() =>
                //{
                //error
                MessageWindow.Show(
                    string.Format(CultureInfo.CurrentCulture, Properties.Resources.ErrorLoadingReport,
                        ex.Message), Properties.Resources.B3CenterName, MessageWindowType.Close);
                //}));
            }
            finally
            {
                IsLoading = false;
            }
            //});
            DefaultViewMode = Visibility.Collapsed;
            CRViewMode = Visibility.Visible;
            //m_bingocardvm.ReportViewerVisibility = Visibility.Visible;
            //m_bingocardvm.ReportParameterVisible = Visibility.Collapsed;
            m_rptBaseVm.vReportViewer = tempcr;


        }


        #endregion

    }
}
