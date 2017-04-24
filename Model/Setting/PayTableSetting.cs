using GameTech.Elite.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTech.Elite.Client.Modules.B3Center.Business;
using System.Collections.ObjectModel;
using GameTech.Elite.Client.Modules.B3Center.UI.SettingViews.PayTable;
using System.Windows;

namespace GameTech.Elite.Client.Modules.B3Center.Model.Setting
{
   public class PayTableSetting : Notifier
    {
        #region field

       private bool m_commonRngBallCall;
       private bool m_enforceMix;
       private B3IsGameEnabledSetting m_crazyboutGameSetting = new B3IsGameEnabledSetting();
       private B3IsGameEnabledSetting m_jailBreakGameSetting = new B3IsGameEnabledSetting();
       private B3IsGameEnabledSetting m_mayaMoneyGameSetting = new B3IsGameEnabledSetting();
       private B3IsGameEnabledSetting m_spirit76GameSetting = new B3IsGameEnabledSetting();
       private B3IsGameEnabledSetting m_timeBombGameSetting = new B3IsGameEnabledSetting();
       private B3IsGameEnabledSetting m_uKickemGameSetting = new B3IsGameEnabledSetting();
       private B3IsGameEnabledSetting m_wildBallGameSetting = new B3IsGameEnabledSetting();
       private B3IsGameEnabledSetting m_wildFireGameSetting = new B3IsGameEnabledSetting();

        #endregion

        #region Constructors


        #endregion

        #region Properties

       //private Visibility m_VisibleTest;
       //public Visibility VisibleTest
       //{
       //    get { return m_VisibleTest; }
       //    set
       //    {
       //        m_VisibleTest = value;
       //        RaisePropertyChanged("VisibleTest");
       //    }
       //}

       //private Visibility m_VisibleTest;
       //public Visibility VisibleTest { get; set; }
       //{
       //    get { return m_VisibleTest; }
       //    set
       //    {
       //        m_VisibleTest = value;
       //        RaisePropertyChanged("VisibleTest");
       //    }
       //}

       public bool CommonRngBallCall
       {
           get { return m_commonRngBallCall; }
           set
           {
               m_commonRngBallCall = value;
               RaisePropertyChanged("CommonRngBallCall");
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

        //public bool m_enforceMixEnable;
        //public bool EnforceMixEnable
        //{
        //    get
        //    {
        //        return m_enforceMixEnable;
        //    }
        //    set
        //    {
        //        m_enforceMixEnable = value;
        //        RaisePropertyChanged("EnforceMixEnable");
        //    }
        //}

        public bool EnforceMix
       {
           get
           {
               return m_enforceMix;
           }
           set
           {
               m_enforceMix = value;
               RaisePropertyChanged("EnforceMix");
           }
       }


        #endregion
    }
}
