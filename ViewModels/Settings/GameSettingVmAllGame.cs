using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using System.Collections.Generic;
using System.Linq;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
    public class GameSettingVmAllGame : ViewModelBase
    {
        #region Fields

        private readonly List<B3SettingGlobal> m_originalGameSettings;
        private GameSetting m_gameSetting;
        private B3IsGameEnabledSetting m_isGameEnabledSetting;
        private bool m_isPayTableSettingHasChanged;

        #endregion

        #region Constructor

        public GameSettingVmAllGame(List<B3SettingGlobal> gameSettingsList, B3GameType gameType, B3IsGameEnabledSetting isGameEnabledSetting)
        {
            GameType = gameType;
            Settings = new GameSetting { GameType = gameType };
            UpdateGameSettingsListToModel(gameSettingsList, isGameEnabledSetting);
            SettingViewModel.Instance.BtnSaveIsEnabled = Settings.EnableGameSetting.IsEnabled;
            m_originalGameSettings = gameSettingsList;
            m_isGameEnabledSetting = isGameEnabledSetting;
            m_isPayTableSettingHasChanged = false;
        }

        #endregion

        #region Properties
        
        public GameSetting Settings
        {
            get { return m_gameSetting; }
            set
            {
                m_gameSetting = value;
                RaisePropertyChanged("Settings");
            }
        }

        public B3GameType GameType { get; private set; }
        public List<string> ListMaxBetLevel { get { return Business.Helpers.OneToTenList; } }
        public List<string> ListMaxCards { get { return Business.Helpers.MaxCardCountList; } }
        public List<string> ListCallSpeedMin { get { return Business.Helpers.OneToTenList; } }
        public List<string> ListCallSpeedMax { get { return Business.Helpers.OneToTenList; } }
        public List<string> ListCallSpeed { get { return Business.Helpers.OneToTenList; } }
        public List<string> ListCallSpeedBonus { get { return Business.Helpers.OneToTenList; } }
        public bool IsPayTableSettingHasChanged { get { return m_isPayTableSettingHasChanged; } }

        #endregion

        #region Methods

        /// <summary>
        /// Converts the game settings list to model. Updates local model with game settings
        /// </summary>
        /// <param name="gameSettingList">The b3 setting.</param>
        /// <param name="isGameEnabledSetting">The is game enabled setting.</param>
        /// <returns></returns>
        private void UpdateGameSettingsListToModel(List<B3SettingGlobal> gameSettingList, B3IsGameEnabledSetting isGameEnabledSetting)
        {
            Settings.EnableGameSetting = isGameEnabledSetting;

            foreach (B3SettingGlobal setting in gameSettingList)
            {
                switch (setting.SettingType)
                {
                    case B3SettingType.Denom1:
                        Settings.Denom1 = setting.B3SettingValue;
                        break;
                    case B3SettingType.Denom5:
                        Settings.Denom5 = setting.B3SettingValue;
                        break;
                    case B3SettingType.Denom10:
                        Settings.Denom10 = setting.B3SettingValue;
                        break;
                    case B3SettingType.Denom25:
                        Settings.Denom25 = setting.B3SettingValue;
                        break;
                    case B3SettingType.Denom50:
                        Settings.Denom50 = setting.B3SettingValue;
                        break;
                    case B3SettingType.Denom100:
                        Settings.Denom100 = setting.B3SettingValue;
                        break;
                    case B3SettingType.Denom200:
                        Settings.Denom200 = setting.B3SettingValue;
                        break;
                    case B3SettingType.Denom500:
                        Settings.Denom500 = setting.B3SettingValue;
                        break;
                    case B3SettingType.MaxBetLevel:
                        Settings.MaxBetLevel = setting.B3SettingValue;
                        break;
                    case B3SettingType.MaxCards:
                        Settings.MaxCards = setting.B3SettingValue;
                        break;
                    case B3SettingType.CallSpeed:
                        Settings.CallSpeed = setting.B3SettingValue;
                        break;
                    case B3SettingType.AutoCall:
                        Settings.AutoCall = setting.ConvertB3StringValueToBool();
                        break;
                    case B3SettingType.AutoPlay:
                        Settings.AutoPlay = setting.ConvertB3StringValueToBool();
                        break;
                    case B3SettingType.HideSerialNumber:
                        Settings.HideSerialNumber = setting.ConvertB3StringValueToBool();
                        break;
                    case B3SettingType.SingleOfferBonus:
                        Settings.SingleOfferBonus = setting.ConvertB3StringValueToBool();
                        break;
                    case B3SettingType.CallSpeedMin:
                        Settings.CallSpeedMin = setting.B3SettingValue;
                        break;
                    case B3SettingType.CallSpeedBonus:
                        Settings.CallSpeedBonus = setting.B3SettingValue;
                        break;
                }
            }
        }
        public bool HasChanged { get; set; }
        private void UpdateModelToGameSettingsList()
        {
            HasChanged = false;
            foreach (B3SettingGlobal gameSetting in m_originalGameSettings)
            {
                gameSetting.HasChanged = false;
                var tempOldSettingValue = gameSetting.B3SettingValue;//saved current setting value
                switch (gameSetting.SettingType)
                {
                    case B3SettingType.Denom1:
                            gameSetting.B3SettingValue = Settings.Denom1;
                            break;
                    case B3SettingType.Denom5:
                            gameSetting.B3SettingValue = Settings.Denom5;
                            break;
                    case B3SettingType.Denom10:
                            gameSetting.B3SettingValue = Settings.Denom10;
                            break;
                    case B3SettingType.Denom25:
                            gameSetting.B3SettingValue = Settings.Denom25;
                            break;
                    case B3SettingType.Denom50:
                            gameSetting.B3SettingValue = Settings.Denom50;
                            break;
                    case B3SettingType.Denom100:
                            gameSetting.B3SettingValue = Settings.Denom100;
                            break;
                    case B3SettingType.Denom200:
                            gameSetting.B3SettingValue = Settings.Denom200;
                            break;
                    case B3SettingType.Denom500:
                            gameSetting.B3SettingValue = Settings.Denom500;
                            break;
                    case B3SettingType.MaxBetLevel:
                            gameSetting.B3SettingValue = Settings.MaxBetLevel;
                            break;
                    case B3SettingType.MaxCards:
                            gameSetting.B3SettingValue = Settings.MaxCards;
                            break;
                    case B3SettingType.CallSpeed:
                            gameSetting.B3SettingValue = Settings.CallSpeed;
                            break;
                    case B3SettingType.AutoCall:
                            gameSetting.B3SettingValue = Settings.AutoCall.ConvertToB3StringValue();
                            break;
                    case B3SettingType.AutoPlay:
                            gameSetting.B3SettingValue = Settings.AutoPlay.ConvertToB3StringValue();
                            break;
                    case B3SettingType.HideSerialNumber:
                            gameSetting.B3SettingValue = Settings.HideSerialNumber.ConvertToB3StringValue();
                            break;
                    case B3SettingType.SingleOfferBonus:
                            gameSetting.B3SettingValue = Settings.SingleOfferBonus.ConvertToB3StringValue();
                            break;
                    case B3SettingType.CallSpeedMin:
                            gameSetting.B3SettingValue = Settings.CallSpeedMin;
                            break;
                    case B3SettingType.CallSpeedBonus:
                            gameSetting.B3SettingValue = Settings.CallSpeedBonus;
                            break;
                }
                if (tempOldSettingValue != gameSetting.B3SettingValue)//check if current = new setting
                {
                    gameSetting.B3SettingDefaultValue = tempOldSettingValue;
                    gameSetting.HasChanged = true;
                    if (HasChanged != true) HasChanged = true;
                }
            }
        }
    
        public List<B3SettingGlobal> Save()
        {
            UpdateModelToGameSettingsList();
            m_isGameEnabledSetting = Settings.EnableGameSetting;
            return m_originalGameSettings;
        }

        public void ResetSettingsToDefault()
        {
            UpdateGameSettingsListToModel(m_originalGameSettings, m_isGameEnabledSetting);
        }
    
        #endregion

    }
}
