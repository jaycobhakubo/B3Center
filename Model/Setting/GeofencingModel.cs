﻿using GameTech.Elite.Base;

namespace GameTech.Elite.Client.Modules.B3Center.Model.Setting
{
    public class GeofencingModel : Notifier
    {
        #region Fields
        private bool m_enableGeofencing;
        private string m_longitude;
        private string m_latitude;
        private string m_yellowZone;
        private string m_redZone;
        #endregion

        #region Properties

        public bool EnableGeofencing
        {
            get
            {
                return m_enableGeofencing;
            }
            set
            {
                m_enableGeofencing = value;
                RaisePropertyChanged("EnableGeofencing");
            }
        }

        public string Longitude
        {
            get
            {
                return m_longitude;
            }
            set
            {
                m_longitude = value;             
                RaisePropertyChanged("Longitude");
            }
        }

        public string Latitude
        {
            get
            {
                return m_latitude;
            }
            set
            {
                m_latitude = value;
                RaisePropertyChanged("Latitude");
            }
        }

        public string YellowZone
        {
            get
            {
                return m_yellowZone;
            }
            set
            {
                m_yellowZone = value;
                RaisePropertyChanged("YellowZone");
            }
        }

        public string RedZone
        {
            get
            {
                return m_redZone;
            }
            set
            {
                m_redZone = value;              
                RaisePropertyChanged("RedZone");
            }
        }

        #endregion
        
    }
}
