using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Model;
using System.Windows;
using GameTech.Elite.Reports;
using GameTech.Elite.Client.Modules.B3Center.UI.Shared;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Shared;
using GameTech.Elite.Client.Modules.B3Center.Model.Shared;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{

    public class ReportParameterViewModel : ViewModelBase
    {

        #region MEMBER VARIABLE

        private DatePickerM m_datepickerModel;
        private ObservableCollection<Session> AllSessionList;
        private List<string> m_paramList;
        private ObservableCollection<string> m_accountList;
        private List<string> m_categoryList;
        private ObservableCollection<Session> m_sessionList;
        private Session m_SelectedSession;
        private string m_AccountSelected;
        private string m_endingCard;
        private Visibility m_visibility;
        private ObservableCollection<string> m_months;

        #endregion
        #region STATIC (properties and variable)

        //private static readonly object m_syncRoot = new Object();
        //private static volatile ReportParameterViewModel m_instance;
        //public static ReportParameterViewModel Instance
        //{
        //    get
        //    {
        //        if (m_instance == null)
        //        {
        //            lock (m_syncRoot)
        //            {
        //                if (m_instance == null) { }
        //                m_instance = new ReportParameterViewModel();
        //            }
        //        }
        //        return m_instance;
        //    }
        //}

        #endregion
        #region CONSTRUCTORS

        public ReportParameterViewModel(List<string> paramlist, ReportParameterModel rptParameter)
        {
            m_accountList = new ObservableCollection<string>();
            m_sessionList = new ObservableCollection<Session>();
            m_paramList = paramlist;
            RptParameterDataHandler = rptParameter;
            HideAllparameter();
            HideEnableParamControls(paramlist);
            if (rptParameter.rptid == ReportId.B3AccountHistory)
            {
                EventCommand();
            }
        }


        //internal void Initialize(List<string> paramlist, ReportParameterModel rptParameter)
        //{
        //    m_paramList = paramlist;
        //    RptParameterDataHandler = rptParameter;
        //    HideAllparameter();
        //    HideEnableParamControls(paramlist);
        //    if (rptParameter.rptid == ReportId.B3AccountHistory)
        //    {
        //        EventCommand();
        //    }           
        //}

        #endregion
        #region METHOD

        //This also trigger from codebehind ReportParameter.xaml.cs
        public void EventCommand()
        {
            if (RptParameterDataHandler.rptid == ReportId.B3AccountHistory)
            {
                if (SelectedSession != null)
                {
                    UpdateAccountList();
                }
            }
        }

        //Another validation from code behind.
        //Enable or disable view and print button.
        public void ValidateCard(string CardNumber, bool isStartingCard)
        {
            bool ViewReportVisibility = false;
            string startingCardNow = "";
            string endingCardNow = "";

            if (isStartingCard == true)
            {
                startingCardNow = CardNumber;
                endingCardNow = RptParameterDataHandler.b3EndingCard;
            }
            else
            {
                startingCardNow = RptParameterDataHandler.b3StartingCard;
                endingCardNow = CardNumber;
            }

            int tempStartingCard;
            int tempEndingCard;

            var tempResultsc = int.TryParse(startingCardNow, out tempStartingCard); //Right now user can enter any character on the textbox 
            var tempResultec = int.TryParse(endingCardNow, out tempEndingCard);

            if (tempResultec == true && tempResultsc == true)
            {
                if (tempStartingCard > tempEndingCard)
                {
                    ViewReportVisibility = false;
                }
                else
                {
                    if (tempEndingCard == 0)
                    {
                        ViewReportVisibility = false;
                    }
                    else
                    {
                        ViewReportVisibility = true;
                    }
                }
            }
            else
            {
                ViewReportVisibility = false;
            }

            var ii = ReportsViewModel.Instance;
            ii.ViewReportVisibility = ViewReportVisibility;
        }


        //For all user validation (Enable or disable View and print button in the B3Report)
        public void CheckUserValidation()
        {
            bool ViewReportVisibility = false;

            switch (RptParameterDataHandler.rptid)
            {
                case ReportId.B3AccountHistory://For account
                    {
                        if (AccountSelected != null)
                        {
                            ViewReportVisibility = true;
                        }
                        else
                        {
                            ViewReportVisibility = false;
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
                            ViewReportVisibility = true;
                        }
                        else
                        {
                            ViewReportVisibility = false;
                        }
                        break;
                    }
             
                case ReportId.B3Detail:
                case ReportId.B3BallCallBySession:
                case ReportId.B3Void:
                    {
                        DateTime TempStartDate = DateTime.Parse(StartDatePickerVm.DatepickerModel.DateFullwTime);//No need to check if its a date. It will always be a date
                        DateTime TempEndDate = DateTime.Parse(EndDatePickerVm.DatepickerModel.DateFullwTime);
                        //Loop 28x
                        if (TempStartDate > TempEndDate)
                        {
                            ViewReportVisibility = false;
                        }
                        else
                        {
                            ViewReportVisibility = true;
                        }

                        break;
                    }
                case ReportId.B3Monthly:
                case ReportId.B3Drawer:
                case ReportId.B3Daily:
                case ReportId.B3Accounts:
                    {
                        ViewReportVisibility = true;
                        break;
                    }
            }

               if ( ReportId.B3BingoCardReport != RptParameterDataHandler.rptid)
                    {
                        var ii = ReportsViewModel.Instance;
                        ii.ViewReportVisibility = ViewReportVisibility;
                    }          
        }

        private bool IsShowTime()
        {
            bool result = false;
            if (RptParameterDataHandler.rptid == ReportId.B3Detail || RptParameterDataHandler.rptid == ReportId.B3Void)
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

            foreach (var session in ReportsViewModel.Instance.SessionList)
            {
                var sessionStartDateTime = DateTime.Parse(session.SessionStartTime);
                var sessionEndDateTime = DateTime.Parse(session.SessionEndTime);

                if (sessionStartDateTime == sessionEndDateTime)
                {
                    sessionEndDateTime = DateTime.Today;
                }

                if (sessionStartDateTime.Year == selectedDateTime.Year &&
                sessionStartDateTime.Month == selectedDateTime.Month &&
                (sessionStartDateTime.Day <= selectedDateTime.Day && sessionEndDateTime.Day >= selectedDateTime.Day))
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
            }
            else
            {
                if (RptParameterDataHandler.rptid == ReportId.B3AccountHistory)
                {
                    m_accountList.Clear();
                }
            }
            CheckUserValidation();
        }

        public void UpdateAccountList()
        {
            if (SelectedSession != null)
            {
                m_accountList.Clear();
                if (RptParameterDataHandler.rptid == ReportId.B3AccountHistory)
                {
                    Messages.GetB3AccountNumber msg = new Messages.GetB3AccountNumber(SelectedSession.Number);
                    msg.Send();
                    if (msg.AccountNumberList.Count != 0)
                    {
                        AccountList = msg.AccountNumberList;
                        AccountSelected = m_accountList.FirstOrDefault();
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
                            DatePickerVm = new DatePickerVm(RptParameterDataHandler.b3DateData, false);
                            DateInput = Visibility.Visible;
                            break;
                        }
                    case "MonthYear":
                        {
                            DatePickerVm = new DatePickerVm(RptParameterDataHandler.b3DateData, false);
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
            DateTime.TryParse(RptParameterDataHandler.b3DateData.DateFullwTime, out tempResult);
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

        public DateTime SelectedDateTime
        {
            get { return GetDate(); }
        }


        public DatePickerM datePickermModel
        {
            get { return RptParameterDataHandler.b3DateData; }
            set
            {
                RptParameterDataHandler.b3DateData = value;
                RaisePropertyChanged("datePickermModel");
            }
        }

        public Visibility setVisibility
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
                return RptParameterDataHandler.b3Session;
            }
            set
            {
                RptParameterDataHandler.b3Session = value;
                RaisePropertyChanged("SelectedSession");
            }
        }

        public string StartingCard
        {
            get { return RptParameterDataHandler.b3StartingCard; }
            set
            {
                RptParameterDataHandler.b3StartingCard = value;      
                RaisePropertyChanged("StartingCard");             
            }
        }

        public string EndingCard
        {
            get { return RptParameterDataHandler.b3EndingCard; }
            set
            {
                RptParameterDataHandler.b3EndingCard = value;    
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
                return RptParameterDataHandler.b3AccountNumber;
            }
            set
            {
                RptParameterDataHandler.b3AccountNumber = value;
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

#region REF(old script)
//    class ReportParameterViewModel : ViewModelBase
//    {

//        private ReportParameterModel m_reportParameter = new ReportParameterModel();


//        public ReportParameterViewModel(ReportParameterModel reportparamaterdata)
//        {

//            Months = Enum.GetNames(typeof(Month)).Where(m => m != Month.NotSet.ToString());

//            DateDay = reportparamaterdata.dateDay;
//            DateYear = reportparamaterdata.dateYear;
//            B3Session = reportparamaterdata.b3Session;
//            B3AccountNumber = reportparamaterdata.b3AccountNumber;
//            B3Category = reportparamaterdata.b3Category;
//            B3StartingCard = reportparamaterdata.b3StartingCard;
//            B3EndingCard = reportparamaterdata.b3EndingCard;
//            TimeHrMin = reportparamaterdata.timeHrMin;
//            AMPM = reportparamaterdata.AMPM;


//        }

//        public ReportParameterModel ReportParameter
//        {
//            get { return m_reportParameter; }
//            set { m_reportParameter = value; }
//        }



//        private int m_dateDay;
//        public int DateDay { get; set; }

//        private int m_dateYear;
//        public int DateYear { get; set; }

//        private int m_b3_Session;
//        public int B3Session { get; set; }

//        private int m_b3_AccountNumber;
//        public int B3AccountNumber { get; set; }

//        private string m_b3_Category;
//        public string B3Category { get; set; }

//        private int m_b3StartingCard;
//        public int B3StartingCard { get; set; }

//        private int m_b3EndingCard;
//        public int B3EndingCard { get; set; }

//        private string m_timeHrMin;
//        public string TimeHrMin { get; set; }

//        private string m_AMPM;
//        public string AMPM { get; set; }



//    }
//}
#endregion