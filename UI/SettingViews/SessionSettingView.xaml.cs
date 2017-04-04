using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews
{
    /// <summary>
    /// Interaction logic for SessionSettingView.xaml
    /// </summary>
    public partial class SessionSettingView : UserControl
    {
        public SessionSettingView(SessionSettingVm sessionSettingVm)
        {
            InitializeComponent();
            DataContext = sessionSettingVm;
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
                var tempResult = false;
                long tempResultInt64;
      
                if (long.TryParse(TxtbxPayoutLimit.Text, out tempResultInt64))
                {
                    if (long.TryParse( TxtbxJackpotlimit.Text, out tempResultInt64))
                    {                        
                        tempResult = true;
                        var ii = SettingViewModel.Instance;
                        ii.BtnSaveIsEnabled = true;                                                  
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
