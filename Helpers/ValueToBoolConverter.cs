using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace GameTech.Elite.Client.Modules.B3Center.Helpers
{
    public class BallCallBySessionEnable : IMultiValueConverter
    {

        //public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)

        public object Convert(object[] values, Type targetType,
               object parameter, System.Globalization.CultureInfo culture)
        {
            var IsBallCallRpt = (bool)values[0];
            var IsBallCallByGame = (bool)values[0];

            Visibility result = Visibility.Visible;

            if (IsBallCallRpt == true)
            {

                if (IsBallCallByGame == true)
                {
                    result = Visibility.Collapsed;
                }
                else
                {
                    result = Visibility.Visible;
                }
            }
            else
            {
                result = Visibility.Visible;
            }

        
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes,
                object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();

        }
    }

    public class BallCallByGameEnable : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool tempResult = false;
            switch (value.ToString().ToLower())
            {
                case "By Game":
                    {
                        return Visibility.Visible;
                    }
                case "By Session":
                    return Visibility.Collapsed;
            }
            return tempResult;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();

        }
    }
}
