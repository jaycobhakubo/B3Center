using GameTech.Elite.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
   public class PayTableSettingVm : ViewModelBase
    {
        public GridLength grdColumnB3GameName { get; set; }
        public GridLength grdColumnPayTableSetting { get; set; }

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
            AssignColumnDefWidth();
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
                           foreach (var gameTypeObj in Enum.GetValues(typeof(B3GameType)))
                           {
                               var gameType = (B3GameType)gameTypeObj;
                               PayTableSettings.LGamePayTable = SettingViewModel.Instance.GetB3MathGamePlay(gameType);
                           }
                           break;
                       }
                  
               }
           }
       }

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

       private void AssignColumnDefWidth()
   {
       var grdlength = new GridLength(1, GridUnitType.Star);
       grdColumnB3GameName = grdlength;
       RaisePropertyChanged("grdColumnB3GameName");
       grdlength = new GridLength(3, GridUnitType.Star);
       grdColumnPayTableSetting = grdlength;
       RaisePropertyChanged("grdColumnPayTableSetting");
   }

      
    }
}
