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

        public GameSettingVm(ObservableCollection<B3SettingGlobal> _b3GameSetting)
        {
            m_b3GameStting = _b3GameSetting;
            GameCrzyBout = new GameSettingCrazyBoutVm(ConvertToModel (m_b3GameStting.Where(l => l.B3GameID == 1)));
        }


        private GameSettingCrazyBoutVm m_gameCrzyBout;
        public GameSettingCrazyBoutVm GameCrzyBout
        {
            get
            {
                return m_gameCrzyBout;
            }
            set
            {
                m_gameCrzyBout = value;
                RaisePropertyChanged("GameCrzyBout");
            }
        }


        private GameSetting ConvertToModel(IEnumerable<B3SettingGlobal> _b3Setting)
        {

            GameSetting gs = new GameSetting();

            foreach (B3SettingGlobal b3SettingGlobal_ in _b3Setting)
            {
                switch (b3SettingGlobal_.B3GameID)
                {
                    case ((int)B3SettingId.Denom1):
                        {
                            gs.Denom1 = b3SettingGlobal_.B3SettingValue;
                            break;
                        }
                    case ((int)B3SettingId.Denom5):
                        {
                            gs.Denom5 = b3SettingGlobal_.B3SettingValue;
                            break;
                        }
                    case ((int)B3SettingId.Denom10):
                        {
                            gs.Denom10 = b3SettingGlobal_.B3SettingValue;
                            break;
                        }
                    case ((int)B3SettingId.Denom25):
                        {
                            gs.Denom25 = b3SettingGlobal_.B3SettingValue;
                            break;
                        }
                    case ((int)B3SettingId.Denom50):
                        {
                            gs.Denom50 = b3SettingGlobal_.B3SettingValue;
                            break;
                        }
                    case ((int)B3SettingId.Denom100):
                        {
                            gs.Denom100 = b3SettingGlobal_.B3SettingValue;
                            break;
                        }
                    case ((int)B3SettingId.Denom200):
                        {
                            gs.Denom200 = b3SettingGlobal_.B3SettingValue;
                            break;
                        }
                    case ((int)B3SettingId.Denom500):
                        {
                            gs.Denom500 = b3SettingGlobal_.B3SettingValue;
                            break;
                        }
                    case ((int)B3SettingId.MaxBetLevel):
                        {
                            gs.MaxBetLevel = b3SettingGlobal_.B3SettingValue;
                            break;
                        }
                    case ((int)B3SettingId.MaxCards):
                        {
                            gs.MaxCards = b3SettingGlobal_.B3SettingValue;
                            break;
                        }
                    case ((int)B3SettingId.CallSpeed):
                        {
                            gs.CallSpeed = b3SettingGlobal_.B3SettingValue;
                            break;
                        }
                    case ((int)B3SettingId.AutoCall):
                        {
                            gs.AutoCall = (b3SettingGlobal_.B3SettingValue == "T")? true : false;
                            break;
                        }
                    case ((int)B3SettingId.AutoPlay):
                        {
                            gs.AutoPlay = (b3SettingGlobal_.B3SettingValue == "T")? true :false;
                            break;
                        }
                    case ((int)B3SettingId.HideSerialNumber):
                        {
                            gs.HideSerialNumber = (b3SettingGlobal_.B3SettingValue == "T") ? true : false;
                            break;

                        }
                    case ((int)B3SettingId.SingleOfferBonus):
                        {
                            gs.SingleOfferBonus = (b3SettingGlobal_.B3SettingValue == "T") ? true : false;
                            break;

                        }
                    case ((int)B3SettingId.MathPayTableSetting):
                        {
                            gs.MathPayTableSetting = b3SettingGlobal_.B3SettingValue;
                            break;

                        }
                    case ((int)B3SettingId.CallSpeedMin):
                        {
                            gs.CallSpeedMin = b3SettingGlobal_.B3SettingValue;
                            break;

                        }
                    case ((int)B3SettingId.CallSpeedBonus):
                        {
                            gs.CallSpeedBonus = b3SettingGlobal_.B3SettingValue;
                            break;

                        }
                }


            }

            return gs;
        }

      

    }
}
