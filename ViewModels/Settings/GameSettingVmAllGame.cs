using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using System.Collections.Generic;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
    public class GameSettingVmAllGame : ViewModelBase
    {
        private GameSetting m_gameSetting;

        public GameSettingVmAllGame(GameSetting gameSetting, B3GameType gameType)
        {
            m_gameSetting = gameSetting;
            GameType = gameType;
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

        public B3GameType GameType { get; private set; }
        public List<string> ListMaxBetLevel { get { return SettingViewModel.OneToTenList(); } }
        public List<string> ListMaxCards { get { return SettingViewModel.MaxCardCountList(); } }
        public List<string> ListCallSpeedMin { get { return SettingViewModel.OneToTenList(); } }
        public List<string> ListCallSpeedMax { get { return SettingViewModel.OneToTenList(); } }
        public List<string> ListCallSpeed { get { return SettingViewModel.OneToTenList(); } }
        public List<string> ListCallSpeedBonus { get { return SettingViewModel.OneToTenList(); } }    
    }
}
