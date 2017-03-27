#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2011 GameTech International, Inc.
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GameTech.Elite.Base;

namespace GameTech.Elite.Client.Modules.B3Center.Business.GameModels
{
    internal abstract class GameModel : Notifier, ICloneable
    {
        #region Member Variables
        protected string m_name;
        #endregion

        #region Constructors
        public GameModel()
        {
        }
        #endregion

        #region Member Methods
        public abstract object Clone();
        #endregion

        #region Member Properties
        /// <summary>
        /// Gets or set the name of the game
        /// </summary>
        public override string ToString()
        {
            return m_name;
        }

        #endregion
    }

    internal interface IGameSettingsModel
    {
        ObservableCollection<uint> MaxCards { get; }
        ObservableCollection<uint> MaxBetLevel { get; }
    }

    internal interface IGameBonusSettingsModel
    {
        bool SingleOffer { get; set; }
    }

    internal interface IGameOptionsModel
    {
        uint GameSpeedAbsoluteMin { get; }
        uint GameSpeedAbsoluteMax { get; }

        uint GameSpeedMin { get; set; }
        uint GameSpeedMax { get; set; }

        bool AutoCall { get; set; }
        bool AutoPlay { get; set; }
        bool HideCardSerialNumber { get; set; }
    }

    internal interface IGameDenominationsModel
    {
        bool Denomination1 { get; set; }
        bool Denomination5 { get; set; }
        bool Denomination10 { get; set; }
        bool Denomination25 { get; set; }
        bool Denomination50 { get; set; }
        bool Denomination100 { get; set; }
        bool Denomination200 { get; set; }
        bool Denomination500 { get; set; }
    }

    internal interface IGamePayTableModel
    {
        ObservableCollection<string> PayTables { get; }
    }
}
