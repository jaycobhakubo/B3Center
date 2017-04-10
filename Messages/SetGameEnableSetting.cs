using System;
using System.IO;
using GameTech.Elite.Client.Modules.B3Center.Business;

namespace GameTech.Elite.Client.Modules.B3Center.Messages
{
    internal class SetGameEnableSetting : ServerMessage
    {
        private readonly B3GameType m_gameType;
        private readonly bool m_enable;


        /// <summary>
        /// Gti Server Message
        /// </summary>
        public SetGameEnableSetting(B3GameType gameType, bool enable)
        {
            m_gameType = gameType;
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
            requestWriter.Write(Convert.ToInt32(m_gameType));
            requestWriter.Write(Convert.ToByte(Convert.ToBoolean(m_enable)));

        }
    }
}
