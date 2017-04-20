using GameTech.Elite.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
   public class PayTableSettingVm : ViewModelBase
    {
        public PayTableSettingVm()
        {
            var grdlength = new GridLength(1, GridUnitType.Star);
            grdColumnB3GameName = grdlength;
            RaisePropertyChanged("grdColumnB3GameName");
            grdlength = new GridLength(3, GridUnitType.Star);
            grdColumnPayTableSetting = grdlength;
            RaisePropertyChanged("grdColumnPayTableSetting");
        }


        public GridLength grdColumnB3GameName { get; set; }
        public GridLength grdColumnPayTableSetting { get; set; }
    }
}
