
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using GameTech.Elite.Client.Modules.B3Center.UI.SettingViews.PayTable;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
   public class PayTableSettingVm : ViewModelBase
    {
        private PayTableSetting m_PayTableSettings; 
        private readonly List<B3SettingGlobal> m_originalPayTableSettings;
        private bool m_isRNGEnable;
        private bool m_enforceMixEnable;

        #region CONSTRUCTOR
        public PayTableSettingVm(List<B3SettingGlobal> payTableSettingList)
       {
           ListGamePayTableVm = new List<GamePayTableVm>();
           PayTableSettings = new PayTableSetting();
            UpdateEnableB3GameSettingToModel();
            UpdateSettingsListToModel(payTableSettingList);         
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
           gamePayTableVm.UpdateMathPayTableUI(IsRNG);
       }

        private void UpdateSettingsListToModel(List<B3SettingGlobal> settingsList) 
       {        
           foreach (var setting in settingsList)
           {
               switch (setting.SettingType)
               {
                   case B3SettingType.CommonRngBallCall: PayTableSettings.CommonRngBallCall = setting.ConvertB3StringValueToBool();break;
                    case B3SettingType.EnforceMix: PayTableSettings.EnforceMix = setting.ConvertB3StringValueToBool(); break;
                    case B3SettingType.MathPayTableSetting:
                       {
                           var tempGameType = setting.GameType;
                                                     
                            switch (tempGameType)
                            {
                                case B3GameType.Crazybout:
                                    {
                                        if (CrazyBoutPayTableVm != null) { UpdateSettingPayTableUI(ListGamePayTableVm.Single(l => l == CrazyBoutPayTableVm)); break; }
                                        CrazyBoutPayTableVm = new GamePayTableVm(setting);
                                        CrazyBoutPayTableVm.IsGameEnable (PayTableSettings.CrazyboutGameSetting);
                                        ListGamePayTableVm.Add(CrazyBoutPayTableVm);
                                        break;
                                    }
                                case B3GameType.Jailbreak:
                                    {
                                        if (JailBreakPayTableVm != null) { UpdateSettingPayTableUI(ListGamePayTableVm.Single(l => l == JailBreakPayTableVm)); break; }
                                        JailBreakPayTableVm = new GamePayTableVm(setting);
                                        JailBreakPayTableVm.IsGameEnable(PayTableSettings.JailBreakGameSetting);
                                        ListGamePayTableVm.Add(JailBreakPayTableVm);
                                        break;
                                    }
                                case B3GameType.Mayamoney:
                                    {
                                        if (MayaMoneyPayTableVm != null) { UpdateSettingPayTableUI(ListGamePayTableVm.Single(l => l == MayaMoneyPayTableVm)); break; }
                                        MayaMoneyPayTableVm = new GamePayTableVm(setting);
                                        MayaMoneyPayTableVm.IsGameEnable(PayTableSettings.MayaMoneyGameSetting);
                                        ListGamePayTableVm.Add(MayaMoneyPayTableVm);
                                        break;
                                    }
                                case B3GameType.Spirit76:
                                    {
                                        if (Spirit76PayTableVm != null) { UpdateSettingPayTableUI(ListGamePayTableVm.Single(l => l == Spirit76PayTableVm)); break; }
                                        Spirit76PayTableVm = new GamePayTableVm(setting);
                                        Spirit76PayTableVm.IsGameEnable(PayTableSettings.Spirit76GameSetting);
                                        ListGamePayTableVm.Add(Spirit76PayTableVm);
                                        break;
                                    }
                                case B3GameType.Timebomb:
                                    {
                                        if (TimeBombPayTableVm != null) { UpdateSettingPayTableUI(ListGamePayTableVm.Single(l => l == TimeBombPayTableVm)); break; }
                                        TimeBombPayTableVm = new GamePayTableVm(setting);
                                        TimeBombPayTableVm.IsGameEnable( PayTableSettings.TimeBombGameSetting);
                                        ListGamePayTableVm.Add(TimeBombPayTableVm);
                                        break;
                                    }
                                case B3GameType.Ukickem:
                                    {
                                        if (UkickEmPayTableVm != null) { UpdateSettingPayTableUI(ListGamePayTableVm.Single(l => l == UkickEmPayTableVm)); break; }
                                        UkickEmPayTableVm = new GamePayTableVm(setting);
                                        UkickEmPayTableVm.IsGameEnable( PayTableSettings.UKickemGameSetting);
                                        ListGamePayTableVm.Add(UkickEmPayTableVm);
                                        break;
                                    }
                                case B3GameType.Wildball:
                                    {
                                        if (WildBallPayTableVm != null) { UpdateSettingPayTableUI(ListGamePayTableVm.Single(l => l == WildBallPayTableVm)); break; }
                                        WildBallPayTableVm = new GamePayTableVm(setting);
                                        WildBallPayTableVm.IsGameEnable( PayTableSettings.WildBallGameSetting);
                                        ListGamePayTableVm.Add(WildBallPayTableVm);
                                        break;
                                    }
                                case B3GameType.Wildfire:
                                    {
                                        if (WildFirePayTableVm != null) { UpdateSettingPayTableUI(ListGamePayTableVm.Single(l => l == WildFirePayTableVm)); break; }
                                        WildFirePayTableVm = new GamePayTableVm(setting);
                                        WildFirePayTableVm.IsGameEnable( PayTableSettings.WildFireGameSetting);
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
                                        if (CrazyBoutPayTableVm.GamePayTableModel.MathPayValue != null
                                            && CrazyBoutPayTableVm.GamePayTableModel.MathPayValue.IsRng == IsRNG)
                                        {
                                            setting.B3SettingValue = CrazyBoutPayTableVm.GamePayTableModel.MathPayValue.MathPackageId.ToString();
                                        }
                                        break;
                                    }
                           
                                case B3GameType.Jailbreak:
                                    {
                                        if (JailBreakPayTableVm.GamePayTableModel.MathPayValue != null
                                                  && JailBreakPayTableVm.GamePayTableModel.MathPayValue.IsRng == IsRNG)
                                            setting.B3SettingValue = JailBreakPayTableVm.GamePayTableModel.MathPayValue.MathPackageId.ToString();

                                    }
                                    break;
                                case B3GameType.Mayamoney:
                                    {
                                        if (MayaMoneyPayTableVm.GamePayTableModel.MathPayValue != null
                                                  && MayaMoneyPayTableVm.GamePayTableModel.MathPayValue.IsRng == IsRNG)
                                            setting.B3SettingValue = MayaMoneyPayTableVm.GamePayTableModel.MathPayValue.MathPackageId.ToString();
                                    }
                                    break;
                                case B3GameType.Spirit76:
                                    {
                                        if (Spirit76PayTableVm.GamePayTableModel.MathPayValue != null
                                                  && Spirit76PayTableVm.GamePayTableModel.MathPayValue.IsRng == IsRNG)
                                            setting.B3SettingValue = Spirit76PayTableVm.GamePayTableModel.MathPayValue.MathPackageId.ToString();
                                    }
                                    break;
                                case B3GameType.Timebomb:
                                    {
                                        if (TimeBombPayTableVm.GamePayTableModel.MathPayValue != null
                                                  && TimeBombPayTableVm.GamePayTableModel.MathPayValue.IsRng == IsRNG)
                                            setting.B3SettingValue = TimeBombPayTableVm.GamePayTableModel.MathPayValue.MathPackageId.ToString();
                                    }
                                    break;
                                case B3GameType.Ukickem:
                                    {
                                        if (UkickEmPayTableVm.GamePayTableModel.MathPayValue != null
                                                  && UkickEmPayTableVm.GamePayTableModel.MathPayValue.IsRng == IsRNG)
                                            setting.B3SettingValue = UkickEmPayTableVm.GamePayTableModel.MathPayValue.MathPackageId.ToString();
                                    }
                                    break;
                                case B3GameType.Wildball:
                                    {
                                        if (WildBallPayTableVm.GamePayTableModel.MathPayValue != null
                                                  && WildBallPayTableVm.GamePayTableModel.MathPayValue.IsRng == IsRNG)
                                            setting.B3SettingValue = WildBallPayTableVm.GamePayTableModel.MathPayValue.MathPackageId.ToString();
                                    }
                                    break;
                                case B3GameType.Wildfire:
                                    {
                                        if (WildFirePayTableVm.GamePayTableModel.MathPayValue != null
                                                  && WildFirePayTableVm.GamePayTableModel.MathPayValue.IsRng == IsRNG)
                                            setting.B3SettingValue = WildFirePayTableVm.GamePayTableModel.MathPayValue.MathPackageId.ToString();
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
            CrazyBoutPayTableVm.UpdateMathPayTableUI(IsRNG);
            JailBreakPayTableVm.UpdateMathPayTableUI(IsRNG);
            MayaMoneyPayTableVm.UpdateMathPayTableUI(IsRNG);
            Spirit76PayTableVm.UpdateMathPayTableUI(IsRNG);
            TimeBombPayTableVm.UpdateMathPayTableUI(IsRNG);
            UkickEmPayTableVm.UpdateMathPayTableUI(IsRNG);
            WildBallPayTableVm.UpdateMathPayTableUI(IsRNG);
            WildFirePayTableVm.UpdateMathPayTableUI(IsRNG);
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
        public bool IsRNG { get { return !PayTableSettings.EnforceMix; } }

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
    }
}
