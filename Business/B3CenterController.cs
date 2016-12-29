#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

//US4333: B3 Center: Notify clients when session starts / end.

using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Windows;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Messages;
using GameTech.Elite.Client.Modules.B3Center.Properties;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using GameTech.Elite.Client.Modules.B3Center.UI;
using GameTech.Elite.Client.Reports;
using GameTech.Elite.Reports;
using GameTech.Elite.UI;
using System.Collections;

namespace GameTech.Elite.Client.Modules.B3Center.Business
{
    /// <summary>
    /// Represents the B3Center application.
    /// </summary>
    internal sealed class B3CenterController : Notifier, IEliteModuleController, IB3CenterController
    {
        #region Member Variables
        private bool m_isInitialized;
        private bool m_isBusy;
        private int m_instanceId = -1;
        private int m_staffId;
        private int m_operatorId;
        private int m_machineId;
        private B3Controller m_b3Controller;
        private MessageRouter m_msgRouter;
        private B3CenterSettings m_settings;
        private LoadingForm m_loadingForm;
        private Window m_mainWindow;
        private Window m_currentWindow;
        private IEnumerable m_moduleFeaturesList;
        private const int B3SessionChangedCommandId = 2030;
        #endregion

        #region Member Methods
        /// <summary>
        /// Performs any processing or data setup needed before the module is
        /// started.
        /// </summary>
        /// <param name="showLoadingWindow">Whether the module should display a
        /// loading or splash screen while performing initialization.</param>
        /// <param name="isTouchScreen">Whether this module is running on a
        /// touchscreen-based device.</param>
        /// <param name="instanceId">The unique identifier of the running
        /// instance of this module.</param>
        /// <returns>true if the initialization was successful; otherwise,
        /// false.</returns>
        public bool Initialize(bool showLoadingWindow, bool isTouchScreen, int instanceId)
        {
            // Check to see if we are already initialized.
            if(IsInitialized)
                return IsInitialized;

            InstanceId = instanceId;

            // Create a settings object with the default values.
            Settings = new B3CenterSettings();

            // Start COM interface to EliteMCP.
            EliteModuleComm modComm;
            int operatorId, machineId;

            try
            {
                modComm = new EliteModuleComm();
                machineId = modComm.GetMachineId(); m_machineId = machineId;
                operatorId = modComm.GetOperatorId(); m_operatorId = operatorId;
                m_staffId = modComm.GetStaffId();
            }
            catch(Exception ex)
            {
                MessageBoxOptions options = 0;

                if(CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft)
                    options = MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign;

                MessageBox.Show(string.Format(CultureInfo.CurrentCulture, Resources.GetDeviceInfoFailed, ex.Message),
                                Resources.B3CenterName, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, options);

                return IsInitialized;
            }

            // Create and show the loading form.
            LoadingForm = new LoadingForm(Settings.DisplayMode)
            {
                ApplicationName = Resources.B3CenterName,
                Version = EliteModule.GetVersion(Assembly.GetExecutingAssembly()),
                Copyright = EliteModule.GetCopyright(Assembly.GetExecutingAssembly()),
                Cursor = System.Windows.Forms.Cursors.WaitCursor
            };

            if(showLoadingWindow)
                LoadingForm.Show();

            LoadingForm.Status = Resources.LoadingMachineInfo;

            CreateApplication();

            if(!LoadSettings(operatorId, machineId))
                return IsInitialized;

            if (!GetB3UserModulePermission())
            {
                    return IsInitialized;   
            }

            if(!CreateLogger())
                return IsInitialized;

            // Check to see if we only want to display in English.
            if(Settings.ForceEnglish)
            {
                EliteModule.ForceEnglish();
                Logger.Log("Forcing English.", LoggerLevel.Configuration);
            }

            if (!CreateControllers())
                return IsInitialized;

            //updating b3 Reports
            UpdateB3Reports();

            if (!ListenForMessages(modComm))
                return IsInitialized;

            LoadingForm.Status = Resources.Starting;

            if(!CreateWindows())
                return IsInitialized;

            // Have the controller notified when the main window opens.
            MainWindow.Loaded += MainWindowLoaded;

            IsInitialized = true;

            Logger.Log("B3Center initialized!", LoggerLevel.Debug);

            return IsInitialized;
        }

        private void UpdateB3Reports()
        {
            //Get Reports from server
            GetB3ReportListMessage message = new GetB3ReportListMessage();
            message.Send();

            if (message.ReturnCode == ServerReturnCode.Success)
            {
                B3Controller.Reports = message.Reports;
            }
            else
                throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, Resources.SessionOperatorListFailed, ServerErrorTranslator.GetReturnCodeMessage(message.ReturnCode)));

