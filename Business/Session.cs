#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

namespace GameTech.Elite.Client.Modules.B3Center.Business
{
    public class Session
    {
        #region Constructors

        /// <summary>
        /// Constructs a Session object
        /// </summary>
        /// <param name="sessionNumber">The session number.</param>
        /// <param name="sessionActive">Flag to set if the session is active or not.</param>
        /// <param name="name"></param>
        /// <param name="startTime"></param>
        public Session(int sessionNumber, bool sessionActive, string name, string startTime)
        {
            Number = sessionNumber;
            Active = sessionActive;
            OperatorName = name;
            SessionStartTime = startTime;
        }
        #endregion

        #region Member Properties

        /// <summary>
        /// Get or sets the session number.
        /// </summary>
        public int Number
        {
            get;
            private set;
        } 

        public string OperatorName 
        { 
            get; 
            private set; 
        }

        public string SessionStartTime
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets or set weather the session is active or not.
        /// </summary>
        public bool Active
        {
            get;
            private set;
        }

        public string DisplayName
        {
            get { return string.Format("{0} {1}", Number, OperatorName); }
        }

        #endregion
    }
}
