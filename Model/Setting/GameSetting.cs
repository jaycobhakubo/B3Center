using System.Collections.Generic;
using GameTech.Elite.Client.Modules.B3Center.Business;

namespace GameTech.Elite.Client.Modules.B3Center.Model.Setting
{
    public class GameSetting
    {
        public int GameId { get; set; }
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
        public int SelectedMathPayTableSettingInt { get; set; }
        public string CallSpeedMin { get; set; }
        public string CallSpeedBonus { get; set; }
        public List<B3MathGamePay> LGamePayTable { get; set; }
        public List<string> LMaxBetLevel { get; set; }
        public List<string> LMaxCards { get; set; }
        public List<string> LCallSpeedMin { get; set; }
        public List<string> LCallSpeedMax { get; set; }
        public List<string> LCallSpeed { get; set; }
        public List<string> LCallSpeedBonus { get; set; }
        public B3GameSetting IsEnableGame { get; set; }
    }
}
