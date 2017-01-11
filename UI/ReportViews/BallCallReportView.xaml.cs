﻿#region Copyright
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

        public BallCallReportView(ballcallVm bvm)
        {

            InitializeComponent();
            DataContext = bvm;
       
            //ReportViewer.ViewerCore.Zoom(85);
            //ReportViewer.ViewerCore.ToggleSidePanel = Constants.SidePanelKind.None;

            //NewReportButton.Visibility = Visibility.Hidden;
            //ReportViewerBorder.Visibility = Visibility.Hidden;
            //SelectDateBorder.Visibility = Visibility.Visible;
    
        }


        //public ReportTemplateViewModel rtvm { get; set; }

        //private void EnableOrDisablePrintOrViewButton()
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

        //private void UpdateBallCallReportSessionList()
        //{
        //    var viewModel = ReportsViewModel.Instance;
        //    var dateTime = StartDateTime.GetDateTime();

        //    if (viewModel.UpdateBallCallReportSessionsByDate(dateTime) == false)
        //    {
        //        if (ViewReportButton.IsEnabled) ViewReportButton.IsEnabled = false;
        //        if (PrintReportButton.IsEnabled)PrintReportButton.IsEnabled = false;
        //    }else
        //    {
        //        if (!ViewReportButton.IsEnabled) ViewReportButton.IsEnabled = true;
        //        if (!PrintReportButton.IsEnabled) PrintReportButton.IsEnabled = true;

        //    }
        //}

        //private void LoadBallCallReportDefList()
        //{
        //    var viewModel = ReportsViewModel.Instance;
        //    viewModel.LoadBallCallReportDefList();
        //}

        ///// <summary>
        ///// Handles the Loaded event of the UserControl control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //ReportViewer.Owner = Window.GetWindow(this);
            //if (ReportViewer.ViewerCore.ActiveViewIndex != -1)
            //{
            //    ReportViewer.Focusable = true;
            //    ReportViewer.Focus();
            //}
            //UpdateBallCallReportSessionList();
        }

        ///// <summary>
        ///// Handles the Click event of the ViewReportButton control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        //private void ViewReportButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var viewModel = ReportsViewModel.Instance;
        //    var dateTime = StartDateTime.GetDateTime();
        //    var endDateTime = EndDateTime.GetDateTime();

        //    if (SessionCombobox.SelectedItem == null && m_ballcallDefID == 1)
        //    {
        //        return;
        //    }

        //    //Load Report
        //    Task.Factory.StartNew(() =>
        //    {
        //        viewModel.IsLoading = true;
        //        try
        //        {
        //            var report = viewModel.LoadBallCallReportDocument(dateTime, endDateTime, m_ballcallDefID);

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

        //    NewReportButton.Visibility = Visibility.Visible;
        //    ReportViewerBorder.Visibility = Visibility.Visible;
        //    SelectDateBorder.Visibility = Visibility.Hidden;

        //}

        ///// <summary>
        ///// Handles the Click event of the SelectNewReportButton control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        //private void SelectNewReportButton_Click(object sender, RoutedEventArgs e)
        //{
        //    NewReportButton.Visibility = Visibility.Hidden;
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
        //    var dateTime = StartDateTime.GetDateTime();
        //     var endDateTime = EndDateTime.GetDateTime();

        //    Task.Factory.StartNew(() =>
        //    {
        //        try
        //        {
        //            //update ui with printing message
        //            viewModel.IsPrinting = true;

        //            //load report 

        //            var report = viewModel.LoadBallCallReportDocument(dateTime, endDateTime, m_ballcallDefID);

        //            //try to print
        //            //if failed to print directly, then let the user select printer manually
        //            if (!viewModel.PrintReport(Elite.Reports.ReportId.B3Session, report))
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

        //private void DateTime_ChangedEvent(object sender, EventArgs e)
        //{
        //    if (m_ballcallDefID == 1)
        //    {
        //        UpdateBallCallReportSessionList();
        //    } 
        //    else if (m_ballcallDefID == 0)
        //    {
        //        EnableOrDisablePrintOrViewButton();
        //    }
        //}

        //private void BallCallDefCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ComboBox cmbx = (ComboBox)sender;
        //    if (cmbx.SelectedIndex == 1)
        //    {
        //        m_ballcallDefID = 1;
        //        SessionCombobox.Visibility = Visibility.Visible;
        //        lblBallCallSession.Visibility = Visibility.Visible;
        //        lblBallCallCategory.Visibility = Visibility.Visible;
        //        EndDateTime.Visibility = Visibility.Collapsed;
        //        txtblkStartDate.Text = "Date";
        //        txtblkEndDate.Visibility = Visibility.Collapsed;
        //        UpdateBallCallReportSessionList();
        //    }
        //    else if (cmbx.SelectedIndex == 0)
        //    {
        //        m_ballcallDefID = 0;
        //        SessionCombobox.Visibility = Visibility.Collapsed;
        //        EndDateTime.Visibility = Visibility.Visible;
        //        txtblkStartDate.Text = "Start Date";
        //        txtblkEndDate.Visibility = Visibility.Visible;
        //        lblBallCallSession.Visibility = Visibility.Collapsed;
        //        EnableOrDisablePrintOrViewButton();
        //    }
        //}
    }
}
