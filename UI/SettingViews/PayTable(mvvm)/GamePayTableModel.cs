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

        private List<B3MathGamePay> m_b3MathGamePayList;
        public List<B3MathGamePay> B3MathGamePayList
        {
            get { return m_b3MathGamePayList; }
            set
            {
                m_b3MathGamePayList = value;
                RaisePropertyChanged("B3MathGamePayList");
            }
        }

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
    }
}
