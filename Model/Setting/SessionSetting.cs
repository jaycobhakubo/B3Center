using GameTech.Elite.Base;

namespace GameTech.Elite.Client.Modules.B3Center.Model.Setting
{
    public class SessionSetting : Notifier
    {
        #region Fields

        private string m_payoutLimit;
        private string m_jackpotLimit;
        private bool m_enforceMix;
        #endregion

        #region Properties

        public string PayoutLimit
        {
            get
            {
                return m_payoutLimit;
            }
            set
            {
                m_payoutLimit = value;
                RaisePropertyChanged("PayoutLimit");
            }
        }

        public string JackpotLimit
        {
            get
            {
                return m_jackpotLimit;
            }
            set
            {
                m_jackpotLimit = value;
                RaisePropertyChanged("JackpotLimit");
            }
        }

        public bool EnforceMix
        {
            get
            {
                return m_enforceMix;
            }
            set
            {
                m_enforceMix = value;
                RaisePropertyChanged("EnforceMix");
            }
        }

        #endregion
        
    }
}
