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

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{
    class SettingViewModel : ViewModelBase
    {

        private static volatile SettingViewModel m_instance;
        private static readonly object m_syncRoot = new Object();
        private B3Controller m_controller;

        private GameSettingView m_gameSettingView;// = new GameSettingView();
        private SystemSettingView m_systemSettingView;// = new SystemSettingView();
        private ServerGameSettingView m_serverGameSettingView;// = new ServerGameSettingView();
        private SalesSettingView m_salesSettingView;// = new SalesSettingView();
        private PlayerSettingView m_playerSettingView;// = new PlayerSettingView();
        private SessionSettingView m_sessionSettingView;// = new SessionSettingView();
        private  OperatorSettingView m_operatorView;


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
            B3Setting b3GameSetting = new B3Setting();
            b3GameSetting.B3GameSetting_ = Settings.B3GameSetting_;//Game enabled 
            b3GameSetting.B3SettingGlobal_ = Settings.B3SettingGlobal_;//All settings
            b3GameSetting.ListB3mathGamePlay_ = Settings.B3GameMathPlay_;
            m_gameSettingView = new GameSettingView(b3GameSetting);
            m_systemSettingView = new SystemSettingView(b3GameSetting.B3SettingGlobal_.Where(l => l.B3SettingCategoryID == 7).ToList());
            m_playerSettingView = new PlayerSettingView(b3GameSetting.B3SettingGlobal_.Where(l => l.B3SettingCategoryID == 3).ToList());
            m_salesSettingView = new SalesSettingView(b3GameSetting.B3SettingGlobal_.Where(l => l.B3SettingCategoryID == 4).ToList());
            m_sessionSettingView = new SessionSettingView(b3GameSetting.B3SettingGlobal_.Where(l => l.B3SettingCategoryID == 6).ToList());
            m_operatorView = new OperatorSettingView();
            m_serverGameSettingView = new ServerGameSettingView(b3GameSetting.B3SettingGlobal_.Where(l => l.B3SettingCategoryID == 5).ToList());

            if (IsClassIIB3GameEnable == true)
            {
                m_serverGameSettingView = new ServerGameSettingView(b3GameSetting.B3SettingGlobal_.Where(l => l.B3SettingCategoryID == 5).ToList());
                //ServerGameSettingToggleButton.Visibility = Visibility.Visible;
            }
            else
            {
                //ServerGameSettingToggleButton.Visibility = Visibility.Collapsed;
            }
            LoadSetting();

        }


        private List<string> m_settingList = new List<string>();

        public List<string> SettingList
        {
            get { return m_settingList; }
        }

        public void LoadSetting()
        {
            m_settingList.Clear();
            //m_settingList.Add("Games");
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

            switch (SettingName)
            {
                //case "Games":
                //    {
                //        view = m_gameSettingView;
                //        break;
                //    }
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
