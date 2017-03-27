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
        public DatePickerM b3DateData {get;set;}
        public DatePickerM StartDate { get; set; }
        public DatePickerM EndDate { get; set; }
        public DateTime Date_ { get; set; }
        public string dateMonth { get; set; }
        public int dateYear { get; set; }
        public Session b3Session { get; set; }
        public string b3AccountNumber { get; set; }
        public string b3Category { get; set; }
        public string b3StartingCard { get; set; }
        public string b3EndingCard { get; set; }
        public ReportId rptid { get; set; }
    }
}


#region SCRATCH 

//private ObservableCollection<Session> m_sessionList = new ObservableCollection<Session>();
//public ObservableCollection<Session> SessionList 
//{ 
//    get {return m_sessionList;}
//    set
//    {
//        m_sessionList = value;
//    }
//}

//private ObservableCollection<string> m_accountList = new ObservableCollection<string>();
//public ObservableCollection<string> AccountList
//{
//    get { return m_accountList; }
//    set
//    {
//        m_accountList = value;
//    }
//}

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

#endregion