#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System;
using System.Windows;

//US4296: B3 Start Session

namespace GameTech.Elite.Client.Modules.B3Center.UI.SessionViews
{
    /// <summary>
    /// Interaction logic for StartSessionConfirmView.xaml
    /// </summary>
    public partial class StartSessionConfirmView
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="StartSessionConfirmView"/> class.
        /// </summary>
        public StartSessionConfirmView()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when [show previous page event].
        /// </summary>
        public event EventHandler<EventArgs> ShowPreviousPageEvent;  
        
        #endregion

        #region private methods

        /// <summary>
        /// Handles the Click event of the PreviousButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            var handler = ShowPreviousPageEvent;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles the Click event of the StartButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.Command.Execute(null);
            var handler = ShowPreviousPageEvent;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
        #endregion

        #region public methods

        /// <summary>
        /// Enables the previous button.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        public void EnablePreviousButton(bool enable)
        {
            BackButton.Visibility = enable ? Visibility.Visible : Visibility.Hidden;
        }
        #endregion

    }
}
