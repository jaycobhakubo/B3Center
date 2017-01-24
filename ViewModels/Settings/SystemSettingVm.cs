using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
    public class SystemSettingVm : ViewModelBase
    {
        private SystemSetting m_systemSetting;

        public SystemSettingVm(SystemSetting _systemSetting)
        {
            VolumeList = Volume();
            CurrencyList = GetCurrencyList();
            AutoSessionEndList = GetAutoSessionEndItemList();
            SystemSetting_ = _systemSetting;

            if (string.IsNullOrEmpty(SystemSetting_.AutoSessionEnd))
            {
                SystemSetting_.AutoSessionEnd = "OFF";
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

        public SystemSetting SystemSetting_
        {
            get
            {
                return m_systemSetting;
            }
            set
            {
                m_systemSetting = value;
                RaisePropertyChanged("SystemSetting_");
            }
        }

        public static List<string> Volume()
        {
            List<string> result = new List<string>();
            result.Add("0");
            result.Add("1");
            result.Add("2");
            result.Add("3");
            result.Add("4");
            result.Add("5");
            result.Add("6");
            result.Add("7");
            result.Add("8");
            result.Add("9");
            result.Add("10");
            return result;
        }

        public static List<string> BetLevel()
        {
            List<string> result = new List<string>();
            result.Add("1");
            result.Add("2");
            result.Add("3");
            result.Add("4");
            result.Add("5");
            result.Add("6");
            result.Add("7");
            result.Add("8");
            result.Add("9");
            result.Add("10");
            return result;
        }

        public static List<string> MaxCard()
        {
            var result = new List<string>();
            result.Add("4");
            result.Add("6");
            return result;
        }


      
        private List<string> GetCurrencyList()
        {
            List<string> CurrencyItems = new List<string>();
            CurrencyItems.Add("CREDIT");
            CurrencyItems.Add("DOLLAR");
            CurrencyItems.Add("PESO");
            CurrencyItems.Add("POUND");
            return CurrencyItems;
        }


        private List<string> GetAutoSessionEndItemList()
        {
            List<string> AutoSessionEndItems = new List<string>();
            AutoSessionEndItems.Add("JACKPOT");
            AutoSessionEndItems.Add("PAYOUT");
            AutoSessionEndItems.Add("OFF");
            return AutoSessionEndItems;
        }


        private string GetCallSpeedEquivToDB(int callspeedvalue)
        {
            string result = "";
            switch (callspeedvalue)
            {
                case 1: { result = "5000"; break; }
                case 2: { result = "4020"; break; }
                case 3: { result = "3530"; break; }
                case 4: { result = "3040"; break; }
                case 5: { result = "2550"; break; }
                case 6: { result = "2060"; break; }
                case 7: { result = "1570"; break; }
                case 8: { result = "1080"; break; }
                case 9: { result = "590"; break; }
                case 10: { result = "100"; break; }
            }
            return result;
        }
    }


}

