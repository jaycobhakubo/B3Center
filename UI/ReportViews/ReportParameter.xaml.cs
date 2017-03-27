using System.Windows.Controls;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;

namespace GameTech.Elite.Client.Modules.B3Center.UI.ReportViews
{
    /// <summary>
    /// Interaction logic for ReportParameter.xaml
    /// </summary>
    public partial class ReportParameter
    {

        public ReportParameter()
        {
            InitializeComponent();
            DataContext = this;
            txtbxStartingCard.GotFocus += delegate { txtbxStartingCard.Select(0, 0); };
        }

        private void SessionCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var reportParameterViewModel = (ReportParameterViewModel)DataContext;

            if (reportParameterViewModel.WorkInProgress == false)//Skip unecessary event
            {
                reportParameterViewModel.SelectedSessionChange();//Only fire on actual selection 
            }
        }

        private void StartingCard_TextChanged(object sender, TextChangedEventArgs e)
        {
            var start = txtbxStartingCard.Text;
            var end = txtbxEndingCard.Text;
            var reportParameterViewModel = (ReportParameterViewModel)DataContext;

            reportParameterViewModel.ValidateCard(start, end);
        }
    
    }
}
