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
        private ReportTemplateModel m_reportTemplateModel; //= new ReportTemplateModel();
        private ReportParameterModel m_reportParModel;

        public ReportTemplateViewModel(ReportTemplateModel reportTemplateModel)
        {
            ReportTemplate_Vm = reportTemplateModel;
            //ReportTitle = ReportTemplate_Vm.ReportTitle;
            reportParameterList = ReportTemplate_Vm.ReportParameter;
            //ReportParameterVisible = Visibility.Visible;
            //ReportViewerVisibility = ReportTemplate_Vm.ShowCRReportViewer;
           m_parVm = new ReportParameterViewModel(reportParameterList);
           //m_canExecute = true;
           CloseViewReportCommand = new RelayCommand(parameter => CloseViewReport());
        }
        public ICommand CloseViewReportCommand { get; set; }
        private List<string> reportParameterList;

        private void SetReportparameter()
        {

        }

        public ReportTemplateModel ReportTemplate_Vm
        {
            get { return m_reportTemplateModel; }
            set
            {
                m_reportTemplateModel = value;
                RaisePropertyChanged("ReportTemplate_Vm");
            }
        }

        private string m_reportTitle;
        public string ReportTitle
        {
            get { return ReportTemplate_Vm.ReportTitle; }
            set
            {
                if (ReportTemplate_Vm.ReportTitle != value)
                {
                    ReportTemplate_Vm.ReportTitle = value;
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
            get { return ReportTemplate_Vm.CrystalReportViewer; }
            set
            {
                ReportTemplate_Vm.CrystalReportViewer = value;
                RaisePropertyChanged("vReportViewer");
            }
        }

        private Visibility m_reportParameterVisibility;
        public Visibility ReportParameterVisible
        {
            get { return ReportTemplate_Vm.DefaultViewerm; }
            set
            {
                ReportTemplateModel testdd = ReportTemplate_Vm;
                testdd.DefaultViewerm = value;
                ReportTemplate_Vm = testdd;
     //           ReportTemplate_Vm.DefaultViewerm = value;
                RaisePropertyChanged("ReportParameterVisible");
            }
        }

        private Visibility m_ReportViewerVisibility;
        public Visibility ReportViewerVisibility
        {
           
            get { return ReportTemplate_Vm.ReportViewerm; }
            set
            {
                ReportTemplate_Vm.ReportViewerm = value;
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
