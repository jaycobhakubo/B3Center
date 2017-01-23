using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
    public class SalesSettingVm : ViewModelBase
    {
        private SalesSettings m_salesSetting;

        public SalesSettingVm(SalesSettings m_salesSetting)
        {
            SalesSetting_ = m_salesSetting;
        }

        public SalesSettings SalesSetting_
        {
            get
            {
                return m_salesSetting;
            }
            set
            {
                m_salesSetting = value;
                RaisePropertyChanged("SalesSetting_");
            }
        }

        private List<string> Volume()
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

      
        private string GetVolumeEquivValue(int Volume)
        {

            string tempValue = "";
            if (Volume <= 100 && Volume >= 91) { tempValue = "10"; }
            else if (Volume < 91 && Volume >= 81) { tempValue = "9"; }
            else if (Volume < 81 && Volume >= 71) { tempValue = "8"; }
            else if (Volume < 71 && Volume >= 61) { tempValue = "7"; }
            else if (Volume < 61 && Volume >= 51) { tempValue = "6"; }
            else if (Volume < 51 && Volume >= 41) { tempValue = "5"; }
            else if (Volume < 41 && Volume >= 31) { tempValue = "4"; }
            else if (Volume < 31 && Volume >= 21) { tempValue = "3"; }
            else if (Volume < 21 && Volume >= 11) { tempValue = "2"; }
            else if (Volume < 11 && Volume >= 1) { tempValue = "1"; }
            else if (Volume == 0) { tempValue = "0"; }
            return tempValue;
        }


        private string GetVolumeEquivToDB(int VolumeLevel)
        {
            string result = "";
            switch (VolumeLevel)
            {
                case 0: { result = "0"; break; }
                case 1: { result = "10"; break; }
                case 2: { result = "20"; break; }
                case 3: { result = "30"; break; }
                case 4: { result = "40"; break; }
                case 5: { result = "50"; break; }
                case 6: { result = "60"; break; }
                case 7: { result = "70"; break; }
                case 8: { result = "80"; break; }
                case 9: { result = "90"; break; }
                case 10: { result = "100"; break; }


            }
            return result;
        }
    }
}

