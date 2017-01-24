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
