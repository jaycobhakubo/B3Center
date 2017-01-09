using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading.Tasks;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Messages;
using GameTech.Elite.Client.Modules.B3Center.Properties;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;



namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews
{
   
    public partial class GameSettingView : UserControl
    {
        #region local variables

        //private readonly List<ToggleButton> m_menuItems;
        //private readonly GameSettingCrazyBoutView m_gsCrazyBout;
        //private readonly GameSettingJailBreak m_gsJailBreak;
        //private readonly GameSettingJailBreak m_gsMayaMoney;
        //private readonly GameSettingJailBreak m_gsSpirit76;
        //private readonly GameSettingJailBreak m_gsTimeBomb;
        //private readonly GameSettingUkickEmView m_gsUkickEm;
        //private readonly SaveCancelCrtl m_saveCancelCtrl;
        //private readonly GameSettingCrazyBoutView m_gsWildBall;
        //private readonly GameSettingWildBallProgView m_gsWilfFireProg;
        //UserControl view;
        //private Button m_btnSave;
        //private Button m_btnCancel;
        //private string m_selectedGame;
        //private bool m_isGameEnable = false;
        //private B3Setting m_B3Setting;
        //private int m_gameID;
        //private ContentPresenter GameSettingTransitionControl = new ContentPresenter();
        //private ToggleButton tglBtnEnable = new ToggleButton();
        //private Label lblSavedNotification = new Label();

        //private Label lblEnable= new Label();


        #endregion

        #region Properties

        //public B3Setting B3_Setting
        //{
        //    get { return m_B3Setting; }
        //    set { m_B3Setting = value; }
        //}

        //public Button btnSave
        //{
        //    get { return m_btnSave; }
        //    set { m_btnSave = value; }
        // }

        #endregion

        #region Constructor

        public GameSettingView()
        {
            InitializeComponent();  
        }

        //public GameSettingView(B3Setting b3setting)
        //{
            //InitializeComponent();        
            //m_gsCrazyBout = new GameSettingCrazyBoutView(b3setting.B3SettingGlobal_.Where(l => l.B3GameID == 1).ToList(), b3setting.ListB3mathGamePlay_.Where(l => l.GameID == 1).ToList());
            //m_gsJailBreak = new GameSettingJailBreak(b3setting.B3SettingGlobal_.Where(l => l.B3GameID == 2).ToList(), b3setting.ListB3mathGamePlay_.Where(l => l.GameID == 2).ToList());
            //m_gsMayaMoney = new GameSettingJailBreak(b3setting.B3SettingGlobal_.Where(l => l.B3GameID == 3).ToList(), b3setting.ListB3mathGamePlay_.Where(l => l.GameID == 3).ToList());
            //m_gsSpirit76 = new GameSettingJailBreak(b3setting.B3SettingGlobal_.Where(l => l.B3GameID == 4).ToList(), b3setting.ListB3mathGamePlay_.Where(l => l.GameID == 4).ToList());
            //m_gsTimeBomb = new GameSettingJailBreak(b3setting.B3SettingGlobal_.Where(l => l.B3GameID == 5).ToList(), b3setting.ListB3mathGamePlay_.Where(l => l.GameID == 5).ToList());
            //m_gsUkickEm = new GameSettingUkickEmView(b3setting.B3SettingGlobal_.Where(l => l.B3GameID == 6).ToList(), b3setting.ListB3mathGamePlay_.Where(l => l.GameID == 6).ToList());
            //m_gsWildBall = new GameSettingCrazyBoutView(b3setting.B3SettingGlobal_.Where(l => l.B3GameID == 7).ToList(), b3setting.ListB3mathGamePlay_.Where(l => l.GameID == 7).ToList());
            //m_gsWilfFireProg = new GameSettingWildBallProgView(b3setting.B3SettingGlobal_.Where(l => l.B3GameID == 8).ToList(), b3setting.ListB3mathGamePlay_.Where(l => l.GameID == 8).ToList());
            //m_menuItems = new List<ToggleButton>();
            //m_B3Setting = b3setting;
            //LoadGameAllowed();
            //m_saveCancelCtrl = new SaveCancelCrtl();
            //m_btnSave = m_saveCancelCtrl.btnSave;
            //m_btnSave.Click += new RoutedEventHandler (m_btnSave_Click);
            //m_btnCancel = m_saveCancelCtrl.btnCancel;
            //m_btnCancel.Click += new RoutedEventHandler(m_btnCancel_Click);
            //SaveCancelTransition.Content = (UserControl)m_saveCancelCtrl;
            //lblSavedNotification.Visibility = Visibility.Hidden;
        //}
     
        #endregion

        #region Methods

        public void LoadGameAllowed()
        {
            //foreach (B3GameSetting b3GameSetting in m_B3Setting.B3GameSetting_)
            //{
            //    if (b3GameSetting.IsAllowed == true)
            //    {
            //        ToggleButton TempTglBtnName = new ToggleButton();
            //        //TempTglBtnName.SetResourceReference(Control.StyleProperty, "ToggleButtonSLightBlueStyle");
            //        TempTglBtnName.SetResourceReference(Control.StyleProperty, "ToggleButtonDarkBlueStyle");
            //        TempTglBtnName.Click += new RoutedEventHandler(MenuToggleButton_Changed);

            //        switch (b3GameSetting.GameId)
            //        {
            //            case 1://CRAZYBOUT
            //                {
            //                    TempTglBtnName.Content = "Crazy Bout";
            //                    TempTglBtnName.Name = "GameSettingToggleButton_CrazyBt";
            //                    break;
            //                }
            //            case 2://JAILBREAK
            //                {
            //                    TempTglBtnName.Content = "JailBreak";
            //                    TempTglBtnName.Name = "GameSettingToggleButton_JailBreak";
            //                    break;
            //                }
            //            case 3:
            //                {
            //                    TempTglBtnName.Content = "Maya Money";
            //                    TempTglBtnName.Name = "GameSettingToggleButton_MayaMoney";
            //                    break;
            //                }
            //            case 4:
            //                {
            //                    TempTglBtnName.Content = "76 Bingo";
            //                    TempTglBtnName.Name = "GameSettingToggleButton_Spirit";
            //                    break;
            //                }
            //            case 5:
            //                {
            //                    TempTglBtnName.Content = "Time Bomb";
            //                    TempTglBtnName.Name = "GameSettingToggleButton_TimeBomb";
            //                    break;
            //                }
            //            case 6:
            //                {
            //                    TempTglBtnName.Content = "U Kick Em";
            //                    TempTglBtnName.Name = "GameSettingToggleButton_UkickEm";
            //                    break;
            //                }
            //            case 7:
            //                {
            //                    TempTglBtnName.Content = "WildFire w/Bonus";
            //                    TempTglBtnName.Name = "GameSettingToggleButton_WildBall";
            //                    break;
            //                }
            //            case 8:
            //                {
            //                    TempTglBtnName.Content = "WildFire";//This is the progressive wildfire.
            //                    TempTglBtnName.Name = "GameSettingToggleButton_WildFireProg";
            //                    break;
            //                }
            //        }
            //        m_menuItems.Add(TempTglBtnName);
            //        //stackpanel1.Children.Add(TempTglBtnName);
            //    }
            //}
        }


        public void ClearSelected()
        {
            //foreach (var menuItem in m_menuItems)
            //{
            //    if (menuItem.IsChecked != null && (bool)menuItem.IsChecked)
            //    {
            //        menuItem.IsChecked = false;
            //        //GameSettingTransitionControl.Content = null;
            //        //tglBtnEnable.Visibility = Visibility.Hidden;
            //        //lblEnable.Visibility = Visibility.Hidden;
            //        //SaveCancelTransition.Visibility = Visibility.Hidden;
            //        m_selectedGame = "";
            //        ClearSavedNotification();
            //    }
            //}
            //lblB3GameName.Content = string.Empty;
        }

        public void ClearSavedNotification()
        {
            //if (lblSavedNotification.Visibility != Visibility.Hidden) lblSavedNotification.Visibility = Visibility.Hidden;
        }

        private void Execute_SetB3SettingsMessage(List<SettingMember> lSettingID)
        {
            //Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            //SetB3SettingsMessage msg = new SetB3SettingsMessage(lSettingID);
            //msg.Send();

            //if (msg.ReturnCode != ServerReturnCode.Success)
            //    throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, "B3 Set Game Setting Failed.", ServerErrorTranslator.GetReturnCodeMessage(msg.ReturnCode)));
            //Mouse.OverrideCursor = null;
        }


        public string GetCallSpeedEquivValue(int callSpeedValue)
        {
            //100 = 10 (fastest)  //5000 = 1 (Slowest)       
            string tempCallSpeed = "";
            //if (callSpeedValue == 100) { tempCallSpeed = "10"; }
            //else if (callSpeedValue > 100 && callSpeedValue <= 590) { tempCallSpeed = "9"; }
            //else if (callSpeedValue > 590 && callSpeedValue <= 1080) { tempCallSpeed = "8"; }
            //else if (callSpeedValue > 1080 && callSpeedValue <= 1570) { tempCallSpeed = "7"; }
            //else if (callSpeedValue > 1570 && callSpeedValue <= 2060) { tempCallSpeed = "6"; }
            //else if (callSpeedValue > 2060 && callSpeedValue <= 2550) { tempCallSpeed = "5"; }
            //else if (callSpeedValue > 2550 && callSpeedValue <= 3040) { tempCallSpeed = "4"; }
            //else if (callSpeedValue > 3040 && callSpeedValue <= 3530) { tempCallSpeed = "3"; }
            //else if (callSpeedValue > 3530 && callSpeedValue <= 4020) { tempCallSpeed = "2"; }
            //else if (callSpeedValue == 5000) { tempCallSpeed = "1"; }
            return tempCallSpeed;
        }

        //private string GetB3GameNameDesc(int b3GameID)
        //{
        //    B3GameNameDesc b3GameName = new B3GameNameDesc();
        //    string GameName = b3GameName[b3GameID];
        //    return GameName;
        //}

        #endregion

        #region Event


        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_btnSave_Click(object sender, RoutedEventArgs e)
        {
            //bool SavedOk = false;
            //ClearSavedNotification();
            //string Description = "";

            //if (m_selectedGame != "")
            //{
           
            //    bool m_isModify_EnableDisable = false;
            //     List<SettingMember> lSettingID = new List<SettingMember>();

            //    switch (m_selectedGame)
            //    {
            //        case "CRAZYBOUT":
            //            {
            //                if (m_B3Setting.B3GameSetting_[0].IsEnabled != m_isGameEnable)
            //                {
            //                    m_isModify_EnableDisable = true;
            //                    m_B3Setting.B3GameSetting_[0].IsEnabled = m_isGameEnable;
            //                }

            //                lSettingID = m_gsCrazyBout.ListOfSettingIDToBeUpdated(1);
            //                if (lSettingID.Count != 0)
            //                {
            //                    Execute_SetB3SettingsMessage(lSettingID);                               
            //                    m_gsCrazyBout.RepopulateNewSaveData(lSettingID);
            //                    SavedOk = true;
            //                }

            //                break;
            //            }

            //        case "JAILBREAK":
            //            {

            //                if (m_B3Setting.B3GameSetting_[1].IsEnabled != m_isGameEnable)
            //                {
            //                    m_isModify_EnableDisable = true;                               
            //                    m_B3Setting.B3GameSetting_[1].IsEnabled = m_isGameEnable;
            //                }

            //                lSettingID = m_gsJailBreak.ListOfSettingIDToBeUpdated(2); //m_gsCrazyBout.ListOfSettingIDToBeUpdated();
            //                if (lSettingID.Count != 0)
            //                {

            //                    Execute_SetB3SettingsMessage(lSettingID);   
            //                     m_gsJailBreak.RepopulateNewSaveData(lSettingID);
            //                     SavedOk = true;
            //                }
            //                break;
            //            }

            //        case "MAYAMONEY":
            //            {

            //                if (m_B3Setting.B3GameSetting_[2].IsEnabled != m_isGameEnable)
            //                {
            //                    m_isModify_EnableDisable = true;                               
            //                    m_B3Setting.B3GameSetting_[2].IsEnabled = m_isGameEnable;
            //                }

            //                lSettingID =  m_gsMayaMoney.ListOfSettingIDToBeUpdated(3); 
            //                if (lSettingID.Count != 0)
            //                {

            //                    Execute_SetB3SettingsMessage(lSettingID);   
            //                    m_gsMayaMoney.RepopulateNewSaveData(lSettingID);
            //                    SavedOk = true;
            //                }
            //                break;
            //            }

            //        case "SPIRIT76":
            //            {

            //                if (m_B3Setting.B3GameSetting_[3].IsEnabled != m_isGameEnable)
            //                {
            //                    m_isModify_EnableDisable = true;                             
            //                    m_B3Setting.B3GameSetting_[3].IsEnabled = m_isGameEnable;
            //                }

            //                lSettingID = m_gsSpirit76.ListOfSettingIDToBeUpdated(4);
            //                if (lSettingID.Count != 0)
            //                {
            //                    Execute_SetB3SettingsMessage(lSettingID);   
            //                    m_gsSpirit76.RepopulateNewSaveData(lSettingID);
            //                    SavedOk = true;
            //                }
            //                break;
            //            }

            //        case "TIMEBOMB":
            //            {
            //                if (m_B3Setting.B3GameSetting_[4].IsEnabled != m_isGameEnable)
            //                {
            //                    m_isModify_EnableDisable = true;                             
            //                    m_B3Setting.B3GameSetting_[4].IsEnabled = m_isGameEnable;
            //                }

            //                lSettingID = m_gsTimeBomb.ListOfSettingIDToBeUpdated(5);
            //                if (lSettingID.Count != 0)
            //                {
            //                    Execute_SetB3SettingsMessage(lSettingID);   
            //                    m_gsTimeBomb.RepopulateNewSaveData(lSettingID);
            //                    SavedOk = true;
            //                }
            //                break;
            //            }

            //        case "UKICKEM":
            //            {
            //                if (m_B3Setting.B3GameSetting_[5].IsEnabled != m_isGameEnable)
            //                {
            //                    m_isModify_EnableDisable = true;                              
            //                    m_B3Setting.B3GameSetting_[5].IsEnabled = m_isGameEnable;
            //                }

            //                lSettingID =  m_gsUkickEm.ListOfSettingIDToBeUpdated();
            //                if (lSettingID.Count != 0)
            //                {
            //                    Execute_SetB3SettingsMessage(lSettingID);   
            //                    m_gsUkickEm.RepopulateNewSaveData(lSettingID);
            //                    SavedOk = true;
            //                }

            //                break;
            //            }

            //        case "WILDBALL":
            //            {
            //                if (m_B3Setting.B3GameSetting_[6].IsEnabled != m_isGameEnable)
            //                {
            //                    m_isModify_EnableDisable = true;                             
            //                    m_B3Setting.B3GameSetting_[6].IsEnabled = m_isGameEnable;
            //                }

            //                lSettingID =  m_gsWildBall.ListOfSettingIDToBeUpdated(7);
            //                if (lSettingID.Count != 0)
            //                {
            //                    Execute_SetB3SettingsMessage(lSettingID);   
            //                    m_gsWildBall.RepopulateNewSaveData(lSettingID);
            //                    SavedOk = true;
            //                }

            //                break;
            //            }
            //        case "WILDFIREPROG":
            //            {
            //                if (m_B3Setting.B3GameSetting_[7].IsEnabled != m_isGameEnable)
            //                {
            //                    m_isModify_EnableDisable = true;                               
            //                    m_B3Setting.B3GameSetting_[7].IsEnabled = m_isGameEnable;
            //                }

            //                lSettingID =  m_gsWilfFireProg.ListOfSettingIDToBeUpdated();
            //                if (lSettingID.Count != 0)
            //                {

            //                    Execute_SetB3SettingsMessage(lSettingID);   
            //                    m_gsWilfFireProg.RepopulateNewSaveData(lSettingID);
            //                    SavedOk = true;
            //                }

            //                break;
            //            }
            //    }


            //    if (m_isModify_EnableDisable == true)
            //    {
            //        Messages.SetGameEnableSQL setGameEnable;
            //        setGameEnable = new Messages.SetGameEnableSQL(m_gameID, m_isGameEnable);
            //        try 
            //        {
            //            setGameEnable.Send();
            //            if (setGameEnable.ReturnCode == ServerReturnCode.Success)
            //            {                                     
            //                SavedOk = true;
            //            }
            //            else
            //            {
            //                throw new Exception(ServerErrorTranslator.GetReturnCodeMessage(setGameEnable.ReturnCode));
            //            }
            //        }
            //        catch (ServerCommException ex)
            //        {
            //            throw new Exception("SetGameEnable: " + ex.Message);
            //        }                                               
            //    }

            //    if (SavedOk == true)
            //    {
            //        if (lblSavedNotification.Visibility != Visibility.Visible) lblSavedNotification.Visibility = Visibility.Visible;
            //    }


            //}
        }

        /// <summary>
        /// Handles the Changed event of the MenuToggleButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuToggleButton_Changed(object sender, RoutedEventArgs e)
        {
           // var toggleButton = sender as ToggleButton;
           // ClearSavedNotification();

           // if (toggleButton == null)
           // {
           //     return;
           // }

           //view = null;

        

           // if (toggleButton.IsChecked == false)
           // {
           //     //GameSettingTransitionControl.Content = null;
           //     //tglBtnEnable.Visibility = Visibility.Hidden;
           //     //lblEnable.Visibility = Visibility.Hidden;
           //     //SaveCancelTransition.Visibility = Visibility.Hidden;
           //     //m_selectedGame = "";
           //     //lblB3GameName.Content = string.Empty;
           //     //return;
           // }
           // else
           // {
           //     //tglBtnEnable.Visibility = Visibility.Visible;
           //     //lblEnable.Visibility = Visibility.Visible;
           //     //SaveCancelTransition.Visibility = Visibility.Visible;
           // }

           // List<SettingMember> lSettingID = new List<SettingMember>();
           // switch (toggleButton.Name)
           // {
           //     case "GameSettingToggleButton_CrazyBt":
           //         {
           //             m_selectedGame = "CRAZYBOUT";
           //             m_gameID = 0;     
           //             view = m_gsCrazyBout;

           //             if (m_B3Setting.B3GameSetting_[0].IsEnabled == true)
           //             {
           //                 tglBtnEnable.IsChecked = true;
           //             }
           //             else
           //             {
           //                 tglBtnEnable.IsChecked = false;
           //             }
       
           //            lSettingID = m_gsCrazyBout.ListOfSettingIDToBeUpdated(1);
           //             if (lSettingID.Count != 0)
           //             {
           //               m_gsCrazyBout.PopulateDataIntoControls();
           //             }
           //             break;                     
           //         }

           //     case "GameSettingToggleButton_JailBreak":
           //         {
           //             m_selectedGame = "JAILBREAK";
           //             m_gameID = 1;
           //             view = m_gsJailBreak;

           //             if (m_B3Setting.B3GameSetting_[1].IsEnabled == true)
           //             {
           //                 //tglBtnEnable.IsChecked = true;
           //             }
           //             else
           //             {
           //                 //tglBtnEnable.IsChecked = false;
           //             }

           //             lSettingID = m_gsJailBreak.ListOfSettingIDToBeUpdated(2);
           //             if (lSettingID.Count != 0)
           //             {
           //                 m_gsJailBreak.PopulateDataIntoControls();
           //             }

           //             break;       

         

           //         }

           //     case "GameSettingToggleButton_MayaMoney":
           //         {
           //             m_selectedGame = "MAYAMONEY";
           //             view =m_gsMayaMoney;          
           //             m_gameID = 2;
           //             if (m_B3Setting.B3GameSetting_[2].IsEnabled == true)
           //             {
           //                 //tglBtnEnable.IsChecked = true;
           //             }
           //             else
           //             {
           //                 //tglBtnEnable.IsChecked = false;
           //             }

           //             lSettingID = m_gsMayaMoney.ListOfSettingIDToBeUpdated(3);
           //             if (lSettingID.Count != 0)
           //             {
           //                 m_gsMayaMoney.PopulateDataIntoControls();
           //             }

           //             break;     

           //         }

           //     case "GameSettingToggleButton_Spirit":
           //         {
           //             m_selectedGame = "SPIRIT76";
           //             //m_gsSpirit76.GameName = "76 Bingo";
           //             view = m_gsSpirit76;                       
           //             m_gameID = 3;
           //             if (m_B3Setting.B3GameSetting_[3].IsEnabled == true)
           //             {
           //                 //tglBtnEnable.IsChecked = true;
           //             }
           //             else
           //             {
           //                 //tglBtnEnable.IsChecked = false;
           //             }

           //             lSettingID = m_gsSpirit76.ListOfSettingIDToBeUpdated(4);
           //             if (lSettingID.Count != 0)
           //             {
           //                 m_gsSpirit76.PopulateDataIntoControls();
           //             }

           //             break;     

           //         }

           //     case "GameSettingToggleButton_TimeBomb":
           //         {
           //             m_selectedGame = "TIMEBOMB";
           //             view = m_gsTimeBomb;
           //             m_gameID = 4;
           //             if (m_B3Setting.B3GameSetting_[4].IsEnabled == true)
           //             {
           //                 //tglBtnEnable.IsChecked = true;
           //             }
           //             else
           //             {
           //                 //tglBtnEnable.IsChecked = false;
           //             }

           //             lSettingID = m_gsTimeBomb.ListOfSettingIDToBeUpdated(5);
           //             if (lSettingID.Count != 0)
           //             {
           //                 m_gsTimeBomb.PopulateDataIntoControls();
           //             }

           //             break;     

           //         }

           //     case "GameSettingToggleButton_UkickEm":
           //         {
           //             m_selectedGame = "UKICKEM";
           //             view = m_gsUkickEm;
           //             m_gameID = 5;
           //             if (m_B3Setting.B3GameSetting_[5].IsEnabled == true)
           //             {
           //                 //tglBtnEnable.IsChecked = true;
           //             }
           //             else
           //             {
           //                 //tglBtnEnable.IsChecked = false;
           //             }

           //             lSettingID = m_gsUkickEm.ListOfSettingIDToBeUpdated();
           //             if (lSettingID.Count != 0)
           //             {
           //                 m_gsUkickEm.PopulateDataIntoControls();
           //             }

           //             break;     

           //         }

           //     case "GameSettingToggleButton_WildBall":
           //         {
           //             m_selectedGame = "WILDBALL";
           //             view = m_gsWildBall;
           //             m_gameID = 6;
           //             if (m_B3Setting.B3GameSetting_[6].IsEnabled == true)
           //             {
           //                 //tglBtnEnable.IsChecked = true;
           //             }
           //             else
           //             {
           //                 //tglBtnEnable.IsChecked = false;
           //             }

           //             lSettingID = m_gsWildBall.ListOfSettingIDToBeUpdated(7);
           //             if (lSettingID.Count != 0)
           //             {
           //                 m_gsWildBall.PopulateDataIntoControls();
           //             }

           //             break;    

           //         }

           //     case "GameSettingToggleButton_WildFireProg":
           //         {
           //             m_selectedGame = "WILDFIREPROG";
           //             view = m_gsWilfFireProg;
           //             m_gameID = 7;
           //             if (m_B3Setting.B3GameSetting_[7].IsEnabled == true)
           //             {
           //                 //tglBtnEnable.IsChecked = true;
           //             }
           //             else
           //             {
           //                 //tglBtnEnable.IsChecked = false;
           //             }

           //             lSettingID = m_gsWilfFireProg.ListOfSettingIDToBeUpdated();
           //             if (lSettingID.Count != 0)
           //             {
           //                 m_gsWilfFireProg.PopulateDataIntoControls();
           //             }

           //             break;   
           //         }             
           // }

            //switch(m_gameID)
            //{
                //case 0: lblB3GameName.Content =  "Crazy Bout"; break;
                //case 1: lblB3GameName.Content = "JailBreak"; break;
                //case 2: lblB3GameName.Content = "Maya Money"; break;
                //case 3: lblB3GameName.Content = "76 Bingo"; break;
                //case 4: lblB3GameName.Content = "Time Bomb";  break;
                //case 5: lblB3GameName.Content = "U Kick Em"; break;
                //case 6: lblB3GameName.Content = "WildFire w/Bonus"; break;
                //case 7: lblB3GameName.Content = "WildFire"; break;
        //}
           
            //We have to uncheck any previously checked buttons
            //foreach (var menuItem in m_menuItems)
            //{
            //    if (Equals(menuItem, toggleButton))
            //    {
            //        continue;
            //    }

            //    if (menuItem.IsChecked != null && (bool)menuItem.IsChecked)
            //    {
            //        menuItem.IsChecked = false;
            //    }
            //}

            //GameSettingTransitionControl.Content = view;

            //if (tglBtnEnable.IsChecked == true)
            //{
            //    GameSettingTransitionControl.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    GameSettingTransitionControl.Visibility = Visibility.Hidden;
            //}
        }

        private void tglBtnEnable_Checked(object sender, RoutedEventArgs e)
        {
            //if (tglBtnEnable.IsChecked == true)
            //{
            //    m_isGameEnable = true;
            //    lblEnable.Content = "ON";
            //    GameSettingTransitionControl.Content = view;
            //    if (GameSettingTransitionControl.Visibility != Visibility.Visible)
            //    {
            //        GameSettingTransitionControl.Visibility = Visibility.Visible;
            //    }

            //}            
        }

        private void tglBtnEnable_Unchecked(object sender, RoutedEventArgs e)
        {
            //if (tglBtnEnable.IsChecked != true)
            //{
            //    m_isGameEnable = false;
            //    lblEnable.Content = "OFF";
            //    GameSettingTransitionControl.Content = null;
            //}    
        }

        private void Grid_GotFocus(object sender, RoutedEventArgs e)
        {
            //ClearSavedNotification();
        }

        private void btnBackOperatorSettingsFromGameSettings_Click(object sender, RoutedEventArgs e)
        {

        }

        void m_btnCancel_Click(object sender, RoutedEventArgs e)
        {
          //  List<SettingMember> lSettingID = new List<SettingMember>();
          //switch (m_gameID)
          //{
          //    case 0:
          //        {
          //            if (m_B3Setting.B3GameSetting_[0].IsEnabled == true)
          //            {
          //                tglBtnEnable.IsChecked = true;
          //            }
          //            else
          //            {
          //                tglBtnEnable.IsChecked = false;
          //            }


          //            lSettingID = m_gsCrazyBout.ListOfSettingIDToBeUpdated(1);
          //            if (lSettingID.Count != 0)
          //            {
          //                m_gsCrazyBout.PopulateDataIntoControls();
          //            }
          //            break;
          //        }
          //    case 1:
          //        {
          //            if (m_B3Setting.B3GameSetting_[1].IsEnabled == true)
          //            {
          //                tglBtnEnable.IsChecked = true;
          //            }
          //            else
          //            {
          //                tglBtnEnable.IsChecked = false;
          //            }

          //            lSettingID = m_gsJailBreak.ListOfSettingIDToBeUpdated(2);
          //            if (lSettingID.Count != 0)
          //            {
          //                m_gsJailBreak.PopulateDataIntoControls();
          //            }

          //            break;
          //        }
          //    case 2:
          //        {
          //            if (m_B3Setting.B3GameSetting_[2].IsEnabled == true)
          //            {
          //                tglBtnEnable.IsChecked = true;
          //            }
          //            else
          //            {
          //                tglBtnEnable.IsChecked = false;
          //            }

          //            lSettingID = m_gsMayaMoney.ListOfSettingIDToBeUpdated(3);
          //            if (lSettingID.Count != 0)
          //            {
          //                m_gsMayaMoney.PopulateDataIntoControls();
          //            }

          //            break;
          //        }
          //    case 3:
          //        {
          //            if (m_B3Setting.B3GameSetting_[3].IsEnabled == true)
          //            {
          //                tglBtnEnable.IsChecked = true;
          //            }
          //            else
          //            {
          //                tglBtnEnable.IsChecked = false;
          //            }

          //            lSettingID = m_gsSpirit76.ListOfSettingIDToBeUpdated(4);
          //            if (lSettingID.Count != 0)
          //            {
          //                m_gsSpirit76.PopulateDataIntoControls();
          //            }

          //            break;
          //        }
          //    case 4:
          //        {
          //            if (m_B3Setting.B3GameSetting_[4].IsEnabled == true)
          //            {
          //                tglBtnEnable.IsChecked = true;
          //            }
          //            else
          //            {
          //                tglBtnEnable.IsChecked = false;
          //            }

          //            lSettingID = m_gsTimeBomb.ListOfSettingIDToBeUpdated(5);
          //            if (lSettingID.Count != 0)
          //            {
          //                m_gsTimeBomb.PopulateDataIntoControls();
          //            }

          //            break;
          //        }
          //    case 5:
          //        {
          //            if (m_B3Setting.B3GameSetting_[5].IsEnabled == true)
          //            {
          //                tglBtnEnable.IsChecked = true;
          //            }
          //            else
          //            {
          //                tglBtnEnable.IsChecked = false;
          //            }

          //            lSettingID = m_gsUkickEm.ListOfSettingIDToBeUpdated();
          //            if (lSettingID.Count != 0)
          //            {
          //                m_gsUkickEm.PopulateDataIntoControls();
          //            }

          //            break;
          //        }
          //    case 6:
          //        {
          //            if (m_B3Setting.B3GameSetting_[6].IsEnabled == true)
          //            {
          //                tglBtnEnable.IsChecked = true;
          //            }
          //            else
          //            {
          //                tglBtnEnable.IsChecked = false;
          //            }

          //            lSettingID = m_gsWildBall.ListOfSettingIDToBeUpdated(7);
          //            if (lSettingID.Count != 0)
          //            {
          //                m_gsWildBall.PopulateDataIntoControls();
          //            }


          //            break;
          //        }
          //    case 7:
          //        {
          //            if (m_B3Setting.B3GameSetting_[7].IsEnabled == true)
          //            {
          //                //tglBtnEnable.IsChecked = true;
          //            }
          //            else
          //            {
          //                //tglBtnEnable.IsChecked = false;
          //            }

          //            lSettingID = m_gsWilfFireProg.ListOfSettingIDToBeUpdated();
          //            if (lSettingID.Count != 0)
          //            {
          //                m_gsWilfFireProg.PopulateDataIntoControls();
          //            }

          //            break;
          //        }
          //}
        }


    #endregion
        
    }

    public class B3GameNameDesc
    {
        //string[] B3GameName =         
        //{
        //    "Crazy Bout" ,
        //    "JailBreak" ,
        //    "Maya Money",
        //    "76 Bingo ",
        //    "Time Bomb ",
        //    "U Kick Em ",
        //    "WildFire w/Bonus ", 
        //    "WildFire ",
        //};

        
        //public string this[int index]
        //{
        //    get { return B3GameName[index]; }
        //    set { B3GameName[index] = value; }
        //}
    
    }
}
