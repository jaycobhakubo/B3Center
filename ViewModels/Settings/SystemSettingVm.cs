using System;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using System.Collections.Generic;
using GameTech.Elite.Client.Modules.B3Center.Business;


namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
    public class SystemSettingVm : ViewModelBase
    {
        #region Fields
        private SystemSetting m_systemSetting;
        private readonly List<B3SettingGlobal> m_originalSystemSettings; 
        #endregion

        #region Constructor
        public SystemSettingVm(List<B3SettingGlobal> systemSettingList)
        {
            VolumeList = Business.Helpers.ZeroToTenList;
            CurrencyList = GetCurrencyList();
            AutoSessionEndList = GetAutoSessionEndItemList();
            SystemSettings = new SystemSetting();
            UpdateSettingsListToModel(systemSettingList);
            m_originalSystemSettings = systemSettingList;
        }
        #endregion

        #region Properties
        public List<string> VolumeList{get;set;}
        public List<string> CurrencyList{get;set;}
		
        public List<string> AutoSessionEndList
        {
            get;
            set;
        }

        public SystemSetting SystemSettings
        {
            get
            {
                return m_systemSetting;
            }
            set
            {
                m_systemSetting = value;
                RaisePropertyChanged("SystemSettings");
            }
        }		
        #endregion

        #region Methods

        private List<string> GetAutoSessionEndItemList()
        {
            List<string> autoSessionEndItems = new List<string> { "JACKPOT", "PAYOUT", "OFF" };
            return autoSessionEndItems;
        }
		
        private void UpdateSettingsListToModel(List<B3SettingGlobal> settingsList)
        {
            foreach (var setting in settingsList)
            {
                switch (setting.SettingType)
                {
                    case B3SettingType.EnableUk:
                        SystemSettings.EnableUk = setting.ConvertB3StringValueToBool();
                        break;
                    case B3SettingType.DualAccount:
                        SystemSettings.DualAccount = setting.ConvertB3StringValueToBool();
                        break;
                    case B3SettingType.MultiOperator:
                        SystemSettings.MultiOperator = setting.ConvertB3StringValueToBool();
                        break;
                    case B3SettingType.NorthDakotaMode:                                          
                        SystemSettings.NorthDakotaMode = setting.ConvertB3StringValueToBool();
                        break;
                    case B3SettingType.HandPayTrigger:
                        SystemSettings.HandPayTrigger = setting.B3SettingValue;
                        break;
                    case B3SettingType.MinimumPlayers:
                        SystemSettings.MinimumPlayers = setting.B3SettingValue;
                        break;
                    case B3SettingType.VipPointMultiplier:
                        SystemSettings.VipPointMultiplier = setting.B3SettingValue;
                        break;
                    case B3SettingType.MagCardSentinelStart:
                        SystemSettings.MagCardSentinelStart = setting.B3SettingValue;
                        break;
                    case B3SettingType.MagCardSentinelEnd:
                        SystemSettings.MagCardSentinelEnd = setting.B3SettingValue;
                        break;
                    case B3SettingType.Currency:
                        SystemSettings.Currency = setting.B3SettingValue;
                        break;
                    case B3SettingType.RngBallCallTime:
                        SystemSettings.RngBallCallTime = setting.B3SettingValue;
                        break;
                    case B3SettingType.PlayerPinLength:
                        SystemSettings.PlayerPinLength = setting.B3SettingValue;
                        break;
                    case B3SettingType.AutoSessionEnd:
                        SystemSettings.AutoSessionEnd = string.IsNullOrEmpty(setting.B3SettingValue) ? "OFF" : setting.B3SettingValue;                       
                        break;
                    case B3SettingType.SiteName:
                        SystemSettings.SiteName = setting.B3SettingValue;
                        break;
                    case B3SettingType.SystemMainVolume:
                        SystemSettings.SystemMainVolume = Business.Helpers.GetVolumeEquivValue(Convert.ToInt32(setting.B3SettingValue));
                        break;
                }
            }
        }

        private void UpdateModelToSettingsList()
        {
            foreach (var setting in m_originalSystemSettings)
            {
                setting.HasChanged = false;
                var tempOldSettingValue = setting.B3SettingValue;//saved current setting value

                switch (setting.SettingType)
                {
                    case B3SettingType.EnableUk:
                        setting.B3SettingValue = SystemSettings.EnableUk.ConvertToB3StringValue();
                        break;
                    case B3SettingType.DualAccount:
                        setting.B3SettingValue = SystemSettings.DualAccount.ConvertToB3StringValue();
                        break;
                    case B3SettingType.MultiOperator:
                        setting.B3SettingValue = SystemSettings.MultiOperator.ConvertToB3StringValue();
                        break;
                    case B3SettingType.NorthDakotaMode:
                        setting.B3SettingValue = SystemSettings.NorthDakotaMode.ConvertToB3StringValue();
                        break;
                    case B3SettingType.HandPayTrigger:
                        setting.B3SettingValue = SystemSettings.HandPayTrigger;
                        break;
                    case B3SettingType.MinimumPlayers:
                        setting.B3SettingValue = SystemSettings.MinimumPlayers;
                        break;
                    case B3SettingType.VipPointMultiplier:
                        setting.B3SettingValue = SystemSettings.VipPointMultiplier;
                        break;
                    case B3SettingType.MagCardSentinelStart:
                        setting.B3SettingValue = SystemSettings.MagCardSentinelStart;
                        break;
                    case B3SettingType.MagCardSentinelEnd:
                        setting.B3SettingValue = SystemSettings.MagCardSentinelEnd;
                        break;
                    case B3SettingType.Currency:
                        setting.B3SettingValue = SystemSettings.Currency;
                        break;
                    case B3SettingType.RngBallCallTime:
                        setting.B3SettingValue = SystemSettings.RngBallCallTime;
                        break;
                    case B3SettingType.PlayerPinLength:
                        setting.B3SettingValue = SystemSettings.PlayerPinLength;
                        break;
                    case B3SettingType.AutoSessionEnd:
                        setting.B3SettingValue = SystemSettings.AutoSessionEnd == "OFF" ? string.Empty : setting.B3SettingValue;
                        break;
                    case B3SettingType.SiteName:
                        setting.B3SettingValue = SystemSettings.SiteName;
                        break;
                    case B3SettingType.SystemMainVolume:
                        setting.B3SettingValue = Business.Helpers.GetVolumeEquivToDb(Convert.ToInt32(SystemSettings.SystemMainVolume));
                        break;
                }

                if (tempOldSettingValue != setting.B3SettingValue)//check if current = new setting
                {
                    setting.B3SettingDefaultValue = tempOldSettingValue;
                    setting.HasChanged = true;;
                }
            }
        }


        public List<B3SettingGlobal> Save()
        {
            UpdateModelToSettingsList();
            return m_originalSystemSettings;
        }

        public void ResetSettingsToDefault()
        {
            UpdateSettingsListToModel(m_originalSystemSettings);
        }
		
				public static List<string> BetLevel()
        {
            return Business.Helpers.OneToTenList;
        }

        public static List<string> MaxCard()
        {
            return Business.Helpers.MaxCardCountList;
        }

        private List<string> GetCurrencyList()
        {
            List<string> currencyItems = new List<string> { "CREDIT", "DOLLAR", "PESO", "POUND" };
            return currencyItems;
        }

        #endregion

    }


}

