﻿//using GameTech.Elite.Base;
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

        public CharityViewModel(List<B3IconColor> b3Iconcolor)
        {
            B3IconColor = b3Iconcolor;
        }


        public Operator Operatorc
        {
            get { return m_operator; }
            set
            {
                m_operator = value;
                SelectedColor = m_B3IconColor.Single(l => l.ColorID == value.IconColor);
                RaisePropertyChanged("Operatorc");
                
            }
        }

        private B3IconColor m_selectedColor;
        public B3IconColor SelectedColor
        {
            get { return m_selectedColor; }
            set
            {
                m_selectedColor = value;
                RaisePropertyChanged("SelectedColor");
            }
        }


        private List<B3IconColor> m_B3IconColor;
        public List<B3IconColor> B3IconColor
        {
            get {return m_B3IconColor; }
            set
            {
                m_B3IconColor = value;
                RaisePropertyChanged("B3IconColor");
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
