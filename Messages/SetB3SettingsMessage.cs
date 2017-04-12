using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections.ObjectModel;
using GameTech.Elite.Client.Modules.B3Center.Business;

using System;
using System.Linq;
using GameTech.Elite.Base;
using System.Collections.ObjectModel;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;


namespace GameTech.Elite.Client.Modules.B3Center.Messages
{
    public class SetB3SettingsMessage : ServerMessage
    {
        private readonly List<SettingMember> m_lB3Settings = new List<SettingMember>();

        public SetB3SettingsMessage(ObservableCollection<B3SettingGlobal> lB3Settings)
        {
            B3SettingGlobal MathPayTableSetting = new B3SettingGlobal();
            bool IsMathPayTable = true;

            try { MathPayTableSetting = lB3Settings.Single(l => l.SettingType == B3SettingType.MathPayTableSetting); }
            catch { IsMathPayTable = false; }
     
                if (MathPayTableSetting.B3SettingValue != MathPayTableSetting.B3SettingdefaultValue && IsMathPayTable == true) //Check if it is a paytable setting  request.
                {
                var settingMember = new SettingMember()
                {
                    GameType = MathPayTableSetting.GameType,
                    SettingType = MathPayTableSetting.SettingType,
                    Value = MathPayTableSetting.B3SettingValue,
                    OldValue = MathPayTableSetting.B3SettingdefaultValue
                };

                m_lB3Settings.Add(settingMember);//Add it on the very first list (it will excute first on the SP).

                foreach (B3SettingGlobal sm in lB3Settings)//Add all settings. This will revert back to the current settings and not the default setting from paytable.
                {
                    if (sm.SettingType == B3SettingType.MathPayTableSetting) continue;
                       settingMember = new SettingMember
                        {
                            GameType = sm.GameType,
                            SettingType = sm.SettingType,
                            Value = sm.B3SettingValue,
                            OldValue = sm.B3SettingdefaultValue
                        };

                        m_lB3Settings.Add(settingMember);                
                }
            }
            else 
            {
                foreach (B3SettingGlobal sm in lB3Settings)
                {
                    if (sm.B3SettingValue != sm.B3SettingdefaultValue)//filter only setting that need to be changed.
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
