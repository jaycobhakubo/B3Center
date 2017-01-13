#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2011 GameTech International, Inc.
#endregion

using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using GameTech.Elite.UI;
using SAPBusinessObjects.WPF.Viewer;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Reports;

//US4317: B3 Void Report

namespace GameTech.Elite.Client.Modules.B3Center.UI.ReportViews
{
    /// <summary>
    /// Interaction logic for VoidReportView.xaml
    /// </summary>
    public partial class VoidReportView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VoidReportView"/> class.
        /// </summary>
        public VoidReportView(ReportBaseVm bcvm)
        {
            InitializeComponent();
            DataContext = bcvm;
            //ReportViewer.ViewerCore.Zoom(85);
            //ReportViewer.ViewerCore.ToggleSidePanel = Constants.SidePanelKind.None;

            //StartDateTime.SetDateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0);

            //StartDateTime.DateTimeChangedEvent += DateTime_DateTimeChangedEvent;
            //EndDateTime.DateTimeChangedEvent += DateTime_DateTimeChangedEvent;

            //SelectDatesButton.Visibility = Visibility.Hidden;
            //ReportViewerBorder.Visibility = Visibility.Hidden;
            //SelectDateBorder.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Handles the Loaded event of the UserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //ReportViewer.Owner = Window.GetWindow(this);
            //if (ReportViewer.ViewerCore.ActiveViewIndex != -1)
            //{
            //    ReportViewer.Focusable = true;
            //    ReportViewer.Focus();
            //}
        }

        ///// <summary>
        ///// Handles the DateTimeChangedEvent event of the DateTime control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        //private void DateTime_DateTimeChangedEvent(object sender, EventArgs e)
        //{
        //    var start = StartDateTime.GetDateTime();
        //    var end = EndDateTime.GetDateTime();

        //    if (DateTime.Compare(start, end) > 0)
        //    {
        //        ErrorTextBlock.Text = Properties.Resources.ErrorEndDateOccuresBeforStartDate;
        //        ViewReportButton.IsEnabled = false;
        //        PrintReportButton.IsEnabled = false;
        //    }
        //    else
        //    {
        //        ErrorTextBlock.Text = string.Empty;
        //        ViewReportButton.IsEnabled = true;
        //        PrintReportButton.IsEnabled = true;
        //    }
        //}

        ///// <summary>
        ///// Handles the Click event of the ViewReportButton control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        //private void ViewReportButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var start = StartDateTime.GetDateTime();
        //    var end = EndDateTime.GetDateTime();

        //    var viewModel = ReportsViewModel.Instance;

        //    //Load Report
        //    Task.Factory.StartNew(() =>
        //    {
        //        try
        //        {
        //            viewModel.IsLoading = true;
        //            var report = viewModel.LoadVoidReportDocument(start, end);

        //            if (report == null)
        //            {
        //                viewModel.IsLoading = false;
        //                return;
        //            }

        //            Dispatcher.Invoke(new Action(() =>
        //            {
        //                ReportViewer.ViewerCore.ReportSource = report;
        //                ReportViewer.Focusable = true;
        //                ReportViewer.Focus();
        //            }));
        //        }
        //        catch (Exception ex)
        //        {
        //            //display message box on UI thread
        //            Dispatcher.Invoke(new Action(() =>
        //            {
        //                //error
        //                MessageWindow.Show(
        //                    string.Format(CultureInfo.CurrentCulture, Properties.Resources.ErrorLoadingReport,
        //                        ex.Message), Properties.Resources.B3CenterName, MessageWindowType.Close);
        //            }));
        //        }
        //        finally
        //        {
        //            viewModel.IsLoading = false;
        //        }
        //    });

        //    SelectDatesButton.Visibility = Visibility.Visible;
        //    ReportViewerBorder.Visibility = Visibility.Visible;
        //    SelectDateBorder.Visibility = Visibility.Hidden;
        //}

        ///// <summary>
        ///// Handles the Click event of the SelectDatesButton control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        //private void SelectDatesButton_Click(object sender, RoutedEventArgs e)
        //{
        //    SelectDatesButton.Visibility = Visibility.Hidden;
        //    ReportViewerBorder.Visibility = Visibility.Hidden;
        //    SelectDateBorder.Visibility = Visibility.Visible;
        //}

        ///// <summary>
        ///// Handles the Click event of the PrintReportButton control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        //private void PrintReportButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var viewModel = ReportsViewModel.Instance;
        //    var start = StartDateTime.GetDateTime();
        //    var end = EndDateTime.GetDateTime();

        //    Task.Factory.StartNew(() =>
        //    {
        //        try
        //        {
        //            //update ui with printing message
        //            viewModel.IsPrinting = true;

        //            //load report
        //            var report = viewModel.LoadVoidReportDocument(start, end);

        //            //try to print
        //            //if failed to print directly, then let the user select printer manually
        //            if (!viewModel.PrintReport(Elite.Reports.ReportId.B3Void, report))
        //            {
        //                //display print dialog
        //                Dispatcher.Invoke(new Action(() =>
        //                {
        //                    PrintDialog printDialog = new PrintDialog();

        //                    if (printDialog.ShowDialog() == true)
        //                    {
        //                        report.PrintOptions.PrinterName = printDialog.PrintQueue.Name;
        //                        report.PrintToPrinter(1, true, 0, 0);
        //                    }
        //                }));
        //            }

        //        }
        //        catch (Exception ex)
        //        {                    
        //            //display message box on UI thread
        //            Dispatcher.Invoke(new Action(() =>
        //            {
        //                //error
        //                MessageWindow.Show(
        //                    string.Format(CultureInfo.CurrentCulture, Properties.Resources.ErrorLoadingReport,
        //                        ex.Message), Properties.Resources.B3CenterName, MessageWindowType.Close);
        //            }));
        //        }
        //        finally
        //        {
        //            viewModel.IsPrinting = false;
        //        }
        //    });
        //}
    }
}
