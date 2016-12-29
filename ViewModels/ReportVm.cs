
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.


using System;
using System.Collections;
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
using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using GameTech.Elite.Client.Modules.B3Center.Model;
using GameTech.Elite.Client.Modules.B3Center.UI.ReportViews;
using System.Windows.Controls;


namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{
    internal class ReportVm : ViewModelBase
    {

        #region
        private bool m_enableJackpotReportButtons;
        private bool m_enableSessionReportButtons;
        private bool m_enableAccountHistoryReportButtons;
        private bool m_enableBingoCardReportButton;
        private bool m_isLoading;
        private bool m_isPrinting;
        private B3Controller m_controller;
        private ObservableCollection<Session> m_sessionList = new ObservableCollection<Session>();
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
        private static volatile ReportVm m_instance;
        private static readonly object m_syncRoot = new Object();
        private int m_accountNumberSelected;

        private AccountHistoryReportVm m_vm = new AccountHistoryReportVm();
        private AccountReportVm m_vm2 = new AccountReportVm();

        //public   AccountHistoryReportView xyz
        //{ 
        //    get; 
        //    set; 
        //}

        //public  AccountsReportView xyz2
        //{
        //    get;
        //    set;
        //}


        //public ContentPresenter ReportTransitionControlContent
        //{
        //    get { return m_reportTCContent; }
        //    set { m_reportTCContent = value;}
        //}

        #endregion


        ObservableCollection<Model.Reports> m_reportList2;// = new ObservableCollection<Model.Reports>();

        public ObservableCollection<Model.Reports> ReportsList2
        {
            get 
            { 
                return m_reportList2; 
            }
            set 
            { 
                m_reportList2 = value;
                RaisePropertyChanged("ReportsList2");
            }
        }



        public ReportVm(B3Controller controller)
        {


            //m_reportList2 = new ObservableCollection<Model.Reports>()
            //{
            //    new Model.Reports(){ReportID = 1, ReportName = "Accounts"}, //ReportView = new AccountsReportView() {DataContext =  new AccountReportVm() }},
            //   new Model.Reports(){ReportID = 1, ReportName = "Account History"}//, ReportView = new AccountHistoryReportView() {DataContext =  new AccountHistoryReportVm() }}
            //};
        }


        public List<string> textx1
        {
            get { return m_reportList; }
            set { m_reportList = value; }
        }

        List<string> m_reportList = new List<string>();


        private void LoadReportList()
        {
            m_reportList.Clear();
            m_reportList.Add("Accounts");
            m_reportList.Add("Account History");
            m_reportList.Add("Ball Call");
            m_reportList.Add("Bingo Card");
            m_reportList.Add("Daily");
            m_reportList.Add("Detail");
            m_reportList.Add("Drawer");
            m_reportList.Add("Jackpot");
            m_reportList.Add("Monthly");
            m_reportList.Add("Session");
            m_reportList.Add("Session Summary");
            m_reportList.Add("Session Transaction");
            m_reportList.Add("Void");
            m_reportList.Add("Winner Cards");
            //m_reportSelected = m_reportList.FirstOrDefault();
        }


        private Report m_reportSelected;

        public Report ReportSelected
        {
            get { return m_reportSelected; }
            set 
            { 
                m_reportSelected = value; 
               RaisePropertyChanged("ReportSelected");
            
            }
        }


/*
            m_controller = controller;

            m_controller.SessionInfoCompleted += OnListInfoDone;


            foreach (var session in controller.Sessions)
            {
                m_sessionList.Add(session);
            }

            SessionReportSessionSelected = SessionList.LastOrDefault();
            JackpotReportSessionSelected = SessionList.LastOrDefault();

            LoadReportList();
            m_reports = controller.Reports;
            xyz = new AccountHistoryReportView() { DataContext = m_vm };
            xyz2 = new AccountsReportView() { DataContext = m_vm2 };
            m_reportTCContent.Content = xyz2;          
*/
       // }


        





      

        //private void ActivateReportView(string reportSelected)
        //{
        //    switch (reportSelected)
        //    {
        //        case "Accounts":
        //            break;

        //    }
        //}


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



    
        internal void UpdateAccountHistoryReportAccountBySession(int SessionNumber)
        {
            AccountHistoryReportAccountList.Clear();
            AccountHistoryReportAccountSelected = new int();
            Messages.GetB3AccountNumber msg = new Messages.GetB3AccountNumber(SessionNumber);
            msg.Send();
            AccountList = new ObservableCollection<int>();
            AccountList = msg.AccountNumberList;

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
            get { return m_enableAccountHistoryReportButtons; }
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

      

    }
      
}
