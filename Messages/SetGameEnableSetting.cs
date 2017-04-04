using System;
using System.IO;

namespace GameTech.Elite.Client.Modules.B3Center.Messages
{
    internal class SetGameEnableSetting : ServerMessage
    {
        private readonly int m_gameId;
        private readonly bool m_enable;


        /// <summary>
        /// Gti Server Message
        /// </summary>
        public SetGameEnableSetting(int gameId, bool enable)
        {    
            m_gameId = gameId;
            m_enable = enable;
        }

        public override int Id
        {
            get { return 39009; }
        }

        public override string Name
        {
            get { return "Set B3 Games Enable/Disable"; }
        }

        protected override void PackRequest(BinaryWriter requestWriter)
        {
            requestWriter.Write(Convert.ToInt32(m_gameId));
            requestWriter.Write(Convert.ToByte(Convert.ToBoolean(m_enable)));

        }
    }
}
