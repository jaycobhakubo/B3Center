#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

//US4155: B3 Void Accounts

using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Globalization;
using GameTech.Elite.Base;
using GameTech.Elite.UI;
using GameTech.Elite.Client.Modules.B3Center.Properties;
using GameTech.Elite.Client.Modules.B3Center.Messages;


namespace GameTech.Elite.Client.Modules.B3Center.Business
{
    internal class B3Controller : ControllerBase
    {
        #region Events
        /// <summary>
        /// Occurs when the session start message has completed.
        /// </summary>
        public event AsyncCompletedEventHandler SessionStartCompleted;
        /// <summary>
        /// Occurs when the session end message has completed.
        /// </summary>
        public event AsyncCompletedEventHandler SessionEndCompleted;
        /// <summary>
        /// Occurs when the session information message has completed.
        /// </summary>
        public event AsyncCompletedEventHandler SessionInfoCompleted;
        /// <summary>
        /// Occurs when the session operator list message has completed.
        /// </summary>
        public event AsyncCompletedEventHandler SessionOperatorListCompleted;

        #endregion

        #region Constructors
        public B3Controller(B3CenterController parent)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            Parent = parent;

            DoSessionOperatorListNow();
            DoSessionListNow();
            GetSelectedBalls();
        }
        #endregion

        #region Member Methods
        #region Session Start Message
        /// <summary>
        /// Start a B3 Session
        /// </summary>
        public void SessionStart(Operator op)
        {
            if (op == null)
                throw new ArgumentNullException();

            if (!IsBusy)
            {
                IsBusy = true;

                ArrayList args = new ArrayList {op.OperatorId, op.OperatorName};

                RunWorker(Resources.SessionStartProgress,
                          DoSessionStart,
                          args,
                          false,
                          OnSessionStartCompleted);
            }
        }

