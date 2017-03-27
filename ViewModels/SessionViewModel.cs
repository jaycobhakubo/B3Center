#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply: © 2015 GameTech International, Inc.
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using GameTech.Elite.Base;
using GameTech.Elite.UI;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Properties;

//US4296: B3 Start Session
//US4298: B3 End Session
//US4299: B3 Set Balls
//US4155: B3 Void Accounts

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{
    class SessionViewModel : ViewModelBase
    {
        #region Local Variables
        private string m_statusMessage;
        private string m_sessionStartStatusMessage;
        private string m_sessionEndStatusMessage;
        private string m_sessionSetBallStatusMessage;
        private string m_outstandingTicketCountMessage;
        private bool m_startSessionVisibility;
        private bool m_endSessionVisibility;
        private bool m_setBallsVisibility;
        private bool m_setBallsMenuVisibility;
        private bool m_yesNoVoidAccountDialogVisibility;
        private B3Controller m_controller;
        private bool m_isOperatorListModify;

        private static volatile SessionViewModel m_instance;
        private static readonly object m_syncRoot = new Object();
        #endregion

        #region Constructors
        /// <summary>
        /// Represents the view model for managing a session
        /// </summary>
        private SessionViewModel()
        {
            SessionStartCommand = new RelayCommand(parameter => Start());
            SessionEndCommand = new RelayCommand(parameter => End());
            VoidAccountCommand = new RelayCommand(parameter => VoidAccounts());
            VoidAccountYesCommand = new RelayCommand(parameter => VoidAccountsYes());
            VoidAccountNoCommand = new RelayCommand(parameter => VoidAccountsNo());
        }

        #endregion

        #region Member Properties

        public static SessionViewModel Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_syncRoot)
                    {
                        if (m_instance == null)
                            m_instance = new SessionViewModel();
                    }
                }

                return m_instance;
            }
        }


        public bool IsOperatorListModify
        {
            get {return m_isOperatorListModify;}
            set { m_isOperatorListModify = value; }
        }

        public B3CenterSettings Settings
        {
            get
            {
                if (m_controller == null)
                {
                    return null;
                }

                return m_controller.Settings;
            }
        }
        
        public bool IsSuccess
        {
            get;
            private set;
        }
        
        public bool StartSessionDisabledVisibility
        {
            get
            {
                return m_startSessionVisibility;
            }
            set
            {
                m_startSessionVisibility = value;

                //if allow in session ball change is false and session is started then disable set balls
                if (!Settings.AllowInSessBallChange && m_startSessionVisibility)
                {
                    SetBallsDisabledVisibility = true;
                    SessionSetBallStatusMessage = Resources.SessionInSessionSetBallsDisabled;
                }
                else
                {
                    SetBallsDisabledVisibility = false;
                    SessionSetBallStatusMessage = string.Empty;
                }

                RaisePropertyChanged("StartSessionDisabledVisibility");
            }
        }

        public bool EndSessionDisabledVisibility
        {
            get
            {
                return m_endSessionVisibility;
            }
            set
            {
                m_endSessionVisibility = value;
                RaisePropertyChanged("EndSessionDisabledVisibility");
            }
        }

        public bool SetBallsDisabledVisibility
        {
            get
            {
                return m_setBallsVisibility;
            }
            set
            {
                m_setBallsVisibility = value;
                RaisePropertyChanged("SetBallsDisabledVisibility");
            }
        }

        public bool SetBallsMenuVisibility
        {
            get
            {
                return m_setBallsMenuVisibility;
            }
            set
            {
                m_setBallsMenuVisibility = value;
                RaisePropertyChanged("SetBallsMenuVisibility");
            }
        }
        
        public bool YesNoVoidAccountDialogVisibility
        {
            get
            {
                return m_yesNoVoidAccountDialogVisibility;
            }
            set
            {
                m_yesNoVoidAccountDialogVisibility = value;
                RaisePropertyChanged("YesNoVoidAccountDialogVisibility");
            }
        }

        public ObservableCollection<Business.Operator> Operators
        {
            get
            {
                return m_controller.Operators;
            }
        }

        public Business.Operator SelectedOperator
        {
            get;
            set;
        }

        public string StatusMessage
        {
            get
            {
                if (m_controller.IsBusy)
                    return m_controller.ProgressText;

                return m_statusMessage;
            }
            set
            {
                if (m_statusMessage != value)
                {
                    m_statusMessage = value;
                    RaisePropertyChanged("StatusMessage");
                }

            }
        }

        public string SessionStartStatusMessage
        {
            get
            {
                return m_sessionStartStatusMessage;
            }
            set
            {
                if (m_sessionStartStatusMessage != value)
                {
                    m_sessionStartStatusMessage = value;
                    RaisePropertyChanged("SessionStartStatusMessage");
                }
            }
        }

        public string SessionEndStatusMessage
        {
            get
            {
                return m_sessionEndStatusMessage;
            }
            set
            {
                if (m_sessionEndStatusMessage != value)
                {
                    m_sessionEndStatusMessage = value;
                    RaisePropertyChanged("SessionEndStatusMessage");
                }
            }
        }

        public string SessionSetBallStatusMessage
        {
            get { return m_sessionSetBallStatusMessage; }
            set
            {
                if (m_sessionSetBallStatusMessage != value)
                {
                    m_sessionSetBallStatusMessage = value;
                    RaisePropertyChanged("SessionSetBallStatusMessage");
                }
            }
        }

        public List<int> SelectedBalls { get; set; }

        public string OutstandingTicketCountMessage
        {
            get
            {
                return m_outstandingTicketCountMessage;
            }
            set
            {
                if (m_outstandingTicketCountMessage != value)
                {
                    m_outstandingTicketCountMessage = value;
                    RaisePropertyChanged("OutstandingTicketCountMessage");
                }
            }
        }
        #endregion

        #region Member Command Properties

        /// <summary>
        /// Gets or set the command to start a session
        /// </summary>
        public ICommand SessionStartCommand { get; set; }

        /// <summary>
        /// Gets or ses the command to end a session
        /// </summary>
        public ICommand SessionEndCommand { get; set; }

        /// <summary>
        /// Gets or ses the command to end a session
        /// </summary>
        public ICommand VoidAccountCommand { get; set; }

        /// <summary>
        /// Gets or ses the command to end a session
        /// </summary>
        public ICommand VoidAccountYesCommand { get; set; }

        /// <summary>
        /// Gets or ses the command to end a session
        /// </summary>
        public ICommand VoidAccountNoCommand { get; set; }
        #endregion

        #region Member Methods

        public void Initialize(B3Controller controller)
        {
            if (controller == null)
                throw new ArgumentNullException();

            m_controller = controller;

            // Listen for changes to the parent and children.
            PropertyChangedEventManager.AddListener(m_controller, this, string.Empty);
            m_controller.SessionStartCompleted += OnStartDone;
            m_controller.SessionEndCompleted += OnEndDone;
            m_controller.SessionInfoCompleted += OnInfoDone;
            m_controller.SessionOperatorListCompleted += OnOperatorListDone;
            m_controller.GetSessionList();

            SelectedBalls = new List<int>(m_controller.GameBallList);

            UpdateSesion();

            SetBallsDisabledVisibility = Settings.IsCommonRngBallCall;
            SetBallsMenuVisibility = !Settings.IsCommonRngBallCall;
            
            SessionEndStatusMessage = Resources.SessionEnded;
            SessionStartStatusMessage = Resources.SessionStarted;
        }

        /// <summary>
        /// Notifies the controller to start a session.
        /// </summary>
        private void Start()
        {
            SessionStartStatusMessage = Resources.SessionStartProgress;
            StartSessionDisabledVisibility = true;

            //if multioperator, then send selected operator
            //else send default operator
            if (Settings.IsMultiOperator)
            {
                if (SelectedOperator != null)
                {
                    m_controller.SessionStart(SelectedOperator);
                }
            }
            else
            {
                m_controller.SessionStart(new Business.Operator(0, "B3 Session"));
            }
        }

        /// <summary>
        /// Notifies the controller to end a session.
        /// </summary>
        private void End()
        {
            SessionEndStatusMessage = Resources.SessionEndProgress;
            EndSessionDisabledVisibility = true;
            m_controller.SessionEnd();
        }

        private void VoidAccounts()
        {
            GetOutstandingSessionTicketCount();
            YesNoVoidAccountDialogVisibility = true;
        }

        private void VoidAccountsYes()
        {
            m_controller.VoidOutstandingSessionTickets();
            YesNoVoidAccountDialogVisibility = false;
        }

        private void VoidAccountsNo()
        {
            YesNoVoidAccountDialogVisibility = false;
        }

        private void OnStartDone(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                IsSuccess = true;
                ProgressMessage = Resources.SessionStartSuccess;

            }
            else
            {
                IsSuccess = false;
                DisplayMessageBox(string.Format(Resources.SessionStartFailed, e.Error));
            }
        }

        private void OnEndDone(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                IsSuccess = true;
            }
            else
            {
                IsSuccess = false;
                DisplayMessageBox(string.Format(Resources.SessionEndFailed, e.Error));
            }
        }

        private void OnInfoDone(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                IsSuccess = true;
                StatusMessage = Resources.SessionInfoSuccess;
            }
            else
            {
                IsSuccess = false;
                DisplayMessageBox(string.Format(Resources.SessionInfoFailed, e.Error));
            }

            UpdateSesion();
        }

        private void OnOperatorListDone(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                IsSuccess = true;
                StatusMessage = Resources.SessionOperatorListSuccess;
            }
            else
            {
                IsSuccess = false;
                DisplayMessageBox(Resources.SessionOperatorListFailed);
            }
        }

        protected override void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //if (e.PropertyName == "IsBusy")
            //{
            //    IsBusy = Parent.IsBusy;
            //    CommandManager.InvalidateRequerySuggested();
            //}
            //else if (e.PropertyName == "ProgressText")
            //    ProgressMessage = Parent.ProgressText;
        }

        private void DisplayMessageBox(string message)
        {
            MessageBox.Show(string.Format(CultureInfo.CurrentCulture, message));
        }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    PropertyChangedEventManager.RemoveListener(m_controller, this, string.Empty);
                }

                base.Dispose(disposing);
            }
        }

        private void UpdateSesion()
        {
            if (m_controller.Session == null)
            {
                SessionStartStatusMessage = string.Empty;
                StartSessionDisabledVisibility = false;

                SessionEndStatusMessage = Resources.SessionEndSuccess;
                EndSessionDisabledVisibility = true;
                return;
            }

            if (m_controller.Session.Active)
            {
                SessionStartStatusMessage = Resources.SessionStartSuccess;
                StartSessionDisabledVisibility = true;

                SessionEndStatusMessage = string.Empty;
                EndSessionDisabledVisibility = false;
            }
            else
            {
                SessionStartStatusMessage = string.Empty;
                StartSessionDisabledVisibility = false;

                SessionEndStatusMessage = Resources.SessionEndSuccess;
                EndSessionDisabledVisibility = true;
            }

        }

        public void UpdateStatusMessage()
        {
           // GetUpdatedOperatorList();

            if (m_controller.Session == null)
            {
                SessionStartStatusMessage = string.Empty;
                StartSessionDisabledVisibility = false;

                SessionEndStatusMessage = Resources.SessionEnded;
                EndSessionDisabledVisibility = true;
                return;
            }

            if (m_controller.Session.Active)
            {
                SessionStartStatusMessage = Resources.SessionStarted;
                StartSessionDisabledVisibility = true;

                SessionEndStatusMessage = string.Empty;
                EndSessionDisabledVisibility = false;
            }
            else
            {
                SessionStartStatusMessage = string.Empty;
                StartSessionDisabledVisibility = false;

                SessionEndStatusMessage = Resources.SessionEnded;
                EndSessionDisabledVisibility = true;
            }
        }

        public void SetBalls(List<int> balls)
        {
            SetBallsDisabledVisibility = true;
            SessionSetBallStatusMessage = Resources.SessionSetBallsProgress;

            m_controller.SetBalls(balls);
            SelectedBalls = new List<int>(balls);

            SetBallsDisabledVisibility = false;
            SessionSetBallStatusMessage = string.Empty;
        }

        private void GetOutstandingSessionTicketCount()
        {
            var count = m_controller.GetOutstandingSessionTicketCount();

            OutstandingTicketCountMessage = string.Format(Resources.VoidOutstandingAccountsYesNoString, count);
        }

        public void GetUpdatedOperatorList()
        {
            m_controller.SessionOperatorList();
        }

        #endregion
    }
}
