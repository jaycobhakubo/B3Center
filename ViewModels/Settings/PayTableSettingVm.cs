using GameTech.Elite.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using GameTech.Elite.Client.Modules.B3Center.UI.SettingViews.PayTable;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
   public class PayTableSettingVm : ViewModelBase
    {
        

        private PayTableSetting m_PayTableSettings;
        public PayTableSetting PayTableSettings
        {
            get
            {
                return m_PayTableSettings;
            }
            set
            {
                m_PayTableSettings = value;
                RaisePropertyChanged("PayTableSettings");
            }
        }

        private bool m_isRNG;
        public bool IsRNG
        {
            get { return m_isRNG; }
            set 
            {
                m_isRNG = value;
                RaisePropertyChanged("IsRNG");
            }
        }

        private readonly List<B3SettingGlobal> m_originalPayTableSettings;


       public PayTableSettingVm(List<B3SettingGlobal> payTableSettingList)
        {
           
            PayTableSettings = new PayTableSetting();
            UpdateSettingsListToModel(payTableSettingList);
            m_originalPayTableSettings = payTableSettingList;           
        }

       private void UpdateSettingsListToModel(List<B3SettingGlobal> settingsList)
       {
           //PayTableSettings.LGamePayTable = SettingViewModel.Instance.GetB3MathGamePlay(GameType);
           foreach (var setting in settingsList)
           {
               switch (setting.SettingType)
               {
                  
                   case B3SettingType.CommonRngBallCall:
                       PayTableSettings.CommonRngBallCall = setting.ConvertB3StringValueToBool();
                       break;
                   case B3SettingType.EnforceMix:
                       {
                           PayTableSettings.EnforceMix = setting.ConvertB3StringValueToBool();
                           break;
                       }
                    case B3SettingType.MathPayTableSetting:
                       {
                            var tempGameType = setting.GameType;
                            switch (tempGameType)
                            {                              
                                case B3GameType.Crazybout:
                                    CrazyBoutPayTableVm = new GamePayTableVm(setting);
                                    break;
                                case B3GameType.Jailbreak:
                                    JailBreakPayTableVm = new GamePayTableVm(setting);
                                    break;
                                case B3GameType.Mayamoney:
                                    MayaMoneyPayTableVm = new GamePayTableVm(setting);
                                    break;
                                case B3GameType.Spirit76:
                                    Spirit76PayTableVm = new GamePayTableVm(setting);
                                    break;
                                case B3GameType.Timebomb:
                                    TimeBombPayTableVm = new GamePayTableVm( setting);
                                    break;
                                case B3GameType.Ukickem:
                                    UkickEmPayTableVm = new GamePayTableVm( setting);
                                    break;
                                case B3GameType.Wildball:
                                    WildBallPayTableVm = new GamePayTableVm(setting);
                                    break;
                                case B3GameType.Wildfire:
                                    WildFirePayTableVm = new GamePayTableVm(setting);
                                    break;
                            }
                            break;                                                      
                    }                 
               }
           }
       }
              
        public GamePayTableVm CrazyBoutPayTableVm{ get; set; }
        public GamePayTableVm JailBreakPayTableVm { get; set; }
        public GamePayTableVm MayaMoneyPayTableVm { get; set; }
        public GamePayTableVm Spirit76PayTableVm { get; set; }
        public GamePayTableVm TimeBombPayTableVm { get; set; }
        public GamePayTableVm UkickEmPayTableVm { get; set; }
        public GamePayTableVm WildBallPayTableVm { get; set; }
        public GamePayTableVm WildFirePayTableVm { get; set; }

        public bool SettingHasChanged { get; set; }

       private void UpdateModelToSettingsList()
       {
           SettingHasChanged = false;
           foreach (var setting in m_originalPayTableSettings)
           {
              
               switch (setting.SettingType)
               {
                   case B3SettingType.CommonRngBallCall:
                       {
                           if (setting.B3SettingValue != PayTableSettings.CommonRngBallCall.ConvertToB3StringValue())
                           {
                               setting.B3SettingDefaultValue = setting.B3SettingValue;
                               setting.B3SettingValue = PayTableSettings.CommonRngBallCall.ConvertToB3StringValue();
                               SettingHasChanged = true;
                           }
                          
                           break;
                       }
                   case B3SettingType.EnforceMix:
                       {
                           if (setting.B3SettingValue != PayTableSettings.EnforceMix.ConvertToB3StringValue())
                           {
                               setting.B3SettingDefaultValue = setting.B3SettingValue;
                               setting.B3SettingValue = PayTableSettings.EnforceMix.ConvertToB3StringValue();
                               SettingHasChanged = true;
                           }

                           break;
                       }
               }
           }
       }

       public List<B3SettingGlobal> Save()
       {
           UpdateModelToSettingsList();
           return m_originalPayTableSettings;
       }

       public void ResetSettingsToDefault()
       {
           UpdateSettingsListToModel(m_originalPayTableSettings);
       }      
    }
}
