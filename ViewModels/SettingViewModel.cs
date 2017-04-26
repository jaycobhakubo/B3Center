#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply: © 2015 GameTech International, Inc.
#endregion

using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Globalization;
using GameTech.Elite.Base;
using GameTech.Elite.UI;
using GameTech.Elite.Client.Modules.B3Center.Business;
using System.Linq;
using System.Threading;
using GameTech.Elite.Client.Modules.B3Center.UI.SettingViews;
using System.Windows.Controls;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using GameTech.Elite.Client.Modules.B3Center.Messages;
using System.Threading.Tasks;
using GameTech.Elite.Client.Modules.B3Center.Model;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{
    class SettingViewModel : ViewModelBase
    {
        #region Fields
        //Parent
        private B3Controller m_controller;
        //Views/vm
        private GameSettingView m_gameSettingView;
        private SystemSettingView m_systemSettingView;
        private ServerGameSettingView m_serverGameSettingView;
        private SalesSettingView m_salesSettingView;
        private PlayerSettingView m_playerSettingView;
        private SessionSettingView m_sessionSettingView;
        private PayTableSettingView m_payTableSettingView;
        private readonly Lazy<GameSettingVm> m_lazyGameSettingVm;
        private readonly Lazy<PlayerSettingVm> m_lazyPlayerSettingVm;
        private readonly Lazy<SalesSettingVm> m_lazySalesSettingVm;
        private readonly Lazy<ServerSettingVm> m_lazyServerSettingVm;
        private readonly Lazy<SessionSettingVm> m_lazySessionSettingVm;
        private readonly Lazy<SystemSettingVm> m_lazySystemSettingVm;
        private readonly Lazy<PayTableSettingVm> m_lazyPayTableSettingVm;
        //Other
        private List<B3SettingGlobal> m_settingTobeSaved;
        private ObservableCollection<string> m_settingList = new ObservableCollection<string>();
        private B3SettingCategory m_selectedSettingCategoryType;
        private UserControl m_selectedSettingView = new UserControl();
        private B3SettingCategory m_previousB3SettingCategory;
        private bool m_hasChanged;
        private bool m_isRngBallCall;
        private bool m_indicatorVisibility;
        private bool m_btnSaveIsEnabled;
        private int m_borderValue;
        private string m_selectedB3SettingsCategory;
        //static
        private static volatile SettingViewModel m_instance;
        private static readonly object m_syncRoot = new object();
        #endregion

        #region Constructor

        private SettingViewModel()
        {
            m_lazyGameSettingVm = new Lazy<GameSettingVm>(InitializeGameSettingVm, LazyThreadSafetyMode.ExecutionAndPublication);
            m_lazyPlayerSettingVm = new Lazy<PlayerSettingVm>(InitializePlayerSettingVm, LazyThreadSafetyMode.ExecutionAndPublication);
            m_lazySalesSettingVm = new Lazy<SalesSettingVm>(InitializeSalesSettingVm, LazyThreadSafetyMode.ExecutionAndPublication);
            m_lazyServerSettingVm = new Lazy<ServerSettingVm>(InitializeServerSettingVm, LazyThreadSafetyMode.ExecutionAndPublication);
            m_lazySessionSettingVm = new Lazy<SessionSettingVm>(InitializeSessionSettingVm, LazyThreadSafetyMode.ExecutionAndPublication);
            m_lazySystemSettingVm = new Lazy<SystemSettingVm>(InitializeSystemSettingVm, LazyThreadSafetyMode.ExecutionAndPublication);
            m_lazyPayTableSettingVm = new Lazy<PayTableSettingVm>(InitializePayTableSettingVm, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        #endregion

        #region Method(Initialize)

        public void Initialize(B3Controller controller)
        {
            if (controller == null)
                throw new ArgumentNullException();

            m_controller = controller;
            B3IsGameEnabledSettings = new List<B3IsGameEnabledSetting>(m_controller.Settings.B3GameSettings);

            //set commands
            SaveSettingcmd = new RelayCommand(parameter => RunSavedCommand());
            CancelSettingcmd = new RelayCommand(parameter => CancelSetting());

            LoadSettingList(controller.Settings.NorthDakotaMode);
            BtnSaveIsEnabled = true;
            m_isRngBallCall = m_controller.Settings.IsCommonRngBallCall;
        }

        #endregion

        #region Method

        private GameSettingVm InitializeGameSettingVm()
        {
            var gameSettingsFromServer = m_controller.Settings.B3GlobalSettings.Where(l => l.B3SettingCategoryType == B3SettingCategory.Games);
            var gameSettingsVm = new GameSettingVm(new List<B3SettingGlobal>(gameSettingsFromServer), B3IsGameEnabledSettings);
            m_gameSettingView = new GameSettingView(gameSettingsVm);
            return gameSettingsVm;
        }

        private PlayerSettingVm InitializePlayerSettingVm()
        {
            var playerSettingsFromServer = m_controller.Settings.B3GlobalSettings.Where(l => l.B3SettingCategoryType == B3SettingCategory.Player).ToList();
            var playerSettingVm = new PlayerSettingVm(playerSettingsFromServer, B3IsGameEnabledSettings);
            m_playerSettingView = new PlayerSettingView(playerSettingVm);

            return playerSettingVm;
        }

        private SalesSettingVm InitializeSalesSettingVm()
        {
            var saleSettingsFromServer = m_controller.Settings.B3GlobalSettings.Where(l => l.B3SettingCategoryType == B3SettingCategory.Sales).ToList();
            var salesSettingVm = new SalesSettingVm(saleSettingsFromServer);
            m_salesSettingView = new SalesSettingView(salesSettingVm);

            return salesSettingVm;
        }

        private ServerSettingVm InitializeServerSettingVm()
        {
            var serverSettingsFromServer = m_controller.Settings.B3GlobalSettings.Where(l => l.B3SettingCategoryType == B3SettingCategory.ServerGame).ToList();
            var serverSettingVm = new ServerSettingVm(serverSettingsFromServer);
            m_serverGameSettingView = new ServerGameSettingView(serverSettingVm);
            return serverSettingVm;
        }

        private SessionSettingVm InitializeSessionSettingVm()
        {
            var sessionSettingsFromServer = m_controller.Settings.B3GlobalSettings.Where(l => l.B3SettingCategoryType == B3SettingCategory.Session).ToList();
            var sessionSettingVm = new SessionSettingVm(sessionSettingsFromServer);
            m_sessionSettingView = new SessionSettingView(sessionSettingVm);
            return sessionSettingVm;
        }

        private SystemSettingVm InitializeSystemSettingVm()
        {
            var systemSettingsFromServer = m_controller.Settings.B3GlobalSettings.Where(l => l.B3SettingCategoryType == B3SettingCategory.System && l.IsPayTableSettings == false).ToList();
            var systemSettingVm = new SystemSettingVm(systemSettingsFromServer);
            m_systemSettingView = new SystemSettingView(systemSettingVm);
            return systemSettingVm;
        }

        private PayTableSettingVm InitializePayTableSettingVm()
        {
            var payTableSettingsFromServer = m_controller.Settings.B3GlobalSettings.Where(l => l.IsPayTableSettings == true).ToList();
            var payTableSettingVm = new PayTableSettingVm(payTableSettingsFromServer);
            m_payTableSettingView = new PayTableSettingView(payTableSettingVm);
            return payTableSettingVm;
        }


        private void RunSavedCommand()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            SaveSetting();
            Mouse.OverrideCursor = null;
        }

        private void SetNewValue()
        {
            if (SelectedSettingView != null)
            {
                m_settingTobeSaved = new List<B3SettingGlobal>();
                switch (m_selectedSettingCategoryType)
                {
                    case B3SettingCategory.Games:
                        {
                            m_settingTobeSaved = GameSettingsVm.SelectedGameVm.Save();                          
                            break;
                        }
                    case B3SettingCategory.Player:
                        {
                            //save enabled game settings
                            foreach (var gameEnabledSetting in PlayerSettingVm.ModifiedB3GameEnabledSettings)
                            {
                                var setGameEnabledMessage = new SetGameEnableSetting(gameEnabledSetting.GameType, gameEnabledSetting.IsEnabled);
                                try
                                {
                                    setGameEnabledMessage.Send();
                                    if (setGameEnabledMessage.ReturnCode != ServerReturnCode.Success)
                                    {
                                        throw new Exception(ServerErrorTranslator.GetReturnCodeMessage(setGameEnabledMessage.ReturnCode));
                                    }
                                }
                                catch (ServerCommException ex)
                                {
                                    throw new Exception("SetGameEnableSetting: " + ex.Message);
                                }
                            }

                            m_settingTobeSaved = PlayerSettingVm.Save();
                            break;
                        }
                    case B3SettingCategory.Sales:
                        {
                            m_settingTobeSaved = SalesSettingVm.Save();
                            break;
                        }
                    case B3SettingCategory.ServerGame:
                        {
                            m_settingTobeSaved = ServerSettingVm.Save();
                            break;
                        }
                    case B3SettingCategory.Session:
                        {
                            m_settingTobeSaved = SessionSettingVm.Save();
                            break;
                        }
                    case B3SettingCategory.System:
                        {
                            m_settingTobeSaved = SystemSettingVm.Save();
                            break;
                        }
                    case B3SettingCategory.PayTable:
                        {
                            m_settingTobeSaved = PayTableSettingVm.Save();
                            m_hasChanged = PayTableSettingVm.SettingHasChanged;
                            if (m_hasChanged == true)//Check if user requested for any setting update.
                            {                              
                                if (m_settingTobeSaved.Exists(l => l.SettingType == B3SettingType.MathPayTableSetting))//Check if it is a game paytable update.
                                {                                                               
                                    var tempAllgamesetting = new List<B3SettingGlobal>();     //Get all the game that has been changed                                  
                                    foreach (var paytablesetting in m_settingTobeSaved.Where(l => l.SettingType == B3SettingType.MathPayTableSetting))
                                    {
                                        var tempEachgamesetting = new List<B3SettingGlobal>();
                                        switch (paytablesetting.GameType)
                                        {                                          
                                            case B3GameType.Crazybout: tempEachgamesetting = GameSettingsVm.GameCrzyBout.Save(); break;
                                            case B3GameType.Jailbreak: tempEachgamesetting = GameSettingsVm.GameJailBreak.Save(); break;
                                            case B3GameType.Mayamoney: tempEachgamesetting = GameSettingsVm.GameMayaMoney.Save(); break;
                                            case B3GameType.Spirit76: tempEachgamesetting = GameSettingsVm.GameSpirit76.Save(); break;
                                            case B3GameType.Timebomb: tempEachgamesetting = GameSettingsVm.GameTimeBomb.Save(); break;
                                            case B3GameType.Ukickem: tempEachgamesetting = GameSettingsVm.GameUkickEm.Save(); break;
                                            case B3GameType.Wildball: tempEachgamesetting = GameSettingsVm.GameWildBall.Save(); break;
                                            case B3GameType.Wildfire: tempEachgamesetting = GameSettingsVm.GameWildfire.Save(); break;
                                        }
                                        tempAllgamesetting.AddRange(tempEachgamesetting);
                                    }
                                    tempAllgamesetting.Select(c => { c.B3SettingDefaultValue = ""; return c; }).ToList();
                                    m_settingTobeSaved.AddRange(tempAllgamesetting);
                                }
                            }
                            break;
                        }
                }           
            }
        }

        //Update UI for certain B3 Setting changed.
        private void UpdateUIPerSettingChanged(List<B3SettingGlobal> b3settingList)
        {
            foreach (B3SettingGlobal b3setting in b3settingList)
            {
                switch (b3setting.SettingType)
                {
                    case B3SettingType.CommonRngBallCall:
                        {
                            m_isRngBallCall = PayTableSettingVm.PayTableSettings.CommonRngBallCall;
                            var rptViewModel = ReportsViewModel.Instance;          //Show BallCallReport by Game or by session
                            rptViewModel.ReportSelectedIndex = 0;
                            rptViewModel.SetBallCallReportBySessionOrByGame(b3setting.B3SettingValue);
                          
                            break;
                        }
                    case B3SettingType.NorthDakotaMode:
                        {
                            if (b3setting.B3SettingValue == "T")
                            {
                                if (!SettingList.Contains(B3SettingCategory.ServerGame.ToString()))
                                {
                                    var indexOfSalesSetting = SettingList.IndexOf(B3SettingCategory.Sales.ToString());
                                    SettingList.Insert(indexOfSalesSetting + 1, B3SettingCategory.ServerGame.ToString());
                                }
                            }
                            else
                            {
                                if (SettingList.Contains(B3SettingCategory.ServerGame.ToString()))
                                {
                                    SettingList.Remove(B3SettingCategory.ServerGame.ToString());
                                }
                            }
                            break;
                        }
                }
            }
        }

        private void LoadSettingList(bool IsDakotaMode)
        {
            m_settingList.Clear();
            var categories = Enum.GetValues(typeof(B3SettingCategory)).Cast<B3SettingCategory>();

            foreach (var b3SettingCategory in categories)
            {
                if (b3SettingCategory != B3SettingCategory.Operator)
                {
                    if (b3SettingCategory == B3SettingCategory.ServerGame && !IsDakotaMode)
                    {
                        continue;
                    }
                    m_settingList.Add(b3SettingCategory.ToString());
                }             
            }
            SelectedB3SettingsCategory = m_settingList.FirstOrDefault();
        }



        public void SaveSetting()
        {
            try
            {
                SetNewValue();
                if (m_hasChanged == true)//Do not send if no changes was made.
                {
                    SetB3SettingsMessage msg = new SetB3SettingsMessage(m_settingTobeSaved);
                    try
                    {
                        msg.Send();
                        UpdateUIPerSettingChanged(m_settingTobeSaved.Where(l => l.UIUpdateRequired == true && l.B3SettingDefaultValue != l.B3SettingValue).ToList());
                    }
                    catch
                    {
                        if (msg.ReturnCode != ServerReturnCode.Success)
                            throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, "B3 Set Server Setting Failed"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SetGameEnableSetting: " + ex.Message);
            }
        }

        public ObservableCollection<B3MathGamePay> GetB3MathGamePlay(B3GameType gameType)
        {
            var tempResult = new ObservableCollection<B3MathGamePay>(m_controller.Settings.B3MathGamePays.Where(l => l.GameType == gameType));
            return tempResult;
        }

        public bool GetIsRngSetting()
        {
            return m_isRngBallCall;
        }

        public List<B3IsGameEnabledSetting> GetAllB3GameEnableSetting()
        {
            return B3IsGameEnabledSettings;
        }

        public void SelectedItemEvent()
        {
            m_selectedSettingCategoryType = (B3SettingCategory)Enum.Parse(typeof(B3SettingCategory), SelectedB3SettingsCategory);

            if (m_selectedSettingCategoryType != B3SettingCategory.Games)
            {
                if (m_previousB3SettingCategory == B3SettingCategory.Player)
                {
                    B3IsGameEnabledSettings = PlayerSettingVm.B3SettingEnableDisable;
                }
                IndicatorVisibility = true;
            }
            else
            {
                IndicatorVisibility = false;
            }

            SetBorderValue = m_selectedSettingCategoryType == B3SettingCategory.Games ? 0 : 2;
            BtnSaveIsEnabled = true;
            switch (m_selectedSettingCategoryType)
            {
                case B3SettingCategory.Games:
                    {
                        GameSettingsVm = m_lazyGameSettingVm.Value;
                        BtnSaveIsEnabled = GameSettingsVm.SelectedGameVm.Settings.EnableGameSetting.IsEnabled;
                        SelectedSettingView = m_gameSettingView;
                        break;
                    }
                case B3SettingCategory.Player:
                    {
                        PlayerSettingVm = m_lazyPlayerSettingVm.Value;
                        SelectedSettingView = m_playerSettingView;
                        break;
                    }
                case B3SettingCategory.Sales:
                    {
                        SalesSettingVm = m_lazySalesSettingVm.Value;
                        SelectedSettingView = m_salesSettingView;
                        break;
                    }
                case B3SettingCategory.ServerGame:
                    {
                        ServerSettingVm = m_lazyServerSettingVm.Value;
                        SelectedSettingView = m_serverGameSettingView;
                        break;
                    }
                case B3SettingCategory.Session:
                    {
                        SessionSettingVm = m_lazySessionSettingVm.Value;
                        SelectedSettingView = m_sessionSettingView;
                        break;
                    }
                case B3SettingCategory.System:
                    {
                        SystemSettingVm = m_lazySystemSettingVm.Value;
                        SelectedSettingView = m_systemSettingView;
                        break;
                    }
                case B3SettingCategory.PayTable:
                    {
                        PayTableSettingVm = m_lazyPayTableSettingVm.Value;
                        SelectedSettingView = m_payTableSettingView;
                        break; 
                    }
            }

            m_previousB3SettingCategory = m_selectedSettingCategoryType;
        }

        public void CancelSetting()
        {
            switch (m_selectedSettingCategoryType)
            {
                case B3SettingCategory.Games:
                    {
                        GameSettingsVm.ResetSettingsToDefault();
                        break;
                    }
                case B3SettingCategory.Player:
                    {
                        PlayerSettingVm.ResetSettingsToDefault();
                        break;
                    }
                case B3SettingCategory.Sales:
                    {
                        SalesSettingVm.ResetSettingsToDefault();
                        break;
                    }
                case B3SettingCategory.ServerGame:
                    {
                        ServerSettingVm.ResetSettingsToDefault();
                        break;
                    }
                case B3SettingCategory.Session:
                    {
                        SessionSettingVm.ResetSettingsToDefault();
                        break;
                    }
                case B3SettingCategory.System:
                    {
                        SystemSettingVm.ResetSettingsToDefault();
                        break;
                    }
                case B3SettingCategory.PayTable:
                    {
                       PayTableSettingVm.ResetSettingsToDefault();
                        break;
                    }
            }
        }
        #endregion

        #region Properties(singleton instance)

        //singleton instance
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

        #region Properties

        private List<B3IsGameEnabledSetting> B3IsGameEnabledSettings { get; set; }  
        public ICommand SaveSettingcmd { get; set; }
        public ICommand CancelSettingcmd { get; set; }
        public bool IsSelectedSetting { get; set; }
        public ServerSettingVm ServerSettingVm { get; set; }
        public SessionSettingVm SessionSettingVm { get; set; }
        public SalesSettingVm SalesSettingVm { get; set; }
        public PlayerSettingVm PlayerSettingVm { get; set; }
        public SystemSettingVm SystemSettingVm { get; set; }
        public GameSettingVm GameSettingsVm { get; set; }
        public PayTableSettingVm PayTableSettingVm { get; set; }


        public bool IndicatorVisibility
        {
            get { return m_indicatorVisibility; }
            set
            {
                if (m_indicatorVisibility != value)
                {
                    m_indicatorVisibility = value;
                    RaisePropertyChanged("IndicatorVisibility");
                }
            }
        }

        public bool BtnSaveIsEnabled
        {
            get { return m_btnSaveIsEnabled; }
            set
            {
                m_btnSaveIsEnabled = value;
                RaisePropertyChanged("BtnSaveIsEnabled");
            }
        }

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

        public string SelectedB3SettingsCategory
        {
            get { return m_selectedB3SettingsCategory; }
            set
            {
                m_selectedB3SettingsCategory = value;
                RaisePropertyChanged("SelectedB3Category");
            }
        }

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

        public ObservableCollection<Business.Operator> Operators
        {
            get
            {
                if (m_controller == null)
                {
                    return null;
                }
                return m_controller.Operators;
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

        public bool IsClassIib3GameEnable
        {
            get
            {
                return m_controller.Parent.Settings.IsClassIIB3Enable;
            }
        }

        public ObservableCollection<string> SettingList
        {
            get { return m_settingList; }
        }

     
        #endregion
    }
}
