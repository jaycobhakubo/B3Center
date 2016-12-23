using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using GameTech.Elite.Client.Modules.B3Center.Business;
using System.IO;

namespace GameTech.Elite.Client.Modules.B3Center.Messages
{
    internal class SetGameEnableSQL : ServerMessage
    {
        private int m_gameID;
        private bool m_enable;


        /// <summary>
        /// Gti Server Message
        /// </summary>
        public SetGameEnableSQL(int GameID, bool Enable)
        {    
            m_gameID = GameID + 1;
            m_enable = Enable;
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
            requestWriter.Write(Convert.ToInt32(m_gameID));
            requestWriter.Write(Convert.ToByte(Convert.ToBoolean(m_enable)));

        }
    }
}
