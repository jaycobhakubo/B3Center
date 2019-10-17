#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

//Hello there
namespace GameTech.Elite.Client.Modules.B3Center.Helpers
{

    public class MultiplyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 1.0;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] is double)
                    result *= (double)values[i];
            }
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new Exception("Not implemented");
        }
    }

    public class BoolToVisibilityConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)//parameter true = revert valuek false do not revert
        {
            Visibility showControl = new Visibility();
            showControl = Visibility.Collapsed;

            if (value is bool)
            {
                if ((bool)value)
                {
                    showControl = Visibility.Visible;
                }

                if (parameter != null)
                {
                    var isReverse = parameter.ToString();
                    if (isReverse == "true")
                    {
                        if (showControl == Visibility.Visible)
                        {
                            showControl = Visibility.Collapsed;
                        }
                        else
                        {
                            showControl = Visibility.Visible;
                        }
                    }
                }
            }
            return showControl;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }


    public class BoolToVisibilityConvHidden : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)//parameter true = revert valuek false do not revert
        {
            Visibility showControl = new Visibility();
            showControl = Visibility.Hidden;

            if (value is bool)
            {
                if ((bool)value)
                {
                    showControl = Visibility.Visible;
                }
            }
            return showControl;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class ValueToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string tempresult = "";
            var tempResult = (bool)value;
            switch (tempResult)
            {
                case true:
                    {
                        tempresult = "T";
                        break;
                    }
                case false:
                    {
                        tempresult = "F";
                        break;
                    }
            }
            return tempresult;
        }
    }



    public class ReverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)//parameter true = revert valuek false do not revert
        {
            bool tempResult = false;
            if (value is bool)
            {
                tempResult = !(bool)value;
            }
            return tempResult;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class ValueDbToValueAppCallSpeed : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int callSpeedValue;

            if (value != null && int.TryParse(value.ToString(), out callSpeedValue))
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
                return tempCallSpeed;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int callspeedValue;
            var x = int.TryParse(value.ToString(), out callspeedValue);

            string result = "";
            switch (callspeedValue)
            {
                case 1: { result = "5000"; break; }
                case 2: { result = "4020"; break; }
                case 3: { result = "3530"; break; }
                case 4: { result = "3040"; break; }
                case 5: { result = "2550"; break; }
                case 6: { result = "2060"; break; }
                case 7: { result = "1570"; break; }
                case 8: { result = "1080"; break; }
                case 9: { result = "590"; break; }
                case 10: { result = "100"; break; }
            }
            return result;
        }
    }

    public class ShowDefaultColumnDef : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GridLength columnDefWidthValue = new GridLength();
            columnDefWidthValue = (GridLength)value;
            var gridStarType = new GridLength(1, GridUnitType.Star);

            if (columnDefWidthValue == gridStarType)
            {
                return new GridLength(0, GridUnitType.Auto);
            }
            return new GridLength(1, GridUnitType.Star);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class ValueToBoolForEmptyString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = true;
            if (value is string)
            {
                var tempResult = value.ToString();
                if (tempResult.Count() == 0)
                {
                    result = false;
                }
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
