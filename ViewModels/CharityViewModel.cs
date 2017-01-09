//using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GameTech.Elite.Client.Modules.B3Center.Business;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{
    class CharityViewModel : GameTech.Elite.Base.ViewModelBase
    {
        //private ObservableCollection<CharityModel> m_charity = new ObservableCollection<CharityModel>();

        private Operator m_operator;
        //private string m_operatorName;

        public CharityViewModel()
        {
         
        }

        public void SetSelecteOperator(Operator operator_)
        {
            Operatorc = operator_;
            CharityOperatorName = m_operator.OperatorName; 
        }

        public Operator Operatorc
        {
            get { return m_operator; }
            set
            {
                m_operator = value; 
                RaisePropertyChanged("Operatorc");
                
            }
        }

        public string CharityOperatorName
        {
            get;
            set;
            //get
            //{
            //    return m_operator.OperatorName; //"Helloaaa";
            //}
            //set
            //{
            //    m_operator.OperatorName = value;
            //    RaisePropertyChanged("CharityOperatorName");
            //}

        }
    }
}
