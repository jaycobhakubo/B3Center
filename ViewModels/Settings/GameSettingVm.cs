using System;
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

        private readonly ObservableCollection<B3SettingGlobal> m_b3GameStting;
        private GameSetting m_gs = new GameSetting();
        private readonly ObservableCollection<GameSettingVmAllGame> m_gameSettingViewModels;
        private B3GameType m_currentGameType;
        private int m_tabSelectedindex;
        private ObservableCollection<B3GameSetting> m_b3SettingEnableDisable;

        #endregion
        #region CONSTRUCTOR

        public GameSettingVm(ObservableCollection<B3SettingGlobal> b3GameSetting, ObservableCollection<B3GameSetting> gameEnableDisableSetting)
        {
            m_gameSettingViewModels = new ObservableCollection<GameSettingVmAllGame>();
            m_b3SettingEnableDisable = gameEnableDisableSetting;
            m_b3GameStting = b3GameSetting;

            m_currentGameType = B3GameType.Crazybout;
            var gameSettings = ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.GameType == m_currentGameType)));
            GameCrzyBout = new GameSettingVmAllGame(gameSettings, m_currentGameType);
            m_gameSettingViewModels.Add(GameCrzyBout);

            m_currentGameType = B3GameType.Jailbreak;
            gameSettings = ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.GameType == m_currentGameType)));
            GameJailBreak = new GameSettingVmAllGame(gameSettings, m_currentGameType);
            m_gameSettingViewModels.Add(GameJailBreak);

            m_currentGameType = B3GameType.Mayamoney;
            gameSettings = ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.GameType == m_currentGameType)));
            GameMayaMoney = new GameSettingVmAllGame(gameSettings, m_currentGameType);
            m_gameSettingViewModels.Add(GameMayaMoney);

            m_currentGameType = B3GameType.Spirit76;
            gameSettings = ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.GameType == m_currentGameType)));
            GameSpirit76 = new GameSettingVmAllGame(gameSettings, m_currentGameType);
            m_gameSettingViewModels.Add(GameSpirit76);

            m_currentGameType = B3GameType.Timebomb;
            gameSettings = ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.GameType == m_currentGameType)));
            GameTimeBomb = new GameSettingVmAllGame(gameSettings, m_currentGameType);
            m_gameSettingViewModels.Add(GameTimeBomb);

            m_currentGameType = B3GameType.Ukickem;
            gameSettings = ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.GameType == m_currentGameType)));
            GameUkickEm = new GameSettingVmAllGame(gameSettings, m_currentGameType);
            m_gameSettingViewModels.Add(GameUkickEm);

            m_currentGameType = B3GameType.Wildball;
            gameSettings = ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.GameType == m_currentGameType)));
            GameWildBall = new GameSettingVmAllGame(gameSettings, m_currentGameType);
            m_gameSettingViewModels.Add(GameWildBall);

            m_currentGameType = B3GameType.Wildfire;
            gameSettings = ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.GameType == m_currentGameType)));
            GameWildfire = new GameSettingVmAllGame(gameSettings, m_currentGameType);
            m_gameSettingViewModels.Add(GameWildfire);

            SelectedGameVm = GameCrzyBout;
        }
        #endregion
        #region PROPERTIES

        public GameSettingVmAllGame SelectedGameVm
        {
            get;
            set;
        }

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

        public ObservableCollection<B3GameSetting> B3SettingEnableDisable
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

        #endregion
        #region METHOD



        private B3GameSetting GetEnableDisableSettingValue(B3GameType gameId)
        {
            return m_b3SettingEnableDisable.Single(l => l.GameType == gameId);
        }

        public void ReloadSelectedItemForAnyChangesNotSaved()
        {
            int tabSelectedIndex = m_tabSelectedindex;
            switch (tabSelectedIndex)
            {
                case 0:
                    {
                        m_currentGameType = B3GameType.Crazybout;
                        var gameSettings = ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.GameType == m_currentGameType)));
                        GameCrzyBout = new GameSettingVmAllGame(gameSettings, m_currentGameType);
                        SelectedGameVm = GameCrzyBout;
                        RaisePropertyChanged("GameCrzyBout");
                        break;
                    }
                case 1:
                    {
                        m_currentGameType = B3GameType.Jailbreak;
                        var gameSettings = ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.GameType == m_currentGameType)));
                        GameJailBreak = new GameSettingVmAllGame(gameSettings, m_currentGameType);
                        SelectedGameVm = GameJailBreak;
                        RaisePropertyChanged("GameJailBreak");
                        break;
                    }
                case 2:
                    {
                        m_currentGameType = B3GameType.Mayamoney;
                        var gameSettings = ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.GameType == m_currentGameType)));
                        GameMayaMoney = new GameSettingVmAllGame(gameSettings, m_currentGameType);
                        SelectedGameVm = GameMayaMoney;
                        RaisePropertyChanged("GameMayaMoney");
                        break;
                    }
                case 3:
                    {
                        m_currentGameType = B3GameType.Spirit76; 
                        var gameSettings = ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.GameType == m_currentGameType)));
                        GameSpirit76 = new GameSettingVmAllGame(gameSettings, m_currentGameType);
                        SelectedGameVm = GameSpirit76;
                        RaisePropertyChanged("GameSpirit76");
                        break;
                    }
                case 4:
                    {
                        m_currentGameType = B3GameType.Timebomb;
                        var gameSettings = ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.GameType == m_currentGameType)));
                        GameTimeBomb = new GameSettingVmAllGame(gameSettings, m_currentGameType);
                        SelectedGameVm = GameTimeBomb;
                        RaisePropertyChanged("GameTimeBomb");
                        break;
                    }
                case 5:
                    {
                        m_currentGameType = B3GameType.Ukickem;
                        var gameSettings = ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.GameType == m_currentGameType)));
                        GameUkickEm = new GameSettingVmAllGame(gameSettings, m_currentGameType);
                        SelectedGameVm = GameUkickEm;
                        RaisePropertyChanged("GameUkickEm");
                        break;
                    }
                case 6:
                    {
                        m_currentGameType = B3GameType.Wildball;
                        var gameSettings = ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.GameType == m_currentGameType)));
                        GameWildBall = new GameSettingVmAllGame(gameSettings, m_currentGameType); 
                        SelectedGameVm = GameWildBall;
                        RaisePropertyChanged("GameWildBall");
                        break;
                    }
                case 7:
                    {
                        m_currentGameType = B3GameType.Wildfire;
                        var gameSettings = ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.GameType == m_currentGameType)));
                        GameWildfire = new GameSettingVmAllGame(gameSettings, m_currentGameType);
                        SelectedGameVm = GameWildfire;
                        RaisePropertyChanged("GameWildfire");
                        break;
                    }
            }
        }

        public int Myprevindex = -1;

        public void SelectedItemEvent()
        {
            SelectedGameVm = GetSelectedVm();
            ReloadSelectedItemForAnyChangesNotSaved();
            Myprevindex = m_tabSelectedindex;
        }

        private GameSettingVmAllGame GetSelectedVm()
        {
            return m_gameSettingViewModels[m_tabSelectedindex];
        }

        private GameSetting ConvertToModel(ObservableCollection<B3SettingGlobal> b3Setting)
        {
            m_gs = new GameSetting { GameType = m_currentGameType };

            foreach (B3SettingGlobal b3SettingGlobal in b3Setting)
            {
                if (B3SettingType.Denom1 == b3SettingGlobal.SettingType)
                {
                    m_gs.Denom1 = b3SettingGlobal.B3SettingValue;
                }
                else if (B3SettingType.Denom5 == b3SettingGlobal.SettingType)
                {
                    m_gs.Denom5 = b3SettingGlobal.B3SettingValue;
                }
                else if (B3SettingType.Denom10 == b3SettingGlobal.SettingType)
                {
                    m_gs.Denom10 = b3SettingGlobal.B3SettingValue;
                }
                else if (B3SettingType.Denom25 == b3SettingGlobal.SettingType)
                {
                    m_gs.Denom25 = b3SettingGlobal.B3SettingValue;
                }
                else if
                   (B3SettingType.Denom50 == b3SettingGlobal.SettingType)
                {
                    m_gs.Denom50 = b3SettingGlobal.B3SettingValue;

                }
                else if (B3SettingType.Denom100 == b3SettingGlobal.SettingType)
                {
                    m_gs.Denom100 = b3SettingGlobal.B3SettingValue;

                }
                else if (B3SettingType.Denom200 == b3SettingGlobal.SettingType)
                {
                    m_gs.Denom200 = b3SettingGlobal.B3SettingValue;

                }
                else if (B3SettingType.Denom500 == b3SettingGlobal.SettingType)
                {
                    m_gs.Denom500 = b3SettingGlobal.B3SettingValue;

                }
                else if (B3SettingType.MaxBetLevel == b3SettingGlobal.SettingType)
                {
                    m_gs.MaxBetLevel = b3SettingGlobal.B3SettingValue;
                    //m_gs.LMaxBetLevel = SystemSettingVm.BetLevel();

                }
                else if (B3SettingType.MaxCards == b3SettingGlobal.SettingType)
                {
                    m_gs.MaxCards = b3SettingGlobal.B3SettingValue;
                    //gs.LMaxBetLevel = SystemSettingVm.BetLevel();
                    //m_gs.LMaxCards = SystemSettingVm.MaxCard();
                }
                else if (B3SettingType.CallSpeed == b3SettingGlobal.SettingType)
                {
                    m_gs.CallSpeed = b3SettingGlobal.B3SettingValue;
                }
                else if (B3SettingType.AutoCall == b3SettingGlobal.SettingType)
                {
                    m_gs.AutoCall = b3SettingGlobal.ConvertB3StringValueToBool();
                }
                else if (B3SettingType.AutoPlay == b3SettingGlobal.SettingType)
                {
                    m_gs.AutoPlay = b3SettingGlobal.ConvertB3StringValueToBool();
                }
                else if (B3SettingType.HideSerialNumber == b3SettingGlobal.SettingType)
                {
                    m_gs.HideSerialNumber = b3SettingGlobal.ConvertB3StringValueToBool();
                }
                else if (B3SettingType.SingleOfferBonus == b3SettingGlobal.SettingType)
                {
                    m_gs.SingleOfferBonus = b3SettingGlobal.ConvertB3StringValueToBool();
                }
                else if (B3SettingType.MathPayTableSetting == b3SettingGlobal.SettingType)
                {
                    m_gs.SelectedMathPayTableSettingInt = Convert.ToInt32(b3SettingGlobal.B3SettingValue);
                }
                else if (B3SettingType.CallSpeedMin == b3SettingGlobal.SettingType)
                {
                    m_gs.CallSpeedMin = b3SettingGlobal.B3SettingValue;
                }
                else if (B3SettingType.CallSpeedBonus == b3SettingGlobal.SettingType)
                {
                    m_gs.CallSpeedBonus = b3SettingGlobal.B3SettingValue;
                }
            }

            //if (m_currentGameType == B3GameType.Crazybout
            //    || m_currentGameType == B3GameType.Mayamoney)
            //{
            //    m_gs.LCallSpeedMin = SettingViewModel.OneToTenList();
            //    m_gs.LCallSpeed = SettingViewModel.OneToTenList();
            //}
            //else if (m_currentGameType == B3GameType.Jailbreak
            //    || m_currentGameType == B3GameType.Ukickem
            //    || m_currentGameType == B3GameType.Timebomb
            //    || m_currentGameType == B3GameType.Wildfire
            //    || m_currentGameType == B3GameType.Wildball)
            //{
            //    m_gs.LCallSpeed = SettingViewModel.OneToTenList();
            //}
            //else if (m_currentGameType == B3GameType.Spirit76)
            //{
            //    m_gs.LCallSpeed = SettingViewModel.OneToTenList();
            //    m_gs.LCallSpeedBonus = SettingViewModel.OneToTenList();
            //}

            m_gs.IsEnableGame = GetEnableDisableSettingValue(m_currentGameType);
            m_gs.LGamePayTable = SettingViewModel.Instance.GetB3MathGamePlay(m_currentGameType).ToList();

            return m_gs;
        }

        #endregion
    }
}

