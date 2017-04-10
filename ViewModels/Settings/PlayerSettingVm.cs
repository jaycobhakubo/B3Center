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
            VolumeList = SettingViewModel.ZeroToTenList();
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
                switch(gameSettings.GameType)
                {
                    case B3GameType.Crazybout: 
                        {
                            m_playerSetting.CrazyboutGameSetting = gameSettings;                       
                            break; 
                        }
                    case B3GameType.Jailbreak: 
                        {
                            m_playerSetting.JailBreakGameSetting = gameSettings;
                            break;                         
                        }
                    case B3GameType.Mayamoney:
                        {
                            m_playerSetting.MayaMoneyGameSetting = gameSettings;
                            break;
                        }
                    case B3GameType.Spirit76: 
                        {
                            m_playerSetting.Spirit76GameSetting = gameSettings;
                            break;
                        }
                    case B3GameType.Timebomb:
                        {
                            m_playerSetting.TimeBombGameSetting = gameSettings;
                            break;
                        }
                    case B3GameType.Ukickem:
                        {
                            m_playerSetting.UKickemGameSetting = gameSettings;
                            break;
                        }
                    case B3GameType.Wildball: 
                        {
                            m_playerSetting.WildBallGameSetting = gameSettings;
                            break;
                        }
                    case B3GameType.Wildfire: 
                        {
                            m_playerSetting.WildFireGameSetting = gameSettings;
                            break;
                        }
                }
            }
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

