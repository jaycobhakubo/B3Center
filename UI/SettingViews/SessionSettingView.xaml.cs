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
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews
{
    /// <summary>
    /// Interaction logic for SessionSettingView.xaml
    /// </summary>
    public partial class SessionSettingView : UserControl
    {
        public SessionSettingView(SessionSettingVm SessionSettingVm)
        {
            InitializeComponent();
            DataContext = SessionSettingVm;
        }

        private void ValidateUserInput(object sender, TextChangedEventArgs e)
        {
            TextBox currentTextBox = (TextBox)sender;

            if (string.IsNullOrEmpty(currentTextBox.Text))
            {
                var ii = SettingViewModel.Instance;
                ii.ViewReportVisibility = false;
            }
            else//If its not empty then lets validate all values.
            {
                var tempResult = false;
                Int64 tempResultInt64;
      
                if (Int64.TryParse(txtbxPayoutLimit.Text.ToString(), out tempResultInt64))
                {
                    if (Int64.TryParse( txtbxJackpotlimit.Text.ToString(), out tempResultInt64))
                    {                        
                        tempResult = true;
                        var ii = SettingViewModel.Instance;
                        ii.ViewReportVisibility = true;                                                  
                    }
                }

                if (tempResult == false)
                {
                    var ii = SettingViewModel.Instance;
                    ii.ViewReportVisibility = false;
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
