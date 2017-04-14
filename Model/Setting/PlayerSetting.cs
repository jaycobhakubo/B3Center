using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;

namespace GameTech.Elite.Client.Modules.B3Center.Model.Setting
{
    public class PlayerSettings: Notifier
    {
        #region Fields
        private bool m_playerCalibrateTouch;
        private bool m_presstoCollect;
        private bool m_announceCall;
        private bool m_playerScreenCursor;
        private string m_timeToCollect;
        private bool m_disclaimer;
        private bool m_disclaimerTextId;
        private string m_playerMainVolume;
        private B3IsGameEnabledSetting m_crazyboutGameSetting;
        private B3IsGameEnabledSetting m_jailBreakGameSetting;
        private B3IsGameEnabledSetting m_mayaMoneyGameSetting;
        private B3IsGameEnabledSetting m_spirit76GameSetting;
        private B3IsGameEnabledSetting m_timeBombGameSetting;
        private B3IsGameEnabledSetting m_uKickemGameSetting;
        private B3IsGameEnabledSetting m_wildBallGameSetting;
        private B3IsGameEnabledSetting m_wildFireGameSetting;
        
        #endregion
        #region Constructor

        public PlayerSettings()
        {
            CrazyboutGameSetting = new B3IsGameEnabledSetting{GameType = B3GameType.Crazybout};
            JailBreakGameSetting = new B3IsGameEnabledSetting { GameType = B3GameType.Jailbreak };
            MayaMoneyGameSetting = new B3IsGameEnabledSetting { GameType = B3GameType.Mayamoney };
            Spirit76GameSetting = new B3IsGameEnabledSetting { GameType = B3GameType.Spirit76 };
            TimeBombGameSetting = new B3IsGameEnabledSetting { GameType = B3GameType.Timebomb };
            UKickemGameSetting = new B3IsGameEnabledSetting { GameType = B3GameType.Ukickem };
            WildBallGameSetting = new B3IsGameEnabledSetting { GameType = B3GameType.Wildball };
            WildFireGameSetting = new B3IsGameEnabledSetting { GameType = B3GameType.Wildfire };
        }

        #endregion

        #region Properties

        public bool PlayerCalibrateTouch
        {
            get
            {
                return m_playerCalibrateTouch;
            }
            set
            {
                m_playerCalibrateTouch = value;
                RaisePropertyChanged("PlayerCalibrateTouch");
            }
        }

        public bool PresstoCollect
        {
            get
            {
                return m_presstoCollect;
            }
            set
            {
                m_presstoCollect = value;
                RaisePropertyChanged("PresstoCollect");
            }
        }

        public bool AnnounceCall
        {
            get
            {
                return m_announceCall;
            }
            set
            {
                m_announceCall = value;
                RaisePropertyChanged("AnnounceCall");
            }
        }

        public bool PlayerScreenCursor
        {
            get
            {
                return m_playerScreenCursor;
            }
            set
            {
                m_playerScreenCursor = value;
                RaisePropertyChanged("PlayerScreenCursor");
            }
        }

        public string TimeToCollect
        {
            get
            {
                return m_timeToCollect;
            }
            set
            {
                m_timeToCollect = value;
                RaisePropertyChanged("TimeToCollect");
            }
        }

        public bool Disclaimer
        {
            get
            {
                return m_disclaimer;
            }
            set
            {
                m_disclaimer = value;
                RaisePropertyChanged("Disclaimer");
            }
        }

        public bool DisclaimerTextId
        {
            get
            {
                return m_disclaimerTextId;
            }
            set
            {
                m_disclaimerTextId = value;
                RaisePropertyChanged("DisclaimerTextId");
            }
        }

        public string PlayerMainVolume { 
            get
            {
                return m_playerMainVolume;
            }
            set
            {
                m_playerMainVolume = value;
                RaisePropertyChanged("PlayerMainVolume");
            }
        }

        public B3IsGameEnabledSetting CrazyboutGameSetting
        {
            get
            {
                return m_crazyboutGameSetting;
            }
            set
            {
                m_crazyboutGameSetting = value;
                RaisePropertyChanged("CrazyboutGameSetting");
            }
        }

        public B3IsGameEnabledSetting JailBreakGameSetting
        {
            get
            {
                return m_jailBreakGameSetting;
            }
            set
            {
                m_jailBreakGameSetting = value; 
                RaisePropertyChanged("JailBreakGameSetting");
            }
        }

        public B3IsGameEnabledSetting MayaMoneyGameSetting
        {
            get
            {
                return m_mayaMoneyGameSetting;
            }
            set
            {
                m_mayaMoneyGameSetting = value;
                RaisePropertyChanged("MayaMoneyGameSetting");
            }
        }

        public B3IsGameEnabledSetting Spirit76GameSetting
        {
            get
            {
                return m_spirit76GameSetting;
            }
            set
            {
                m_spirit76GameSetting = value; 
                RaisePropertyChanged("Spirit76GameSetting");
            }
        }

        public B3IsGameEnabledSetting TimeBombGameSetting
        {
            get
            {
                return m_timeBombGameSetting;
            }
            set
            {
                m_timeBombGameSetting = value;
                RaisePropertyChanged("TimeBombGameSetting");
            }
        }

        public B3IsGameEnabledSetting UKickemGameSetting
        {
            get
            {
                return m_uKickemGameSetting;
            }
            set
            {
                m_uKickemGameSetting = value;
                RaisePropertyChanged("UKickemGameSetting");
            }
        }

        public B3IsGameEnabledSetting WildBallGameSetting
        {
            get
            {
                return m_wildBallGameSetting;
            }
            set
            {
                m_wildBallGameSetting = value;
                RaisePropertyChanged("WildBallGameSetting");
            }
        }

        public B3IsGameEnabledSetting WildFireGameSetting
        {
            get
            {
                return m_wildFireGameSetting;
            }
            set
            {
                m_wildFireGameSetting = value;
                RaisePropertyChanged("WildFireGameSetting");
            }
        }
        #endregion
    }
}
