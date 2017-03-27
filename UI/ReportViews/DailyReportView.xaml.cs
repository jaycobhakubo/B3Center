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
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using GameTech.Elite.UI;
using SAPBusinessObjects.WPF.Viewer;

//US4300: B3 Daily Report

namespace GameTech.Elite.Client.Modules.B3Center.UI.ReportViews
{
    /// <summary>
    /// Interaction logic for DailyReportView.xaml
    /// </summary>
    public partial class DailyReportView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DailyReportView"/> class.
        /// </summary>
        public DailyReportView()
        {
            InitializeComponent();

            ReportViewer.ViewerCore.Zoom(85);
            ReportViewer.ViewerCore.ToggleSidePanel = Constants.SidePanelKind.None;

            NewReportButton.Visibility = Visibility.Hidden;
            ReportViewerBorder.Visibility = Visibility.Hidden;
            SelectDateBorder.Visibility = Visibility.Visible;

        }

        /// <summary>
        /// Handles the Loaded event of the UserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ReportViewer.Owner = Window.GetWindow(this);
        }

        /// <summary>
        /// Handles the Click event of the ViewReportButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ViewReportButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = ReportsViewModel.Instance;
            var dateTime = StartDateTime.GetDateTime();
            //Load Report
            Task.Factory.StartNew(() =>
            {
                try
                {
                    viewModel.IsLoading = true;
                    var report = viewModel.LoadDailyReportDocument(dateTime);

                    if (report == null)
                    {
                        viewModel.IsLoading = false;
                        return;
                    }

                    Dispatcher.Invoke(new Action(() =>
                    {
                        ReportViewer.ViewerCore.ReportSource = report;
                        ReportViewer.Focusable = true;
                        ReportViewer.Focus();
                    }));

                    viewModel.IsLoading = false;
                }
                catch (Exception ex)
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        //error
                        MessageWindow.Show(
                            string.Format(CultureInfo.CurrentCulture,
                                Properties.Resources.ErrorLoadingReport, ex.Message),
                            Properties.Resources.B3CenterName, MessageWindowType.Close);
                    }));
                }
                finally
                {
                    viewModel.IsLoading = false;
                }
            });

            NewReportButton.Visibility = Visibility.Visible;
            ReportViewerBorder.Visibility = Visibility.Visible;
            SelectDateBorder.Visibility = Visibility.Hidden;

        }

        /// <summary>
        /// Handles the Click event of the SelectNewReportButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SelectNewReportButton_Click(object sender, RoutedEventArgs e)
        {
            NewReportButton.Visibility = Visibility.Hidden;
            ReportViewerBorder.Visibility = Visibility.Hidden;
            SelectDateBorder.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Handles the Click event of the PrintReportButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void PrintReportButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = ReportsViewModel.Instance;
            var dateTime = StartDateTime.GetDateTime();

            Task.Factory.StartNew(() =>
            {
                try
                {
                    //update ui with printing message
                    viewModel.IsPrinting = true;

                    //load report
                    var report = viewModel.LoadDailyReportDocument(dateTime);

                    //try to print
                    //if failed to print directly, then let the user select printer manually
                    if (!viewModel.PrintReport(Elite.Reports.ReportId.B3Daily, report))
                    {
                        //display print dialog
                        Dispatcher.Invoke(new Action(() =>
                        {
                            PrintDialog printDialog = new PrintDialog();

                            if (printDialog.ShowDialog() == true)
                            {
                                report.PrintOptions.PrinterName = printDialog.PrintQueue.Name;
                                report.PrintToPrinter(1, true, 0, 0);
                            }
                        }));
                    }

                }
                catch (Exception ex)
                {
                    //display message box on UI thread
                    Dispatcher.Invoke(new Action(() =>
                    {
                        //error
                        MessageWindow.Show(
                            string.Format(CultureInfo.CurrentCulture, Properties.Resources.ErrorLoadingReport,
                                ex.Message), Properties.Resources.B3CenterName, MessageWindowType.Close);
                    }));
                }
                finally
                {
                    viewModel.IsPrinting = false;
                }
            });
        }

    }
}
