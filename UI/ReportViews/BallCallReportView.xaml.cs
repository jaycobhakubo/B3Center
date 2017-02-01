#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using GameTech.Elite.UI;
using SAPBusinessObjects.WPF.Viewer;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Reports;

namespace GameTech.Elite.Client.Modules.B3Center.UI.ReportViews
{
    /// <summary>
    /// Interaction logic for BallCallReportView.xaml
    /// </summary>
    public partial class BallCallReportView : UserControl
    {
        private int m_ballcallDefID;

        public BallCallReportView(ReportBaseVm bvm)
        {
            InitializeComponent();
            DataContext = bvm;     
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {          
        }     
    }
}
