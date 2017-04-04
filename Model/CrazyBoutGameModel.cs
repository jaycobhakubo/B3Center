using System;
using System.Collections.ObjectModel;

namespace GameTech.Elite.Client.Modules.B3Center.Business.GameModels
{
    class CrazyBoutGameModel : GameModel,
                               IGameSettingsModel,
                               IGameBonusSettingsModel,
                               IGameOptionsModel,
                               IGameDenominationsModel,
                               IGamePayTableModel
    {
        #region Constants and Data Types
        /// <summary>
        /// Max and min call speed values in miliseconds.
        /// </summary>
        private const uint ABSOLUTE_CALL_SPEED_MIN = 500;
        private const uint ABSOLUTE_CALL_SPEED_MAX = 5000;
        #endregion

        #region Member Variables

        readonly ObservableCollection<uint> m_maxCardsList;
        readonly ObservableCollection<uint> m_maxBetLevelList;
        readonly ObservableCollection<string> m_payTableList;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a crazy bout game model object
        /// </summary>
        public CrazyBoutGameModel()
        {
            m_maxCardsList = new ObservableCollection<uint> {4, 6};

            m_maxBetLevelList = new ObservableCollection<uint> {1, 2, 3, 4};

            m_payTableList = new ObservableCollection<string> {"60%", "70%", "80%"};

            GameSpeedMin = GameSpeedAbsoluteMin;
            GameSpeedMax = GameSpeedAbsoluteMax;

            m_name = "Crazy Bout Bingo";
        }

        public CrazyBoutGameModel(CrazyBoutGameModel game)
        {
            if (game == null)
                throw new ArgumentNullException("game");

            m_maxCardsList = new ObservableCollection<uint>(game.m_maxCardsList);
            m_maxBetLevelList = new ObservableCollection<uint>(game.m_maxBetLevelList);

            GameSpeedMin = game.GameSpeedMin;
            GameSpeedMax = game.GameSpeedMax;

            AutoCall = game.AutoCall;
            AutoPlay = game.AutoPlay;
            HideCardSerialNumber = game.HideCardSerialNumber;

            Denomination1   = game.Denomination1;
            Denomination5   = game.Denomination5;
            Denomination10  = game.Denomination10;
            Denomination25  = game.Denomination25;
            Denomination50  = game.Denomination50;
            Denomination100 = game.Denomination100;
            Denomination200 = game.Denomination200;
            Denomination500 = game.Denomination500;

            SingleOffer = game.SingleOffer;

            m_payTableList = new ObservableCollection<string>(game.m_payTableList);
        }

        #endregion

        #region Member Methods
        /// <summary>
        /// Creates an new copy of the object
        /// </summary>
        /// <returns>The newly copied object</returns>
        public override object Clone()
        {
            CrazyBoutGameModel clone = new CrazyBoutGameModel(this);
            return clone;
        }
        #endregion

        #region Member Properties
        public ObservableCollection<uint> MaxCards
        {
            get
            {
                return m_maxCardsList;
            }
        }

        public ObservableCollection<uint> MaxBetLevel
        {
            get
            {
                return m_maxBetLevelList;
            }
        }

        public bool SingleOffer { get; set; }

        public bool Denomination1 { get; set; }
        public bool Denomination5 { get; set; }
        public bool Denomination10 { get; set; }
        public bool Denomination25 { get; set; }
        public bool Denomination50 { get; set; }
        public bool Denomination100 { get; set; }
        public bool Denomination200 { get; set; }
        public bool Denomination500 { get; set; }

        public uint GameSpeedAbsoluteMin
        {
            get
            {
                return ABSOLUTE_CALL_SPEED_MIN;
            }
        }

        public uint GameSpeedAbsoluteMax
        {
            get
            {
                return ABSOLUTE_CALL_SPEED_MAX;
            }
        }

        public uint GameSpeedMin { get; set; }
        public uint GameSpeedMax { get; set; }

        public bool AutoCall { get; set; }
        public bool AutoPlay { get; set; }
        public bool HideCardSerialNumber { get; set; }

        public ObservableCollection<string> PayTables
        {
            get
            {
                return m_payTableList;
            }
        }

        #endregion
    }
}
