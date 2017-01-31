using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using GameTech.Elite.Client.Modules.B3Center.Business;
using System.Linq;
using System.Collections.ObjectModel;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews
{

    public partial class SettingView 
    {
        public SettingView()
        {
            InitializeComponent();          
        }

        private void lstbx_SettingList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SettingViewModel ii = (SettingViewModel)DataContext;
            ii.SelectedItemEvent();
        }      
    }
}

