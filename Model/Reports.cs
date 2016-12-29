using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace GameTech.Elite.Client.Modules.B3Center.Model
{
    public class Reports
    {
        public Reports()
        {

        }


        private string m_reportName;
        public string ReportName
        {
            get { return m_reportName; }
            set { m_reportName = value; }
        }

        private int m_reportID;
        public int ReportID
        {
            get { return m_reportID; }
            set { m_reportID = value; }
        }


        private UserControl m_reportView;
       public UserControl ReportView
        {
            get { return m_reportView; }
            set { m_reportView = value; }
        }

       public string DisplayName
       {
           get { return string.Format("{0}", ReportName); }
       }

    }

    
}
