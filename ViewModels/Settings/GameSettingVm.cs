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
   public  class GameSettingVm : GameSettingTemplateVm
    {


        private GameSetting m_gameSettingModel;
        ObservableCollection<B3SettingGlobal> m_b3GameStting;
        GameSetting gs = new GameSetting();
        public GameSettingVm(ObservableCollection<B3SettingGlobal> _b3GameSetting)
        {
            m_b3GameStting = _b3GameSetting;
            GameCrzyBout = new GameSettingCrazyBoutVm((ConvertToModel(new ObservableCollection<B3SettingGlobal>(m_b3GameStting.Where(l => l.B3GameID == 1)))));
        }


        private GameSettingCrazyBoutVm m_gameCrzyBout;
        public GameSettingCrazyBoutVm GameCrzyBout
        {
            get;set;          
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

                }
                else if ((int)B3SettingId.MaxCards == b3SettingGlobal_.B3SettingID)
                {
                    gs.MaxCards = b3SettingGlobal_.B3SettingValue;

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
            return gs;
        }


      

    }
}
