using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
    public class SessionSettingVm : ViewModelBase
    {
        private SessionSetting m_sessionSetting;

        public SessionSettingVm(SessionSetting _sessionSetting)
        {
            SessionSetting_ = _sessionSetting;
        }

        public SessionSetting SessionSetting_
        {
            get
            {
                return m_sessionSetting;
            }
            set
            {
                m_sessionSetting = value;
                RaisePropertyChanged("SessionSetting_");
            }
        }
    }
}
