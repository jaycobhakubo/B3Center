#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2011 GameTech International, Inc.
#endregion

using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Business.GameModels;
using GameTech.Elite.UI;
using System.Collections;
using System.Windows;
using GameTech.Elite.Client.Modules.B3Center.Model;

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
            if (controller == null)
                throw new ArgumentNullException("controller");
            Controller = controller;
            ModuleFeatureList = controller.ModuleFeatureList;

            SessionVm = SessionViewModel.Instance;
            SessionVm.Initialize(controller.B3Controller); 
            HideAllBtnViewModel();

            //No need to initialize if staff dont have permission.
            foreach (int moduleFeatureID in controller.ModuleFeatureList)
            {
                switch (moduleFeatureID)
                {
                    case 43://Reports
                            ReportsVm = ReportsViewModel.Instance;
                            ReportsVm.Initialize(controller.B3Controller);
                            IsReportVisible = Visibility.Visible;
                    break;

                    case 44://Settings
                        SettingVm = SettingViewModel.Instance;
                        SettingVm.Initialize(controller.B3Controller);
                        IsSettingVisible = Visibility.Visible;
                     break;
                }
            }

            var testxx = controller.B3Controller.Settings.B3IconColor_;
            OperatorVm = new OperatorViewModel(controller.B3Controller.Operators, controller.B3Controller.Settings.B3IconColor_); 
            FileExitCommand = new RelayCommand(parameter => Exit());     
            PropertyChangedEventManager.AddListener(Controller, this, string.Empty);    
        }

        //private ObservableCollection<OperatorModel> m_operator;

        #endregion

        #region Member Methods

        /// <summary>
        /// Exits the program.
        /// </summary>
        private void Exit()
        {
            Controller.StartExit();
        }

        private void HideAllBtnViewModel()
        {
            IsReportVisible = Visibility.Collapsed;
            IsSettingVisible = Visibility.Collapsed;
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

        public Visibility IsReportVisible
        {
            get;
            set;
        }

        public Visibility IsSettingVisible
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
        /// Gets or set the sub view model: game view model.
        /// </summary>
        public GameViewModel GameVm
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

        public OperatorViewModel OperatorVm { get; private set; }


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
