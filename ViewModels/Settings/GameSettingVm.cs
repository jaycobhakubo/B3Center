using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTech.Elite.Base;
using System.Collections.ObjectModel;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{

    #region (some) OBJECT
    enum B3Game
    {
        CRAZYBOUT = 1,
        JAILBREAK = 2,
        MAYAMONEY = 3,
        SPIRIT76 = 4,
        TIMEBOMB = 5,
        UKICKEM = 6,
        WILDBALL = 7,
        WILDFIRE = 8,
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
         
        private ObservableCollection<B3SettingGlobal> m_b3GameStting;
        private GameSetting gs = new GameSetting();
        private ObservableCollection<TabItem> GameTabItem { get; set; }
        private int m_currentGameId;

        #endregion
        #region CONSTRUCTOR
        public GameSettingVm(ObservableCollection<B3SettingGlobal> _b3GameSetting)
        {
            GameTabItem = new ObservableCollection<TabItem>();
            m_b3GameStting = _b3GameSetting;
            TabItem temp = new TabItem();
        

            m_currentGameId = (int)B3Game.CRAZYBOUT;
            GameCrzyBout = new GameSettingCrazyBoutVm_((ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameID == m_currentGameId)))), m_currentGameId);
            temp.GameIndex = 0;
            temp.ViewModel = GameCrzyBout;
            GameTabItem.Add(temp);

            temp = new TabItem();
            m_currentGameId = (int)B3Game.JAILBREAK;
            GameJailBreak = new GameSettingJailBreakVm((ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameID == m_currentGameId)))), m_currentGameId);
            temp.GameIndex = 1;
            temp.ViewModel = GameJailBreak;
            GameTabItem.Add(temp);

            temp = new TabItem();
            m_currentGameId = (int)B3Game.MAYAMONEY;
            GameMayaMoney = new GameSettingMayaMoneyVm((ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameID == m_currentGameId)))), m_currentGameId);
            temp.GameIndex = 2;
            temp.ViewModel = GameMayaMoney;
            GameTabItem.Add(temp);

            temp = new TabItem();
            m_currentGameId = (int)B3Game.SPIRIT76;
            GameSpirit76 = new GameSettingSpirit76Vm((ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameID == m_currentGameId)))), m_currentGameId);
            temp.GameIndex = 3;
            temp.ViewModel = GameSpirit76;
            GameTabItem.Add(temp);

            temp = new TabItem();
            m_currentGameId = (int)B3Game.SPIRIT76;
            GameTimeBomb = new GameSettingTimeBombVm((ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameID == m_currentGameId)))), m_currentGameId);
            temp.GameIndex = 4;
            temp.ViewModel = GameTimeBomb;
            GameTabItem.Add(temp);

            temp = new TabItem();
            m_currentGameId = (int)B3Game.UKICKEM;
            GameUkickEm = new GameSettingGameUkickEmVm((ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameID == m_currentGameId)))), m_currentGameId);
            temp.GameIndex = 5;
            temp.ViewModel = GameUkickEm;
            GameTabItem.Add(temp);

            temp = new TabItem();
            m_currentGameId = (int)B3Game.WILDBALL;
            GameWildBall = new GameSettingGameWildBallVm((ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameID == m_currentGameId)))), m_currentGameId);
            temp.GameIndex = 6;
            temp.ViewModel = GameWildBall;
            GameTabItem.Add(temp);

            temp = new TabItem();
            m_currentGameId = (int)B3Game.WILDFIRE;
            GameWildfire = new GameSettingGameWildfireVm((ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameID == m_currentGameId)))), m_currentGameId);
            temp.GameIndex = 7;
            temp.ViewModel = GameWildfire;
            GameTabItem.Add(temp);
        }
        #endregion
        #region PROPERTIES

   

        public GameSettingCrazyBoutVm_ GameCrzyBout { get; set; }
        public GameSettingJailBreakVm GameJailBreak { get; set; }
        public GameSettingMayaMoneyVm GameMayaMoney { get; set; }
        public GameSettingSpirit76Vm GameSpirit76 { get; set; }
        public GameSettingTimeBombVm GameTimeBomb { get; set; }
        public GameSettingGameUkickEmVm GameUkickEm { get; set; }
        public GameSettingGameWildBallVm GameWildBall { get; set; }
        public GameSettingGameWildfireVm GameWildfire { get; set; }

        private int m_tabSelectedindex;
        public int TabSelectedIndex
        {
            get { return m_tabSelectedindex; }
            set
            {
                m_tabSelectedindex = value;
                RaisePropertyChanged("TabSelectedIndex");
            }
        }

        #endregion
        #region METHOD

        public void SelectedItemEvent()
        {
            SelectedGameVm = GetSelectedVm();
        }

        private GameSettingVmAllGame GetSelectedVm()
        {
            return GameTabItem[m_tabSelectedindex].ViewModel;
        }

        public GameSettingVmAllGame SelectedGameVm
        {
            get;
            set;
        }

        private GameSetting ConvertToModel(ObservableCollection<B3SettingGlobal> _b3Setting)
        {
            gs = new GameSetting();
            gs.GameId = m_currentGameId;

            foreach (B3SettingGlobal b3SettingGlobal_ in _b3Setting)
            {


                if ((int)B3SettingId.Denom1 == b3SettingGlobal_.B3SettingID)
                {
                    gs.Denom1 = b3SettingGlobal_.B3SettingValue;
                }
                if
                ((int)B3SettingId.Denom5 == b3SettingGlobal_.B3SettingID)
                {
                    gs.Denom5 = b3SettingGlobal_.B3SettingValue;

                }
                else
                if
                 ((int)B3SettingId.Denom10 == b3SettingGlobal_.B3SettingID)
                {
                    gs.Denom10 = b3SettingGlobal_.B3SettingValue;

                }
                else
                if
                 ((int)B3SettingId.Denom25 == b3SettingGlobal_.B3SettingID)
                {
                    gs.Denom25 = b3SettingGlobal_.B3SettingValue;

                }
                else if
                   ((int)B3SettingId.Denom50 == b3SettingGlobal_.B3SettingID)
                {
                    gs.Denom50 = b3SettingGlobal_.B3SettingValue;

                }
                else if ((int)B3SettingId.Denom100 == b3SettingGlobal_.B3SettingID)
                {
                    gs.Denom100 = b3SettingGlobal_.B3SettingValue;

                }
                else if ((int)B3SettingId.Denom200 == b3SettingGlobal_.B3SettingID)
                {
                    gs.Denom200 = b3SettingGlobal_.B3SettingValue;

                }
                else if ((int)B3SettingId.Denom500 == b3SettingGlobal_.B3SettingID)
                {
                    gs.Denom500 = b3SettingGlobal_.B3SettingValue;

                }
                else if ((int)B3SettingId.MaxBetLevel == b3SettingGlobal_.B3SettingID)
                {
                    gs.MaxBetLevel = b3SettingGlobal_.B3SettingValue;
                    gs.LMaxCards = SystemSettingVm.MaxCard();
                }
                else if ((int)B3SettingId.MaxCards == b3SettingGlobal_.B3SettingID)
                {
                    gs.MaxCards = b3SettingGlobal_.B3SettingValue;
                    gs.LMaxBetLevel = SystemSettingVm.BetLevel();
                }
                else if ((int)B3SettingId.CallSpeed == b3SettingGlobal_.B3SettingID)
                {
                    gs.CallSpeed = b3SettingGlobal_.B3SettingValue;
                  
                }
                else if ((int)B3SettingId.AutoCall == b3SettingGlobal_.B3SettingID)
                {
                    gs.AutoCall = (b3SettingGlobal_.B3SettingValue == "T") ? true : false;

                }
                else if ((int)B3SettingId.AutoPlay == b3SettingGlobal_.B3SettingID)
                {
                    gs.AutoPlay = (b3SettingGlobal_.B3SettingValue == "T") ? true : false;

                }
                else if ((int)B3SettingId.HideSerialNumber == b3SettingGlobal_.B3SettingID)
                {
                    gs.HideSerialNumber = (b3SettingGlobal_.B3SettingValue == "T") ? true : false;


                }
                else if ((int)B3SettingId.SingleOfferBonus == b3SettingGlobal_.B3SettingID)
                {
                    gs.SingleOfferBonus = (b3SettingGlobal_.B3SettingValue == "T") ? true : false;


                }
                else if ((int)B3SettingId.MathPayTableSetting == b3SettingGlobal_.B3SettingID)
                {
                    gs.MathPayTableSetting = b3SettingGlobal_.B3SettingValue;


                }
                else if ((int)B3SettingId.CallSpeedMin == b3SettingGlobal_.B3SettingID)
                {
                    gs.CallSpeedMin = b3SettingGlobal_.B3SettingValue;
                 
                }
                else if ((int)B3SettingId.CallSpeedBonus == b3SettingGlobal_.B3SettingID)
                {
                    gs.CallSpeedBonus = b3SettingGlobal_.B3SettingValue;

                }

            }

            if (m_currentGameId == (int)B3Game.CRAZYBOUT
                || m_currentGameId == (int)B3Game.MAYAMONEY)
            {
                gs.LCallSpeedMin = SettingViewModel.Volume();
                gs.LCallSpeed = SettingViewModel.Volume();                    
            }
            else if (m_currentGameId == (int)B3Game.JAILBREAK
                || m_currentGameId == (int)B3Game.UKICKEM
                || m_currentGameId == (int)B3Game.TIMEBOMB
                || m_currentGameId == (int)B3Game.WILDFIRE
                || m_currentGameId == (int)B3Game.WILDBALL)
            {
               gs.LCallSpeed = SettingViewModel.Volume();          
            }        
            else if (m_currentGameId == (int)B3Game.SPIRIT76)
            {
                gs.LCallSpeed = SettingViewModel.Volume();
                gs.LCallSpeedBonus = SettingViewModel.Volume();
            }

            gs.LGamePayTable = SettingViewModel.Instance.GetB3MathGamePlay(m_currentGameId).ToList();

            return gs;
        }

        #endregion


    }
}


