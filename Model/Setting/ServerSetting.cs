using GameTech.Elite.Base;

namespace GameTech.Elite.Client.Modules.B3Center.Model.Setting
{
   public class ServerSetting : Notifier
   {
       #region Fields

       private string m_minPlayer;
       private string m_gameStartDelay;
       private string m_consolationPrize;
       private string m_gameRecallPassword;
       private string m_waitCountDown;
       #endregion

       #region Properties
       public string MinPlayer
       {
           get
           {
               return m_minPlayer;
           }
           set
           {
               m_minPlayer = value;
               RaisePropertyChanged("MinPlayer");
           }
       }

       public string GameStartDelay
       {
           get
           {
               return m_gameStartDelay;
           }
           set
           {
               m_gameStartDelay = value;
               RaisePropertyChanged("GameStartDelay");
           }
       }

       public string ConsolationPrize
       {
           get
           {
               return m_consolationPrize;
           }
           set
           {
               m_consolationPrize = value;
               RaisePropertyChanged("ConsolationPrize");
           }
       }

       public string GameRecallPassword
       {
           get
           {
               return m_gameRecallPassword;
           }
           set
           {
               m_gameRecallPassword = value;
               RaisePropertyChanged("GameRecallPassword");
           }
       }

       public string WaitCountDown
       {
           get
           {
               return m_waitCountDown;
           }
           set
           {
               m_waitCountDown = value;
               RaisePropertyChanged("WaitCountDown");
           }
       }
       #endregion
   }
}
