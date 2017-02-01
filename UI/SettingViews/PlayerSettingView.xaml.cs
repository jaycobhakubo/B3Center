using System.Windows.Controls;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews
{
    /// <summary>
    /// Interaction logic for PlayerSettingView.xaml
    /// </summary>
    public partial class PlayerSettingView : UserControl
    {
        public PlayerSettingView(PlayerSettingVm B3PlayerSettingvm)
        {
            InitializeComponent();
            DataContext = B3PlayerSettingvm;
        }   
    }
}
