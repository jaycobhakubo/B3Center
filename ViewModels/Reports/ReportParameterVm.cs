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
using Microsoft.Practices.Composite.Presentation.Commands;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{

    public class ReportParameterViewModel : ViewModelBase
    {
        #region MEMBER VARIABLE

        private ReportParameterModel m_reportParameterModel;

        private DatePickerM m_datepickerModel;
        private DatePickerVm m_datePickerVm;

        private ObservableCollection<Session> m_sessionList;
        private ObservableCollection<Session> AllSessionList;
        private List<string> m_paramList;
        private ObservableCollection<string> m_accountList;
        private List<string> m_categoryList;

        private Session m_SelectedSession;
        private string m_AccountSelected;
        private string m_startingCard;
        private string m_endingCard;

        private Visibility m_visibility;
        private IEnumerable<string> m_months;

        #endregion

        #region STATIC (properties and variable)

        private static readonly object m_syncRoot = new Object();
        private static volatile ReportParameterViewModel m_instance;

        public static ReportParameterViewModel Instance
        {
       get
            {
                if (m_instance == null)
                {
                    lock (m_syncRoot)
                    {
                        if (m_instance == null) { }
                        m_instance = new ReportParameterViewModel();
                    }
                }

                return m_instance;
            }

          
        }

        #endregion

        #region CONSTRUCTORS

        public ReportParameterViewModel()
        {
    
        }


        internal void Initialize(List<string> paramlist, ReportParameterModel reportparM)
        {
            m_paramList = paramlist;          
            reportParameterModel = reportparM;//new ReportParameterModel();
            m_sessionList = reportParameterModel.SessionList;
            AccountList = reportParameterModel.AccountList;
            DatePickerVm = new DatePickerVm(reportParameterModel.DatePickerModel);///Do we want to pass any value? not for now.
            EventItemChanged();
            GetSessionListofAllSession();
            UpdateSessionList(DateTime.Now);
            UpdateAccountList();
            HideAllparameter();
            HideEnableParamControls(paramlist);
           
        }

        #endregion


        private void HideDateTimePar()
        {
            if (reportParameterModel.rptid == ReportId.B3Detail || reportParameterModel.rptid == ReportId.B3Void)
            {

            }
        }


        #region EVENT (selectionchangedmvvm)

        public ICommand SelectedSessioncmd { get; private set; }
        public ICommand DateSelectedChanged { get; private set; }

        private void EventItemChanged()
        {
            SelectedSessioncmd = new DelegateCommand<Session>(obj =>
            {
                SelectedSession = obj;
                if (SelectedSession != null)
                {
                    UpdateAccountList();
                }
            });

            //DateSelectedChanged = new DelegateCommand<string>(obj =>
            //{
              
            //});
        }

        #endregion

        #region METHOD

        public void GetSessionListofAllSession() //This is bad we should nt get all session available in B3 db. Message should be modify
        {
            var t = ReportsViewModel.Instance;
            AllSessionList =  t.SessionList;
        }



        public void UpdateSessionList(DateTime selectedDateTime)
        {
            //ObservableCollection<Session> result = new ObservableCollection<Session>();
            m_sessionList.Clear();
            AccountList.Clear();

            foreach (var session in AllSessionList)
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
                    m_sessionList.Add(session);
                }

            }

            if (m_sessionList.Count != 0)
            {

                SelectedSession = m_sessionList.LastOrDefault();     
            }
           

            SessionList = m_sessionList;
        }

        private void UpdateAccountList()
        {

            if (m_reportParameterModel.rptid == ReportId.B3AccountHistory)
            {         
                Messages.GetB3AccountNumber msg = new Messages.GetB3AccountNumber(SelectedSession.Number);
                msg.Send();
                var AccountListtemp = msg.AccountNumberList;
                AccountList = AccountListtemp;
                AccountSelected = AccountList.FirstOrDefault();
            }
        }

        private ObservableCollection<string> getAccountListPerSession(int SessionNumber)
        {
            m_accountList = new ObservableCollection<string>();
            if (m_paramList.Contains("AccountNumber"))
            {

            }
            return m_accountList;
        }

        private void HideEnableParamControls(List<string> paramlist)
        {
            foreach (string param in paramlist)
            {
                switch (param)
                {

                    case "Date":
                        {
                            // DatePickerVm= new DatePickerVm();
                            DateInput = Visibility.Visible;
                            break;
                        }
                    case "MonthYear":
                        {
                            MonthYearInput = Visibility.Visible;
                            break;
                        }
                    case "StartEndDate":
                        {
                            //m_StartDatedatepickerVm = new DatePickerVm();
                            //StartEndDate = Visibility.Visible;
                            break;
                        }
                    case "Session":
                        {
                            SessionInput = Visibility.Visible;
                            break;
                        }
                    case "AccountNumber":
                        {
                            AccountNumberInput = Visibility.Visible;
                            break;
                        }
                    case "Category":
                        {
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
            DateTime tempResult;// = new DateTime();
            DateTime.TryParse(reportParameterModel.DatePickerModel.DateFullwTime, out tempResult);
            return tempResult;
        }

        

        #endregion

        #region PROPERTIES


        public DateTime SelectedDateTime
        {
            get { return GetDate(); }
        }     

        public ReportParameterModel reportParameterModel
        {
            //get;set;
            get { return m_reportParameterModel; }
            set
            {
                m_reportParameterModel = value;
                RaisePropertyChanged("reportParameterModel");
            }
        }


        public DatePickerM datePickermModel
        {
            get { return reportParameterModel.DatePickerModel; }
            set
            {
                reportParameterModel.DatePickerModel = value;
                RaisePropertyChanged("datePickermModel");
            }

        }

        public DatePickerVm DatePickerVm
        {
            get { return m_datePickerVm; }
            set
            {
                m_datePickerVm = value;
                RaisePropertyChanged("DatePickerVm");
            }
        }


        public ObservableCollection<Session>  SessionList
        {
            get { return reportParameterModel.SessionList ; }
            set 
            {
                reportParameterModel.SessionList = value;
                RaisePropertyChanged("SessionList");
            }
        }

 
        public Session SelectedSession
        {
            get{return reportParameterModel.b3Session;}
            set{
                reportParameterModel.b3Session = value;
                RaisePropertyChanged("SelectedSession");
            }  
        }

    
        public string StartingCard
        {
            get { return m_reportParameterModel.b3StartingCard; }
            set
            {
                m_reportParameterModel.b3StartingCard = value;
                RaisePropertyChanged("StartingCard");
            }
        }

   
        public string EndingCard
        {
            get { return m_reportParameterModel.b3EndingCard; }
            set
            {
                m_reportParameterModel.b3EndingCard = value;
                RaisePropertyChanged("EndingCard");
            }
        }

        public ObservableCollection<string> AccountList
        {
            get { return m_reportParameterModel.AccountList; }
            set
            {
                m_reportParameterModel.AccountList = value;
                RaisePropertyChanged("AccountList");
            }
        }


        public string AccountSelected
        {
            get { return reportParameterModel.b3AccountNumber; }
            set
            {
                reportParameterModel.b3AccountNumber = value;
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

        public Visibility StartEndDateWTime
        {
            get;
            set;
        }

        public Visibility StartEndCardInput
        {
            get;
            set;
        }

        public Visibility CategoryInput
        {
            get;
            set;
        }


        public Visibility AccountNumberInput
        {
            get;
            set;
        }


        public Visibility SessionInput
        {
            get;
            set;
        }

        public Visibility StartEndDate
        {
            get;
            set;
        }

        public Visibility MonthYearInput
        {
            get;
            set;
        }

        public Visibility DateInput
        {
            get;
            set;
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

       

        public Elite.Reports.ReportId reportid { get; set; }

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


//        private IEnumerable<string> m_months;
//        public IEnumerable<string> Months 
//        {
//            get
//            {
//                return m_months;
//            }
//            set
//            {
//                m_months = value;
//                RaisePropertyChanged("Months");
//            }
//        }

//        private string m_monthSelected;
//        public string MonthSelected
//        {
//            get
//            {
//                return m_monthSelected;
//            }
//            set
//            {
//                m_monthSelected = value;
//                RaisePropertyChanged("MonthSelected");
//            }
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