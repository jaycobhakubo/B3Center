#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2011 GameTech International, Inc.
#endregion

using System.ComponentModel;
using System.Windows;
using GameTech.Elite.Client.Modules.B3Center.UI;

namespace GameTech.Elite.Client.Modules.B3Center.Business
{
    /// <summary>
    /// Represents a specific user interface in B3Center.
    /// </summary>
    public enum B3CenterView
    {
        Main,
        About
    }

    /// <summary>
    /// The interface representing the application controller.
    /// </summary>
    internal interface IB3CenterController : INotifyPropertyChanged
    {
        #region Member Methods
        /// <summary>
        /// Displays the specified view.
        /// </summary>
        /// <param name="view">The view to display.</param>
        /// <exception cref="System.ArgumentException">view is
        /// invalid.</exception>
        void NavigateTo(B3CenterView view);

        /// <summary>
        /// Closes the current view.
        /// </summary>
        void CloseCurrentView();

        /// <summary>
        /// Displays a window that tells the user the application is closing
        /// because of a server comm. failure.
        /// </summary>
        void ServerCommFailure();

        /// <summary>
        /// Starts the process of exiting the application.
        /// </summary>
        void StartExit();

        /// <summary>
        /// Exits the application (if the program isn't busy).
        /// </summary>
        /// <returns>true if the application is exiting; otherwise
        /// false.</returns>
        bool Exit();
        #endregion

        #region Member Properties
        /// <summary>
        /// Gets whether the controller is performing a long running operation.
        /// </summary>
        bool IsBusy
        {
            get;
        }

        /// <summary>
        /// Gets the application's main window.
        /// </summary>
        Window MainWindow
        {
            get;
        }

        /// <summary>
        /// Gets the current, active view.
        /// </summary>
        Window CurrentView
        {
            get;
        }

        /// <summary>
        /// Gets B3Center's settings.
        /// </summary>
        B3CenterSettings Settings
        {
            get;
        }
        #endregion
    }

    /// <summary>
    /// Represents the B3Center application running at design time.
    /// </summary>
    internal sealed class DesignB3CenterController : IB3CenterController
    {
        #region Events
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the DesignB3CenterController
        /// class.
        /// </summary>
        /// <param name="window"></param>
        public DesignB3CenterController(MainWindow window)
        {
            MainWindow = window;
            CurrentView = MainWindow;
        }
        #endregion

        #region Member Methods
        /// <summary>
        /// Notifies any listeners that a property has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that has
        /// changed.</param>
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if(handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Displays the specified view.
        /// </summary>
        /// <param name="view">The view to display.</param>
        /// <exception cref="System.ArgumentException">view is
        /// invalid.</exception>
        public void NavigateTo(B3CenterView view)
        {
        }

        /// <summary>
        /// Closes the current view.
        /// </summary>
        public void CloseCurrentView()
        {
        }

        /// <summary>
        /// Displays a window that tells the user the application is closing
        /// because of a server comm. failure.
        /// </summary>
        public void ServerCommFailure()
        {
        }

        /// <summary>
        /// Starts the process of exiting the application.
        /// </summary>
        public void StartExit()
        {
            if(MainWindow != null)
                MainWindow.Close();
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        /// <returns>true if the application is exiting; otherwise
        /// false.</returns>
        public bool Exit()
        {
            return true;
        }
        #endregion

        #region Member Properties
        /// <summary>
        /// Gets whether the controller is performing a long running operation.
        /// </summary>
        public bool IsBusy
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the application's main window.
        /// </summary>
        public Window MainWindow
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the current, active view.
        /// </summary>
        public Window CurrentView
        {
            get;
            set;
        }

        /// <summary>
        /// Gets B3Center's settings.
        /// </summary>
        public B3CenterSettings Settings
        {
            get;
            set;
        }
        #endregion
    }
}