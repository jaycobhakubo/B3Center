using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
   public class ServerSettingVm : ViewModelBase
    {
       
       public ServerSettingVm(ServerSetting serversetting)
        {
            ServerSetting = serversetting;
        }


        private ServerSetting m_serverSetting;
        public ServerSetting ServerSetting
        {
            get
            {
                return m_serverSetting;
            }
            set
            {
                m_serverSetting = value;
                RaisePropertyChanged("ServerSetting");
            }
        }

        public string MinPlayer
        {
            get { return m_serverSetting.MinPlayer; }
            set
            {
                m_serverSetting.MinPlayer = value;
                RaisePropertyChanged("MinPlayer");
            }
        }

     

        //public string GameStartDelay
        //{
        //    get;
        //    set
        //    {
        //        m_serverSetting.GameStartDelay = value;
        //        RaisePropertyChanged("ServerSetting");
        //    }
        //}

        //public string Consolation
        //{
        //    get;
        //    set
        //    {
        //        m_serverSetting.Consolation = value;
        //        RaisePropertyChanged("ServerSetting");
        //    }
        //}

        //public string GameRecallPassword
        //{
        //    get;
        //    set
        //    {
        //        m_serverSetting.GameRecallPassw = value;
        //        RaisePropertyChanged("ServerSetting");
        //    }
        //}


        //public string WaitCountDown
        //{
        //    get;
        //    set
        //    {
        //        m_serverSetting.WaitCountDown = value;
        //        RaisePropertyChanged("ServerSetting");
        //    }
        //}
    }
}
