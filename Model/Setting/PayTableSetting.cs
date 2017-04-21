using GameTech.Elite.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameTech.Elite.Client.Modules.B3Center.Model.Setting
{
   public class PayTableSetting : Notifier
    {
        #region field

       private bool m_commonRngBallCall;
       private bool m_enforceMix;
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
