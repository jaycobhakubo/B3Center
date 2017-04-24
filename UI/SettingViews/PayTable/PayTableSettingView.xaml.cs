﻿using GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings;
using System.Windows.Controls;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews
{
    /// <summary>
    /// Interaction logic for PayTableSettingView.xaml
    /// </summary>
    public partial class PayTableSettingView
    {
        public PayTableSettingView(PayTableSettingVm vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void rdobtnIsRNG_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            RadioButton rdobtn = (RadioButton)sender;
         
                PayTableSettingVm ii = (PayTableSettingVm)DataContext;
            ii.EnforceMixEnable = (bool)rdobtn.IsChecked;



        }
    }
}
