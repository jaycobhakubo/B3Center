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

 
       private string m_minPlayer;
       private string m_gameStart;
       private string m_consolationPrize;
       private string m_gameRecallPassword;
       private string m_waitCountDown;
       
       //INIT
       public ServerSettingVm(ServerSetting serversetting)
        {
             ServerSetting = serversetting;
             SaveDefaultSetting(serversetting);
        }

       //SAVED ORIGINAL SETTING
       private void SaveDefaultSetting(ServerSetting serversetting)
       {
           m_minPlayer = serversetting.MinPlayer;
           m_gameStart = serversetting.GameStartDelay;
           m_consolationPrize = serversetting.Consolation;
           m_gameRecallPassword = serversetting.GameRecallPassw;
           m_waitCountDown = serversetting.WaitCountDown;
       }

       //GET SAVED ORIGINAL SETTING
       public ServerSetting GetOriginalValue()
       {
           var x = new ServerSetting();
           x.MinPlayer = m_minPlayer;
           x.GameStartDelay = m_gameStart;
           x.Consolation = m_consolationPrize;
           x.GameRecallPassw = m_gameRecallPassword;
           x.WaitCountDown = m_waitCountDown;
           return x;
       }

       //NEW SETTING
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
    }
}
