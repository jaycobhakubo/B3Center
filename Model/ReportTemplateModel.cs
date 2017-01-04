using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTech.Elite.Base;


namespace GameTech.Elite.Client.Modules.B3Center.Model
{
    class ReportTemplateModel : ViewModelBase
    {
        private string m_reportTitle;
        public string ReportTitle
        {
            get { return m_reportTitle; }
            set
            { 
                m_reportTitle = value; 
                RaisePropertyChanged("ReportTitle");
            }
            
        }

    }
}
