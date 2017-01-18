using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Messages;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews
{
    /// <summary>
    /// Interaction logic for ServerGameSettingView.xaml
    /// </summary>
    public partial class ServerGameSettingView : UserControl
    {

        #region MEMBER VARIABLES

        private List<B3SettingGlobal> m_B3Settings;
        private int m_minPlayers;
        private int m_gameStartDelay;
        private decimal m_consolationPrize;
        private string m_gameRecallPassword;
        private int m_waitCountDown;
        private SaveCancelCrtl m_saveCancelCtrl;
                    private Button m_btnSave;
        private Button m_btnCancel;
        private List<SettingMember> m_lB3Settings;//Save setting id that needs to be updated.
        private bool m_isValidationOk;  

        #endregion

        #region PROPERTIES

        public Button btnSave
        {
            get { return m_btnSave; }
            set { m_btnSave = value; }
        }

        #endregion

        #region CONSTRUCTOR

        public ServerGameSettingView(/*ServerM ServerSetting*/List<B3SettingGlobal> B3Settings)
        {
            InitializeComponent();
            //DataContext = ServerSetting;
            //List<B3SettingGlobal> B3Settings = new List<B3SettingGlobal>();
            m_B3Settings = B3Settings;
            PopulateDataIntoVar();
            PopulateDataIntoControls();
            m_saveCancelCtrl = new SaveCancelCrtl();
            m_btnSave = m_saveCancelCtrl.btnSave;
            m_btnSave.Click += new RoutedEventHandler(m_btnSave_Click);
            m_btnCancel = m_saveCancelCtrl.btnCancel;
            m_btnCancel.Click += new RoutedEventHandler(m_btnCancel_Click);
            //SaveCancelTransition.Content = (UserControl)m_saveCancelCtrl;
            //SaveCancelTransition.Visibility = Visibility.Visible;
            //lblSavedNotification.Visibility = Visibility.Hidden;
        }

        #endregion

        #region PRIVATE METHODS

        private void PopulateDataIntoVar()
        {
            m_minPlayers = Convert.ToInt32(m_B3Settings[0].B3SettingValue);
            m_gameStartDelay= Convert.ToInt32(m_B3Settings[1].B3SettingValue);
            m_consolationPrize = Convert.ToDecimal(m_B3Settings[2].B3SettingValue);
            m_gameRecallPassword = m_B3Settings[3].B3SettingValue.ToString();
            m_waitCountDown = Convert.ToInt32(m_B3Settings[4].B3SettingValue);
        }


        private void PopulateDataIntoControls()
        {
            txtbxMinPlayers.Text = m_minPlayers.ToString();
            txtbxGameStartDelay.Text = m_gameStartDelay.ToString();
            txtbxConsolationPrize.Text = ConvertToString.decimal_(m_consolationPrize.ToString());
            txtbxGameRecallPassword.Text = m_gameRecallPassword.ToString();
            txtbxWaitCountDown.Text = m_waitCountDown.ToString();
        }


        private string GetOldValuesForComparison(int settingID)
        {
            string result = "";

            switch (settingID)
            {
                case 34: { result = m_minPlayers.ToString(); break; }
                case 35: { result = m_gameStartDelay.ToString(); break; }
                case 36: { result =  m_consolationPrize.ToString(); break; }
                case 37: { result = m_gameRecallPassword.ToString(); break; }
                case 38: { result = m_waitCountDown.ToString(); break; }
            }

            return result;
        }


        private bool ValidateUserInput()
        {
            bool result = true; //True = validation ok ; false validation not ok.
            decimal decValue;

            if (string.IsNullOrEmpty(txtbxMinPlayers.Text) == true)
            {
                result = false;
                txtbxMinPlayers.BorderBrush = Brushes.Red;
            }

            if (string.IsNullOrEmpty(txtbxGameStartDelay.Text) == true)
            {
                result = false;
                txtbxGameStartDelay.BorderBrush = Brushes.Red;
            }

            if (string.IsNullOrEmpty(txtbxWaitCountDown.Text) == true)
            {
                result = false;
                txtbxWaitCountDown.BorderBrush = Brushes.Red;
            }

            if (string.IsNullOrEmpty(txtbxConsolationPrize.Text) == true)
            {
                result = false;
                txtbxConsolationPrize.BorderBrush = Brushes.Red;
            }
            else
                if (!Decimal.TryParse(txtbxConsolationPrize.Text, out decValue))//If not valid decimal.
                {
                    result = false;
                    txtbxConsolationPrize.BorderBrush = Brushes.Red;
                }

            if (string.IsNullOrEmpty(txtbxGameRecallPassword.Text) == true || string.IsNullOrWhiteSpace(txtbxGameRecallPassword.Text) == true)
            {
                result = false;
                txtbxGameRecallPassword.BorderBrush = Brushes.Red;
            }

            if (@result == false)
            {
                m_isValidationOk = false;
            }

            return result;
        }


        private void SetBorderBrushToDefault()
        {
            //txtbxMinPlayers.BorderBrush = Brushes.LightGray;
            //txtbxGameStartDelay.BorderBrush = Brushes.LightGray;
            //txtbxWaitCountDown.BorderBrush = Brushes.LightGray;
            //txtbxConsolationPrize.BorderBrush = Brushes.LightGray;
            //txtbxGameRecallPassword.BorderBrush = Brushes.LightGray;
        }

        /// <summary>
        /// Compare old value to the new value.
        /// </summary>
        private List<SettingMember> ListOfSettingIDToBeUpdated()
        {
            m_lB3Settings = new List<SettingMember>();
            foreach (UIElement element in stkpnlServerGame.Children)
            {
                if (element is TextBox)
                {
                    TextBox txt = (TextBox)element;
                    if (txt.Tag != null && txt != null)
                    {
                        SettingMember sm = new SettingMember();
                        sm.m_settingID = Convert.ToInt32(txt.Tag);
                        sm.m_gameID = 0;
                        string OldValue = GetOldValuesForComparison(sm.m_settingID); //m_B3SettingCrazyBt.Single(l => l.B3SettingID == sm.m_settingID).B3SettingValue;
                        string NewValue = txt.Text;                    
                        sm.m_value = NewValue;
                        sm.m_oldValue = OldValue;
                
                        if (NewValue != OldValue)
                        {
                            m_lB3Settings.Add(sm);
                        }
                    }
                }           
            }
            return m_lB3Settings;
        }

        private void RepopulateNewSaveData(List<SettingMember> lsm)
        {
         
            foreach (SettingMember sm in lsm)
            {
                switch (sm.m_settingID)
                {
                    case 34:
                        {
                            m_minPlayers = Convert.ToInt32(sm.m_value);
                            break;
                        }
                    case 35:
                        {
                            m_gameStartDelay = Convert.ToInt32(sm.m_value);
                            break;
                        }
                    case 36:
                        {
                            m_consolationPrize = Convert.ToDecimal(sm.m_value);
                            break;
                        }
                    case 37:
                        {
                            m_gameRecallPassword = sm.m_value.ToString();
                            break;
                        }
                    case 38:
                        {
                            m_waitCountDown = Convert.ToInt32(sm.m_value);
                            break;
                        }
                  
                }
            }
            PopulateDataIntoControls();
        }

        private void ClearSavedNotification()
        {
            //if (lblSavedNotification.Visibility != Visibility.Hidden) lblSavedNotification.Visibility = Visibility.Hidden;
        }

        #endregion

        #region PUBLIC METHODS

        public void ReloadDataIntoControls()
        {
            ClearSavedNotification();
            List<SettingMember> lSettingMember = new List<SettingMember>();
            lSettingMember = ListOfSettingIDToBeUpdated();
            if (lSettingMember.Count != 0)
            {
                PopulateDataIntoControls();  
            }
        }



        #endregion

        #region EVENTS

        void m_btnSave_Click(object sender, RoutedEventArgs e)
        {

            if (ValidateUserInput() == false)
            {
                return;
            }

            List<SettingMember> lSettingMember = new List<SettingMember>();
             lSettingMember    = ListOfSettingIDToBeUpdated();

             if (lSettingMember.Count != 0)
             {
                 Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                 SetB3SettingsMessage msg = new SetB3SettingsMessage(lSettingMember);
                 msg.Send();
                 Mouse.OverrideCursor = null;


                 if (msg.ReturnCode != ServerReturnCode.Success)
                     throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, "B3 Set Server Setting Failed", ServerErrorTranslator.GetReturnCodeMessage(msg.ReturnCode)));

                 RepopulateNewSaveData(lSettingMember);
                 //lblSavedNotification.Visibility = Visibility.Visible;
             }
        }

        void m_btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReloadDataIntoControls();
        }

        private void gridServerMessage_GotFocus(object sender, RoutedEventArgs e)
        {
            ClearSavedNotification();
            if (m_isValidationOk == false)
            {
                SetBorderBrushToDefault();
                m_isValidationOk = true;
            }
        }

        private void txtbxNumericOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtbx_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            bool notAllow = false;

            if (e.Key == Key.Space)
            {
                notAllow = true;
            }
            else
                if (e.Key == Key.Back)
                {
                    notAllow = false;
                }
            e.Handled = notAllow;
        }

        private void txtbxPrice_PreviewTextInput2(object sender, TextCompositionEventArgs e)
        {
            bool NotAllow = false;

            //Get all the text in the textbox including keypreview.
            TextBox y = (TextBox)sender;
            string x = y.Text;
            x = x.Insert(y.SelectionStart, e.Text);

            int count = x.Split('.').Length - 1;//Count how many decimal places on the text input

            if (count > 1)//One decimal point only.
            {
                NotAllow = true;
            }
            else if ((Convert.ToChar(e.Text)) == '.')
            {
                NotAllow = false;
            }
            else if (Char.IsNumber(Convert.ToChar(e.Text)))
            {
                NotAllow = false;

                if (Regex.IsMatch(x, @"\.\d\d\d"))//Only allow .## 
                {
                    NotAllow = true;
                }
            }
            else
            {
                NotAllow = true;
            }

            e.Handled = NotAllow;
        }

        private void txtbxMinPlayers_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txtbx = (TextBox)sender;
            int tempMinPlayer;
            tempMinPlayer = Convert.ToInt32(txtbx.Text);
            if (tempMinPlayer < 2)
            {
                txtbxMinPlayers.Text = "2";
            }
        }

        #endregion


    }
}