        /// <summary>
        /// Sends the start session message to the server.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A DoWorkEventArgs object that contains the event data.</param>
        private void DoSessionStart(object sender, DoWorkEventArgs e)
        {
            EliteModule.SetThreadCulture(Parent.Settings);
            ArrayList args = (ArrayList)e.Argument;
            SessionStartMessage message = new SessionStartMessage((int)args[0], (string)args[1]);
            message.Send();

            if (message.ReturnCode == ServerReturnCode.Success)
            {
                //DoSessionListNow();
            }
            else
            {
                throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, Resources.SessionStartFailed,
                    ServerErrorTranslator.GetReturnCodeMessage(message.ReturnCode)));
            }

        }

        /// <summary>
        /// Handles when the Session Start background worker is completed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A RunWorkerCompletedEventArgs object that contains the event data.</param>
        private void OnSessionStartCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsBusy = false;
            ProgressText = null;

            if (!CheckForError(e.Error))
            {
                
            }

            AsyncCompletedEventHandler handler = SessionStartCompleted;
            if (handler != null)
                handler(this, new AsyncCompletedEventArgs(e.Error, e.Cancelled, null)); 
        }
        #endregion

        #region Session End Message
        /// <summary>
        /// Ends a B3 Session
        /// </summary>
        public void SessionEnd()
        {
            if (!IsBusy)
            {
                IsBusy = true;

                RunWorker(Resources.SessionEndProgress,
                          DoSessionEnd,
                          null,
                          false,
                          OnSessionEndCompleted);
            }
        }

        /// <summary>
        /// Sends the end session message to the server.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">A DoWorkEventArgs object that contains the event data.</param>
        private void DoSessionEnd(object sender, DoWorkEventArgs e)
        {
            EliteModule.SetThreadCulture(Parent.Settings);
            SessionEndMessage message = new SessionEndMessage();
            message.Send();
            
            if (message.ReturnCode == ServerReturnCode.Success)
            {
                //DoSessionListNow();
            }
            else
                throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, Resources.SessionEndFailed,
                    ServerErrorTranslator.GetReturnCodeMessage(message.ReturnCode)));

        }

        /// <summary>
        /// Handles when the Session End background worker is completed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A RunWorkerCompletedEventArgs object that contains the event data.</param>
        private void OnSessionEndCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsBusy = false;
            ProgressText = null;

            if (!CheckForError(e.Error))
            {

            }

            AsyncCompletedEventHandler handler = SessionEndCompleted;
            if (handler != null)
                handler(this, new AsyncCompletedEventArgs(e.Error, e.Cancelled, null)); 
        }
        #endregion

        #region Set/Get balls

        public void SetBalls(List<int> ballList)
        {
            SetB3BallsMessage message = new SetB3BallsMessage(ballList.ToArray());
            message.Send();

            if (message.ReturnCode != ServerReturnCode.Success)
                throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, Resources.SessionSetBallsFailed, ServerErrorTranslator.GetReturnCodeMessage(message.ReturnCode)));
        }

        private void GetSelectedBalls()
        {
            GetB3BallsMessage message = new GetB3BallsMessage();
            message.Send();

            if (message.ReturnCode == ServerReturnCode.Success)
            {
                GameBallList = message.GameBallList;
            }
            else
                throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, Resources.SessionSetBallsFailed, ServerErrorTranslator.GetReturnCodeMessage(message.ReturnCode)));
        }

        #endregion

        #region Void Account Messages

        public int GetOutstandingSessionTicketCount()
        {
            int count;
            EliteModule.SetThreadCulture(Parent.Settings);
            var message = new GetB3SessionTicketCountMessage();
            message.Send();

            if (message.ReturnCode == ServerReturnCode.Success)
            {
                count = message.OutstandingTicketCount;
            }
            else
            {
                throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, Resources.SessionStartFailed,
                    ServerErrorTranslator.GetReturnCodeMessage(message.ReturnCode)));
            }
            return count;
        }

        public void VoidOutstandingSessionTickets()
        {
            EliteModule.SetThreadCulture(Parent.Settings);
            var message = new VoidB3SessionTicketsMessage();
            message.Send();

            if (message.ReturnCode != ServerReturnCode.Success)
            {
                throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, Resources.SessionStartFailed,
                    ServerErrorTranslator.GetReturnCodeMessage(message.ReturnCode)));
            }
        }

        #endregion


        #region Session List Message

        /// <summary>
        /// Gets information about the current session
        /// </summary>
        public void GetSessionList()
        {
            IsBusy = true;

            RunWorker("In progress",
                        DoSessionList,
                        null,
                        false,
                        OnSessionListCompleted);
        }

        /// <summary>
        /// Sends the session message to the server
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">A DoWorkEventArgs object that contains the event data.</param>
        private void DoSessionList(object sender, DoWorkEventArgs e)
        {
            GetB3SessionListMessage message = new GetB3SessionListMessage();
            message.Send();

            if (message.ReturnCode == ServerReturnCode.Success)
                e.Result = message.SessionList;
            else
                throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, Resources.SessionInfoFailed, ServerErrorTranslator.GetReturnCodeMessage(message.ReturnCode)));

        }

        public void DoSessionListNow()
        {
            GetB3SessionListMessage message = new GetB3SessionListMessage();
            message.Send();

            if (message.ReturnCode == ServerReturnCode.Success)
            {
                Sessions = message.SessionList;

                //set any active session
                Session = Sessions.FirstOrDefault(s => s.Active);
            }
            else
                throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, Resources.SessionInfoFailed, ServerErrorTranslator.GetReturnCodeMessage(message.ReturnCode)));
        }

        /// <summary>
        /// Handles when the Session Active background worker is completed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A RunWorkerCompletedEventArgs object that contains the event data.</param>
        private void OnSessionListCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsBusy = false;
            ProgressText = null;

            if (!CheckForError(e.Error))
            {
                Sessions = (List<Session>)e.Result;

                Session = Sessions.FirstOrDefault(s => s.Active);
            }

            AsyncCompletedEventHandler handler = SessionInfoCompleted;
            if (handler != null)
                handler(this, new AsyncCompletedEventArgs(e.Error, e.Cancelled, null));

        }

        #endregion

        #region Session Operator List Message

        /// <summary>
        /// Gets information about the current session
        /// </summary>
        public void SessionOperatorList()
        {

            if (!IsBusy)
            {
                IsBusy = true;

                RunWorker(Resources.SessionOperatorListProgress,
                          DoSessionOperatorList,
                          null,
                          false,
                          OnSessionOperatorListCompleted);
            }
        }

        /// <summary>
        /// Sends the session message to the server
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">A DoWorkEventArgs object that contains the event data.</param>
        private void DoSessionOperatorList(object sender, DoWorkEventArgs e)
        {
            EliteModule.SetThreadCulture(Parent.Settings);
            GetSessionOperatorListMessage message = new GetSessionOperatorListMessage(Parent.StaffId);
            message.Send();

            if (message.ReturnCode == ServerReturnCode.Success)
                e.Result = message.OperatorList;
            else
                throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, Resources.SessionOperatorListFailed, ServerErrorTranslator.GetReturnCodeMessage(message.ReturnCode)));

        }

        private void DoSessionOperatorListNow()
        {
            EliteModule.SetThreadCulture(Parent.Settings);
            GetSessionOperatorListMessage message = new GetSessionOperatorListMessage(Parent.StaffId);
            message.Send();

            if (message.ReturnCode == ServerReturnCode.Success)
                Operators = message.OperatorList;
            else
                throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, Resources.SessionOperatorListFailed, ServerErrorTranslator.GetReturnCodeMessage(message.ReturnCode)));
        }

        /// <summary>
        /// Handles when the Session Active background worker is completed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A RunWorkerCompletedEventArgs object that contains the event data.</param>
        private void OnSessionOperatorListCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsBusy = false;
            ProgressText = null;

            if (!CheckForError(e.Error))
            {
                Operators = (ObservableCollection<Operator>)e.Result;
            }

            AsyncCompletedEventHandler handler = SessionOperatorListCompleted;
            if (handler != null)
                handler(this, new AsyncCompletedEventArgs(e.Error, e.Cancelled, null));

        }

        #endregion
        
        protected override bool CheckForError(Exception ex)
        {
            if (ex == null)
                return false;
            else
            {
                if (ex is ServerCommException)
                    Parent.ServerCommFailure();
                else
                {
                    if (Parent.CurrentView != null)
                        MessageWindow.Show(Parent.CurrentView, ex.Message, Resources.B3CenterName, MessageWindowType.Close);
                    else
                        MessageWindow.Show(ex.Message, Resources.B3CenterName, MessageWindowType.Close);
                }

                return true;
            }
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Gets or sets the owner of this controller.
        /// </summary>
        public B3CenterController Parent
        {
            get;
            set;
        }
       
        public List<Session> Sessions
        {
            get;
            private set;
        }

        public Session Session
        {
            get;
            private set;
        }

        public ObservableCollection<Operator> Operators
        {
            get;
            set;
        }

        public B3CenterSettings Settings
        {
            get { return Parent.Settings; }
        }

        public List<B3Report> Reports { get; set; }

        public List<int> GameBallList { get; set; }

        #endregion
    }
}
