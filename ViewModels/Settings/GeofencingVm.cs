
using System.Collections.Generic;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
    public class GeofencingVm : ViewModelBase
    {
        #region Fields       
        private GeofencingModel m_geofencingModel;
        private readonly List<B3SettingGlobal> m_originalGeofencingSettings; 
        #endregion
        
        #region Constructor

        public GeofencingVm(List<B3SettingGlobal> geofencingSettingList)
        {
            GeofencingModel = new GeofencingModel();
            UpdateSettingsListToModel(geofencingSettingList);
            m_originalGeofencingSettings = geofencingSettingList;
        }

        #endregion


        #region Properties

        public GeofencingModel GeofencingModel
        {
            get
            {
                return m_geofencingModel;
            }
            set
            {
                m_geofencingModel = value;
                RaisePropertyChanged("GeofencingModel");
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
                    case B3SettingType.EnableGeofencing:
                        GeofencingModel.EnableGeofencing = setting.ConvertB3StringValueToBool();
                        break;
                    case B3SettingType.Longtitude:
                        GeofencingModel.Longtitude = setting.B3SettingValue;
                        break;
                    case B3SettingType.Latitude:
                        GeofencingModel.Latitude = setting.B3SettingValue;
                        break;
                    case B3SettingType.YellowZone:
                        GeofencingModel.YellowZone = setting.B3SettingValue;
                        break;
                    case B3SettingType.RedZone:
                        GeofencingModel.RedZone = setting.B3SettingValue;
                        break;
                }
            }
        }

        private void UpdateModelToSettingsList()
        {
            foreach (var setting in m_originalGeofencingSettings)
            {
                setting.HasChanged = false;
                var tempOldSettingValue = setting.B3SettingValue;//saved current setting value
                
                switch (setting.SettingType)
                {
                    case B3SettingType.EnableGeofencing:
                        setting.B3SettingValue = GeofencingModel.EnableGeofencing.ConvertToB3StringValue();
                        break;
                    case B3SettingType.Longtitude:
                        setting.B3SettingValue = GeofencingModel.Longtitude;
                        break;
                    case B3SettingType.Latitude:
                       setting.B3SettingValue = GeofencingModel.Latitude;
                        break;
                    case B3SettingType.YellowZone:
                        setting.B3SettingValue = GeofencingModel.YellowZone;
                        break;
                    case B3SettingType.RedZone:
                        setting.B3SettingValue = GeofencingModel.RedZone;
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
            return m_originalGeofencingSettings;
        }

        public void ResetSettingsToDefault()
        {
            UpdateSettingsListToModel(m_originalGeofencingSettings);
        }

        #endregion
    }
}
