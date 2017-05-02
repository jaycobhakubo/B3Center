using GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings;
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

        private void rdobtn_RNGOr55455_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            PayTableSettingVm vm = (PayTableSettingVm)DataContext;
            vm.IsRngCheckEvent();
        }       
    }
}
