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
            txtbxStartingCard.GotFocus += delegate { txtbxStartingCard.Select(0, 0); };
        }

        private void SessionCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ReportParameterVmHereInCodeBehind = (ReportParameterViewModel)DataContext;
            if (ReportParameterVmHereInCodeBehind.WorkInProgress == false)//Skip unecessary event
            {
                ReportParameterVmHereInCodeBehind.SelectedSessionChange();//Only fire on actual selection 
            }
        }

        private void txtbxStartingCard_TextChanged(object sender, TextChangedEventArgs e)
        {
            var start = txtbxStartingCard.Text;
            var end = txtbxEndingCard.Text;
            var ii = (ReportParameterViewModel)DataContext;
            ii.ValidateCard(start, end);
        }
    
    }
}
