using GameTech.Elite.Client.Modules.B3Center.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GameTech.Elite.Client.Modules.B3Center.Business;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading.Tasks;
using GameTech.Elite.UI;
using Microsoft.Practices.Composite.Presentation.Commands;
using GameTech.Elite.Client.Modules.B3Center.Messages;
using System.Globalization;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{
    class OperatorViewModel : GameTech.Elite.Base.ViewModelBase
    {
        public OperatorViewModel(ObservableCollection<Operator> operators_, List<B3IconColor> b3Iconcolor)
        {
            B3IconColor = b3Iconcolor;
            Operators = operators_;
            SelectedOperator = Operators.FirstOrDefault();
            SetCommand();
        }


        #region COMMAND ()

        public ICommand SelectedItemChanged { get; private set; }
        public ICommand SaveSettingcmd { get; set; }
        public ICommand CancelSettingcmd { get; set; }

        private void SetCommand()
        {
            SaveSettingcmd = new RelayCommand(parameter => RunSavedCommand());
            CancelSettingcmd = new RelayCommand(parameter => CancelSetting());
            SelectedItemChanged = new DelegateCommand<Operator>(obj =>
            {
                //IsSelectedSetting = (obj.ToString() != SettingSelected) ? true : false;
                //SelectedItemEvent(SettingSelected);
                SelectedItemEvent();
            });
        }

        //WAIT TILL THE COMMAND IS COMPLETED
        private void RunSavedCommand()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            Task save = Task.Factory.StartNew(() => SaveSetting());
            save.Wait();
            Mouse.OverrideCursor = null;
        }

        private void SelectedItemEvent()
        {
            //System.Windows.MessageBox.Show("Hi there");
        }

        public void SaveSetting()
        {
            try
            {
                SelectedOperator.IconColor = SelectedOperator.IconColorValue.ColorID;
                SetB3OperatorMessage msg = new SetB3OperatorMessage(SelectedOperator, 0);
                try
                {
                    msg.Send();
                }
                catch
                {
                    if (msg.ReturnCode != ServerReturnCode.Success)
                        throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, "B3 Set Server Setting Failed", ServerErrorTranslator.GetReturnCodeMessage(msg.ReturnCode)));
                }
                //lblSavedNotification.Visibility = Visibility.Visible;
            }
            catch
            { }
        }

        public void CancelSetting()
        {
         
        
        }

        #endregion

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
            get
            {
                return m_selectedOperator.IconColorValue;
            }
            set
            {
                m_selectedOperator.IconColorValue = value;
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

        public ObservableCollection<Operator> Operators
        {
            get;set;
        }

        private Operator m_selectedOperator;
        public Operator SelectedOperator
        {
            get { return m_selectedOperator; }
            set
            {
                m_selectedOperator = value;
                m_selectedOperator.IconColorValue = m_B3IconColor.Single(l => l.ColorID == value.IconColor);
                RaisePropertyChanged("selectedOperator");
            }
        }   
    }
}

