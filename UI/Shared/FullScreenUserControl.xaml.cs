#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System;
using System.Windows;
using System.Windows.Controls;

namespace GameTech.Elite.Client.Modules.B3Center.UI.Shared
{
    /// <summary>
    /// Interaction logic for FullScreenUserControl.xaml
    /// </summary>
    public partial class FullScreenUserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FullScreenUserControl"/> class.
        /// </summary>
        public FullScreenUserControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Occurs when [full screen event].
        /// </summary>
        public event EventHandler<EventArgs> FullScreenEvent;

        /// <summary>
        /// Occurs when [exit screen event].
        /// </summary>
        public event EventHandler<EventArgs> ExitScreenEvent;

        /// <summary>
        /// Handles the Click event of the ScreenSizeButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ScreenSizeButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.ToolTip.ToString() == Properties.Resources.FullScreen)
            {
                var handler = FullScreenEvent;
                if (handler != null)
                {
                    handler(sender, EventArgs.Empty);
                }

                button.Content = FindResource("ExitFullScreen");
                button.ToolTip = Properties.Resources.ExitFullScreen;
            }
            else if (button != null && button.ToolTip.ToString() == Properties.Resources.ExitFullScreen)
            {
                var handler = ExitScreenEvent;
                if (handler != null)
                {
                    handler(sender, EventArgs.Empty);
                }

                button.Content = FindResource("FullScreen");
                button.ToolTip = Properties.Resources.FullScreen;
            }
        }
    }
}
