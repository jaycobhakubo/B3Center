﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using GameTech.Elite.Client.Modules.B3Center.Business;
//using GameTech.Elite.Client.Modules.B3Center.Messages;

//namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews
//{
//    /// <summary>
//    /// Interaction logic for GameSettingWildBallProgView.xaml
//    /// </summary>
//    public partial class GameSettingWildBallProgView : UserControl
//    {

//        private List<B3SettingGlobal> m_B3SettingWildProg = new List<B3SettingGlobal>();
//        private int m_maxBetLevel;
//        private int m_maxCards;
//        private int m_callSpeed;
//        private bool m_autoPlay;
//        private bool m_autoCall;
//        private bool m_hideSerialNumber;
//        private int m_mathPackageId;
//        private string m_mathPackage;
//        private List<SettingMember> m_lB3Settings;
//        private List<B3MathGamePay> m_listB3MathGamePlay;

//        public GameSettingWildBallProgView(List<B3SettingGlobal> B3SettingWildProg, List<B3MathGamePay> ListB3MathGamePlay)
//        {
//            InitializeComponent();
//            m_B3SettingWildProg = B3SettingWildProg;
//            m_listB3MathGamePlay = ListB3MathGamePlay;
//            PopulateComboBoxControls();
//            PopulateDataIntoVar();
//            PopulateDataIntoControls();
//            m_lB3Settings = new List<SettingMember>();
//        }


//        #region METHODS

  
//        private void PopulateComboBoxControls()
//        {
//            cmbxMaxBetLevel.ItemsSource = LoadMaxBetLevel();
//            cmbxMaxcards.ItemsSource = LoadMaxCard();
//            cmbxCallSpeed.ItemsSource = LoadMaxBetLevel();//1 to 10
//            cmbxGamePayTable.ItemsSource = m_listB3MathGamePlay.Select(l => l.PackageDesc);
//        }

//        private void PopulateDataIntoVar()
//        {
//            bool result;

//            m_maxBetLevel = Convert.ToInt32(m_B3SettingWildProg[0].B3SettingValue);
//            m_maxCards = Convert.ToInt32(m_B3SettingWildProg[1].B3SettingValue);
//            m_callSpeed = Convert.ToInt32(m_B3SettingWildProg[2].B3SettingValue);
//            m_autoCall = Convert.ToBoolean(result = (m_B3SettingWildProg[3].B3SettingValue == "T") ? true : false);
//            m_autoPlay = Convert.ToBoolean(result = (m_B3SettingWildProg[4].B3SettingValue == "T") ? true : false);
//            m_hideSerialNumber = Convert.ToBoolean(result = (m_B3SettingWildProg[5].B3SettingValue == "T") ? true : false);
//            m_mathPackageId = Convert.ToInt32(m_B3SettingWildProg[6].B3SettingValue);
//            if (m_mathPackageId == 0)
//            {
//                m_mathPackage = "";
//            }
//            else
//            {
//                m_mathPackage = m_listB3MathGamePlay.FirstOrDefault(l => l.MathPackageID == m_mathPackageId).PackageDesc;
//            }
//        }


//        public void PopulateDataIntoControls()
//        {

//            cmbxMaxBetLevel.SelectedIndex = cmbxMaxBetLevel.Items.IndexOf(m_maxBetLevel.ToString());
//            cmbxMaxcards.SelectedIndex = cmbxMaxcards.Items.IndexOf(m_maxCards.ToString());
//            cmbxCallSpeed.SelectedIndex = cmbxCallSpeed.Items.IndexOf(GetCallSpeedEquivValue(m_callSpeed));
//            chkbxAutoCall.IsChecked = m_autoCall;
//            chkbxAutoPlay.IsChecked = m_autoPlay;
//            chkbxHideSerialNumber.IsChecked = m_hideSerialNumber;
//            cmbxGamePayTable.SelectedIndex = cmbxGamePayTable.Items.IndexOf(m_mathPackage);
//        }

//        public void RepopulateNewSaveData(List<SettingMember> lsm)
//        {
//            bool result = false;
//            foreach (SettingMember sm in lsm)
//            {
//                switch (sm.m_settingID)
//                {
                    
//                    case 9:
//                        {
//                            m_maxBetLevel = Convert.ToInt32(sm.m_value);
//                            break;
//                        }
//                    case 10:
//                        {
//                            m_maxCards = Convert.ToInt32(sm.m_value);
//                            break;
//                        }

//                    case 11:
//                        {
//                            m_callSpeed = Convert.ToInt32(sm.m_value);
//                            break;
//                        }
//                    case 12:
//                        {
//                            m_autoCall = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
//                            break;
//                        }
//                    case 13:
//                        {
//                            m_autoPlay = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
//                            break;
//                        }
//                    case 14:
//                        {
//                            m_hideSerialNumber = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
//                            break;
//                        }
//                    case 58:
//                        {
//                            m_mathPackageId = Convert.ToInt32(sm.m_value);
//                            if (m_mathPackageId == 0)
//                            {
//                                m_mathPackage = "";
//                            }
//                            else
//                            {
//                                m_mathPackage = m_listB3MathGamePlay.FirstOrDefault(l => l.MathPackageID == m_mathPackageId).PackageDesc;
//                            }
//                            break;
//                        }
//                }
//            }
//            PopulateDataIntoControls();
//        }

