﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.UI;
using GameTech.Elite.Client.Modules.B3Center.UI.ReportViews;
using GameTech.Elite.Client.Modules.B3Center.UI.SessionViews;
using GameTech.Elite.Client.Modules.B3Center.UI.SettingViews;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;

namespace GameTech.Elite.Client.Modules.B3Center.UI
{
    internal partial class MainWindow
    {
        #region Local Variables

        private readonly GridLength m_originalMenuColumnWidth;
        private readonly GridLength m_collapsedMenuColumnWidth = new GridLength(0);
        private readonly List<ToggleButton> m_menuItems;
        private readonly SessionView m_sessionView;
        private readonly ReportsView m_reportsView;
        private readonly SettingView m_settingsView;
        private B3Setting B3Setting;
        private Button m_btnSave;
        private ToggleButton m_tglbtnOperator;
        private ToggleButton m_tglbtnGameSettings;
        private Button m_btnBackReports;
        private Button m_btnBackSessions;
        private Button m_btnBackSettings;

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
            //m_originalMenuColumnWidth = MenuColumn.Width;

            //m_menuItems = new List<ToggleButton> 
            //{
            //    SessionToggleButton, 
            //    ReportsToggleButton,
            //    SettingsToggleButton
            //};

            //set datacontext
            DataContext = mainViewModel;
            ParentController = parent;

            m_sessionView = new SessionView { DataContext = mainViewModel.SessionVm };
            m_btnBackSessions = m_sessionView.btnBackSessions;
            m_btnBackSessions.Click += new RoutedEventHandler(m_btnBack_Click);

            //m_reportsView = new ReportsView { DataContext = mainViewModel.ReportsVm };
            //m_reportsView.FullScreenEvent += ReportsViewOnFullScreenEvent;
            //m_reportsView.ExitScreenEvent += ReportsViewOnExitFullScreenEvent;

            //m_btnBackReports = m_reportsView.btnBackReports;
            //m_btnBackReports.Click += new RoutedEventHandler(m_btnBack_Click);


            //No need to initialize if staff dont have permission.
            foreach (int moduleFeatureID in mainViewModel.ModuleFeatureList)
            {
                switch (moduleFeatureID)
                {
                    //case 43://Reports
                    //    m_reportsView = new ReportsView { DataContext = mainViewModel.ReportsVm };
                    //    m_reportsView.FullScreenEvent += ReportsViewOnFullScreenEvent;
                    //    m_reportsView.ExitScreenEvent += ReportsViewOnExitFullScreenEvent;

                    //    m_btnBackReports = m_reportsView.btnBackReports;
                    //    m_btnBackReports.Click += new RoutedEventHandler(m_btnBack_Click);
                    //    break;

                    case 44://Settings
                        //m_settingsView = new SettingView { DataContext = mainViewModel.SettingVm };

                        //m_btnSave = m_settingsView.btnSave;
                        //m_btnSave.Click += new RoutedEventHandler(m_btnSave_Click);

                        //m_tglbtnOperator = m_settingsView.OperatorSettingToggleButton;
                        //m_tglbtnOperator.Checked += new RoutedEventHandler(m_tglbtnOperator_Checked);

                        //m_tglbtnGameSettings = m_settingsView.GameSettingToggleButton;
                        //m_tglbtnGameSettings.Checked += new RoutedEventHandler(m_tglbtnOperator_Checked);

                        //m_btnBackSettings = m_settingsView.btnBackSettings;
                        //m_btnBackSettings.Click += new RoutedEventHandler(m_btnBack_Click);

                        break;
                }
            }
            //ReportTransitionControl.Content = m_reportsView;
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
            //MenuColumn.Width = m_originalMenuColumnWidth;
        }

        /// <summary>
        /// Reportses the view on full screen event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ReportsViewOnFullScreenEvent(object sender, EventArgs eventArgs)
        {
            //MenuColumn.Width = m_collapsedMenuColumnWidth;
        }

        /// <summary>
        /// Handles the Changed event of the MenuToggleButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuToggleButton_Changed(object sender, RoutedEventArgs e)
        {
            //var toggleButton = sender as ToggleButton;

            //if (toggleButton == null)
            //{
            //    return;
            //}

            //UserControl view = null;
            //MenuColumn.Width = GridLength.Auto;
            //brdrMenuColumn.Visibility = Visibility.Collapsed;

            //switch (toggleButton.Name)
            //{


            //    case "SessionToggleButton":
            //        {
            //            m_sessionView.ClearSelected();
            //            view = m_sessionView;

            //            break;
            //        }
            //    case "ReportsToggleButton":
            //        {
            //            m_reportsView.ClearSelected();
            //            view = m_reportsView;

            //            //MenuColumn.Width = GridLength.Auto;
            //            //brdrMenuColumn.Visibility = Visibility.Collapsed;

            //            break;
            //        }
            //    case "SettingsToggleButton":
            //        {
            //            m_settingsView.ClearSelected();
            //            view = m_settingsView;
            //            break;
            //        }


            //}
            //if (toggleButton.IsChecked == true)
            //{
            //    //We have to uncheck any previously checked buttons
            //    foreach (var menuItem in m_menuItems)
            //    {
            //        if (Equals(menuItem, toggleButton))
            //        {
            //            continue;
            //        }

            //        if (menuItem.IsChecked != null && (bool)menuItem.IsChecked)
            //        {
            //            menuItem.IsChecked = false;
            //        }
            //    }
            //    MainWindowTransitionControl.Content = view;
            //}
            //else
            //{
            //    MainWindowTransitionControl.Content = null;
            //}
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


        void m_btnBack_Click(object sender, RoutedEventArgs e)
        {
            //MenuColumn.Width = new GridLength(200, GridUnitType.Pixel);
            //brdrMenuColumn.Visibility = Visibility.Visible;

            //foreach (var menuItem in m_menuItems)
            //{
            //    menuItem.IsChecked = false;
            //}

            //MainWindowTransitionControl.Content = null;
        }


        private void m_tglbtnOperator_Checked(object sender, RoutedEventArgs e)
        {
            //MenuColumn.Width = GridLength.Auto;
            //brdrMenuColumn.Visibility = Visibility.Collapsed;
        }


        #endregion

    }
}
