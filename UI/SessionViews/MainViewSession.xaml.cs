#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System.Windows;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;

//US4296: B3 Start Session
//US4298: B3 End Session
//US4299: B3 Set Balls

namespace GameTech.Elite.Client.Modules.B3Center.UI.SessionViews
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class MainViewSession
    {
        #region local variables

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewSession"/> class.
        /// </summary>
        public MainViewSession()
        {
            InitializeComponent();
        }

        #endregion

        #region private methods

        private void StartSessionButtonClick(object sender, RoutedEventArgs e)
        {
            var viewModel = SessionViewModel.Instance;

            if (viewModel.Settings.IsMultiOperator)
            {
                viewModel.GetUpdatedOperatorList();

                var operatorWindowDialag = new SetOperatorView { DataContext = viewModel };

                operatorWindowDialag.ShowDialog();
            }
            else
            {
                viewModel.SessionStartCommand.Execute(null);
            }
        }

        private void EndSessionButtonClick(object sender, RoutedEventArgs e)
        {
            var viewModel = SessionViewModel.Instance;
            viewModel.SessionEndCommand.Execute(null);
        }

        private void SetBallsButtonClick(object sender, RoutedEventArgs e)
        {
            var viewModel = SessionViewModel.Instance;
            var setBallsView = new SetBallsView { DataContext = viewModel };
            setBallsView.InitializeSelectedBalls(viewModel.SelectedBalls);
            setBallsView.ShowDialog();
        }

        private void VoidAccountsButtonClick(object sender, RoutedEventArgs e)
        {
            var viewModel = SessionViewModel.Instance;
            viewModel.VoidAccountCommand.Execute(null);
            var voidAccountsDialog = new VoidAccountsView { DataContext = viewModel };
            voidAccountsDialog.ShowDialog();
        }

        #endregion

    }
}
