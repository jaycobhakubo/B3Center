#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

        //No ref to db.

        internal static string GetVolumeEquivValue(int volume)
        {
            string tempValue = "";
            if (volume <= 100 && volume >= 91) { tempValue = "10"; }
            else if (volume < 91 && volume >= 81) { tempValue = "9"; }
            else if (volume < 81 && volume >= 71) { tempValue = "8"; }
            else if (volume < 71 && volume >= 61) { tempValue = "7"; }
            else if (volume < 61 && volume >= 51) { tempValue = "6"; }
            else if (volume < 51 && volume >= 41) { tempValue = "5"; }
            else if (volume < 41 && volume >= 31) { tempValue = "4"; }
            else if (volume < 31 && volume >= 21) { tempValue = "3"; }
            else if (volume < 21 && volume >= 11) { tempValue = "2"; }
            else if (volume < 11 && volume >= 1) { tempValue = "1"; }
            else if (volume == 0) { tempValue = "0"; }
            return tempValue;
        }

        internal static string GetVolumeEquivToDb(int volumeLevel)
        {
            //volume values are 0-100 for database
            var level = volumeLevel * 10;

            //if below 0 return zero
            if (level < 0)
            {
                return 0.ToString();
            }

            //if above 100 then return 100
            if (level > 100)
            {
                return 100.ToString();
            }

            return level.ToString();
        }

        //static
        public static readonly List<string> ZeroToTenList = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

        public static readonly List<string> OneToTenList = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

        public static readonly List<string> MaxCardCountList = new List<string> { "4", "6" };

        public static void Sort<T>(this ObservableCollection<T> collection)
        {
            var sorted = collection.OrderBy(x => x).ToList();
            int index = 0;

            while (index < sorted.Count)
            {
                if (!collection[index].Equals(sorted[index]))
                {
                    T t = collection[index];
                    collection.Move(index,sorted.IndexOf(t));
                }
                else
                {
                    index++;
                }
            }
        }
    }

}
