using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using System.Collections.Generic;


namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
    public class SystemSettingVm : ViewModelBase
    {
        private SystemSetting m_systemSetting;

        public SystemSettingVm(SystemSetting systemSetting)
        {
            VolumeList = SettingViewModel.ZeroToTenList();
            CurrencyList = GetCurrencyList();
            AutoSessionEndList = GetAutoSessionEndItemList();
            SystemSettings = systemSetting;

            if (string.IsNullOrEmpty(SystemSettings.AutoSessionEnd))
            {
                SystemSettings.AutoSessionEnd = "OFF";
            }
        }

        public List<string> VolumeList
        {
            get;
            set;
        }

        public List<string> CurrencyList
        {
            get;
            set;
        }

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

        public static List<string> BetLevel()
        {
            return SettingViewModel.OneToTenList();
        }

        public static List<string> MaxCard()
        {
            return SettingViewModel.MaxCardCountList();
        }



        private List<string> GetCurrencyList()
        {
            List<string> currencyItems = new List<string> { "CREDIT", "DOLLAR", "PESO", "POUND" };
            return currencyItems;
        }


        private List<string> GetAutoSessionEndItemList()
        {
            List<string> autoSessionEndItems = new List<string> {"JACKPOT", "PAYOUT", "OFF"};
            return autoSessionEndItems;
        }
    }


}

