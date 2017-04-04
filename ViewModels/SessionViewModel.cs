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
using System.Collections;
using System.Linq;
using System.Timers;

//US4296: B3 Start Session
//US4298: B3 End Session
//US4299: B3 Set Balls
//US4155: B3 Void Accounts

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{
    class SessionViewModel : ViewModelBase
    {
        #region Local Variables

        private bool m_isSuccess;
        private bool m_hasError;
        private string m_statusMessage;
        private string m_sessionStatusMessage;
        private string m_sessionSetBallStatusMessage;
        private string m_outstandingTicketCountMessage;
        private bool m_setBallsButtonVisibility;
        private bool m_voidAccountButtonIsEnabled;
        private bool m_voidAccountYesNoButtonIsEnabled;
        private bool m_isUserPermissionBallCallSet;
        private bool m_hasStartSessionFeaturePermission;
        private bool m_hasEndSessionFeaturePermission;
        private bool m_startSessionButtonVisibility;
        private bool m_endSessionButtonVisibility;
        private ObservableCollection<Session> m_sessionList;
        private ObservableCollection<Business.Operator> m_operators;
        private bool m_setBallsIsEnabled;
        private B3Controller m_controller;
        private static volatile SessionViewModel m_instance;
        private static readonly object m_syncRoot = new object();
        private readonly Timer m_statusMessageTimer;
        #endregion

        #region Constructors
        /// <summary>
        /// Represents the view model for managing a session
        /// </summary>
        private SessionViewModel()
        {
            //status message timer
            m_statusMessageTimer = new Timer(5000);
            m_statusMessageTimer.Elapsed += StatusTimerElapsedHandler;
            m_statusMessageTimer.Stop();
            m_statusMessageTimer.AutoReset = false;

            SessionStartCommand = new RelayCommand(parameter => StartSession());
            SessionEndCommand = new RelayCommand(parameter => EndSession());
            VoidAccountCommand = new RelayCommand(parameter => VoidAccounts());
            VoidAccountYesCommand = new RelayCommand(parameter => VoidAccountsYes());
            SessionList = new ObservableCollection<Session>();
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
            get
            {
                return m_isSuccess;
            }
            set
            {
                m_isSuccess = value;
                RaisePropertyChanged("IsSuccess");
            }
        }

        public bool HasError
        {
            get
            {
                return m_hasError;
            }
            set
            {
                m_hasError = value;
                RaisePropertyChanged("HasError");
            }
        }

        public bool StartSessionButtonVisibility
        {
            get
            {
                return m_startSessionButtonVisibility;
            }
            set
            {
                m_startSessionButtonVisibility = value && m_hasStartSessionFeaturePermission;
                RaisePropertyChanged("StartSessionButtonVisibility");
            }
        }

        public bool EndSessionButtonVisibility
        {
            get
            {
                return m_endSessionButtonVisibility;
            }
            set
            {
                m_endSessionButtonVisibility = value && m_hasEndSessionFeaturePermission;
                RaisePropertyChanged("EndSessionButtonVisibility");
            }
        }

        public bool SetBallsButtonVisibility
        {
            get { return m_setBallsButtonVisibility; }
            set
            {
                m_setBallsButtonVisibility = value;
                RaisePropertyChanged("SetBallsButtonVisibility");
            }
        }

        public bool SetBallsIsEnabled
        {
            get
            {
                return m_setBallsIsEnabled;
            }
            set
            {
                m_setBallsIsEnabled = value;
                RaisePropertyChanged("SetBallsIsEnabled");
            }
        }

        public bool VoidAccountButtonIsEnabled
        {
            get { return m_voidAccountButtonIsEnabled; }
            set
            {
                m_voidAccountButtonIsEnabled = value;
                RaisePropertyChanged("VoidAccountButtonIsEnabled");
            }
        }

        public bool VoidAccountYesNoButtonIsEnabled
        {
            get { return m_voidAccountYesNoButtonIsEnabled; }
            set
            {
                m_voidAccountYesNoButtonIsEnabled = value;
                RaisePropertyChanged("VoidAccountYesNoButtonIsEnabled");
            }
        }

        public ObservableCollection<Business.Operator> Operators
        {
            get
            {
                return m_operators;
            }
            set
            {
                {
                    m_operators = value;
                    RaisePropertyChanged("Operators");
                }
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

        public string SessionStatusMessage
        {
            get
            {
                return m_sessionStatusMessage;
            }
            set
            {
                if (m_sessionStatusMessage != value)
                {
                    m_sessionStatusMessage = value;
                    RaisePropertyChanged("SessionStatusMessage");
                }
            }
        }

        //public string SessionStartStatusMessage
        //{
        //    get
        //    {
        //        return m_sessionStatusMessage;
        //    }
        //    set
        //    {
        //        if (m_sessionStatusMessage != value)
        //        {
        //            m_sessionStatusMessage = value;
        //            RaisePropertyChanged("SessionStartStatusMessage");
        //        }
        //    }
        //}

        //public string SessionEndStatusMessage
        //{
        //    get
        //    {
        //        return m_sessionEndStatusMessage;
        //    }
        //    set
        //    {
        //        if (m_sessionEndStatusMessage != value)
        //        {
        //            m_sessionEndStatusMessage = value;
        //            RaisePropertyChanged("SessionEndStatusMessage");
        //        }
        //    }
        //}

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

        public ObservableCollection<Session> SessionList
        {
            get { return m_sessionList; }
            set
            {
                m_sessionList = value;
                RaisePropertyChanged("SessionList");
            }
        }

        public Session CurrentSession
        {
            get
            {
                return m_controller.Session;
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
        #endregion

        #region Member Methods

        public void Initialize(B3Controller controller)
        {
            if (controller == null)
                throw new ArgumentNullException();

            m_controller = controller;
            PropertyChangedEventManager.AddListener(m_controller, this, string.Empty);        // Listen for changes to the parent and children.
            m_controller.SessionStartCompleted += OnStartDone;
            m_controller.SessionEndCompleted += OnEndDone;
            m_controller.SessionInfoCompleted += OnInfoDone;
            m_controller.SessionOperatorListCompleted += OnOperatorListDone;
            m_controller.GetSessionList();
            SelectedBalls = new List<int>(m_controller.GameBallList);
            DisableB3Features();
            EnableB3Features(controller.ModuleFeatureList);

            if (m_controller.Session != null && m_controller.Session.Active)
            {
                SessionStatusMessage = Resources.SessionStarted;
            }
            else
            {
                SessionStatusMessage = Resources.SessionEnded;
            }

        }

        private void DisableB3Features()
        {
            StartSessionButtonVisibility = false;
            EndSessionButtonVisibility = false;
            SetBallsButtonVisibility = false;
        }

        private void EnableB3Features(IEnumerable featureIdL)
        {
            m_isUserPermissionBallCallSet = false;

            if (featureIdL != null)
            {
                foreach (int featureId in featureIdL)
                {
                    if (featureId == 40)
                    {
                        m_hasStartSessionFeaturePermission = true;
                    }
                    else
                        if (featureId == 41)
                        {
                            m_hasEndSessionFeaturePermission = true;
                        }
                        else
                            if (featureId == 42)
                            {
                                m_isUserPermissionBallCallSet = true;

                                if (Settings.IsCommonRngBallCall == false)
                                {
                                    SetBallsButtonVisibility = true;
                                }
                            }
                }
            }
        }

        /// <summary>
        /// Notifies the controller to start a session.
        /// </summary>
        private void StartSession()
        {
            SessionStatusMessage = Resources.SessionStartProgress;

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
        private void EndSession()
        {
            SessionStatusMessage = Resources.SessionEndProgress;
            m_controller.SessionEnd();
        }

        private void VoidAccounts()
        {
            GetOutstandingSessionTicketCount();
        }

        private void VoidAccountsYes()
        {
            m_controller.VoidOutstandingSessionTickets();
        }

        private void OnStartDone(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                SetIconStatus(true);
                SessionStatusMessage = Resources.SessionStartSuccess;
            }
            else
            {
                SetIconStatus(false);
                SessionStatusMessage = Resources.SessionStartFailed;
                DisplayMessageBox(string.Format(Resources.SessionStartFailed, e.Error));
            }
        }

        private void OnEndDone(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                SetIconStatus(true);
                SessionStatusMessage = Resources.SessionEndSuccess;
            }
            else
            {
                SetIconStatus(false);
                SessionStatusMessage = Resources.SessionEndFailed;
                DisplayMessageBox(string.Format(Resources.SessionEndFailed, e.Error));
            }
        }

        private void OnInfoDone(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                SetIconStatus(true);
                StatusMessage = Resources.SessionInfoSuccess;
                UpdateSesionListUi();
                UpdateSessionButtons();
            }
            else
            {
                SetIconStatus(false);
                SessionStatusMessage = Resources.SessionInfoFailed;
                DisplayMessageBox(string.Format(Resources.SessionInfoFailed, e.Error));
            }
        }

        private void OnOperatorListDone(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Operators = m_controller.Operators;
                StatusMessage = Resources.SessionOperatorListSuccess;
            }
            else
            {
                SetIconStatus(false);
                SessionStatusMessage = Resources.SessionOperatorListFailed;
                DisplayMessageBox(Resources.SessionOperatorListFailed);
            }
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

        private void UpdateSesionListUi()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                SessionList.Clear();
                foreach (var session in m_controller.Sessions.OrderByDescending(s => s.Number))
                {
                    SessionList.Add(session);
                }
            }));
        }

        private void UpdateSessionButtons()
        {
            if (m_controller.Session != null && m_controller.Session.Active)
            {
                StartSessionButtonVisibility = false;
                EndSessionButtonVisibility = true;
                VoidAccountButtonIsEnabled = false;

                //if setting is set, then enable set ball button even though in a session
                SetBallsIsEnabled = m_controller.Settings.AllowInSessBallChange;

            }
            else
            {
                StartSessionButtonVisibility = true;
                EndSessionButtonVisibility = false;
                SetBallsIsEnabled = true;
                VoidAccountButtonIsEnabled = true;
            }
        }

        public void UpdateIsBallCallPermission(bool result)
        {
            if (result == false && m_isUserPermissionBallCallSet)
            {
                SetBallsButtonVisibility = true;
            }
            else
            {
                SetBallsButtonVisibility = false;
            }
        }

        public void SetBalls(List<int> balls)
        {
            m_controller.SetBalls(balls);
            SelectedBalls = new List<int>(balls);
        }

        private void GetOutstandingSessionTicketCount()
        {
            var count = m_controller.GetOutstandingSessionTicketCount();

            if (count == 0)
            {
                VoidAccountYesNoButtonIsEnabled = false;
                OutstandingTicketCountMessage = Resources.NoOutstandingAccountsToVoidString;
            }
            else
            {
                VoidAccountYesNoButtonIsEnabled = true;
                OutstandingTicketCountMessage = string.Format(Resources.VoidOutstandingAccountsYesNoString, count);
            }
        }

        public void GetUpdatedOperatorList()
        {
            m_controller.SessionOperatorList();
        }

        private void SetIconStatus(bool success)
        {
            IsSuccess = success;
            HasError = !success;
            m_statusMessageTimer.Stop();
            m_statusMessageTimer.Start();
        }

        private void StatusTimerElapsedHandler(object sender, EventArgs args)
        {
            //clear status
            SessionStatusMessage = string.Empty;
            HasError = false;
            IsSuccess = false;
            m_statusMessageTimer.Stop();
        }
        #endregion
    }
}
