using GameTech.Elite.Client.Modules.B3Center.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GameTech.Elite.Client.Modules.B3Center.Business;
using System.Windows.Controls;
//using GameTech.Elite.Base;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{
    class OperatorViewModel : GameTech.Elite.Base.ViewModelBase
    {
        //This is where get the operator message happen.

        private ObservableCollection<Operator> m_operators;

        public ObservableCollection<Operator> operators
        {
            get { return m_operators; }

            set
            {
                m_operators = value;
                RaisePropertyChanged("operators");
            }
        }


        private Operator m_selectedOperator;
        private CharityViewModel m_charityVm;
        public Operator selectedOperator
        {
            get { return m_selectedOperator; }
            set
            {
                m_selectedOperator = value;
                m_charityVm.SetSelecteOperator(value);
                charityVm = m_charityVm;
                RaisePropertyChanged("selectedOperator");
            }
        }


        public CharityViewModel charityVm
        {
            get { return m_charityVm; }
            set { m_charityVm = value;
            RaisePropertyChanged("charityVm");
            }
        }

        public string curOperatorName
        {
            get {return selectedOperator.OperatorName; }
            set {
                selectedOperator.OperatorName = value;
                RaisePropertyChanged("curOperatorName");
            }
        }

        ObservableCollection<OperatorModel> m_operatorso;
        public ObservableCollection<OperatorModel> operatorso
        {
            get { return m_operatorso; }
            set { m_operatorso = value; }
        }

        public OperatorViewModel(ObservableCollection<Operator> operators_)
        {
            operators = operators_;
            m_charityVm = new CharityViewModel();
            selectedOperator = operators.FirstOrDefault();
          
            //charityVm = new CharityViewModel(selectedOperator);
           // var testop = GameTech.Elite.Base.Operator;
        }

        //public string OperatorName
        //{
        //    //get {return Operators. };
        //    //set;
        //}
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ContactName { get; set; }
        public string IconColor { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
    }
}
