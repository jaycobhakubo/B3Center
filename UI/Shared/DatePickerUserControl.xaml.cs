#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GameTech.Elite.Client.Modules.B3Center.UI.Shared
{
    /// <summary>
    ///     Interaction logic for DateTimePickerUserControl.xaml
    /// </summary>
    public partial class DatePickerUserControl
    {
        #region local variables

        private bool m_showTime;
        private readonly string[] m_amPm = { "AM", "PM" };

        private readonly string[] m_hours =
        {
            "12:00", "1:00", "2:00", "3:00", "4:00", "5:00", "6:00", "7:00", "8:00", "9:00",
            "10:00", "11:00"
        };

        private readonly string[] m_months =
        {
            "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct",
            "Nov", "Dec"
        };

        private readonly int[] m_thirtyDayMonth =
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
            21, 22, 23, 24, 25, 26, 27, 28, 29, 30
        };

        private readonly int[] m_thirtyOneDayMonth =
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
            20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31
        };

        private readonly int[] m_twentyEightDayMonth =
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18,
            19, 20, 21, 22, 23, 24, 25, 26, 27, 28
        };

        private readonly int[] m_twentyNineDayMonth =
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
            20, 21, 22, 23, 24, 25, 26, 27, 28, 29
        };

        #endregion

        #region constructor

      
        /// <summary>
        /// Initializes a new instance of the <see cref="DatePickerUserControl"/> class.
        /// </summary>
        public DatePickerUserControl()
        {
            InitializeComponent();

            var years = new List<int>();
            for (var i = DateTime.Now.Year; i > DateTime.Now.Year - 50; i--)
            {
                years.Add(i);
            }

            MonthCombobox.ItemsSource = m_months;
            DayCombobox.ItemsSource = m_thirtyOneDayMonth;
            YearCombobox.ItemsSource = years;
            HourCombobox.ItemsSource = m_hours;
            AmPmCombobox.ItemsSource = m_amPm;

            YearCombobox.SelectedItem = years.FirstOrDefault();
            MonthCombobox.SelectedIndex = DateTime.Now.Month - 1;
            DayCombobox.SelectedIndex = DateTime.Now.Day - 1;

            var hour = DateTime.Now.Hour;
            AmPmCombobox.SelectedIndex = hour > 11 ? 1 : 0;

            HourCombobox.SelectedIndex = hour % 12;   
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when [date time changed event].
        /// </summary>
        public event EventHandler<EventArgs> DateTimeChangedEvent;  

        #endregion

        #region

        /// <summary>
        /// Gets or sets a value indicating whether to show the time comboboxes.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show time]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowTime
        {
            get
            {
                return m_showTime;
            }
            set
            {
                m_showTime = value;
                TimeGridRow.Height = m_showTime ? DateGridRow.Height : new GridLength(0);
                SpaceGridRow.Height = m_showTime ? new GridLength(4) : new GridLength(0);
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the SelectionChanged event of the MonthYearCombobox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void MonthYearCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MonthCombobox.SelectedValue == null || YearCombobox.SelectedValue == null)
            {
                return;
            }

            switch (MonthCombobox.SelectedValue.ToString())
            {
                case "Feb":

                    //leap year logic:
                    //The year is evenly divisible by 4;
                    //If the year can be evenly divided by 100, it is NOT a leap year, unless;
                    //The year is also evenly divisible by 400. Then it is a leap year.
                    int year;
                    if (int.TryParse(YearCombobox.SelectedValue.ToString(), out year))
                    {
                        if (year % 4 == 0)
                        {
                            if (year % 100 == 0)
                            {
                                DayCombobox.ItemsSource = year % 400 == 0 ? m_twentyNineDayMonth : m_twentyEightDayMonth;
                                break;
                            }

                            DayCombobox.ItemsSource = m_twentyNineDayMonth;
                            break;
                        }
                    }

                    DayCombobox.ItemsSource = m_twentyEightDayMonth;

                    break;

                case "Jan":
                case "Mar":
                case "May":
                case "Jul":
                case "Aug":
                case "Oct":
                case "Dec":
                    DayCombobox.ItemsSource = m_thirtyOneDayMonth;
                    break;

                case "Apr":
                case "Jun":
                case "Sep":
                case "Nov":
                    DayCombobox.ItemsSource = m_thirtyDayMonth;

                    break;
            }


            OnDateTimeChangedEvent();
        }

        /// <summary>
        /// Handles the SelectionChanged event of the Combobox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnDateTimeChangedEvent();
        }

        /// <summary>
        /// Called when [date time changed event].
        /// </summary>
        private void OnDateTimeChangedEvent()
        {
            var handler = DateTimeChangedEvent;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        #endregion
        
        #region Public Methods

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <returns></returns>
        public DateTime GetDateTime()
        {
            var year = int.Parse(YearCombobox.SelectedValue.ToString());
            var month = MonthCombobox.SelectedIndex + 1;
            var day = DayCombobox.SelectedIndex +1;
            var hour = HourCombobox.SelectedIndex;// +1;

            if (AmPmCombobox.SelectedIndex == 1)
            {
                if (hour == 12)//there is no 24:00:00 hour
                {
                    hour = 0;
                }
                else
                {
                    hour += 12;
                }
            }
            return new DateTime(year, month, day, hour, 0, 0);
        }

        /// <summary>
        /// Sets the date time.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <param name="hour">The hour.</param>
        public void SetDateTime(int year, int month, int day, int hour)
        {
            foreach (int item in YearCombobox.Items)
            {
                if (item == year)
                {
                    YearCombobox.SelectedItem = item;
                    break;
                }
            }

            MonthCombobox.SelectedIndex = month - 1;
            DayCombobox.SelectedIndex = day - 1;
            AmPmCombobox.SelectedIndex = hour > 11 ? 1 : 0;
            HourCombobox.SelectedIndex = hour % 12;
        }

        #endregion
    }
}