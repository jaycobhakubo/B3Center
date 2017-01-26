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
using GameTech.Elite.Client.Modules.B3Center.Helpers;
using System.Collections.Specialized;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{
    class OperatorViewModel : GameTech.Elite.Base.ViewModelBase
    {
        #region MEMBERS
        #region (private only)
        private  List<Operator> m_lofOperatorOrginalSetting = new List<Operator>();//I dont think we need to save all old operator.
        private Operator m_OperatorOrginalSettingSelected = new Operator();
        
        #endregion
        #endregion
        #region CONSTRUCTOR
        public OperatorViewModel(ObservableCollection<Operator> operators_, List<B3IconColor> b3Iconcolor)
        {
            B3IconColor = b3Iconcolor;        
            SaveListSettingOriginalValue(operators_.ToList());
            //Do i really have to convert u from a different type just to put u in order.
            var Orderby = operators_.OrderBy(l => l.OperatorName);
            Operators = new ObservableCollection<Operator>(Orderby);
            SelectedOperator = Operators.FirstOrDefault();
            m_OperatorOrginalSettingSelected = (SaveSettingOriginalValue(SelectedOperator));
            SetCommand();
        }
        #endregion

        private bool m_isNew;
        public bool IsNew 
        {
            get { return m_isNew; }
            set 
            { m_isNew = value;
            RaisePropertyChanged("IsNew");
            }

        }

        private int m_colorSelectedIndex;
        public int ColorSelectedIndex
        {
            get { return m_colorSelectedIndex; }
            set
            {
                m_colorSelectedIndex = value;
                RaisePropertyChanged("ColorSelectedIndex");
            }

        }

        #region METHOD
        #region Saved Original State
        private void SaveListSettingOriginalValue(List<Operator> operators_)
        {
            foreach(Operator c  in operators_ )
            {
               m_lofOperatorOrginalSetting.Add(SaveSettingOriginalValue(c));
            }
        }

        private Operator SaveSettingOriginalValue(Operator c)
        {          
                var g = new Operator();
                g.Address = c.Address;
                g.City = c.City;
                g.ContactName = c.ContactName;
                g.FaxNumber = c.FaxNumber;
                g.IconColor = c.IconColor;
                //g.IconColorValue = c.IconColorValue;
                g.OperatorId = c.OperatorId;
                g.OperatorName = c.OperatorName;
                g.OperatorNameDescription = c.OperatorNameDescription;
                g.PhoneNumber = c.PhoneNumber;
                g.State = c.State;
                g.ZipCode = c.ZipCode;
            return g;         
        }
        #endregion
        #endregion
        #region COMMAND/EVENT

        private void SetCommand()
        {
            SaveOperatorcmd = new RelayCommand(parameter =>
            {
                RunSaveCommand();
            });

            DeleteOperatorcmd = new RelayCommand(parameter =>
            {
                RunDeleteCommand();
            });
                 
           UndoChangesCmd = new RelayCommand(parameter => 
                {
                    UndoChanges();
                });
           
            SelectedItemChanged = new DelegateCommand<Operator>(obj =>
            {
                //IsSelectedSetting = (obj.ToString() != SettingSelected) ? true : false;
                //SelectedItemEvent(SettingSelected);
                SelectedItemEvent();
            });

            NewOperatorCmd = new RelayCommand(parameter => 
                {
                    NewOperatorCommand();
                });
          
        }


            
        public ICommand NewOperatorCmd{get;set;}
        private void NewOperatorCommand()
        {
        
            m_selectedOperator = new Operator();
            SelectedOperator.Address = System.String.Empty;
            SelectedOperator.City = System.String.Empty;
            SelectedOperator.ContactName = System.String.Empty;
            SelectedOperator.FaxNumber = System.String.Empty;
            SelectedOperator.IconColor = m_B3IconColor.FirstOrDefault().ColorID;
            //SelectedOperator.IconColorValue = new Business.B3IconColor();
            SelectedOperator.OperatorId = 0;
            SelectedOperator.OperatorName = System.String.Empty;
            SelectedOperator.OperatorNameDescription = System.String.Empty;
            SelectedOperator.PhoneNumber = System.String.Empty;
            SelectedOperator.State = System.String.Empty;
            SelectedOperator.ZipCode = System.String.Empty;
            RaisePropertyChanged("SelectedOperator");
            RaisePropertyChanged("SelectedColor");
            //IsNew = true;           
        }


        #region(delete)
        public ICommand DeleteOperatorcmd { get; set; }
        private void RunDeleteCommand()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            Task save = Task.Factory.StartNew(() => 
                DeleteOperator()
                );
            save.Wait();
            Mouse.OverrideCursor = null;
        }

        public void DeleteOperator()
        {
            try
            {
                //SelectedOperator.IconColor = SelectedOperator.IconColorValue.ColorID;
                SetB3OperatorMessage msg = new SetB3OperatorMessage(SelectedOperator, 1);
                try
                {
                    msg.Send();
                }
                catch
                {
                    if (msg.ReturnCode != ServerReturnCode.Success)
                        throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, "B3 Set Server Setting Failed", ServerErrorTranslator.GetReturnCodeMessage(msg.ReturnCode)));
                }

                //remove item to the collection
                 int indexOperator = m_operators.IndexOf(m_selectedOperator);
                 var Operators_ = m_operators.ToList();
                 Operators_.Remove(m_selectedOperator);
                //update UI
                 Operators = new ObservableCollection<Operator>(Operators_);

                //Auto select operator
                 if (indexOperator == 0 ||  m_operators.Count != 0)
                 {
                     SelectedOperator = m_operators.FirstOrDefault();
                 }
                 else if (m_operators.Count > 0)
                 {
                     SelectedOperator = m_operators[indexOperator - 1];
                 }
                 else
                 {

                 }
            }
            catch
            { }
        }
        #endregion
        #region (itemselectionchanged)
        public ICommand SelectedItemChanged { get; private set; }
        private void SelectedItemEvent()
        {
            if (IsNew == true)
            {
                IsNew = false;
                ColorSelectedIndex = B3IconColor.FindIndex(l => l.ColorID == SelectedOperator.IconColor);
            }
            //Saved current state
            m_OperatorOrginalSettingSelected = SaveSettingOriginalValue(SelectedOperator);

        }
        #endregion
        #region (save)
        public ICommand SaveOperatorcmd { get; set; }
        private void RunSaveCommand()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            Task save = Task.Factory.StartNew(() => SaveOperator());
            save.Wait();
            Mouse.OverrideCursor = null;
        }



        public void SaveOperator()
        {
            bool success = true;
            try
            {
                //SelectedOperator.IconColor = SelectedOperator.IconColorValue.ColorID;
                SetB3OperatorMessage msg = new SetB3OperatorMessage(SelectedOperator, 0);
                try
                {
                    msg.Send();
                }
                catch
                {
                    success = false;
                    if (msg.ReturnCode != ServerReturnCode.Success)
                        throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, "B3 Set Server Setting Failed", ServerErrorTranslator.GetReturnCodeMessage(msg.ReturnCode)));
                }
            
                //lblSavedNotification.Visibility = Visibility.Visible;
            }
            catch
            { 
                success = false;
                //Not sure if this catch will return(exit)
            }

            if (success)
            {
                m_OperatorOrginalSettingSelected = SaveSettingOriginalValue(SelectedOperator);
            }
        }

        #endregion
        #region (undo)
         public ICommand UndoChangesCmd { get; set; }
         public void UndoChanges()
        {
            SelectedOperator = m_OperatorOrginalSettingSelected;
            m_OperatorOrginalSettingSelected= SaveSettingOriginalValue(SelectedOperator);
        }
        #endregion

        #endregion

        #region PROPERTIES
        #region(w members assoc w properties)

         private ObservableCollection<Operator> m_operators;
         public ObservableCollection<Operator> Operators
         {
             get
             {
                 return m_operators;
             }
             set
             {
                 m_operators = value;
                 RaisePropertyChanged("Operators");

             }
         }

         private Operator m_selectedOperator;
         public Operator SelectedOperator
         {
             get { return m_selectedOperator; }
             set
             {
                 if (value != null)
                 {
                     m_selectedOperator = value;
                     SelectedColor = convertToB3Color(value.IconColor);
                  
                 }
                 RaisePropertyChanged("SelectedOperator");
             }
         }

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

        public B3IconColor SelectedColor
        {
            get
            {
                return convertToB3Color(m_selectedOperator.IconColor);
            }
            set
            {
                m_selectedOperator.IconColor = value.ColorID;
                RaisePropertyChanged("SelectedColor");
            }
        }


        private B3IconColor convertToB3Color(int IconColor)
        {
            var colorValue = B3IconColor.Single(l => l.ColorID == IconColor);
            return colorValue;
        }

        
        private List<B3IconColor> m_operatorcolorList;
        public List<B3IconColor>OperatorColorList
        {
            get { return m_operatorcolorList; }
            set { m_operatorcolorList = value;
                RaisePropertyChanged("OperatorColorList");
            }
        }

      

        
        #endregion
        #endregion



    }
}

