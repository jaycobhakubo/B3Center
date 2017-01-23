using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
    public class SessionSettingVm : ViewModelBase
    {
  

        public SessionSettingVm(ObservableCollection<B3SettingGlobal> b3ServerSetting)
        {
            m_b3ServerSetting = b3ServerSetting;
            PayoutLimit = (m_b3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.PayoutLimit)));
            JackpotLimit = (m_b3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.JackpotLimit)));
            var tempBool = (m_b3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.EnforceMix)));
        }

        public ObservableCollection<B3SettingGlobal> m_b3ServerSetting
        { get; set; }


        public B3SettingGlobal PayoutLimit
        {
            get; set;
        }



        public B3SettingGlobal JackpotLimit
        {
            get; set;
        }



        public B3SettingGlobal EnforceMix
        {
            get; set;
        }

    }
}
