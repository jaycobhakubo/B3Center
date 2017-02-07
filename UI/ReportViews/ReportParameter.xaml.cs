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
using GameTech.Elite.Client.Modules.B3Center.ViewModels;

namespace GameTech.Elite.Client.Modules.B3Center.UI.ReportViews
{
    /// <summary>
    /// Interaction logic for ReportParameter.xaml
    /// </summary>
    public partial class ReportParameter : UserControl
    {

        public ReportParameter()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void SessionCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ReportParameterVmHereInCodeBehind = (ReportParameterViewModel)DataContext;
            if (ReportParameterVmHereInCodeBehind.WorkInProgress == false)//Skip unecessary event
            {
                ReportParameterVmHereInCodeBehind.EventCommand();//Only fire on actual selection 
            }
        }

        private void keyInput(object sender, System.Windows.Input.KeyEventArgs e)
        {
           //true = no ; false = ok
            bool allow = !true;

            if (e.Key == Key.Space)
            {
                allow = true;
            }
            else
                if (e.Key == Key.Back)
            {
                allow = !true;
            }
            e.Handled = allow;
        }


        private void ValidateCard(object sender, TextCompositionEventArgs e)
        {
            TextBox Items = (TextBox)sender;
            string sCardNumber = Items.Text;
            sCardNumber = sCardNumber.Insert(Items.SelectionStart, e.Text);
            var ii = (ReportParameterViewModel)DataContext;
            ii.ValidateCard(sCardNumber, bool.Parse(Items.Tag.ToString()));
        }

        private void revalidate(object sender, TextChangedEventArgs e)
        {
            //var iie = ReportsViewModel.Instance;

            //bool ViewReportVisibility = false;
            //var ii = (ReportParameterViewModel)DataContext;
            //int tempStartingCard;
            //int tempEndingCard;
            //var x = int.TryParse(ii.StartingCard, out tempStartingCard);
            //var y = int.TryParse(ii.EndingCard, out tempEndingCard);
            //if (x == true && y == true)
            //{
            //    if (tempEndingCard != 0)
            //    {
            //        if (tempStartingCard <= tempEndingCard)
            //        {
            //            ViewReportVisibility = true;
            //        }
            //    }
            //}
            //iie.ViewReportVisibility = ViewReportVisibility;
        }

        private void txtbxStartingCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void txtbxStartingCard_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
