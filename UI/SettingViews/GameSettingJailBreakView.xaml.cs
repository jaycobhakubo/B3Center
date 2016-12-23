using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Messages;


namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews
{
    /// <summary>
    /// Interaction logic for GameSettingJailBreak.xaml
    /// </summary>
    public partial class GameSettingJailBreak : UserControl
    {
        #region MEMBER VARIABLES

        private List<B3SettingGlobal> m_B3SettingJailBreak = new List<B3SettingGlobal>();
        private int m_maxBetLevel;
        private int m_maxCards;
        private int m_callSpeed;
        private int m_minCallSpeed;
        private int m_mathPackageId;
        private bool m_autoPlay;
        private bool m_autoCall;
        private bool m_hideSerialNumber;
        private bool m_denom01;
        private bool m_denom05;
        private bool m_denom10;
        private bool m_denom25;
        private bool m_denom50;
        private bool m_denom100;
        private bool m_denom200;
        private bool m_denom500;
        private bool m_hasMinSpeed;
        private bool m_hasCallSpeedBonus;
        private string m_mathPackage;
        private List<SettingMember> m_lB3Settings;
        private List<B3MathGamePay> m_listB3MathGamePlay;

        #endregion  

        #region CONSTRUCTOR

        public GameSettingJailBreak(List<B3SettingGlobal> B3SettingJailBreak, List<B3MathGamePay> ListB3MathGamePlay)
        {
            InitializeComponent();
            m_B3SettingJailBreak = B3SettingJailBreak;

            m_hasMinSpeed = false;
            m_hasCallSpeedBonus = false;

            if (m_B3SettingJailBreak[0].B3GameID == 3)
            {
                m_hasMinSpeed = true;        
            }
            else if (m_B3SettingJailBreak[0].B3GameID == 4)
            {
                m_hasCallSpeedBonus = true;
                lblCallSpeedOrMax.Content = "Call Speed";
                lblCallSpeedMinOrBonus.Content = "Call Speed Bonus";
                cmbxCallSpeedMinOrBonus.Visibility = Visibility.Visible;
                lblCallSpeedMinOrBonus.Visibility = Visibility.Visible;
                rowDefCallSpeedItems.Height = new GridLength(50);
                rowDefCallSpeedMargin.Height = new GridLength(15);
                lblCallSpeedMinOrBonus.SetValue(Grid.RowProperty, 9);
                Grid.SetRow(lblCallSpeedMinOrBonus, 9);
                lblCallSpeedOrMax.SetValue(Grid.RowProperty, 7);
                Grid.SetRow(lblCallSpeedOrMax, 7);
                cmbxCallSpeedMinOrBonus.SetValue(Grid.RowProperty, 9);
                Grid.SetRow(cmbxCallSpeedMinOrBonus, 9);
                cmbxCallSpeedOrMax.SetValue(Grid.RowProperty, 7);
                Grid.SetRow(cmbxCallSpeedOrMax, 7);
            }
            else
            {
                lblCallSpeedOrMax.Content = "Call Speed";
                cmbxCallSpeedMinOrBonus.Visibility = Visibility.Collapsed;
                lblCallSpeedMinOrBonus.Visibility = Visibility.Collapsed;
                rowDefCallSpeedItems.Height = GridLength.Auto;
                rowDefCallSpeedMargin.Height = GridLength.Auto;
            }

            m_listB3MathGamePlay = ListB3MathGamePlay;
            PopulateComboBoxControls();
            PopulateDataIntoVar();
            PopulateDataIntoControls();
            m_lB3Settings = new List<SettingMember>();
        }

        #endregion

        #region METHODS

        private void PopulateComboBoxControls()
        {
            cmbxMaxBetLevel.ItemsSource = LoadMaxBetLevel();
            cmbxMaxcards.ItemsSource = LoadMaxCard();
            cmbxCallSpeedOrMax.ItemsSource = LoadMaxBetLevel();//1 to 10
            cmbxGamePayTable.ItemsSource = m_listB3MathGamePlay.Select(l => l.PackageDesc);


            if (m_hasMinSpeed == true || m_hasCallSpeedBonus == true)
            {
                cmbxCallSpeedMinOrBonus.ItemsSource = LoadMaxBetLevel();
            }
            
                
        }

     
        private void PopulateDataIntoVar()
        {
            bool result;
            m_denom01 = Convert.ToBoolean(result = (m_B3SettingJailBreak[0].B3SettingValue == "T") ? true : false);
            m_denom05 = Convert.ToBoolean(result = (m_B3SettingJailBreak[1].B3SettingValue == "T") ? true : false);
            m_denom10 = Convert.ToBoolean(result = (m_B3SettingJailBreak[2].B3SettingValue == "T") ? true : false);
            m_denom25 = Convert.ToBoolean(result = (m_B3SettingJailBreak[3].B3SettingValue == "T") ? true : false);
            m_denom50 = Convert.ToBoolean(result = (m_B3SettingJailBreak[4].B3SettingValue == "T") ? true : false);
            m_denom100 = Convert.ToBoolean(result = (m_B3SettingJailBreak[5].B3SettingValue == "T") ? true : false);
            m_denom200 = Convert.ToBoolean(result = (m_B3SettingJailBreak[6].B3SettingValue == "T") ? true : false);
            m_denom500 = Convert.ToBoolean(result = (m_B3SettingJailBreak[7].B3SettingValue == "T") ? true : false);
            m_maxBetLevel = Convert.ToInt32(m_B3SettingJailBreak[8].B3SettingValue);
            m_maxCards = Convert.ToInt32(m_B3SettingJailBreak[9].B3SettingValue);
            m_callSpeed = Convert.ToInt32(m_B3SettingJailBreak[10].B3SettingValue);
            m_autoCall = Convert.ToBoolean(result = (m_B3SettingJailBreak[11].B3SettingValue == "T") ? true : false);
            m_autoPlay = Convert.ToBoolean(result = (m_B3SettingJailBreak[12].B3SettingValue == "T") ? true : false);
            m_hideSerialNumber = Convert.ToBoolean(result = (m_B3SettingJailBreak[13].B3SettingValue == "T") ? true : false);
            m_mathPackageId = Convert.ToInt32(m_B3SettingJailBreak[14].B3SettingValue);
            if (m_mathPackageId == 0)
            {
                m_mathPackage = "";
            }
            else
            {
                m_mathPackage = m_listB3MathGamePlay.FirstOrDefault(l => l.MathPackageID == m_mathPackageId).PackageDesc;
            }

            if (m_hasMinSpeed == true || m_hasCallSpeedBonus == true)
            {
                m_minCallSpeed = Convert.ToInt32(m_B3SettingJailBreak[15].B3SettingValue);
            }
           
        }

        public void PopulateDataIntoControls()
        {

            cmbxMaxBetLevel.SelectedIndex = cmbxMaxBetLevel.Items.IndexOf(m_maxBetLevel.ToString());
            cmbxMaxcards.SelectedIndex = cmbxMaxcards.Items.IndexOf(m_maxCards.ToString());
            cmbxCallSpeedOrMax.SelectedIndex = cmbxCallSpeedOrMax.Items.IndexOf(GetCallSpeedEquivValue(m_callSpeed));
            chkbxAutoCall.IsChecked = m_autoCall;
            chkbxAutoPlay.IsChecked = m_autoPlay;
            chkbxHideSerialNumber.IsChecked = m_hideSerialNumber;
            chkbxDenom01.IsChecked = m_denom01;
            chkbxDenom05.IsChecked = m_denom05;
            chkbxDenom10.IsChecked = m_denom10;
            chkbxDenom25.IsChecked = m_denom25;
            chkbxDenom50.IsChecked = m_denom50;
            chkbxDenom100.IsChecked = m_denom100;
            chkbxDenom200.IsChecked = m_denom200;
            chkbxDenom500.IsChecked = m_denom500;
            cmbxGamePayTable.SelectedIndex = cmbxGamePayTable.Items.IndexOf(m_mathPackage);

            if (m_hasMinSpeed == true || m_hasCallSpeedBonus == true)
            {
                cmbxCallSpeedMinOrBonus.SelectedIndex = cmbxCallSpeedMinOrBonus.Items.IndexOf(GetCallSpeedEquivValue(m_minCallSpeed));
            }
           
        }


        public void RepopulateNewSaveData(List<SettingMember> lsm)
        {
            bool result = false;
            foreach (SettingMember sm in lsm)
            {
                switch (sm.m_settingID)
                {
                    case 1:
                        {
                            m_denom01 = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 2:
                        {
                            m_denom05 = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 3:
                        {
                            m_denom10 = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 4:
                        {
                            m_denom25 = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 5:
                        {
                            m_denom50 = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 6:
                        {
                            m_denom100 = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;

                        }
                    case 7:
                        {
                            m_denom200 = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 8:
                        {
                            m_denom500 = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 9:
                        {
                            m_maxBetLevel = Convert.ToInt32(sm.m_value);
                            break;
                        }
                    case 10:
                        {
                            m_maxCards = Convert.ToInt32(sm.m_value);
                            break;
                        }
                    case 11:
                        {
                            m_callSpeed = Convert.ToInt32(sm.m_value);
                            break;
                        }
                    case 12:
                        {
                            m_autoCall = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 13:
                        {
                            m_autoPlay = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 14:
                        {
                            m_hideSerialNumber = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                    case 58:
                        {
                            m_mathPackageId = Convert.ToInt32(sm.m_value);
                            if (m_mathPackageId == 0)
                            {
                                m_mathPackage = "";
                            }
                            else
                            {
                                m_mathPackage = m_listB3MathGamePlay.FirstOrDefault(l => l.MathPackageID == m_mathPackageId).PackageDesc;
                            }
                            break;
                        }
                    case 59:
                        {
                            if (m_hasMinSpeed == true || m_hasCallSpeedBonus == true)
                            {
                                m_minCallSpeed = Convert.ToInt32(sm.m_value);
                            }
                          
                            break;
                        }
                    case 60:
                        {
                            if (m_hasCallSpeedBonus == true)
                            {
                                m_minCallSpeed = Convert.ToInt32(sm.m_value);
                            }
                            break;
                        }

                }
            }
            PopulateDataIntoControls();
        }



        private void LoopThroughElementInAContainer(UIElement element, int gameID)
        {
            if (element is ComboBox)
            {
                ComboBox btn = (ComboBox)element;
                if (btn.Tag != null && btn != null)
                {
                    SettingMember sm = new SettingMember();
                    sm.m_settingID = Convert.ToInt32(btn.Tag);
                    sm.m_gameID = gameID;
                    string OldValue = "";

                    if (sm.m_settingID == 59)
                    {
                        int tempID;
                        if (m_hasMinSpeed == true)
                        {
                            tempID = 59;
                        }
                        else
                        {
                            tempID = 60;
                        }
                        OldValue = GetOldValuesForComparison(tempID);
                    }
                    else
                    {
                        OldValue = GetOldValuesForComparison(sm.m_settingID);
                    }

                    string NewValue = "";

                    if (sm.m_settingID == 11 || (sm.m_settingID == 59))
                    {
                        if (btn.SelectedIndex != -1)
                        {
                            NewValue = btn.SelectedItem.ToString();
                            OldValue = GetCallSpeedEquivValue(Convert.ToInt32(OldValue));
                        }

                        if (NewValue != OldValue)
                        {
                            NewValue = GetCallSpeedEquivToDB(Convert.ToInt32(NewValue));
                        }
                    }
                    else
                    {
                        if (btn.SelectedIndex != -1)
                        {
                            NewValue = btn.SelectedItem.ToString();
                        }
                    }

                    sm.m_value = NewValue;
                    sm.m_oldValue = OldValue;

                    if (NewValue != OldValue)
                    {
                        if (sm.m_settingID == 58)
                        {
                            int tempValue = m_listB3MathGamePlay.FirstOrDefault(l => l.PackageDesc == NewValue).MathPackageID;
                            sm.m_value = tempValue.ToString();
                        }
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
                    sm.m_gameID = gameID;
                    sm.m_value = (chkbx.IsChecked == true) ? "T" : "F";
                    string OldValue = GetOldValuesForComparison(sm.m_settingID);//m_B3SettingCrazyBt.Single(l => l.B3SettingID == sm.m_settingID).B3SettingValue;
                    sm.m_oldValue = OldValue;
                    if (sm.m_value != OldValue)
                    {
                        m_lB3Settings.Add(sm);
                    }
                }
            }


            else if (element is Grid)
            {
                foreach (UIElement element2 in griddenomJailBreak.Children)
                {

                    if (element2 is CheckBox)
                    {
                        SettingMember sm = new SettingMember();
                        CheckBox chkbx = (CheckBox)element2;
                        if (chkbx.Tag != null && chkbx != null)
                        {
                            sm.m_settingID = Convert.ToInt32(chkbx.Tag);
                            sm.m_gameID = gameID;

                            string NewValue = (chkbx.IsChecked == true) ? "T" : "F";
                            string OldValue = GetOldValuesForComparison(sm.m_settingID); //m_B3SettingCrazyBt.Single(l => l.B3SettingID == sm.m_settingID).B3SettingValue;
                            sm.m_value = NewValue;
                            sm.m_oldValue = OldValue;
                            if (sm.m_value != OldValue)
                            {
                                m_lB3Settings.Add(sm);
                            }
                        }
                    }
                }
            }
        }



        
        /// <summary>
        /// Compare old value to the new value.
        /// </summary>
        public List<SettingMember> ListOfSettingIDToBeUpdated(int gameID)
        {
            m_lB3Settings = new List<SettingMember>();
            foreach (UIElement element in gridGameSettingJailBreak.Children)
            {

                LoopThroughElementInAContainer(element, gameID);
                if (element is StackPanel)
                {
                    StackPanel stkpnl = (StackPanel)element;
                    foreach (UIElement elemStkPnl in stkpnl.Children)
                    {
                        LoopThroughElementInAContainer(elemStkPnl, gameID);
                    }
                }
            }

            if (m_lB3Settings.Exists(p => p.m_settingID == 58))
            {
                SetAllControlsToComplyWithPayTable(gameID);
            }

            return m_lB3Settings;

        }

        private void SetAllControlsToComplyWithPayTable(int gameID)//Set all checkboxes to true if its a pay table changes.
        {
            string NewValue;
            string OldValue;
            int TempSettingID;

            //Set denom all to false.
            foreach (UIElement element2 in griddenomJailBreak.Children)
            {

                if (element2 is CheckBox)
                {
                    SettingMember sm = new SettingMember();
                    CheckBox chkbx = (CheckBox)element2;
          
                    if (chkbx.Tag != null && chkbx != null)
                    {
                        TempSettingID = Convert.ToInt32(chkbx.Tag);
                        if (TempSettingID < 9)
                        {
                            chkbx.IsChecked = true;
                            sm.m_settingID = TempSettingID;
                            sm.m_gameID = gameID;

                            NewValue = (chkbx.IsChecked == true) ? "T" : "F";
                            OldValue = GetOldValuesForComparison(sm.m_settingID);
                            sm.m_value = NewValue;
                            sm.m_oldValue = OldValue;
                            if (sm.m_value != OldValue)
                            {
                                m_lB3Settings.Add(sm);
                            }
                        }
                    }
                }
            }

            //HideSeriaNumber
            chkbxHideSerialNumber.IsChecked = false;
            TempSettingID = Convert.ToInt32(chkbxHideSerialNumber.Tag);
            OldValue = GetOldValuesForComparison(TempSettingID);
            NewValue = (chkbxHideSerialNumber.IsChecked == true) ? "T" : "F";

            if (NewValue != OldValue)
            {
                SettingMember sm = new SettingMember();
                sm.m_settingID = TempSettingID;
                sm.m_gameID = gameID;
                sm.m_value = NewValue;
                sm.m_oldValue = OldValue;
                m_lB3Settings.Add(sm);

            }

            //SET Bet Level = 1
            cmbxMaxBetLevel.SelectedIndex = 0;
            TempSettingID = Convert.ToInt32(cmbxMaxBetLevel.Tag);
            OldValue = GetOldValuesForComparison(TempSettingID);
            NewValue = cmbxMaxBetLevel.SelectedItem.ToString();

            if (NewValue != OldValue)
            {
                SettingMember sm = new SettingMember();
                sm.m_settingID = TempSettingID;
                sm.m_gameID = gameID;
                sm.m_value = NewValue;
                sm.m_oldValue = OldValue;
                m_lB3Settings.Add(sm);
            }

            //SET Max Cards = 6
            cmbxMaxcards.SelectedIndex = 1;
            TempSettingID = Convert.ToInt32(cmbxMaxcards.Tag);
            OldValue = GetOldValuesForComparison(TempSettingID);
            NewValue = cmbxMaxcards.SelectedItem.ToString();

            if (NewValue != OldValue)
            {
                SettingMember sm = new SettingMember();
                sm.m_settingID = TempSettingID;
                sm.m_gameID = gameID;
                sm.m_value = NewValue;
                sm.m_oldValue = OldValue;
                m_lB3Settings.Add(sm);
            }
        }

        


        private string GetCallSpeedEquivValue(int callSpeedValue)
        {

            string tempCallSpeed = "";
            if (callSpeedValue == 100) { tempCallSpeed = "10"; }
            else if (callSpeedValue > 100 && callSpeedValue <= 590) { tempCallSpeed = "9"; }
            else if (callSpeedValue > 590 && callSpeedValue <= 1080) { tempCallSpeed = "8"; }
            else if (callSpeedValue > 1080 && callSpeedValue <= 1570) { tempCallSpeed = "7"; }
            else if (callSpeedValue > 1570 && callSpeedValue <= 2060) { tempCallSpeed = "6"; }
            else if (callSpeedValue > 2060 && callSpeedValue <= 2550) { tempCallSpeed = "5"; }
            else if (callSpeedValue > 2550 && callSpeedValue <= 3040) { tempCallSpeed = "4"; }
            else if (callSpeedValue > 3040 && callSpeedValue <= 3530) { tempCallSpeed = "3"; }
            else if (callSpeedValue > 3530 && callSpeedValue <= 4020) { tempCallSpeed = "2"; }
            else if (callSpeedValue == 5000) { tempCallSpeed = "1"; }
            return tempCallSpeed;
        }

        private string GetCallSpeedEquivToDB(int callspeedvalue)
        {
            string result = "";
            switch (callspeedvalue)
            {
                case 1: { result = "5000"; break; }
                case 2: { result = "4020"; break; }
                case 3: { result = "3530"; break; }
                case 4: { result = "3040"; break; }
                case 5: { result = "2550"; break; }
                case 6: { result = "2060"; break; }
                case 7: { result = "1570"; break; }
                case 8: { result = "1080"; break; }
                case 9: { result = "590"; break; }
                case 10: { result = "100"; break; }
            }
            return result;
        }

        private string GetOldValuesForComparison(int settingID)
        {
            string result = "";

            switch (settingID)
            {
                case 1: { result = (m_denom01 == true) ? "T" : "F"; break; }
                case 2: { result = (m_denom05 == true) ? "T" : "F"; break; }
                case 3: { result = (m_denom10 == true) ? "T" : "F"; break; }
                case 4: { result = (m_denom25 == true) ? "T" : "F"; break; }
                case 5: { result = (m_denom50 == true) ? "T" : "F"; break; }
                case 6: { result = (m_denom100 == true) ? "T" : "F"; break; }
                case 7: { result = (m_denom200 == true) ? "T" : "F"; break; }
                case 8: { result = (m_denom500 == true) ? "T" : "F"; break; }
                case 9: { result = m_maxBetLevel.ToString(); break; }
                case 10: { result = m_maxCards.ToString(); break; }
                case 11: { result = m_callSpeed.ToString(); break; }//Original value
                case 12: { result = (m_autoCall == true) ? "T" : "F"; break; }
                case 13: { result = (m_autoPlay == true) ? "T" : "F"; break; }
                case 14: { result = (m_hideSerialNumber == true) ? "T" : "F"; break; }
                case 58: { result = m_mathPackage.ToString(); break; }
                case 59: 
                    {
                        if (m_hasMinSpeed == true)
                        {
                            result = m_minCallSpeed.ToString();
                        } break;                 
                    }//Original value
                case 60:
                    {
                        if (m_hasCallSpeedBonus == true)
                        {
                            result = m_minCallSpeed.ToString();
                        }
                        break;
                    }
            }

            return result;
        }


        private List<string> LoadMaxBetLevel()
        {
            List<string> result = new List<string>();
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

        private List<string> LoadMaxCard()
        {
            List<string> result = new List<string>();

            result.Add("4");
            result.Add("6");

            return result;
        }

        #endregion

        #region EVENTS

        private void txtbxCallSpeedMin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
 
        #endregion
    }
}
