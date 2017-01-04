#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;
//Hello there
namespace GameTech.Elite.Client.Modules.B3Center.Helper
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

   
}
