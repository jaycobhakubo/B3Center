using System.Windows.Controls;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews
{

    public partial class GameSettingView
    {
        
        public GameSettingView(GameSettingVm gamesetting)
        {
            InitializeComponent();   
            DataContext = gamesetting;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var x = (TabControl)sender;
            int tabindex = x.SelectedIndex;
            GameSettingVm ii = (GameSettingVm)DataContext;
            if (tabindex != ii.Myprevindex)
            {
                ii.SelectedItemEvent();
            }
        }
    }
  
}
