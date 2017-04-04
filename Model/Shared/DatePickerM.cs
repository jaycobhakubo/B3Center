using System;

namespace GameTech.Elite.Client.Modules.B3Center.Model.Shared
{
    public class DatePickerM
    {
        public DateTime DateFull { get; set; }
        public string DateMonthWord  {get;set;}
        public string DateYearInt  {get;set;}
        public string DateDayInt { get; set; }
        public string DateTimestring { get; set; }
        public string DateAmpm { get; set; }

        public string DateFullwTime
        {
            get
            {
                return DateMonthWord + " " + DateDayInt + " " + DateYearInt + " " + DateTimestring + " " + DateAmpm;
            }
        }
    }
}
