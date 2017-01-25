using GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace GameTech.Elite.Client.Modules.B3Center.Model.Setting
{
   public class MainGameSetting
    {
        public int GameID { get; set; }
        public string Header { get; set; }
        public GameSettingVmAllGame GameVm { get; set; }
        public UserControl GameUserControl { get; set; }

    }
}
