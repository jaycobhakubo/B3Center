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
using System.Windows;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{
    class OperatorViewModel : GameTech.Elite.Base.ViewModelBase
    {

        public enum CurrentOperation
        {         
            SelectedItem = 1,
            Cancel = 2,
            None = 3,
        }

        #region MEMBERS
        #region (private only)
        private  List<Operator> m_lofOperatorOrginalSetting = new List<Operator>();//I dont think we need to save all old operator.
        private Operator m_OperatorOrginalSettingSelected = new Operator();
        #endregion
        #endregion
        #region CONSTRUCTOR
        public OperatorViewModel(/**/)
        {     
        }

         
        public void Initialize(ObservableCollection<Operator> operators_, List<B3IconColor> b3Iconcolor)
        {
            var Orderby = operators_.OrderBy(l => l.OperatorName);
            m_operators = new ObservableCollection<Operator>(Orderby);
            m_selectedOperator = new Operator();
            m_operatorcolorList = b3Iconcolor;         
            OperatorSelectedIndex = -1;
            cOperation = CurrentOperation.None;
            ShowOper = false;
            IsEdit = true;
            SetCommand();
        }         

        //This will access anything that is public on this View Model.
        private static volatile OperatorViewModel m_instance;
        private static readonly object m_syncRoot = new Object();
        public static OperatorViewModel Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_syncRoot)
                    {
                        if (m_instance == null)
                            m_instance = new OperatorViewModel();
                    }
                }
                return m_instance;
            }
        }
        #endregion

        private bool m_isEdit;
        public bool IsEdit
        {
            get { return m_isEdit; }
            set
            {
                m_isEdit = value;
                RaisePropertyChanged("IsEdit");
            }
        }

        #region METHOD

        private void SetDefaultView()
        {
            if (m_selectedOperator != null)//New and current item selected
            {
                //if (m_selectedOperator.OperatorId != 0)//Selected Item ok
                //{
                if (ShowOper != true)
                {
                    ShowOper = true;

                }

                if (ShowOper != true)
                {
                    ShowOper = true;
                }
                //}
                //else//New
                //{


                //}             
            }
            else//Cancel
            {
                ShowOper = false;
            }


        }


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

        //True yes changes were made false no changes were made.
        private bool IsChanges(Operator currentState, Operator prevState)
        {
            bool result = false;
            if (currentState.Address != prevState.Address
            || currentState.City != prevState.City
            || currentState.ContactName != prevState.ContactName
            || currentState.FaxNumber != prevState.FaxNumber
            || currentState.IconColor != prevState.IconColor
            || currentState.OperatorName != prevState.OperatorName
            || currentState.OperatorNameDescription != prevState.OperatorNameDescription
            || currentState.PhoneNumber != prevState.PhoneNumber
            || currentState.State != prevState.State
            || currentState.ZipCode != prevState.ZipCode)
            {
                result = true;
            }

            return result;
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

            CancelCmd = new RelayCommand(parameter =>
            {
                CancelCommand();
            });

            //UndoChangesCmd = new RelayCommand(parameter => 
            //     {
            //         UndoChanges();
            //     });

            //SelectedItemChanged = new DelegateCommand<Operator>(obj =>
            //{
            //IsSelectedSetting = (obj.ToString() != SettingSelected) ? true : false;
            //SelectedItemEvent(SettingSelected);
            //    SelectedItemEvent();
            //});

            NewOperatorCmd = new RelayCommand(parameter =>
               {
                   NewOperatorCommand();
               });        
        }


        #region(new)
        public ICommand NewOperatorCmd{get;set;}
        private void NewOperatorCommand()
        {        
            m_selectedOperator = new Operator();
            SelectedOperator.Address = System.String.Empty;
            SelectedOperator.City = System.String.Empty;
            SelectedOperator.ContactName = System.String.Empty;
            SelectedOperator.FaxNumber = System.String.Empty;
            SelectedOperator.IconColor = m_operatorcolorList.FirstOrDefault().ColorID;       
            SelectedOperator.OperatorId = 0;
            SelectedOperator.OperatorName = System.String.Empty;
            SelectedOperator.OperatorNameDescription = System.String.Empty;
            SelectedOperator.PhoneNumber = System.String.Empty;
            SelectedOperator.State = System.String.Empty;
            SelectedOperator.ZipCode = System.String.Empty;
            RaisePropertyChanged("SelectedOperator");
            RaisePropertyChanged("SelectedColor");
            OperatorSelectedIndex = -1;
            m_OperatorOrginalSettingSelected = SaveSettingOriginalValue(SelectedOperator);
            //IsNew = true;           
        }
        #endregion        
        #region (itemselectionchanged)
        public ICommand SelectedItemChanged { get; private set; }
        public void SelectedItemChangevm()
        {
           
                if (m_selectedOperator != null)
                {
                    if (m_selectedOperator.OperatorId != -1)
                    {
                        if (m_showOper != true)
                        {
                        m_OperatorOrginalSettingSelected = SaveSettingOriginalValue(SelectedOperator);
                        cOperation = CurrentOperation.SelectedItem;
                            ShowOper = true;
                        }
                    }
                    else
                    {
                        ShowOper = false;
                    }
                }
                else
                {
                    ShowOper = false;
                }
         }
                
        
        #endregion
        #region (save)
        public ICommand SaveOperatorcmd { get; set; }
        private void RunSaveCommand()
        {
            if (m_isEdit == true)
            {
                IsEdit = false;
                return;
            }

            //if (m_isEdit == false)
            //{
            //    IsEdit = true;
            //    OperatorSelectedIndex = -1;
            //    ShowOper = false;
            //    return;
            //}

            else
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            if (SelectedOperator.OperatorId != 0)//Update Save
            {
                Task save = Task.Factory.StartNew(() => UpdateSelectedOperator());
                save.Wait();
            }
            else//New Save
            {
                Task save = Task.Factory.StartNew(() => SaveNewOperator());
                save.Wait();
            }
            Mouse.OverrideCursor = null;
            IsEdit = true;
            OperatorSelectedIndex = -1;
            ShowOper = false;
        }
    

        
        public void UpdateSelectedOperator()//Update
        {
            bool success = true;
            try
            {
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

                var Operators_ = m_operators.ToList();
                Operators = new ObservableCollection<Operator>(Operators_.OrderBy(l => l.OperatorName));//Update UI and collection
                //IsEdit = true;
                //OperatorSelectedIndex = -1;
                //ShowOper = false;
            }
            catch
            {
                success = false;
            }

            if (success)
            {
              //  m_OperatorOrginalSettingSelected = SaveSettingOriginalValue(SelectedOperator);
            }
        }



        public void SaveNewOperator()//new
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

                if (SelectedOperator.OperatorId == 0)
                {
                    m_selectedOperator.OperatorId = msg.OperatorID;
                }

                var Operators_ = m_operators.ToList();            
                Operators_.Add(m_selectedOperator);                  
                Operators = new ObservableCollection<Operator>(Operators_.OrderBy(l => l.OperatorName));//Update UI and collection
                var indexofcurrentoperator = m_operators.IndexOf(SelectedOperator);
                OperatorSelectedIndex = indexofcurrentoperator;            
            }
            catch
            { 
                success = false;
            }

            if (success)
            {
                m_OperatorOrginalSettingSelected = SaveSettingOriginalValue(SelectedOperator);
            }
        }

        #endregion
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
                SetB3OperatorMessage msg = new SetB3OperatorMessage(SelectedOperator, 1);
                try { msg.Send(); }
                catch
                {
                    if (msg.ReturnCode != ServerReturnCode.Success)
                        throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, "B3 Set Server Setting Failed", ServerErrorTranslator.GetReturnCodeMessage(msg.ReturnCode)));
                }

                int indexOperator = m_operators.IndexOf(m_selectedOperator); //removed item inside the collection
                var Operators_ = m_operators.ToList();
                Operators_.Remove(m_selectedOperator);
                Operators = new ObservableCollection<Operator>(Operators_.OrderBy(l => l.OperatorName));//Update UI and collection

                OperatorSelectedIndex = -1;
                m_selectedOperator = new Operator();
            }
            catch
            { }
        }
        #endregion
        #region (undo)
        public ICommand UndoChangesCmd { get; set; }
        public void UndoChanges()
        {
            SelectedOperator = m_OperatorOrginalSettingSelected;
            m_OperatorOrginalSettingSelected = SaveSettingOriginalValue(SelectedOperator);
        }
        #endregion

        public CurrentOperation cOperation { get; set; }

        public ICommand CancelCmd { get; set; }
        public void CancelCommand()
        {
            cOperation = CurrentOperation.Cancel;
            if (OperatorSelectedIndex != -1)
            {
                OperatorSelectedIndex = -1;
                ShowOper = false;
            }
            cOperation = CurrentOperation.None;

            SelectedOperator = m_OperatorOrginalSettingSelected;
            m_OperatorOrginalSettingSelected = SaveSettingOriginalValue(SelectedOperator);
        }

        #endregion

        #region PROPERTIES

        private bool m_showOper;
        public bool ShowOper
        {
            get { return m_showOper; }
            set
            {
                m_showOper = value;
                RaisePropertyChanged("ShowOper");
            }
        }

        public bool ShowDefault
        {
            get;
            set;
        }

        public int SelectedColorIndex
        {
            get; set;
        }


        private int m_operatorSelectedIndex;
        public int OperatorSelectedIndex
        {
            get { return m_operatorSelectedIndex; }
            set
            {
                m_operatorSelectedIndex = value;
                RaisePropertyChanged("OperatorSelectedIndex");
            }
        }

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

        private List<B3IconColor> m_operatorcolorList;
        public List<B3IconColor> OperatorColorList
        {
            get { return m_operatorcolorList; }
            set
            {
                m_operatorcolorList = value;
                RaisePropertyChanged("OperatorColorList");
            }
        }

        private Operator m_selectedOperator;
        public Operator SelectedOperator
        {
            get
            {
                if (m_selectedOperator != null)
                {
                    return m_selectedOperator;
                }
                return null;
            }
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

        private bool m_isNew;
        public bool IsNew
        {
            get { return m_isNew; }
            set
            {
                m_isNew = value;
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

        public B3IconColor SelectedColor
        {
            get
            {
                if (m_selectedOperator != null)
                {
                    return convertToB3Color(m_selectedOperator.IconColor);
                }
                return null;
            }
        
            set
            {
                m_selectedOperator.IconColor = value.ColorID;
                RaisePropertyChanged("SelectedColor");
            }
        }

        private B3IconColor convertToB3Color(int IconColor)
        {
            var colorValue = OperatorColorList.Single(l => l.ColorID == IconColor);
            return colorValue;
        }

     

        
        #endregion
        #endregion
    }
}

