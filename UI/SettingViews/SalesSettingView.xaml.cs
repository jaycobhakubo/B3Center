using System.Windows.Controls;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews
{
    /// <summary>
    /// Interaction logic for SalesSettingView.xaml
    /// </summary>
    public partial class SalesSettingView : UserControl
    {
 
        public SalesSettingView(SalesSettingVm SessionSettingVm)
        {
            InitializeComponent();
            DataContext = SessionSettingVm;
        }
    }
}