//        private void LoopThroughElementInAContainer(UIElement element)
//        {
//            if (element is ComboBox)
//            {
//                ComboBox btn = (ComboBox)element;
//                if (btn.Tag != null && btn != null)
//                {
//                    SettingMember sm = new SettingMember();
//                    sm.m_settingID = Convert.ToInt32(btn.Tag);
//                    sm.m_gameID = 8;


//                    string OldValue = GetOldValuesForComparison(sm.m_settingID); //m_B3SettingCrazyBt.Single(l => l.B3SettingID == sm.m_settingID).B3SettingValue;
//                    string NewValue = "";
//                    if (sm.m_settingID == 11)
//                    {
//                        NewValue = btn.SelectedItem.ToString();
//                        OldValue = GetCallSpeedEquivValue(Convert.ToInt32(OldValue));
//                        if (NewValue != OldValue)
//                        {
//                            NewValue = GetCallSpeedEquivToDB(Convert.ToInt32(NewValue));
//                        }
//                    }
//                    else
//                    {
//                        if (btn.SelectedIndex != -1)
//                        {
//                            NewValue = btn.SelectedItem.ToString();
//                        }
//                    }

//                    sm.m_value = NewValue;
//                    sm.m_oldValue = OldValue;

//                    if (NewValue != OldValue)
//                    {
//                        if (sm.m_settingID == 58)
//                        {
//                            int tempValue = m_listB3MathGamePlay.FirstOrDefault(l => l.PackageDesc == NewValue).MathPackageID;
//                            sm.m_value = tempValue.ToString();
//                        }
//                        m_lB3Settings.Add(sm);
//                    }
//                }
//            }

//            else if (element is CheckBox)
//            {
//                SettingMember sm = new SettingMember();
//                CheckBox chkbx = (CheckBox)element;
//                if (chkbx.Tag != null && chkbx != null)
//                {
//                    sm.m_settingID = Convert.ToInt32(chkbx.Tag);
//                    sm.m_gameID = 8;
//                    sm.m_value = (chkbx.IsChecked == true) ? "T" : "F";
//                    string OldValue = GetOldValuesForComparison(sm.m_settingID);//m_B3SettingCrazyBt.Single(l => l.B3SettingID == sm.m_settingID).B3SettingValue;
//                    sm.m_oldValue = OldValue;

//                    if (sm.m_value != OldValue)
//                    {
//                        m_lB3Settings.Add(sm);
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// Compare old value to the new value.
//        /// </summary>
//        public List<SettingMember> ListOfSettingIDToBeUpdated()
//        {
//            m_lB3Settings = new List<SettingMember>();
//            foreach (UIElement element in gridWildBallProg.Children)
//            {
//                LoopThroughElementInAContainer(element);
//                if (element is StackPanel)
//                {
//                    StackPanel stkpnl = (StackPanel)element;
//                    foreach (UIElement elemStkPnl in stkpnl.Children)
//                    {
//                        LoopThroughElementInAContainer(elemStkPnl);
//                    }
//                }
//            }

//            if (m_lB3Settings.Exists(p => p.m_settingID == 58))
//            {
//                SetAllControlsToComplyWithPayTable(8);
//            }


//            return m_lB3Settings;

//        }

//        private void SetAllControlsToComplyWithPayTable(int gameID)//Set all checkboxes to true if its a pay table changes.
//        {
//            string NewValue;
//            string OldValue;
//            int TempSettingID;

//            //Set denom all to false.
//            foreach (UIElement element2 in gridWildBallProg.Children)
//            {

//                if (element2 is CheckBox)
//                {
//                    SettingMember sm = new SettingMember();
//                    CheckBox chkbx = (CheckBox)element2;

//                    if (chkbx.Tag != null && chkbx != null)
//                    {
//                        TempSettingID = Convert.ToInt32(chkbx.Tag);
//                        if (TempSettingID < 9)
//                        {
//                            chkbx.IsChecked = true;
//                            sm.m_settingID = TempSettingID;
//                            sm.m_gameID = gameID;
//                            NewValue = (chkbx.IsChecked == true) ? "T" : "F";
//                            OldValue = GetOldValuesForComparison(sm.m_settingID);
//                            sm.m_value = NewValue;
//                            sm.m_oldValue = OldValue;
//                            if (sm.m_value != OldValue)
//                            {
//                                m_lB3Settings.Add(sm);
//                            }
//                        }
//                    }
//                }
//            }

//            //HideSeriaNumber
//            chkbxHideSerialNumber.IsChecked = false;
//            TempSettingID = Convert.ToInt32(chkbxHideSerialNumber.Tag);
//            OldValue = GetOldValuesForComparison(TempSettingID);
//            NewValue = (chkbxHideSerialNumber.IsChecked == true) ? "T" : "F";

