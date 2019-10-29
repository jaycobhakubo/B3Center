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
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using System.Text.RegularExpressions;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews
{
    /// <summary>
    /// Interaction logic for GeofencingView.xaml
    /// </summary>
    public partial class GeofencingView : UserControl
    {
        public GeofencingView(GeofencingVm geofencingViewModel)
        {
            InitializeComponent();
            DataContext = geofencingViewModel;
        }

        /*
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

                //if (long.TryParse(TxtbxPayoutLimit.Text, out tempResultInt64))
                //{
                //    if (long.TryParse(TxtbxJackpotlimit.Text, out tempResultInt64))
                //    {
                //        tempResult = true;
                //        var ii = SettingViewModel.Instance;
                //        ii.BtnSaveIsEnabled = true;
                //    }
                //}

                if (tempResult == false)
                {
                    var ii = SettingViewModel.Instance;
                    ii.BtnSaveIsEnabled = false;
                }
            }
        }
         * */

        private void DontAllowThisKeyboardinput(object sender, KeyEventArgs e)
        {
            //bool notAllow = false;

            //if (e.Key == Key.Space)
            //{
            //    notAllow = true;
            //}
            //else
            //    if (e.Key == Key.Back)
            //    {
            //        notAllow = false;
            //    }
            //e.Handled = notAllow;
        }

        private void _PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //bool NotAllow = false;

            ////Get all the text in the textbox including keypreview.
            //TextBox y = (TextBox)sender;
            //string x = y.Text;
            //x = x.Insert(y.SelectionStart, e.Text);

            //int count = x.Split('.').Length - 1;//Count how many decimal places on the text input

            //if (count > 1)//One decimal point only.
            //{
            //    NotAllow = true;
            //}
            //else if ((Convert.ToChar(e.Text)) == '.')
            //{
            //    NotAllow = false;
            //}
            //else if (Char.IsNumber(Convert.ToChar(e.Text)))
            //{
            //    NotAllow = false;

            //    (Regex.IsMatch(x, @"\d{0,2}.?\d{1,2}"))//Only allow .## 
            //   // if (Regex.IsMatch(x, @"\d{0,2}.?\d{1,2}"))//Only allow .## 
            //  // if (Regex.IsMatch(x, @"[0-9]{1,11}(?:\.[0-9]{1,3})?$"))
            //    {
            //        NotAllow = true;
            //    }
            //}
            //else
            //{
            //    NotAllow = true;
            //}

            //e.Handled = NotAllow;
        }
    }
}
