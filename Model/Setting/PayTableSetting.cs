using GameTech.Elite.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTech.Elite.Client.Modules.B3Center.Business;
using System.Collections.ObjectModel;
using GameTech.Elite.Client.Modules.B3Center.UI.SettingViews.PayTable;

namespace GameTech.Elite.Client.Modules.B3Center.Model.Setting
{
   public class PayTableSetting : Notifier
    {
        #region field

       private bool m_commonRngBallCall;
       private bool m_enforceMix;

        #endregion

        #region Constructors


        #endregion

        #region Properties


       public bool CommonRngBallCall
       {
           get { return m_commonRngBallCall; }
           set
           {
               m_commonRngBallCall = value;
               RaisePropertyChanged("CommonRngBallCall");
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