//            if (NewValue != OldValue)
//            {
//                SettingMember sm = new SettingMember();
//                sm.m_settingID = TempSettingID;
//                sm.m_gameID = gameID;
//                sm.m_value = NewValue;
//                sm.m_oldValue = OldValue;
//                m_lB3Settings.Add(sm);

//            }

//            //SET Bet Level = 1
//            cmbxMaxBetLevel.SelectedIndex = 0;
//            TempSettingID = Convert.ToInt32(cmbxMaxBetLevel.Tag);
//            OldValue = GetOldValuesForComparison(TempSettingID);
//            NewValue = cmbxMaxBetLevel.SelectedItem.ToString();

//            if (NewValue != OldValue)
//            {
//                SettingMember sm = new SettingMember();
//                sm.m_settingID = TempSettingID;
//                sm.m_gameID = gameID;
//                sm.m_value = NewValue;
//                sm.m_oldValue = OldValue;
//                m_lB3Settings.Add(sm);

//            }

//            //SET Max Cards = 6
//            cmbxMaxcards.SelectedIndex = 1;
//            TempSettingID = Convert.ToInt32(cmbxMaxcards.Tag);
//            OldValue = GetOldValuesForComparison(TempSettingID);
//            NewValue = cmbxMaxcards.SelectedItem.ToString();

//            if (NewValue != OldValue)
//            {
//                SettingMember sm = new SettingMember();
//                sm.m_settingID = TempSettingID;
//                sm.m_gameID = gameID;
//                sm.m_value = NewValue;
//                sm.m_oldValue = OldValue;
//                m_lB3Settings.Add(sm);
//            }
//        }


//        private string GetCallSpeedEquivToDB(int callspeedvalue)
//        {
//            string result = "";
//            switch (callspeedvalue)
//            {
//                case 1: { result = "5000"; break; }
//                case 2: { result = "4020"; break; }
//                case 3: { result = "3530"; break; }
//                case 4: { result = "3040"; break; }
//                case 5: { result = "2550"; break; }
//                case 6: { result = "2060"; break; }
//                case 7: { result = "1570"; break; }
//                case 8: { result = "1080"; break; }
//                case 9: { result = "590"; break; }
//                case 10: { result = "100"; break; }
//            }
//            return result;
//        }


//        private string GetOldValuesForComparison(int settingID)
//        {
//            string result = "";

//            switch (settingID)
//            {

//                case 9: { result = m_maxBetLevel.ToString(); break; }
//                case 10: { result =  m_maxCards.ToString(); break; }//Original value
//                case 11: { result = m_callSpeed.ToString(); break; }//Original value
//                case 12: { result = (m_autoCall == true) ? "T" : "F"; break; }
//                case 13: { result = (m_autoPlay == true) ? "T" : "F"; break; }
//                case 14: { result = (m_hideSerialNumber == true) ? "T" : "F"; break; }
//                case 58: { result = m_mathPackage.ToString(); break; }
//            }

//            return result;
//        }

//        private string GetCallSpeedEquivValue(int callSpeedValue)
//        {
//            //100 = 10 (fastest)
//            //5000 = 1 (Slowest)
//            string tempCallSpeed = "";
//            if (m_callSpeed == 100) { tempCallSpeed = "10"; }
//            else if (m_callSpeed > 100 && m_callSpeed <= 590) { tempCallSpeed = "9"; }
//            else if (m_callSpeed > 590 && m_callSpeed <= 1080) { tempCallSpeed = "8"; }
//            else if (m_callSpeed > 1080 && m_callSpeed <= 1570) { tempCallSpeed = "7"; }
//            else if (m_callSpeed > 1570 && m_callSpeed <= 2060) { tempCallSpeed = "6"; }
//            else if (m_callSpeed > 2060 && m_callSpeed <= 2550) { tempCallSpeed = "5"; }
//            else if (m_callSpeed > 2550 && m_callSpeed <= 3040) { tempCallSpeed = "4"; }
//            else if (m_callSpeed > 3040 && m_callSpeed <= 3530) { tempCallSpeed = "3"; }
//            else if (m_callSpeed > 3530 && m_callSpeed <= 4020) { tempCallSpeed = "2"; }
//            else if (m_callSpeed == 5000) { tempCallSpeed = "1"; }
//            return tempCallSpeed;
//        }


//        private List<string> LoadMaxBetLevel()
//        {
//            List<string> result = new List<string>();
//            result.Add("1");
//            result.Add("2");
//            result.Add("3");
//            result.Add("4");
//            result.Add("5");
//            result.Add("6");
//            result.Add("7");
//            result.Add("8");
//            result.Add("9");
//            result.Add("10");
//            return result;
//        }

//        private List<string> LoadMaxCard()
//        {
//            List<string> result = new List<string>();

//            result.Add("4");
//            result.Add("6");

//            return result;
//        }


//        #endregion

//        #region EVENTS

//        private void txtbxCallSpeedMin_PreviewTextInput(object sender, TextCompositionEventArgs e)
//        {
//            Regex regex = new Regex("[^0-9]+");
//            e.Handled = regex.IsMatch(e.Text);
//        }

//        #endregion
//    }
//}
