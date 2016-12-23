#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2011 GameTech International, Inc.
#endregion

using System;
using System.Globalization;
using GameTech.Elite.Base;
using GameTech.Elite.UI;
using System.Collections.Generic;

namespace GameTech.Elite.Client.Modules.B3Center.Business
{

    /// <summary>
    /// Contains system settings that pertain to B3Center.
    /// </summary>
    internal class B3CenterSettings : EliteModuleSettings
    {

        internal string ClientInstallDrive = "c:";
        internal string ClientinstallDirectory;
        internal string ReportDirectory = @"\Reports\";

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the B3CenterSettings class.
        /// </summary>
        public B3CenterSettings()
        {
            DisplayMode = DisplayMode.Windowed;
            UseAcceleration = true;
        }
        #endregion

        #region Member Methods
        /// <summary>
        /// Parses the specified setting and loads it into the object, if
        /// valid.
        /// </summary>
        /// <param name="setting">The system setting to parse.</param>
        /// <exception cref="GameTech.Elite.Base.InvalidModuleSettingException">
        /// The specified setting was invalid.</exception>
        public override void LoadSetting(SettingValue setting)
        {
            try
            {
                switch(setting.Id)
                {
                    case Setting.UseHardwareAcceleration:
                        UseAcceleration = Convert.ToBoolean(setting.Value, CultureInfo.InvariantCulture);
                        break;
                    case Setting.ClientInstallDrive:
                        ClientInstallDrive = setting.Value;
                        break;
                    case Setting.ClientInstallRootDirectory:
                        ClientinstallDirectory = setting.Value;
                        break;
                    case Setting.ReceiptPrinterName:
                        ReceiptPrinterName = setting.Value;
                        break;
                    case Setting.PrinterName:
                        PrinterName = setting.Value;
                        break;
                    default:
                        base.LoadSetting(setting);
                        break;
                }
            }
            catch(InvalidModuleSettingException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new InvalidModuleSettingException(setting, ex);
            }
        }

        /// <summary>
        /// Parses the specified setting and loads it into the object, if
        /// valid.
        /// </summary>
        /// <param name="setting">The license setting to parse.</param>
        /// <exception cref="GameTech.Elite.Base.InvalidModuleSettingException">
        /// The specified setting was invalid.</exception>
        public override void LoadSetting(LicenseSettingValue setting)
        {
            try
            {
                switch(setting.Id)
                {
                    case LicenseSetting.EnableClassIIB3Game:
                        bool tempResult;
                        if (bool.TryParse(setting.Value, out tempResult))
                        {
                            IsClassIIB3Enable = tempResult;
                        }
                        break;
                    default:
                        base.LoadSetting(setting);
                        break;
                }
            }
            catch(InvalidModuleSettingException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new InvalidModuleSettingException(setting, ex);
            }
        }
        #endregion

        #region Member Properties
        /// <summary>
        /// Gets or sets the display mode of B3Center.
        /// </summary>
        public DisplayMode DisplayMode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether to use hardware acceleration for rendering.
        /// </summary>
        public bool UseAcceleration
        {
            get;
            set;
        }

        public bool IsMultiOperator { get; set; }
        public bool IsCommonRngBallCall { get; set; }
        public bool AllowInSessBallChange { get; set; }
        public bool IsDoubleAccount { get; set; }
        public bool EnforceMix { get; set; }
        public string ReceiptPrinterName { get; private set; }
        public bool IsClassIIB3Enable { get; set; }
        public string PrinterName { get; private set; }
        public List<B3GameSetting> B3GameSetting_ = new List<B3GameSetting>();
        public List<B3SettingGlobal> B3SettingGlobal_ = new List<B3SettingGlobal>();
        public List<B3IconColor> B3IconColor_ = new List<B3IconColor>();
        public List<B3MathGamePay> B3GameMathPlay_ = new List<B3MathGamePay>();

        #endregion
    }

    public enum B3SettingCategory
    {
        SystemSettings = 7
    }

    public enum B3SettingDescription
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
        SalesScreenCursor = 24,
        SalesCalibrateTouch = 25,
        AutoPrintSessionReport = 26,
        PagePrinter = 27,
        QuickSales = 28,
        PrintLogo = 29,
        AllowinSessionBallChange = 30,
        LoggingEnable = 31,
        LogRecycleDays = 32,
        SalesMainVolume = 33,
        MinPlayer = 34,
        GameStartDelay = 35,
        ConsolotionPrize = 36,
        GameRecallPassword = 37,
        WaitCountDown = 38,
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

    public class B3GameSetting
    {
        #region Constructors
        /// <summary>
        /// Use to set value
        /// </summary>
        public B3GameSetting(int gameID, bool isEnabled, bool isAllowed)
        {
            GameId = gameID;
            IsEnabled = isEnabled;
            IsAllowed = isAllowed;
        }
        #endregion

        #region Properties

        public int GameId
        {
            get;
            set;
        }

        public bool IsEnabled
        {
            get;
            set;
        }

        public bool IsAllowed
        {
            get;
            set;
        }

        #endregion
    }

    //Do we want to make it as static ?
    public class B3SettingGlobal
    {
        //#region Constructor

        //public B3SettingGlobal(int B3SettingID_, int B3SettingCategoryID_, int B3GameID_, string B3SettingValue_)
        //{
        //    B3SettingID = B3SettingID_;
        //    B3SettingCategoryID = B3SettingCategoryID_;
        //    B3GameID = B3GameID_;
        //    B3SettingValue = B3SettingValue_;
        //}

        //public B3SettingGlobal()
        //{
        //}

        //#endregion

        #region Properties

        public int B3SettingID
        {
            get;
            set;
        }

        public int B3SettingCategoryID
        {
            get;
            set;
        }

        public int B3GameID
        {
            get;
            set;
        }

        public string B3SettingValue
        {
            get;
            set;
        }

        public string B3SettingOldValue
        {
            get;
            set;
        }



        #endregion

    }


    public class B3IconColor
    {
        #region Constructors

        public B3IconColor(int colorID, string colorValue)
        {
            ColorID = colorID;
            ColorValue = colorValue;
        }

        #endregion

        #region Properties

        public int ColorID
        {
            get;
            set;
        }

        public string ColorValue
        {
            get;
            set;
        }

        #endregion

    }

    public class B3MathGamePay
    {


        public int MathPackageID
        {
            get;
            set;
        }

        public int GameID
        {
            get;
            set;
        }

        public string PackageDesc
        {
            get;
            set;
        }

        public bool IsRNG
        {
            get;
            set;
        }
    }

    public class SettingMember
    {
        public int m_settingID;
        public int m_gameID;
        public string m_value;
        public string m_oldValue;
    }
}