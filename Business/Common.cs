#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System;
using System.Collections.Generic;

namespace GameTech.Elite.Client.Modules.B3Center.Business
{
    public enum Month
    {
        NotSet = 0,
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    public static class Helpers
    {
        private static readonly Random m_rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = m_rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

                /// <summary>
        /// Converts boolean to a string value of "T" or "F"
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static string ConvertToB3StringValue(this bool value)
        {
            return value ? "T" : "F";
        }

        /// <summary>
        /// Converts the b3 string value to bool.
        /// </summary>
        /// <param name="b3Setting">The b3 setting.</param>
        /// <returns></returns>
        public static bool ConvertB3StringValueToBool(this B3SettingGlobal b3Setting)
        {
            return b3Setting.B3SettingValue == "T";
        }
    }

}
