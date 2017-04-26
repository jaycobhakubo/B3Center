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
   		private bool m_isEnforceMixEnable;
        private readonly List<B3SettingGlobal> m_originalSessionSettings; 
        #endregion
        
        #region Constructor

        public SessionSettingVm(List<B3SettingGlobal> sessionSettingsList)
        {
            SessionSettings = new SessionSetting();
            UpdateSettingsListToModel(sessionSettingsList);
            m_originalSessionSettings = sessionSettingsList;
            IsEnforceMixEnable = !(SettingViewModel.Instance.GetIsRngSetting());//If false then 55455 (enforce mix should be true)
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

        public bool IsEnforceMixEnable
        {
            get { return m_isEnforceMixEnable; }
            set
            {
                m_isEnforceMixEnable = value;
                RaisePropertyChanged("IsEnforceMixEnable");
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
                }
            }
        }



        private void UpdateModelToSettingsList()
        {
            foreach (var setting in m_originalSessionSettings)
            {
                setting.HasChanged = false;
                var tempOldSettingValue = setting.B3SettingValue;//saved current setting value
                switch (setting.SettingType)
                {
                    case B3SettingType.PayoutLimit:
                        setting.B3SettingValue = SessionSettings.PayoutLimit;
                        break;
                    case B3SettingType.JackpotLimit:
                        setting.B3SettingValue = SessionSettings.JackpotLimit;
                        break;
                }
                if (tempOldSettingValue != setting.B3SettingValue)//check if current = new setting
                {
                    setting.B3SettingDefaultValue = tempOldSettingValue;
                    setting.HasChanged = true;                  
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
