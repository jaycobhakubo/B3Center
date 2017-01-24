#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply: © 2015 GameTech International, Inc.
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using GameTech.Elite.Base;
using GameTech.Elite.UI;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Properties;
using System.Linq;
using GameTech.Elite.Client.Modules.B3Center.UI.SettingViews;
using System.Windows.Controls;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using GameTech.Elite.Client.Modules.B3Center.Messages;
using System.Threading.Tasks;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{
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
        DisclaimerTextID = 22,
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
        VIPPointMultiplier = 44,
        MagCardSentinelStart = 45,
        MagCardSentinelEnd = 46,
        Currency = 47,
        RNGBallCallTime = 48,
        PlayerPINLength = 49,
        EnableUK = 50,
        DualAccount = 51,
        MultiOperator = 52,
        CommonRNGBallCall = 53,
        NorthDakotaMode = 54,
        AutoSessionEnd = 55,
        SiteName = 56,
        SystemMainVolume = 57,
        MathPayTableSetting = 58,
        CallSpeedMin = 59,
        CallSpeedBonus = 60,
    }


#endregion


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
        private ObservableCollection<B3SettingGlobal> m_b3Setting { get; set; }
        //Model
        private ServerSetting m_serverSetting;
        private SessionSetting m_sessionSetting;
        private SalesSettings m_salesSetting;
        private PlayerSettings m_playerSetting;
        private SystemSetting m_systemSetting;
        private GameSetting m_gameSetting;
        //Other
        private int m_selectedSettingEquivToId;

        private Dictionary<string, int> m_B3SettingCategory;//Matches the primary key of B3Settingcategory

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
            //m_gameSettingView = new GameSettingView();

            if (IsClassIIB3GameEnable == true)
            {
            }
            else
            {
            }
            SetCommand();
            SetDefaultValue();
            LoadSetting();
            if (SettingSelected == "Games")
            {
                SelectedItemEvent("Games");
            }
        }
        #endregion
        #region OTHER ACCESSOR (static)
        //This will access anything that is public on this View Model.
        private static volatile SettingViewModel m_instance;
        private static readonly object m_syncRoot = new Object();
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
     
        private void ConvertSettingToModel()
        {
            switch (m_selectedSettingEquivToId)
            {
                case 3:
                    {
                        m_playerSetting = new PlayerSettings();
                        var tempBool = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.PlayerCalibrateTouch)).B3SettingValue.ToString());
                        m_playerSetting.PlayerCalibrateTouch = (tempBool == "F") ? false : true;
                        tempBool = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.PresstoCollect)).B3SettingValue.ToString());
                        m_playerSetting.PresstoCollect = (tempBool == "F") ? false : true;
                        tempBool = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.AnnounceCall)).B3SettingValue.ToString());
                        m_playerSetting.AnnounceCall = (tempBool == "F") ? false : true;               
                        tempBool = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.PlayerScreenCursor)).B3SettingValue.ToString());
                        m_playerSetting.PlayerScreenCursor = (tempBool == "F") ? false : true;                   
                        m_playerSetting.TimeToCollect = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.TimeToCollect)).B3SettingValue.ToString());                   
                        tempBool = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.Disclaimer)).B3SettingValue.ToString());
                        m_playerSetting.Disclaimer = (tempBool == "F") ? false : true;                                                      
                        var volumeSales = Convert.ToInt32((m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.PlayerMainVolume)).B3SettingValue.ToString()));
                        m_playerSetting.PlayerMainVolume = GetVolumeEquivValue(volumeSales);
                        break;
                    }

                case 4:
                    {
                        m_salesSetting = new SalesSettings();
                        var tempBool = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.ScreenCursor)).B3SettingValue.ToString());
                        m_salesSetting.ScreenCursor = (tempBool == "F") ? false : true;
                        tempBool = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.CalibrateTouch)).B3SettingValue.ToString());
                        m_salesSetting.CalibrateTouch = (tempBool == "F") ? false : true;
                        tempBool = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.AutoPrintSessionReport)).B3SettingValue.ToString());
                        m_salesSetting.AutoPrintSessionReport = (tempBool == "F") ? false : true;
                        tempBool = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.PagePrinter)).B3SettingValue.ToString());
                        m_salesSetting.PagePrinter = (tempBool == "F") ? false : true;
                        tempBool = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.QuickSales)).B3SettingValue.ToString());
                        m_salesSetting.QuickSales = (tempBool == "F") ? false : true;
                        tempBool = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.PrintLogo)).B3SettingValue.ToString());
                        m_salesSetting.PrintLogo = (tempBool == "F") ? false : true;
                        tempBool = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.AlowinSessionBall)).B3SettingValue.ToString());
                        m_salesSetting.AlowinSessionBall = (tempBool == "F") ? false : true;
                        tempBool = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.LoggingEnable)).B3SettingValue.ToString());
                        m_salesSetting.LoggingEnable = (tempBool == "F") ? false : true;
                        m_salesSetting.LogRecycleDays = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.LogRecycleDays)).B3SettingValue.ToString());
                        var volumeSales  =  Convert.ToInt32 ((m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.VolumeSales)).B3SettingValue.ToString()));
                        m_salesSetting.VolumeSales = GetVolumeEquivValue(volumeSales);
                        break; }
                case 5:
                    {
                        m_serverSetting = new ServerSetting();
                        m_serverSetting.MinPlayer = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.MinPlayer)).B3SettingValue.ToString());
                        m_serverSetting.GameStartDelay = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.GameStartDelay)).B3SettingValue.ToString());
                        m_serverSetting.Consolation = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.ConsolotionPrize)).B3SettingValue.ToString());
                        m_serverSetting.GameRecallPassw = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.GameRecallPass)).B3SettingValue.ToString());
                        m_serverSetting.WaitCountDown = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.WaiCountDown)).B3SettingValue.ToString());
                        break;
                    }
                case 6:
                    {
                        m_sessionSetting = new SessionSetting();
                        m_sessionSetting.PayoutLimit = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.PayoutLimit)).B3SettingValue.ToString());
                        m_sessionSetting.JackpotLimit = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.JackpotLimit)).B3SettingValue.ToString());
                        var tempBool = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.EnforceMix)).B3SettingValue.ToString());
                        m_sessionSetting.EnforceMix = (tempBool == "F") ? false : true;
                        break;
                    }
                case 7:
                    {
                        m_systemSetting = new SystemSetting();
                        var tempBool = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.EnableUK)).B3SettingValue.ToString()); m_systemSetting.EnableUK = (tempBool == "F") ? false : true;
                        tempBool = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.DualAccount)).B3SettingValue.ToString()); m_systemSetting.DualAccount = (tempBool == "F") ? false : true;
                        tempBool = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.MultiOperator)).B3SettingValue.ToString()); m_systemSetting.MultiOperator = (tempBool == "F") ? false : true;
                        tempBool = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.CommonRNGBallCall)).B3SettingValue.ToString()); m_systemSetting.CommonRNGBallCall = (tempBool == "F") ? false : true;
                        tempBool = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.NorthDakotaMode)).B3SettingValue.ToString()); m_systemSetting.NorthDakotaMode = (tempBool == "F") ? false : true;
                        m_systemSetting.HandPayTrigger = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.HandPayTrigger)).B3SettingValue.ToString());
                        m_systemSetting.MinimumPlayers = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.MinimumPlayers)).B3SettingValue.ToString());
                        m_systemSetting.VIPPointMultiplier = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.VIPPointMultiplier)).B3SettingValue.ToString());
                        m_systemSetting.MagCardSentinelStart = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.MagCardSentinelStart)).B3SettingValue.ToString());
                        m_systemSetting.MagCardSentinelEnd = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.MagCardSentinelEnd)).B3SettingValue.ToString());
                        m_systemSetting.Currency = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.Currency)).B3SettingValue.ToString());
                        m_systemSetting.RNGBallCallTime = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.RNGBallCallTime)).B3SettingValue.ToString());
                        m_systemSetting.PlayerPINLength = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.PlayerPINLength)).B3SettingValue.ToString());
                        m_systemSetting.AutoSessionEnd = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.AutoSessionEnd)).B3SettingValue.ToString());
                        m_systemSetting.SiteName = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.SiteName)).B3SettingValue.ToString());
                        m_systemSetting.SystemMainVolume = (m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.SystemMainVolume)).B3SettingValue.ToString());
                        break;
                    }
            }
        }

        private void SetNewValue()
        {
            switch (m_selectedSettingEquivToId)
            {
                case 3:
                    {
                        PlayerSettings _PlayerSettingNewValue = PlayerSetting_Vm.PlayerSetting_;
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.PlayerCalibrateTouch	)).B3SettingValue = ((_PlayerSettingNewValue.PlayerCalibrateTouch	 == true) ? "T" : "F");
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.PresstoCollect	)).B3SettingValue = ((_PlayerSettingNewValue.PresstoCollect	 == true) ? "T" : "F");
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.AnnounceCall	)).B3SettingValue = ((_PlayerSettingNewValue.AnnounceCall	 == true) ? "T" : "F");
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.PlayerScreenCursor	)).B3SettingValue = ((_PlayerSettingNewValue.PlayerScreenCursor	 == true) ? "T" : "F");
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.TimeToCollect)).B3SettingValue = _PlayerSettingNewValue.TimeToCollect;
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.Disclaimer	)).B3SettingValue = ((_PlayerSettingNewValue.Disclaimer	 == true) ? "T" : "F");
                        //m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.DisclaimerTextID	)).B3SettingValue = ((_PlayerSettingNewValue.DisclaimerTextID	 == true) ? "T" : "F");
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.PlayerMainVolume)).B3SettingValue = GetVolumeEquivToDB(Convert.ToInt32(_PlayerSettingNewValue.PlayerMainVolume));

                        break;
                    }

                case 4:
                    {
                        SalesSettings _SalesSettingNewValue = SalesSetting_Vm.SalesSetting_;
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.ScreenCursor)).B3SettingValue = ((_SalesSettingNewValue.ScreenCursor == true) ? "T" : "F");
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.CalibrateTouch)).B3SettingValue = ((_SalesSettingNewValue.CalibrateTouch == true) ? "T" : "F");
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.AutoPrintSessionReport)).B3SettingValue = ((_SalesSettingNewValue.AutoPrintSessionReport == true) ? "T" : "F");
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.PagePrinter)).B3SettingValue = ((_SalesSettingNewValue.PagePrinter == true) ? "T" : "F");
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.QuickSales)).B3SettingValue = ((_SalesSettingNewValue.QuickSales == true) ? "T" : "F");
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.PrintLogo)).B3SettingValue = ((_SalesSettingNewValue.PrintLogo == true) ? "T" : "F");
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.AlowinSessionBall)).B3SettingValue = ((_SalesSettingNewValue.AlowinSessionBall == true) ? "T" : "F");
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.LoggingEnable)).B3SettingValue = ((_SalesSettingNewValue.LoggingEnable == true) ? "T" : "F");
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.LogRecycleDays)).B3SettingValue = _SalesSettingNewValue.LogRecycleDays;
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.VolumeSales)).B3SettingValue = GetVolumeEquivToDB(Convert.ToInt32(_SalesSettingNewValue.VolumeSales));

                        break;
                    }
                case 5:
                    {
                        ServerSetting _ServerSettingNewValue = ServerSetting_Vm.ServerSetting_;
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.MinPlayer)).B3SettingValue = _ServerSettingNewValue.MinPlayer;
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.GameStartDelay)).B3SettingValue = _ServerSettingNewValue.GameStartDelay;
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.ConsolotionPrize)).B3SettingValue = _ServerSettingNewValue.Consolation;
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.GameRecallPass)).B3SettingValue = _ServerSettingNewValue.GameRecallPassw;
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.WaiCountDown)).B3SettingValue = _ServerSettingNewValue.WaitCountDown;
                        break;
                    }
                case 6:
                    {
                        SessionSetting _SessionSettingNewValue = SessionSetting_Vm.SessionSetting_;
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.PayoutLimit)).B3SettingValue = _SessionSettingNewValue.PayoutLimit;
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.JackpotLimit)).B3SettingValue = _SessionSettingNewValue.JackpotLimit;
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.EnforceMix)).B3SettingValue = ((_SessionSettingNewValue.EnforceMix == true) ? "T" : "F");
                        break;
                    }
                case 7:
                    {
                        SystemSetting _SystemSettingNewValue = SystemSetting_Vm.SystemSetting_;
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.EnableUK)).B3SettingValue = ((_SystemSettingNewValue.EnableUK == true) ? "T" : "F");
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.DualAccount)).B3SettingValue = ((_SystemSettingNewValue.DualAccount == true) ? "T" : "F");
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.MultiOperator)).B3SettingValue = ((_SystemSettingNewValue.MultiOperator == true) ? "T" : "F");
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.CommonRNGBallCall)).B3SettingValue = ((_SystemSettingNewValue.CommonRNGBallCall == true) ? "T" : "F");
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.NorthDakotaMode)).B3SettingValue = ((_SystemSettingNewValue.NorthDakotaMode == true) ? "T" : "F");
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.HandPayTrigger)).B3SettingValue = _SystemSettingNewValue.HandPayTrigger;
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.MinimumPlayers)).B3SettingValue = _SystemSettingNewValue.MinimumPlayers;
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.VIPPointMultiplier)).B3SettingValue = _SystemSettingNewValue.VIPPointMultiplier;
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.MagCardSentinelStart)).B3SettingValue = _SystemSettingNewValue.MagCardSentinelStart;
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.MagCardSentinelEnd)).B3SettingValue = _SystemSettingNewValue.MagCardSentinelEnd;
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.Currency)).B3SettingValue = _SystemSettingNewValue.Currency;
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.RNGBallCallTime)).B3SettingValue = _SystemSettingNewValue.RNGBallCallTime;
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.PlayerPINLength)).B3SettingValue = _SystemSettingNewValue.PlayerPINLength;
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.AutoSessionEnd)).B3SettingValue = _SystemSettingNewValue.AutoSessionEnd;
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.SiteName)).B3SettingValue = _SystemSettingNewValue.SiteName;
                        m_b3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.SystemMainVolume)).B3SettingValue = GetVolumeEquivToDB(Convert.ToInt32(_SystemSettingNewValue.SystemMainVolume));
                        break;

                    }
            }
        }    

        private void LoadSetting()
        {
            m_settingList.Clear();
            foreach (var B3Category in m_B3SettingCategory)
            {
                if (B3Category.Value != 2)
                {
                    m_settingList.Add(B3Category.Key);
                }
            }
                SettingSelected = m_settingList.FirstOrDefault();
        }

       #region (setDefault, hardcoded value, ID may reference ID on db table)

       //dbo.B3SettingCategory(Id)
       private void SetDefaultValue()
       {
           m_B3SettingCategory = new Dictionary<string, int>();
           m_B3SettingCategory.Add("Games", 1);
           m_B3SettingCategory.Add("Operator", 2);
           m_B3SettingCategory.Add("Player", 3);
           m_B3SettingCategory.Add("Sales", 4);
           m_B3SettingCategory.Add("Server Game", 5);
           m_B3SettingCategory.Add("Session", 6);
           m_B3SettingCategory.Add("System", 7);
       }

        //No reference to db
       public string GetVolumeEquivToDB(int VolumeLevel)
       {
           string result = "";
           switch (VolumeLevel)
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

        //No ref to db.
       private string GetVolumeEquivValue(int Volume)
       {
           string tempValue = "";
           if (Volume <= 100 && Volume >= 91) { tempValue = "10"; }
           else if (Volume < 91 && Volume >= 81) { tempValue = "9"; }
           else if (Volume < 81 && Volume >= 71) { tempValue = "8"; }
           else if (Volume < 71 && Volume >= 61) { tempValue = "7"; }
           else if (Volume < 61 && Volume >= 51) { tempValue = "6"; }
           else if (Volume < 51 && Volume >= 41) { tempValue = "5"; }
           else if (Volume < 41 && Volume >= 31) { tempValue = "4"; }
           else if (Volume < 31 && Volume >= 21) { tempValue = "3"; }
           else if (Volume < 21 && Volume >= 11) { tempValue = "2"; }
           else if (Volume < 11 && Volume >= 1) { tempValue = "1"; }
           else if (Volume == 0) { tempValue = "0"; }
           return tempValue;
       }

        //No ref to db
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

        #endregion
        #region COMMAND ()

        public ICommand SelectedItemChanged { get; private set; }
        public ICommand SaveSettingcmd { get; set; }
        public ICommand CancelSettingcmd { get; set; }

        private void SetCommand()
        {
            SaveSettingcmd = new RelayCommand(parameter => RunSavedCommand());
            CancelSettingcmd = new RelayCommand(parameter => CancelSetting());
            SelectedItemChanged = new DelegateCommand<string>(obj =>
            {
                IsSelectedSetting = (obj.ToString() != SettingSelected) ? true : false;
                SelectedItemEvent(SettingSelected);
            });
        }

        //WAIT TILL THE COMMAND IS COMPLETED
        private void RunSavedCommand()
        {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                Task save = Task.Factory.StartNew(() => SaveSetting());
                save.Wait();
                Mouse.OverrideCursor = null;
        }

        private void SelectedItemEvent(string SettingName)
        {
            m_selectedSettingEquivToId = (int)m_B3SettingCategory[SettingSelected];
            m_b3Setting = new ObservableCollection<B3SettingGlobal>(m_controller.Settings.B3SettingGlobal_.Where(l => l.B3SettingCategoryID == m_selectedSettingEquivToId));


            if (m_selectedSettingEquivToId != 1)
            {
                ConvertSettingToModel();
            }

            UserControl view = null;
            if (SettingName == "Games")
            {
                SetBorderValue = 0;
            }
            else
            {
                SetBorderValue = 2;
            }

            switch (m_selectedSettingEquivToId)
            {
                case 1:
                    {                     
                        GameSetting_Vm = new GameSettingVm(m_b3Setting);
                        m_gameSettingView = new GameSettingView(GameSetting_Vm);
                         view = m_gameSettingView;
                        break;
                    }
            
                case 3:
                    {
                        m_playerSettingView = new PlayerSettingView(PlayerSetting_Vm = new PlayerSettingVm(m_playerSetting));
                        view = m_playerSettingView;
                        break;
                    }
                case 4:
                    {
                        m_salesSettingView = new SalesSettingView(SalesSetting_Vm = new SalesSettingVm(m_salesSetting));
                        view = m_salesSettingView;
                        break;
                    }

                case 5:
                    {
                        m_serverGameSettingView = new ServerGameSettingView(ServerSetting_Vm = new ServerSettingVm(m_serverSetting));
                        view = m_serverGameSettingView;
                        break;
                    }
                case 6:
                    {
                        m_sessionSettingView = new SessionSettingView(SessionSetting_Vm = new SessionSettingVm(m_sessionSetting));
                        view = m_sessionSettingView;
                        break;
                    }
                case 7:
                    {
                        m_systemSettingView = new SystemSettingView(SystemSetting_Vm = new SystemSettingVm(m_systemSetting));
                        view = m_systemSettingView;
                        break;
                    }
            }
            SelectedSettingView = view;
        }

        public void SaveSetting()
        {
            try
            {
                SetNewValue();
                SetB3SettingsMessage msg = new SetB3SettingsMessage(m_b3Setting);
                try
                {
                    msg.Send();
                }
                catch
                {
                    if (msg.ReturnCode != ServerReturnCode.Success)
                        throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, "B3 Set Server Setting Failed", ServerErrorTranslator.GetReturnCodeMessage(msg.ReturnCode)));
                }
                //lblSavedNotification.Visibility = Visibility.Visible;
            }
            catch
            { }
        }

        public void CancelSetting()
        {
            ConvertSettingToModel();

            switch ((int)m_B3SettingCategory[SettingSelected])
            {
                case 3:
                    {
                        PlayerSetting_Vm.PlayerSetting_ = m_playerSetting;
                        break;
                    }
                case 4:
                    {
                        SalesSetting_Vm.SalesSetting_ = m_salesSetting;
                        break;
                    }
                case 5:
                    {
                        ServerSetting_Vm.ServerSetting_ = m_serverSetting;
                        break;
                    }
                case 6:
                    {
                        SessionSetting_Vm.SessionSetting_ = m_sessionSetting;
                        break;
                    }
                case 7:
                    {
                        SystemSetting_Vm.SystemSetting_  = m_systemSetting;
                        break;
                    }
            }
        }

        #endregion
        #region PROPERTIES 
        #region (with private member)

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

       
        private List<string> m_settingList = new List<string>();
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

        public ObservableCollection<GameTech.Elite.Client.Modules.B3Center.Business.Operator> Operators
        {
            get
            {
                if (m_controller == null)
                {
                    return null;
                }
                return m_controller.Operators;
            }
            set
            {
                //m_controller.Operators
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

        public bool IsClassIIB3GameEnable
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

        public ServerSettingVm ServerSetting_Vm
        {
            get;
            set;
        }

        public SessionSettingVm SessionSetting_Vm
        {
            get;
            set;
        }

        public SalesSettingVm SalesSetting_Vm
        {
            get;set;
        }

        public PlayerSettingVm PlayerSetting_Vm
        {
            get;
            set;
        }

        public SystemSettingVm SystemSetting_Vm
        {
            get;
            set;
        }

        public GameSettingVm GameSetting_Vm
        {
            get;
            set;
        }

        #endregion
        #endregion


    }
}
