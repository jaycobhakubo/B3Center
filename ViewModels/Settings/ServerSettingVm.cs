using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
   public class ServerSettingVm : ViewModelBase
    {
        private ServerSetting m_serverSetting;

        public ServerSettingVm(ServerSetting serversetting)
        {
            ServerSettings = serversetting;
        }
      
        public ServerSetting ServerSettings
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
