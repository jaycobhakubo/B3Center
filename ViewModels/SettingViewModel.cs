#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply: © 2015 GameTech International, Inc.
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using GameTech.Elite.Base;
using GameTech.Elite.UI;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Properties;
using System.Linq;
using GameTech.Elite.Client.Modules.B3Center.UI.SettingViews;
using System.Windows.Controls;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using GameTech.Elite.Client.Modules.B3Center.Messages;
using System.Threading.Tasks;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{
    class SettingViewModel : ViewModelBase
    {
        #region ENUM
        enum B3SettingId
        {
            MinPlayer = 34,
            GameStartDelay = 35,
            ConsolotionPrize = 36,
            GameRecallPass = 37,
            WaiCountDown = 38,
            PayoutLimit = 39,
            JackpotLimit = 40,
            EnforceMix = 41
        }

        private Dictionary<string, int> m_B3SettingCategory;//Matches the primary key of B3Settingcategory


        #endregion
        #region VAR

        private B3Controller m_controller;
        private GameSettingView m_gameSettingView;// = new GameSettingView();
        private SystemSettingView m_systemSettingView;// = new SystemSettingView();
        private ServerGameSettingView m_serverGameSettingView;// = new ServerGameSettingView();
        private SalesSettingView m_salesSettingView;// = new SalesSettingView();
        private PlayerSettingView m_playerSettingView;// = new PlayerSettingView();
        private SessionSettingView m_sessionSettingView;// = new SessionSettingView();
        private ObservableCollection<B3SettingGlobal> m_b3ServerSetting { get; set; }

        private ServerSetting m_serverSetting;
        private SessionSetting m_sessionSetting;

        #endregion
        #region CONSTRUCTOR
        private SettingViewModel()
        {         
        }
    
        public void Initialize(B3Controller controller)
        {
            if (controller == null)
                throw new ArgumentNullException();

            m_controller = controller;
            m_gameSettingView = new GameSettingView();

            if (IsClassIIB3GameEnable == true)
            {               
            }
            else
            {         
            }
            SetDefaultValue();
            LoadSetting();
            SetCommand();
        }
        #endregion
        #region OTHER ACCESSOR (static)
        //This will access anything that is public on this View Model.
        private static volatile SettingViewModel m_instance;
        private static readonly object m_syncRoot = new Object();
        public static SettingViewModel Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_syncRoot)
                    {
                        if (m_instance == null)
                            m_instance = new SettingViewModel();
                    }
                }
                return m_instance;
            }
        }
        #endregion
        #region METHOD
        private void SetDefaultValue()
        {
            m_B3SettingCategory = new Dictionary<string, int>();
            m_B3SettingCategory.Add("Games", 1);
            m_B3SettingCategory.Add("Operator", 2);
            m_B3SettingCategory.Add("Player", 3);
            m_B3SettingCategory.Add("Sales", 4);
            m_B3SettingCategory.Add("Server Game", 5);
            m_B3SettingCategory.Add("Session", 6);
            m_B3SettingCategory.Add("System", 7);
        }

        private void TranslateThisSettingToServerSettingModel()
        {
      
            switch ((int)m_B3SettingCategory[SettingSelected])
            {
                case 1:
                    { break; }
                case 5:
                    {
                        m_serverSetting = new ServerSetting();
                        m_serverSetting.MinPlayer = (m_b3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.MinPlayer)).B3SettingValue.ToString());
                        m_serverSetting.GameStartDelay = (m_b3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.GameStartDelay)).B3SettingValue.ToString());
                        m_serverSetting.Consolation = (m_b3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.ConsolotionPrize)).B3SettingValue.ToString());
                        m_serverSetting.GameRecallPassw = (m_b3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.GameRecallPass)).B3SettingValue.ToString());
                        m_serverSetting.WaitCountDown = (m_b3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.WaiCountDown)).B3SettingValue.ToString());
                        break;
                    }
                case 6:
                    {
                        m_sessionSetting = new SessionSetting();
                        m_sessionSetting.PayoutLimit = (m_b3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.PayoutLimit)).B3SettingValue.ToString());
                        m_sessionSetting.JackpotLimit = (m_b3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.JackpotLimit)).B3SettingValue.ToString());
                        var tempBool = (m_b3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.EnforceMix)).B3SettingValue.ToString());
                        m_sessionSetting.EnforceMix = (tempBool == "F") ? false : true;
                        break;
                    }

            }
 

          
        }

        private void SetNewValue(ServerSetting _ServerSettingNewValue)
        {
            m_b3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.MinPlayer)).B3SettingValue = _ServerSettingNewValue.MinPlayer;
            m_b3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.GameStartDelay)).B3SettingValue = _ServerSettingNewValue.GameStartDelay;
            m_b3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.ConsolotionPrize)).B3SettingValue = _ServerSettingNewValue.Consolation;
            m_b3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.GameRecallPass)).B3SettingValue = _ServerSettingNewValue.GameRecallPassw;
            m_b3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.WaiCountDown)).B3SettingValue = _ServerSettingNewValue.WaitCountDown;
        }

       private void LoadSetting()
        {
            m_settingList.Clear();
            foreach (var B3Category in m_B3SettingCategory)
            {
                m_settingList.Add(B3Category.Key);
            }
            //m_settingList.Add("Games");
            //m_settingList.Add("System");
            //m_settingList.Add("Player");
            //m_settingList.Add("Sales");
            //m_settingList.Add("Session");
            //m_settingList.Add("Server Game");
                SettingSelected = m_settingList.FirstOrDefault();
        }
        #endregion
        #region COMMAND (w method)

        public ICommand SelectedItemChanged { get; private set; }
        public ICommand SaveSettingcmd { get; set; }
        public ICommand CancelSettingcmd { get; set; }

        private void SetCommand()
        {
            SaveSettingcmd = new RelayCommand(parameter => RunSavedCommand());
            CancelSettingcmd = new RelayCommand(parameter => CancelSetting());
            SelectedItemChanged = new DelegateCommand<string>(obj =>
            {
                IsSelectedSetting = (obj.ToString() != SettingSelected) ? true : false;
                SelectedItemEvent(SettingSelected);
            });
        }

        private void SelectedItemEvent(string SettingName)
        {
            m_b3ServerSetting = new ObservableCollection<B3SettingGlobal>(m_controller.Settings.B3SettingGlobal_.Where(l => l.B3SettingCategoryID == (int)m_B3SettingCategory[SettingSelected]));
            TranslateThisSettingToServerSettingModel();

            UserControl view = null;
            if (SettingName == "Games")
            {
                SetBorderValue = 0;
            }
            else
            {
                SetBorderValue = 2;
            }

            switch (SettingName)
            {
                case "Games":
                    {
                        view = m_gameSettingView;
                        break;
                    }
                case "System":
                    {
                        view = m_systemSettingView;
                        break;
                    }
                case "Player":
                    {
                        view = m_playerSettingView;
                        break;
                    }
                case "Sales":
                    {
                        view = m_salesSettingView;
                        break;
                    }
                case "Session":
                    {
                        //m_b3ServerSetting = new ObservableCollection<B3SettingGlobal>(m_controller.Settings.B3SettingGlobal_.Where(l => l.B3SettingCategoryID == 6));
                        //m_sessionSetting = TranslateThisSettingToServerSettingModel(m_b3ServerSetting);
                    m_sessionSettingView    = new SessionSettingView (SessionSetting_Vm = new SessionSettingVm(m_sessionSetting));
                        view = m_sessionSettingView;
                        break;
                    }
                case "Server Game":
                    {
                        m_serverGameSettingView = new ServerGameSettingView(ServerSetting_Vm = new ServerSettingVm(m_serverSetting));
                        view = m_serverGameSettingView;
                        break;
                    }
            }
            SelectedSettingView = view;
        }

        //WAIT TILL THE COMMAND IS COMPLETED
        private void RunSavedCommand()
        {

            //if (IsEditOrSave == "Edit")
            //{
            //    IsEditOrSave = "Save";
            //    return;
            //}
            //else
            //{
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                Task save = Task.Factory.StartNew(() => SaveSetting());
                save.Wait();
                Mouse.OverrideCursor = null;
                //IsEditOrSave = "Edit";
            //}
        }


        public void SaveSetting()
        {
            try
            {
                SetNewValue(ServerSetting_Vm.ServerSetting_);
                SetB3SettingsMessage msg = new SetB3SettingsMessage(m_b3ServerSetting);
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
            TranslateThisSettingToServerSettingModel();

            switch ((int)m_B3SettingCategory[SettingSelected])
            {
                case 1:
                    {
                        break;
                    }
                case 5:
                    {
                        ServerSetting_Vm.ServerSetting_ = m_serverSetting;
                        break;
                    }
            }
        }

        //private string m_IsEditSave;
        //public string IsEditOrSave
        //{
        //    get { return m_IsEditSave; }
        //    set 
        //    {
        //        m_IsEditSave = value;
        //        RaisePropertyChanged("IsEditOrSave");
        //    }
        //}


        #endregion
        #region PROPERTIES 
        #region (with private member)

        private int m_borderValue;
        public int SetBorderValue
        {
            get { return m_borderValue; }
            set
            {
                if (m_borderValue != value)
                {
                    m_borderValue = value;
                    RaisePropertyChanged("SetBorderValue");
                }
            }
        }

        private string m_settingSelected;
        public string SettingSelected
        {
            get { return m_settingSelected; }
            set
            {
                m_settingSelected = value;
                RaisePropertyChanged("SettingSelected");
            }
        }

        private UserControl m_selectedSettingView = new UserControl();
        public UserControl SelectedSettingView
        {
            get
            {
                return m_selectedSettingView;
            }
            set
            {
                m_selectedSettingView = value;
                RaisePropertyChanged("SelectedSettingView");
            }
        }

       
        private List<string> m_settingList = new List<string>();
        public List<string> SettingList
        {
            get { return m_settingList; }
        }
        #endregion
        #region (w member in other class get set)

        public B3CenterSettings Settings
        {
            get
            {
                if (m_controller == null)
                {
                    return null;
                }
                return m_controller.Settings;
            }
        }

        public ObservableCollection<GameTech.Elite.Client.Modules.B3Center.Business.Operator> Operators
        {
            get
            {
                if (m_controller == null)
                {
                    return null;
                }
                return m_controller.Operators;
            }
            set
            {
                //m_controller.Operators
            }
        }

        public int StaffId
        {
            get
            {
                if (m_controller == null)
                {

                    return 0;
                }
                else
                {
                    return m_controller.Parent.StaffId;
                }
            }
        }

        public int OperatorId
        {
            get
            {
                if (m_controller == null)
                {

                    return 0;
                }
                else
                {
                    return m_controller.Parent.OperatorId;
                }
            }
        }

        public int MachineId
        {
            get
            {
                if (m_controller == null)
                {

                    return 0;
                }
                else
                {
                    return m_controller.Parent.MachineId;
                }
            }
        }

        public bool IsClassIIB3GameEnable
        {
            get
            {
                return m_controller.Parent.Settings.IsClassIIB3Enable;
            }
        }
        #endregion
        #region(no member)

        public bool IsSelectedSetting
        {
            get;
            set;
        }

        public ServerSettingVm ServerSetting_Vm
        {
            get;
            set;
        }

        public SessionSettingVm SessionSetting_Vm
        {
            get;
            set;
        }

        #endregion
        #endregion
    }
}
