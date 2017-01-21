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

        private static volatile SettingViewModel m_instance;
        private static readonly object m_syncRoot = new Object();
        private B3Controller m_controller;
        
        //VIEW
        private GameSettingView m_gameSettingView;// = new GameSettingView();
        private SystemSettingView m_systemSettingView;// = new SystemSettingView();
        private ServerGameSettingView m_serverGameSettingView;// = new ServerGameSettingView();
        private SalesSettingView m_salesSettingView;// = new SalesSettingView();
        private PlayerSettingView m_playerSettingView;// = new PlayerSettingView();
        private SessionSettingView m_sessionSettingView;// = new SessionSettingView();


        private ObservableCollection<B3SettingGlobal> m_b3ServerSetting;
        private ServerSetting m_serverSetting;


       


        #region CONSTRUCTOR
        private SettingViewModel()
        {         
        }

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

        public void Initialize(B3Controller controller)
        {
            if (controller == null)
                throw new ArgumentNullException();

            m_controller = controller;
            m_b3ServerSetting = new ObservableCollection<B3SettingGlobal>(m_controller.Settings.B3SettingGlobal_.Where(l => l.B3SettingCategoryID == 5));          
            m_gameSettingView = new GameSettingView();
            m_serverSetting =  TranslateThisSettingToServerSettingModel(m_b3ServerSetting);
            m_serverGameSettingView = new ServerGameSettingView(ServerSetting_Vm = new ServerSettingVm(m_serverSetting));

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

        private void SetDefaultValue()
        {
            //m_isFocus = false;
            m_IsEditSave = "Edit"; 
        }

 
        public ServerSettingVm ServerSetting_Vm
        {
            get;
            set;
        }

        #region EVENT(selectionchanged)


        private string m_IsEditSave;
        public string IsEditOrSave
        {
            get { return m_IsEditSave; }
            set 
            {
                m_IsEditSave = value;
                RaisePropertyChanged("IsEditOrSave");
            }
        }

        public bool IsSelectedSetting
        {
            get;
            set;
        }

      

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
                        view = m_sessionSettingView;
                        break;
                    }
                case "Server Game":
                    {
                        view = m_serverGameSettingView;
                        break;
                    }
            }


            SelectedSettingView = view;
        }


        //WAIT TILL THE COMMAND IS COMPLETED
        private void RunSavedCommand()
        {

            if (IsEditOrSave == "Edit")
            {
                IsEditOrSave = "Save";
                return;
            }
            else
            {

                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                Task save = Task.Factory.StartNew(() => SaveSetting());
                save.Wait();
                Mouse.OverrideCursor = null;
                IsEditOrSave = "Edit";
            }
        }


       

        public void CancelSetting()
        {
            m_b3ServerSetting = new ObservableCollection<B3SettingGlobal>(m_b3ServerSetting.Reverse());
            m_serverSetting = TranslateThisSettingToServerSettingModel(m_b3ServerSetting);
            ServerSetting_Vm.ServerSettingx = m_serverSetting;
        }


        #endregion

        enum B3SettingId
        {
            MinPlayer = 34,
            GameStartDelay = 35,
            ConsolotionPrize = 36,
            GameRecallPass = 37,
            WaiCountDown = 38
        }

        private ServerSetting TranslateThisSettingToServerSettingModel(ObservableCollection<B3SettingGlobal> _B3Setting)
        {   
              var tempResult                     = new ServerSetting();
              tempResult.MinPlayer               = (_B3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.MinPlayer)).B3SettingValue.ToString());
              tempResult.GameStartDelay          = (_B3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.GameStartDelay)).B3SettingValue.ToString());
              tempResult.Consolation             = (_B3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.ConsolotionPrize)).B3SettingValue.ToString());
              tempResult.GameRecallPassw         = (_B3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.GameRecallPass)).B3SettingValue.ToString());
              tempResult.WaitCountDown           = (_B3Setting.Single(l => Convert.ToInt32(l.B3SettingID) == Convert.ToInt32(B3SettingId.WaiCountDown)).B3SettingValue.ToString());
              return tempResult;
        }


        private void SetNewValue(ServerSetting _ServerSettingNewValue)
        {
            m_b3ServerSetting[0].B3SettingValue = _ServerSettingNewValue.MinPlayer;
            m_b3ServerSetting[1].B3SettingValue = _ServerSettingNewValue.GameStartDelay;
            m_b3ServerSetting[2].B3SettingValue = _ServerSettingNewValue.Consolation;
            m_b3ServerSetting[3].B3SettingValue = _ServerSettingNewValue.GameRecallPassw;
            m_b3ServerSetting[4].B3SettingValue = _ServerSettingNewValue.WaitCountDown;         
        }



        public void SaveSetting()
        {
            try
            {
                SetNewValue(ServerSetting_Vm.ServerSettingx);

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





        private List<string> m_settingList = new List<string>();

        public List<string> SettingList
        {
            get { return m_settingList; }
        }

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

        public void LoadSetting()
        {
            m_settingList.Clear();
            m_settingList.Add("Games");
            m_settingList.Add("System");
            m_settingList.Add("Player");
            m_settingList.Add("Sales");
            m_settingList.Add("Session");
            m_settingList.Add("Server Game");
            SettingSelected = m_settingList.FirstOrDefault();

        }

        private string m_settingSelected;

        public  string SettingSelected
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

        public void SelectionChanged(string SettingName)
        {

          
        }

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
    }
}
