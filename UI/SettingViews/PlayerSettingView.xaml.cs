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
    /// Interaction logic for PlayerSettingView.xaml
    /// </summary>
    public partial class PlayerSettingView : UserControl
    {
        #region MEMBER VARIABLEs

        private List<B3SettingGlobal> m_B3PlayerSetting = new List<B3SettingGlobal>();
        private bool m_calibrateTouch;
        private bool m_pressToCollect;
        private bool m_announceCall;
        private bool m_screenCursor;
        private int m_timeToCollect;
        private bool m_disclaimer;
        private int m_disclaimerID;
        private int m_mainVol;
        private readonly SaveCancelCrtl m_saveCancelCtrl;
        private List<SettingMember> m_lB3Settings;
        private Button m_btnSave;
        private Button m_btnCancel;

        #endregion

        #region CONSTRUCTOR

        public PlayerSettingView(List<B3SettingGlobal> B3PlayerSetting )
        {
            InitializeComponent();
            m_B3PlayerSetting = B3PlayerSetting;
            PopulateComboBoxControls();
            PopulateDataIntoVar();
            PopulateDataIntoControls();
            m_saveCancelCtrl = new SaveCancelCrtl();
            m_btnSave = m_saveCancelCtrl.btnSave;        
            m_btnSave.Click += new RoutedEventHandler(m_btnSave_Click);
            m_btnCancel = m_saveCancelCtrl.btnCancel;
            m_btnCancel.Click += new RoutedEventHandler(m_btnCancel_Click);
            SaveCancelTransition.Content = (UserControl)m_saveCancelCtrl;
            SaveCancelTransition.Visibility = Visibility.Visible;
        }

        #endregion

        #region PRIVATE METHODs

        private void PopulateDataIntoVar()
        {
            bool result;
            m_calibrateTouch = Convert.ToBoolean(result = (m_B3PlayerSetting[0].B3SettingValue == "T") ? true : false);
            m_pressToCollect = Convert.ToBoolean(result = (m_B3PlayerSetting[1].B3SettingValue == "T") ? true : false);
            m_announceCall = Convert.ToBoolean(result = (m_B3PlayerSetting[2].B3SettingValue == "T") ? true : false);
            m_screenCursor = Convert.ToBoolean(result = (m_B3PlayerSetting[3].B3SettingValue == "T") ? true : false);
            m_timeToCollect = Convert.ToInt32(m_B3PlayerSetting[4].B3SettingValue);
            m_disclaimer = Convert.ToBoolean(result = (m_B3PlayerSetting[5].B3SettingValue == "T") ? true : false);
            m_disclaimerID = Convert.ToInt32(m_B3PlayerSetting[6].B3SettingValue);
            m_mainVol = Convert.ToInt32(m_B3PlayerSetting[7].B3SettingValue);
        }

        public void PopulateDataIntoControls()
        {
            chkbxCalibrateTouch.IsChecked = m_calibrateTouch;
            chkbxPressToCollect.IsChecked = m_pressToCollect;
            chkbxAnnounceCall.IsChecked = m_announceCall;
            chkbxScreenCursor.IsChecked = m_screenCursor;
            txtbxTimeToCollect.Text = m_timeToCollect.ToString();
            chkbxDisclaimer.IsChecked = m_disclaimer;
            cmbxMainVol.SelectedIndex = cmbxMainVol.Items.IndexOf(GetVolumeEquivValue(m_mainVol));
           
        }

        private string GetOldValuesForComparison(int settingID)
        {
            string result = "";

            switch (settingID)
            {
                case 16: { result = (m_calibrateTouch == true) ? "T" : "F"; break; }
                case 17: { result = (m_pressToCollect == true) ? "T" : "F"; break; }
                case 18: { result = (m_announceCall == true) ? "T" : "F"; break; }
                case 19: { result = (m_screenCursor == true) ? "T" : "F"; break; }
                case 20: { result = m_timeToCollect.ToString(); break; }
                case 21: { result = (m_disclaimer == true) ? "T" : "F"; break; }
                case 22: { result =  m_disclaimerID.ToString(); break; }
                case 23: { result = m_mainVol.ToString(); break; }   
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
                    case 16:
                        {
                            m_calibrateTouch = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 17:
                        {
                            m_pressToCollect = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 18:
                        {
                            m_announceCall = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 19:
                        {
                            m_screenCursor = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 20:
                        {
                            m_timeToCollect = Convert.ToInt32(sm.m_value);
                            break;
                        }
                    case 21:
                        {
                            m_disclaimer = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 22:
                        {
                           m_disclaimerID = Convert.ToInt32(sm.m_value);
                            break;
                        }
                    case 23:
                        {
                            m_mainVol = Convert.ToInt32(sm.m_value);
                            break;
                        }                   
                }
            }
            PopulateDataIntoControls();
        }


        private bool ValidateUserInput()
        {
            bool result = true; //True = validation ok ; false validation not ok.
           
            if (string.IsNullOrEmpty( txtbxTimeToCollect.Text) == true)
            {
                result = false;
                txtbxTimeToCollect.BorderBrush = Brushes.Red;
            }

            return result;
        }

        private void SetBorderBrushToDefault()
        {
            txtbxTimeToCollect.BorderBrush = Brushes.LightGray;
        }


        /// <summary>
        /// Compare old value to the new value.
        /// </summary>
        private List<SettingMember> ListOfSettingIDToBeUpdated()
        {
            m_lB3Settings = new List<SettingMember>();
            foreach (UIElement element in stkpnlPlayerSetting.Children)
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

                else if (element is CheckBox)
                {
                    SettingMember sm = new SettingMember();
                    CheckBox chkbx = (CheckBox)element;
                    if (chkbx.Tag != null && chkbx != null)
                    {
                        sm.m_settingID = Convert.ToInt32(chkbx.Tag);
                        sm.m_gameID = 0;
                        sm.m_value = (chkbx.IsChecked == true) ? "T" : "F";
                        string OldValue = GetOldValuesForComparison(sm.m_settingID);
                        sm.m_oldValue = OldValue;

                        if (sm.m_value != OldValue)
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

                        if (sm.m_settingID == 23)
                        {
                            NewValue = btn.SelectedItem.ToString();
                            OldValue = GetVolumeEquivValue(Convert.ToInt32(OldValue));
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


            }

            return m_lB3Settings;
        }

        public void PopulateComboBoxControls()
        {
            cmbxMainVol.ItemsSource = Volume();

        }

        public string GetVolumeEquivValue(int Volume)
        {
            string tempValue = "";
            if (Volume <= 100 && Volume >= 91) { tempValue = "10"; }
            else if (Volume < 91 && Volume >= 81) { tempValue = "9"; }
            else if (Volume < 81 && Volume >= 71) { tempValue = "8"; }
            else if (Volume < 71 && Volume >= 61) { tempValue = "7"; }
            else if (Volume < 61 && Volume >= 51) { tempValue = "6"; }
            else if (Volume < 51 && Volume >= 41) { tempValue = "5"; }
            else if (Volume < 41 && Volume >= 31) { tempValue = "4"; }
            else if (Volume < 31 && Volume >= 21) { tempValue = "3"; }
            else if (Volume < 21 && Volume >= 11) { tempValue = "2"; }
            else if (Volume < 11 && Volume >= 1) { tempValue = "1"; }
            else if (Volume == 0) { tempValue = "0"; }
            return tempValue;
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

      

       
       
        private List<string> Volume()
        {
            List<string> result = new List<string>();
            result.Add("0");
            result.Add("1");
            result.Add("2");
            result.Add("3");
            result.Add("4");
            result.Add("5");
            result.Add("6");
            result.Add("7");
            result.Add("8");
            result.Add("9");
            result.Add("10");
            return result;
        }

        private void ClearSavedNotification()
        {
            if (lblSavedNotification.Visibility != Visibility.Hidden) lblSavedNotification.Visibility = Visibility.Hidden;
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
            lSettingMember = ListOfSettingIDToBeUpdated();

            if (lSettingMember.Count != 0)
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                SetB3SettingsMessage msg = new SetB3SettingsMessage(lSettingMember);
                msg.Send();

                Mouse.OverrideCursor = null;

                if (msg.ReturnCode != ServerReturnCode.Success)
                    throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, "B3 Set Server Setting Failed", ServerErrorTranslator.GetReturnCodeMessage(msg.ReturnCode)));

                RepopulateNewSaveData(lSettingMember);
                lblSavedNotification.Visibility = Visibility.Visible;       
            }
        }
         void m_btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReloadDataIntoControls();
        }


        private void Grid_GotFocus(object sender, RoutedEventArgs e)
        {
            ClearSavedNotification();
            SetBorderBrushToDefault();
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

        #endregion
    }
}
