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
using CrystalDecisions.ReportAppServer;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Reports;
using GameTech.Elite.UI;
using SAPBusinessObjects.WPF.Viewer;

//US4302: B3 Accounts Report

namespace GameTech.Elite.Client.Modules.B3Center.UI.ReportViews
{
    /// <summary>
    /// Interaction logic for AccountsReportView.xaml
    /// </summary>
    public partial class AccountsReportView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountsReportView"/> class.
        /// </summary>
        public AccountsReportView(ReportBaseVm bcvm)
        {
            InitializeComponent();
            DataContext = bcvm;

            //ReportViewer.ViewerCore.Zoom(85);
            //ReportViewer.ViewerCore.ToggleSidePanel = Constants.SidePanelKind.None;


            //NewReportButton.Visibility = Visibility.Hidden;
            //ReportViewerBorder.Visibility = Visibility.Hidden;
            //SelectDateBorder.Visibility = Visibility.Visible;

        }

        public AccountsReportView()
        {
            InitializeComponent();

            //ReportViewer.ViewerCore.Zoom(85);
            //ReportViewer.ViewerCore.ToggleSidePanel = Constants.SidePanelKind.None;


            //NewReportButton.Visibility = Visibility.Hidden;
            //ReportViewerBorder.Visibility = Visibility.Hidden;
            //SelectDateBorder.Visibility = Visibility.Visible;

        }
        
    //    /// <summary>
    //    /// Handles the Click event of the ViewReportButton control.
    //    /// </summary>
    //    /// <param name="sender">The source of the event.</param>
    //    /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
    //    private void ViewReportButton_Click(object sender, RoutedEventArgs e)
    //    {
    //        var viewModel = ReportsViewModel.Instance;

    //        if (MonthCombobox.SelectedItem == null)
    //        {
    //            return;
    //        }

    //        //Load Report
    //        Task.Factory.StartNew(() =>
    //        {
    //            viewModel.IsLoading = true;
    //            try
    //            {
    //                var report = viewModel.LoadAccountReportDocument();

    //                if (report == null)
    //                {
    //                    viewModel.IsLoading = false;
    //                    return;
    //                }

    //                Dispatcher.Invoke(new Action(() =>
    //                {
    //                    ReportViewer.ViewerCore.ReportSource = report;
    //                    ReportViewer.Focusable = true;
    //                    ReportViewer.Focus();
    //                }));
    //            }
    //            catch (Exception ex)
    //            {
    //                //display message box on UI thread
    //                Dispatcher.Invoke(new Action(() =>
    //                {
    //                    //error
    //                    MessageWindow.Show(
    //                        string.Format(CultureInfo.CurrentCulture, Properties.Resources.ErrorLoadingReport,
    //                            ex.Message), Properties.Resources.B3CenterName, MessageWindowType.Close);
    //                }));
    //            }
    //            finally
    //            {
    //                viewModel.IsLoading = false;
    //            }
    //        });

    //        NewReportButton.Visibility = Visibility.Visible;
    //        ReportViewerBorder.Visibility = Visibility.Visible;
    //        SelectDateBorder.Visibility = Visibility.Hidden;

    //    }

    //    /// <summary>
    //    /// Handles the Loaded event.
    //    /// </summary>
    //    /// <param name="sender">The source of the event.</param>
    //    /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //ReportViewer.Owner = Window.GetWindow(this);
            //if (ReportViewer.ViewerCore.ActiveViewIndex != -1)
            //{
            //    ReportViewer.Focusable = true;
            //    ReportViewer.Focus();
            //}
        }

    //    /// <summary>
    //    /// Handles the Click event of the SelectNewReportButton control.
    //    /// </summary>
    //    /// <param name="sender">The source of the event.</param>
    //    /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
    //    private void SelectNewReportButton_Click(object sender, RoutedEventArgs e)
    //    {
    //        NewReportButton.Visibility = Visibility.Hidden;
    //        ReportViewerBorder.Visibility = Visibility.Hidden;
    //        SelectDateBorder.Visibility = Visibility.Visible;
    //    }

    //    /// <summary>
    //    /// Handles the Click event of the Print Report Button.
    //    /// </summary>
    //    /// <param name="sender">The source of the event.</param>
    //    /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
    //    private void PrintReportButton_Click(object sender, RoutedEventArgs e)
    //    {
    //        var viewModel = ReportsViewModel.Instance;

    //        Task.Factory.StartNew(() =>
    //        {
    //            try
    //            {
    //                //update ui with printing message
    //                viewModel.IsPrinting = true;

    //                //load report
    //                var report = viewModel.LoadAccountReportDocument();

    //                //try to print
    //                //if failed to print directly, then let the user select printer manually
    //                if (!viewModel.PrintReport(Elite.Reports.ReportId.B3Accounts, report))
    //                {
    //                    //display print dialog
    //                    Dispatcher.Invoke(new Action(() =>
    //                    {
    //                        PrintDialog printDialog = new PrintDialog();

    //                        if (printDialog.ShowDialog() == true)
    //                        {
    //                            report.PrintOptions.PrinterName = printDialog.PrintQueue.Name;
    //                            report.PrintToPrinter(1, true, 0, 0);
    //                        }
    //                    }));
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                //display message box on UI thread
    //                Dispatcher.Invoke(new Action(() =>
    //                {
    //                    //error
    //                    MessageWindow.Show(
    //                        string.Format(CultureInfo.CurrentCulture, Properties.Resources.ErrorLoadingReport,
    //                            ex.Message), Properties.Resources.B3CenterName, MessageWindowType.Close);
    //                }));
    //            }
    //            finally
    //            {
    //                viewModel.IsPrinting = false;
    //            }
    //        });
    //    }
    }
}
