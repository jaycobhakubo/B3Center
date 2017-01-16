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
       
       
        public ReportTemplateViewModel(ReportTemplateModel reportTemplateModel)
        {
            ReportTemplate_Model  = reportTemplateModel;       
            reportParameterList = ReportTemplate_Model.ReportParameter;
            parVm = ReportParameterViewModel.Instance;
            parVm.Initialize(reportParameterList, reportTemplateModel.rptParModel);
          //  m_parVm = new ReportParameterViewModel(reportParameterList, reportTemplateModel.rptParModel );       
            CloseViewReportCommand = new RelayCommand(parameter => CloseViewReport());
        }
        public ICommand CloseViewReportCommand { get; set; }
        private List<string> reportParameterList;

        private void SetReportparameter()
        {

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

        private string m_reportTitle;
        public string ReportTitle
        {
            get { return ReportTemplate_Model.ReportTitle; }
            set
            {
                if (ReportTemplate_Model.ReportTitle != value)
                {
                    ReportTemplate_Model.ReportTitle = value;
                    RaisePropertyChanged("ReportTitle");
                }
            }
        }

        private ReportParameterViewModel m_parVm;
        public ReportParameterViewModel parVm
        {
            get { return m_parVm; }
            set
            {
                m_parVm = value;
                RaisePropertyChanged("parVm");
            }
        }

        private CrystalReportsViewer m_selectedCrystalReportViewer;
        public CrystalReportsViewer vReportViewer
        {
            get { return ReportTemplate_Model.CrystalReportViewer; }
            set
            {
                ReportTemplate_Model.CrystalReportViewer = value;
                RaisePropertyChanged("vReportViewer");
            }
        }

        private Visibility m_reportParameterVisibility;
        public Visibility ReportParameterVisible
        {
            get { return ReportTemplate_Model.DefaultViewerm; }
            set
            {
                ReportTemplateModel testdd = ReportTemplate_Model;
                testdd.DefaultViewerm = value;
                ReportTemplate_Model = testdd;
     //           ReportTemplate_Model.DefaultViewerm = value;
                RaisePropertyChanged("ReportParameterVisible");
            }
        }

        private Visibility m_ReportViewerVisibility;
        public Visibility ReportViewerVisibility
        {
           
            get { return ReportTemplate_Model.ReportViewerm; }
            set
            {
                ReportTemplate_Model.ReportViewerm = value;
                RaisePropertyChanged("ReportViewerVisibility");
            }
        }

   

           public void CloseViewReport()
           {
               var x = ReportsViewModel.Instance;
               x.DefaultViewMode = Visibility.Visible;
               x.CRViewMode = Visibility.Collapsed;
           }


           private Visibility m_closeCRViewerVis;
           public Visibility CloseCRViewerVis
           {
               get { return m_closeCRViewerVis; }
               set 
               {
                   m_closeCRViewerVis = value;
                   RaisePropertyChanged("CloseCRViewerVis");
               }
           }     
    }
}
