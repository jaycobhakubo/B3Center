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
    }
}

