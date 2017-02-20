﻿using GameTech.Elite.Client.Modules.B3Center.ViewModels;
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
            ReportViewer_.ToggleSidePanel = Constants.SidePanelKind.None;
            ReportViewer_.Focusable = true;
            ReportViewer_.Focus();
        }

        public void ViewReport(ReportDocument rptDoc)
        {
            ReportViewer_.Dispatcher.Invoke(new Action(() =>
                {
                    ReportViewer_.ViewerCore.ReportSource = rptDoc;
                }));        
        }
    }
}
//ReportViewer.ViewerCore.Zoom(85);
//ReportViewer.ViewerCore.ToggleSidePanel = Constants.SidePanelKind.None;

//NewReportButton.Visibility = Visibility.Hidden;
//ReportViewerBorder.Visibility = Visibility.Hidden;
//SelectDateBorder.Visibility = Visibility.Visible;