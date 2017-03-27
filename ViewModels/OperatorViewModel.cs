using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GameTech.Elite.Client.Modules.B3Center.Business;
using System.Windows.Input;
using GameTech.Elite.UI;
using GameTech.Elite.Client.Modules.B3Center.Messages;
using System.Globalization;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{
    class OperatorViewModel : Base.ViewModelBase
    {
        #region Fields

        //This will access anything that is public on this View Model.
        private static volatile OperatorViewModel m_instance;
        private static readonly object m_syncRoot = new object();

        private ObservableCollection<Operator> m_orginalOperatorSettings = new ObservableCollection<Operator>();
        private int m_newOperatorId;

        private bool m_showOperator;
        private bool m_isEdit;
        private int m_operatorSelectedIndex;
        private ObservableCollection<Operator> m_operators;
        private List<B3IconColor> m_operatorColorList;
        private Operator m_selectedOperator;

        #endregion

        #region Constructor

        private OperatorViewModel()
        {
        }

        #endregion

        #region Properties
        //singleton
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

        public ICommand NewOperatorCmd { get; set; }

        public ICommand SaveOperatorcmd { get; set; }

        public ICommand DeleteOperatorcmd { get; set; }

        public ICommand CancelCmd { get; set; }

        public bool WorkInProgress { get; set; }

        public bool ShowOperator
        {
            get { return m_showOperator; }
            set
            {
                m_showOperator = value;
                RaisePropertyChanged("ShowOperator");
            }
        }

        public bool IsEdit
        {
            get { return m_isEdit; }
            set
            {
                m_isEdit = value;
                RaisePropertyChanged("IsEdit");
            }
        }

        public int OperatorSelectedIndex
        {
            get { return m_operatorSelectedIndex; }
            set
            {
                m_operatorSelectedIndex = value;
                RaisePropertyChanged("OperatorSelectedIndex");
            }
        }

        public ObservableCollection<Operator> Operators
        {
            get { return m_operators; }
            set
            {
                m_operators = value;
                RaisePropertyChanged("Operators");//Using this event UI not changing
            }
        }

        public List<B3IconColor> OperatorColorList
        {
            get { return m_operatorColorList; }
            set
            {
                m_operatorColorList = value;
                RaisePropertyChanged("OperatorColorList");
            }
        }

        public Operator SelectedOperator
        {
            get
            {
                return m_selectedOperator;
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

        #endregion

        #region Methods

        public void Initialize(ObservableCollection<Operator> operators, List<B3IconColor> b3Iconcolor)
        {
            var orderBy = operators.OrderBy(l => l.OperatorName);
            m_operators = new ObservableCollection<Operator>(orderBy);
            m_selectedOperator = new Operator();
            m_operatorColorList = b3Iconcolor;
            OperatorSelectedIndex = -1;
            SaveListSettingOriginalValue(operators);
            ShowOperator = false;
            IsEdit = true;
            SetCommand();
        }

        //Save a back up of the current operator.
        //Will be used in undoing changes made from the active Operator observable collection
        private void SaveListSettingOriginalValue(ObservableCollection<Operator> operators)
        {
            var operatorListInOrder = operators.OrderBy(l => l.OperatorName);
            var tempResult = new ObservableCollection<Operator>(operatorListInOrder);
            m_orginalOperatorSettings = new ObservableCollection<Operator>();

            foreach (Operator c in tempResult)
            {
                m_orginalOperatorSettings.Add(SaveSettingOriginalValue(c));
            }
        }

        //This will break the binding from the active observable collection.
        private Operator SaveSettingOriginalValue(Operator c)
        {
            var newOperator = new Operator
            {
                Address = c.Address,
                City = c.City,
                ContactName = c.ContactName,
                FaxNumber = c.FaxNumber,
                IconColor = c.IconColor,
                OperatorId = c.OperatorId,
                OperatorName = c.OperatorName,
                OperatorNameDescription = c.OperatorNameDescription,
                PhoneNumber = c.PhoneNumber,
                State = c.State,
                ZipCode = c.ZipCode
            };

            return newOperator;
        }

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
                
        private void NewOperatorCommand()
        {
            WorkInProgress = true;
            m_selectedOperator = new Operator();
            SelectedOperator.Address = string.Empty;
            SelectedOperator.City = string.Empty;
            SelectedOperator.ContactName = string.Empty;
            SelectedOperator.FaxNumber = string.Empty;

            var firstOrDefault = m_operatorColorList.FirstOrDefault();
            if (firstOrDefault != null)
                SelectedOperator.IconColor = firstOrDefault.ColorID;

            SelectedOperator.OperatorId = 0;
            SelectedOperator.OperatorName = string.Empty;
            SelectedOperator.OperatorNameDescription = string.Empty;
            SelectedOperator.PhoneNumber = string.Empty;
            SelectedOperator.State = string.Empty;
            SelectedOperator.ZipCode = string.Empty;

            RaisePropertyChanged("SelectedOperator");

            OperatorSelectedIndex = -1;
            ShowOperator = true;
            IsEdit = false;
            WorkInProgress = false;
        }

        private void RunSaveCommand()
        {
            if (m_isEdit)
            {
                IsEdit = false;
                return;
            }

            WorkInProgress = true;
            Mouse.OverrideCursor = Cursors.Wait;

            SaveNewOperator();

            Mouse.OverrideCursor = null;
            var operators = m_operators.ToList();
            if (SelectedOperator.OperatorId == 0)
            {
                SelectedOperator.OperatorId = m_newOperatorId;
                operators.Add(m_selectedOperator);
            }

            Operators = new ObservableCollection<Operator>(operators.OrderBy(l => l.OperatorName));//Update UI and collection                                                                                                        //SaveListSettingOriginalValue(Operators.ToList());
            SaveListSettingOriginalValue(Operators);

            IsEdit = true;
            ShowOperator = false;
            OperatorSelectedIndex = -1;
            WorkInProgress = false;
        }

        public void SaveNewOperator()
        {
            SetB3OperatorMessage msg = new SetB3OperatorMessage(SelectedOperator, 0);
            try
            {
                msg.Send();
            }
            catch
            {
                if (msg.ReturnCode != ServerReturnCode.Success)
                    throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, "B3 Set Server Setting Failed"));
            }

            if (SelectedOperator.OperatorId == 0)
            {
                m_newOperatorId = msg.OperatorID;
            }
        }
        
        public void CancelCommand()
        {
            WorkInProgress = true;
            Operators = m_orginalOperatorSettings;
            SaveListSettingOriginalValue(Operators);
            IsEdit = true;
            OperatorSelectedIndex = -1;
            ShowOperator = false;
            WorkInProgress = false;
        }

        private void RunDeleteCommand()
        {
            WorkInProgress = true;

            Mouse.OverrideCursor = Cursors.Wait;
            
            DeleteOperator();

            Mouse.OverrideCursor = null;
            var operators = m_operators.ToList();
            operators.Remove(m_selectedOperator);
            Operators = new ObservableCollection<Operator>(operators.OrderBy(l => l.OperatorName));//Update UI and collection
            SaveListSettingOriginalValue(Operators);
            IsEdit = true;
            OperatorSelectedIndex = -1;
            ShowOperator = false;
            WorkInProgress = false;
        }

        public void DeleteOperator()
        {
            try
            {
                SetB3OperatorMessage msg = new SetB3OperatorMessage(SelectedOperator, 1);
                
                msg.Send();
            }
            catch
            {
                throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, "B3 delete operator message failed"));
            }

        }

        public void SelectedItemChangevm(int currentOpertorIndex)
        {
            WorkInProgress = true;
            Operators = m_orginalOperatorSettings;
            OperatorSelectedIndex = currentOpertorIndex;
            WorkInProgress = false;

            SaveListSettingOriginalValue(m_operators);
            if (m_selectedOperator != null)
            {
                if (m_selectedOperator.OperatorId != -1)
                {
                    if (m_showOperator != true)
                    {
                        ShowOperator = true;
                    }

                    IsEdit = true;
                }
                else
                {
                    ShowOperator = false;
                }
            }
            else
            {
                ShowOperator = false;
            }
        }

        #endregion
    }
}

