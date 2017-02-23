using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using SAPBusinessObjects.WPF.Viewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CrystalDecisions.CrystalReports.Engine;

namespace GameTech.Elite.Client.Modules.B3Center.UI.ReportViews
{
    /// <summary>
    /// Interaction logic for ReportTemplate.xaml
    /// </summary>
    public partial class ReportTemplate : UserControl
    {
        public ReportTemplate(ReportTemplateViewModel reportvm)
        {
            InitializeComponent();
            DataContext = reportvm;
     
            //ReportViewer_ = new CrystalReportsViewer();
            //ReportViewer_.ToggleSidePanel = Constants.SidePanelKind.None;
            //ReportViewer_.Focusable = true;
            //ReportViewer_.Focus();
        }

        public void ViewReport(ReportDocument rptDoc)
        {
            var refresh = new ReportDocument();
            refresh.Refresh();
            CrViewer.ViewerCore.ReportSource = refresh;
            CrViewer.ViewerCore.ReportSource = rptDoc;
            CrViewer.Focus();
            //ReportViewer_.Dispatcher.Invoke(new Action(() =>
            //        {
            //            ReportViewer_.Dispatcher.Thread.Join();
            //            ReportViewer_.Cursor = Cursors.Wait;
            //            ReportViewer_.ViewerCore.ReportSource = rptDoc;
            //            ReportViewer_.ViewerCore.ShowFirstPage();
            //            ReportViewer_.Cursor = Cursors.Arrow;

            //        }));

            //a.Completed += dispatcherOp_Completed;

        }

        //void dispatcherOp_Completed(object sender, EventArgs e)
        //{
        //    var c = (CrystalReportsViewer)sender;
        //    var d = e;
        //    MessageBox.Show("c");

        //}
    }
}
