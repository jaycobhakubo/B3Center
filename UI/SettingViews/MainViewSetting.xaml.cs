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
            SettingViewModel ii = (SettingViewModel)DataContext;
            ii.SelectedItemEvent();
        }      
    }
}

