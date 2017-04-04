#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply: © 2015 GameTech International, Inc.
#endregion

using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Globalization;
using GameTech.Elite.Base;
using GameTech.Elite.UI;
using GameTech.Elite.Client.Modules.B3Center.Business;
using System.Linq;
using GameTech.Elite.Client.Modules.B3Center.UI.SettingViews;
using System.Windows.Controls;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using GameTech.Elite.Client.Modules.B3Center.Messages;
using System.Threading.Tasks;
using GameTech.Elite.Client.Modules.B3Center.Model;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{
    class SettingViewModel : ViewModelBase
    {     
        #region VAR
        //Parent
        private B3Controller m_controller;
        //Views
        private GameSettingView m_gameSettingView;
        private SystemSettingView m_systemSettingView;
        private ServerGameSettingView m_serverGameSettingView;
        private SalesSettingView m_salesSettingView;
        private PlayerSettingView m_playerSettingView;
        private SessionSettingView m_sessionSettingView;
        private ObservableCollection<B3SettingGlobal> B3Setting { get; set; }
        private ObservableCollection<B3GameSetting> B3SettingEnableDisable { get; set; }
        //Model
        private ServerSetting m_serverSetting;
        private SessionSetting m_sessionSetting;
        private SalesSettings m_salesSetting;
        private PlayerSettings m_playerSetting;
        private SystemSetting m_systemSetting;
        //private GameSetting m_gameSetting;
        //Other
        private ObservableCollection<B3SettingGlobal> m_settingTobeSaved;
        private int m_selectedSettingEquivToId;
        private bool m_isRngBallCall;
        private Dictionary<string, int> m_b3SettingCategory;//Matches the primary key of B3Settingcategory
        private SetModelDefaultValue m_modelDefValue;

        #endregion
        #region CONSTRUCTOR
        private SettingViewModel()
        {
        }

        public void Initialize(B3Controller controller)
        {
            if (controller == null)
                throw new ArgumentNullException();

            m_controller = controller;
             B3SettingEnableDisable = new ObservableCollection<B3GameSetting>(m_controller.Settings.B3GameSettings);    

            SetCommand();
            SetDefaultValue();
            LoadSetting();
            BtnSaveIsEnabled = true;
        }
        #endregion
        #region OTHER ACCESSOR (static)
        //This will access anything that is public on this View Model.
        private static volatile SettingViewModel m_instance;
        private static readonly object m_syncRoot = new object();
        public static SettingViewModel Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_syncRoot)
                    {
                        if (m_instance == null)
                            m_instance = new SettingViewModel();
                    }
                }
                return m_instance;
            }
        }
        #endregion
        #region METHOD

        #region (Convert to our model class assign to a specific setting.)

        private void ConvertSettingToModel()
        {
            switch (m_selectedSettingEquivToId)
            {
                case 3:
                    {
                        m_playerSetting = new PlayerSettings();
                        var tempBool = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.PlayerCalibrateTouch)).B3SettingValue;
                        m_playerSetting.PlayerCalibrateTouch = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.PresstoCollect)).B3SettingValue;
                        m_playerSetting.PresstoCollect = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.AnnounceCall)).B3SettingValue;
                        m_playerSetting.AnnounceCall = tempBool != "F";               
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.PlayerScreenCursor)).B3SettingValue;
                        m_playerSetting.PlayerScreenCursor = tempBool != "F";                   
                        m_playerSetting.TimeToCollect = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.TimeToCollect)).B3SettingValue;                   
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.Disclaimer)).B3SettingValue;
                        m_playerSetting.Disclaimer = tempBool != "F";                                                      
                        var volumeSales = Convert.ToInt32(B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.PlayerMainVolume)).B3SettingValue);
                        m_playerSetting.PlayerMainVolume = GetVolumeEquivValue(volumeSales);

                        break;
                    }

                case 4:
                    {
                        m_salesSetting = new SalesSettings();
                        var tempBool = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.ScreenCursor)).B3SettingValue;
                        m_salesSetting.ScreenCursor = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.CalibrateTouch)).B3SettingValue;
                        m_salesSetting.CalibrateTouch = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.AutoPrintSessionReport)).B3SettingValue;
                        m_salesSetting.AutoPrintSessionReport = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.PagePrinter)).B3SettingValue;
                        m_salesSetting.PagePrinter = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.QuickSales)).B3SettingValue;
                        m_salesSetting.QuickSales = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.PrintLogo)).B3SettingValue;
                        m_salesSetting.PrintLogo = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.AlowinSessionBall)).B3SettingValue;
                        m_salesSetting.AlowinSessionBall = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.LoggingEnable)).B3SettingValue;
                        m_salesSetting.LoggingEnable = tempBool != "F";
                        m_salesSetting.LogRecycleDays = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.LogRecycleDays)).B3SettingValue;
                        var volumeSales  =  Convert.ToInt32 (B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.VolumeSales)).B3SettingValue);
                        m_salesSetting.VolumeSales = GetVolumeEquivValue(volumeSales);
                        break; }
                case 5:
                    {
                        m_serverSetting = new ServerSetting
                        {
                            MinPlayer =
                                B3Setting.Single(
                                    l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.MinPlayer))
                                    .B3SettingValue,
                            GameStartDelay =
                                B3Setting.Single(
                                    l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.GameStartDelay))
                                    .B3SettingValue,
                            Consolation =
                                B3Setting.Single(
                                    l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.ConsolotionPrize))
                                    .B3SettingValue,
                            GameRecallPassw =
                                B3Setting.Single(
                                    l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.GameRecallPass))
                                    .B3SettingValue,
                            WaitCountDown =
                                B3Setting.Single(
                                    l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.WaiCountDown))
                                    .B3SettingValue
                        };
                        break;
                    }
                case 6:
                    {
                        m_sessionSetting = new SessionSetting
                        {
                            PayoutLimit =
                                B3Setting.Single(
                                    l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.PayoutLimit))
                                    .B3SettingValue,
                            JackpotLimit =
                                B3Setting.Single(
                                    l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.JackpotLimit))
                                    .B3SettingValue
                        };

                        var tempBool = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.EnforceMix)).B3SettingValue;
                        m_sessionSetting.EnforceMix = tempBool != "F";
                        break;
                    }
                case 7:
                    {
                        m_systemSetting = new SystemSetting();
                        var tempBool = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.EnableUk)).B3SettingValue; m_systemSetting.EnableUk = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.DualAccount)).B3SettingValue; m_systemSetting.DualAccount = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.MultiOperator)).B3SettingValue; m_systemSetting.MultiOperator = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.CommonRngBallCall)).B3SettingValue; m_systemSetting.CommonRngBallCall = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.NorthDakotaMode)).B3SettingValue; m_systemSetting.NorthDakotaMode = tempBool != "F";
                        m_systemSetting.HandPayTrigger = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.HandPayTrigger)).B3SettingValue;
                        m_systemSetting.MinimumPlayers = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.MinimumPlayers)).B3SettingValue;
                        m_systemSetting.VipPointMultiplier = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.VipPointMultiplier)).B3SettingValue;
                        m_systemSetting.MagCardSentinelStart = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.MagCardSentinelStart)).B3SettingValue;
                        m_systemSetting.MagCardSentinelEnd = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.MagCardSentinelEnd)).B3SettingValue;
                        m_systemSetting.Currency = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.Currency)).B3SettingValue;
                        m_systemSetting.RngBallCallTime = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.RngBallCallTime)).B3SettingValue;
                        m_systemSetting.PlayerPinLength = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.PlayerPinLength)).B3SettingValue;
                        m_systemSetting.AutoSessionEnd = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.AutoSessionEnd)).B3SettingValue;
                        m_systemSetting.SiteName = B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.SiteName)).B3SettingValue;
                        var volumeSales = Convert.ToInt32(B3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.SystemMainVolume)).B3SettingValue);
                        m_systemSetting.SystemMainVolume = GetVolumeEquivValue(volumeSales);
                        break;
                    }
            }
        }

        #endregion
        #region (Assign new value)

        private void SetNewValue()
        {
            IEnumerable<B3SettingGlobal> b3Setting;
            switch (m_selectedSettingEquivToId)
            {
                case 1:
                    {
                        GameSetting gameSettingNewValue = GameSettingsVm.SelectedGameVm.Settings;
                        b3Setting = B3Setting.Where(l => l.B3GameId == gameSettingNewValue.GameId);

                        foreach (B3SettingGlobal sg in b3Setting)
                        {
                            switch (sg.B3SettingId)
                            {
                                case 1:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.Denom1;
                                        break;
                                    }
                                case 2:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.Denom5;
                                        break;
                                    }
                                case 3:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.Denom10;
                                        break;
                                    }
                                case 4:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.Denom25;
                                        break;
                                    }
                                case 5:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.Denom50;
                                        break;
                                    }
                                case 6:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.Denom100;
                                        break;
                                    }
                                case 7:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.Denom200;
                                        break;
                                    }
                                case 8:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.Denom500;
                                        break;
                                    }
                                case 9:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.MaxBetLevel;
                                        break;
                                    }
                                case 10:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.MaxCards;
                                        break;
                                    }
                                case 11:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.CallSpeed;
                                        break;
                                    }
                                case 12:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.AutoCall ? "T" : "F";
                                        break;
                                    }
                                case 13:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.AutoPlay ? "T" : "F";
                                        break;
                                    }
                                case 14:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.HideSerialNumber ? "T" : "F";
                                        break;
                                    }
                                case 15:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.SingleOfferBonus ? "T" : "F";
                                        break;
                                    }
                                case 58:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.SelectedMathPayTableSettingInt.ToString();
                                        break;
                                    }
                                case 59:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.CallSpeedMin;
                                        break;
                                    }
                                case 60:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.CallSpeedBonus;
                                        break;
                                    }
                            }
                        }
                        m_settingTobeSaved = new ObservableCollection<B3SettingGlobal>(b3Setting);
                        break;
                    }
                case 3:
                    {
                        PlayerSettings playerSettingNewValue = PlayerSettingVm.PlayerSetting;
                        b3Setting = B3Setting;//.Where(l => l.B3SettingID == _GameSettingNewValue.);
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.PlayerCalibrateTouch)).B3SettingValue = playerSettingNewValue.PlayerCalibrateTouch ? "T" : "F";
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.PresstoCollect)).B3SettingValue = playerSettingNewValue.PresstoCollect ? "T" : "F";
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.AnnounceCall)).B3SettingValue = playerSettingNewValue.AnnounceCall ? "T" : "F";
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.PlayerScreenCursor)).B3SettingValue = playerSettingNewValue.PlayerScreenCursor ? "T" : "F";
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.TimeToCollect)).B3SettingValue = playerSettingNewValue.TimeToCollect;
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.Disclaimer)).B3SettingValue = playerSettingNewValue.Disclaimer ? "T" : "F";
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.PlayerMainVolume)).B3SettingValue = GetVolumeEquivToDb(Convert.ToInt32(playerSettingNewValue.PlayerMainVolume));
                        m_settingTobeSaved = new ObservableCollection<B3SettingGlobal>(b3Setting);
                      
                        break;
                    }
                case 4:
                    {
                        SalesSettings salesSettingNewValue = SalesSettingVm.SalesSetting;
                        b3Setting = B3Setting; //_m_b3Setting = m_b3Setting.Where(l => l.B3SettingID == (int)B3SettingCategory.Sales);
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.ScreenCursor)).B3SettingValue = salesSettingNewValue.ScreenCursor ? "T" : "F";
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.CalibrateTouch)).B3SettingValue = salesSettingNewValue.CalibrateTouch ? "T" : "F";
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.AutoPrintSessionReport)).B3SettingValue = salesSettingNewValue.AutoPrintSessionReport ? "T" : "F";
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.PagePrinter)).B3SettingValue = salesSettingNewValue.PagePrinter ? "T" : "F";
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.QuickSales)).B3SettingValue = salesSettingNewValue.QuickSales ? "T" : "F";
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.PrintLogo)).B3SettingValue = salesSettingNewValue.PrintLogo ? "T" : "F";
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.AlowinSessionBall)).B3SettingValue = salesSettingNewValue.AlowinSessionBall ? "T" : "F";
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.LoggingEnable)).B3SettingValue = salesSettingNewValue.LoggingEnable ? "T" : "F";
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.LogRecycleDays)).B3SettingValue = salesSettingNewValue.LogRecycleDays;
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.VolumeSales)).B3SettingValue = GetVolumeEquivToDb(Convert.ToInt32(salesSettingNewValue.VolumeSales));
                        m_settingTobeSaved = new ObservableCollection<B3SettingGlobal>(b3Setting);
                        break;
                    }
                case 5:
                    {
                        ServerSetting serverSettingNewValue = ServerSettingVm.ServerSettings;
                        b3Setting = B3Setting;// _m_b3Setting = m_b3Setting.Where(l => l.B3SettingID == (int)B3SettingCategory.ServerGame);
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.MinPlayer)).B3SettingValue = serverSettingNewValue.MinPlayer;
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.GameStartDelay)).B3SettingValue = serverSettingNewValue.GameStartDelay;
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.ConsolotionPrize)).B3SettingValue = serverSettingNewValue.Consolation;
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.GameRecallPass)).B3SettingValue = serverSettingNewValue.GameRecallPassw;
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.WaiCountDown)).B3SettingValue = serverSettingNewValue.WaitCountDown;
                        m_settingTobeSaved = new ObservableCollection<B3SettingGlobal>(b3Setting);
                        break;
                    }
                case 6:
                    {
                        SessionSetting sessionSettingNewValue = SessionSettingVm.SessionSettings;
                        b3Setting = B3Setting; //_m_b3Setting = m_b3Setting.Where(l => l.B3SettingID == (int)B3SettingCategory.Session);
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.PayoutLimit)).B3SettingValue = sessionSettingNewValue.PayoutLimit;
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.JackpotLimit)).B3SettingValue = sessionSettingNewValue.JackpotLimit;
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.EnforceMix)).B3SettingValue = sessionSettingNewValue.EnforceMix ? "T" : "F";
                        m_settingTobeSaved = new ObservableCollection<B3SettingGlobal>(b3Setting);
                        break;
                    }
                case 7:
                    {
                        SystemSetting systemSettingNewValue = SystemSettingVm.SystemSettings;
                        b3Setting = B3Setting; //_m_b3Setting = m_b3Setting.Where(l => l.B3SettingID == (int)B3SettingCategory.System);
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.EnableUk)).B3SettingValue = systemSettingNewValue.EnableUk ? "T" : "F";
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.DualAccount)).B3SettingValue = systemSettingNewValue.DualAccount ? "T" : "F";
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.MultiOperator)).B3SettingValue = systemSettingNewValue.MultiOperator ? "T" : "F";
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.CommonRngBallCall)).B3SettingValue = systemSettingNewValue.CommonRngBallCall ? "T" : "F";

                        m_isRngBallCall = systemSettingNewValue.CommonRngBallCall;

                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.NorthDakotaMode)).B3SettingValue = systemSettingNewValue.NorthDakotaMode ? "T" : "F";
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.HandPayTrigger)).B3SettingValue = systemSettingNewValue.HandPayTrigger;
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.MinimumPlayers)).B3SettingValue = systemSettingNewValue.MinimumPlayers;
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.VipPointMultiplier)).B3SettingValue = systemSettingNewValue.VipPointMultiplier;
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.MagCardSentinelStart)).B3SettingValue = systemSettingNewValue.MagCardSentinelStart;
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.MagCardSentinelEnd)).B3SettingValue = systemSettingNewValue.MagCardSentinelEnd;
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.Currency)).B3SettingValue = systemSettingNewValue.Currency;
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.RngBallCallTime)).B3SettingValue = systemSettingNewValue.RngBallCallTime;
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.PlayerPinLength)).B3SettingValue = systemSettingNewValue.PlayerPinLength;
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.AutoSessionEnd)).B3SettingValue = systemSettingNewValue.AutoSessionEnd;
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.SiteName)).B3SettingValue = systemSettingNewValue.SiteName;
                        b3Setting.Single(l => Convert.ToInt32(l.B3SettingId) == Convert.ToInt32(B3SettingId.SystemMainVolume)).B3SettingValue = GetVolumeEquivToDb(Convert.ToInt32(systemSettingNewValue.SystemMainVolume));
                        m_settingTobeSaved = new ObservableCollection<B3SettingGlobal>(b3Setting);
                        break;
                    }
            }         
        }

        #endregion
        #region (unspecified)

        private void LoadSetting()
        {
            m_settingList.Clear();
            foreach (var b3Category in m_b3SettingCategory)
            {
                if (b3Category.Value != 2)
                {
                    m_settingList.Add(b3Category.Key);
                }
            }
            SettingSelected = m_settingList.FirstOrDefault();
        }

        public ObservableCollection<B3MathGamePay> GetB3MathGamePlay(int gameId)
        {
            var tempResult = new ObservableCollection<B3MathGamePay>(m_controller.Settings.B3MathGamePays.Where(l => l.GameId == gameId));
            return tempResult;
        }

        #endregion
        #region (setDefault, hardcoded value, ID may reference ID on db table)

        private void SetDefaultValue()
       {
            m_b3SettingCategory = new Dictionary<string, int>
            {
                {"Games", 1},
                {"Operator", 2},
                {"Player", 3},
                {"Sales", 4},
                {"Server Game", 5},
                {"Session", 6},
                {"System", 7}
            };
       }

        //No reference to db
       public string GetVolumeEquivToDb(int volumeLevel)
       {
           string result = "";
           switch (volumeLevel)
           {
               case 0: { result = "0"; break; }
               case 1: { result = "10"; break; }
               case 2: { result = "20"; break; }
               case 3: { result = "30"; break; }
               case 4: { result = "40"; break; }
               case 5: { result = "50"; break; }
               case 6: { result = "60"; break; }
               case 7: { result = "70"; break; }
               case 8: { result = "80"; break; }
               case 9: { result = "90"; break; }
               case 10: { result = "100"; break; }
           }
           return result;
       }

        public static List<string> Volume()
        {
            List<string> result = new List<string> {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"};
            return result;
        }

        public static List<string> BetLevelOrSpeedVal()
        {
            List<string> result = new List<string> {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"};
            return result;
        }
      

        public static List<string> MaxCard()
        {
            var result = new List<string> {"4", "6"};
            return result;
        }
        
        //No ref to db.
        private string GetVolumeEquivValue(int volume)
       {
           string tempValue = "";
           if (volume <= 100 && volume >= 91) { tempValue = "10"; }
           else if (volume < 91 && volume >= 81) { tempValue = "9"; }
           else if (volume < 81 && volume >= 71) { tempValue = "8"; }
           else if (volume < 71 && volume >= 61) { tempValue = "7"; }
           else if (volume < 61 && volume >= 51) { tempValue = "6"; }
           else if (volume < 51 && volume >= 41) { tempValue = "5"; }
           else if (volume < 41 && volume >= 31) { tempValue = "4"; }
           else if (volume < 31 && volume >= 21) { tempValue = "3"; }
           else if (volume < 21 && volume >= 11) { tempValue = "2"; }
           else if (volume < 11 && volume >= 1) { tempValue = "1"; }
           else if (volume == 0) { tempValue = "0"; }
           return tempValue;
       }

       #endregion

        #endregion
        #region COMMAND ()

        public ICommand SaveSettingcmd { get; set; }
        public ICommand CancelSettingcmd { get; set; }

        private void SetCommand()
        {
            SaveSettingcmd = new RelayCommand(parameter => RunSavedCommand());
            CancelSettingcmd = new RelayCommand(parameter => CancelSetting());
        }

        #region (save)
        private void RunSavedCommand()        //WAIT TILL THE COMMAND IS COMPLETED
        {
                Mouse.OverrideCursor = Cursors.Wait;
                SaveSetting();
                Mouse.OverrideCursor = null;
        }

        public void SaveSetting()
        {
            try
            {
                SetNewValue();
                SetB3SettingsMessage msg = new SetB3SettingsMessage(m_settingTobeSaved);
                try
                {
                    msg.Send();

                    if (m_selectedSettingEquivToId == (int)B3SettingCategory.Player)
                    {
                        foreach (B3GameSetting i in B3SettingEnableDisable)
                        {
                            if (i.IsEnabled !=  m_modelDefValue.B3SettingEnableDisablePreviousValue.Single(l => l.GameId == i.GameId).IsEnabled)
                            {
                                SetGameEnableSetting msg2 = new SetGameEnableSetting(i.GameId, i.IsEnabled);
                                try
                                {
                                    msg2.Send();
                                    if (msg2.ReturnCode != ServerReturnCode.Success)
                                    {
                                        throw new Exception(ServerErrorTranslator.GetReturnCodeMessage(msg2.ReturnCode));
                                    }
                                }
                                catch (ServerCommException ex)
                                {
                                    throw new Exception("SetGameEnableSetting: " + ex.Message);
                                }
                                m_modelDefValue = new SetModelDefaultValue(B3SettingEnableDisable, m_selectedSettingEquivToId);

                            }
                        }             
                    }
                    else if (m_selectedSettingEquivToId == (int)B3SettingCategory.System)//Update B3ReportCenter 
                    {
                        var ii = ReportsViewModel.Instance;
                        ii.ReportSelectedIndex = -1;
                        ii.SetBallCallReportBySessionOrByGame(m_isRngBallCall);
                    }
                }
                catch
                {
                    if (msg.ReturnCode != ServerReturnCode.Success)
                        throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, "B3 Set Server Setting Failed"));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SetGameEnableSetting: " + ex.Message);
            }
        }
        #endregion
        #region (on selection change)
        int m_prevB3CatIdSelected;
        public void SelectedItemEvent()
        {
            string settingName = m_settingSelected;
            m_selectedSettingEquivToId = m_b3SettingCategory[SettingSelected];
            B3Setting = new ObservableCollection<B3SettingGlobal>(m_controller.Settings.B3GlobalSettings.Where(l => l.B3SettingCategoryId == m_selectedSettingEquivToId));

            if (m_selectedSettingEquivToId != 1)
            {
                ConvertSettingToModel();
                if (m_prevB3CatIdSelected == 3)
                {
                    B3SettingEnableDisable = m_modelDefValue.B3SettingEnableDisablePreviousValue;                 
                }
                IndicatorVisibility = true;
            }
            else
            {
                IndicatorVisibility = false;
            }

            UserControl view = null;
            SetBorderValue = settingName == "Games" ? 0 : 2;
           
            switch (m_selectedSettingEquivToId)
            {
                case 1:
                    {
                        GameSettingsVm = new GameSettingVm(B3Setting, B3SettingEnableDisable);
                        m_gameSettingView = new GameSettingView(GameSettingsVm);
                        BtnSaveIsEnabled = GameSettingsVm.SelectedGameVm.IsGameEnable;
                        view = m_gameSettingView;
                        break;
                    }
            
                case 3:
                    {                   
                        m_playerSettingView =  new PlayerSettingView(PlayerSettingVm = new PlayerSettingVm(m_playerSetting, B3SettingEnableDisable));
                        m_modelDefValue = new SetModelDefaultValue(B3SettingEnableDisable, 3);
                        view = m_playerSettingView;
                        break;
                    }
                case 4:
                    {
                        m_salesSettingView = new SalesSettingView(SalesSettingVm = new SalesSettingVm(m_salesSetting));
                        view = m_salesSettingView;
                        break;
                    }

                case 5:
                    {
                        m_serverGameSettingView = new ServerGameSettingView(ServerSettingVm = new ServerSettingVm(m_serverSetting));
                        view = m_serverGameSettingView;
                        break;
                    }
                case 6:
                    {
                        m_sessionSettingView = new SessionSettingView(SessionSettingVm = new SessionSettingVm(m_sessionSetting));
                        view = m_sessionSettingView;
                        break;
                    }
                case 7:
                    {
                        m_systemSettingView = new SystemSettingView(SystemSettingVm = new SystemSettingVm(m_systemSetting));
                        view = m_systemSettingView;
                        break;
                    }
            }
            SelectedSettingView = view;
            m_prevB3CatIdSelected = m_selectedSettingEquivToId;
        }
        #endregion
        #region (cancel)
        public void CancelSetting()
        {
            ConvertSettingToModel();

            switch (m_b3SettingCategory[SettingSelected])
            {
                case 1:
                    {
                        GameSettingsVm.ReloadSelectedItemForAnyChangesNotSaved();
                        break;
                    }
                case 3:
                    {
                        B3SettingEnableDisable = m_modelDefValue.B3SettingEnableDisablePreviousValue;
                        PlayerSettingVm.B3SettingEnableDisable = B3SettingEnableDisable;
                        PlayerSettingVm.PlayerSetting = m_playerSetting;
                        PlayerSettingVm.RevertValueBack();
                        m_modelDefValue = new SetModelDefaultValue(B3SettingEnableDisable, 3);                 
                        break;                      
                    }
                case 4:
                    {
                        SalesSettingVm.SalesSetting = m_salesSetting;
                        break;
                    }
                case 5:
                    {
                        ServerSettingVm.ServerSettings = m_serverSetting;
                        break;
                    }
                case 6:
                    {
                        SessionSettingVm.SessionSettings = m_sessionSetting;
                        break;
                    }
                case 7:
                    {
                        SystemSettingVm.SystemSettings  = m_systemSetting;
                        break;
                    }
            }
        }
        #endregion

        #endregion
        #region PROPERTIES 
        #region (with private member)

        private bool m_indicatorVisibility;
        public bool IndicatorVisibility
        {
            get { return m_indicatorVisibility; }
            set
            {
                if (m_indicatorVisibility != value)
                {
                    m_indicatorVisibility = value;
                    RaisePropertyChanged("IndicatorVisibility");
                }
            }
        }

        private bool m_btnSaveIsEnabledy;
        public bool BtnSaveIsEnabled
        {
            get { return m_btnSaveIsEnabledy; }
            set 
            {
                m_btnSaveIsEnabledy = value;
                RaisePropertyChanged("BtnSaveIsEnabled");
            }
        }

        private int m_borderValue;
        public int SetBorderValue
        {
            get { return m_borderValue; }
            set
            {
                if (m_borderValue != value)
                {
                    m_borderValue = value;
                    RaisePropertyChanged("SetBorderValue");
                }
            }
        }

        private string m_settingSelected;
        public string SettingSelected
        {
            get { return m_settingSelected; }
            set
            {
                m_settingSelected = value;
                RaisePropertyChanged("SettingSelected");
            }
        }

        private UserControl m_selectedSettingView = new UserControl();
        public UserControl SelectedSettingView
        {
            get
            {
                return m_selectedSettingView;
            }
            set
            {
                m_selectedSettingView = value;
                RaisePropertyChanged("SelectedSettingView");
            }
        }

       
        private readonly List<string> m_settingList = new List<string>();
        public List<string> SettingList
        {
            get { return m_settingList; }
        }
        #endregion
        #region (w member in other class get set)

        public B3CenterSettings Settings
        {
            get
            {
                if (m_controller == null)
                {
                    return null;
                }
                return m_controller.Settings;
            }
        }

        public ObservableCollection<Business.Operator> Operators
        {
            get
            {
                if (m_controller == null)
                {
                    return null;
                }
                return m_controller.Operators;
            }
        }

        public int StaffId
        {
            get
            {
                if (m_controller == null)
                {

                    return 0;
                }
                else
                {
                    return m_controller.Parent.StaffId;
                }
            }
        }

        public int OperatorId
        {
            get
            {
                if (m_controller == null)
                {

                    return 0;
                }
                else
                {
                    return m_controller.Parent.OperatorId;
                }
            }
        }

        public int MachineId
        {
            get
            {
                if (m_controller == null)
                {

                    return 0;
                }
                else
                {
                    return m_controller.Parent.MachineId;
                }
            }
        }

        public bool IsClassIib3GameEnable
        {
            get
            {
                return m_controller.Parent.Settings.IsClassIIB3Enable;
            }
        }
        #endregion
        #region(no member)

        public bool IsSelectedSetting
        {
            get;
            set;
        }

        public ServerSettingVm ServerSettingVm
        {
            get;
            set;
        }

        public SessionSettingVm SessionSettingVm
        {
            get;
            set;
        }

        public SalesSettingVm SalesSettingVm
        {
            get;set;
        }

        public PlayerSettingVm PlayerSettingVm
        {
            get;
            set;
        }

        public SystemSettingVm SystemSettingVm
        {
            get;
            set;
        }

        public GameSettingVm GameSettingsVm
        {
            get;
            set;
        }

        #endregion
        #endregion
    }
    #region ENUM/DIC

    enum B3SettingCategory
    {
        Games = 1,
        Operator = 2,
        Player = 3,
        Sales = 4,
        ServerGame = 5,
        Session = 6,
        System = 7,
    }


    enum B3SettingId
    {
        Denom1 = 1,
        Denom5 = 2,
        Denom10 = 3,
        Denom25 = 4,
        Denom50 = 5,
        Denom100 = 6,
        Denom200 = 7,
        Denom500 = 8,
        MaxBetLevel = 9,
        MaxCards = 10,
        CallSpeed = 11,
        AutoCall = 12,
        AutoPlay = 13,
        HideSerialNumber = 14,
        SingleOfferBonus = 15,
        PlayerCalibrateTouch = 16,
        PresstoCollect = 17,
        AnnounceCall = 18,
        PlayerScreenCursor = 19,
        TimeToCollect = 20,
        Disclaimer = 21,
        DisclaimerTextId = 22,
        PlayerMainVolume = 23,
        ScreenCursor = 24,
        CalibrateTouch = 25,
        AutoPrintSessionReport = 26,
        PagePrinter = 27,
        QuickSales = 28,
        PrintLogo = 29,
        AlowinSessionBall = 30,
        LoggingEnable = 31,
        LogRecycleDays = 32,
        VolumeSales = 33,
        MinPlayer = 34,
        GameStartDelay = 35,
        ConsolotionPrize = 36,
        GameRecallPass = 37,
        WaiCountDown = 38,
        PayoutLimit = 39,
        JackpotLimit = 40,
        EnforceMix = 41,
        HandPayTrigger = 42,
        MinimumPlayers = 43,
        VipPointMultiplier = 44,
        MagCardSentinelStart = 45,
        MagCardSentinelEnd = 46,
        Currency = 47,
        RngBallCallTime = 48,
        PlayerPinLength = 49,
        EnableUk = 50,
        DualAccount = 51,
        MultiOperator = 52,
        CommonRngBallCall = 53,
        NorthDakotaMode = 54,
        AutoSessionEnd = 55,
        SiteName = 56,
        SystemMainVolume = 57,
        MathPayTableSetting = 58,
        CallSpeedMin = 59,
        CallSpeedBonus = 60,
    }


    #endregion
}
