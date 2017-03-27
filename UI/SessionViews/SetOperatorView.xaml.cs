#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;

//US4296: B3 Start Session

namespace GameTech.Elite.Client.Modules.B3Center.UI.SessionViews
{
    /// <summary>
    /// Interaction logic for SetOperatorView.xaml
    /// </summary>
    public partial class SetOperatorView
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SetOperatorView"/> class.
        /// </summary>
        public SetOperatorView()
        {
            InitializeComponent();
            InitializeOperatorButtons();
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when [operator selected event].
        /// </summary>
        public event EventHandler<EventArgs> OperatorSelectedEvent;  

        #endregion

        #region private methods

        /// <summary>
        /// Initializes the operator buttons.
        /// </summary>
        private void InitializeOperatorButtons()
        {
            var viewModel = SessionViewModel.Instance;

            if (viewModel == null)
            {
                return;
            }
            var list = viewModel.Operators.ToList();
            
            list.Sort((x, y) => string.Compare(x.OperatorName, y.OperatorName, StringComparison.CurrentCulture));

            foreach (var charity in list)
            {
                var button = new Button
                {
                    Content = charity.OperatorName,
                    Margin = new Thickness(5),
                    Width = 180
                };

                button.Click += CharityButton_Click;

                CharityWrapPanel.Children.Add(button);
            }
        }


        public void GetUpdatedOperatorList()
        {
            var viewModel = SessionViewModel.Instance;
            viewModel.GetUpdatedOperatorList();

            var list = viewModel.Operators.ToList();

            list.Sort((x, y) => string.Compare(x.OperatorName, y.OperatorName, StringComparison.CurrentCulture));

            CharityWrapPanel.Children.Clear();

            foreach (var charity in list)
            {
                var button = new Button
                {
                    Content = charity.OperatorName,
                    Margin = new Thickness(5),
                    Width = 180
                };

                button.Click += CharityButton_Click;

                CharityWrapPanel.Children.Add(button);
            }
        }

        /// <summary>
        /// Handles the Click event of the CharityButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void CharityButton_Click(object sender, RoutedEventArgs e)
        {
            var charity = sender as Button;
            var viewModel = SessionViewModel.Instance;

            if (charity == null)
            {
                return;
            }

            if (viewModel == null)
            {
                return;
            }

            var handler = OperatorSelectedEvent;
            if (handler != null)
            {
                handler(charity, EventArgs.Empty);
            }

            //set the operator
            viewModel.SelectedOperator = viewModel.Operators.FirstOrDefault(o => o.OperatorName == charity.Content.ToString());
        }
        
        #endregion
    }

}
