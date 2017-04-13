using System.Collections.Generic;
using GameTech.Elite.Base;

namespace GameTech.Elite.Client.Modules.B3Center.Model.Setting
{
    public class SalesSettings : Notifier
    {
        #region Fields

        private string m_logRecycleDays;
        private bool m_screenCursor;
        private bool m_autoPrintSessionReport;
        private bool m_alowinSessionBall;
        private bool m_printLogo;
        private bool m_pagePrinter;
        private bool m_calibrateTouch;
        private bool m_quickSales;
        private bool m_loggingEnable;
        private string m_volumeSales;

        #endregion
        
        #region Properties
        
        public string LogRecycleDays 
        {          
            get
           {
               return m_logRecycleDays;
           }
           set
           {
               m_logRecycleDays = value;
               RaisePropertyChanged("LogRecycleDays");
           }
        }

        public bool ScreenCursor
        {
            get
            {
                return m_screenCursor;
            }
            set
            {
                m_screenCursor = value;
                RaisePropertyChanged("ScreenCursor");
            }
        }

        public bool AutoPrintSessionReport
        {
            get
            {
                return m_autoPrintSessionReport;
            }
            set
            {
                m_autoPrintSessionReport = value;
                RaisePropertyChanged("AutoPrintSessionReport");
            }
        }

        public bool AlowinSessionBall
        {
            get
            {
                return m_alowinSessionBall;
            }
            set
            {
                m_alowinSessionBall = value;
                RaisePropertyChanged("AlowinSessionBall");
            }
        }

        public bool PrintLogo
        {
            get
            {
                return m_printLogo;
            }
            set
            {
                m_printLogo = value;
                RaisePropertyChanged("PrintLogo");
            }
        }

        public bool PagePrinter
        {
            get
            {
                return m_pagePrinter;
            }
            set
            {
                m_pagePrinter = value;
                RaisePropertyChanged("PagePrinter");
            }
        }

        public bool CalibrateTouch
        {
            get
            {
                return m_calibrateTouch;
            }
            set
            {
                m_calibrateTouch = value;
                RaisePropertyChanged("CalibrateTouch");
            }
        }

        public bool QuickSales
        {
            get
            {
                return m_quickSales;
            }
            set
            {
                m_quickSales = value;
                RaisePropertyChanged("QuickSales");
            }
        }

        public bool LoggingEnable
        {
            get
            {
                return m_loggingEnable;
            }
            set
            {
                m_loggingEnable = value;
                RaisePropertyChanged("LoggingEnable");
            }
        }

        public string VolumeSales
        {
            get
            {
                return m_volumeSales;
            }
            set
            {
                m_volumeSales = value;
                RaisePropertyChanged("VolumeSales");
            }
        }

        #endregion
    }
}
