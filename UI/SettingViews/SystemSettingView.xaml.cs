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

namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews
{
    /// <summary>
    /// Interaction logic for SystemSettingView.xaml
    /// </summary>
    public partial class SystemSettingView : UserControl
    {
        #region MEMBER VARIABLES

        private List<B3SettingGlobal> m_B3Settings = new List<B3SettingGlobal>();
        private decimal m_handPayoutTrigger;
        private decimal m_vipPointMultiPlayer;
        private string m_magcardSentinelStart;
        private string m_magcardSentineEnd;
        private string m_currency;
        private string m_autoSessionEnd; //'','T','F'
        private string m_siteName;
        private int m_minimumPlayers;
        private int m_rngBallCallTime;
        private int m_playerPinLength;
        private int m_systemSettingmainVolume;
        private bool m_enableUK;
        private bool m_dualAccount;
        private bool m_multiOperator;
        private bool m_commonRNGBallCall;
        private bool m_northDakotaMode;
        private bool isValidateOk;
        private bool m_isNDSettingEnable;
        private readonly SaveCancelCrtl m_saveCancelCtrl;
        private Button m_btnSave;
        private Button m_btnCancel;
        private List<SettingMember> m_lB3Settings;//Save setting id that needs to be updated.


        #endregion

        #region PROPERTIES
       
        public Button btnSave
        {
           get { return m_btnSave; }
           set { m_btnSave = value; }
        }

        public bool isNDSettingEnable
        {
            get { return m_isNDSettingEnable; }
        }
       
        #endregion 

        #region CONSTRUCTORS

        public SystemSettingView(List<B3SettingGlobal> B3Settings)
        {
            InitializeComponent();
            m_B3Settings = B3Settings;
            PopulateComboBoxControls();
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

        #region PRIVATE METHODs

        private void PopulateDataIntoVar()
        {
            bool result;
            m_handPayoutTrigger = Convert.ToDecimal(m_B3Settings[0].B3SettingValue);
            m_minimumPlayers = Convert.ToInt32(m_B3Settings[1].B3SettingValue);
            m_vipPointMultiPlayer = Convert.ToDecimal(m_B3Settings[2].B3SettingValue);
            m_magcardSentinelStart = (m_B3Settings[3].B3SettingValue.ToString());
            m_magcardSentineEnd = (m_B3Settings[4].B3SettingValue.ToString());
            m_currency = m_B3Settings[5].B3SettingValue.ToString();
            m_rngBallCallTime = Convert.ToInt32(m_B3Settings[6].B3SettingValue);
            m_playerPinLength = Convert.ToInt32(m_B3Settings[7].B3SettingValue);
            m_enableUK = Convert.ToBoolean(result = (m_B3Settings[8].B3SettingValue == "T") ? true : false);
            m_dualAccount = Convert.ToBoolean(result = (m_B3Settings[9].B3SettingValue == "T") ? true : false);
            m_multiOperator = Convert.ToBoolean(result = (m_B3Settings[10].B3SettingValue == "T") ? true : false);
            m_commonRNGBallCall = Convert.ToBoolean(result = (m_B3Settings[11].B3SettingValue == "T") ? true : false);
            m_northDakotaMode = Convert.ToBoolean(result = (m_B3Settings[12].B3SettingValue == "T") ? true : false);
            m_autoSessionEnd = GetAutoSessionValue(m_B3Settings[13].B3SettingValue.ToString());            
            m_siteName = m_B3Settings[14].B3SettingValue.ToString();
            m_systemSettingmainVolume = Convert.ToInt32(m_B3Settings[15].B3SettingValue);
        }

        private string GetAutoSessionValue(string AutoSessionEndDbValue)
        {
            string result = "";

            if (AutoSessionEndDbValue == "")
            {
                result = "OFF";
            }
            else
            {
                result = AutoSessionEndDbValue;
            }

            return result;
        }

        private void PopulateDataIntoControls()
        {
            txtbxHandPayoutTrigger.Text = m_handPayoutTrigger.ToString();
            txtbxMinimumPlayers.Text = m_minimumPlayers.ToString();
            txtbxVipPointPlayer.Text = m_vipPointMultiPlayer.ToString();
            txtbxMagCardStart.Text = m_magcardSentinelStart.ToString();
            txtbxMagCardEnd.Text = m_magcardSentineEnd.ToString();
            cmbxCurrency.SelectedIndex = cmbxCurrency.Items.IndexOf(m_currency.ToString());
            txtbxRNGBallCallTime.Text = m_rngBallCallTime.ToString();
            txtbxPlayerpinLength.Text = m_playerPinLength.ToString();
            chkbxEnableUK.IsChecked = m_enableUK;
            chkbxDualAccount.IsChecked = m_dualAccount;
            chkbxMultiOperator.IsChecked = m_multiOperator;
            chkbxCommonRNGBallCall.IsChecked = m_commonRNGBallCall;
            chkbxNorthDakotaMode.IsChecked = m_northDakotaMode;
            cmbxAutoSessionEnd.SelectedIndex = cmbxAutoSessionEnd.Items.IndexOf(m_autoSessionEnd.ToString());       
            txtbxSiteName.Text = m_siteName.ToString();
            cmbxMainVol.SelectedIndex = cmbxMainVol.Items.IndexOf(GetEquivValue(m_systemSettingmainVolume));
        }


        private string GetOldValuesForComparison(int settingID)
        {
            string result = "";

            switch (settingID)
            {
                case 42: { result = m_handPayoutTrigger.ToString(); break; }
                case 43: { result = m_minimumPlayers.ToString(); break; }
                case 44: { result = m_vipPointMultiPlayer.ToString(); break; }
                case 45: { result = m_magcardSentinelStart.ToString(); break; }
                case 46: { result = m_magcardSentineEnd.ToString(); break; }
                case 47: { result = m_currency.ToString(); break; }
                case 48: { result = m_rngBallCallTime.ToString(); break; }
                case 49: { result = m_playerPinLength.ToString(); break; }
                case 50: { result = (m_enableUK == true) ? "T" : "F"; break; }
                case 51: { result = (m_dualAccount == true) ? "T" : "F"; break; }
                case 52: { result = (m_multiOperator == true) ? "T" : "F"; break; }
                case 53: { result = (m_commonRNGBallCall == true) ? "T" : "F"; break; }
                case 54: { result = (m_northDakotaMode == true) ? "T" : "F"; break; }
                case 55: { result = m_autoSessionEnd.ToString(); break; }//Set to "F" if NULL.
                case 56: { result =  m_siteName.ToString(); break; }
                case 57: { result = m_systemSettingmainVolume.ToString(); break; }
            }

            return result;
        }

        private void RepopulateNewSaveData(List<SettingMember> lsm)
        {
            bool result = false;
            foreach (SettingMember sm in lsm)
            {
                switch (sm.m_settingID)
                {
                    case 42:
                        {
                            m_handPayoutTrigger  = Convert.ToDecimal(sm.m_value);
                            break;
                        }
                    case 43:
                        {
                            m_minimumPlayers = Convert.ToInt32(sm.m_value);
                            break;
                        }
                    case 44:
                        {
                            m_vipPointMultiPlayer = Convert.ToDecimal(sm.m_value);
                            break;
                        }
                    case 45:
                        {
                            m_magcardSentinelStart = sm.m_value.ToString();
                            break;
                        }
                    case 46:
                        {
                            m_magcardSentineEnd = sm.m_value.ToString();
                            break;
                        }
                    case 47:
                        {
                            m_currency = sm.m_value.ToString();
                            break;
                        }
                    case 48:
                        {
                          m_rngBallCallTime = Convert.ToInt32(sm.m_value);
                            break;
                        }
                    case 49:
                        {
                            m_playerPinLength= Convert.ToInt32(sm.m_value);
                            break;
                        }
                    case 50:
                        {
                           m_enableUK =  Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 51:
                        {
                            m_dualAccount = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 52:
                        {
                            m_multiOperator = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 53:
                        {
                            m_commonRNGBallCall = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 54:
                        {
                            m_northDakotaMode = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 55:
                        {
                            m_autoSessionEnd = sm.m_value.ToString();
                            break;
                        }
                    case 56:
                        {
                            m_siteName = sm.m_value.ToString();
                            break;
                        }
                    case 57:
                        {
                            m_systemSettingmainVolume = Convert.ToInt32(sm.m_value);
                            break;
                        }
                }
            }
            PopulateDataIntoControls();
        }

        private bool ValidateUserInput()
        {
            bool result = true; //True = validation ok ; false validation not ok.
            decimal decValuel;

            if (string.IsNullOrEmpty( txtbxRNGBallCallTime.Text) == true)
            {
                result = false;
                txtbxRNGBallCallTime.BorderBrush = Brushes.Red;
            }

            if (string.IsNullOrEmpty(txtbxHandPayoutTrigger.Text) == true)
            {
                result = false;
                txtbxHandPayoutTrigger.BorderBrush = Brushes.Red;
            }
            else
                if (!Decimal.TryParse(txtbxHandPayoutTrigger.Text, out decValuel))
                {
                    result = false;
                    txtbxHandPayoutTrigger.BorderBrush = Brushes.Red;
                }

            if (string.IsNullOrEmpty(txtbxVipPointPlayer.Text) == true)
            {
                result = false;
                txtbxVipPointPlayer.BorderBrush = Brushes.Red;
            }
            else
                if (!Decimal.TryParse(txtbxVipPointPlayer.Text, out decValuel))
                {
                    result = false;
                    txtbxVipPointPlayer.BorderBrush = Brushes.Red;
                }

            if (string.IsNullOrEmpty(txtbxMagCardStart.Text) == true || string.IsNullOrWhiteSpace(txtbxMagCardStart.Text) == true)
            {
                result = false;
                txtbxMagCardStart.BorderBrush = Brushes.Red;
            }

            if (string.IsNullOrEmpty(txtbxMagCardEnd.Text) == true || string.IsNullOrWhiteSpace(txtbxMagCardEnd.Text) == true)
            {
                result = false;
                txtbxMagCardEnd.BorderBrush = Brushes.Red;
            }

            if (string.IsNullOrEmpty(txtbxMinimumPlayers.Text) == true || string.IsNullOrWhiteSpace(txtbxMinimumPlayers.Text) == true)
            {
                result = false;
                txtbxMinimumPlayers.BorderBrush = Brushes.Red;
            }

            if (string.IsNullOrEmpty(txtbxSiteName.Text) == true || string.IsNullOrWhiteSpace(txtbxSiteName.Text) == true)
            {          
                result = false;
                txtbxSiteName.BorderBrush = Brushes.Red;
            }

            if (string.IsNullOrEmpty(txtbxPlayerpinLength.Text) == true || string.IsNullOrWhiteSpace(txtbxPlayerpinLength.Text) == true)
            {
                result = false;
                txtbxSiteName.BorderBrush = Brushes.Red;
            }

            if (result == false)
            {
                isValidateOk = false;
            }
            return result;
        }

        private void SetBorderBrushToDefault()
        {
            txtbxRNGBallCallTime.BorderBrush = Brushes.LightGray;
            txtbxSiteName.BorderBrush = Brushes.LightGray;
            txtbxVipPointPlayer.BorderBrush = Brushes.LightGray;
            txtbxHandPayoutTrigger.BorderBrush = Brushes.LightGray;
            txtbxMagCardStart.BorderBrush = Brushes.LightGray;
            txtbxMagCardEnd.BorderBrush = Brushes.LightGray;
            txtbxMinimumPlayers.BorderBrush = Brushes.LightGray;
            txtbxPlayerpinLength.BorderBrush = Brushes.LightGray;
        }


        private void LoopThroughElementInAContainer(UIElement element)
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

            else if (element is ComboBox)
            {
                ComboBox btn = (ComboBox)element;
                if (btn.Tag != null && btn != null)
                {
                    SettingMember sm = new SettingMember();
                    sm.m_settingID = Convert.ToInt32(btn.Tag);
                    sm.m_gameID = 0;


                    string OldValue = GetOldValuesForComparison(sm.m_settingID); //m_B3SettingCrazyBt.Single(l => l.B3SettingID == sm.m_settingID).B3SettingValue;
                    string NewValue = "";

                    if (sm.m_settingID == 57)
                    {
                        NewValue = btn.SelectedItem.ToString();
                        OldValue = GetEquivValue(Convert.ToInt32(OldValue));
                        if (NewValue != OldValue)
                        {
                            NewValue = GetVolumeEquivToDB(Convert.ToInt32(NewValue));
                        }
                    }
                    else
                    {
                        NewValue = btn.SelectedItem.ToString();
                    }

                    sm.m_value = NewValue;
                    sm.m_oldValue = OldValue;

                    if (NewValue != OldValue)
                    {
                        m_lB3Settings.Add(sm);
                    }
                }
            }
            else  if (element is CheckBox)
            {
                SettingMember sm = new SettingMember();
                CheckBox chkbx = (CheckBox)element;
                if (chkbx.Tag != null && chkbx != null)
                {
                    sm.m_settingID = Convert.ToInt32(chkbx.Tag);
                    sm.m_gameID = 0;
                    sm.m_value = (chkbx.IsChecked == true) ? "T" : "F";
                    string OldValue = GetOldValuesForComparison(sm.m_settingID);//m_B3SettingCrazyBt.Single(l => l.B3SettingID == sm.m_settingID).B3SettingValue;
                    sm.m_oldValue = OldValue;

                    if (sm.m_settingID == 55)
                    {
                        if (OldValue == "" && sm.m_value == "F")
                        {
                            sm.m_value = "";
                        }
                    }

                    if (sm.m_value != OldValue)
                    {
                        m_lB3Settings.Add(sm);
                    }
                }
            }
        }

        /// <summary>
        /// Compare old value to the new value.
        /// </summary>
        private List<SettingMember> ListOfSettingIDToBeUpdated()
        {
            m_lB3Settings = new List<SettingMember>();

            foreach (UIElement element in gridSystemSettings.Children)
            {
                LoopThroughElementInAContainer(element);
                if (element is StackPanel)
                {
                    StackPanel stkpnl = (StackPanel)element;
                    foreach (UIElement elemStkPnl in stkpnl.Children)
                    {
                        LoopThroughElementInAContainer(elemStkPnl);
                    }
                }
            }
                    
            return m_lB3Settings;
        }

        /// <summary>
        /// Load combobox control.
        /// </summary>

        private void PopulateComboBoxControls()
        {
            cmbxMainVol.ItemsSource = cmbxValue(0, 10);
           
            List<string> CurrencyItems = new List<string>();//Harcoded no table currency available in db.
            CurrencyItems.Add("CREDIT");
            CurrencyItems.Add("DOLLAR");
            CurrencyItems.Add("PESO");
            CurrencyItems.Add("POUND");
            cmbxCurrency.ItemsSource = CurrencyItems;

            List<string> AutoSessionEndItems = new List<string>();
            AutoSessionEndItems.Add("JACKPOT");
            AutoSessionEndItems.Add("PAYOUT");
            AutoSessionEndItems.Add("OFF");
            cmbxAutoSessionEnd.ItemsSource = AutoSessionEndItems;
        }

        private string GetEquivValue(int fromvalue)
        {
            string toValue = "";
              
                if (fromvalue <= 100 && fromvalue >= 91) { toValue = "10"; }
                else if (fromvalue < 91 && fromvalue >= 81) { toValue = "9"; }
                else if (fromvalue < 81 && fromvalue >= 71) { toValue = "8"; }
                else if (fromvalue < 71 && fromvalue >= 61) { toValue = "7"; }
                else if (fromvalue < 61 && fromvalue >= 51) { toValue = "6"; }
                else if (fromvalue < 51 && fromvalue >= 41) { toValue = "5"; }
                else if (fromvalue < 41 && fromvalue >= 31) { toValue = "4"; }
                else if (fromvalue < 31 && fromvalue >= 21) { toValue = "3"; }
                else if (fromvalue < 21 && fromvalue >= 11) { toValue = "2"; }
                else if (fromvalue < 11 && fromvalue >= 1) { toValue = "1"; }
                else if (fromvalue == 0) { toValue = "0"; }
        
                return toValue;
        }

        private string GetVolumeEquivToDB(int VolumeLevel)
        {
            string result = "";
            switch (VolumeLevel)
            {
                case 0: { result = "0"; break; }
                case 1: { result = "10"; break; }
                case 2: { result = "20"; break; }
                case 3: { result = "30"; break; }
                case 4: { result = "40"; break; }
                case 5: { result = "50"; break; }
                case 6: { result = "60"; break; }
                case 7: { result = "70"; break; }
                case 8: { result = "80"; break; }
                case 9: { result = "90"; break; }
                case 10: { result = "100"; break; }


            }
            return result;
        }

        private List<string> cmbxValue(int StartCount, int EndCount)
        {
            List<string> result = new List<string>();
                     
                for (int i = StartCount ; i < EndCount + 1; i++)
                {
                    result.Add(i.ToString());
                }
      
            return result;
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

        void m_btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReloadDataIntoControls();
        }

        void m_btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateUserInput() == false)
            {
                return;
            }

            List<SettingMember> lSettingMember = new List<SettingMember>();
            lSettingMember = ListOfSettingIDToBeUpdated();

            if (lSettingMember.Count != 0)
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                SetB3SettingsMessage msg = new SetB3SettingsMessage(lSettingMember);
                msg.Send();
                Mouse.OverrideCursor = null;

                if (msg.ReturnCode != ServerReturnCode.Success)
                    throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, "B3 Set Server Setting Failed", ServerErrorTranslator.GetReturnCodeMessage(msg.ReturnCode)));

                if (lSettingMember.Exists(l => l.m_settingID == 54))//North Dakota Mode
                {
                    m_isNDSettingEnable = (lSettingMember.Single(l => l.m_settingID == 54).m_value == "T" ? true : false);                   
                }

                if (lSettingMember.Exists(l => l.m_settingID == 53))//Common RNG Ball Call
                {      
                    var viewModel = SessionViewModel.Instance;
                    viewModel.UpdateIsBallCallPermission(lSettingMember.Single(l => l.m_settingID == 53).m_value == "T" ? true : false);
                }

                RepopulateNewSaveData(lSettingMember);
                //lblSavedNotification.Visibility = Visibility.Visible;
            }
        }

        private void Grid_GotFocus(object sender, RoutedEventArgs e)
        {
            ClearSavedNotification();
            if (isValidateOk == false)
            {
                SetBorderBrushToDefault();
                isValidateOk = true;
            }
        }


        private void txtbxNumericOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool result = false; //false = ok; true = !ok
            Regex regex = new Regex("[^0-9]+");
            result = regex.IsMatch(e.Text);
            if (result != false)
            {
                //if its not numeric skip the next statement.
            }
            else
            {
                TextBox Items = (TextBox)sender;
                if (Convert.ToInt32(Items.Tag) == 48)
                {
                    if (Items.Text.Count() == 7)
                    {
                        int ValueInMilliseconds = Convert.ToInt32(Items.Text + e.Text);
                        if (ValueInMilliseconds >  86400000) // No more than 24 hours.
                        {
                            result = true;
                        }
                    }
                }
            }

            e.Handled = result;
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

        private void txtbxRNGBallCallTime_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox Items = (TextBox)sender;
            if (string.IsNullOrEmpty(Items.Text) != true || string.IsNullOrWhiteSpace(Items.Text))
            {
                int RNGBallCallTimeMilliSeconds;
                if (int.TryParse(Items.Text, out RNGBallCallTimeMilliSeconds))
                {
                    if (RNGBallCallTimeMilliSeconds < 100)
                    {
                        Items.Text = "100".ToString();
                    }
                }
            }
        }

        #endregion

     


    }
}
