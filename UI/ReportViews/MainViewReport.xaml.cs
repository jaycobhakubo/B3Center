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
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;

namespace GameTech.Elite.Client.Modules.B3Center.UI.ReportViews
{
    /// <summary>
    /// Interaction logic for ReportsView.xaml
    /// </summary>
    public partial class ReportsView
    {
        //#region Local Variables

        private readonly GridLength m_originalMenuColumnWidth;
        private readonly GridLength m_collapsedMenuColumnWidth = new GridLength(0);
        private readonly List<ToggleButton> m_menuItems;
        private int m_staffId;
        private int m_machineId;
       // #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportsView"/> class. Initializes all Reports.
        /// </summary>
        public ReportsView()
        {
            InitializeComponent();
            m_originalMenuColumnWidth = ReportMenuColumn.Width;
            //m_menuItems = new List<ToggleButton>();
            m_staffId = SettingViewModel.Instance.StaffId;
            m_machineId = SettingViewModel.Instance.MachineId;
           
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
            //ReportMenuColumn.Width = m_originalMenuColumnWidth;
            var handler = ExitScreenEvent;
            if (handler != null)
            {
                handler(sender, EventArgs.Empty);
            }
        }



        #endregion


     


    }
}
