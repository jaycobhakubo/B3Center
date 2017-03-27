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
        private GameSetting m_gameSetting = new GameSetting();
        private readonly int _gameId;

        public GameSettingVmAllGame(GameSetting gameSetting, int gameId)
        {
            m_gameSetting = gameSetting;
            _gameId = gameId;
            SettingViewModel.Instance.BtnSaveIsEnabled = Settings.IsEnableGame.IsEnabled; 
        }


        public GameSetting Settings
        {
            get { return m_gameSetting; }
            set
            {
                m_gameSetting = value;
                RaisePropertyChanged("Settings");
            }
        }

        public bool IsGameEnable
        {
            get { return !Settings.IsEnableGame.IsEnabled; }
            set
            {
                Settings.IsEnableGame.IsEnabled = !value;
                RaisePropertyChanged("IsGameEnable");
            }
        }
    }
}
