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

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{

    public class ReportParameterViewModel : ViewModelBase
    {
        private ReportParameterModel m_reportParameterModel;
        private DatePickerM m_datepicker;

        private List<string> m_paramList;
        //private List<Visibility> m_ParameterList2;

        public ReportParameterViewModel(List<string> paramlist, ReportParameterModel reportparM)
        {
            //Months = Enum.GetNames(typeof(Month)).Where(m => m != Month.NotSet.ToString());
            m_paramList = paramlist;
            reportParameterModel = reportparM;//new ReportParameterModel();
            m_StartDatedatepickerVm = new DatePickerVm();//Do we want to pass any value? not for now.
            HideAllparameter();
            HideEnableParamControls(paramlist);
           

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



        private DatePickerVm m_StartDatedatepickerVm;
        public DatePickerVm StartDatedatepickerVm
        {
            get { return m_StartDatedatepickerVm; }
            set
            {
                m_StartDatedatepickerVm = value;
                 RaisePropertyChanged("StartDatedatepickerVm");
            }
        }


        private ObservableCollection<Session> m_sessionList;
        public ObservableCollection<Session>  SessionList
        {
            get { return reportParameterModel.SessionList ; }
            set 
            {
                reportParameterModel.SessionList = value;
                RaisePropertyChanged("SessionList");
            }
        }

        private Session m_SelectedSession;
        public Session SelectedSession
        {
            get{return reportParameterModel.b3Session;}
            set{
                reportParameterModel.b3Session = value;
                SelectionChangedNotProper();
                RaisePropertyChanged("SelectedSession");
            }  
        }



        private void SelectionChangedNotProper()
        {
            if (m_reportParameterModel.rptid == ReportId.B3AccountHistory)
            {
                Messages.GetB3AccountNumber msg = new Messages.GetB3AccountNumber(SelectedSession.Number);
                msg.Send();
                var AccountListtemp = msg.AccountNumberList;
                AccountList = AccountListtemp.Select(x => (x.ToString())).ToList();
                AccountSelected = m_accountList.FirstOrDefault();
            }
        }

        private List<string> getAccountListPerSession(int SessionNumber)
        {
            m_accountList = new List<string>();
            if (m_paramList.Contains("AccountNumber"))
            {
           
            }
            return m_accountList;
        }

        private List<string> m_accountList;
        public List<string> AccountList
        {
            get { return m_accountList; }
            set
            {
                m_accountList = value;
                RaisePropertyChanged("AccountList");
            }
        }

        private string m_AccountSelected;
        public string AccountSelected
        {
            get { return reportParameterModel.b3AccountNumber; }
            set
            {
                reportParameterModel.b3AccountNumber = value;
                RaisePropertyChanged("AccountSelected");
            }
        }

        private List<string> m_categoryList;
        public List<string> CategoryList
        {
            get { return m_categoryList; }
            set
            {
                m_categoryList = value;
                RaisePropertyChanged("CategoryList");
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
                            m_StartDatedatepickerVm = new DatePickerVm();
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
                            m_StartDatedatepickerVm = new DatePickerVm();
                            StartEndDate = Visibility.Visible;
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




        private Visibility m_visibility;
        public Visibility setVisibility
        {
            get { return m_visibility; }
            set
            {
                m_visibility = value;
                RaisePropertyChanged("setVisibility");
            }
        }

        public ReportParameterModel reportParameterModel
        {
      
            get { return m_reportParameterModel; }
            set { m_reportParameterModel = value;
            RaisePropertyChanged("reportParameterModel");
            }
        }

        private IEnumerable<string> m_months;

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

        private string m_startingCard;
        public string StartingCard
        {
            get { return m_reportParameterModel.b3StartingCard; }
            set
            {
                m_reportParameterModel.b3StartingCard = value;
                RaisePropertyChanged("StartingCard");
            }
        }

        private string m_endingCard;
        public string EndingCard
        {
            get { return m_reportParameterModel.b3EndingCard; }
            set
            {
                m_reportParameterModel.b3EndingCard = value;
                RaisePropertyChanged("EndingCard");
            }
        }


        public Elite.Reports.ReportId reportid { get; set; }
    }
}


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
