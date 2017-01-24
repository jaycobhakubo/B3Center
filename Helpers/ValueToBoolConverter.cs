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
            bool tempresult = true;
            switch (value.ToString())
            {
                case "T":
                    {
                        tempresult = true;
                        break;
                    }
                case "t":
                    {
                        tempresult = true;
                        break;
                    }

                case "F":
                    {
                        tempresult = false;
                        break;
                    }
                case "f":
                    {
                        tempresult = false;
                        break;
                    }
            }
            return tempresult;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
        #endregion

    }
}
