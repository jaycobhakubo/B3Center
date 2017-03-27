using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using CrystalDecisions.CrystalReports.Engine;

namespace GameTech.Elite.Client.Modules.B3Center.UI.ReportViews
{
    /// <summary>
    /// Interaction logic for ReportTemplate.xaml
    /// </summary>
    public partial class ReportTemplate
    {
        public ReportTemplate(ReportTemplateViewModel reportvm)
        {
            InitializeComponent();
            DataContext = reportvm;
        }

        public void ViewReport(ReportDocument rptDoc)
        {
            var reportDocument = new ReportDocument();
            reportDocument.Refresh();
            CrViewer.ViewerCore.ReportSource = reportDocument;
            CrViewer.ViewerCore.ReportSource = rptDoc;
            CrViewer.Focus();
        }
    }
}
