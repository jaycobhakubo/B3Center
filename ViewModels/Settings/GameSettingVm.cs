using System;
using System.Collections.Generic;
using System.Linq;
using GameTech.Elite.Base;
using System.Collections.ObjectModel;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
    public class GameSettingVm : ViewModelBase
    {
        #region MEMBER

        private readonly List<B3SettingGlobal> m_b3GameStting;
        private GameSetting m_gs = new GameSetting();
        private readonly List<GameSettingVmAllGame> m_gameSettingViewModels;
        private B3GameType m_currentGameType;
        private int m_tabSelectedindex;
        private List<B3IsGameEnabledSetting> m_b3SettingEnableDisable;

        #endregion
        #region CONSTRUCTOR

        public GameSettingVm(List<B3SettingGlobal> b3GameSetting, List<B3IsGameEnabledSetting> gameEnableDisableSetting )
        {
            m_gameSettingViewModels = new List<GameSettingVmAllGame>();
            m_b3SettingEnableDisable = gameEnableDisableSetting;
            m_b3GameStting = b3GameSetting;

            foreach (var gameTypeObj in Enum.GetValues(typeof(B3GameType)))
            {
                var gameType = (B3GameType)gameTypeObj;
                var gameSettings = m_b3GameStting.Where(l => l.GameType == gameType && l.IsPayTableSettings != true).ToList();
                var gameSettingViewModel = new GameSettingVmAllGame(gameSettings, gameType, GetEnableDisableSettingValue(gameType));
                m_gameSettingViewModels.Add(gameSettingViewModel);
                switch (gameType)
                {
                    case B3GameType.Crazybout:
                        GameCrzyBout = gameSettingViewModel;
                        break;
                    case B3GameType.Jailbreak:
                        GameJailBreak = gameSettingViewModel;
                        break;
                    case B3GameType.Mayamoney:
                        GameMayaMoney = gameSettingViewModel;
                        break;
                    case B3GameType.Spirit76:
                        GameSpirit76 = gameSettingViewModel;
                        break;
                    case B3GameType.Timebomb:
                        GameTimeBomb = gameSettingViewModel;
                        break;
                    case B3GameType.Ukickem:
                        GameUkickEm = gameSettingViewModel;
                        break;
                    case B3GameType.Wildball:
                        GameWildBall = gameSettingViewModel;
                        break;
                    case B3GameType.Wildfire:
                        GameWildfire = gameSettingViewModel;
                        break;
                }
            }

            m_currentGameType = B3GameType.Crazybout;
            SelectedGameVm = GameCrzyBout;
        }

        #endregion

        #region PROPERTIES

        public GameSettingVmAllGame SelectedGameVm { get; set; }

        public GameSettingVmAllGame GameCrzyBout { get; set; }
        public GameSettingVmAllGame GameJailBreak { get; set; }
        public GameSettingVmAllGame GameMayaMoney { get; set; }
        public GameSettingVmAllGame GameSpirit76 { get; set; }
        public GameSettingVmAllGame GameTimeBomb { get; set; }
        public GameSettingVmAllGame GameUkickEm { get; set; }
        public GameSettingVmAllGame GameWildBall { get; set; }
        public GameSettingVmAllGame GameWildfire { get; set; }


        public int TabSelectedIndex
        {
            get { return m_tabSelectedindex; }
            set
            {
                m_tabSelectedindex = value;
                RaisePropertyChanged("TabSelectedIndex");
            }
        }

        public List<B3IsGameEnabledSetting> B3SettingEnableDisable
        {
            get { return m_b3SettingEnableDisable; }
            set
            {
                if (value != null && value != m_b3SettingEnableDisable)
                {
                    m_b3SettingEnableDisable = value;
                    RaisePropertyChanged("B3SettingEnableDisable");
                }
            }
        }

        public List<GameSettingVmAllGame> GameSettingViewModels { get { return m_gameSettingViewModels; } }

        #endregion

        #region METHOD

        private B3IsGameEnabledSetting GetEnableDisableSettingValue(B3GameType gameType)
        {
            return m_b3SettingEnableDisable.Single(l => l.GameType == gameType);
        }

        public void ResetSettingsToDefault()
        {
            int tabSelectedIndex = m_tabSelectedindex;
            switch (tabSelectedIndex)
            {
                case 0:
                {
                    m_currentGameType = B3GameType.Crazybout;
                    GameCrzyBout.ResetSettingsToDefault();
                    SelectedGameVm = GameCrzyBout;
                    RaisePropertyChanged("GameCrzyBout");
                    break;
                }
                case 1:
                {
                    m_currentGameType = B3GameType.Jailbreak;
                    GameJailBreak.ResetSettingsToDefault();
                    SelectedGameVm = GameJailBreak;
                    RaisePropertyChanged("GameJailBreak");
                    break;
                }
                case 2:
                {
                    m_currentGameType = B3GameType.Mayamoney;
                    GameMayaMoney.ResetSettingsToDefault();
                    SelectedGameVm = GameMayaMoney;
                    RaisePropertyChanged("GameMayaMoney");
                    break;
                }
                case 3:
                {
                    m_currentGameType = B3GameType.Spirit76;
                    GameSpirit76.ResetSettingsToDefault();
                    SelectedGameVm = GameSpirit76;
                    RaisePropertyChanged("GameSpirit76");
                    break;
                }
                case 4:
                {
                    m_currentGameType = B3GameType.Timebomb;
                    GameTimeBomb.ResetSettingsToDefault();
                    SelectedGameVm = GameTimeBomb;
                    RaisePropertyChanged("GameTimeBomb");
                    break;
                }
                case 5:
                {
                    m_currentGameType = B3GameType.Ukickem;
                    GameUkickEm.ResetSettingsToDefault();
                    SelectedGameVm = GameUkickEm;
                    RaisePropertyChanged("GameUkickEm");
                    break;
                }
                case 6:
                {
                    m_currentGameType = B3GameType.Wildball;
                    GameWildBall.ResetSettingsToDefault();
                    SelectedGameVm = GameWildBall;
                    RaisePropertyChanged("GameWildBall");
                    break;
                }
                case 7:
                {
                    m_currentGameType = B3GameType.Wildfire;
                    GameWildfire.ResetSettingsToDefault();
                    SelectedGameVm = GameWildfire;
                    RaisePropertyChanged("GameWildfire");
                    break;
                }
            }      
            SettingViewModel.Instance.BtnSaveIsEnabled = SelectedGameVm.Settings.EnableGameSetting.IsEnabled;//Enable or disable button.
        }

        public int Myprevindex = -1;

        public void SelectedItemEvent()
        {
            SelectedGameVm = GetSelectedVm();
            ResetSettingsToDefault();
            Myprevindex = m_tabSelectedindex;
        }

        private GameSettingVmAllGame GetSelectedVm()
        {
            return m_gameSettingViewModels[m_tabSelectedindex];
        }


      
        #endregion
    }
}

