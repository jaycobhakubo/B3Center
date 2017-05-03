using GameTech.Elite.Base;
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

        private B3MathGamePay m_mathPayValue;
        public B3MathGamePay MathPayValue
        {
            get
            {
                return m_mathPayValue;
            }
            set
            {
                m_mathPayValue = value;
                RaisePropertyChanged("MathPayValue");
            }
        }

        private bool m_isGameEnable;
        public bool IsGameEnable
        {
            get { return m_isGameEnable; }
            set
            {
                m_isGameEnable = value;
                RaisePropertyChanged("IsGameEnable");
            }
        }
    }
}
