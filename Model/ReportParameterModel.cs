using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTech.Elite.Base;

namespace GameTech.Elite.Client.Modules.B3Center.Model
{
    class ReportParameterModel : ViewModelBase
    {
        private IEnumerable<string> m_dateMonth;
        public IEnumerable<string> dateMonth { get; set; }

        private int m_dateDay;
        public int dateDay { get; set; }
        
        private int m_dateYear;
        public int dateYear { get; set; }
        
        private int m_b3_Session;
        public int b3Session { get; set; }
        
        private int m_b3_AccountNumber;
        public int b3AccountNumber { get; set; }
        
        private string m_b3_Category;
        public string b3Category { get; set; }
        
        private int m_b3StartingCard;
        public int b3StartingCard { get; set; }
        
        private int m_b3EndingCard;
        public int b3EndingCard { get; set; }
        
        private string m_timeHrMin;
        public string timeHrMin { get; set; }
        
        private string m_AMPM;
        public string AMPM { get; set; }


    }
}
