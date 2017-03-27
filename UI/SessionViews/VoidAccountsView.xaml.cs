using GameTech.Elite.Client.Modules.B3Center.ViewModels;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SessionViews
{
    /// <summary>
    /// Interaction logic for VoidAccountsView.xaml
    /// </summary>
    public partial class VoidAccountsView
    {
        public VoidAccountsView()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }
    }
}
