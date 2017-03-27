using System.Windows.Controls;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;

namespace GameTech.Elite.Client.Modules.B3Center.UI.OperatorViews
{
    /// <summary>
    /// Interaction logic for OperatorSettingView.xaml
    /// </summary>
    public partial class OperatorView
    {
        public OperatorView()      
        {
            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// Operators the list selection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void OperatorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OperatorViewModel operatorViewModel = (OperatorViewModel)DataContext;

            if (operatorViewModel.WorkInProgress)
            {
                return;
            }

            var currentOperatorIndex = operatorViewModel.OperatorSelectedIndex;
            operatorViewModel.SelectedItemChangevm(currentOperatorIndex);
        }
    }
}
