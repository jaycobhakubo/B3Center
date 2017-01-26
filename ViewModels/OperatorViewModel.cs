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
        public OperatorViewModel(ObservableCollection<Operator> operators_, List<B3IconColor> b3Iconcolor)
        {
            OperatorColorList = b3Iconcolor;
            Operators = operators_;
            SelectedOperator = Operators.FirstOrDefault();
           
        }


        //private B3IconColor getIconColor(int colorId)
        //{
        //    B3IconColor operatorColor;

        //        return;
        //}

        private List<B3IconColor> m_B3IconColor;
        public List<B3IconColor> B3IconColor
        {
            get { return m_B3IconColor; }
            set
            {
                m_B3IconColor = value;
                RaisePropertyChanged("B3IconColor");
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

        private List<B3IconColor> m_operatorcolorList;
        public List<B3IconColor>OperatorColorList
        {
            get { return m_operatorcolorList; }
            set { m_operatorcolorList = value;
                RaisePropertyChanged("OperatorColorList");
            }
        }

        //private ObservableCollection<Operator> m_operators;
        public ObservableCollection<Operator> Operators
        {
            get;set;
            //get { return m_operators; }
            //set
            //{
            //    m_operators = value;
            //    RaisePropertyChanged("operators");
            //}
        }


        private Operator m_selectedOperator;
        public Operator SelectedOperator
        {
            get { return m_selectedOperator; }
            set
            {
                m_selectedOperator = value;
                RaisePropertyChanged("selectedOperator");
            }
        }   
    }
}


//public Operator Operatorc
//{
//    get { return m_operator; }
//    set
//    {
//        m_operator = value;
//        SelectedColor = m_B3IconColor.Single(l => l.ColorID == value.IconColor);
//        RaisePropertyChanged("Operatorc");

//    }
//}

//private B3IconColor m_selectedColor;
//public B3IconColor SelectedColor
//{
//    get { return m_selectedColor; }
//    set
//    {
//        m_selectedColor = value;
//        RaisePropertyChanged("SelectedColor");
//    }
//}