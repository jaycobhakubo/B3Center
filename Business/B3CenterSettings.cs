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
using System.Collections.ObjectModel;

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
        public List<B3GameSetting> B3GameSettings = new List<B3GameSetting>();
        public ObservableCollection<B3SettingGlobal> B3GlobalSettings; //= new List<B3SettingGlobal>();
        public List<B3IconColor> B3IconColors = new List<B3IconColor>();
        public List<B3MathGamePay> B3MathGamePays = new List<B3MathGamePay>();

        #endregion
    }
}