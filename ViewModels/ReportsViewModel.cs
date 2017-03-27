#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
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

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{
    public class ReportsViewModel : ViewModelBase
    {
        #region Local variables

        private bool m_enableJackpotReportButtons;
        private bool m_enableSessionReportButtons;
        private bool m_enableAccountHistoryReportButtons;
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
        private IEnumerable<int> m_years;

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
        private int m_monthlyReportYearSelected;
        private string m_accountReportMonthSelected;
        private int m_accountReportYearSelected;
        private static volatile ReportsViewModel m_instance;
        private static readonly object m_syncRoot = new Object();
        private int m_accountNumberSelected;
      

        #endregion

        #region Constructors

        /// <summary>
        /// Represents the view model for managing reports
        /// </summary>
        private ReportsViewModel()
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

            Months = Enum.GetNames(typeof(Month)).Where(m => m != Month.NotSet.ToString());

            var years = new List<int>();
            for (var i = DateTime.Now.Year; i > DateTime.Now.Year - 50; i--)
            {
                years.Add(i);
            }

            Years = years;

            //Get current month months
            var currentMonth = Months.FirstOrDefault(m => m == ((Month)DateTime.Now.Month).ToString());
            var currentYear = DateTime.Now.Year;

            //Set MonthlyReport
            MonthlyReportMonthSelected = currentMonth;
            MonthlyReportYearSelected = currentYear;

            //Set Account Report
            AccountReportMonthSelected = currentMonth;
            AccountReportYearSelected = currentYear;

            IsLoading = false;
            IsPrinting = false;
        }

        #endregion

        #region Properties

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

        public IEnumerable<int> Years
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
        public int MonthlyReportYearSelected
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
        public int AccountReportYearSelected
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

        internal B3CenterSettings Settings
        {
            get { return m_controller.Settings; }
        }

        #endregion

        #region Member Methods

        /// <summary>
        /// Initializes the ViewModel with the specified controller.
        /// </summary>
        /// <param name="controller">The controller.</param>
        internal void Initialize(B3Controller controller)
        {
            m_controller = controller;

            m_controller.SessionInfoCompleted += OnListInfoDone;

            //set session list
            foreach (var session in controller.Sessions)
            {
                SessionList.Add(session);
            }

            SessionReportSessionSelected = SessionList.LastOrDefault();
            JackpotReportSessionSelected = SessionList.LastOrDefault();
            AccountHistoryReportSessionSelected  = SessionList.LastOrDefault();

            m_reports = controller.Reports;
        }

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
        internal ReportDocument LoadDailyReportDocument(DateTime dateTime)
        {
            var dailyReport = m_reports.FirstOrDefault(r => r.Id == ReportId.B3Daily);

            if (dailyReport == null)
            {
                return null;
            }

            LoadCrystalReport(dailyReport);

            dailyReport.CrystalReportDocument.SetParameterValue("@SessionNum", null);
            dailyReport.CrystalReportDocument.SetParameterValue("@UserId", null);
            dailyReport.CrystalReportDocument.SetParameterValue("@Station", null);
            dailyReport.CrystalReportDocument.SetParameterValue("@DateTime", dateTime.Date.ToString(CultureInfo.InvariantCulture));

            return dailyReport.CrystalReportDocument;
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
        /// Loads the drawer report document.
        /// </summary>
        /// <returns></returns>
        internal ReportDocument LoadDrawerReportDocument(DateTime dateTime)
        {
            var drawerReport = m_reports.FirstOrDefault(r => r.Id == ReportId.B3Drawer);

            if (drawerReport == null)
            {
                return null;
            }

            LoadCrystalReport(drawerReport);

            drawerReport.CrystalReportDocument.SetParameterValue("@SessionNumber", null);
            drawerReport.CrystalReportDocument.SetParameterValue("@cClientMac", null);
            drawerReport.CrystalReportDocument.SetParameterValue("@cClientName", null);
            drawerReport.CrystalReportDocument.SetParameterValue("@nDate", dateTime.Date.ToString(CultureInfo.InvariantCulture));

            return drawerReport.CrystalReportDocument;
        }

        /// <summary>
        /// Loads the jackpot report document.
        /// </summary>
        /// <returns></returns>
        internal ReportDocument LoadJackpotReportDocument(DateTime dateTime)
        {
            var jackpotReport = m_reports.FirstOrDefault(r => r.Id == ReportId.B3Jackpot);

            if (jackpotReport == null)
            {
                return null;
            }

            LoadCrystalReport(jackpotReport);

            var sessionId = JackpotReportSessionSelected.Number;

            jackpotReport.CrystalReportDocument.SetParameterValue("@nSessNum", sessionId);
            jackpotReport.CrystalReportDocument.SetParameterValue("@UserId", null);
            jackpotReport.CrystalReportDocument.SetParameterValue("@Station", null);
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
        internal ReportDocument LoadSessionTranReportDocument(/*DateTime dateTime*/)
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

            return sessionTranReport.CrystalReportDocument;
        }


        /// <summary>
        /// Loads the session report document.
        /// </summary>
        /// <returns></returns>
        internal ReportDocument LoadSessionReportDocument(DateTime dateTime)
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
            sessionReport.CrystalReportDocument.SetParameterValue("@UserID", null);
            sessionReport.CrystalReportDocument.SetParameterValue("@Station", null);
        

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
        /// Loads the crystal report.
        /// </summary>
        /// <param name="report">The report.</param>
        private void LoadCrystalReport(B3Report report)
        {
            var server = "B3-Server"; //m_controller.Settings.DatabaseServer;
            var name = m_controller.Settings.DatabaseName;
            var user = m_controller.Settings.DatabaseUser;
            var password = m_controller.Settings.DatabasePassword;
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
                var sessionDateTime = DateTime.Parse(session.SessionStartTime);
                if (sessionDateTime.Year == datetime.Year &&
                    sessionDateTime.Month == datetime.Month &&
                    sessionDateTime.Day == datetime.Day)
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
                var sessionDateTime = DateTime.Parse(session.SessionStartTime);
                if (sessionDateTime.Year == datetime.Year &&
                    sessionDateTime.Month == datetime.Month &&
                    sessionDateTime.Day == datetime.Day)
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
                var sessionDateTime = DateTime.Parse(session.SessionStartTime);
                if (sessionDateTime.Year == datetime.Year &&
                    sessionDateTime.Month == datetime.Month &&
                    sessionDateTime.Day == datetime.Day)
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
                var sessionDateTime = DateTime.Parse(session.SessionStartTime);
                if (sessionDateTime.Year == datetime.Year &&
                    sessionDateTime.Month == datetime.Month &&
                    sessionDateTime.Day == datetime.Day)
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
        internal void UpdateBallCallReportSessionsByDate(DateTime datetime)
        {

            BallCallReportSessionList.Clear();

            foreach (var session in SessionList)
            {
                var sessionDateTime = DateTime.Parse(session.SessionStartTime);
                if (sessionDateTime.Year == datetime.Year &&
                    sessionDateTime.Month == datetime.Month &&
                    sessionDateTime.Day == datetime.Day)
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
        }

        /// <summary>
        /// Updates the account history report sessions by date.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        internal void UpdateAccountHistoryReportSessionsByDate(DateTime datetime)
        {

            AccountHistoryReportSessionList.Clear();
            AccountHistoryReportAccountList.Clear();
            EnableSessionReportButtons = false;

            foreach (var session in SessionList)
            {
                var sessionDateTime = DateTime.Parse(session.SessionStartTime);
                if (sessionDateTime.Year == datetime.Year &&
                    sessionDateTime.Month == datetime.Month &&
                    sessionDateTime.Day == datetime.Day)
                {
                    AccountHistoryReportSessionList.Add(session);
                }
            }

            AccountHistoryReportSessionSelected = AccountHistoryReportSessionList.LastOrDefault();
            AccountHistoryReportAccountSelected = AccountHistoryReportAccountList.LastOrDefault();
          
        }

        /// <summary>
        /// Updates the Account History report sessions by date.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        internal void UpdateAccountHistoryReportAccountBySession(int SessionNumber)
        {
          
           Messages.GetB3AccountNumber msg = new Messages.GetB3AccountNumber(SessionNumber);

           AccountList = new ObservableCollection<int>();
           AccountList = msg.AccountNumberList;

            foreach (int account in AccountList)
            {
                AccountHistoryReportAccountList.Add(account);
            }

            AccountHistoryReportAccountSelected = AccountHistoryReportAccountList.LastOrDefault();

            if (AccountHistoryReportAccountList.Count != 0)
            {
                EnableAccountHistoryReportButtons = true;
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
    }
}
