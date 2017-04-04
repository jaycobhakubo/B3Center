using System.Collections.Generic;
using GameTech.Elite.Client.Modules.B3Center.Messages;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews
{

    /// <summary>
    /// Interaction logic for GameSettingCrazyBoutView.xaml
    /// </summary>
    public partial class GameSettingCrazyBoutView
    {
        #region Variables(private)

        private List<SettingMember> m_lB3Settings;

        #endregion

        #region Constructor

        public GameSettingCrazyBoutView () 
        {
            InitializeComponent();
            DataContext = this;
        }

        #endregion
 
        #region Methods

        /// <summary>
        /// Compare old value to the new value.
        /// </summary>
        public List<SettingMember> ListOfSettingIdToBeUpdated(int gameId)
        {
            m_lB3Settings = new List<SettingMember>();
            return m_lB3Settings;   
        }


        public string GetCallSpeedEquivValue(int callSpeedValue)
        {
            string tempCallSpeed = "";
            return tempCallSpeed;
        }
        #endregion
    }
}
