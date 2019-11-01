using System;
using System.Collections.Generic;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
   public class ServerSettingVm : ViewModelBase
   {
       #region Fields
       private ServerSetting m_serverSetting;
       private readonly List<B3SettingGlobal> m_origianlServerSettings; 
       #endregion

       #region Constructor
       public ServerSettingVm(List<B3SettingGlobal> serversettingsList)
       {
           ServerSettings = new ServerSetting();
           UpdateSettingsListToModel(serversettingsList);
           m_origianlServerSettings = serversettingsList;

       }
       #endregion

       #region Properties

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

       #endregion

       #region Methods

       private void UpdateSettingsListToModel(List<B3SettingGlobal> settingsList)
       {
           foreach (var setting in settingsList)
           {             
              switch (setting.SettingType)
               {
                   case B3SettingType.MinPlayer:
                       ServerSettings.MinPlayer = setting.B3SettingValue;
                       if (int.Parse(ServerSettings.MinPlayer) > 1)
                       {
                           ServerSettings.IsMultiplayerMode = true;
                           ServerSettings.IsSinglePlayerMode = false;
                       }
                       else
                       {
                           ServerSettings.IsMultiplayerMode = false;
                           ServerSettings.IsSinglePlayerMode = true;
                       }
                       break;
                   case B3SettingType.GameStartDelay:
                       ServerSettings.GameStartDelay = setting.B3SettingValue;
                       break;
                   case B3SettingType.ConsolotionPrize:
                       ServerSettings.ConsolationPrize = setting.B3SettingValue;
                       break;
                   case B3SettingType.GameRecallPass:
                       ServerSettings.GameRecallPassword = setting.B3SettingValue;
                       break;
                   case B3SettingType.WaitCountDown:
                       ServerSettings.WaitCountDown = setting.B3SettingValue;
                       break;
                  case B3SettingType.HandpayByPattern:
                       ServerSettings.IsHandPayCalculateByPattern = setting.ConvertB3StringValueToBool();
                       ServerSettings.IsHandPayCalculateByGame = !ServerSettings.IsHandPayCalculateByPattern;
                       break;
                  case B3SettingType.RfRequiredForPlay:
                       ServerSettings.RfRequiredForPlayTimeout = setting.B3SettingValue;
                       break;
                  case B3SettingType.GameThread:
                       ServerSettings.GameThread = setting.B3SettingValue;
                       break;
               }
           }
       }

       private void UpdateModelToSettingsList()
       {
            foreach (var setting in m_origianlServerSettings)
           {
               setting.HasChanged = false;
               var tempOldSettingValue = setting.B3SettingValue;//saved current setting value
               switch (setting.SettingType)
               {
                   case B3SettingType.MinPlayer:
                       setting.B3SettingValue = ServerSettings.MinPlayer;
                       break;
                   case B3SettingType.GameStartDelay:
                       setting.B3SettingValue = ServerSettings.GameStartDelay;
                       break;
                   case B3SettingType.ConsolotionPrize:
                      setting.B3SettingValue = ServerSettings.ConsolationPrize;
                       break;
                   case B3SettingType.GameRecallPass:
                       setting.B3SettingValue = ServerSettings.GameRecallPassword;
                       break;
                   case B3SettingType.WaitCountDown:
                       setting.B3SettingValue = ServerSettings.WaitCountDown;
                       break;
                   case B3SettingType.HandpayByPattern:
                       setting.B3SettingValue = ServerSettings.IsHandPayCalculateByPattern.ConvertToB3StringValue();
                       break;
                   case B3SettingType.RfRequiredForPlay:
                       setting.B3SettingValue = ServerSettings.RfRequiredForPlayTimeout;
                       break;
                   case B3SettingType.GameThread:
                       setting.B3SettingValue = ServerSettings.GameThread;
                       break;
               }

               if (tempOldSettingValue != setting.B3SettingValue)//check if current = new setting
               {
                   setting.B3SettingDefaultValue = tempOldSettingValue;
                   setting.HasChanged = true;
               }
            }
        }


        public List<B3SettingGlobal> Save()
       {
           UpdateModelToSettingsList();
           return m_origianlServerSettings;
       }

       public void ResetSettingsToDefault()
       {
           UpdateSettingsListToModel(m_origianlServerSettings);
       }
       #endregion

          
    }
}
