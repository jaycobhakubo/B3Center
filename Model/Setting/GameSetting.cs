using System.Collections.Generic;
using System.Collections.ObjectModel;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;

namespace GameTech.Elite.Client.Modules.B3Center.Model.Setting
{
    public class GameSetting : Notifier
    {
        #region Fields

        private string m_denom1;
        private string m_denom5;
        private string m_denom10;
        private string m_denom25;
        private string m_denom50;
        private string m_denom100;
        private string m_denom200;
        private string m_denom500;
        private string m_maxBetLevel;
        private string m_maxCards;
        private string m_callSpeed;
        private bool m_autoCall;
        private bool m_autoPlay;
        private bool m_hideSerialNumber;
        private bool m_singleOfferBonus;
        //private B3MathGamePay m_mathPayTable;
        private string m_callSpeedMin;
        private string m_callSpeedBonus;
        //private ObservableCollection<B3MathGamePay> m_gamePaytableList;
        private B3IsGameEnabledSetting m_enableGameSetting;

        #endregion

        #region Properties

        public B3GameType GameType { get; set; }

        public string Denom1
        {
            get
            {
                return m_denom1;
            }
            set
            {
                m_denom1 = value;
                RaisePropertyChanged("Denom1");
            }
        }

        public string Denom5
        {
            get
            {
                return m_denom5;
            }
            set
            {
                m_denom5 = value;
                RaisePropertyChanged("Denom5");
            }
        }

        public string Denom10
        {
            get
            {
                return m_denom10;
            }
            set
            {
                m_denom10 = value;
                RaisePropertyChanged("Denom10");
            }
        }

        public string Denom25
        {
            get
            {
                return m_denom25;
            }
            set
            {
                m_denom25 = value;
                RaisePropertyChanged("Denom25");
            }
        }

        public string Denom50
        {
            get
            {
                return m_denom50;
            }
            set
            {
                m_denom50 = value;
                RaisePropertyChanged("Denom50");
            }
        }

        public string Denom100
        {
            get
            {
                return m_denom100;
            }
            set
            {
                m_denom100 = value;
                RaisePropertyChanged("Denom100");
            }
        }

        public string Denom200
        {
            get
            {
                return m_denom200;
            }
            set
            {
                m_denom200 = value;
                RaisePropertyChanged("Denom200");
            }
        }

        public string Denom500
        {
            get
            {
                return m_denom500;
            }
            set
            {
                m_denom500 = value;
                RaisePropertyChanged("Denom500");
            }
        }

        public string MaxBetLevel
        {
            get
            {
                return m_maxBetLevel;
            }
            set
            {
                m_maxBetLevel = value;
                RaisePropertyChanged("MaxBetLevel");
            }
        }

        public string MaxCards
        {
            get
            {
                return m_maxCards;
            }
            set
            {
                m_maxCards = value;
                RaisePropertyChanged("MaxCards");
            }
        }

        public string CallSpeed
        {
            get
            {
                return m_callSpeed;
            }
            set
            {
                m_callSpeed = value;
                RaisePropertyChanged("CallSpeed");
            }
        }

        public bool AutoCall
        {
            get
            {
                return m_autoCall;
            }
            set
            {
                m_autoCall = value;
                RaisePropertyChanged("AutoCall");
            }
        }

        public bool AutoPlay
        {
            get
            {
                return m_autoPlay;
            }
            set
            {
                m_autoPlay = value;
                RaisePropertyChanged("AutoPlay");
            }
        }

        public bool HideSerialNumber
        {
            get
            {
                return m_hideSerialNumber;
            }
            set
            {
                m_hideSerialNumber = value;
                RaisePropertyChanged("HideSerialNumber");
            }
        }

        public bool SingleOfferBonus
        {
            get
            {
                return m_singleOfferBonus;
            }
            set
            {
                m_singleOfferBonus = value;
                RaisePropertyChanged("SingleOfferBonus");
            }
        }

        //public B3MathGamePay MathPayTable 
        //{
        //    get
        //    {
        //        return m_mathPayTable;
        //    }
        //    set
        //    {
        //        m_mathPayTable = value;
        //        RaisePropertyChanged("MathPayTable");
        //    }
        //}

        public string CallSpeedMin
        {
            get
            {
                return m_callSpeedMin;
            }
            set
            {
                m_callSpeedMin = value;
                RaisePropertyChanged("CallSpeedMin");
            }
        }

        public string CallSpeedBonus
        {
            get
            {
                return m_callSpeedBonus;
            }
            set
            {
                m_callSpeedBonus = value;
                RaisePropertyChanged("CallSpeedBonus");
            }
        }

        //public ObservableCollection<B3MathGamePay> LGamePayTable
        //{
        //    get
        //    {
        //        return m_gamePaytableList;
        //    }
        //    set
        //    {
        //        m_gamePaytableList = value;
        //        RaisePropertyChanged("LGamePayTable");
        //    }
        //}

        public B3IsGameEnabledSetting EnableGameSetting
        {
            get
            {
                return m_enableGameSetting;
            }
            set
            {
                m_enableGameSetting = value;
                RaisePropertyChanged("EnableGameSetting");
            }
        }

        #endregion
    }
}
