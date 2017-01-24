using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
    public class GameSettingCrazyBoutVm : GameSettingTemplateVm
    {
        public GameSettingCrazyBoutVm(GameSetting _gameSettingCrazyBout)
        {
            GamesettingCrzyBt_ = _gameSettingCrazyBout;
            textbxtest = "HELLO";
            //GamesettingCrzyBt_.AutoPlay;
            //GamesettingCrzyBt_.HideSerialNumber;
            //GamesettingCrzyBt_.SingleOfferBonus;
            //GamesettingCrzyBt_.Denom1;
            //GamesettingCrzyBt_.Denom10;
            //GamesettingCrzyBt_.Denom5;
            //GamesettingCrzyBt_.Denom25;
            //GamesettingCrzyBt_.Denom50;
            //GamesettingCrzyBt_.Denom100;
            //GamesettingCrzyBt_.Denom200;
            //GamesettingCrzyBt_.Denom500;
        }

        private GameSetting m_gameSettingCrzyBt;
        public GameSetting GamesettingCrzyBt_
        {
            get { return m_gameSettingCrzyBt; }
            set
            {
                m_gameSettingCrzyBt = value;
                RaisePropertyChanged("GamesettingCrzyBt_");
            }
        }

        string tt;
        public string textbxtest
        {
            get { return tt; }
            set
            {
                tt = value;
                RaisePropertyChanged("textbxtest");
            }
        }



    }
}