#region SCRATCH PAPER
//public GameSettingCrazyBoutVm_ GameCrzyBout
//{ get { return (GameSettingCrazyBoutVm_)GameTabItem[0].ViewModel; }
//    set
//    {
//        GameTabItem[0].ViewModel = value;
//        RaisePropertyChanged("GameCrzyBout");
//    }
//}

//public GameSettingVmAllGame GameJailBreak
//{
//    get { return GameTabItem[1].ViewModel; }
//    set
//    {
//        GameTabItem[1].ViewModel = value;
//        RaisePropertyChanged("GameJailBreak");
//    }
//}

//public GameSettingVmAllGame GameMayaMoney
//{
//    get { return GameTabItem[2].ViewModel; }
//    set
//    {
//        GameTabItem[2].ViewModel = value;
//        RaisePropertyChanged("GameMayaMoney");
//    }
//}


//public GameSettingVmAllGame GameSpirit76
//{
//    get { return GameTabItem[3].ViewModel; }
//    set
//    {
//        GameTabItem[3].ViewModel = value;
//        RaisePropertyChanged("GameSpirit76");
//    }
//}

//public GameSettingVmAllGame GameTimeBomb
//{
//    get { return GameTabItem[4].ViewModel; }
//    set
//    {
//        GameTabItem[4].ViewModel = value;
//        RaisePropertyChanged("GameTimeBomb");
//    }
//}

//public GameSettingVmAllGame GameUkickEm
//{
//    get { return GameTabItem[5].ViewModel; }
//    set
//    {
//        GameTabItem[5].ViewModel = value;
//        RaisePropertyChanged("GameUkickEm");
//    }
//}

//public GameSettingVmAllGame GameWildBall
//{
//    get { return GameTabItem[6].ViewModel; }
//    set
//    {
//        GameTabItem[6].ViewModel = value;
//        RaisePropertyChanged("GameWildBall");
//    }
//}

//public GameSettingVmAllGame GameWildfire
//{
//    get { return GameTabItem[7].ViewModel; }
//    set
//    {
//        GameTabItem[7].ViewModel = value;
//        RaisePropertyChanged("GameWildfire");
//    }
//}
#endregion