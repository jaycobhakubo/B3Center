using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using GameTech.Elite.Client.Modules.B3Center.Business;

namespace GameTech.Elite.Client.Modules.B3Center.Messages
{
    public class SetB3SettingsMessage : ServerMessage
    {

        private ObservableCollection<B3SettingGlobal> m_lB3Settings2;
        private List<SettingMember> m_lB3Settings = new List<SettingMember>();

        public SetB3SettingsMessage(ObservableCollection<B3SettingGlobal> lB3Settings)
        {
            m_lB3Settings2 = lB3Settings;
            foreach (B3SettingGlobal sm in lB3Settings)
            {
                var settingMember = new SettingMember();
                settingMember.m_gameID = sm.B3GameID;
                settingMember.m_settingID = sm.B3SettingID;
                settingMember.m_value = sm.B3SettingValue;
                settingMember.m_oldValue = sm.B3SettingdefaultValue;
                m_lB3Settings.Add(settingMember);
            }
        }


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
