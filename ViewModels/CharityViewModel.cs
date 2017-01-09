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


        private Operator m_operator;

        public CharityViewModel()
        {
         
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

        //public string cOperatorName
        //{
        //    get;
        //    set;
        //    //get
        //    //{
        //    //    return m_operator.OperatorName; //"Helloaaa";
        //    //}
        //    //set
        //    //{
        //    //    m_operator.OperatorName = value;
        //    //    RaisePropertyChanged("CharityOperatorName");
        //    //}

        //}

        //public string cDescription
        //{ 
        //    get; set; 
        //}

        //public string cAddress
        //{
        //    get;
        //    set;
        //}

        //public string cCity
        //{
        //    get;
        //    set;
        //}

        //public string cState
        //{
        //    get;
        //    set;
        //}

        //public string cZipCode
        //{
        //    get;
        //    set;
        //}

        //public string cIconColor
        //{
        //    get;
        //    set;
        //}

        //public string cPhoneNumber
        //{
        //    get;
        //    set;
        //}

        //public string cFaxNumber
        //{
        //    get;
        //    set;
        //}
    }
}
