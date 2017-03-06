using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using GameTech.Elite.Base;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings

{
    public class GameSettingVmAllGame : ViewModelBase
    {
        public GameSettingVmAllGame(GameSetting _gameSetting, int GameId)
        {
            m_gameSetting_ = _gameSetting;
            var settingvm = SettingViewModel.Instance;
            settingvm.BtnSaveIsEnabled = Gamesetting_.IsEnableGame.IsEnabled; 
        }

        private GameSetting m_gameSetting_ = new GameSetting();
        public GameSetting Gamesetting_
        {
            get { return m_gameSetting_; }
            set
            {
                m_gameSetting_ = value;
                RaisePropertyChanged("Gamesetting_");
            }
        }

        public bool IsGameEnable
        {
            get { return !Gamesetting_.IsEnableGame.IsEnabled; }
            set
            {
                Gamesetting_.IsEnableGame.IsEnabled = !value;
                RaisePropertyChanged("IsGameEnable");
            }
        }
    }
}
