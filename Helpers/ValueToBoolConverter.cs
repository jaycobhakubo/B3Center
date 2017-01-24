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


    public class ValueDBToValueAppCallSpeed : IValueConverter
    {
        #region Constructors
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int callSpeedValue;
         
            if (System.Int32.TryParse(value.ToString(), out callSpeedValue))
            {
                string tempCallSpeed = "";         
                if (callSpeedValue == 100) { tempCallSpeed = "10"; }
                else if (callSpeedValue > 100 && callSpeedValue <= 590) { tempCallSpeed = "9"; }
                else if (callSpeedValue > 590 && callSpeedValue <= 1080) { tempCallSpeed = "8"; }
                else if (callSpeedValue > 1080 && callSpeedValue <= 1570) { tempCallSpeed = "7"; }
                else if (callSpeedValue > 1570 && callSpeedValue <= 2060) { tempCallSpeed = "6"; }
                else if (callSpeedValue > 2060 && callSpeedValue <= 2550) { tempCallSpeed = "5"; }
                else if (callSpeedValue > 2550 && callSpeedValue <= 3040) { tempCallSpeed = "4"; }
                else if (callSpeedValue > 3040 && callSpeedValue <= 3530) { tempCallSpeed = "3"; }
                else if (callSpeedValue > 3530 && callSpeedValue <= 4020) { tempCallSpeed = "2"; }
                else if (callSpeedValue == 5000) { tempCallSpeed = "1"; }
                return tempCallSpeed.ToString();
            }
            else
            {
                return null;
            }

          
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
        #endregion

    }
}
