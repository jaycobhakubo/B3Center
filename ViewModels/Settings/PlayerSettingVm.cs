using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
    public class PlayerSettingVm : ViewModelBase
    {
        private PlayerSettings m_playerSetting;

        public PlayerSettingVm(PlayerSettings _playerSetting)
        {
            VolumeList = Volume();
            PlayerSetting_ = _playerSetting;    
         
           
        }

        public List<string> VolumeList
        {
            get;
            set;
        }

        public PlayerSettings PlayerSetting_
        {
            get
            {
                return m_playerSetting;
            }
            set
            {
                m_playerSetting = value;
                RaisePropertyChanged("PlayerSetting_");
            }
        }

        private List<string> Volume()
        {
            List<string> result = new List<string>();
            result.Add("0");
            result.Add("1");
            result.Add("2");
            result.Add("3");
            result.Add("4");
            result.Add("5");
            result.Add("6");
            result.Add("7");
            result.Add("8");
            result.Add("9");
            result.Add("10");
            return result;
        }

      
     


        
    }
}

