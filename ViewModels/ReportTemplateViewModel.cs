using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Model;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{

    

    class ReportTemplateViewModel :ViewModelBase
    {
        private ReportTemplateModel m_reportTemplateModel = new ReportTemplateModel();

        public ReportTemplateViewModel(ReportTemplateModel rtm)
        {
            ReportTemplateModel = rtm;
            ReportTitle = ReportTemplateModel.ReportTitle;
            
        }

        public ReportTemplateModel ReportTemplateModel
        {
            get { return m_reportTemplateModel; }
            set 
            {
                if (m_reportTemplateModel != value)
                {
                    m_reportTemplateModel = value;
                    RaisePropertyChanged("ReportTemplateModel");
                }
            }
        }

        public string ReportTitle
        {
            get ;
            set;
        }
    }
}
