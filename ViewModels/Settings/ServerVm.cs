using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
   public class ServerVm : ViewModelBase
    {
        private ServerM m_serverSetting;
        public ServerM ServerSetting
        {
            get { return m_serverSetting; }
            set { m_serverSetting = value;
                RaisePropertyChanged("ServerSetting");
            }
        }

        public ServerVm(ServerM serversetting)
        {
            ServerSetting = serversetting;
        }

        //private string m_minPlayer;
        //private string m_gamestart;
        //private string m_
    }
}
