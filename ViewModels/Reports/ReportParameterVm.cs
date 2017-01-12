using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Model;
using System.Windows;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{

    public class ReportParameterViewModel : ViewModelBase
    {
        private ReportParameterModel m_reportParameterModel;
        private List<string> m_paramList;
        //private List<Visibility> m_ParameterList2;

        public ReportParameterViewModel(List<string> paramlist)
        {
            Months = Enum.GetNames(typeof(Month)).Where(m => m != Month.NotSet.ToString());
            m_paramList = paramlist;
            m_reportParameterModel = new ReportParameterModel();
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

        private void HideEnableParamControls(List<string> paramlist)
        {
            foreach (string param in paramlist)
            {
                switch (param)
                {

                    case "Date":
                        {
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
        public int StartingCard
        {
            get { return m_reportParameterModel.b3StartingCard; }
            set
            {
                m_reportParameterModel.b3StartingCard = value;
                RaisePropertyChanged("StartingCard");
            }
        }

        private string m_endingCard;
        public int EndingCard
        {
            get { return m_reportParameterModel.b3EndingCard; }
            set
            {
                m_reportParameterModel.b3EndingCard = value;
                RaisePropertyChanged("EndingCard");
            }
        }

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
