using System;
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
       private bool m_handPayCalculateByGame;
       private bool m_handPayCalculateByPattern;
       private string m_rfRequiredForPlayTimeout;
       private bool m_singlePlayerMode;
       private bool m_multiplayerMode;
       private string m_gameThread;
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

       public bool IsHandPayCalculateByGame
       {
           get
           {
               return m_handPayCalculateByGame;
           }
           set
           {
               m_handPayCalculateByGame = value;
               RaisePropertyChanged("HandPayCalculateByGame");
           }
       }

       public bool IsHandPayCalculateByPattern
       {
           get
           {
               return m_handPayCalculateByPattern;
           }
           set
           {
               m_handPayCalculateByPattern = value;
               RaisePropertyChanged("HandPayCalculateByPattern");
           }
       }

       public string RfRequiredForPlayTimeout
       {
           get
           {
               return m_rfRequiredForPlayTimeout;
           }
           set
           {
               m_rfRequiredForPlayTimeout = value;
               RaisePropertyChanged("RfRequiredForPlayTimeout");
           }
       }

       public bool IsSinglePlayerMode
       {
           get
           {
               return m_singlePlayerMode;
           }
           set
           {
               m_singlePlayerMode = value;
               if (m_singlePlayerMode && Int32.Parse(MinPlayer) > 1)
               {
                   MinPlayer = 1.ToString();
               }
               RaisePropertyChanged("IsSinglePlayerMode");
           }
       }

       public bool IsMultiplayerMode
       {
           get
           {
               return m_multiplayerMode;
           }
           set
           {
               m_multiplayerMode = value;
               if (m_multiplayerMode && Int32.Parse(MinPlayer) <= 1)
               {
                   MinPlayer = 2.ToString();
               }
               RaisePropertyChanged("IsMultiplayerMode");
           }
       }

       public string GameThread
       {
           get
           {
               return m_gameThread;
           }
           set
           {
               m_gameThread = value;
               RaisePropertyChanged("GameThread");
           }
       }
       #endregion
   }
}
