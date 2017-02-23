using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;



namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
    public class PlayerSettingVm : ViewModelBase
    {
        private PlayerSettings m_playerSetting;
        public ObservableCollection<B3GameSetting> B3SettingEnableDisable { get; set; }

        public PlayerSettingVm(PlayerSettings _playerSetting, ObservableCollection<B3GameSetting> m_b3SettingEnableDisable_)
        {
            VolumeList = Volume();
            PlayerSetting_ = _playerSetting;        
            B3SettingEnableDisable = m_b3SettingEnableDisable_;
            AssignEnableSettingToGame(B3SettingEnableDisable);//Now this collection wont change.
        }

        private void AssignEnableSettingToGame(ObservableCollection<B3GameSetting> m_b3SettingEnableDisable_)
        {
            foreach (var i in m_b3SettingEnableDisable_)
            {
                int b3gameid = i.GameId;
                switch(b3gameid)
                {
                    case (int)B3Game.CRAZYBOUT: { PlayerSetting_.IsEnableCRAZYBOUT = i.IsEnabled; break; }
                    case (int)B3Game.JAILBREAK: { PlayerSetting_.IsEnableJAILBREAK = i.IsEnabled; break; }
                    case (int)B3Game.MAYAMONEY: { PlayerSetting_.IsEnableMAYAMONEY = i.IsEnabled; break; }
                    case (int)B3Game.SPIRIT76: { PlayerSetting_.IsEnableSPIRIT76 = i.IsEnabled; break; }
                    case (int)B3Game.TIMEBOMB: { PlayerSetting_.IsEnableTIMEBOMB = i.IsEnabled; break; }
                    case (int)B3Game.UKICKEM: { PlayerSetting_.IsEnableUKICKEM = i.IsEnabled; break; }
                    case (int)B3Game.WILDBALL: { PlayerSetting_.IsEnableWILDBALL = i.IsEnabled; break; }
                    case (int)B3Game.WILDFIRE: { PlayerSetting_.IsEnableWILDFIRE = i.IsEnabled; break; }
                }
            }
        }

        public List<string> VolumeList { get; set; }
        public PlayerSettings PlayerSetting_
        {
            get { return m_playerSetting; }          
            set
            {
                m_playerSetting = value;
                RaisePropertyChanged("PlayerSetting_");
            }
        }

        private List<string> Volume()
        {
            List<string> result = new List<string>();
            result.Add("0");
            result.Add("1");
            result.Add("2");
            result.Add("3");
            result.Add("4");
            result.Add("5");
            result.Add("6");
            result.Add("7");
            result.Add("8");
            result.Add("9");
            result.Add("10");
            return result;
        }        
    }
}
//private ObservableCollection<B3GameSetting> m_b3SettingEnableDisable;
//private B3GameSetting m_isCRAZYBOUTEnableIsAllowedSetting;
//private B3GameSetting m_isJAILBREAKEnableIsAllowedSetting;
//private B3GameSetting m_isMAYAMONEYEnableIsAllowedSetting;
//private B3GameSetting m_isSPIRIT76EnableIsAllowedSetting;
//private B3GameSetting m_isTIMEBOMBEnableIsAllowedSetting;
//private B3GameSetting m_isUKICKEMEnableIsAllowedSetting;
//private B3GameSetting m_isWILDBALLEnableIsAllowedSetting;
//private B3GameSetting m_isWILDFIREEnableIsAllowedSetting;
