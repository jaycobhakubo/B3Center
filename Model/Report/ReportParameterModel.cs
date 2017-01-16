using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.UI.Shared;
using GameTech.Elite.Reports;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Shared;
using GameTech.Elite.Client.Modules.B3Center.Model.Shared;

namespace GameTech.Elite.Client.Modules.B3Center.Model
{

    public class ReportParameterModel : ViewModelBase
    {

        //private DatePickerM m_datePickerModel;
        public DatePickerM DatePickerModel
        {
            get;set;
            //get { return m_datePickerModel; }
            //set { m_datePickerModel = value;
            //    RaisePropertyChanged("DatePickerModel");
            //}
        }

        private DateTime m_date;
        public DateTime Date_ { get; set; }

        private IEnumerable<string> m_dateMonth;
        public IEnumerable<string> dateMonth { get; set; }

        private int m_dateDay;
        public int dateDay { get; set; }

        private int m_dateYear;
        public int dateYear { get; set; }

        private Session m_b3_Session;
        public Session b3Session { get; set; }

        private string m_b3_AccountNumber;
        public string b3AccountNumber { get; set; }

        private string m_b3_Category;
        public string b3Category { get; set; }

        private string m_b3StartingCard;
        public string b3StartingCard { get; set; }

        private string m_b3EndingCard;
        public string b3EndingCard { get; set; }

        private string m_timeHrMin;
        public string timeHrMin { get; set; }

        private string m_AMPM;
        public string AMPM { get; set; }

        public ReportId rptid { get; set; }

        private ObservableCollection<Session> m_sessionList = new ObservableCollection<Session>();
        public ObservableCollection<Session> SessionList 
        { 
            get {return m_sessionList;}
            set
            {
                m_sessionList = value;
            }
        }

    }
}




//    class ReportParameterModel : ViewModelBase
//    {
//        private IEnumerable<string> m_dateMonth;
//        public IEnumerable<string> dateMonth { get; set; }

//        private int m_dateDay;
//        public int dateDay { get; set; }

//        private int m_dateYear;
//        public int dateYear { get; set; }

//        private int m_b3_Session;
//        public int b3Session { get; set; }

//        private int m_b3_AccountNumber;
//        public int b3AccountNumber { get; set; }

//        private string m_b3_Category;
//        public string b3Category { get; set; }

//        private int m_b3StartingCard;
//        public int b3StartingCard { get; set; }

//        private int m_b3EndingCard;
//        public int b3EndingCard { get; set; }

//        private string m_timeHrMin;
//        public string timeHrMin { get; set; }

//        private string m_AMPM;
//        public string AMPM { get; set; }


//    }
//}
