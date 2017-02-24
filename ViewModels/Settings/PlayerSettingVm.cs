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
    public class PlayerSettingVm : ViewModelBase
    {
        #region MEMBER

        private PlayerSettings m_playerSetting;// = new PlayerSettings();
        private ObservableCollection<B3GameSetting> m_b3SettingEnableDisable;// m_b3SettingEnableDisable = new ObservableCollection<B3GameSetting>();

        #endregion
        #region CONSTRUCTOR

        public PlayerSettingVm(PlayerSettings _playerSetting, ObservableCollection<B3GameSetting> m_b3SettingEnableDisable_)
        {
            VolumeList = Volume();
            PlayerSetting_ = _playerSetting;
            m_b3SettingEnableDisable = m_b3SettingEnableDisable_;
            AssignEnableSettingToGame();
      
        }

        #endregion
        #region METHOD

        public void RevertValueBack()
        {           
            AssignEnableSettingToGame();
            PlayerSetting_ = m_playerSetting;
        }

        private void AssignEnableSettingToGame()
        {     
            foreach (var i in m_b3SettingEnableDisable)
            {
                int b3gameid = i.GameId;
                switch(b3gameid)
                {
                    case (int)B3Game.CRAZYBOUT: 
                        {
                            m_playerSetting.IsEnableCRAZYBOUT = i;                       
                            break; 
                        }
                    case (int)B3Game.JAILBREAK: 
                        {
                            m_playerSetting.IsEnableJAILBREAK = i;
                            break;                         
                        }
                    case (int)B3Game.MAYAMONEY:
                        {
                            m_playerSetting.IsEnableMAYAMONEY = i;
                            break;
                        }
                    case (int)B3Game.SPIRIT76: 
                        {
                            m_playerSetting.IsEnableSPIRIT76 = i;
                            break;
                        }
                    case (int)B3Game.TIMEBOMB:
                        {
                            m_playerSetting.IsEnableTIMEBOMB = i;
                            break;
                        }
                    case (int)B3Game.UKICKEM:
                        {
                            m_playerSetting.IsEnableUKICKEM =  i;
                            break;
                        }
                    case (int)B3Game.WILDBALL: 
                        {
                            m_playerSetting.IsEnableWILDBALL = i;
                            break;
                        }
                    case (int)B3Game.WILDFIRE: 
                        {
                            m_playerSetting.IsEnableWILDFIRE = i;
                            break;
                        }
                }
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

        #endregion
        #region PROPERTIES
        public List<string> VolumeList { get; set; }

        public ObservableCollection<B3GameSetting> B3SettingEnableDisable
        {
       
            get { return m_b3SettingEnableDisable; }
            set
            {
                if (value != m_b3SettingEnableDisable)
                {
                    m_b3SettingEnableDisable = value;
                    //RaisePropertyChanged("B3SettingEnableDisable");
                }
            }
        }

        public PlayerSettings PlayerSetting_
        {
            get { return m_playerSetting; }
            set
            {
                m_playerSetting = value;
                RaisePropertyChanged("PlayerSetting_");
            }
        }

        #endregion

    }
}

#region REF
//private ObservableCollection<B3GameSetting> m_b3SettingEnableDisable;
//private B3GameSetting m_isCRAZYBOUTEnableIsAllowedSetting;
//private B3GameSetting m_isJAILBREAKEnableIsAllowedSetting;
//private B3GameSetting m_isMAYAMONEYEnableIsAllowedSetting;
//private B3GameSetting m_isSPIRIT76EnableIsAllowedSetting;
//private B3GameSetting m_isTIMEBOMBEnableIsAllowedSetting;
//private B3GameSetting m_isUKICKEMEnableIsAllowedSetting;
//private B3GameSetting m_isWILDBALLEnableIsAllowedSetting;
//private B3GameSetting m_isWILDFIREEnableIsAllowedSetting;


        //public bool GetB3EnableSettingPreviousValue(int GameId)
        //{
        //    bool tempResult = true;
        //    switch (GameId)
        //    {              
        //            case (int)B3Game.CRAZYBOUT: 
        //                {                          
        //                    tempResult = m_playerSetting.IsEnableCRAZYBOUT;
        //                    break; 
        //                }
        //            case (int)B3Game.JAILBREAK: 
        //                {
        //                    tempResult = m_playerSetting.IsEnableJAILBREAK;
        //                    break; 
        //                }
        //            case (int)B3Game.MAYAMONEY:
        //                {
        //                    tempResult = m_playerSetting.IsEnableMAYAMONEY;
        //                    break; 
        //                }
        //            case (int)B3Game.SPIRIT76: 
        //                {
        //                    tempResult = m_playerSetting.IsEnableSPIRIT76;
        //                    break; 
        //                }
        //            case (int)B3Game.TIMEBOMB:
        //                {
        //                    tempResult = m_playerSetting.IsEnableTIMEBOMB;
        //                    break; 
        //                }
        //            case (int)B3Game.UKICKEM:
        //                {
        //                    tempResult = m_playerSetting.IsEnableUKICKEM;
        //                    break; 
        //                }
        //            case (int)B3Game.WILDBALL: 
        //                {
        //                    tempResult = m_playerSetting.IsEnableWILDBALL;
        //                    break; 
        //                }
        //            case (int)B3Game.WILDFIRE: 
        //                {
        //                    tempResult = m_playerSetting.IsEnableWILDFIRE;
        //                    break; 
        //                }
        //     }           
        //     return tempResult;
        //}
#endregion