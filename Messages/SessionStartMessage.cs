#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System.IO;

namespace GameTech.Elite.Client.Modules.B3Center.Messages
{
    internal class SessionStartMessage : ServerMessage
    {
        #region Member Variables
        private readonly int m_operatorId;
        private readonly string m_operatorName;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the SessionStartMessage class
        /// </summary>
        public SessionStartMessage(int operatorId, string operatorName)
        {
            m_operatorId = operatorId;
            m_operatorName = operatorName;
        }
        #endregion

        #region Member Methods

        protected override void PackRequest(BinaryWriter requestWriter)
        {
            requestWriter.Write(m_operatorId);
            requestWriter.Write((ushort)m_operatorName.Length);
            requestWriter.Write(m_operatorName.ToCharArray());
        }

        protected override void UnpackResponse(BinaryReader responseReader)
        {
        }

        #endregion

        #region Member Properties
        public override int Id
        {
            get
            {
                return 39050;
            }
        }

        public override string Name
        {
            get
            {
                return "Start B3 Session";
            }
        }

        #endregion
    }
}
