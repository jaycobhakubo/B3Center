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
    /// Interaction logic for SessionSettingView.xaml
    /// </summary>
    public partial class SessionSettingView : UserControl
    {
        #region MEMBER VARIABLEs

        private List<B3SettingGlobal> m_B3Settings = new List<B3SettingGlobal>();
        private decimal m_payoutLimit;
        private decimal m_jackpotLimit;
        private bool m_enforceMix;
        private readonly SaveCancelCrtl m_saveCancelCtrl;
        private List<SettingMember> m_lB3Settings;
        private Button m_btnSave;
        private Button m_btnCancel;
        private bool m_isValidationOk;

        #endregion

        #region CONSTRUCTORs

        public SessionSettingView(List<B3SettingGlobal> B3Settings)
        {
            InitializeComponent();
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
        }

        #endregion

        #region PRIVATE METHODs

        private void PopulateDataIntoVar()
        {
            bool result;
            m_payoutLimit = Convert.ToDecimal(m_B3Settings[0].B3SettingValue);
            m_jackpotLimit = Convert.ToDecimal(m_B3Settings[1].B3SettingValue);
            m_enforceMix = Convert.ToBoolean(result = (m_B3Settings[2].B3SettingValue == "T") ? true : false);
        }


        private void PopulateDataIntoControls()
        {
            txtbxPayoutLimit.Text = ConvertToString.decimal_(m_payoutLimit.ToString());
            txtbxJackpotLimit.Text = ConvertToString.decimal_(m_jackpotLimit.ToString());
            chkbxEnforeMix.IsChecked = m_enforceMix;
        }

        private string GetOldValuesForComparison(int settingID)
        {
            string result = "";

            switch (settingID)
            {
                case 39: { result =  m_payoutLimit.ToString(); break; }
                case 40: { result =  m_jackpotLimit.ToString(); break; }
                case 41: { result = (m_enforceMix == true) ? "T" : "F"; break; }
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
                    case 39:
                        {
                            m_payoutLimit = Convert.ToDecimal(sm.m_value);
                            break;
                        }
                    case 40:
                        {
                            m_jackpotLimit = Convert.ToDecimal(sm.m_value);
                            break;
                        }
                    case 41:
                        {
                            m_enforceMix = Convert.ToBoolean(result = (sm.m_value == "T") ? true : false);
                            break;
                        }
                }
            }
            PopulateDataIntoControls();
        }

        private bool ValidateUserInput()
        {
            bool result = true; //True = validation ok ; false validation not ok.
            decimal decValue;

            if (string.IsNullOrEmpty(txtbxPayoutLimit.Text) == true)
            {
                result = false;
                txtbxPayoutLimit.BorderBrush = Brushes.Red;
            }
            else
                if (!Decimal.TryParse(txtbxPayoutLimit.Text, out decValue))//If not valid decimal.
                {
                    result = false;
                    txtbxPayoutLimit.BorderBrush = Brushes.Red;
                }


            if (string.IsNullOrEmpty(txtbxJackpotLimit.Text) == true)
            {
                result = false;
                txtbxJackpotLimit.BorderBrush = Brushes.Red;
            }
            else
                if (!Decimal.TryParse(txtbxJackpotLimit.Text, out decValue))//If not valid decimal.
                {
                    result = false;
                    txtbxJackpotLimit.BorderBrush = Brushes.Red;
                }



            if (@result == false)
            {
                m_isValidationOk = false;
            }
            return result;
        }

       
        private void SetBorderBrushToDefault()
        {
            //txtbxPayoutLimit.BorderBrush = Brushes.LightGray;
            //txtbxJackpotLimit.BorderBrush = Brushes.LightGray;         
        }

        /// <summary>
        /// Compare old value to the new value.
        /// </summary>
        private List<SettingMember> ListOfSettingIDToBeUpdated()
        {
            m_lB3Settings = new List<SettingMember>();
            foreach (UIElement element in  stkpnlSessionSetting.Children)
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
            }
            return m_lB3Settings;
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

        #region EVENTs

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
                //lblSavedNotification.Visibility = Visibility.Visible;
            }
        }

        private void Grid_GotFocus(object sender, RoutedEventArgs e)
        {
            ClearSavedNotification();
            if (m_isValidationOk == false)
            {
                SetBorderBrushToDefault();
                m_isValidationOk = true;
            }
        }

        void m_btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReloadDataIntoControls();
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

        #endregion
    }
}
