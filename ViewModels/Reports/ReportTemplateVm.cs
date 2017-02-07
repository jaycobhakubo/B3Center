using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Model;
using SAPBusinessObjects.WPF.Viewer;
using System.ComponentModel;
using GameTech.Elite.UI;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{

    

    public partial class ReportTemplateViewModel :ViewModelBase
    {
        private ReportTemplateModel m_reportTemplateModel;
        public ICommand CloseViewReportCommand { get; set; }
        private List<string> reportParameterList;
       

        //public ReportTemplateViewModel(ReportTemplateModel reportTemplateModel)
        //{
        //    ReportTemplate_Model  = reportTemplateModel;       
        //    reportParameterList = ReportTemplate_Model.ReportParameter;
        //    parVm = ReportParameterViewModel.Instance;
        //    parVm.Initialize(reportParameterList, reportTemplateModel.rptParModel);    
        //    CloseViewReportCommand = new RelayCommand(parameter => CloseViewReport());
        //}


        public ReportTemplateViewModel()
        {
          
        }

        internal void Initialize(ReportTemplateModel reportTemplateModel)
        {
            ReportTemplate_Model = reportTemplateModel;
            reportParameterList = ReportTemplate_Model.ReportParameter;
            parVm = ReportParameterViewModel.Instance;
            parVm.Initialize(reportParameterList, reportTemplateModel.rptParModel);
            CloseViewReportCommand = new RelayCommand(parameter => CloseViewReport());
        }



         private static volatile ReportTemplateViewModel m_instance;
        private static readonly object m_syncRoot = new Object();

        public static ReportTemplateViewModel Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_syncRoot)
                    {
                        if (m_instance == null)
                            m_instance = new ReportTemplateViewModel();
                    }
                }

                return m_instance;
            }
        }

        public ReportTemplateModel ReportTemplate_Model
        {
            get { return m_reportTemplateModel; }
            set
            {
                m_reportTemplateModel = value;
                RaisePropertyChanged("ReportTemplate_Model");
            }
        }

        public string ReportTitle
        {
            get
            {
                return ReportTemplate_Model.ReportTitle;
            }     
        }

        public ReportParameterViewModel parVm
        {
            get;
            set;
        }

        public CrystalReportsViewer vReportViewer
        {
            get { return ReportTemplate_Model.CrystalReportViewer; }
            set
            {
                ReportTemplate_Model.CrystalReportViewer = value;
                RaisePropertyChanged("vReportViewer");
            }
        }

        public Visibility ReportParameterVisible
        {
            get { return ReportTemplate_Model.DefaultViewerm; }//knc
            set
            {
                var testdd = ReportTemplate_Model;
                testdd.DefaultViewerm = value;
                ReportTemplate_Model = testdd;
                RaisePropertyChanged("ReportParameterVisible");
            }
        }

        public Visibility ReportViewerVisibility
        {        
            get { return ReportTemplate_Model.ReportViewerm; }
            set
            {
                ReportTemplate_Model.ReportViewerm = value;
                RaisePropertyChanged("ReportViewerVisibility");
            }
        }

           public void CloseViewReport()//knc
           {
               var x = ReportsViewModel.Instance;
               x.DefaultViewMode = Visibility.Visible;
               x.CRViewMode = Visibility.Collapsed;
           }
    }
}
