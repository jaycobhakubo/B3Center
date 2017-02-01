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
    public partial class ReportTemplateViewModel2 :ViewModelBase
    {
        public ReportTemplateModel2 ReportTemplateModel2_;// { get; set; } 
       
       
        public ReportTemplateViewModel2(ReportTemplateModel2 _ReportTemplateModel2)
        {
            ReportTemplateModel2_ = new ReportTemplateModel2();
            ReportTemplateModel2_ = _ReportTemplateModel2;
            m_parModel = new ReportParameterModel();
            m_parModel = ReportTemplateModel2_.rptParModel;
            //parVm = new ReportParameterViewModel(ReportTemplateModel2_.ReportParameter, m_parModel);
            //parVm.paramlist = ReportTemplateModel2_.ReportParameter;
            //parVm.rptParameter = m_parModel;
            //ReportTemplate_Model  = reportTemplateModel;       
            //reportParameterList = ReportTemplate_Model.ReportParameter;
            parVm = ReportParameterViewModel.Instance;
            parVm.Initialize(ReportTemplateModel2_.ReportParameter, m_parModel);
            //CloseViewReportCommand = new RelayCommand(parameter => CloseViewReport());
        }
        public ICommand CloseViewReportCommand { get; set; }
        //private List<string> reportParameterList;

     

        //private string m_reportTitle;
        public string ReportTitle
        {
            get

            {
                return ReportTemplateModel2_.ReportTitle;
            }

        }

        //private CrystalReportsViewer m_selectedCrystalReportViewer;
        public CrystalReportsViewer vReportViewer
        {
            get;set;
        }
        //    get { return ReportTemplate_Model.CrystalReportViewer; }
        //    set
        //    {
        //        ReportTemplate_Model.CrystalReportViewer = value;
        //        RaisePropertyChanged("vReportViewer");
        //    }
        //}


        private ReportParameterModel m_parModel;
        public ReportParameterViewModel parVm
        {get;set;
            //get { return m_parVm; }
            //set
            //{
            //    m_parVm = value;
            //    RaisePropertyChanged("parVm");
            //}
        }


        //public ReportTemplateModel2 ReportTemplate_Model2
        //{
        //    get { return m_reportTemplateModel2; }
        //    set
        //    {
        //        m_reportTemplateModel2 = value;
        //        RaisePropertyChanged("ReportTemplate_Model");
        //    }
        //}



        //   private Visibility m_reportParameterVisibility;
        //   public Visibility ReportParameterVisible
        //   {
        //       get { return ReportTemplate_Model.DefaultViewerm; }
        //       set
        //       {
        //           ReportTemplateModel testdd = ReportTemplate_Model;
        //           testdd.DefaultViewerm = value;
        //           ReportTemplate_Model = testdd;
        ////           ReportTemplate_Model.DefaultViewerm = value;
        //           RaisePropertyChanged("ReportParameterVisible");
        //       }
        //   }

        //   private Visibility m_ReportViewerVisibility;
        //   public Visibility ReportViewerVisibility
        //   {

        //       get { return ReportTemplate_Model.ReportViewerm; }
        //       set
        //       {
        //           ReportTemplate_Model.ReportViewerm = value;
        //           RaisePropertyChanged("ReportViewerVisibility");
        //       }
        //   }



        //public void CloseViewReport()
        //{
        //    var x = ReportsViewModel.Instance;
        //    x.DefaultViewMode = Visibility.Visible;
        //    x.CRViewMode = Visibility.Collapsed;
        //}


        //private Visibility m_closeCRViewerVis;
        //public Visibility CloseCRViewerVis
        //{
        //    get { return m_closeCRViewerVis; }
        //    set 
        //    {
        //        m_closeCRViewerVis = value;
        //        RaisePropertyChanged("CloseCRViewerVis");
        //    }
        //}     
    }
}
