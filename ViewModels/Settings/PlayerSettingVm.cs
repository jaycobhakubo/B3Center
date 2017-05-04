using System;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using System.Collections.Generic;


namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
    public class PlayerSettingVm : ViewModelBase
    {
        #region Fields

        private PlayerSettings m_playerSetting;
        private List<B3IsGameEnabledSetting> m_originalEnableDisableSetings;
        private readonly List<B3SettingGlobal> m_origianlPlayerSettings; 

        #endregion

        #region Constructor

        public PlayerSettingVm(List<B3SettingGlobal> playerSettingsList, List<B3IsGameEnabledSetting> b3SettingEnableDisable)
        {
            VolumeList = Business.Helpers.ZeroToTenList;
            PlayerSetting = new PlayerSettings();
            UpdateSettingsListToModel(playerSettingsList, b3SettingEnableDisable);           
            m_origianlPlayerSettings = playerSettingsList;
            m_originalEnableDisableSetings = b3SettingEnableDisable;
        }

        #endregion

        #region Method

        public List<B3IsGameEnabledSetting> GetCurrentEnableDisableGameSettings()
        {
            return m_originalEnableDisableSetings;
        }

        public List<B3SettingGlobal> Save()
        {
            UpdateModelToSettingsList();
            return m_origianlPlayerSettings;
        }

        public void ResetSettingsToDefault()
        {
            UpdateSettingsListToModel(m_origianlPlayerSettings, m_originalEnableDisableSetings);
        }

        private void UpdateSettingsListToModel(List<B3SettingGlobal> playerSettingsList, List<B3IsGameEnabledSetting> isGameEnabledSettings)
        {
       
            foreach (var setting in playerSettingsList)
            {
                switch (setting.SettingType)
                {
                    case B3SettingType.PlayerCalibrateTouch:
                        PlayerSetting.PlayerCalibrateTouch = setting.ConvertB3StringValueToBool();
                        break;
                    case B3SettingType.PresstoCollect:
                        PlayerSetting.PresstoCollect = setting.ConvertB3StringValueToBool();
                        break;
                    case B3SettingType.AnnounceCall:
                        PlayerSetting.AnnounceCall = setting.ConvertB3StringValueToBool();
                        break;
                    case B3SettingType.PlayerScreenCursor:
                        PlayerSetting.PlayerScreenCursor = setting.ConvertB3StringValueToBool();
                        break;
                    case B3SettingType.TimeToCollect:
                        PlayerSetting.TimeToCollect = setting.B3SettingValue;
                        break;
                    case B3SettingType.Disclaimer:
                        PlayerSetting.Disclaimer = setting.ConvertB3StringValueToBool();
                        break;
                    case B3SettingType.PlayerMainVolume:
                        PlayerSetting.PlayerMainVolume = Business.Helpers.GetVolumeEquivValue(Convert.ToInt32(setting.B3SettingValue));
                        break;
                }
            }
         
            foreach (var gameEnabledSetting in isGameEnabledSettings)
            {
                switch (gameEnabledSetting.GameType)
                {
                    case B3GameType.Crazybout:
                        PlayerSetting.CrazyboutGameSetting.IsEnabled = gameEnabledSetting.IsEnabled;
                            break;
                    case B3GameType.Jailbreak:
                            PlayerSetting.JailBreakGameSetting.IsEnabled = gameEnabledSetting.IsEnabled;
                            break;
                    case B3GameType.Mayamoney:
                            PlayerSetting.MayaMoneyGameSetting.IsEnabled = gameEnabledSetting.IsEnabled;
                            break;
                    case B3GameType.Spirit76:
                            PlayerSetting.Spirit76GameSetting.IsEnabled = gameEnabledSetting.IsEnabled;
                            break;
                    case B3GameType.Timebomb:
                            PlayerSetting.TimeBombGameSetting.IsEnabled = gameEnabledSetting.IsEnabled;
                            break;
                    case B3GameType.Ukickem:
                            PlayerSetting.UKickemGameSetting.IsEnabled = gameEnabledSetting.IsEnabled;
                            break;
                    case B3GameType.Wildball:
                            PlayerSetting.WildBallGameSetting.IsEnabled = gameEnabledSetting.IsEnabled;
                            break;
                    case B3GameType.Wildfire:
                            PlayerSetting.WildFireGameSetting.IsEnabled = gameEnabledSetting.IsEnabled;
                            break;
                }
            }
        }

        private void UpdateModelToSettingsList()
        {
            foreach (var setting in m_origianlPlayerSettings)
            {
                setting.HasChanged = false;
                var tempOldSettingValue = setting.B3SettingValue;//saved current setting value
                switch (setting.SettingType)
                {
                    case B3SettingType.PlayerCalibrateTouch:
                        setting.B3SettingValue = PlayerSetting.PlayerCalibrateTouch.ConvertToB3StringValue();
                        break;
                    case B3SettingType.PresstoCollect:
                        setting.B3SettingValue = PlayerSetting.PresstoCollect.ConvertToB3StringValue();
                        break;
                    case B3SettingType.AnnounceCall:
                        setting.B3SettingValue = PlayerSetting.AnnounceCall.ConvertToB3StringValue();
                        break;
                    case B3SettingType.PlayerScreenCursor:
                        setting.B3SettingValue = PlayerSetting.PlayerScreenCursor.ConvertToB3StringValue();
                        break;
                    case B3SettingType.TimeToCollect:
                        setting.B3SettingValue = PlayerSetting.TimeToCollect;
                        break;
                    case B3SettingType.Disclaimer:
                        setting.B3SettingValue = PlayerSetting.Disclaimer.ConvertToB3StringValue();
                        break;
                    case B3SettingType.PlayerMainVolume:
                        setting.B3SettingValue = Business.Helpers.GetVolumeEquivToDb(Convert.ToInt32(PlayerSetting.PlayerMainVolume));
                        break;
                }

                if (tempOldSettingValue != setting.B3SettingValue)//check if current = new setting
                {
                    setting.B3SettingDefaultValue = tempOldSettingValue;
                    setting.HasChanged = true;
                }
            }


            foreach (var gameEnabledSetting in m_originalEnableDisableSetings)
            {
                gameEnabledSetting.HasChanged = false;
                var tempOldSettingValue = gameEnabledSetting.IsEnabled;//saved current setting value
                switch (gameEnabledSetting.GameType)
                {
                    case B3GameType.Crazybout:
                        gameEnabledSetting.IsEnabled = PlayerSetting.CrazyboutGameSetting.IsEnabled;
                        break;
                    case B3GameType.Jailbreak:
                        gameEnabledSetting.IsEnabled = PlayerSetting.JailBreakGameSetting.IsEnabled;
                        break;
                    case B3GameType.Mayamoney:
                        gameEnabledSetting.IsEnabled = PlayerSetting.MayaMoneyGameSetting.IsEnabled;
                        break;
                    case B3GameType.Spirit76:
                        gameEnabledSetting.IsEnabled = PlayerSetting.Spirit76GameSetting.IsEnabled;
                        break;
                    case B3GameType.Timebomb:
                        gameEnabledSetting.IsEnabled = PlayerSetting.TimeBombGameSetting.IsEnabled;
                        break;
                    case B3GameType.Ukickem:
                        gameEnabledSetting.IsEnabled = PlayerSetting.UKickemGameSetting.IsEnabled;
                        break;
                    case B3GameType.Wildball:
                        //ModifiedB3GameEnabledSettings.Add(PlayerSetting.WildBallGameSetting);
                        gameEnabledSetting.IsEnabled = PlayerSetting.WildBallGameSetting.IsEnabled;
                        break;
                    case B3GameType.Wildfire:
                        gameEnabledSetting.IsEnabled = PlayerSetting.WildFireGameSetting.IsEnabled;
                        break;
                }
                if (tempOldSettingValue != gameEnabledSetting.IsEnabled)//check if current = new setting
                {
                    gameEnabledSetting.B3SettingDefaultValue = tempOldSettingValue;
                    gameEnabledSetting.HasChanged = true;
                }
            }          
        }
      
        #endregion

        #region Properties

        public List<string> VolumeList { get; private set; }

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

