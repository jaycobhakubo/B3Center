using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameTech.Elite.Client.Modules.B3Center.Model.Shared
{
    public class DatePickerM
    {
        public DateTime DateFull
        {
            get;
            set;
        }

        public string DateFullwTime
        {
            get
            {

                return (DateMonthWord.ToString() + " " + DateDayInt.ToString() + " " + DateYearInt.ToString() + " " + DateTimestring.ToString() + " " + dateAMPM.ToString());
            }
            //set;
        }

        public string DateMonthWord
        {
            get;
            set;
        }

        public string DateYearInt
        {
            get;
            set;
        }

        public string DateDayInt
        {
            get;
            set;
        }

        public string DateTimestring
        {
            get;
            set;
        }

        public string dateAMPM
        {
            get;
            set;
        }

    }
}
