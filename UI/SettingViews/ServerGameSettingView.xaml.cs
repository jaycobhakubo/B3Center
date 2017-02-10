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
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews
{
    /// <summary>
    /// Interaction logic for ServerGameSettingView.xaml
    /// </summary>
    public partial class ServerGameSettingView : UserControl
    {
     public ServerGameSettingView(ServerSettingVm ServerSettingViewModel)
        {
            InitializeComponent();
            DataContext = ServerSettingViewModel;
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
                 int tempResultInt;
                 string tempResultString;

                 //Do not allow whitespace on password
                 tempResultString = txtGameRecallPassword.Text.ToString();
                 if (tempResultString.Contains(" "))
                 {
                     tempResult = false;
                     return;
                 }

                if (int.TryParse(txtMinPlayer.Text.ToString(), out tempResultInt))
                 {
                     if (int.TryParse(txtGameStartDelay.Text.ToString(), out tempResultInt))
                     {
                         if (int.TryParse(txtConsolationPrize.Text.ToString(), out tempResultInt))
                         {
                            if (int.TryParse(txtGameWaitCountDown.Text.ToString(), out tempResultInt))
                            {
                                tempResult = true;
                                var ii = SettingViewModel.Instance;
                                ii.ViewReportVisibility = true;
                            }                           
                         }
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

#region OLD (ref)

//private void txtbxNumericOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    Regex regex = new Regex("[^0-9]+");
        //    e.Handled = regex.IsMatch(e.Text);
        //}

        //private void txtbx_PreviewKeyDown(object sender, KeyEventArgs e)
        //{
        //    bool notAllow = false;

        //    if (e.Key == Key.Space)
        //    {
        //        notAllow = true;
        //    }
        //    else
        //        if (e.Key == Key.Back)
        //        {
        //            notAllow = false;
        //        }
        //    e.Handled = notAllow;
        //}

        //private void txtbxPrice_PreviewTextInput2(object sender, TextCompositionEventArgs e)
        //{
        //    bool NotAllow = false;

        //    //Get all the text in the textbox including keypreview.
        //    TextBox y = (TextBox)sender;
        //    string x = y.Text;
        //    x = x.Insert(y.SelectionStart, e.Text);

        //    int count = x.Split('.').Length - 1;//Count how many decimal places on the text input

        //    if (count > 1)//One decimal point only.
        //    {
        //        NotAllow = true;
        //    }
        //    else if ((Convert.ToChar(e.Text)) == '.')
        //    {
        //        NotAllow = false;
        //    }
        //    else if (Char.IsNumber(Convert.ToChar(e.Text)))
        //    {
        //        NotAllow = false;

        //        if (Regex.IsMatch(x, @"\.\d\d\d"))//Only allow .## 
        //        {
        //            NotAllow = true;
        //        }
        //    }
        //    else
        //    {
        //        NotAllow = true;
        //    }

        //    e.Handled = NotAllow;
        //}

        //private void txtbxMinPlayers_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    TextBox txtbx = (TextBox)sender;
        //    int tempMinPlayer;
        //    tempMinPlayer = Convert.ToInt32(txtbx.Text);
        //    if (tempMinPlayer < 2)
        //    {
        //        txtbxMinPlayers.Text = "2";
        //    }
        //}

#endregion