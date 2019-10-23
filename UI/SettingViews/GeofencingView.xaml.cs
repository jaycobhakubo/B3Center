using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews
{
    /// <summary>
    /// Interaction logic for GeofencingView.xaml
    /// </summary>
    public partial class GeofencingView : UserControl
    {
        public GeofencingView(GeofencingVm geofencingViewModel)
        {
            InitializeComponent();
            DataContext = geofencingViewModel;
        }
    }
}
