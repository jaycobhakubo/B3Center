#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System.Windows.Controls;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;

namespace GameTech.Elite.Client.Modules.B3Center.UI.ReportViews
{
    public partial class ReportsView
    {
        public ReportsView()
        {
            InitializeComponent();        
        }

        private void ReportList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var reportsViewModel = (ReportsViewModel)DataContext;
            reportsViewModel.SelectionChange();
        }
    }
}
