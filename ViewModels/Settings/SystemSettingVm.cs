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
            VolumeList = Volume();
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

        private List<string> Volume()
        {
            List<string> result = new List<string> {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"};
            return result;
        }

        public static List<string> BetLevel()
        {
            List<string> result = new List<string> {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"};
            return result;
        }

        public static List<string> MaxCard()
        {
            var result = new List<string> {"4", "6"};
            return result;
        }


      
        private List<string> GetCurrencyList()
        {
            List<string> currencyItems = new List<string> {"CREDIT", "DOLLAR", "PESO", "POUND"};
            return currencyItems;
        }


        private List<string> GetAutoSessionEndItemList()
        {
            List<string> autoSessionEndItems = new List<string> {"JACKPOT", "PAYOUT", "OFF"};
            return autoSessionEndItems;
        }
    }


}

