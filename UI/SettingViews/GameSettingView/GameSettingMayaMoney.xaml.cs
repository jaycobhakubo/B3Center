using System.Collections.Generic;
using GameTech.Elite.Client.Modules.B3Center.Messages;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews
{

    /// <summary>
    /// Interaction logic for GameSettingCrazyBoutView.xaml
    /// </summary>
    public partial class GameSettingMayaMoney
    {
        #region Variables(private)

        private List<SettingMember> m_lB3Settings;

        #endregion

        #region Constructor

        public GameSettingMayaMoney ()
        {
            InitializeComponent();
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
            //100 = 10 (fastest)  //5000 = 1 (Slowest)       
            string tempCallSpeed = "";

            return tempCallSpeed;
        }

        #endregion
    }

   
}
