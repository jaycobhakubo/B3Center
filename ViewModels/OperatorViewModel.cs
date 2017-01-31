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
using GameTech.Elite.Client.Modules.B3Center.Messages;
using System.Globalization;
using GameTech.Elite.Client.Modules.B3Center.Helpers;
using System.Collections.Specialized;
using System.Windows;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{
    class OperatorViewModel : GameTech.Elite.Base.ViewModelBase
    {
        #region MEMBERS       
        private ObservableCollection<Operator> m_lofOperatorOrginalSettingcol = new ObservableCollection<Operator>();
        private int m_newOperatorId;
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
            SaveListSettingOriginalValuecol(operators_);
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

        #region METHOD
        //Save a back up of the current operator.
        //Will be used in undoing changes made from the active Operator observable collection
        private void SaveListSettingOriginalValuecol(ObservableCollection<Operator> operators_)
        {
            var OperatorListInOrder = operators_.OrderBy(l => l.OperatorName);
            var tempResult = new ObservableCollection<Operator>(OperatorListInOrder);
            m_lofOperatorOrginalSettingcol = new ObservableCollection<Operator>();

            foreach (Operator c in tempResult)
            {
                m_lofOperatorOrginalSettingcol.Add(SaveSettingOriginalValue(c));
            }
        }
        
        //This will break the binding from the active observable collection.
        private Operator SaveSettingOriginalValue(Operator c)
        {          
                var g = new Operator();
                g.Address = c.Address;
                g.City = c.City;
                g.ContactName = c.ContactName;
                g.FaxNumber = c.FaxNumber;
                g.IconColor = c.IconColor;
                g.OperatorId = c.OperatorId;
                g.OperatorName = c.OperatorName;
                g.OperatorNameDescription = c.OperatorNameDescription;
                g.PhoneNumber = c.PhoneNumber;
                g.State = c.State;
                g.ZipCode = c.ZipCode;
            return g;         
        }
        #endregion
        #region COMMAND/EVENT
        #region (add all command)
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

            NewOperatorCmd = new RelayCommand(parameter =>
               {
                   NewOperatorCommand();
               });        
        }
        #endregion
        #region(new)
        public ICommand NewOperatorCmd{get;set;}
        private void NewOperatorCommand()
        {
            WorkInProgress = true;
            DelBtnIsEnabled = false;
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
            ShowOper = true;
            IsEdit = false;
            WorkInProgress = false;
        }
        #endregion
        #region (itemselectionchanged)
        public void SelectedItemChangevm(int currentOpertorIndex)
        {
            WorkInProgress = true;
                Operators = m_lofOperatorOrginalSettingcol;
                OperatorSelectedIndex = currentOpertorIndex;
            WorkInProgress = false;

            SaveListSettingOriginalValuecol(m_operators);
            if (m_selectedOperator != null)
                {
                    if (m_selectedOperator.OperatorId != -1)
                    {
                        if (m_showOper != true)
                        {
                        ShowOper = true;            
                        }
                    DelBtnIsEnabled = true;                      
                    IsEdit = true;
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

            WorkInProgress = true;
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;        
                Task save = Task.Factory.StartNew(() => SaveNewOperator());
                save.Wait();       
            Mouse.OverrideCursor = null;

            var Operators_ = m_operators.ToList();
            if (SelectedOperator.OperatorId == 0)
            {
                SelectedOperator.OperatorId = m_newOperatorId;
                Operators_.Add(m_selectedOperator);
            }
                Operators = new ObservableCollection<Operator>(Operators_.OrderBy(l => l.OperatorName));//Update UI and collection                                                                                                        //SaveListSettingOriginalValue(Operators.ToList());
            SaveListSettingOriginalValuecol(Operators);

            IsEdit = true;          
            ShowOper = false;
            OperatorSelectedIndex = -1;
            DelBtnIsEnabled = false;
            WorkInProgress = false;

        }
       
     
        public void SaveNewOperator()//new
        {                  
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

                if (SelectedOperator.OperatorId == 0)
                {
                    m_newOperatorId = msg.OperatorID;               
                }
            }
        #endregion
        #region(delete)
        public ICommand DeleteOperatorcmd { get; set; }
        private void RunDeleteCommand()
        {
            WorkInProgress = true;
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            Task save = Task.Factory.StartNew(() =>
                DeleteOperator()
                );
            save.Wait();
            Mouse.OverrideCursor = null;
          //  int indexOperator = m_operators.IndexOf(m_selectedOperator); //removed item inside the collection
            var Operators_ = m_operators.ToList();
            Operators_.Remove(m_selectedOperator);
            Operators = new ObservableCollection<Operator>(Operators_.OrderBy(l => l.OperatorName));//Update UI and collection
            SaveListSettingOriginalValuecol(Operators);
            IsEdit = true;
            OperatorSelectedIndex = -1;
            ShowOper = false;
            DelBtnIsEnabled = false;
            WorkInProgress = false;
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
            }
            catch{ }
        
        }
        #endregion
        #region (cancel)
        public ICommand CancelCmd { get; set; }
        public void CancelCommand()
        {
            WorkInProgress = true;
            Operators = m_lofOperatorOrginalSettingcol;
            SaveListSettingOriginalValuecol(Operators);
            IsEdit = true;
            OperatorSelectedIndex = -1;
            ShowOper = false;
            DelBtnIsEnabled = false;
            WorkInProgress = false;     
        }
        #endregion
        #endregion
        #region PROPERTIES

        public int SelectedColorIndex { get; set; }
        public bool WorkInProgress { get; set; }

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

        private ObservableCollection<Operator> m_operators;
         public ObservableCollection<Operator> Operators
         {
             get { return m_operators; }
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
                }
                RaisePropertyChanged("SelectedOperator");
            }
        }

        private bool m_delBtnEnabled;
        public bool DelBtnIsEnabled
        {
            get { return m_delBtnEnabled; }
            set
            {
                m_delBtnEnabled = value;
                RaisePropertyChanged("DelBtnIsEnabled");
            }
        }

        private bool m_saveBtnIsEnabled;
        public bool SaveBtnIsEnabled
        {
            get { return m_saveBtnIsEnabled; }
            set
            {
                m_saveBtnIsEnabled = value;
                RaisePropertyChanged("DelBtnIsEnabled");
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

        #endregion     
    }
}

