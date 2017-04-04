using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.UI.ReportViews;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;

namespace GameTech.Elite.Client.Modules.B3Center.Model.Report
{
    public class ReportMain
    {
       public B3Report B3Reports { get; set; }
        public string ReportDisplayName { get; set; }
        public ReportTemplateViewModel RptTemplateVm { get; set; }
        public ReportTemplate RptView { get; set; }
    }
}
