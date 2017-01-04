using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace GameTech.Elite.Client.Modules.B3Center.Helpers
{
    public class ValueToBoolConverter : IValueConverter
    {
        #region Constructors
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "Games":
                    return 0;
                default:
                    return 2;
            }
     
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int)
            {
                if ((int)value == 0)
                    return "Games";
                else
                    return "";
            }
            return "";

        }
        #endregion
    }
}