            // First check to see if our directory is present.
            string reportPath = Settings.ClientInstallDrive + Settings.ClientinstallDirectory + Settings.ReportDirectory;

            if (!Directory.Exists(reportPath))
            {
                Directory.CreateDirectory(reportPath);
            }

            // Loop through the reports to check to see if we have the latest
            // copy.
            foreach (B3Report report in B3Controller.Reports)
            {
                // Does the file exist?
                report.FileName = reportPath + report.Name;

                if (!File.Exists(report.FileName))
                {
                    LoadingForm.Status = Resources.DownloadingReports;

                    DownloadReport((int)report.Id, report.FileName);
                }

                // Check to see if the MD5 from the server matches this one.
                if (!CheckReportMd5(report.FileName, report.Hash))
                {
                    // The hash is wrong, try to download it.
                    LoadingForm.Status = Resources.DownloadingReports;

                    DownloadReport((int)report.Id, report.FileName);

                    // Check it again.
                    if (!CheckReportMd5(report.FileName, report.Hash))
                    {
                        // Something is really wrong, error out.
                        throw new ApplicationException(string.Format(Resources.ErrorDownloadingReports, report.Name));
                    }
                }
            }
        }

        /// <summary>
        /// Starts the process of listening for server initiated messages.
        /// </summary>
        /// <param name="comm">The interface to use when subscribing to server
        /// messages.</param>
        private bool ListenForMessages(EliteModuleComm comm)
        {
            try
            {
                Router = new MessageRouter();

                //ID: 2030 session changed event
                comm.SubscribeToMessage(InstanceId, B3SessionChangedCommandId);

            }
            catch (Exception ex)
            {
                Logger.Log("Failed to listen to messages: " + ex.Message, LoggerLevel.Severe);

                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Downloads a report from the server and saves it to the specified 
        /// path.
        /// </summary>
        /// <param name="reportId">The id of the report to download.</param>
        /// <param name="fileName">The path to save the report to.</param>
        internal static void DownloadReport(int reportId, string fileName)
        {
            // Send the message.
            GetReportMessage getReportMsg = new GetReportMessage((ReportId)reportId);
            getReportMsg.Send();

            // Save the binary data to the path.
            FileStream rptFile = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Write);
            rptFile.Write(getReportMsg.ReportFile, 0, getReportMsg.ReportFile.Length);

            // Clean up.
            rptFile.Flush();
            rptFile.Close();
            rptFile.Dispose();
        }
        /// <summary>
        /// Compares the fileName's MD5 hash against the one passed in, if the 
        /// match true is returned.
        /// </summary>
        /// <param name="fileName">The path of the file to hash.</param>
        /// <param name="hash">The hash to compare against.</param>
        /// <returns>true if the hashes match; otherwise false.</returns>
        /// <exception cref="System.ArgumentException">hash is null or 0 
        /// length.</exception>
        internal static bool CheckReportMd5(string fileName, byte[] hash)
        {
            if (hash == null || hash.Length == 0)
                throw new ArgumentException("hash");

            // Check to see if the file exists.
            if (!File.Exists(fileName))
                return false;

            // Read the file in.
            FileStream rptFile = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] fileBytes = new byte[rptFile.Length];
            rptFile.Read(fileBytes, 0, fileBytes.Length);

            // Release the file.
            rptFile.Close();
            rptFile.Dispose();

            // Compare the hashes.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] fileHash = md5.ComputeHash(fileBytes);

            if (fileHash.Length != hash.Length)
                return false;

            for (int x = 0; x < fileHash.Length; x++)
            {
                if (fileHash[x] != hash[x])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Creates the WPF application and loads resource dictionaries.
        /// </summary>
        private void CreateApplication()
        {
            LoadingForm.Status = Resources.LoadingResources;

            new Application();

            try
            {
                Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
                Application.ResourceAssembly = Assembly.GetExecutingAssembly();

                Application.Current.Resources.MergedDictionaries.Add(ThemeLoader.LoadTheme(Settings.DisplayMode));
            }
            catch(Exception ex)
            {
                MessageWindow.Show(string.Format(CultureInfo.CurrentCulture, Resources.LoadResourceDictionariesFailed, ex.Message), Resources.B3CenterName, MessageWindowType.Close);
            }
        }

        /// <summary>
        /// Loads the B3Center's settings from the server.
        /// </summary>
        /// <param name="operatorId">The id of the operator to use in the
        /// settings messages.</param>
        /// <param name="machineId">The id of the machine to use in the
        /// settings messages.</param>
        /// <returns>true if success; otherwise false.</returns>
        private bool LoadSettings(int operatorId, int machineId)
        {
            try
            {
                EliteModule.GetModuleSettings(Settings, machineId, operatorId, SettingCategory.GlobalSystemSettings);

                GetB3Settings();
                GetB3GamesEnable();
                GetB3iconColor();
                GetB3MathGamePlay();
            }
            catch(Exception ex)
            {
                MessageWindow.Show(string.Format(CultureInfo.CurrentCulture, Resources.GetSettingsFailed, ex.Message), Resources.B3CenterName, MessageWindowType.Close);
                return false;
            }

            return true;
        }

        private bool GetB3UserModulePermission()
        {
            try
            {
                var sendMessage = new GetStaffModuleFeaturesMessage(m_staffId, 247, 0);//247 is the ModuleID for B3Center (select * from daily.dbo.modules where modulename = 'B3 Center')
                sendMessage.Send();
                m_moduleFeaturesList = sendMessage.ModuleFeatures;
            }
            catch(Exception ex)
            {
                MessageWindow.Show(string.Format(CultureInfo.CurrentCulture, Resources.GetSettingsFailed, ex.Message), Resources.B3CenterName, MessageWindowType.Close);
                return false;
            }       
            return true;
        }
    

        private void GetB3Settings()
        {
            var message = new GetB3SettingsMessage();
            message.Send();

            if (message.ReturnCode == ServerReturnCode.Success)
            {
                Settings.IsMultiOperator = message.IsMultiOperator;
                Settings.IsCommonRngBallCall = message.IsCommonRng;
                Settings.AllowInSessBallChange = message.AllowInSessBallChange;
                Settings.EnforceMix = message.EnforceMix;
                Settings.IsDoubleAccount = message.IsDoubleAccount;
                Settings.B3SettingGlobal_ = message.b3SettingGlobal;

            }
            else
                throw new B3CenterException(string.Format(CultureInfo.CurrentCulture,
                    Resources.GetB3SettingsFailed, ServerErrorTranslator.GetReturnCodeMessage(message.ReturnCode)));

        }

        private void GetB3GamesEnable()
        {

            var message = new GetB3SettingGameEnable();
            message.Send();

            if (message.ReturnCode == ServerReturnCode.Success)
            {
                Settings.B3GameSetting_ = message.ListB3GameSetting;

            }
            else
                throw new B3CenterException(string.Format(CultureInfo.CurrentCulture,
                    Resources.GetB3SettingsFailed, ServerErrorTranslator.GetReturnCodeMessage(message.ReturnCode)));
        }


        private void GetB3iconColor()
        {
            var message = new GetB3ColorIcon();
            message.Send();

            if (message.ReturnCode == ServerReturnCode.Success)
            {
                Settings.B3IconColor_ = message.Listb3IconColor;

            }
            else
                throw new B3CenterException(string.Format(CultureInfo.CurrentCulture,
                    Resources.GetB3SettingsFailed + " color icon.", ServerErrorTranslator.GetReturnCodeMessage(message.ReturnCode)));
        }

        private void GetB3MathGamePlay()
        {
            var message = new GetB3MathGamePlay();
            message.Send();

            if (message.ReturnCode == ServerReturnCode.Success)
            {
                Settings.B3GameMathPlay_ = message.ListB3MathGamePlay;
            }
            else
            {
                throw new B3CenterException(string.Format(CultureInfo.CurrentCulture,
                   Resources.GetB3SettingsFailed + " math game pay table.", ""));
            }
 
        }


        /// <summary>
        /// Creates B3Center's logger.
        /// </summary>
        /// <returns>true if success; otherwise false.</returns>
        private bool CreateLogger()
        {
            try
            {
                Logger = Logger.Create(Resources.B3CenterName, Settings);
                RaisePropertyChanged("Logger");
            }
            catch(Exception ex)
            {
                MessageWindow.Show(string.Format(CultureInfo.CurrentCulture, Resources.LogFailed, ex.Message), Resources.B3CenterName, MessageWindowType.Close);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Creates all the controller for the B3 Center.
        /// </summary>
        /// <returns>true if succes; otherwise false.</returns>
        private bool CreateControllers()
        {
            LoadingForm.Status = Resources.LoadingControllers;

            try
            {
                B3Controller = new B3Controller(this);
            }
            catch (Exception ex)
            {
                Logger.Log("Create controllers failed: " + ex.Message, LoggerLevel.Severe);
                MessageWindow.Show(string.Format(CultureInfo.CurrentCulture, Resources.LoadingControllersFailed, ex.Message), Resources.B3CenterName, MessageWindowType.Close);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Creates the windows used in the application.
        /// </summary>
        private bool CreateWindows()
        {
            try
            {
                MainWindow = new MainWindow(this, new MainViewModel(this), Settings.UseAcceleration);
                CurrentView = MainWindow;
            }
            catch(Exception ex)
            {
                Logger.Log("Create UI failed: " + ex.Message, LoggerLevel.Severe);
                MessageWindow.Show(string.Format(CultureInfo.CurrentCulture, Resources.CreateUIFailed, ex.Message), Resources.B3CenterName, MessageWindowType.Close);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Starts the module.
        /// </summary>
        public void Run()
        {
            if(IsInitialized && MainWindow != null)
            {
                Logger.Log("Starting B3Center.", LoggerLevel.Information);

                Application.Current.Run(MainWindow);
            }
        }

        /// <summary>
        /// Handles when something wants the module to stop.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs object that contains the event
        /// data.</param>
        public void OnStop(object sender, EventArgs e)
        {
            Stop(true);
        }

        /// <summary>
        /// Shuts down the application and, optionally, closes the main window.
        /// </summary>
        /// <param name="closeMainWindow">true if the main window should be
        /// closed; otherwise false.</param>
        public void Stop(bool closeMainWindow)
        {
            if(IsInitialized && MainWindow != null && closeMainWindow)
                MainWindow.Dispatcher.Invoke(new Action(MainWindow.Close), null);

            if(IsInitialized && Application.Current != null)
                Application.Current.Shutdown();
        }

        /// <summary>
        /// Handles when something wants the module to come to the top of the
        /// screen.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs object that contains the event
        /// data.</param>
        public void OnComeToFront(object sender, EventArgs e)
        {
            if(IsInitialized && MainWindow != null)
                MainWindow.Dispatcher.Invoke(new Action(ActivateMainWindow), null);
        }

        /// <summary>
        /// Activates the main window and sets its window state to Normal.
        /// </summary>
        private void ActivateMainWindow()
        {
            if(MainWindow != null)
            {
                MainWindow.WindowState = WindowState.Normal;
                MainWindow.Activate();
            }
        }

        /// <summary>
        /// Handles when a server initiated message is received for the module.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A MessageReceivedEventArgs object that contains the
        /// event data.</param>
        public void OnServerMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            switch (e.CommandId)
            {
                case B3SessionChangedCommandId: //B3_SESSION_CHANGED 
                    if (B3Controller != null)
                    {
                        B3Controller.GetSessionList();
                    }
                    break;
            }
        }

        /// <summary>
        /// Handles when the MainWindow is loaded.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A RoutedEventArgs object that contains the
        /// event data.</param>
        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            MainWindow.Loaded -= MainWindowLoaded;
            DisposeLoadingForm();
        }

        /// <summary>
        /// Displays a window that tells the user the application is closing
        /// because of a server comm. failure.
        /// </summary>
        public void ServerCommFailure()
        {
            Window window = WindowHelper.GetActiveWindow();

            if(window != null)
                MessageWindow.Show(window, Resources.ServerCommFailed, TextAlignment.Center, string.Empty, MessageWindowType.Pause, EliteModule.ServerCommWaitTime);

            Logger.Log("Server communications failed. Shutting down.", LoggerLevel.Severe);
            OnStop(this, EventArgs.Empty);
        }

        /// <summary>
        /// Closes and disposes of the loading form.
        /// </summary>
        private void DisposeLoadingForm()
        {
            if(LoadingForm != null)
            {
                LoadingForm.CloseForm();
                LoadingForm.Dispose();
                LoadingForm = null;
            }
        }

        /// <summary>
        /// Starts the process of exiting the application.
        /// </summary>
        public void StartExit()
        {
            if(MainWindow != null)
                MainWindow.Close();
        }

        /// <summary>
        /// Exits the application (if the program isn't busy).
        /// </summary>
        /// <returns>true if the application is exiting; otherwise
        /// false.</returns>
        public bool Exit()
        {
            if(!IsBusy || (IsBusy && MessageWindow.Show(MainWindow, Resources.ConfirmExit, string.Empty, MessageWindowType.YesNo) == MessageBoxResult.Yes))
            {
                Stop(false);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Closes the current view.
        /// </summary>
        public void CloseCurrentView()
        {
            if(CurrentView != null && CurrentView != MainWindow)
                CurrentView.Close();
        }

        /// <summary>
        /// Displays the specified view.
        /// </summary>
        /// <param name="view">The view to display.</param>
        /// <exception cref="System.ArgumentException">view is
        /// invalid.</exception>
        public void NavigateTo(B3CenterView view)
        {
            switch(view)
            {
                case B3CenterView.About:
                    AboutWindow aboutWin = new AboutWindow(Settings.UseAcceleration, Resources.B3CenterName,
                                                           EliteModule.GetVersion(Assembly.GetExecutingAssembly()),
                                                           EliteModule.GetCopyright(Assembly.GetExecutingAssembly()),
                                                           Resources.ModuleDescription);
                    aboutWin.Owner = MainWindow;
                    CurrentView = aboutWin;
                    aboutWin.ShowDialog();

                    CurrentView = MainWindow;
                    break;

                default:
                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// Performs any process or data clean up needed before the module is
        /// unloaded.
        /// </summary>
        public void Shutdown()
        {
            Logger.Log("B3 Center shutting down.", LoggerLevel.Debug);

            IsInitialized = false;

            CurrentView = null;

            if(MainWindow != null)
            {
                MainWindow.Close();
                MainWindow = null;
            }

            InstanceId = -1;

            Logger.Log("B3 Center shutdown complete.", LoggerLevel.Information);
        }
        #endregion

        #region Member Properties
        /// <summary>
        /// Gets or sets the unique identifier of the running instance of this
        /// module.
        /// </summary>
        private int InstanceId
        {
            get
            {
                return m_instanceId;
            }
            set
            {
                if(m_instanceId != value)
                {
                    m_instanceId = value;
                    RaisePropertyChanged("InstanceId");
                }
            }
        }

        /// <summary>
        /// Gets whether the call to the module's Initialize method was
        /// successful.
        /// </summary>
        public bool IsInitialized
        {
            get
            {
                return m_isInitialized;
            }
            private set
            {
                m_isInitialized = value;
                RaisePropertyChanged("IsInitialized");
            }
        }

        /// <summary>
        /// Gets the the name of this module.
        /// </summary>
        public string Name
        {
            get
            {
                return Resources.B3CenterName;
            }
        }

        /// <summary>
        /// Gets or sets the loading form.
        /// </summary>
        private LoadingForm LoadingForm
        {
            get
            {
                return m_loadingForm;
            }
            set
            {
                m_loadingForm = value;
                RaisePropertyChanged("LoadingForm");
            }
        }

        /// <summary>
        /// Gets the application's main window.
        /// </summary>
        public Window MainWindow
        {
            get
            {
                return m_mainWindow;
            }
            private set
            {
                m_mainWindow = value;
                RaisePropertyChanged("MainWindow");
            }
        }

        /// <summary>
        /// Gets the current, active view.
        /// </summary>
        public Window CurrentView
        {
            get
            {
                return m_currentWindow;
            }
            private set
            {
                if(m_currentWindow != value)
                {
                    m_currentWindow = value;
                    RaisePropertyChanged("CurrentView");
                }
            }
        }

        /// <summary>
        /// Gets the B3 Center's settings.
        /// </summary>
        public B3CenterSettings Settings
        {
            get
            {
                return m_settings;
            }
            private set
            {
                m_settings = value;
                RaisePropertyChanged("Settings");
            }
        }

        /// <summary>
        /// Gets List of Module Features enable
        /// </summary>
        public IEnumerable ModuleFeatureList
        {
            get
            {
                return m_moduleFeaturesList;
            }
            private set
            {
                m_moduleFeaturesList = value;
                RaisePropertyChanged("ModuleFeatureList");
            }
        }

        /// <summary>
        /// Gets whether the controller is performing a long running operation.
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return m_isBusy;
            }
            private set
            {
                m_isBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }

        public B3Controller B3Controller
        {
            get
            {
                return m_b3Controller;
            }
            private set
            {
                m_b3Controller = value;
                RaisePropertyChanged("SessionController");
            }
        }

        public int StaffId
        {
            get
            {
                return m_staffId;
            }
        }

        public int OperatorId
        {
            get
            {
                return m_operatorId;
            }
        }

        public int MachineId
        {
            get
            {
                return m_machineId;
            }
        }

        /// <summary>
        /// Gets or sets the application's message router.
        /// </summary>
        private MessageRouter Router
        {
            get
            {
                return m_msgRouter;
            }
            set
            {
                m_msgRouter = value;
                RaisePropertyChanged("Router");
            }
        }

        #endregion

        #region Static Properties
        /// <summary>
        /// Gets the B3Center's logger.
        /// </summary>
        public static Logger Logger
        {
            get;
            private set;
        }
        #endregion
    }
}