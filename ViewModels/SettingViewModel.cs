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
        #region Fields
        //Parent
        private B3Controller m_controller;
        //Views
        private GameSettingView m_gameSettingView;
        private SystemSettingView m_systemSettingView;
        private ServerGameSettingView m_serverGameSettingView;
        private SalesSettingView m_salesSettingView;
        private PlayerSettingView m_playerSettingView;
        private SessionSettingView m_sessionSettingView;
        //Model
        private ServerSetting m_serverSetting;
        private SessionSetting m_sessionSetting;
        private SalesSettings m_salesSetting;
        private PlayerSettings m_playerSetting;
        private SystemSetting m_systemSetting;
        //private GameSetting m_gameSetting;
        //Other
        private ObservableCollection<B3SettingGlobal> m_settingTobeSaved;
        private B3SettingCategory m_selectedSettingCategoryType;
        private bool m_isRngBallCall;
        //private Dictionary<string, int> m_b3SettingCategory;//Matches the primary key of B3Settingcategory
        private SetModelDefaultValue m_modelDefValue;
        private bool m_indicatorVisibility;
        private bool m_btnSaveIsEnabledy;
        private int m_borderValue;
        private string m_settingSelected;
        private readonly List<string> m_settingList = new List<string>();
        private UserControl m_selectedSettingView = new UserControl();
        private B3SettingCategory m_previousB3SettingCategory;
        private static readonly List<string> m_zeroToTenList = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
        private static readonly List<string> m_oneToTenList = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
        private static readonly List<string> m_maxCardCountList = new List<string> { "4", "6" };

        private static volatile SettingViewModel m_instance;

        private static readonly object m_syncRoot = new object();
        #endregion

        #region METHOD

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

        private void RunSavedCommand()        //WAIT TILL THE COMMAND IS COMPLETED
        {
            Mouse.OverrideCursor = Cursors.Wait;
            SaveSetting();
            Mouse.OverrideCursor = null;
        }

        private void ConvertSettingToModel()
        {
            switch (m_selectedSettingCategoryType)
            {
                case B3SettingCategory.Player:
                    {
                        m_playerSetting = new PlayerSettings();
                        var tempBool = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.PlayerCalibrateTouch)).B3SettingValue;
                        m_playerSetting.PlayerCalibrateTouch = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.PresstoCollect)).B3SettingValue;
                        m_playerSetting.PresstoCollect = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.AnnounceCall)).B3SettingValue;
                        m_playerSetting.AnnounceCall = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.PlayerScreenCursor)).B3SettingValue;
                        m_playerSetting.PlayerScreenCursor = tempBool != "F";
                        m_playerSetting.TimeToCollect = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.TimeToCollect)).B3SettingValue;
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.Disclaimer)).B3SettingValue;
                        m_playerSetting.Disclaimer = tempBool != "F";
                        var volumeSales = Convert.ToInt32(B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.PlayerMainVolume)).B3SettingValue);
                        m_playerSetting.PlayerMainVolume = GetVolumeEquivValue(volumeSales);

                        break;
                    }

                case B3SettingCategory.Sales:
                    {
                        m_salesSetting = new SalesSettings();
                        var tempBool = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.ScreenCursor)).B3SettingValue;
                        m_salesSetting.ScreenCursor = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.CalibrateTouch)).B3SettingValue;
                        m_salesSetting.CalibrateTouch = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.AutoPrintSessionReport)).B3SettingValue;
                        m_salesSetting.AutoPrintSessionReport = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.PagePrinter)).B3SettingValue;
                        m_salesSetting.PagePrinter = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.QuickSales)).B3SettingValue;
                        m_salesSetting.QuickSales = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.PrintLogo)).B3SettingValue;
                        m_salesSetting.PrintLogo = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.AlowinSessionBall)).B3SettingValue;
                        m_salesSetting.AlowinSessionBall = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.LoggingEnable)).B3SettingValue;
                        m_salesSetting.LoggingEnable = tempBool != "F";
                        m_salesSetting.LogRecycleDays = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.LogRecycleDays)).B3SettingValue;
                        var volumeSales = Convert.ToInt32(B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.VolumeSales)).B3SettingValue);
                        m_salesSetting.VolumeSales = GetVolumeEquivValue(volumeSales);
                        break;
                    }
                case B3SettingCategory.ServerGame:
                    {
                        m_serverSetting = new ServerSetting
                        {
                            MinPlayer =
                                B3Setting.Single(
                                    l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.MinPlayer))
                                    .B3SettingValue,
                            GameStartDelay =
                                B3Setting.Single(
                                    l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.GameStartDelay))
                                    .B3SettingValue,
                            Consolation =
                                B3Setting.Single(
                                    l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.ConsolotionPrize))
                                    .B3SettingValue,
                            GameRecallPassw =
                                B3Setting.Single(
                                    l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.GameRecallPass))
                                    .B3SettingValue,
                            WaitCountDown =
                                B3Setting.Single(
                                    l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.WaiCountDown))
                                    .B3SettingValue
                        };
                        break;
                    }
                case B3SettingCategory.Session:
                    {
                        m_sessionSetting = new SessionSetting
                        {
                            PayoutLimit =
                                B3Setting.Single(
                                    l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.PayoutLimit))
                                    .B3SettingValue,
                            JackpotLimit =
                                B3Setting.Single(
                                    l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.JackpotLimit))
                                    .B3SettingValue
                        };

                        var tempBool = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.EnforceMix)).B3SettingValue;
                        m_sessionSetting.EnforceMix = tempBool != "F";
                        break;
                    }
                case B3SettingCategory.System:
                    {
                        m_systemSetting = new SystemSetting();
                        var tempBool = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.EnableUk)).B3SettingValue; m_systemSetting.EnableUk = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.DualAccount)).B3SettingValue; m_systemSetting.DualAccount = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.MultiOperator)).B3SettingValue; m_systemSetting.MultiOperator = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.CommonRngBallCall)).B3SettingValue; m_systemSetting.CommonRngBallCall = tempBool != "F";
                        tempBool = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.NorthDakotaMode)).B3SettingValue; m_systemSetting.NorthDakotaMode = tempBool != "F";
                        m_systemSetting.HandPayTrigger = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.HandPayTrigger)).B3SettingValue;
                        m_systemSetting.MinimumPlayers = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.MinimumPlayers)).B3SettingValue;
                        m_systemSetting.VipPointMultiplier = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.VipPointMultiplier)).B3SettingValue;
                        m_systemSetting.MagCardSentinelStart = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.MagCardSentinelStart)).B3SettingValue;
                        m_systemSetting.MagCardSentinelEnd = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.MagCardSentinelEnd)).B3SettingValue;
                        m_systemSetting.Currency = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.Currency)).B3SettingValue;
                        m_systemSetting.RngBallCallTime = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.RngBallCallTime)).B3SettingValue;
                        m_systemSetting.PlayerPinLength = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.PlayerPinLength)).B3SettingValue;
                        m_systemSetting.AutoSessionEnd = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.AutoSessionEnd)).B3SettingValue;
                        m_systemSetting.SiteName = B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.SiteName)).B3SettingValue;
                        var volumeSales = Convert.ToInt32(B3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.SystemMainVolume)).B3SettingValue);
                        m_systemSetting.SystemMainVolume = GetVolumeEquivValue(volumeSales);
                        break;
                    }
            }
        }

        private void SetNewValue()
        {
            IEnumerable<B3SettingGlobal> b3Setting;
            switch (m_selectedSettingCategoryType)
            {
                case B3SettingCategory.Games:
                    {
                        GameSetting gameSettingNewValue = GameSettingsVm.SelectedGameVm.Settings;
                        b3Setting = B3Setting.Where(l => l.GameType == gameSettingNewValue.GameType);

                        var b3SettingGlobals = b3Setting as B3SettingGlobal[] ?? b3Setting.ToArray();
                        foreach (B3SettingGlobal sg in b3SettingGlobals)
                        {
                            sg.B3SettingdefaultValue = sg.B3SettingValue;
                            switch (sg.SettingType)
                            {
                                case B3SettingType.Denom1:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.Denom1;
                                        break;
                                    }
                                case B3SettingType.Denom5:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.Denom5;
                                        break;
                                    }
                                case B3SettingType.Denom10:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.Denom10;
                                        break;
                                    }
                                case B3SettingType.Denom25:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.Denom25;
                                        break;
                                    }
                                case B3SettingType.Denom50:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.Denom50;
                                        break;
                                    }
                                case B3SettingType.Denom100:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.Denom100;
                                        break;
                                    }
                                case B3SettingType.Denom200:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.Denom200;
                                        break;
                                    }
                                case B3SettingType.Denom500:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.Denom500;
                                        break;
                                    }
                                case B3SettingType.MaxBetLevel:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.MaxBetLevel;
                                        break;
                                    }
                                case B3SettingType.MaxCards:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.MaxCards;
                                        break;
                                    }
                                case B3SettingType.CallSpeed:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.CallSpeed;
                                        break;
                                    }
                                case B3SettingType.AutoCall:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.AutoCall.ConvertToB3StringValue();
                                        break;
                                    }
                                case B3SettingType.AutoPlay:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.AutoPlay.ConvertToB3StringValue();
                                        break;
                                    }
                                case B3SettingType.HideSerialNumber:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.HideSerialNumber.ConvertToB3StringValue();
                                        break;
                                    }
                                case B3SettingType.SingleOfferBonus:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.SingleOfferBonus.ConvertToB3StringValue();
                                        break;
                                    }
                                case B3SettingType.MathPayTableSetting:
                                    {
                                        if (gameSettingNewValue.MathPayTable != null)
                                        {
                                            sg.B3SettingValue = gameSettingNewValue.MathPayTable.MathPackageId.ToString();
                                        }
                                        break;
                                    }
                                case B3SettingType.CallSpeedMin:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.CallSpeedMin;
                                        break;
                                    }
                                case B3SettingType.CallSpeedBonus:
                                    {
                                        sg.B3SettingValue = gameSettingNewValue.CallSpeedBonus;
                                        break;
                                    }
                            }
                        }
                        m_settingTobeSaved = new ObservableCollection<B3SettingGlobal>(b3SettingGlobals);
                        break;
                    }
                case B3SettingCategory.Player:
                    {
                        PlayerSettings playerSettingNewValue = PlayerSettingVm.PlayerSetting;
                        b3Setting = B3Setting;//.Where(l => l.B3SettingID == _GameSettingNewValue.);
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.PlayerCalibrateTouch)).B3SettingValue = playerSettingNewValue.PlayerCalibrateTouch.ConvertToB3StringValue();
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.PresstoCollect)).B3SettingValue = playerSettingNewValue.PresstoCollect.ConvertToB3StringValue();
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.AnnounceCall)).B3SettingValue = playerSettingNewValue.AnnounceCall.ConvertToB3StringValue();
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.PlayerScreenCursor)).B3SettingValue = playerSettingNewValue.PlayerScreenCursor.ConvertToB3StringValue();
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.TimeToCollect)).B3SettingValue = playerSettingNewValue.TimeToCollect;
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.Disclaimer)).B3SettingValue = playerSettingNewValue.Disclaimer.ConvertToB3StringValue();
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.PlayerMainVolume)).B3SettingValue = GetVolumeEquivToDb(Convert.ToInt32(playerSettingNewValue.PlayerMainVolume));
                        m_settingTobeSaved = new ObservableCollection<B3SettingGlobal>(b3Setting);

                        break;
                    }
                case B3SettingCategory.Sales:
                    {
                        SalesSettings salesSettingNewValue = SalesSettingVm.SalesSetting;
                        b3Setting = B3Setting; //_m_b3Setting = m_b3Setting.Where(l => l.B3SettingID == (int)B3SettingCategory.Sales);
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.ScreenCursor)).B3SettingValue = salesSettingNewValue.ScreenCursor.ConvertToB3StringValue(); ;
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.CalibrateTouch)).B3SettingValue = salesSettingNewValue.CalibrateTouch.ConvertToB3StringValue();
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.AutoPrintSessionReport)).B3SettingValue = salesSettingNewValue.AutoPrintSessionReport.ConvertToB3StringValue();
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.PagePrinter)).B3SettingValue = salesSettingNewValue.PagePrinter.ConvertToB3StringValue();
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.QuickSales)).B3SettingValue = salesSettingNewValue.QuickSales.ConvertToB3StringValue();
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.PrintLogo)).B3SettingValue = salesSettingNewValue.PrintLogo.ConvertToB3StringValue();
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.AlowinSessionBall)).B3SettingValue = salesSettingNewValue.AlowinSessionBall.ConvertToB3StringValue();
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.LoggingEnable)).B3SettingValue = salesSettingNewValue.LoggingEnable.ConvertToB3StringValue();
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.LogRecycleDays)).B3SettingValue = salesSettingNewValue.LogRecycleDays;
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.VolumeSales)).B3SettingValue = GetVolumeEquivToDb(Convert.ToInt32(salesSettingNewValue.VolumeSales));
                        m_settingTobeSaved = new ObservableCollection<B3SettingGlobal>(b3Setting);
                        break;
                    }
                case B3SettingCategory.ServerGame:
                    {
                        ServerSetting serverSettingNewValue = ServerSettingVm.ServerSettings;
                        b3Setting = B3Setting;// _m_b3Setting = m_b3Setting.Where(l => l.B3SettingID == (int)B3SettingCategory.ServerGame);
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.MinPlayer)).B3SettingValue = serverSettingNewValue.MinPlayer;
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.GameStartDelay)).B3SettingValue = serverSettingNewValue.GameStartDelay;
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.ConsolotionPrize)).B3SettingValue = serverSettingNewValue.Consolation;
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.GameRecallPass)).B3SettingValue = serverSettingNewValue.GameRecallPassw;
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.WaiCountDown)).B3SettingValue = serverSettingNewValue.WaitCountDown;
                        m_settingTobeSaved = new ObservableCollection<B3SettingGlobal>(b3Setting);
                        break;
                    }
                case B3SettingCategory.Session:
                    {
                        SessionSetting sessionSettingNewValue = SessionSettingVm.SessionSettings;
                        b3Setting = B3Setting; //_m_b3Setting = m_b3Setting.Where(l => l.B3SettingID == (int)B3SettingCategory.Session);
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.PayoutLimit)).B3SettingValue = sessionSettingNewValue.PayoutLimit;
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.JackpotLimit)).B3SettingValue = sessionSettingNewValue.JackpotLimit;
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.EnforceMix)).B3SettingValue = sessionSettingNewValue.EnforceMix.ConvertToB3StringValue();
                        m_settingTobeSaved = new ObservableCollection<B3SettingGlobal>(b3Setting);
                        break;
                    }
                case B3SettingCategory.System:
                    {
                        SystemSetting systemSettingNewValue = SystemSettingVm.SystemSettings;
                        b3Setting = B3Setting; //_m_b3Setting = m_b3Setting.Where(l => l.B3SettingID == (int)B3SettingCategory.System);
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.EnableUk)).B3SettingValue = systemSettingNewValue.EnableUk.ConvertToB3StringValue();
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.DualAccount)).B3SettingValue = systemSettingNewValue.DualAccount.ConvertToB3StringValue();
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.MultiOperator)).B3SettingValue = systemSettingNewValue.MultiOperator.ConvertToB3StringValue();
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.CommonRngBallCall)).B3SettingValue = systemSettingNewValue.CommonRngBallCall.ConvertToB3StringValue();

                        m_isRngBallCall = systemSettingNewValue.CommonRngBallCall;

                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.NorthDakotaMode)).B3SettingValue = systemSettingNewValue.NorthDakotaMode.ConvertToB3StringValue();
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.HandPayTrigger)).B3SettingValue = systemSettingNewValue.HandPayTrigger;
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.MinimumPlayers)).B3SettingValue = systemSettingNewValue.MinimumPlayers;
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.VipPointMultiplier)).B3SettingValue = systemSettingNewValue.VipPointMultiplier;
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.MagCardSentinelStart)).B3SettingValue = systemSettingNewValue.MagCardSentinelStart;
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.MagCardSentinelEnd)).B3SettingValue = systemSettingNewValue.MagCardSentinelEnd;
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.Currency)).B3SettingValue = systemSettingNewValue.Currency;
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.RngBallCallTime)).B3SettingValue = systemSettingNewValue.RngBallCallTime;
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.PlayerPinLength)).B3SettingValue = systemSettingNewValue.PlayerPinLength;
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.AutoSessionEnd)).B3SettingValue = systemSettingNewValue.AutoSessionEnd;
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.SiteName)).B3SettingValue = systemSettingNewValue.SiteName;
                        b3Setting.Single(l => Convert.ToInt32(l.SettingType) == Convert.ToInt32(B3SettingType.SystemMainVolume)).B3SettingValue = GetVolumeEquivToDb(Convert.ToInt32(systemSettingNewValue.SystemMainVolume));
                        m_settingTobeSaved = new ObservableCollection<B3SettingGlobal>(b3Setting);
                        break;
                    }
            }
        }

        private void LoadSettingList()
        {
            m_settingList.Clear();

            var categories = Enum.GetValues(typeof(B3SettingCategory)).Cast<B3SettingCategory>();

            foreach (var b3SettingCategory in categories)
            {
                if ((int)b3SettingCategory != 2)
                {
                    m_settingList.Add(b3SettingCategory.ToString());
                }
            }

            SettingSelected = m_settingList.FirstOrDefault();
        }

        public static List<string> ZeroToTenList()
        {
            return m_zeroToTenList;
        }

        public static List<string> OneToTenList()
        {
            return m_oneToTenList;
        }

        public static List<string> MaxCardCountList()
        {
            return m_maxCardCountList;
        }

        public void Initialize(B3Controller controller)
        {
            if (controller == null)
                throw new ArgumentNullException();

            m_controller = controller;
            B3SettingEnableDisable = new ObservableCollection<B3GameSetting>(m_controller.Settings.B3GameSettings);

            //set commands
            SaveSettingcmd = new RelayCommand(parameter => RunSavedCommand());
            CancelSettingcmd = new RelayCommand(parameter => CancelSetting());

            LoadSettingList();
            BtnSaveIsEnabled = true;
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

                    if (m_selectedSettingCategoryType == B3SettingCategory.Player)
                    {
                        foreach (B3GameSetting i in B3SettingEnableDisable)
                        {
                            if (i.IsEnabled != m_modelDefValue.B3SettingEnableDisablePreviousValue.Single(l => l.GameType == i.GameType).IsEnabled)
                            {
                                SetGameEnableSetting msg2 = new SetGameEnableSetting(i.GameType, i.IsEnabled);
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
                                m_modelDefValue = new SetModelDefaultValue(B3SettingEnableDisable, (int)m_selectedSettingCategoryType);

                            }
                        }
                    }
                    else if (m_selectedSettingCategoryType == B3SettingCategory.System)//Update B3ReportCenter 
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

        public ObservableCollection<B3MathGamePay> GetB3MathGamePlay(B3GameType gameType)
        {
            var tempResult = new ObservableCollection<B3MathGamePay>(m_controller.Settings.B3MathGamePays.Where(l => l.GameType == gameType));
            return tempResult;
        }

        public string GetVolumeEquivToDb(int volumeLevel)
        {
            //volume values are 0-100 for database
            var level = volumeLevel * 10;

            //if below 0 return zero
            if (level < 0)
            {
                return 0.ToString();
            }

            //if above 100 then return 100
            if (level > 100)
            {
                return 100.ToString();
            }

            return level.ToString();
        }

        public void SelectedItemEvent()
        {
            string settingName = m_settingSelected;


            m_selectedSettingCategoryType = (B3SettingCategory)Enum.Parse(typeof(B3SettingCategory), SettingSelected);

            B3Setting = new ObservableCollection<B3SettingGlobal>(m_controller.Settings.B3GlobalSettings.Where(l => l.B3SettingCategoryType == m_selectedSettingCategoryType));

            if (m_selectedSettingCategoryType != B3SettingCategory.Games)
            {
                ConvertSettingToModel();
                if (m_previousB3SettingCategory == B3SettingCategory.Player)
                {
                    B3SettingEnableDisable = m_modelDefValue.B3SettingEnableDisablePreviousValue;
                }
                IndicatorVisibility = true;
            }
            else
            {
                IndicatorVisibility = false;
            }

            SetBorderValue = settingName == "Games" ? 0 : 2;

            switch (m_selectedSettingCategoryType)
            {
                case B3SettingCategory.Games:
                    {
                        GameSettingsVm = new GameSettingVm(B3Setting, B3SettingEnableDisable);
                        m_gameSettingView = new GameSettingView(GameSettingsVm);
                        BtnSaveIsEnabled = GameSettingsVm.SelectedGameVm.IsGameEnable;
                        SelectedSettingView = m_gameSettingView;
                        break;
                    }
                case B3SettingCategory.Player:
                    {
                        PlayerSettingVm = new PlayerSettingVm(m_playerSetting, B3SettingEnableDisable);
                        m_playerSettingView = new PlayerSettingView(PlayerSettingVm);
                        m_modelDefValue = new SetModelDefaultValue(B3SettingEnableDisable, 3);
                        SelectedSettingView = m_playerSettingView;
                        break;
                    }
                case B3SettingCategory.Sales:
                    {
                        SalesSettingVm = new SalesSettingVm(m_salesSetting);
                        m_salesSettingView = new SalesSettingView(SalesSettingVm);
                        SelectedSettingView = m_salesSettingView;
                        break;
                    }
                case B3SettingCategory.ServerGame:
                    {
                        ServerSettingVm = new ServerSettingVm(m_serverSetting);
                        m_serverGameSettingView = new ServerGameSettingView(ServerSettingVm);
                        SelectedSettingView = m_serverGameSettingView;
                        break;
                    }
                case B3SettingCategory.Session:
                    {
                        SessionSettingVm = new SessionSettingVm(m_sessionSetting);
                        m_sessionSettingView = new SessionSettingView(SessionSettingVm);
                        SelectedSettingView = m_sessionSettingView;
                        break;
                    }
                case B3SettingCategory.System:
                    {
                        SystemSettingVm = new SystemSettingVm(m_systemSetting);
                        m_systemSettingView = new SystemSettingView(SystemSettingVm);
                        SelectedSettingView = m_systemSettingView;
                        break;
                    }
            }

            m_previousB3SettingCategory = m_selectedSettingCategoryType;
        }

        public void CancelSetting()
        {
            ConvertSettingToModel();

            switch (m_selectedSettingCategoryType)
            {
                case B3SettingCategory.Games:
                    {
                        GameSettingsVm.ReloadSelectedItemForAnyChangesNotSaved();
                        break;
                    }
                case B3SettingCategory.Player:
                    {
                        B3SettingEnableDisable = m_modelDefValue.B3SettingEnableDisablePreviousValue;
                        PlayerSettingVm.B3SettingEnableDisable = B3SettingEnableDisable;
                        PlayerSettingVm.PlayerSetting = m_playerSetting;
                        PlayerSettingVm.RevertValueBack();
                        m_modelDefValue = new SetModelDefaultValue(B3SettingEnableDisable, 3);
                        break;
                    }
                case B3SettingCategory.Sales:
                    {
                        SalesSettingVm.SalesSetting = m_salesSetting;
                        break;
                    }
                case B3SettingCategory.ServerGame:
                    {
                        ServerSettingVm.ServerSettings = m_serverSetting;
                        break;
                    }
                case B3SettingCategory.Session:
                    {
                        SessionSettingVm.SessionSettings = m_sessionSetting;
                        break;
                    }
                case B3SettingCategory.System:
                    {
                        SystemSettingVm.SystemSettings = m_systemSetting;
                        break;
                    }
            }
        }

        #endregion

        #region Properties

        private ObservableCollection<B3SettingGlobal> B3Setting { get; set; }

        private ObservableCollection<B3GameSetting> B3SettingEnableDisable { get; set; }

        //singleton instance
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

        public ICommand SaveSettingcmd { get; set; }

        public ICommand CancelSettingcmd { get; set; }

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

        public bool BtnSaveIsEnabled
        {
            get { return m_btnSaveIsEnabledy; }
            set
            {
                m_btnSaveIsEnabledy = value;
                RaisePropertyChanged("BtnSaveIsEnabled");
            }
        }

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

        public string SettingSelected
        {
            get { return m_settingSelected; }
            set
            {
                m_settingSelected = value;
                RaisePropertyChanged("SettingSelected");
            }
        }

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

        public List<string> SettingList
        {
            get { return m_settingList; }
        }

        public bool IsSelectedSetting { get; set; }

        public ServerSettingVm ServerSettingVm { get; set; }

        public SessionSettingVm SessionSettingVm { get; set; }

        public SalesSettingVm SalesSettingVm { get; set; }

        public PlayerSettingVm PlayerSettingVm { get; set; }

        public SystemSettingVm SystemSettingVm { get; set; }

        public GameSettingVm GameSettingsVm { get; set; }

        #endregion

    }
}
