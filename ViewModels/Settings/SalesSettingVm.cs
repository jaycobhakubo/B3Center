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
            VolumeList = Volume();
            SalesSetting_ = m_salesSetting;              
        }


        public List<string> VolumeList
        {
            get;
            set;
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
    }
}

