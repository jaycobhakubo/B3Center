using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Model.Shared;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Shared
{
    public class DatePickerVm : ViewModelBase
    {
        public DatePickerVm()
        {

        }

        private DatePickerM m_datepicker;
        public DatePickerM datepicker
        {
            get {return m_datepicker; }
            set { m_datepicker = value;
            RaisePropertyChanged("datepicker");
            }
        }

    }
}
