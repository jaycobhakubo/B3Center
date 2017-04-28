﻿using GameTech.Elite.Base;
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
       private readonly List<B3SettingGlobal> m_originalPayTableSettings;
       private bool m_isRNG;
        private bool m_isRNGEnable;
        private bool m_enforceMixEnable;
        #region CONSTRUCTOR
        public PayTableSettingVm(List<B3SettingGlobal> payTableSettingList)
       {
           ListGamePayTableVm = new List<GamePayTableVm>();
           PayTableSettings = new PayTableSetting();
           UpdateSettingsListToModel(payTableSettingList);
           UpdateEnableB3GameSettingToModel();
           m_originalPayTableSettings = payTableSettingList;
                 
       }
       #endregion

       private void UpdateEnableB3GameSettingToModel()
       {
           foreach (var enablesetting in SettingViewModel.Instance.GetAllB3GameEnableSetting())
           {
                 switch (enablesetting.GameType)
                {
                    case B3GameType.Crazybout:
                        PayTableSettings.CrazyboutGameSetting = enablesetting;
                            break;
                    case B3GameType.Jailbreak:
                            PayTableSettings.JailBreakGameSetting = enablesetting;
                            break;
                    case B3GameType.Mayamoney:
                            PayTableSettings.MayaMoneyGameSetting = enablesetting;
                            break;
                    case B3GameType.Spirit76:
                            PayTableSettings.Spirit76GameSetting = enablesetting;
                            break;
                    case B3GameType.Timebomb:
                            PayTableSettings.TimeBombGameSetting = enablesetting;
                            break;
                    case B3GameType.Ukickem:
                            PayTableSettings.UKickemGameSetting = enablesetting;
                            break;
                    case B3GameType.Wildball:
                            PayTableSettings.WildBallGameSetting = enablesetting;
                            break;
                    case B3GameType.Wildfire:
                            PayTableSettings.WildFireGameSetting = enablesetting;
                            break;
                }
            }           
       }

       private void UpdateSettingPayTableUI(GamePayTableVm gamePayTableVm)
       {
           gamePayTableVm.UpdateMathPayTableUI(m_isRNG);
       }

        private void UpdateSettingsListToModel(List<B3SettingGlobal> settingsList) 
       {        
           foreach (var setting in settingsList)
           {
               switch (setting.SettingType)
               {
                   case B3SettingType.CommonRngBallCall:
                       {
                           PayTableSettings.CommonRngBallCall = setting.ConvertB3StringValueToBool();
                           m_isRNG = PayTableSettings.CommonRngBallCall;                         
                           break;
                       }
                   case B3SettingType.EnforceMix:
                       {                     
                           PayTableSettings.EnforceMix = setting.ConvertB3StringValueToBool();                        
                            IsRNGEnable = !EnforceMix;
                            if (EnforceMix)//setting.ConvertB3StringValueToBool();)
                            {
                                PayTableSettings.CommonRngBallCall = IsRNGEnable;
                            }
                            break;
                       }
                    case B3SettingType.MathPayTableSetting:
                       {
                           var tempGameType = setting.GameType;
                                                     
                            switch (tempGameType)
                            {
                                case B3GameType.Crazybout:
                                    {
                                        if (CrazyBoutPayTableVm != null) { UpdateSettingPayTableUI(ListGamePayTableVm.Single(l => l == CrazyBoutPayTableVm)); break; }
                                        CrazyBoutPayTableVm = new GamePayTableVm(setting);
                                        ListGamePayTableVm.Add(CrazyBoutPayTableVm);
                                        break;
                                    }
                                case B3GameType.Jailbreak:
                                    {
                                        if (JailBreakPayTableVm != null) { UpdateSettingPayTableUI(ListGamePayTableVm.Single(l => l == JailBreakPayTableVm)); break; }
                                        JailBreakPayTableVm = new GamePayTableVm(setting);
                                        ListGamePayTableVm.Add(JailBreakPayTableVm);
                                        break;
                                    }
                                case B3GameType.Mayamoney:
                                    {
                                        if (MayaMoneyPayTableVm != null) { UpdateSettingPayTableUI(ListGamePayTableVm.Single(l => l == MayaMoneyPayTableVm)); break; }
                                        MayaMoneyPayTableVm = new GamePayTableVm(setting);
                                        ListGamePayTableVm.Add(MayaMoneyPayTableVm);
                                        break;
                                    }
                                case B3GameType.Spirit76:
                                    {
                                        if (Spirit76PayTableVm != null) { UpdateSettingPayTableUI(ListGamePayTableVm.Single(l => l == Spirit76PayTableVm)); break; }
                                        Spirit76PayTableVm = new GamePayTableVm(setting);
                                        ListGamePayTableVm.Add(Spirit76PayTableVm);
                                        break;
                                    }
                                case B3GameType.Timebomb:
                                    {
                                        if (TimeBombPayTableVm != null) { UpdateSettingPayTableUI(ListGamePayTableVm.Single(l => l == TimeBombPayTableVm)); break; }
                                        TimeBombPayTableVm = new GamePayTableVm(setting);
                                        ListGamePayTableVm.Add(TimeBombPayTableVm);
                                        break;
                                    }
                                case B3GameType.Ukickem:
                                    {
                                        if (UkickEmPayTableVm != null) { UpdateSettingPayTableUI(ListGamePayTableVm.Single(l => l == UkickEmPayTableVm)); break; }
                                        UkickEmPayTableVm = new GamePayTableVm(setting);
                                        ListGamePayTableVm.Add(UkickEmPayTableVm);
                                        break;
                                    }
                                case B3GameType.Wildball:
                                    {
                                        if (WildBallPayTableVm != null) { UpdateSettingPayTableUI(ListGamePayTableVm.Single(l => l == WildBallPayTableVm)); break; }
                                        WildBallPayTableVm = new GamePayTableVm(setting);
                                        ListGamePayTableVm.Add(WildBallPayTableVm);
                                        break;
                                    }
                                case B3GameType.Wildfire:
                                    {
                                        if (WildFirePayTableVm != null) { UpdateSettingPayTableUI(ListGamePayTableVm.Single(l => l == WildFirePayTableVm)); break; }
                                        WildFirePayTableVm = new GamePayTableVm(setting);
                                        ListGamePayTableVm.Add(WildFirePayTableVm);
                                        break;
                                    }
                            }
                            break;                                                      
                    }                 
               }
           }
       }

       private void UpdateModelToSettingsList()
       {
    
           foreach (var setting in m_originalPayTableSettings)
           {
                setting.HasChanged = false;
                var tempOldSettingValue = setting.B3SettingValue;//saved current setting value

               switch (setting.SettingType)
               {
                        case B3SettingType.CommonRngBallCall:
                       {                                               
                               setting.B3SettingValue = PayTableSettings.CommonRngBallCall.ConvertToB3StringValue();
                                m_isRNG = (setting.B3SettingValue == "T") ? true : false;                         
                           break;
                       }
                        case B3SettingType.EnforceMix:
                       {
                           setting.B3SettingValue = PayTableSettings.EnforceMix.ConvertToB3StringValue(); break;
                       }                    
                        case B3SettingType.MathPayTableSetting:
                       {
                            var tempGameType = setting.GameType;
                                  
                            switch (tempGameType)
                            {
                                case B3GameType.Crazybout:
                                    {
                                        if (CrazyBoutPayTableVm.GamePayTableModel.MathPayTable != null
                                            && CrazyBoutPayTableVm.GamePayTableModel.MathPayTable.IsRng == m_isRNG)
                                        {
                                            setting.B3SettingValue = CrazyBoutPayTableVm.GamePayTableModel.MathPayTable.MathPackageId.ToString();
                                        }
                                        break;
                                    }
                           
                                case B3GameType.Jailbreak:
                                    {
                                        if (JailBreakPayTableVm.GamePayTableModel.MathPayTable != null
                                                  && JailBreakPayTableVm.GamePayTableModel.MathPayTable.IsRng == m_isRNG)
                                            setting.B3SettingValue = JailBreakPayTableVm.GamePayTableModel.MathPayTable.MathPackageId.ToString();

                                    }
                                    break;
                                case B3GameType.Mayamoney:
                                    {
                                        if (MayaMoneyPayTableVm.GamePayTableModel.MathPayTable != null
                                                  && MayaMoneyPayTableVm.GamePayTableModel.MathPayTable.IsRng == m_isRNG)
                                            setting.B3SettingValue = MayaMoneyPayTableVm.GamePayTableModel.MathPayTable.MathPackageId.ToString();
                                    }
                                    break;
                                case B3GameType.Spirit76:
                                    {
                                        if (Spirit76PayTableVm.GamePayTableModel.MathPayTable != null
                                                  && Spirit76PayTableVm.GamePayTableModel.MathPayTable.IsRng == m_isRNG)
                                            setting.B3SettingValue = Spirit76PayTableVm.GamePayTableModel.MathPayTable.MathPackageId.ToString();
                                    }
                                    break;
                                case B3GameType.Timebomb:
                                    {
                                        if (TimeBombPayTableVm.GamePayTableModel.MathPayTable != null
                                                  && TimeBombPayTableVm.GamePayTableModel.MathPayTable.IsRng == m_isRNG)
                                            setting.B3SettingValue = TimeBombPayTableVm.GamePayTableModel.MathPayTable.MathPackageId.ToString();
                                    }
                                    break;
                                case B3GameType.Ukickem:
                                    {
                                        if (UkickEmPayTableVm.GamePayTableModel.MathPayTable != null
                                                  && UkickEmPayTableVm.GamePayTableModel.MathPayTable.IsRng == m_isRNG)
                                            setting.B3SettingValue = UkickEmPayTableVm.GamePayTableModel.MathPayTable.MathPackageId.ToString();
                                    }
                                    break;
                                case B3GameType.Wildball:
                                    {
                                        if (WildBallPayTableVm.GamePayTableModel.MathPayTable != null
                                                  && WildBallPayTableVm.GamePayTableModel.MathPayTable.IsRng == m_isRNG)
                                            setting.B3SettingValue = WildBallPayTableVm.GamePayTableModel.MathPayTable.MathPackageId.ToString();
                                    }
                                    break;
                                case B3GameType.Wildfire:
                                    {
                                        if (WildFirePayTableVm.GamePayTableModel.MathPayTable != null
                                                  && WildFirePayTableVm.GamePayTableModel.MathPayTable.IsRng == m_isRNG)
                                            setting.B3SettingValue = WildFirePayTableVm.GamePayTableModel.MathPayTable.MathPackageId.ToString();
                                    }
                                    break;
                            }
                            
                            break;
                        }                  
                }
               if (tempOldSettingValue != setting.B3SettingValue)//check if current = new setting
               {
                   setting.B3SettingDefaultValue = tempOldSettingValue;
                   setting.HasChanged = true;
               }
           }
       }

       public void IsRngCheckEvent()
        {
            m_isRNG = PayTableSettings.CommonRngBallCall;
    
            IsRNGEnable = !EnforceMix;

            if (EnforceMix)
            {
                PayTableSettings.CommonRngBallCall = m_isRNGEnable;
            }
            else
            {
                PayTableSettings.CommonRngBallCall = (m_originalPayTableSettings.Single(l => l.SettingType == B3SettingType.CommonRngBallCall).B3SettingValue) == "T" ? true : false;
            }

            CrazyBoutPayTableVm.UpdateMathPayTableUI(m_isRNG);
            JailBreakPayTableVm.UpdateMathPayTableUI(m_isRNG);
            MayaMoneyPayTableVm.UpdateMathPayTableUI(m_isRNG);
            Spirit76PayTableVm.UpdateMathPayTableUI(m_isRNG);
            TimeBombPayTableVm.UpdateMathPayTableUI(m_isRNG);
            UkickEmPayTableVm.UpdateMathPayTableUI(m_isRNG);
            WildBallPayTableVm.UpdateMathPayTableUI(m_isRNG);
            WildFirePayTableVm.UpdateMathPayTableUI(m_isRNG);
        }




       public List<B3SettingGlobal> Save()
       {
           UpdateModelToSettingsList();
           return m_originalPayTableSettings.Where(l => l.HasChanged == true).ToList();
       }

       public void ValidateUserInput()
       {
           var x = ListGamePayTableVm.Exists(l => l.changeme == true);
           if (x == true)
           {
               SettingViewModel.Instance.BtnSaveIsEnabled = false;
           }
           else
           {
               SettingViewModel.Instance.BtnSaveIsEnabled = true;
           }
          
       }

       public void ResetSettingsToDefault()
       {
           UpdateSettingsListToModel(m_originalPayTableSettings);
       }

       public bool HasChanged { get; set; }
       public List<GamePayTableVm> ListGamePayTableVm { get; set; }//not used 
       public GamePayTableVm CrazyBoutPayTableVm { get; set; }
       public GamePayTableVm JailBreakPayTableVm { get; set; }
       public GamePayTableVm MayaMoneyPayTableVm { get; set; }
       public GamePayTableVm Spirit76PayTableVm { get; set; }
       public GamePayTableVm TimeBombPayTableVm { get; set; }
       public GamePayTableVm UkickEmPayTableVm { get; set; }
       public GamePayTableVm WildBallPayTableVm { get; set; }
       public GamePayTableVm WildFirePayTableVm { get; set; }

        public bool EnforceMix { get { return PayTableSettings.EnforceMix; } }

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

        public bool EnforceMixEnable
        {
            get
            {
                return m_enforceMixEnable;
            }
            set
            {
                m_enforceMixEnable = value;
                RaisePropertyChanged("EnforceMixEnable");
            }
        }

        public bool IsRNGEnable
        {
           get
           {
               return m_isRNGEnable;
           }
           set
           {
                m_isRNGEnable = value;
               RaisePropertyChanged("IsRNGEnable");
           }
       }
    }
}
