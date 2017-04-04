using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
    public class PlayerSettingVm : ViewModelBase
    {
        #region MEMBER

        private PlayerSettings m_playerSetting;
        private ObservableCollection<B3GameSetting> m_b3SettingEnableDisable;

        #endregion
        #region CONSTRUCTOR

        public PlayerSettingVm(PlayerSettings playerSetting, ObservableCollection<B3GameSetting> b3SettingEnableDisable)
        {
            VolumeList = Volume();
            PlayerSetting = playerSetting;
            m_b3SettingEnableDisable = b3SettingEnableDisable;
            AssignEnableSettingToGame();    
        }

        #endregion
        #region METHOD

        public void RevertValueBack()
        {           
            AssignEnableSettingToGame();
            PlayerSetting = m_playerSetting;
        }

        private void AssignEnableSettingToGame()
        {     
            foreach (var gameSettings in m_b3SettingEnableDisable)
            {
                int b3Gameid = gameSettings.GameId;
                switch(b3Gameid)
                {
                    case (int)B3Game.Crazybout: 
                        {
                            m_playerSetting.CrazyboutGameSetting = gameSettings;                       
                            break; 
                        }
                    case (int)B3Game.Jailbreak: 
                        {
                            m_playerSetting.JailBreakGameSetting = gameSettings;
                            break;                         
                        }
                    case (int)B3Game.Mayamoney:
                        {
                            m_playerSetting.MayaMoneyGameSetting = gameSettings;
                            break;
                        }
                    case (int)B3Game.Spirit76: 
                        {
                            m_playerSetting.Spirit76GameSetting = gameSettings;
                            break;
                        }
                    case (int)B3Game.Timebomb:
                        {
                            m_playerSetting.TimeBombGameSetting = gameSettings;
                            break;
                        }
                    case (int)B3Game.Ukickem:
                        {
                            m_playerSetting.UKickemGameSetting = gameSettings;
                            break;
                        }
                    case (int)B3Game.Wildball: 
                        {
                            m_playerSetting.WildBallGameSetting = gameSettings;
                            break;
                        }
                    case (int)B3Game.Wildfire: 
                        {
                            m_playerSetting.WildFireGameSetting = gameSettings;
                            break;
                        }
                }
            }
        }
        private List<string> Volume()
        {
            List<string> result = new List<string> {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"};
            return result;
        }

        #endregion
        #region PROPERTIES
        public List<string> VolumeList { get; set; }

        public ObservableCollection<B3GameSetting> B3SettingEnableDisable
        {
       
            get { return m_b3SettingEnableDisable; }
            set
            {
                if (m_b3SettingEnableDisable != null && value != m_b3SettingEnableDisable)
                {
                    m_b3SettingEnableDisable = value;
                }
            }
        }

        public PlayerSettings PlayerSetting
        {
            get { return m_playerSetting; }
            set
            {
                m_playerSetting = value;
                RaisePropertyChanged("PlayerSetting");
            }
        }
        #endregion
    }
}

