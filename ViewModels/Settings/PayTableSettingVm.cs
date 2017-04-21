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

        private readonly List<B3SettingGlobal> m_originalPayTableSettings;

        private bool m_BindingTest;
        public bool BindingTest 
        {
            get { return PayTableSettings.CommonRngBallCall; }
            set
            {
                m_BindingTest = PayTableSettings.CommonRngBallCall;
                RaisePropertyChanged("BindingTest");
            }
        }

       public PayTableSettingVm(List<B3SettingGlobal> payTableSettingList)
        {
            AssignColumnDefWidth();
            PayTableSettings = new PayTableSetting();
            UpdateSettingsListToModel(payTableSettingList);
            m_originalPayTableSettings = payTableSettingList;
            BindingTest = true;
        }


       private void UpdateSettingsListToModel(List<B3SettingGlobal> settingsList)
       {
           foreach (var setting in settingsList)
           {
               switch (setting.SettingType)
               {
                  
                   case B3SettingType.CommonRngBallCall:
                       PayTableSettings.CommonRngBallCall = setting.ConvertB3StringValueToBool();
                       break;
                  
               }
           }
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
