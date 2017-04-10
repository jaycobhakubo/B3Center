using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using System.Collections.Generic;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
    public class SalesSettingVm : ViewModelBase
    {
        private SalesSettings m_salesSetting;

        public SalesSettingVm(SalesSettings salesSetting)
        {
            VolumeList = SettingViewModel.ZeroToTenList();
            SalesSetting = salesSetting;              
        }


        public List<string> VolumeList
        {
            get;
            set;
        }

        public SalesSettings SalesSetting
        {
            get
            {
                return m_salesSetting;
            }
            set
            {
                m_salesSetting = value;
                RaisePropertyChanged("SalesSetting");
            }
        }
    }
}

