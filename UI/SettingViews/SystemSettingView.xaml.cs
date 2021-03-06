﻿using System.Text.RegularExpressions;
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
        public SystemSettingView(SystemSettingVm systemSetting)
        {
            InitializeComponent();
            DataContext = systemSetting;
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
                long tempResultInt64;
                string tempResultString;


                tempResultString = TxtbxSiteName.Text;//Do not allow whitespace on password
                if (TxtbxSiteName.Text.Contains(" "))
                {
                    if (TxtbxMagCardStart.Text.Contains(" "))
                    {
                        if (TxtbxMagCardStart.Text.Contains(" "))
                        {
                            tempResult = false;
                        }   
                    }                                   
                }

                if (tempResult)
                {
                    var ii = SettingViewModel.Instance;
                    ii.BtnSaveIsEnabled = false;
                }


                if (long.TryParse(TxtbxHandPayoutTrigger.Text, out tempResultInt64))
                {
                    if (long.TryParse(TxtbxVipPointPlayer.Text, out tempResultInt64))
                    {
                        if (int.TryParse(TxtbxPlayerpinLength.Text, out tempResultInt))
                        {
                            if (int.TryParse(TxtbxRngBallCallTime.Text, out tempResultInt))
                            {
                                if (int.TryParse(TxtbxPlayerpinLength.Text, out tempResultInt))
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
