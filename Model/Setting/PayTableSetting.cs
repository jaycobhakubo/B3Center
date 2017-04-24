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
        //private GamePayTableModel m_gamePayTablemodel = new GamePayTableModel();
        //private B3MathGamePay m_b3MathGamePay;
        #endregion

        #region Constructors


        #endregion

        #region Properties

        //List<B3MathGamePay> B3MathGamePayList
        //{

        //}
        //public GamePayTableModel GamePayTableModelProperty
        //{
        //    get { return m_gamePayTablemodel; }
        //    set
        //    {

        //        m_gamePayTablemodel = value;
        //        RaisePropertyChanged("GamePayTableModelProperty");
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

       //public B3MathGamePay MathPayTable
       //{
       //    get
       //    {
       //        return m_mathPayTableCrazyBout;
       //    }
       //    set
       //    {
       //        m_mathPayTableCrazyBout = value;
       //        RaisePropertyChanged("MathPayTable");
       //    }
       //}

       //public ObservableCollection<B3MathGamePay> LGamePayTable
       //{
       //    get
       //    {
       //        return m_gamePaytableList;
       //    }
       //    set
       //    {
       //        m_gamePaytableList = value;
       //        RaisePropertyChanged("LGamePayTable");
       //    }
       //}

        #endregion
    }
}
