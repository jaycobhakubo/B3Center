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
        private SalesSetting m_serverSetting;

        public SalesSettingVm(ServerSetting _serversetting)
        {
            ServerSetting_ = _serversetting;
        }

        public ServerSetting ServerSetting_
        {
            get
            {
                return m_serverSetting;
            }
            set
            {
                m_serverSetting = value;
                RaisePropertyChanged("ServerSetting_");
            }
        }
    }
}

    {
    }
}
