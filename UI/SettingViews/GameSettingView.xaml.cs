using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading.Tasks;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Messages;
using GameTech.Elite.Client.Modules.B3Center.Properties;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews
{

    public partial class GameSettingView : UserControl
    {
        
        public GameSettingView(GameSettingVm _gamesetting)
        {
            InitializeComponent();   
            DataContext = _gamesetting;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GameSettingVm ii = (GameSettingVm)DataContext;
            ii.SelectedItemEvent();
        }
    }
  
}
