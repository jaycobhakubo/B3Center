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
            //m_b3ServerSetting = new ObservableCollection<B3SettingGlobal>(b3ServerSetting);
            //B3ServerSettingDefault = b3ServerSetting.ToList();


            //B3Setting b3GameSetting = new B3Setting();
            //b3GameSetting.B3GameSetting_ = Settings.B3GameSetting_;//Game enabled 
            //b3GameSetting.B3SettingGlobal_ = Settings.B3SettingGlobal_;//All settings
            //b3GameSetting.ListB3mathGamePlay_ = Settings.B3GameMathPlay_;
            m_gameSettingView = new GameSettingView();// m_gameSettingView = new GameSettingView(b3GameSetting);
            //m_systemSettingView = new SystemSettingView(b3GameSetting.B3SettingGlobal_.Where(l => l.B3SettingCategoryID == 7).ToList());
            //m_playerSettingView = new PlayerSettingView(b3GameSetting.B3SettingGlobal_.Where(l => l.B3SettingCategoryID == 3).ToList());
            //m_salesSettingView = new SalesSettingView(b3GameSetting.B3SettingGlobal_.Where(l => l.B3SettingCategoryID == 4).ToList());
            //m_sessionSettingView = new SessionSettingView(b3GameSetting.B3SettingGlobal_.Where(l => l.B3SettingCategoryID == 6).ToList());
            //m_operatorView = new OperatorView();



           m_serverSetting =  TranslateThisSettingToServerSettingModel(m_b3ServerSetting);
            m_serverGameSettingView = new ServerGameSettingView(ServerSetting_Vm = new ServerSettingVm(m_serverSetting));



            //  m_serverGameSettingView = new ServerGameSettingView(GetServerSetting( b3GameSetting.B3SettingGlobal_.Where(l => l.B3SettingCategoryID == 5).ToList()));
            if (IsClassIIB3GameEnable == true)
            {
                //m_serverGameSettingView = new ServerGameSettingView(b3GameSetting.B3SettingGlobal_.Where(l => l.B3SettingCategoryID == 5).ToList());
                //ServerGameSettingToggleButton.Visibility = Visibility.Visible;
            }
            else
            {
                //ServerGameSettingToggleButton.Visibility = Visibility.Collapsed;
            }
            LoadSetting();
            SetCommand();
        }

        #endregion

        private ObservableCollection<B3SettingGlobal> m_b3ServerSetting;
        //public ObservableCollection<B3SettingGlobal> m_b3ServerSetting
        //{
        //    get { return m_b3ServerSetting; }
        //    set { m_b3ServerSetting = value; }
        //}


        private ServerSetting m_serverSetting;
       

        public ServerSettingVm ServerSetting_Vm
        {
            get;
            set;
        }

      
        private void SetCommand()
        {
            SaveSettingcmd = new RelayCommand(parameter => RunSavedCommand());
            CancelSettingcmd = new RelayCommand(parameter => CancelSetting());
        }

   

        //WAIT TILL THE COMMAND IS COMPLETED
        private void RunSavedCommand()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            Task save = Task.Factory.StartNew(() => SaveSetting());
            save.Wait();
            Mouse.OverrideCursor = null;
        }



        public ICommand SaveSettingcmd { get; set; }
        public ICommand CancelSettingcmd { get; set; }

        public void CancelSetting()
        {
            m_b3ServerSetting = new ObservableCollection<B3SettingGlobal>(m_b3ServerSetting.Reverse());
            m_serverSetting = TranslateThisSettingToServerSettingModel(m_b3ServerSetting);
            ServerSetting_Vm.ServerSettingx = m_serverSetting;
        }


        enum B3SettingId
        {
            MinPlayer = 34,
            GameStartDelay = 35,
            ConsolotionPrize = 36,
            GameRecallPass = 37,
            WaiCountDown = 38
        }

        private ServerSetting TranslateThisSettingToServerSettingModel(ObservableCollection<B3SettingGlobal> m_B3ServerSetting)
        {

         var _m_serverSetting = new ServerSetting();
            _m_serverSetting.MinPlayer = (m_B3ServerSetting[0].B3SettingValue).ToString();
            _m_serverSetting.GameStartDelay = (m_B3ServerSetting[1].B3SettingValue).ToString();
            _m_serverSetting.Consolation =(m_B3ServerSetting[2].B3SettingValue).ToString();
            _m_serverSetting.GameRecallPassw = m_B3ServerSetting[3].B3SettingValue.ToString();
            _m_serverSetting.WaitCountDown = (m_B3ServerSetting[4].B3SettingValue).ToString();
            //    m_serverSetting.MinPlayer = m_B3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == (int)B3SettingId.MinPlayer).B3SettingValue.ToString(); //(m_B3ServerSetting[0].B3SettingValue).ToString();
            //    m_serverSetting.GameStartDelay = m_B3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == (int)B3SettingId.GameStartDelay).B3SettingValue.ToString();
            //    m_serverSetting.Consolation = m_B3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == (int)B3SettingId.ConsolotionPrize).B3SettingValue.ToString();
            //    m_serverSetting.GameRecallPassw = m_B3ServerSettigng.Single(l => Convert.ToInt32(l.B3SettingID) == (int)B3SettingId.GameRecallPass).B3SettingValue.ToString();
            //    m_serverSetting.WaitCountDown = m_B3ServerSetting.Single(l => Convert.ToInt32(l.B3SettingID) == (int)B3SettingId.WaiCountDown).B3SettingValue.ToString();

            return _m_serverSetting;
        }


        private void SetNewValue(ServerSetting New)
        {
            m_b3ServerSetting[0].B3SettingValue = New.MinPlayer;
            //m_b3ServerSetting[0].B3SettingdefaultValue = _default.MinPlayer;

            m_b3ServerSetting[1].B3SettingValue = New.GameStartDelay;
            //m_b3ServerSetting[1].B3SettingdefaultValue = _default.GameStartDelay;

            m_b3ServerSetting[2].B3SettingValue = New.Consolation;
            //m_b3ServerSetting[2].B3SettingdefaultValue = _default.Consolation;

            m_b3ServerSetting[3].B3SettingValue = New.GameRecallPassw;
            //m_b3ServerSetting[3].B3SettingdefaultValue = _default.GameRecallPassw;

            m_b3ServerSetting[4].B3SettingValue = New.WaitCountDown;
            //m_b3ServerSetting[4].B3SettingdefaultValue = _default.WaitCountDown;
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
                        view =m_salesSettingView;
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
