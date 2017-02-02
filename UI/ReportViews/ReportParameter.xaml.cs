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
        private int m_endingCard;
        private int m_startingCard;
        private TextBlock ErrorTextBlock = new TextBlock();

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

        private void ValidateCard(object sender, TextCompositionEventArgs e)
        {
            TextBox Items = (TextBox)sender;
            string sCardNumber = Items.Text;
            sCardNumber = sCardNumber.Insert(Items.SelectionStart, e.Text);
            var ii = (ReportParameterViewModel)DataContext;
            ii.ValidateCard(sCardNumber, bool.Parse(Items.Tag.ToString()));
        }     
    }
}
