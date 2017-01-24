using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
    public class GameSettingTimeBombVm : GameSettingTemplateVm
    {
        public GameSettingTimeBombVm(GameSetting _gameSetting)
        {
            Gamesetting_ = _gameSetting;
        }

        private GameSetting m_gameSetting_;
        public GameSetting Gamesetting_
        {
            get { return m_gameSetting_; }
            set
            {
                m_gameSetting_ = value;
                RaisePropertyChanged("Gamesetting_");
            }
        }
    }
}
