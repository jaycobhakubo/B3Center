﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTech.Elite.Base;
using System.Collections.ObjectModel;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
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

   public  class GameSettingVm : GameSettingTemplateVm
    {


        private GameSetting m_gameSettingModel;
        ObservableCollection<B3SettingGlobal> m_b3GameStting;
        GameSetting gs = new GameSetting();
        private int m_currentGameId;


        public GameSettingVm(ObservableCollection<B3SettingGlobal> _b3GameSetting)
        {
            m_b3GameStting = _b3GameSetting;
     
            GameCrzyBout = new GameSettingVmAllGame((ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameID == (int)B3Game.CRAZYBOUT)))), SetCurrentGameId((int)B3Game.CRAZYBOUT));

            GameJailBreak = new GameSettingVmAllGame((ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameID == (int)B3Game.JAILBREAK)))), SetCurrentGameId((int)B3Game.JAILBREAK));

            GameMayaMoney = new GameSettingVmAllGame((ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameID == (int)B3Game.MAYAMONEY)))), SetCurrentGameId((int)B3Game.MAYAMONEY));

            GameSpirit76 = new GameSettingVmAllGame((ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameID == (int)B3Game.SPIRIT76)))), SetCurrentGameId((int)B3Game.SPIRIT76));

            GameTimeBomb = new GameSettingVmAllGame((ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameID == (int)B3Game.TIMEBOMB)))), SetCurrentGameId((int)B3Game.TIMEBOMB));

            GameUkickEm = new GameSettingVmAllGame((ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameID == (int)B3Game.UKICKEM)))), SetCurrentGameId((int)B3Game.UKICKEM));

            GameWildBall = new GameSettingVmAllGame((ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameID == (int)B3Game.WILDBALL)))), SetCurrentGameId((int)B3Game.WILDBALL));

            GameWildfire = new GameSettingVmAllGame((ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameID == (int)B3Game.WILDFIRE)))), SetCurrentGameId((int)B3Game.WILDFIRE));    
        }

        public int SetCurrentGameId(int GameId)
        {
            m_currentGameId = GameId;
            return m_currentGameId;
        }

        public GameSettingVmAllGame GameCrzyBout 
        {
            get;
            set;        
        }

        public GameSettingVmAllGame GameJailBreak
        {
            get;
            set;
        }


        public GameSettingVmAllGame GameMayaMoney
        {
            get;
            set;
        }


        public GameSettingVmAllGame GameSpirit76
        {
            get;
            set;
        }


        public GameSettingVmAllGame GameTimeBomb
        {
            get;
            set;
        }


        public GameSettingVmAllGame GameUkickEm
        {
            get;
            set;
        }


        public GameSettingVmAllGame GameWildBall
        {
            get;
            set;
        }

        public GameSettingVmAllGame GameWildfire
        {
            get;
            set;
        }



        private GameSetting ConvertToModel(ObservableCollection<B3SettingGlobal> _b3Setting)
        {
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


            //CallSpeed var is being shuffle in db  thats why I have to do this
            if (m_currentGameId == (int)B3Game.CRAZYBOUT
                || m_currentGameId == (int)B3Game.MAYAMONEY)
            {
                gs.LCallSpeedMin = SystemSettingVm.Volume();
                gs.LCallSpeed = SystemSettingVm.Volume();
          
              
            }
            else if (m_currentGameId == (int)B3Game.JAILBREAK
                || m_currentGameId == (int)B3Game.UKICKEM
                || m_currentGameId == (int)B3Game.TIMEBOMB
                || m_currentGameId == (int)B3Game.WILDFIRE
                || m_currentGameId == (int)B3Game.WILDBALL)
            {
               gs.LCallSpeed = SystemSettingVm.Volume();          
            }        
            else if (m_currentGameId == (int)B3Game.SPIRIT76)
            {
                gs.LCallSpeed = SystemSettingVm.Volume();
                gs.LCallSpeedBonus = SystemSettingVm.Volume();
            }


            gs.LGamePayTable = SettingViewModel.Instance.GetB3MathGamePlay(m_currentGameId).ToList();

            return gs;
        }


      

    }
}
