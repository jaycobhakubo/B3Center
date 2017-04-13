using System.Collections.Generic;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
    public class SessionSettingVm : ViewModelBase
    {

        #region Fields
        
        private SessionSetting m_sessionSetting;

        private readonly List<B3SettingGlobal> m_originalSessionSettings; 
        #endregion
        

        #region Constructor

        public SessionSettingVm(List<B3SettingGlobal> sessionSettingsList)
        {
            SessionSettings = new SessionSetting();
            UpdateSettingsListToModel(sessionSettingsList);
            m_originalSessionSettings = sessionSettingsList;
        }

        #endregion


        #region Properties

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

        #endregion

        #region Methods

        private void UpdateSettingsListToModel(List<B3SettingGlobal> settingsList)
        {
            foreach (var setting in settingsList)
            {
                switch (setting.SettingType)
                {
                    case B3SettingType.PayoutLimit:
                        SessionSettings.PayoutLimit = setting.B3SettingValue;
                        break;
                    case B3SettingType.JackpotLimit:
                        SessionSettings.JackpotLimit = setting.B3SettingValue;
                        break;
                    case B3SettingType.EnforceMix:
                        SessionSettings.EnforceMix = setting.ConvertB3StringValueToBool();
                        break;
                }
            }
        }

        private void UpdateModelToSettingsList()
        {
            foreach (var setting in m_originalSessionSettings)
            {
                setting.B3SettingDefaultValue = setting.B3SettingValue;
                switch (setting.SettingType)
                {
                    case B3SettingType.PayoutLimit:
                        setting.B3SettingValue = SessionSettings.PayoutLimit;
                        break;
                    case B3SettingType.JackpotLimit:
                        setting.B3SettingValue = SessionSettings.JackpotLimit;
                        break;
                    case B3SettingType.EnforceMix:
                        setting.B3SettingValue = SessionSettings.EnforceMix.ConvertToB3StringValue();
                        break;
                }
            }
        }

        public List<B3SettingGlobal> Save()
        {
            UpdateModelToSettingsList();
            return m_originalSessionSettings;
        }

        public void ResetSettingsToDefault()
        {
            UpdateSettingsListToModel(m_originalSessionSettings);
        }

        #endregion

    }
}
