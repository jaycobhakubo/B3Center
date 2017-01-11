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

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{

    

    public partial class ReportTemplateViewModel :ViewModelBase
    {
        private ReportTemplateModel m_reportTemplateModel; //= new ReportTemplateModel();
        private ReportParameterModel m_reportParModel;

        public ReportTemplateViewModel(ReportTemplateModel reportTemplateModel)
        {
            ReportTemplate_Vm = reportTemplateModel;
            ReportTitle = ReportTemplate_Vm.ReportTitle;
            reportParameterList = ReportTemplate_Vm.ReportParameter;
            ReportParameterVisible = Visibility.Visible;
            ReportViewerVisibility = Visibility.Hidden;
           m_parVm = new ReportParameterViewModel(reportParameterList);
       
        }

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
            get { return m_reportTitle; }
            set
            {
                if (m_reportTitle != value)
                {
                    m_reportTitle = value;
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
            get { return m_selectedCrystalReportViewer; }
            set
            {
                m_selectedCrystalReportViewer = value;
                RaisePropertyChanged("vReportViewer");
            }
        }

        private Visibility m_reportParameterVisibility;
        public Visibility ReportParameterVisible
        {
            get { return m_reportParameterVisibility; }
            set { m_reportParameterVisibility = value;
            RaisePropertyChanged("ReportParameterVisible");
            }
        }

        private Visibility m_ReportViewerVisibility;
        public Visibility ReportViewerVisibility
        {
            get { return m_ReportViewerVisibility; }
            set
            {
                m_ReportViewerVisibility = value;
                RaisePropertyChanged("ReportViewerVisibility");
            }
        }

        public void ViewReport()
        {

           
        }
    }
}
