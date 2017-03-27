#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2011 GameTech International, Inc.
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.UI.ReportViews;
using GameTech.Elite.Client.Modules.B3Center.UI.SessionViews;
using GameTech.Elite.Client.Modules.B3Center.UI.SettingViews;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;

namespace GameTech.Elite.Client.Modules.B3Center.UI
{
    /// <summary>
    /// The main window of the application.
    /// </summary>
    internal partial class MainWindow
    {
        #region Local Variables

        private readonly GridLength m_originalMenuColumnWidth;
        private readonly  GridLength m_collapsedMenuColumnWidth = new GridLength(0);
        private readonly List<ToggleButton> m_menuItems;
        private readonly SessionView m_sessionView;
        private readonly ReportsView m_reportsView;
        private readonly SettingView m_settingsView;
        private B3Setting B3Setting;
        private  Button m_btnSave;
        private ToggleButton m_tglbtnOperator;
        private ToggleButton m_tglbtnGameSettings;
        private Button m_btnShowMainMenu;
        private Button m_btnShowMainMenu2;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        /// <param name="parent">The IB3CenterController instance that
        /// owns this window.</param>
        /// <param name="mainViewModel"></param>
        /// <param name="useAcceleration">true to use hardware accelerated
        /// rendering; otherwise false.</param>
        /// <exception cref="System.ArgumentNullException">parent is a null
        /// reference.</exception>
        public MainWindow(IB3CenterController parent, MainViewModel mainViewModel, bool useAcceleration)
            : base(useAcceleration)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            InitializeComponent();
            m_originalMenuColumnWidth = MenuColumn.Width;

            m_menuItems = new List<ToggleButton> 
            {
                SessionToggleButton, 
                ReportsToggleButton,
                SettingsToggleButton
            };

            //set datacontext
            DataContext = mainViewModel;

            //initialize views. MainViewModel contains ViewModels for rest of Views
            m_sessionView = new SessionView { DataContext = mainViewModel.SessionVm };
            m_reportsView = new ReportsView { DataContext = mainViewModel.ReportsVm };
            m_settingsView = new SettingView{DataContext = mainViewModel.SettingVm};          
            m_reportsView.FullScreenEvent += ReportsViewOnFullScreenEvent;
            m_reportsView.ExitScreenEvent += ReportsViewOnExitFullScreenEvent;            
            ParentController = parent;         

            m_btnSave = m_settingsView.btnSave;
            m_btnSave.Click += new RoutedEventHandler(m_btnSave_Click);

            m_tglbtnOperator = m_settingsView.OperatorSettingToggleButton;
            m_tglbtnOperator.Checked += new RoutedEventHandler (m_tglbtnOperator_Checked);

            m_tglbtnGameSettings = m_settingsView.GameSettingToggleButton;
            m_tglbtnGameSettings.Checked += new RoutedEventHandler(m_tglbtnOperator_Checked);

            m_btnShowMainMenu = m_settingsView.btnbtnBackOperatorSetting;
            m_btnShowMainMenu.Click += new RoutedEventHandler(m_btnShowMainMenu_Click);

            m_btnShowMainMenu2 = m_settingsView.btnBackOperatorSettingFromGameSetting;
            m_btnShowMainMenu2.Click += new RoutedEventHandler(m_btnShowMainMenu_Click);
            B3Setting = new B3Setting();
        }

         
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets this window's parent.
        /// </summary>
        private IB3CenterController ParentController
        {
            get;
            set;
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Reportses the view on exit full screen event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ReportsViewOnExitFullScreenEvent(object sender, EventArgs eventArgs)
        {
            MenuColumn.Width = m_originalMenuColumnWidth;
        }

        /// <summary>
        /// Reportses the view on full screen event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ReportsViewOnFullScreenEvent(object sender, EventArgs eventArgs)
        {
            MenuColumn.Width = m_collapsedMenuColumnWidth;
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

            switch (toggleButton.Name)
            {


                case "SessionToggleButton":
                    {
                        m_sessionView.ClearSelected();
                        view = m_sessionView;
                        break;
                    }
                case "ReportsToggleButton":
                    {
                        m_reportsView.ClearSelected();
                        view = m_reportsView;
                        break;
                    }
                    case "SettingsToggleButton":
                    {
                        m_settingsView.ClearSelected();
                        view = m_settingsView;
                        break;
                   }

                   
            }
            if (toggleButton.IsChecked == true)
            {
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
                MainWindowTransitionControl.Content = view;
            }
            else
            {
                MainWindowTransitionControl.Content = null;
            }
        }

        /// <summary>
        /// Raises the Closing event. 
        /// </summary>
        /// <param name="e">An CancelEventArgs that contains the event
        /// data.</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            // Notify the controller we are closing.
            e.Cancel = !ParentController.Exit();
        }

        /// <summary>
        /// Handles the Click event of the CloseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        void m_btnSave_Click(object sender, RoutedEventArgs e)
        {           
            ParentController.Settings.B3GameSetting_ = m_settingsView.B3_Setting.B3GameSetting_;
        }

        void m_btnShowMainMenu_Click(object sender, RoutedEventArgs e)
        {  
            MenuColumn.Width = new GridLength(200, GridUnitType.Pixel);
            brdrMenuColumn.Visibility = Visibility.Visible;
        }
       

        private void m_tglbtnOperator_Checked(object sender, RoutedEventArgs e)
        {   
            MenuColumn.Width = GridLength.Auto;
            brdrMenuColumn.Visibility = Visibility.Collapsed;
        }


        #endregion

    }
}
