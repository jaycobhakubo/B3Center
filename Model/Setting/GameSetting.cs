using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTech.Elite.Client.Modules.B3Center.Business;

namespace GameTech.Elite.Client.Modules.B3Center.Model.Setting
{
    public class GameSetting
    {
        public string Denom1 { get; set; }
        public string Denom5 { get; set; }
        public string Denom10 { get; set; }
        public string Denom25 { get; set; }
        public string Denom50 { get; set; }
        public string Denom100 { get; set; }
        public string Denom200 { get; set; }
        public string Denom500 { get; set; }
        public string MaxBetLevel { get; set; }
        public string MaxCards { get; set; }
        public string CallSpeed { get; set; }
        public bool AutoCall { get; set; }
        public bool AutoPlay { get; set; }
        public bool HideSerialNumber { get; set; }
        public bool SingleOfferBonus { get; set; }
        public string MathPayTableSetting { get; set; }
        public string CallSpeedMin { get; set; }
        public string CallSpeedBonus { get; set; }


        public List<B3MathGamePay> LGamePayTable { get; set; }
        public List<string> LMaxBetLevel { get; set; }
        public List<string> LMaxCards { get; set; }
        public List<string> LCallSpeedMin { get; set; }
        public List<string> LCallSpeedMax { get; set; }
        public List<string> LCallSpeed { get; set; }
        public List<string> LCallSpeedBonus { get; set; }

    //public string GamePlayTableSelected { get; set; }
    //public string MaxBetLevelSelected { get; set; }
    //public string MaxCardsSelected { get; set; }
    //public string CallSpeedMinSelected { get; set; }
    //public string CallSpeedMaxSelected { get; set; }
    //public string CallSpeedSelected { get; set; }
    //public string CallSpeedBonusSelected { get; set; }
    //public bool AutoPlay { get; set; }
    //public bool HideSerialNumber { get; set; }
    //public bool SingleOfferBonus { get; set; }
    //public string GridDenom_01 { get; set; }
    //public string GridDenom_05 { get; set; } 
    //public string GridDenom_10{ get; set; }
    //public string GridDenom_25 { get; set; }
    //public string GridDenom_50 { get; set; }
    //public string GridDenom_1 { get; set; }
    //public string GridDenom_2 { get; set; }
    //public string GridDenom_5 { get; set; }
}
}
