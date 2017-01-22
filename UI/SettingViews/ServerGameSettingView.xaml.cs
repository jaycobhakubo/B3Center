using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Messages;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews
{
    /// <summary>
    /// Interaction logic for ServerGameSettingView.xaml
    /// </summary>
    public partial class ServerGameSettingView : UserControl
    {
     public ServerGameSettingView(ServerSettingVm ServerSettingViewModel)
        {
            InitializeComponent();
            DataContext = ServerSettingViewModel;
        }

        //TEST
        //private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    var x = (TextBox)sender;
        //    var y = x.Text;
        //}
    }
}
