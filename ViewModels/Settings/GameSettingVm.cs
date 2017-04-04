using System;
using System.Linq;
using GameTech.Elite.Base;
using System.Collections.ObjectModel;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{

    #region (some) OBJECT
    enum B3Game
    {
        Crazybout = 1,
        Jailbreak = 2,
        Mayamoney = 3,
        Spirit76 = 4,
        Timebomb = 5,
        Ukickem = 6,
        Wildball = 7,
        Wildfire = 8,
    }

    public  class TabItem
    {
        public int GameIndex{ get; set; }
        public GameSettingVmAllGame ViewModel { get; set; }
    }



    #endregion

    public class GameSettingVm : ViewModelBase
    {
        #region MEMBER

        private readonly ObservableCollection<B3SettingGlobal> m_b3GameStting;     
        private GameSetting m_gs = new GameSetting();
        private readonly ObservableCollection<TabItem> m_gameTabItem;
        private int m_currentGameId;
        private int m_tabSelectedindex;
        private ObservableCollection<B3GameSetting> m_b3SettingEnableDisable;

        #endregion
        #region CONSTRUCTOR

        public GameSettingVm(ObservableCollection<B3SettingGlobal> b3GameSetting, ObservableCollection<B3GameSetting> gameEnableDisableSetting)
        {
            m_gameTabItem = new ObservableCollection<TabItem>();
            m_b3SettingEnableDisable = gameEnableDisableSetting;
            m_b3GameStting = b3GameSetting;
            TabItem temp = new TabItem();

            m_currentGameId = (int)B3Game.Crazybout;
            GameCrzyBout = new GameSettingVmAllGame(ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameId == m_currentGameId))));
            temp.GameIndex = 0;
            temp.ViewModel = GameCrzyBout;
            m_gameTabItem.Add(temp);

            temp = new TabItem();
            m_currentGameId = (int)B3Game.Jailbreak;
            GameJailBreak = new GameSettingVmAllGame(ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameId == m_currentGameId))));
            temp.GameIndex = 1;
            temp.ViewModel = GameJailBreak;
            m_gameTabItem.Add(temp);

            temp = new TabItem();
            m_currentGameId = (int)B3Game.Mayamoney;
            GameMayaMoney = new GameSettingVmAllGame(ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameId == m_currentGameId))));
            temp.GameIndex = 2;
            temp.ViewModel = GameMayaMoney;
            m_gameTabItem.Add(temp);

            temp = new TabItem();
            m_currentGameId = (int)B3Game.Spirit76;
            GameSpirit76 = new GameSettingVmAllGame(ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameId == m_currentGameId))));
            temp.GameIndex = 3;
            temp.ViewModel = GameSpirit76;
            m_gameTabItem.Add(temp);

            temp = new TabItem();
            m_currentGameId = (int)B3Game.Timebomb;
            GameTimeBomb = new GameSettingVmAllGame(ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameId == m_currentGameId))));
            temp.GameIndex = 4;
            temp.ViewModel = GameTimeBomb;
            m_gameTabItem.Add(temp);

            temp = new TabItem();
            m_currentGameId = (int)B3Game.Ukickem;
            GameUkickEm = new GameSettingVmAllGame(ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameId == m_currentGameId))));
            temp.GameIndex = 5;
            temp.ViewModel = GameUkickEm;
            m_gameTabItem.Add(temp);

            temp = new TabItem();
            m_currentGameId = (int)B3Game.Wildball;
            GameWildBall = new GameSettingVmAllGame(ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameId == m_currentGameId))));
            temp.GameIndex = 6;
            temp.ViewModel = GameWildBall;
            m_gameTabItem.Add(temp);

            temp = new TabItem();
            m_currentGameId = (int)B3Game.Wildfire;
            GameWildfire = new GameSettingVmAllGame(ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameId == m_currentGameId))));
            temp.GameIndex = 7;
            temp.ViewModel = GameWildfire;
            m_gameTabItem.Add(temp);

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

       

        private B3GameSetting GetEnableDisableSettingValue(int gameId)
        {
            return  m_b3SettingEnableDisable.Single(l => l.GameId == gameId);
        }

        public void ReloadSelectedItemForAnyChangesNotSaved()
        {
            int tabSelectedIndex = m_tabSelectedindex;
            switch (tabSelectedIndex)
            {
                case 0:
                    {
                        m_currentGameId = (int)B3Game.Crazybout;
                        GameCrzyBout = new GameSettingVmAllGame(ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameId == m_currentGameId))));
                        SelectedGameVm = GameCrzyBout;
                        RaisePropertyChanged("GameCrzyBout");
                        break;
                    }
                case 1:
                    {
                        m_currentGameId = (int)B3Game.Jailbreak;
                        GameJailBreak = new GameSettingVmAllGame(ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameId == m_currentGameId))));
                        SelectedGameVm = GameJailBreak;
                        RaisePropertyChanged("GameJailBreak");
                        break;
                    }
                case 2:
                    {
                        m_currentGameId = (int)B3Game.Mayamoney;
                        GameMayaMoney = new GameSettingVmAllGame(ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameId == m_currentGameId))));
                        SelectedGameVm = GameMayaMoney;
                        RaisePropertyChanged("GameMayaMoney");
                        break;
                    }
                case 3:
                    {
                        m_currentGameId = (int)B3Game.Spirit76;
                        GameSpirit76 = new GameSettingVmAllGame(ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameId == m_currentGameId))));
                        SelectedGameVm = GameSpirit76;
                        RaisePropertyChanged("GameSpirit76");
                        break;
                    }
                case 4:
                    {
                        m_currentGameId = (int)B3Game.Timebomb;
                        GameTimeBomb = new GameSettingVmAllGame(ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameId == m_currentGameId))));
                        SelectedGameVm = GameTimeBomb;
                        RaisePropertyChanged("GameTimeBomb");
                        break;
                    }
                case 5:
                    {
                        m_currentGameId = (int)B3Game.Ukickem;
                        GameUkickEm = new GameSettingVmAllGame(ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameId == m_currentGameId))));
                        SelectedGameVm = GameUkickEm;
                        RaisePropertyChanged("GameUkickEm");
                        break;
                    }
                case 6:
                    {
                        m_currentGameId = (int)B3Game.Wildball;
                        GameWildBall = new GameSettingVmAllGame(ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameId == m_currentGameId))));
                        SelectedGameVm = GameWildBall;
                        RaisePropertyChanged("GameWildBall");
                        break;
                    }
                case 7:
                    {
                        m_currentGameId = (int)B3Game.Wildfire;
                        GameWildfire = new GameSettingVmAllGame(ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameId == m_currentGameId))));
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
            return m_gameTabItem[m_tabSelectedindex].ViewModel;
        }

        private GameSetting ConvertToModel(ObservableCollection<B3SettingGlobal> b3Setting)
        {
            m_gs = new GameSetting {GameId = m_currentGameId};

            foreach (B3SettingGlobal b3SettingGlobal in b3Setting)
            {
                if ((int)B3SettingId.Denom1 == b3SettingGlobal.B3SettingId)
                {
                    m_gs.Denom1 = b3SettingGlobal.B3SettingValue;
                }
                else
                if
                ((int)B3SettingId.Denom5 == b3SettingGlobal.B3SettingId)
                {
                    m_gs.Denom5 = b3SettingGlobal.B3SettingValue;
                }
                else
                if
                 ((int)B3SettingId.Denom10 == b3SettingGlobal.B3SettingId)
                {
                    m_gs.Denom10 = b3SettingGlobal.B3SettingValue;
                }
                else
                if
                 ((int)B3SettingId.Denom25 == b3SettingGlobal.B3SettingId)
                {
                    m_gs.Denom25 = b3SettingGlobal.B3SettingValue;
                }
                else if
                   ((int)B3SettingId.Denom50 == b3SettingGlobal.B3SettingId)
                {
                    m_gs.Denom50 = b3SettingGlobal.B3SettingValue;

                }
                else if ((int)B3SettingId.Denom100 == b3SettingGlobal.B3SettingId)
                {
                    m_gs.Denom100 = b3SettingGlobal.B3SettingValue;

                }
                else if ((int)B3SettingId.Denom200 == b3SettingGlobal.B3SettingId)
                {
                    m_gs.Denom200 = b3SettingGlobal.B3SettingValue;

                }
                else if ((int)B3SettingId.Denom500 == b3SettingGlobal.B3SettingId)
                {
                    m_gs.Denom500 = b3SettingGlobal.B3SettingValue;

                }
                else if ((int)B3SettingId.MaxBetLevel == b3SettingGlobal.B3SettingId)
                {
                    m_gs.MaxBetLevel = b3SettingGlobal.B3SettingValue;
                    m_gs.LMaxBetLevel = SystemSettingVm.BetLevel();
                   
                }
                else if ((int)B3SettingId.MaxCards == b3SettingGlobal.B3SettingId)
                {
                    m_gs.MaxCards = b3SettingGlobal.B3SettingValue;
                    //gs.LMaxBetLevel = SystemSettingVm.BetLevel();
                    m_gs.LMaxCards = SystemSettingVm.MaxCard();
                }
                else if ((int)B3SettingId.CallSpeed == b3SettingGlobal.B3SettingId)
                {
                    m_gs.CallSpeed = b3SettingGlobal.B3SettingValue;
                }
                else if ((int)B3SettingId.AutoCall == b3SettingGlobal.B3SettingId)
                {
                    m_gs.AutoCall = b3SettingGlobal.B3SettingValue == "T";
                }
                else if ((int)B3SettingId.AutoPlay == b3SettingGlobal.B3SettingId)
                {
                    m_gs.AutoPlay = b3SettingGlobal.B3SettingValue == "T";
                }
                else if ((int)B3SettingId.HideSerialNumber == b3SettingGlobal.B3SettingId)
                {
                    m_gs.HideSerialNumber = b3SettingGlobal.B3SettingValue == "T";
                }
                else if ((int)B3SettingId.SingleOfferBonus == b3SettingGlobal.B3SettingId)
                {
                    m_gs.SingleOfferBonus = b3SettingGlobal.B3SettingValue == "T";
                }
                else if ((int)B3SettingId.MathPayTableSetting == b3SettingGlobal.B3SettingId)
                {
                    m_gs.SelectedMathPayTableSettingInt = Convert.ToInt32(b3SettingGlobal.B3SettingValue);
                }
                else if ((int)B3SettingId.CallSpeedMin == b3SettingGlobal.B3SettingId)
                {
                    m_gs.CallSpeedMin = b3SettingGlobal.B3SettingValue;              
                }
                else if ((int)B3SettingId.CallSpeedBonus == b3SettingGlobal.B3SettingId)
                {
                    m_gs.CallSpeedBonus = b3SettingGlobal.B3SettingValue;
                }
            }

            if (m_currentGameId == (int)B3Game.Crazybout
                || m_currentGameId == (int)B3Game.Mayamoney)
            {
                m_gs.LCallSpeedMin = SettingViewModel.BetLevelOrSpeedVal();
                m_gs.LCallSpeed = SettingViewModel.BetLevelOrSpeedVal();            
            }
            else if (m_currentGameId == (int)B3Game.Jailbreak
                || m_currentGameId == (int)B3Game.Ukickem
                || m_currentGameId == (int)B3Game.Timebomb
                || m_currentGameId == (int)B3Game.Wildfire
                || m_currentGameId == (int)B3Game.Wildball)
            {
               m_gs.LCallSpeed = SettingViewModel.Volume();          
            }        
            else if (m_currentGameId == (int)B3Game.Spirit76)
            {
                m_gs.LCallSpeed = SettingViewModel.BetLevelOrSpeedVal();
                m_gs.LCallSpeedBonus = SettingViewModel.BetLevelOrSpeedVal();
            }

            m_gs.IsEnableGame = GetEnableDisableSettingValue(m_currentGameId);
            m_gs.LGamePayTable = SettingViewModel.Instance.GetB3MathGamePlay(m_currentGameId).ToList();

            return m_gs;
        }

        #endregion
    }
}

