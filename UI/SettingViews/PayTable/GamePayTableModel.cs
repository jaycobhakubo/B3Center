﻿using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews.PayTable
{
    public class GamePayTableModel : Notifier
    {
        private B3MathGamePay m_mathPayTable;
        //private List<B3MathGamePay> m_gamePaytableList;

        public B3MathGamePay MathPayTable
        {
            get
            {
                return m_mathPayTable;
            }
            set
            {
                m_mathPayTable = value;
                RaisePropertyChanged("MathPayTable");
            }
        }

        //public List<B3MathGamePay> GamePayTableList
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

    }


}
