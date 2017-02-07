

using System.Windows;
using System.Windows.Controls;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings;


namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews
{
    /// <summary>
    /// Interaction logic for SystemSettingView.xaml
    /// </summary>
    public partial class SystemSettingView : UserControl
    {
        public SystemSettingView(SystemSettingVm _systemSetting)
        {
            InitializeComponent();
            DataContext = _systemSetting;
        }
    }
}
