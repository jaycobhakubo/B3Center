using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
   public class ServerSettingVm : ViewModelBase
    {
        //private ServerSetting m_serverSetting;

        public ServerSettingVm(ObservableCollection<B3SettingGlobal> b3ServerSetting)
        {
                m_b3ServerSetting = b3ServerSetting;
                MinPlayer = (m_b3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.MinPlayer)));
                GameStartDelay = (m_b3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.GameStartDelay)));
                Consolation = (m_b3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.ConsolotionPrize)));
                GameRecallPassword = (m_b3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.GameRecallPass)));
                WaitCountDown = (m_b3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.WaiCountDown)));
        }


        public ObservableCollection<B3SettingGlobal> m_b3ServerSetting
            {get; set;}


        public B3SettingGlobal MinPlayer
        {
            get;set;
        }

        public B3SettingGlobal GameStartDelay
        {
            get; set;
        }
        public B3SettingGlobal Consolation
        {
            get; set;
        }
        public B3SettingGlobal GameRecallPassword
        {
            get; set;
        }

        public B3SettingGlobal WaitCountDown
        {
            get; set;
        }


      
      
        //public ServerSetting ServerSetting_
        //{
           
        //    get
        //    {
        //        return m_serverSetting;
        //    }
        //    set
        //    {
        //        m_serverSetting = value;
        //        RaisePropertyChanged("ServerSetting_");
        //    }
        //}     
    }
}
