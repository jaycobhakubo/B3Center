using System;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using System.Collections.Generic;
using GameTech.Elite.Client.Modules.B3Center.Business;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
    public class SalesSettingVm : ViewModelBase
    {
        #region Fields

        private SalesSettings m_salesSetting;
        private readonly List<B3SettingGlobal> m_originalSaleSettings; 

        #endregion

        #region Constructor

        public SalesSettingVm(List<B3SettingGlobal> salesSettingsList)
        {
            VolumeList = Business.Helpers.ZeroToTenList;
            SalesSetting = new SalesSettings();
            UpdateSettingsListToModel(salesSettingsList);

            m_originalSaleSettings = salesSettingsList;
        }

        #endregion

        #region Properties

        public List<string> VolumeList
        {
            get;
            set;
        }

        public SalesSettings SalesSetting
        {
            get
            {
                return m_salesSetting;
            }
            set
            {
                m_salesSetting = value;
                RaisePropertyChanged("SalesSetting");
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
                    case B3SettingType.ScreenCursor:
                        SalesSetting.ScreenCursor = setting.ConvertB3StringValueToBool();
                        break;
                    case B3SettingType.CalibrateTouch:
                        SalesSetting.CalibrateTouch = setting.ConvertB3StringValueToBool();
                        break;
                    case B3SettingType.AutoPrintSessionReport:
                        SalesSetting.AutoPrintSessionReport = setting.ConvertB3StringValueToBool();
                        break;
                    case B3SettingType.PagePrinter:
                        SalesSetting.PagePrinter = setting.ConvertB3StringValueToBool();
                        break;
                    case B3SettingType.QuickSales:
                        SalesSetting.QuickSales = setting.ConvertB3StringValueToBool();
                        break;
                    case B3SettingType.PrintLogo:
                        SalesSetting.PrintLogo = setting.ConvertB3StringValueToBool();
                        break;
                    case B3SettingType.AlowinSessionBall:
                        SalesSetting.AlowinSessionBall = setting.ConvertB3StringValueToBool();
                        break;
                    case B3SettingType.LoggingEnable:
                        SalesSetting.LoggingEnable = setting.ConvertB3StringValueToBool();
                        break;
                    case B3SettingType.LogRecycleDays:
                        SalesSetting.LogRecycleDays = setting.B3SettingValue;
                        break;
                    case B3SettingType.VolumeSales:
                        SalesSetting.VolumeSales = Business.Helpers.GetVolumeEquivValue(Convert.ToInt32(setting.B3SettingValue));
                        break;
                }
            }
        }

        private void UpdateModelToSettingsList()
        {
            foreach (var setting in m_originalSaleSettings)
            {
                setting.B3SettingDefaultValue = setting.B3SettingValue;
                switch (setting.SettingType)
                {
                    case B3SettingType.ScreenCursor:
                        setting.B3SettingValue = SalesSetting.ScreenCursor.ConvertToB3StringValue();
                        break;
                    case B3SettingType.CalibrateTouch:
                        setting.B3SettingValue = SalesSetting.CalibrateTouch.ConvertToB3StringValue();
                        break;
                    case B3SettingType.AutoPrintSessionReport:
                        setting.B3SettingValue = SalesSetting.AutoPrintSessionReport.ConvertToB3StringValue();
                        break;
                    case B3SettingType.PagePrinter:
                        setting.B3SettingValue = SalesSetting.PagePrinter.ConvertToB3StringValue();
                        break;
                    case B3SettingType.QuickSales:
                        setting.B3SettingValue = SalesSetting.QuickSales.ConvertToB3StringValue();
                        break;
                    case B3SettingType.PrintLogo:
                        setting.B3SettingValue = SalesSetting.PrintLogo.ConvertToB3StringValue();
                        break;
                    case B3SettingType.AlowinSessionBall:
                        setting.B3SettingValue = SalesSetting.AlowinSessionBall.ConvertToB3StringValue();
                        break;
                    case B3SettingType.LoggingEnable:
                        setting.B3SettingValue = SalesSetting.LoggingEnable.ConvertToB3StringValue();
                        break;
                    case B3SettingType.LogRecycleDays:
                        setting.B3SettingValue = SalesSetting.LogRecycleDays;
                        break;
                    case B3SettingType.VolumeSales:
                        setting.B3SettingValue = Business.Helpers.GetVolumeEquivToDb(Convert.ToInt32(SalesSetting.VolumeSales));
                        break;
                }
            }
        }

        public List<B3SettingGlobal> Save()
        {
            UpdateModelToSettingsList();
            return m_originalSaleSettings;
        }

        public void ResetSettingsToDefault()
        {
            UpdateSettingsListToModel(m_originalSaleSettings);
        }
        #endregion



    }
}

