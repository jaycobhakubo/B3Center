using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
    public class SessionSettingVm : ViewModelBase
    {
        private SessionSetting m_sessionSetting;

        public SessionSettingVm(SessionSetting sessionSetting)
        {
            SessionSettings = sessionSetting;
        }

        public SessionSetting SessionSettings
        {
            get
            {
                return m_sessionSetting;
            }
            set
            {
                m_sessionSetting = value;
                RaisePropertyChanged("SessionSettings");
            }
        }
    }
}
