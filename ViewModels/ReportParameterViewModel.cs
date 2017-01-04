using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Model;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{
    class ReportParameterViewModel : ViewModelBase
    {

        private ReportParameterModel m_reportParameter = new ReportParameterModel();
 

        public ReportParameterViewModel(ReportParameterModel reportparamaterdata)
        {

            Months = Enum.GetNames(typeof(Month)).Where(m => m != Month.NotSet.ToString());

            DateDay = reportparamaterdata.dateDay;
            DateYear = reportparamaterdata.dateYear;
            B3Session = reportparamaterdata.b3Session;
            B3AccountNumber = reportparamaterdata.b3AccountNumber;
            B3Category = reportparamaterdata.b3Category;
            B3StartingCard = reportparamaterdata.b3StartingCard;
            B3EndingCard = reportparamaterdata.b3EndingCard;
            TimeHrMin = reportparamaterdata.timeHrMin;
            AMPM = reportparamaterdata.AMPM;


        }

        public ReportParameterModel ReportParameter
        {
            get { return m_reportParameter; }
            set { m_reportParameter = value; }
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

        private int m_dateDay;
        public int DateDay { get; set; }

        private int m_dateYear;
        public int DateYear { get; set; }

        private int m_b3_Session;
        public int B3Session { get; set; }

        private int m_b3_AccountNumber;
        public int B3AccountNumber { get; set; }

        private string m_b3_Category;
        public string B3Category { get; set; }

        private int m_b3StartingCard;
        public int B3StartingCard { get; set; }

        private int m_b3EndingCard;
        public int B3EndingCard { get; set; }

        private string m_timeHrMin;
        public string TimeHrMin { get; set; }

        private string m_AMPM;
        public string AMPM { get; set; }

      

    }
}
