using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;
using GameTech.Elite.Client.Modules.B3Center.Business;

namespace GameTech.Elite.Client.Modules.B3Center.Messages
{
    public class SetB3SettingsMessage : ServerMessage
    {
        private readonly List<SettingMember> m_lB3Settings = new List<SettingMember>();

        public SetB3SettingsMessage(ObservableCollection<B3SettingGlobal> lB3Settings)
        {
            foreach (B3SettingGlobal sm in lB3Settings)
            {
                if (sm.B3SettingValue != sm.B3SettingdefaultValue)
                {
                    var settingMember = new SettingMember
                    {
                        GameType = sm.GameType,
                        SettingType = sm.SettingType,
                        Value = sm.B3SettingValue,
                        OldValue = sm.B3SettingdefaultValue
                    };

                    m_lB3Settings.Add(settingMember);
                }
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
                requestWriter.Write((int)sm.SettingType);
                requestWriter.Write((int)sm.GameType);
                requestWriter.Write((ushort)sm.Value.Length);
                requestWriter.Write(sm.Value.ToCharArray());//3
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
        public B3SettingType SettingType;
        public B3GameType GameType;
        public string Value;
        public string OldValue;
    }

}
