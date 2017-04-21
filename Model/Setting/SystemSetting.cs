using GameTech.Elite.Base;

namespace GameTech.Elite.Client.Modules.B3Center.Model.Setting
{
    public class SystemSetting : Notifier
    {
        #region Fields

        private string m_handPayTrigger;
        private string m_minimumPlayers;
        private string m_vipPointMultiplier;
        private string m_magCardSentinelStart;
        private string m_magCardSentinelEnd;
        private string m_currency;
        private string m_rngBallCallTime;
        private string m_playerPinLength;
        private bool m_enableUk;
        private bool m_dualAccount;
        private bool m_multiOperator;
        //private bool m_commonRngBallCall;
        private bool m_northDakotaMode;
        private string m_autoSessionEnd;
        private string m_siteName;
        private string m_systemMainVolume;

        #endregion
        
        #region Properties


        public string HandPayTrigger
        {
            get { return m_handPayTrigger; }
            set
            {
                m_handPayTrigger = value;
                RaisePropertyChanged("HandPayTrigger");
            }
        }

        public string MinimumPlayers
        {
            get { return m_minimumPlayers; }
            set
            {
                m_minimumPlayers = value;
                RaisePropertyChanged("MinimumPlayers");
            }
        }

        public string VipPointMultiplier
        {
            get { return m_vipPointMultiplier; }
            set
            {
                m_vipPointMultiplier = value;
                RaisePropertyChanged("VipPointMultiplier");
            }
        }

        public string MagCardSentinelStart
        {
            get { return m_magCardSentinelStart; }
            set
            {
                m_magCardSentinelStart = value;
                RaisePropertyChanged("MagCardSentinelStart");
            }
        }

        public string MagCardSentinelEnd
        {
            get { return m_magCardSentinelEnd; }
            set
            {
                m_magCardSentinelEnd = value;
                RaisePropertyChanged("MagCardSentinelEnd");
            }
        }

        public string Currency
        {
            get { return m_currency; }
            set
            {
                m_currency = value;
                RaisePropertyChanged("Currency");
            }
        }

        public string RngBallCallTime
        {
            get { return m_rngBallCallTime; }
            set
            {
                m_rngBallCallTime = value;
                RaisePropertyChanged("RngBallCallTime");
            }
        }

        public string PlayerPinLength
        {
            get { return m_playerPinLength; }
            set
            {
                m_playerPinLength = value;
                RaisePropertyChanged("PlayerPinLength");
            }
        }

        public bool EnableUk
        {
            get { return m_enableUk; }
            set
            {
                m_enableUk = value;
                RaisePropertyChanged("EnableUk");
            }
        }

        public bool DualAccount
        {
            get { return m_dualAccount; }
            set
            {
                m_dualAccount = value;
                RaisePropertyChanged("DualAccount");
            }
        }

        public bool MultiOperator
        {
            get { return m_multiOperator; }
            set
            {
                m_multiOperator = value;
                RaisePropertyChanged("MultiOperator");
            }
        }

        //public bool CommonRngBallCall
        //{
        //    get { return m_commonRngBallCall; }
        //    set
        //    {
        //        m_commonRngBallCall = value;
        //        RaisePropertyChanged("CommonRngBallCall");
        //    }
        //}

        public bool NorthDakotaMode
        {
            get { return m_northDakotaMode; }
            set
            {
                m_northDakotaMode = value;
                RaisePropertyChanged("NorthDakotaMode");
            }
        }

        public string AutoSessionEnd
        {
            get { return m_autoSessionEnd; }
            set
            {
                m_autoSessionEnd = value;
                RaisePropertyChanged("AutoSessionEnd");
            }
        }

        public string SiteName
        {
            get { return m_siteName; }
            set
            {
                m_siteName = value;
                RaisePropertyChanged("SiteName");
            }
        }

        public string SystemMainVolume
        {
            get { return m_systemMainVolume; }
            set
            {
                m_systemMainVolume = value;
                RaisePropertyChanged("SystemMainVolume");
            }
        }

        #endregion
    }
}
 