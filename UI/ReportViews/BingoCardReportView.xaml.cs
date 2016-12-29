using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using GameTech.Elite.UI;
using SAPBusinessObjects.WPF.Viewer;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Linq;

namespace GameTech.Elite.Client.Modules.B3Center.UI.ReportViews
{
    /// <summary>
    /// Interaction logic for BingoCardReportView.xaml
    /// </summary>
    public partial class BingoCardReportView : UserControl
    {
        private int m_startingCard; 
        private int m_endingCard;

        public BingoCardReportView()
        {
            InitializeComponent();
            ReportViewer.ViewerCore.Zoom(85);
            ReportViewer.ViewerCore.ToggleSidePanel = Constants.SidePanelKind.None;

            NewReportButton.Visibility = Visibility.Hidden;
            ReportViewerBorder.Visibility = Visibility.Hidden;
            SelectDateBorder.Visibility = Visibility.Visible;

            m_startingCard = 0;
            m_endingCard = 0;
        }

        //private void ValidateUserInput()
        //{
        //    int startingCardNumber = 0;
        //    int endingCardNumber = 0;
        //}

        /// <summary>
        /// Handles the Loaded event of the UserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ReportViewer.Owner = Window.GetWindow(this);
            if (ReportViewer.ViewerCore.ActiveViewIndex != -1)
            {
                ReportViewer.Focusable = true;
                ReportViewer.Focus();
            }
        }

        /// <summary>
        /// Handles the Click event of the ViewReportButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ViewReportButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = ReportsViewModel.Instance;

           

            //Load Report
            Task.Factory.StartNew(() =>
            {
                viewModel.IsLoading = true;
                try
                {
                    var report = viewModel.LoadBingoCardReportDocument(m_startingCard, m_endingCard);

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
        /// Handles the Click event of the Print Report Button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void PrintReportButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = ReportsViewModel.Instance;

            Task.Factory.StartNew(() =>
            {
                try
                {
                    //update ui with printing message
                    viewModel.IsPrinting = true;

                    //load report
                    var report = viewModel.LoadBingoCardReportDocument(m_startingCard, m_endingCard);

                    //try to print
                    //if failed to print directly, then let the user select printer manually
                    if (!viewModel.PrintReport(Elite.Reports.ReportId.B3Accounts, report))
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


        private void txtbxStartingCard_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(ErrorTextBlock.Text))
            {
                ErrorTextBlock.Text = string.Empty;
            }

            bool notAllow = false;

            if (e.Key == Key.Space)
            {
                notAllow = true;
            }
            else
                if (e.Key == Key.Back)
            {           
                notAllow = false;
            }
            e.Handled = notAllow;
        }


        private void txtbxNumericOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool result = false; //false = ok; true = !ok
            Regex regex = new Regex("[^0-9]+");
            result = regex.IsMatch(e.Text);
            int CardNumber = 0;

            if (result != false)
            {
                //if its not numeric skip the next statement.
            }
            else
            {
                TextBox Items = (TextBox)sender;
                if (Items.Text.Count() == 0)
                {
                    CardNumber = Convert.ToInt32(e.Text);
                    if (CardNumber == 0)
                    {
                        result = true;
                    } 
                }
                else
                    if (Items.Text.Count() == 5)
                {
                    string sCardNumber = Items.Text; 
                    sCardNumber = sCardNumber.Insert(Items.SelectionStart, e.Text);
                    CardNumber = Convert.ToInt32(sCardNumber);
                    if (CardNumber > 250000) // No more than 250000
                    {
                        result = true;
                    }
                }
            }

            e.Handled = result;
        }

        private void txtbxEndingCard_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txtbx = (TextBox)sender;


            if (txtbx.Name == "txtbxEndingCard")
            {
                if (txtbx.Text != string.Empty)
                {
                    m_endingCard = Convert.ToInt32(txtbx.Text);
                }
                else
                {
                    m_endingCard = 0;
                }
                
            }
            else
            {
                if (txtbx.Text != string.Empty)
                {
                    m_startingCard = Convert.ToInt32(txtbx.Text);
                }
                else
                {
                    m_startingCard = 0;
                }                       
            }

            var viewModel = ReportsViewModel.Instance;
            if (viewModel.ValidateUserInputForBingoCardReport(m_startingCard, m_endingCard) == false)
            {
                if (!string.IsNullOrEmpty(txtbxStartingCard.Text) && !string.IsNullOrEmpty(txtbxEndingCard.Text))
                {
                    ErrorTextBlock.Text = Properties.Resources.ErrorEndingCardOccuresBeforeStartingCard;
                }
            }
          
        }
    }
}
