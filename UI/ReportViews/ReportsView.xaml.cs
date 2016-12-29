#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;

namespace GameTech.Elite.Client.Modules.B3Center.UI.ReportViews
{
    /// <summary>
    /// Interaction logic for ReportsView.xaml
    /// </summary>
    public partial class ReportsView
    {
        #region Local Variables

        private readonly GridLength m_originalMenuColumnWidth;
        private readonly GridLength m_collapsedMenuColumnWidth = new GridLength(0);
        private readonly List<ToggleButton> m_menuItems;
        private int m_staffId;
        private int m_machineId;

        //Reports
        private readonly AccountsReportView m_accountsReportView;
        private readonly DailyReportView m_dailyReportView;
        private readonly DetailReportView m_detailReportView;
        private readonly DrawerReportView m_drawerReportView;
        private readonly JackpotReportView m_jackpotReportView;
        private readonly MonthlyReportView m_monthlyReportView;
        private readonly SessionReportView m_sessionReportView;
        private readonly VoidReportView m_voidReportView;
        private readonly SessionSummaryView m_sessionsummaryReportView;
        private readonly AccountHistoryReportView m_accountHistoryReportView;
        private readonly WinnerCardsReportView m_winnerCardsReportView;
        private readonly BallCallReportView m_ballCallReportView;
        private readonly SessionTransactionReportView m_sessionTranReportView;
        private readonly BingoCardReportView m_bingoCardReportView;


        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportsView"/> class. Initializes all Reports.
        /// </summary>
        public ReportsView()
        {
            InitializeComponent();
            m_originalMenuColumnWidth = ReportMenuColumn.Width;
            m_menuItems = new List<ToggleButton>();
            m_staffId = SettingViewModel.Instance.StaffId;
            m_machineId = SettingViewModel.Instance.MachineId;

            m_accountsReportView = new AccountsReportView();
            m_accountsReportView.FullScreenButton.FullScreenEvent += OnFullScreenEvent;
            m_accountsReportView.FullScreenButton.ExitScreenEvent += OnExitScreenEvent;

            m_dailyReportView = new DailyReportView();
            m_dailyReportView.FullScreenButton.FullScreenEvent += OnFullScreenEvent;
            m_dailyReportView.FullScreenButton.ExitScreenEvent += OnExitScreenEvent;

            m_detailReportView = new DetailReportView();
            m_detailReportView.FullScreenButton.FullScreenEvent += OnFullScreenEvent;
            m_detailReportView.FullScreenButton.ExitScreenEvent += OnExitScreenEvent;

            m_drawerReportView = new DrawerReportView();
            m_drawerReportView.FullScreenButton.FullScreenEvent += OnFullScreenEvent;
            m_drawerReportView.FullScreenButton.ExitScreenEvent += OnExitScreenEvent;

            m_jackpotReportView = new JackpotReportView();
            m_jackpotReportView.FullScreenButton.FullScreenEvent += OnFullScreenEvent;
            m_jackpotReportView.FullScreenButton.ExitScreenEvent += OnExitScreenEvent;
            
            m_monthlyReportView = new MonthlyReportView();
            m_monthlyReportView.FullScreenButton.FullScreenEvent += OnFullScreenEvent;
            m_monthlyReportView.FullScreenButton.ExitScreenEvent += OnExitScreenEvent;

            m_sessionReportView = new SessionReportView();
            m_sessionReportView.FullScreenButton.FullScreenEvent += OnFullScreenEvent;
            m_sessionReportView.FullScreenButton.ExitScreenEvent += OnExitScreenEvent;

            m_voidReportView = new VoidReportView();
            m_voidReportView.FullScreenButton.FullScreenEvent += OnFullScreenEvent;
            m_voidReportView.FullScreenButton.ExitScreenEvent += OnExitScreenEvent;

            m_sessionsummaryReportView = new SessionSummaryView();
            m_sessionsummaryReportView.FullScreenButton.FullScreenEvent += OnFullScreenEvent;
            m_sessionsummaryReportView.FullScreenButton.ExitScreenEvent += OnExitScreenEvent;

            m_accountHistoryReportView = new AccountHistoryReportView();
            m_accountHistoryReportView.FullScreenButton.FullScreenEvent += OnFullScreenEvent;
            m_accountHistoryReportView.FullScreenButton.ExitScreenEvent += OnExitScreenEvent;

            m_winnerCardsReportView = new WinnerCardsReportView();
            m_winnerCardsReportView.FullScreenButton.FullScreenEvent += OnFullScreenEvent; ;
            m_winnerCardsReportView.FullScreenButton.ExitScreenEvent += OnExitScreenEvent;

            m_ballCallReportView = new BallCallReportView();
            m_ballCallReportView.FullScreenButton.FullScreenEvent += OnFullScreenEvent; ;
            m_ballCallReportView.FullScreenButton.ExitScreenEvent += OnExitScreenEvent;

            m_sessionTranReportView = new SessionTransactionReportView();
            m_sessionTranReportView.FullScreenButton.FullScreenEvent += OnFullScreenEvent; ;
            m_sessionTranReportView.FullScreenButton.ExitScreenEvent += OnExitScreenEvent;

            m_bingoCardReportView = new BingoCardReportView();
            m_bingoCardReportView.FullScreenButton.FullScreenEvent += OnFullScreenEvent; ;
            m_bingoCardReportView.FullScreenButton.ExitScreenEvent += OnExitScreenEvent;

            m_menuItems = new List<ToggleButton>
            {
                AccountsToggleButton,
                DailyToggleButton,
                DetailsToggleButton,
                DrawerToggleButton,
                JackpotToggleButton,
                MonthlyToggleButton,
                SessionToggleButton,
                VoidToggleButton,
                AccountHistoryTransToggleButton,
                BallCallToggleButton,
                SessioSummaryToggleButton,
                SessionTransToggleButton,
                WinnerCardsTransToggleButton,
                BingoCardToggleButton,
            };
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when [full screen event].
        /// </summary>
        public event EventHandler<EventArgs> FullScreenEvent;

        /// <summary>
        /// Occurs when [exit screen event].
        /// </summary>
        public event EventHandler<EventArgs> ExitScreenEvent;

        #endregion

        #region Private Methods

        /// <summary>
        /// Called when [full screen event].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnFullScreenEvent(object sender, EventArgs eventArgs)
        {
            ReportMenuColumn.Width = m_collapsedMenuColumnWidth;
            var handler = FullScreenEvent;
            if (handler != null)
            {
                handler(sender, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Called when [exit screen event].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnExitScreenEvent(object sender, EventArgs eventArgs)
        {
            ReportMenuColumn.Width = m_originalMenuColumnWidth;
            var handler = ExitScreenEvent;
            if (handler != null)
            {
                handler(sender, EventArgs.Empty);
            }
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
                case "AccountsToggleButton":
                    {
                        view = m_accountsReportView;
                        break;
                    }
                case "DailyToggleButton":
                    {
                        view = m_dailyReportView;
                        break;
                    }
                case "DetailsToggleButton":
                    {
                        view = m_detailReportView;
                        break;
                    }
                case "DrawerToggleButton":
                    {
                        view = m_drawerReportView;
                        break;
                    }
                case "JackpotToggleButton":
                    {
                        view = m_jackpotReportView;
                        break;
                    }
                case "MonthlyToggleButton":
                    {
                        view = m_monthlyReportView;
                        break;
                    }
                case "SessionToggleButton":
                    {
                        view = m_sessionReportView;
                        break;
                    }
                case "VoidToggleButton":
                    {
                        view = m_voidReportView;
                        break;
                    }
                case "SessioSummaryToggleButton":
                    {
                        view = m_sessionsummaryReportView;
                        break;
                    }
                case "AccountHistoryTransToggleButton":
                    {
                        view = m_accountHistoryReportView;
                        break;
                    }
                case "WinnerCardsTransToggleButton":
                    {
                        view = m_winnerCardsReportView;
                        break;
                    }
                case "BallCallToggleButton":
                    {
                        view = m_ballCallReportView;
                        break;
                    }
                case "SessionTransToggleButton":
                    {
                        view = m_sessionTranReportView;
                        break;
                    }
                case "BingoCardToggleButton":
                    {
                        view = m_bingoCardReportView;
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

                ReportsTransitionControl.Content = view;
            }
            else
            {
                ReportsTransitionControl.Content = null;
            }
        }

        #endregion
        
        #region Public Methods

        /// <summary>
        /// Clears the selected Report content.
        /// </summary>
        public void ClearSelected()
        {
            //We have to uncheck any previously checked buttons
            foreach (var menuItem in m_menuItems)
            {
                if (menuItem.IsChecked != null && (bool)menuItem.IsChecked)
                {
                    menuItem.IsChecked = false;
                    ReportsTransitionControl.Content = null;
                }
            }
        }

        #endregion

     


    }
}
