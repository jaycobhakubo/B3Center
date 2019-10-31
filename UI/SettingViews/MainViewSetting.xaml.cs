using System.Windows.Controls;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;

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
            SettingViewModel vm = (SettingViewModel)DataContext;
            vm.SelectedItemEvent();
        }

        //Removed our saved message status indicator.
        private void UserControl_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            SettingViewModel vm = (SettingViewModel)DataContext;
            if (vm.SaveSuccess != false) vm.SaveSuccess = false;
        }      
    }
}

