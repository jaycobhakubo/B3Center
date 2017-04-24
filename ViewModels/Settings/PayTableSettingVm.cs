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
                setting.HasChanged = false;
               switch (setting.SettingType)
               {
                   case B3SettingType.CommonRngBallCall:
                       {
                            setting.B3SettingDefaultValue = setting.B3SettingValue;
                            if (setting.B3SettingValue != PayTableSettings.CommonRngBallCall.ConvertToB3StringValue())
                            {
                               setting.HasChanged = true;                          
                               setting.B3SettingValue = PayTableSettings.CommonRngBallCall.ConvertToB3StringValue();
                               SettingHasChanged = true;
                           }
                          
                           break;
                       }
                   case B3SettingType.EnforceMix:
                       {
                            setting.B3SettingDefaultValue = setting.B3SettingValue;
                            if (setting.B3SettingValue != PayTableSettings.EnforceMix.ConvertToB3StringValue())
                            {
                                setting.HasChanged = true;
                                setting.B3SettingValue = PayTableSettings.EnforceMix.ConvertToB3StringValue();
                               SettingHasChanged = true;
                           }

                           break;
                       }
                    case B3SettingType.MathPayTableSetting:
                        {
                            var tempGameType = setting.GameType;
                            var settingvalue = setting.B3SettingValue;                          
                            m_isRNG = PayTableSettings.CommonRngBallCall;
                            switch (tempGameType)
                            {
                                case B3GameType.Crazybout:
                                    {
                                        if (CrazyBoutPayTableVm.GamePayTableModel.MathPayTable != null 
                                            && CrazyBoutPayTableVm.GamePayTableModel.MathPayTable.IsRng == m_isRNG)
                                        { settingvalue = CrazyBoutPayTableVm.GamePayTableModel.MathPayTable.MathPackageId.ToString(); }

                                    }
                                    break;
                                case B3GameType.Jailbreak:
                                    {
                                        if (JailBreakPayTableVm.GamePayTableModel.MathPayTable != null
                                                  && JailBreakPayTableVm.GamePayTableModel.MathPayTable.IsRng == m_isRNG)
                                            settingvalue = JailBreakPayTableVm.GamePayTableModel.MathPayTable.MathPackageId.ToString();

                                    }
                                    break;
                                case B3GameType.Mayamoney:
                                    {
                                        if (MayaMoneyPayTableVm.GamePayTableModel.MathPayTable != null
                                                  && MayaMoneyPayTableVm.GamePayTableModel.MathPayTable.IsRng == m_isRNG)
                                            settingvalue = MayaMoneyPayTableVm.GamePayTableModel.MathPayTable.MathPackageId.ToString();

                                    }
                                    break;
                                case B3GameType.Spirit76:
                                    {
                                        if (Spirit76PayTableVm.GamePayTableModel.MathPayTable != null
                                                  && Spirit76PayTableVm.GamePayTableModel.MathPayTable.IsRng == m_isRNG)
                                            settingvalue = Spirit76PayTableVm.GamePayTableModel.MathPayTable.MathPackageId.ToString();
                                    }
                                    break;
                                case B3GameType.Timebomb:
                                    {
                                        if (TimeBombPayTableVm.GamePayTableModel.MathPayTable != null
                                                  && TimeBombPayTableVm.GamePayTableModel.MathPayTable.IsRng == m_isRNG)
                                            settingvalue = TimeBombPayTableVm.GamePayTableModel.MathPayTable.MathPackageId.ToString();
                                    }
                                    break;
                                case B3GameType.Ukickem:
                                    {
                                        if (UkickEmPayTableVm.GamePayTableModel.MathPayTable != null
                                                  && UkickEmPayTableVm.GamePayTableModel.MathPayTable.IsRng == m_isRNG)
                                            settingvalue = UkickEmPayTableVm.GamePayTableModel.MathPayTable.MathPackageId.ToString();
                                    }
                                    break;
                                case B3GameType.Wildball:
                                    {
                                        if (WildBallPayTableVm.GamePayTableModel.MathPayTable != null
                                                  && WildBallPayTableVm.GamePayTableModel.MathPayTable.IsRng == m_isRNG)
                                            settingvalue = WildBallPayTableVm.GamePayTableModel.MathPayTable.MathPackageId.ToString();
                                    }
                                    break;
                                case B3GameType.Wildfire:
                                    {
                                        if (WildFirePayTableVm.GamePayTableModel.MathPayTable != null
                                                  && WildFirePayTableVm.GamePayTableModel.MathPayTable.IsRng == m_isRNG)
                                            settingvalue = WildFirePayTableVm.GamePayTableModel.MathPayTable.MathPackageId.ToString();
                                    }
                                    break;
                            }
                            if (setting.B3SettingValue != settingvalue && setting.GameType != 0)
                            {
                                setting.HasChanged = true;
                                setting.B3SettingDefaultValue = setting.B3SettingValue;
                                setting.B3SettingValue = settingvalue;
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
           return m_originalPayTableSettings.Where(l => l.HasChanged == true).ToList();
       }

       public void ResetSettingsToDefault()
       {
           UpdateSettingsListToModel(m_originalPayTableSettings);
       }      
    }
}
