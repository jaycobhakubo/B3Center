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


       //private string m_minPlayer;
       //private string m_gameStart;
       //private string m_consolationPrize;
       //private string m_gameRecallPassword;
       //private string m_waitCountDown;
       
       //INITIALIZE this
       public ServerSettingVm(ServerSetting serversetting)
        {
             ServerSettingx = serversetting;
            //Testx = ServerSettingx.WaitCountDown;
             //SaveDefaultSetting(serversetting);
        }

        //public string m_testx;
        //public string Testx
        //{
        //    get
        //    {
        //        return m_serverSetting.WaitCountDown;
        //    }
        //    set
        //    {
        //        m_serverSetting.WaitCountDown = value;
        //        RaisePropertyChanged("Testx");
        //    }
        //}

        private ServerSetting m_serverSetting;
        public ServerSetting ServerSettingx
        {
            get
            {
                return m_serverSetting;
            }
            set
            {
                m_serverSetting = value;
                RaisePropertyChanged("ServerSettingx");
            }
        }     
    }
}
