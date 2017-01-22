using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameTech.Elite.Client.Modules.B3Center.Model.Setting
{
    public class GameSettings
    {
        public List<string> GamePayTable { get; set; }
        public List<string> MaxBetLevel { get; set; }
        public List<string> MaxCards { get; set; }
        public List<string> CallSpeedMin { get; set; }
        public List<string> CallSpeedMax { get; set; }
        public List<string> CallSpeed { get; set; }
        public List<string> CallSpeedBonus { get; set; }
        public string GamePlayTableSelected { get; set; }
        public string MaxBetLevelSelected { get; set; }
        public string MaxCardsSelected { get; set; }
        public string CallSpeedMinSelected { get; set; }
        public string CallSpeedMaxSelected { get; set; }
        public string CallSpeedSelected { get; set; }
        public string CallSpeedBonusSelected { get; set; }
        public bool AutoPlay { get; set; }
        public bool HideSerialNumber { get; set; }
        public bool SingleOfferBonus { get; set; }
        public string GridDenom_01 { get; set; }
        public string GridDenom_05 { get; set; }
        public string GridDenom_10{ get; set; }
        public string GridDenom_25 { get; set; }
        public string GridDenom_50 { get; set; }
        public string GridDenom_1 { get; set; }
        public string GridDenom_2 { get; set; }
        public string GridDenom_5 { get; set; }
    }
}
