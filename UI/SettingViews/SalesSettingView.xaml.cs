using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews
{
    /// <summary>
    /// Interaction logic for SalesSettingView.xaml
    /// </summary>
    public partial class SalesSettingView
    {
 
        public SalesSettingView(SalesSettingVm sessionSettingVm)
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
                int tempResultInt;

                if (int.TryParse(TxtbxLogRecycleDays.Text, out tempResultInt))
                {                
                    tempResult = true;
                    var ii = SettingViewModel.Instance;
                    ii.BtnSaveIsEnabled = true;                                                                 
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
            var notAllow = false;

            switch (e.Key)
            {
                case Key.Space:
                    notAllow = true;
                    break;
                case Key.Back:
                    break;
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