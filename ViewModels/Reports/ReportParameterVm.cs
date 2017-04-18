using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Model;
using System.Windows;
using GameTech.Elite.Reports;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Shared;
using GameTech.Elite.Client.Modules.B3Center.Model.Shared;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{

    public class ReportParameterViewModel : ViewModelBase
    {
        #region MEMBER VARIABLE

        private ObservableCollection<string> m_accountList;
        private List<string> m_categoryList;
        private ObservableCollection<Session> m_sessionList;
        private Visibility m_visibility;
        private ObservableCollection<string> m_months;

        #endregion      
        #region CONSTRUCTORS

        public ReportParameterViewModel(List<string> paramlist, ReportParameterModel rptParameter)
        {
            m_accountList = new ObservableCollection<string>();
            m_sessionList = new ObservableCollection<Session>();
            RptParameterDataHandler = rptParameter;
            HideAllparameter();
            HideEnableParamControls(paramlist);
            if (rptParameter.Rptid == ReportId.B3AccountHistory)
            {
                SelectedSessionChange();
            }
            CheckUserValidation();
        }

        #endregion
        #region METHOD

        //This also trigger from codebehind ReportParameter.xaml.cs
        public void SelectedSessionChange()
        {
            if (RptParameterDataHandler.Rptid == ReportId.B3AccountHistory)
            {
                if (SelectedSession != null)
                {
                    UpdateAccountList();
                }
            }
        }

        //Another validation from code behind.
        //Enable or disable view and print button.
        public void ValidateCard(string startingCard, string endingCard)
        {
            bool viewReportVisibility = false;
 

            int tempStartingCard;
            int tempEndingCard;

            var tempResultsc = int.TryParse(startingCard, out tempStartingCard); //Right now user can enter any character on the textbox 
            var tempResultec = int.TryParse(endingCard, out tempEndingCard);

            if (tempResultec && tempResultsc)
            {
                if (tempStartingCard > tempEndingCard)
                {
                    viewReportVisibility = false;
                }
                else
                {
                    if (tempEndingCard == 0)
                    {
                        viewReportVisibility = false;
                    }
                    else
                    {
                        viewReportVisibility = true;
                    }
                }
            }
            else
            {
                viewReportVisibility = false;
            }

            var ii = ReportsViewModel.Instance;
            ii.ViewReportVisibility = viewReportVisibility;
        }


        //For all user validation (Enable or disable View and print button in the B3Report)
        public void CheckUserValidation()
        {
            bool viewReportVisibility = false;

            switch (RptParameterDataHandler.Rptid)
            {
                case ReportId.B3AccountHistory://For account
                    {
                        if (AccountSelected != null)
                        {
                            viewReportVisibility = true;
                        }
                        else
                        {
                            viewReportVisibility = false;
                        }
                        break;
                    }
                case ReportId.B3BallCallByGame://For Session
                case ReportId.B3Jackpot:
                case ReportId.B3Session:
                case ReportId.B3SessionSummary:
                case ReportId.B3SessionTransaction:
                case ReportId.B3WinnerCards:
                    {
                        if (SelectedSession != null)
                        {
                            viewReportVisibility = true;
                        }
                        else
                        {
                            viewReportVisibility = false;
                        }
                        break;
                    }
             
                case ReportId.B3Detail:
                case ReportId.B3BallCallBySession:
                case ReportId.B3Void:
                    {
                        DateTime tempStartDate = DateTime.Parse(StartDatePickerVm.DatepickerModel.DateFullwTime);//No need to check if its a date. It will always be a date
                        DateTime tempEndDate = DateTime.Parse(EndDatePickerVm.DatepickerModel.DateFullwTime);
                        //Loop 28x
                        if (tempStartDate > tempEndDate)
                        {
                            viewReportVisibility = false;
                        }
                        else
                        {
                            viewReportVisibility = true;
                        }

                        break;
                    }
                case ReportId.B3Monthly:
                case ReportId.B3Drawer:
                case ReportId.B3Daily:
                case ReportId.B3Accounts:
                    {
                        viewReportVisibility = true;
                        break;
                    }
            }

               if ( ReportId.B3BingoCardReport != RptParameterDataHandler.Rptid)
                    {
                        var ii = ReportsViewModel.Instance;
                        ii.ViewReportVisibility = viewReportVisibility;
                    }          
        }

        private bool IsShowTime()
        {
            bool result = false;
            if (RptParameterDataHandler.Rptid == ReportId.B3Detail || RptParameterDataHandler.Rptid == ReportId.B3Void)
            {
                result = true;
            }
            return result;
        }

        public bool WorkInProgress { get; set; }

        public void UpdateSessionList(DateTime selectedDateTime)
        {
            WorkInProgress = true;

            m_sessionList.Clear();
            WorkInProgress = false;

            foreach (var session in ReportsViewModel.Instance.GetSessionList())
            {
                var sessionStartDateTime = DateTime.Parse(session.SessionStartTime);
                var sessionEndDateTime = DateTime.Parse(session.SessionEndTime);

                if (sessionStartDateTime == sessionEndDateTime)
                {
                    sessionEndDateTime = DateTime.Today;
                }

                if (sessionStartDateTime.Year == selectedDateTime.Year &&
                    sessionStartDateTime.Month == selectedDateTime.Month && sessionStartDateTime.Day <= selectedDateTime.Day && sessionEndDateTime.Day >= selectedDateTime.Day)
                {
                    WorkInProgress = true;
                    m_sessionList.Add(session);
                    WorkInProgress = false;
                }          
            }

            
            if (m_sessionList.Count != 0)
            {
                SessionList = m_sessionList;
                SelectedSession = m_sessionList.LastOrDefault();
                var rptvm = ReportsViewModel.Instance;
                rptvm.NoSession = false;
                rptvm.SetLabelMessageToUser();
                IsSessionEnable = true;
            }
            else
            {
                var rptvm = ReportsViewModel.Instance;
                rptvm.NoSession = true;
                IsSessionEnable = false;          
                rptvm.SetLabelMessageToUser();

                if (RptParameterDataHandler.Rptid == ReportId.B3AccountHistory)
                {
                    m_accountList.Clear();
                    IsAccountEnable = false;
                }
                else
                {                  
                }
            }
            CheckUserValidation();
        }

        public void UpdateAccountList()
        {
            if (SelectedSession != null)
            {
                m_accountList.Clear();
                if (RptParameterDataHandler.Rptid == ReportId.B3AccountHistory)
                {
                    Messages.GetB3AccountNumber msg = new Messages.GetB3AccountNumber(SelectedSession.Number);
                    msg.Send();
                    if (msg.AccountNumberList.Count != 0)
                    {
                        AccountList = msg.AccountNumberList;
                        AccountSelected = m_accountList.FirstOrDefault();
                        var rptvm = ReportsViewModel.Instance;
                        rptvm.NoAccount = false;
                        rptvm.SetLabelMessageToUser();
                        IsAccountEnable = true;
                    }
                    else
                    {
                        var rptvm = ReportsViewModel.Instance;
                        rptvm.NoAccount = true;
                        rptvm.SetLabelMessageToUser();
                        IsAccountEnable = false;
                    }
                    CheckUserValidation();
                }
            }
        }

        private void HideEnableParamControls(List<string> paramlist)
        {
            foreach (string param in paramlist)
            {
                switch (param)
                {

                    case "Date":
                        {
                            DatePickerVm = new DatePickerVm(RptParameterDataHandler.B3DateData, false);
                            DateInput = Visibility.Visible;
                            break;
                        }
                    case "MonthYear":
                        {
                            DatePickerVm = new DatePickerVm(RptParameterDataHandler.B3DateData, false);
                            Months = DatePickerVm.MonthList;
                            MonthSelected = DatePickerVm.SelectedMonth;//m_months.FirstOrDefault();
                            Years = DatePickerVm.YearList;
                            YearSelected = DatePickerVm.SelectedYear;
                            MonthYearInput = Visibility.Visible;
                            break;
                        }
                    case "StartEndDate":
                        {
                            StartDatePickerVm = new DatePickerVm(RptParameterDataHandler.StartDate, false);
                            EndDatePickerVm = new DatePickerVm(RptParameterDataHandler.EndDate, false);
                            StartEndDate = Visibility.Visible;
                            break;
                        }
                    case "Session":
                        {
                            m_sessionList = new ObservableCollection<Session>();
                            UpdateSessionList(DateTime.Now);
                            SessionInput = Visibility.Visible;
                            break;
                        }
                    case "AccountNumber":
                        {
                            m_accountList = new ObservableCollection<string>();
                            UpdateAccountList();
                            AccountNumberInput = Visibility.Visible;
                            break;
                        }
                    case "Category":
                        {
                            m_categoryList = new List<string>();
                            m_categoryList.Add("By Game");
                            m_categoryList.Add("By Session");
                            CategorySelected = m_categoryList.FirstOrDefault();
                            CategoryList = m_categoryList;
                            CategoryInput = Visibility.Visible;
                            break;
                        }
                    case "StartEndCard":
                        {
                            StartEndCardInput = Visibility.Visible;
                            break;
                        }
                    case "StartEndDatewTime":
                        {
                            StartDatePickerVm = new DatePickerVm(RptParameterDataHandler.StartDate, true);
                            EndDatePickerVm = new DatePickerVm(RptParameterDataHandler.EndDate, true);
                            StartEndDateWTime = Visibility.Visible;
                            break;
                        }
                }
            }
        }

        private void HideAllparameter()
        {
            DateInput = Visibility.Collapsed;
            MonthYearInput = Visibility.Collapsed;
            StartEndDate = Visibility.Collapsed;
            SessionInput = Visibility.Collapsed;
            AccountNumberInput = Visibility.Collapsed;
            CategoryInput = Visibility.Collapsed;
            StartEndCardInput = Visibility.Collapsed;
            StartEndDateWTime = Visibility.Collapsed;
        }

        public DateTime GetDate()
        {
            DateTime tempResult;
            DateTime.TryParse(RptParameterDataHandler.B3DateData.DateFullwTime, out tempResult);
            return tempResult;
        }
        #endregion
        #region PROPERTIES

      
        public ReportParameterModel RptParameterDataHandler {get;set;}
        public DatePickerVm DatePickerVm {get;set;}
        public DatePickerVm EndDatePickerVm {get;set;}       
        public DatePickerVm StartDatePickerVm {get;set;}
        public Visibility StartEndDateWTime { get; set; }
        public Visibility StartEndCardInput { get; set; }
        public Visibility CategoryInput { get; set; }
        public Visibility AccountNumberInput { get; set; }
        public Visibility SessionInput { get; set; }
        public Visibility StartEndDate { get; set; }
        public Visibility MonthYearInput { get; set; }
        public Visibility DateInput { get; set; }

        private bool m_isSessionEnable;
        public bool IsSessionEnable
        {
            get { return m_isSessionEnable; }
            set
            {
                if (m_isSessionEnable != value)
                {
                    m_isSessionEnable = value;
                    RaisePropertyChanged("IsSessionEnable");
                }
            }
        }

        private bool m_isAccountEnable;
        public bool IsAccountEnable
        {
            get { return m_isAccountEnable; }
            set
            {
                if (m_isAccountEnable != value)
                {
                    m_isAccountEnable = value;
                    RaisePropertyChanged("IsAccountEnable");
                }
            }
        }

        public DateTime SelectedDateTime
        {
            get { return GetDate(); }
        }

       

        public DatePickerM DatePickermModel
        {
            get { return RptParameterDataHandler.B3DateData; }
            set
            {
                RptParameterDataHandler.B3DateData = value;
                RaisePropertyChanged("datePickermModel");
            }
        }

        public Visibility SetVisibility
        {
            get { return m_visibility; }
            set
            {
                m_visibility = value;
                RaisePropertyChanged("setVisibility");
            }
        }

        public ObservableCollection<string> Months
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

     
        public ObservableCollection<Session> SessionList
        {
            get { return m_sessionList; }
            set
            {
                m_sessionList = value;
                RaisePropertyChanged("SessionList");
            }
        }

        public Session SelectedSession
        {
            get
            {
                return RptParameterDataHandler.B3Session;
            }
            set
            {
                RptParameterDataHandler.B3Session = value;
                RaisePropertyChanged("SelectedSession");
            }
        }

        public string StartingCard
        {
            get { return RptParameterDataHandler.B3StartingCard; }
            set
            {
                RptParameterDataHandler.B3StartingCard = value;      
                RaisePropertyChanged("StartingCard");             
            }
        }

        public string EndingCard
        {
            get { return RptParameterDataHandler.B3EndingCard; }
            set
            {
                RptParameterDataHandler.B3EndingCard = value;    
                RaisePropertyChanged("EndingCard");            
            }
        }

        public ObservableCollection<string> AccountList
        {
            get { return m_accountList; }
            set
            {
                m_accountList = value; RaisePropertyChanged("AccountList");
            }
        }

        public string AccountSelected
        {
            get
            {
                return RptParameterDataHandler.B3AccountNumber;
            }
            set
            {
                RptParameterDataHandler.B3AccountNumber = value;
                RaisePropertyChanged("AccountSelected");
            }
        }

        public List<string> CategoryList
        {
            get { return m_categoryList; }
            set
            {
                m_categoryList = value;
                RaisePropertyChanged("CategoryList");
            }
        }

        public string m_categorySelected;
        public string CategorySelected
        {
            get { return m_categorySelected; }
            set
            {
                m_categorySelected = value;
                RaisePropertyChanged("CategorySelected");
            }
        }
     
        private string m_monthSelected;
        public string MonthSelected
        {
            get
            {
                return m_monthSelected;
            }
            set
            {
                m_monthSelected = value;
                RaisePropertyChanged("MonthSelected");
            }
        }

        private List<string> m_years;
        public List<string> Years
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

        private string m_yearSelected;
        public string YearSelected
        {
            get
            {
                return m_yearSelected;
            }
            set
            {

                if (m_yearSelected != value)
                {
                    m_yearSelected = value;
                    RaisePropertyChanged("YearSelected");
                }
            }
        }

        #endregion
    }
}