

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
            FixLabelOrTextBlockContent();
            DataContext = _systemSetting;

        }

        //Im too lazy too type repeatable words. So I am going to do this.
        //Run once and never again.
        private void FixLabelOrTextBlockContent()
        {
            foreach (UIElement element in gridSystemSettings.Children)
            {
                if (element is Label)
                {
                    var x = (Label)element;
                    x.Content = "*" + x.Content + ":";
                }



            }
        }
    }
}
