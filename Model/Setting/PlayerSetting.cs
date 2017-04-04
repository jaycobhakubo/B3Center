using GameTech.Elite.Client.Modules.B3Center.Business;

namespace GameTech.Elite.Client.Modules.B3Center.Model.Setting
{
    public class PlayerSettings
    {
        public bool PlayerCalibrateTouch { get; set; }
        public bool PresstoCollect { get; set; }
        public bool AnnounceCall { get; set; }
        public bool PlayerScreenCursor { get; set; }
        public string TimeToCollect { get; set; }
        public bool Disclaimer { get; set; }
        public bool DisclaimerTextId { get; set; }
        public string PlayerMainVolume { get; set; }
        public B3GameSetting CrazyboutGameSetting { get; set; }
        public B3GameSetting JailBreakGameSetting { get; set; }
        public B3GameSetting MayaMoneyGameSetting { get; set; }
        public B3GameSetting Spirit76GameSetting { get; set; }
        public B3GameSetting TimeBombGameSetting { get; set; }
        public B3GameSetting UKickemGameSetting { get; set; }
        public B3GameSetting WildBallGameSetting { get; set; }
        public B3GameSetting WildFireGameSetting { get; set; }
    }
}
