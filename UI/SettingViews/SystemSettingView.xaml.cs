

using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings;


namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews
{
    /// <summary>
    /// Interaction logic for SystemSettingView.xaml
    /// </summary>
    public partial class SystemSettingView : UserControl
    {
        public SystemSettingView(SystemSettingVm _systemSetting)
        {
            InitializeComponent();
            DataContext = _systemSetting;
        }


        private void ValidateUserInput(object sender, TextChangedEventArgs e)
        {
            TextBox currentTextBox = (TextBox)sender;

            if (string.IsNullOrEmpty(currentTextBox.Text))
            {
                var ii = SettingViewModel.Instance;
                ii.BtnSaveIsEnabled = false;
            }
            else//If its not empty then lets validate all values.
            {
                var tempResult = true;
                int tempResultInt;
                Int64 tempResultInt64;
                string tempResultString;


                tempResultString = txtbxSiteName.Text.ToString();//Do not allow whitespace on password
                if (txtbxSiteName.Text.ToString().Contains(" "))
                {
                    if (txtbxMagCardStart.Text.ToString().Contains(" "))
                    {
                        if (txtbxMagCardStart.Text.ToString().Contains(" "))
                        {
                            tempResult = false;
                        }   
                    }                                   
                }

                if (tempResult == true)
                {
                    var ii = SettingViewModel.Instance;
                    ii.BtnSaveIsEnabled = false;
                }


                if (Int64.TryParse(txtbxHandPayoutTrigger.Text.ToString(), out tempResultInt64))
                {
                    if (Int64.TryParse(txtbxVipPointPlayer.Text.ToString(), out tempResultInt64))
                    {
                        if (Int32.TryParse(txtbxPlayerpinLength.Text.ToString(), out tempResultInt))
                        {
                            if (Int32.TryParse(txtbxRNGBallCallTime.Text.ToString(), out tempResultInt))
                            {
                                if (Int32.TryParse(txtbxPlayerpinLength.Text.ToString(), out tempResultInt))
                                {
                                    tempResult = true;
                                    var ii = SettingViewModel.Instance;
                                    ii.BtnSaveIsEnabled = true;
                                }
                            }
                        }
                    }
                }

                if (tempResult == false)
                {
                    var ii = SettingViewModel.Instance;
                    ii.BtnSaveIsEnabled = false;
                }
            }
        }

        private void DontAllowThisKeyboardinput(object sender, KeyEventArgs e)
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


        private void _PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }       
    }

}
