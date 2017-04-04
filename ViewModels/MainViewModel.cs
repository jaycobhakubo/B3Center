#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2011 GameTech International, Inc.
#endregion

using System;
using System.ComponentModel;
using System.Windows.Input;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.UI;
using System.Collections;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{
    /// <summary>
    /// Maintains state for the main window
    /// </summary>
    internal class MainViewModel : ViewModelBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MainVeiwModel class.
        /// </summary>
        public MainViewModel(B3CenterController controller)
        {
            if (controller == null)throw new ArgumentNullException("controller");
            Controller = controller;
            ModuleFeatureList = controller.ModuleFeatureList;
            SessionVm = SessionViewModel.Instance;
            SessionVm.Initialize(controller.B3Controller); 

            foreach (int moduleFeatureId in controller.ModuleFeatureList)     //No need to initialize if staff dont have permission.
            {
                switch (moduleFeatureId)
                {
                    case 43://Reports
                            ReportsVm = ReportsViewModel.Instance;
                            ReportsVm.Initialize(controller.B3Controller);                         
                            HasB3RptPermission = true;
                    break;

                    case 44://Settings
                        SettingVm = SettingViewModel.Instance;
                        SettingVm.Initialize(controller.B3Controller);
                        HasB3SettingPermission = true;
                     break;
                }
            }

            OperatorVm = OperatorViewModel.Instance;
            OperatorVm.Initialize(controller.B3Controller.Operators, controller.B3Controller.Settings.B3IconColors);
            FileExitCommand = new RelayCommand(parameter => Exit());     
            PropertyChangedEventManager.AddListener(Controller, this, string.Empty);    
        }

        private bool m_hasB3SettingPermission;
        public bool HasB3SettingPermission
        {
            get { return m_hasB3SettingPermission; }
            set
            {
                if  (value != m_hasB3SettingPermission)
                {
                    m_hasB3SettingPermission = value;
                    RaisePropertyChanged("HasB3SettingPermission");
                }
            }
        }

        private bool m_hasB3RptPermission;
        public bool HasB3RptPermission
        {
            get { return m_hasB3RptPermission; }
            set 
            {
                if ( value != m_hasB3RptPermission)
                {
                    m_hasB3RptPermission = value;
                    RaisePropertyChanged("HasB3RptPermission");
                }
            }
        }
        

        #endregion
        #region Member Methods

        /// <summary>
        /// Exits the program.
        /// </summary>
        private void Exit()
        {
            Controller.StartExit();
        }

      

        /// <summary>
        /// Releases all resources used by MainViewModel.
        /// </summary>
        /// <param name="disposing">Whether this function is being called from 
        /// user code.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    PropertyChangedEventManager.RemoveListener(Controller, this, string.Empty);
                }
                base.Dispose(disposing);
            }
        }

        #endregion
        #region Member Properties
        /// <summary>
        /// Gets or set the view model's parent.
        /// </summary>
        private B3CenterController Controller
        {
            get;
            set;
        }

     
        /// <summary>
        /// Gets or set the sub view model: game view model.
        /// </summary>
        public IEnumerable ModuleFeatureList
        {
            get;
            private set;
        }


        /// <summary>
        /// Gets or set the sub view model: session view model.
        /// </summary>
        public SessionViewModel SessionVm
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or set the sub view model: reports view model.
        /// </summary>
        public ReportsViewModel ReportsVm { get; private set; }

       /// <summary>
       /// Gets or set the sub view model: setting view model.
       /// </summary>
        public SettingViewModel SettingVm { get; private set; }

        public static OperatorViewModel OperatorVm { get; private set; }


        #endregion
        #region Member Command Properties

        /// <summary>
        /// Gets the command that corresponds to exiting the application.
        /// </summary>
        public ICommand FileExitCommand
        {
            get;
            private set;
        }

        #endregion
    }
}
