using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using GameTech.Elite.Base;
using GameTech.Elite.UI;
using GameTech.Elite.Client.Modules.B3Center.Business.GameModels;
using GameTech.Elite.Client.Modules.B3Center.Properties;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{
    /// <summary>
    /// Represents the view model of one game
    /// </summary>
    internal class GameViewModel : ViewModelBase
    {
        #region Member Variables
        private ObservableCollection<GameModel> m_games;
        private GameModel m_selectedGame;
        private GameModel m_editGame;
        //private string m_errorInfo;
        #endregion

        #region Constructors


        /// <summary>
        /// Initialize a game view model
        /// </summar>
        public GameViewModel(ObservableCollection<GameModel> games)
        {
            // create and set the sub view models
            if (games == null)
                throw new ArgumentNullException("games");
            if (games.Count <= 0)
                throw new ArgumentException("games count");

            m_games = games;
            // select the first game
            SelectedGame = m_games.First<GameModel>();

            //SaveCommand = new RelayCommand((parameter) => Save(), (parameter) => !IsBusy && !HasError && IsDataModified);
            //CancelCommand = new RelayCommand((parameter) => Cancel(), (parameter) => !IsBusy && IsDataModified);
            //DeactivateCommand = new RelayCommand((parameter) => Deactivate(), (parameter) => !IsBusy);
            //ActivateCommand = new RelayCommand((parameter) => Activate());

        }
        #endregion

        #region Member Methods

        /// <summary>
        /// Saves the game settings changes
        /// </summary>
        public void Save()
        {
        }

        /// <summary>
        /// Cancels the game settings changes
        /// </summary>
        public void Cancel()
        {
        }

        /// <summary>
        /// Deativates the current game
        /// </summary>
        public void Deactivate()
        {
        }

        /// <summary>
        /// Activates the current game
        /// </summary>
        public void Activate()
        {
        }

        #endregion

        #region Member Properties
        /// <summary>
        /// Gets or sets the ability to show inactive games
        /// </summary>
        public bool ShowInactive
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the list of game names
        /// </summary>
        public ObservableCollection<GameModel> Game
        {
            get
            {
                return m_games;
            }
        }

        /// <summary>
        /// Gets or sets the selected the selected game
        /// </summary>
        public GameModel SelectedGame
        {
            get
            {
                return m_selectedGame;
            }
            set
            {
                try
                {
                    m_selectedGame = value;
                    // Get a deep copy of the current game for editing
                    m_editGame = (GameModel)m_selectedGame.Clone();
                    GameSettingsVM = new GameSettingsViewModel(m_editGame);
                    GameBonusSettingsVM = new GameBonusSettingsViewModel(m_editGame);
                    GameOptionsVM = new GameOptionsViewModel(m_editGame);
                    GameDenominationsVM = new GameDenominationsViewModel(m_editGame);
                    GamePayTableVM = new GamePayTableViewModel(m_editGame);
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// Gets or set the game settings view model
        /// </summary>
        public GameSettingsViewModel GameSettingsVM
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or set the game bonus settings view model
        /// </summary>
        public GameBonusSettingsViewModel GameBonusSettingsVM
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or set the game options view model
        /// </summary>
        public GameOptionsViewModel GameOptionsVM
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or set the game denominations view model
        /// </summary>
        public GameDenominationsViewModel GameDenominationsVM
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or set the game paytable view model
        /// </summary>
        public GamePayTableViewModel GamePayTableVM
        {
            get;
            private set;
        }

        public bool HasError
        {
            get
            {
                bool hasError = false;

                if (GameOptionsVM != null && GameOptionsVM.HasError)
                    hasError = true;

                //if (GameSettingsVM != null && GameSettingsVM.HasError)
                //    hasError = true;
                //else if (GameBonusSettingsVM != null && GameBonusSettingsVM.HasError)
                //    hasError = true;
                //else if (GameOptionsVM != null && GameOptionsVM.HasError)
                //    hasError = true;
                //else if (GameDenominationsVM != null && GameDenominationsVM.HasError)
                //    hasError = true;
                //else if (GamePayTableVM != null && GamePayTableVM.HasError)
                //    hasError = true;

                return hasError;
            }
        }

        #endregion

        #region Member Commands

        public ICommand SaveCommand
        {
            get;
            private set;
        }

        public ICommand CancelCommand
        {
            get;
            private set;
        }

        public ICommand DeactivateCommand
        {
            get;
            private set;
        }

        public ICommand ActivateCommand
        {
            get;
            private set;
        }

        #endregion

        //public string Error
        //{
        //    get
        //    {
        //        foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(this))
        //        {
        //            string propertyError = this[prop.Name];

        //            if (!string.IsNullOrEmpty(propertyError))
        //                return propertyError;
        //        }

        //        return null;
        //    }
        //}

        //public string this[string propertyName]
        //{
        //    get
        //    {
        //        string error = null;
        //        if (propertyName == "
        //    }
        //}
    }

    internal class GameSettingsViewModel : ViewModelBase
    {
        #region Member Variables
        IGameSettingsModel m_game;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the GameSetting class.
        /// </summary>
        public GameSettingsViewModel(GameModel game)
        {
            try
            {
                m_game = (IGameSettingsModel)game;
            }
            catch (InvalidCastException)
            {
                // not the correct type
            }

        }
        #endregion

        #region Member Properties
        /// <summary>
        /// Gets the max cards
        /// </summar>
        public ObservableCollection<uint> MaxCards
        {
            get
            {
                if (m_game != null)
                    return m_game.MaxCards;
                else
                    return null;
            }
        }

        /// <summary>
        /// Gets or sets the max cards selection.
        /// </summary>
        public uint SelectMaxCards
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets weather the max cards are editable
        /// </summary>
        public bool CanEditMaxCards
        {
            get
            {
                return (m_game != null && m_game.MaxCards != null && m_game.MaxCards.Count > 1) ;
            }
        }

        /// <summary>
        /// Gets the max bet level
        /// </summar>
        public ObservableCollection<uint> MaxBetLevel
        {
            get
            {
                if (m_game != null)
                    return m_game.MaxBetLevel;
                else
                    return null;
            }
        }

        /// <summary>
        /// Gets or sets the selected max bet level
        /// </summary>
        public uint SelectMaxBetLevel
        {
            get;
            set;
        }

        public bool CanEditMaxBetLevel
        {
            get
            {
                return (m_game != null && m_game.MaxBetLevel != null && m_game.MaxBetLevel.Count > 1);
            }
        }

        #endregion
    }

    internal class GameBonusSettingsViewModel : ViewModelBase 
    {
        #region Member Variables
        IGameBonusSettingsModel m_game;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the BonusGameSettings class.
        /// </summary>
        public GameBonusSettingsViewModel(GameModel game)
        {
            try
            {
                m_game = (IGameBonusSettingsModel)game;
            }
            catch (InvalidCastException)
            {
            }
        }
        #endregion

        #region Member Properties
        /// <summary>
        /// Gets or sets the weather the bonus is only a single offer
        /// </summar>
        public bool SingleOfferBonus
        {
            get;
            set;
        }

        public bool CanEdit
        {
            get
            {
                return (m_game != null);
            }
        }
        #endregion
    }

    internal class GameOptionsViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Member Variables
        private IGameOptionsModel m_game;
        private string m_statusMessage;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the GameOptions class.
        /// </summary>
        public GameOptionsViewModel(GameModel game)
        {
            try
            {
                m_game = (IGameOptionsModel)game;
            }
            catch (InvalidCastException)
            {
            }
        }
        #endregion

        #region Member Methods

        public void CheckForError()
        {
            RaisePropertyChanged("HasError");
            StatusMessage = Error;
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Gets or sets the min call speed
        /// </summar>
        public uint MinCallSpeed
        {
            get
            {
                if (m_game != null)
                    return m_game.GameSpeedMin;
                else
                    return 0;
            }
            set
            {
                if (m_game != null && m_game.GameSpeedMin != value)
                {
                    m_game.GameSpeedMin = value;
                    RaisePropertyChanged("MinCallSpeed");
                    RaisePropertyChanged("MaxCallSpeed");
                    CheckForError();
                }
            }
        }

        /// <summary>
        /// Gets or sets the max call speed
        /// </summar>
        public uint MaxCallSpeed
        {
            get
            {
                if (m_game != null)
                    return m_game.GameSpeedMax;
                else
                    return 0;
            }
            set
            {
                if (m_game != null && m_game.GameSpeedMax != value)
                {
                    m_game.GameSpeedMax = value;
                    RaisePropertyChanged("MaxCallSpeed");
                    RaisePropertyChanged("MinCallSpeed");
                    CheckForError();
                }
            }
        }

        /// <summary>
        /// Gets or sets the auto call
        /// </summar>
        public bool AutoCall
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the auto play
        /// </summar>
        public bool AutoPlay
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ability to hide the card serial number
        /// </summar>
        public bool HideCardSerialNumber
        {
            get;
            set;
        }

        public bool HasError
        {
            get
            {
                return !string.IsNullOrEmpty(Error);
            }
        }

        public string StatusMessage
        {
            get
            {
                return m_statusMessage;
            }
            private set
            {
                if (m_statusMessage != value)
                {
                    m_statusMessage = value;
                    RaisePropertyChanged("StatusMessage");
                }
            }
        }

        #endregion

        #region IDataErrorInfo Methods
        public string Error
        {
            get
            {
                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(this))
                {
                    string propertyError = this[prop.Name];

                    if (!string.IsNullOrEmpty(propertyError))
                        return propertyError;
                }

                return null;
            }
        }

        public string this[string propertyName]
        {
            get
            {
                string error = null;
                if (m_game != null)
                {
                    switch (propertyName)
                    {
                        case "MinCallSpeed":
                            if (MinCallSpeed < m_game.GameSpeedAbsoluteMin || MinCallSpeed > m_game.GameSpeedAbsoluteMax)
                                error = Resources.InvalidMinCallSpeed; // Call Speed is to low
                            else if (MinCallSpeed > MaxCallSpeed)
                                error = Resources.InvalidCallSpeedRange; // Call speed range is invalid
                            break;
                        case "MaxCallSpeed":
                            if (MaxCallSpeed > m_game.GameSpeedAbsoluteMax || MaxCallSpeed < m_game.GameSpeedAbsoluteMin)
                                error = Resources.InvalidMaxCallSpeed; // Call speed is to hight
                            else if (MaxCallSpeed < MinCallSpeed)
                                error = Resources.InvalidCallSpeedRange; // Call speed range is invalid
                            break;
                    }
                }

                return error;
            }
        }
        #endregion
    }

    internal class GameDenominationsViewModel : ViewModelBase
    {
        #region Member Variables
        IGameDenominationsModel m_game;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the GameDenominations class.
        /// </summary>
        public GameDenominationsViewModel(GameModel game)
        {
            try
            {
                m_game = (IGameDenominationsModel)game;
            }
            catch (InvalidCastException)
            {
            }
        }
        #endregion

        #region Member Properties

        /// <summary>
        /// Gets or sets the availablity of the $0.01 denom
        /// </summar>
        public bool Denom1
        {
            get
            {
                return m_game.Denomination1;
            }
            set
            {
                m_game.Denomination1 = value;
            }
        }

        /// <summary>
        /// Gets or sets the availablity of the $0.05 denom
        /// </summar>
        public bool Denom5
        {
            get
            {
                return m_game.Denomination5;
            }
            set
            {
                m_game.Denomination5 = value;
            }
        }

        /// <summary>
        /// Gets or sets the availablity of the $0.10 denom
        /// </summar>
        public bool Denom10
        {
            get
            {
                return m_game.Denomination10;
            }
            set
            {
                m_game.Denomination10 = value;
            }
        }

        /// <summary>
        /// Gets or sets the availablity of the $0.25 denom
        /// </summar>
        public bool Denom25
        {
            get
            {
                return m_game.Denomination25;
            }
            set
            {
                m_game.Denomination25 = value;
            }
        }

        /// <summary>
        /// Gets or sets the availablity of the $0.50 denom
        /// </summar>
        public bool Denom50
        {
            get
            {
                return m_game.Denomination50;
            }
            set
            {
                m_game.Denomination50 = value;
            }
        }

        /// <summary>
        /// Gets or sets the availablity of the $1.00 denom
        /// </summar>
        public bool Denom100
        {
            get
            {
                return m_game.Denomination100;
            }
            set
            {
                m_game.Denomination100 = value;
            }
        }

        /// <summary>
        /// Gets or sets the availablity of the $2.00 denom
        /// </summar>
        public bool Denom200
        {
            get
            {
                return m_game.Denomination200;
            }
            set
            {
                m_game.Denomination200 = value;
            }
        }

        /// <summary>
        /// Gets or sets the availablity of the $5.00 denom
        /// </summar>
        public bool Denom500
        {
            get
            {
                return m_game.Denomination500;
            }
            set
            {
                m_game.Denomination500 = value;
            }
        }

        #endregion
    }

    internal class GamePayTableViewModel : ViewModelBase
    {
        #region Member Variables
        IGamePayTableModel m_game;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the GamePayTable class.
        /// </summary>
        public GamePayTableViewModel(GameModel game)
        {
            try
            {
                m_game = (IGamePayTableModel)game;
            }
            catch (InvalidCastException)
            {
            }
        }
        #endregion

        #region Member Properties
        /// <summary>
        /// Gets a list of valid sources for the pay table list
        /// </summary>
        public ObservableCollection<string> PayTable
        {
            get
            {
                if (m_game != null)
                    return m_game.PayTables;
                else
                    return null;
            }
        }

        public int SelectPayTable
        {
            get;
            set;
        }

        public bool CanEditPayTable
        {
            get
            {
                return (m_game != null && m_game.PayTables != null && m_game.PayTables.Count > 1);
            }
        }

        #endregion
    }
}
