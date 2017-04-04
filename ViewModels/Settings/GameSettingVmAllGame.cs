using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using GameTech.Elite.Base;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
    public class GameSettingVmAllGame : ViewModelBase
    {
        private GameSetting m_gameSetting;

        public GameSettingVmAllGame(GameSetting gameSetting)
        {
            m_gameSetting = gameSetting;
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
            get { return Settings.IsEnableGame.IsEnabled; }
            set
            {
                Settings.IsEnableGame.IsEnabled = value;
                RaisePropertyChanged("IsGameEnable");
            }
        }
    }
}
