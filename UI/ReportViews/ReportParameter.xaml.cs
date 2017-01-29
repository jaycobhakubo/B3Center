using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameTech.Elite.Client.Modules.B3Center.UI.ReportViews
{
    /// <summary>
    /// Interaction logic for ReportParameter.xaml
    /// </summary>
    public partial class ReportParameter : UserControl
    {
        private int m_endingCard;
        private int m_startingCard;
        private TextBlock ErrorTextBlock = new TextBlock();

        public ReportParameter()
        {
            InitializeComponent();
            DataContext = this;
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

            //var viewModel = ReportsViewModel.Instance;
            //if (viewModel.ValidateUserInputForBingoCardReport(m_startingCard, m_endingCard) == false)
            //{
            //    if (!string.IsNullOrEmpty(txtbxStartingCard.Text) && !string.IsNullOrEmpty(txtbxEndingCard.Text))
            //    {
            //        ErrorTextBlock.Text = Properties.Resources.ErrorEndingCardOccuresBeforeStartingCard;
            //    }
            //}

        }

        
    }
}
