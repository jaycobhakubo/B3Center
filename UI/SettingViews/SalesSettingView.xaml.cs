﻿using System;
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
    /// Interaction logic for SalesSettingView.xaml
    /// </summary>
    public partial class SalesSettingView : UserControl
    {
        #region MEMBER VARIABLEs

        private List<B3SettingGlobal> m_B3Settings = new List<B3SettingGlobal>();
        private bool m_screenCursor;
        private bool m_calibrateTouch;
        private bool m_autoPrintSession;
        private bool m_autoPagePrinter;
        private bool m_quickSales;
        private bool m_printLogo;
        private bool m_allowSessionBallCallChange;
        private bool m_loggingEnable;
        private int m_logRecycleDays;
        private int m_salesMainVolume;
        private readonly SaveCancelCrtl m_saveCancelCtrl;
        private List<SettingMember> m_lB3Settings;
        private Button m_btnSave;
        private Button m_btnCancel;

        #endregion

        #region CONSTRUCTOR

        public SalesSettingView(List<B3SettingGlobal> B3Settings)
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
        }

        #endregion

        #region PRIVATE METHODS

        private void PopulateDataIntoVar()
        {
            bool result;
            m_screenCursor = Convert.ToBoolean(result = (m_B3Settings[0].B3SettingValue == "T") ? true : false);
            m_calibrateTouch = Convert.ToBoolean(result = (m_B3Settings[1].B3SettingValue == "T") ? true : false);
            m_autoPrintSession = Convert.ToBoolean(result = (m_B3Settings[2].B3SettingValue == "T") ? true : false);
            m_autoPagePrinter = Convert.ToBoolean(result = (m_B3Settings[3].B3SettingValue == "T") ? true : false);
            m_quickSales = Convert.ToBoolean(result = (m_B3Settings[4].B3SettingValue == "T") ? true : false);
            m_printLogo = Convert.ToBoolean(result = (m_B3Settings[5].B3SettingValue == "T") ? true : false);
            m_allowSessionBallCallChange = Convert.ToBoolean(result = (m_B3Settings[6].B3SettingValue == "T") ? true : false);
            m_loggingEnable = Convert.ToBoolean(result = (m_B3Settings[7].B3SettingValue == "T") ? true : false);
            m_logRecycleDays = Convert.ToInt32(m_B3Settings[8].B3SettingValue);
            m_salesMainVolume = Convert.ToInt32(m_B3Settings[9].B3SettingValue);
        }

        private void PopulateDataIntoControls()
        {
            chkbxScreenCursor.IsChecked = m_screenCursor;
            chkbxCalibrateTouch.IsChecked = m_calibrateTouch;
            chkbxAutoPrintSession.IsChecked = m_autoPrintSession;
            chkbxAutoPagePrinter.IsChecked = m_autoPagePrinter;
            chkbxQuickSales.IsChecked = m_quickSales;
            chkbxPrintLogo.IsChecked = m_printLogo;
            chkbxAllowSessionBallChange.IsChecked = m_allowSessionBallCallChange;
            chkbxLoggingEnable.IsChecked = m_loggingEnable;
            txtbxLogRecycleDays.Text = m_logRecycleDays.ToString();
            cmbxMainVolume.SelectedIndex = cmbxMainVolume.Items.IndexOf(GetVolumeEquivValue(m_salesMainVolume));           
        }

        private string GetOldValuesForComparison(int settingID)
        {
            string result = "";

            switch (settingID)
            {
                case 24: { result = (m_screenCursor == true) ? "T" : "F"; break; }
                case 25: { result = (m_calibrateTouch == true) ? "T" : "F"; break; }
                case 26: { result = (m_autoPrintSession == true) ? "T" : "F"; break; }
                case 27: { result = (m_autoPagePrinter == true) ? "T" : "F"; break; }
                case 28: { result = (m_quickSales == true) ? "T" : "F"; break; }
                case 29: { result = (m_printLogo == true) ? "T" : "F"; break; }
                case 30: { result = (m_allowSessionBallCallChange == true) ? "T" : "F"; break; }
                case 31: { result = (m_loggingEnable == true) ? "T" : "F"; break; }
                case 32: { result =  m_logRecycleDays.ToString(); break; }
                case 33: { result =  m_salesMainVolume.ToString(); break; }
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
                    case 24:
                        {
                            m_screenCursor = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 25:
                        {
                            m_calibrateTouch = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 26:
                        {
                            m_autoPrintSession = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 27:
                        {
                            m_autoPagePrinter = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 28:
                        {
                            m_quickSales = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 29:
                        {
                            m_printLogo = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 30:
                        {
                            m_allowSessionBallCallChange = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 31:
                        {
                            m_loggingEnable = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 32:
                        {
                            m_logRecycleDays = Convert.ToInt32(sm.m_value);
                            break;
                        }
                    case 33:
                        {
                            m_salesMainVolume = Convert.ToInt32(sm.m_value);
                            break;
                        }
                }
            }
            PopulateDataIntoControls();
        }


        private bool ValidateUserInput()
        {
            bool result = true; //True = validation ok ; false validation not ok.
            if (string.IsNullOrEmpty(txtbxLogRecycleDays.Text) == true)
            {
                result = false;
                txtbxLogRecycleDays.BorderBrush = Brushes.Red;
            }     
            return result;
        }

        private void SetBorderBrushToDefault()
        {
            txtbxLogRecycleDays.BorderBrush = Brushes.LightGray;
        }

        /// <summary>
        /// Compare old value to the new value.
        /// </summary>
        private List<SettingMember> ListOfSettingIDToBeUpdated()
        {
           
            m_lB3Settings = new List<SettingMember>();

            foreach (UIElement elem in gridSalesSettings.Children)
            {
                if (elem is StackPanel)
                {
                    StackPanel stkpnl = (StackPanel)elem;
                    foreach (UIElement element in stkpnl.Children)
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

                                if (sm.m_settingID == 33)
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
                }
            }
            return m_lB3Settings;
        }



        private void PopulateComboBoxControls()
        {
            cmbxMainVolume.ItemsSource = Volume();
        }

        private void ClearSavedNotification()
        {
            //if (lblSavedNotification.Visibility != Visibility.Hidden) lblSavedNotification.Visibility = Visibility.Hidden;
        }

       private string GetVolumeEquivValue(int Volume)
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
                {
                    throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, "B3 Set Server Setting Failed", ServerErrorTranslator.GetReturnCodeMessage(msg.ReturnCode)));
                }

                if (lSettingMember.Exists(p => p.m_settingID == 30))
                {
                    string IsAllowed = lSettingMember.FirstOrDefault(p => p.m_settingID == 30).m_value;
                    SessionViewModel.Instance.Settings.AllowInSessBallChange = (IsAllowed == "T") ? true : false;
                }

                RepopulateNewSaveData(lSettingMember);
                //lblSavedNotification.Visibility = Visibility.Visible;
            }
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

        private void txtbxLogRecycleDays_PreviewKeyDown(object sender, KeyEventArgs e)
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
