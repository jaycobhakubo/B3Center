#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;

//US4296: B3 Start Session
//US4298: B3 End Session
//US4299: B3 Set Balls

namespace GameTech.Elite.Client.Modules.B3Center.UI.SessionViews
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class SessionView
    {
        #region local variables

        private readonly List<ToggleButton> m_menuItems;
        private readonly StartSessionConfirmView m_startSessionConfirmView;
        private readonly SetOperatorView m_setOperatorView;
        private readonly EndSessionView m_stopSessionView;
        private readonly SetBallsView m_setBallsView;
        private readonly VoidAccountsView m_voidAccountsView;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionView"/> class.
        /// </summary>
        public SessionView()
        {
            InitializeComponent();

            m_setOperatorView = new SetOperatorView();
            m_startSessionConfirmView = new StartSessionConfirmView();
            m_stopSessionView = new EndSessionView();
            m_setBallsView = new SetBallsView();
            m_voidAccountsView = new VoidAccountsView();

            m_setOperatorView.OperatorSelectedEvent += OnOperatorSelectedEvent;
            m_startSessionConfirmView.ShowPreviousPageEvent += OnShowPreviousPageFromConfirmEvent;

            //add all menu buttons 
            m_menuItems = new List<ToggleButton> 
            {
                StartSessionToggleButton, 
                StopSessionToggleButton,
                SetBallsToggleButton,
                VoidAccountsToggleButton
            };
        }

        #endregion

        #region private methods

        /// <summary>
        /// Called when [show previous page from confirm event].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnShowPreviousPageFromConfirmEvent(object sender, EventArgs eventArgs)
        {
            SessionTransitionControl.Content = m_setOperatorView;
        }

        /// <summary>
        /// Called when [operator selected event].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnOperatorSelectedEvent(object sender, EventArgs eventArgs)
        {
            var charity = sender as Button;
            if (charity == null)
            {
                return;
            }

            m_startSessionConfirmView.CharityTextBlock.Text = charity.Content.ToString();
            SessionTransitionControl.Content = m_startSessionConfirmView;
        }

        /// <summary>
        /// Handles the Changed event of the MenuToggleButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuToggleButton_Changed(object sender, RoutedEventArgs e)
        {
            var toggleButton = sender as ToggleButton;

            if (toggleButton == null)
            {
                return;
            }

            UserControl view = null;

            if (toggleButton.IsChecked == false)
            {
                SessionTransitionControl.Content = null;
                return;
            }
                        
            var viewModel = SessionViewModel.Instance;
            viewModel.UpdateStatusMessage();

            switch (toggleButton.Name)
            {
                case "StartSessionToggleButton":
                    {

                        if (viewModel.Settings.IsMultiOperator)//knc
                        {
                            view = m_setOperatorView;
                        
                            if (viewModel.IsOperatorListModify == true)
                            {
                                m_setOperatorView.GetUpdatedOperatorList(); 
                                SessionViewModel.Instance.IsOperatorListModify = false;
                            }
                                m_startSessionConfirmView.EnablePreviousButton(true);  
                        }
                        else
                        {
                            view = m_startSessionConfirmView;
                            m_startSessionConfirmView.EnablePreviousButton(false);
                        }

                        viewModel.UpdateStatusMessage();
                        break;
                    }
                case "StopSessionToggleButton":
                    {
                        view = m_stopSessionView;
                        break;
                    }
                case "SetBallsToggleButton":
                    {
                        view = m_setBallsView;
                        m_setBallsView.InitializeSelectedBalls(viewModel.SelectedBalls);
                        break;
                    }

                case "VoidAccountsToggleButton":
                {
                    view = m_voidAccountsView;
                    break;
                }
            }

            //We have to uncheck any previously checked buttons
            foreach (var menuItem in m_menuItems)
            {
                if (Equals(menuItem, toggleButton))
                {
                    continue;
                }

                if (menuItem.IsChecked != null && (bool)menuItem.IsChecked)
                {
                    menuItem.IsChecked = false;
                }

            }

            SessionTransitionControl.Content = view;
        }

        /// <summary>
        /// Clears the selected content.
        /// </summary>
        public void ClearSelected()
        {
            //We have to uncheck any previously checked buttons
            foreach (var menuItem in m_menuItems)
            {
                if (menuItem.IsChecked != null && (bool)menuItem.IsChecked)
                {
                    menuItem.IsChecked = false;
                    SessionTransitionControl.Content = null;
                }
            }
        }

        #endregion

    }
}
