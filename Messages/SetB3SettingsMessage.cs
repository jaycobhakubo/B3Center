using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GameTech.Elite.Client.Modules.B3Center.Messages
{
    public class SetB3SettingsMessage : ServerMessage
    {

        private List<SettingMember> m_lB3Settings = new List<SettingMember>();


        public SetB3SettingsMessage(List<SettingMember> lB3Settings)
        {
            m_lB3Settings = lB3Settings;
        }

           protected override void PackRequest(BinaryWriter requestWriter)
        {
            requestWriter.Write((ushort)m_lB3Settings.Count);

            foreach (SettingMember sm in m_lB3Settings)
            {
                requestWriter.Write(sm.m_settingID);
                requestWriter.Write(sm.m_gameID);
                requestWriter.Write((ushort)sm.m_value.Length);
                requestWriter.Write(sm.m_value.ToCharArray());//3
            }

           
        }

         
        public override int Id
        {
            get { return 39010; }
        }

        public override string Name
        {
            get { return "Set B3 System Settings"; }
        }


      }
    
        public class SettingMember
        {
            public int m_settingID;
            public int m_gameID;
            public string m_value;
            public string m_oldValue;
        }

}
