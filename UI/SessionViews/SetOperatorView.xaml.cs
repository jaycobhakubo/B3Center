#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System.Windows;

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
        }

        #endregion

        #region private methods

        #endregion

        private void StartButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }

}
