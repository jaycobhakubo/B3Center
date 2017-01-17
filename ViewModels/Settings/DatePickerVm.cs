using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Model.Shared;
using System.Windows.Controls;
using System.Windows.Input;
using GameTech.Elite.Client.Modules.B3Center.Helper;
using Microsoft.Practices.Composite.Presentation;
using Microsoft.Practices.Composite;
using Microsoft.Practices.Composite.Presentation.Commands;
using System.Collections.ObjectModel;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Shared
{
    public class DatePickerVm : ViewModelBase
    {

        #region MEMBER VARIABLE AND PROPERTIES

        private bool m_showTime;
        public bool ShowTime
        {
            get
            {
                return m_showTime;
            }
            set
            {
                m_showTime = value;
            }
        }


        private DatePickerM m_datepickerModel;
        public DatePickerM DatepickerModel
        {
            get { return m_datepickerModel; }
            set
            {
                m_datepickerModel = value;
                RaisePropertyChanged("DatepickerModel");
            }
        }

        #endregion

        #region CONSTRUCTOR

        public DatePickerVm(DatePickerM datePickerModel, bool showTime )
        {
            ShowTime = false;
            DatepickerModel = datePickerModel;              
            PopulateItemList();
            EventItemChanged();
        }

        #endregion

        //public Visibility 

        #region MEMBER METHOD

     
        private void PopulateItemList()
        {
            var currentDateTime = DateTime.Now;
            MonthList = new ObservableCollection<string>(m_months);
            var cMonthint = DateTime.Now.Month;
            var cMonthName = m_months[cMonthint - 1];
            SelectedMonth = cMonthName;

            //MonthCombobox.SelectedIndex = DateTime.Now.Month - 1;

            var years = new List<int>();
            for (var i = DateTime.Now.Year; i > DateTime.Now.Year - 50; i--)
            {
                years.Add(i);
            }

            YearList = years.Select(i => i.ToString()).ToList();
            var cYearint = DateTime.Now.Year;
            SelectedYear = cYearint.ToString();

            DayOfMonthList = GetNumOfDayInMonth().Select(i => i.ToString()).ToList();
            var cDayint = DateTime.Now.Day;
            var cDaystring = m_dayOfMonthList[cDayint - 1];
            SelectedDay = cDaystring;
      
                TimeList_ = m_hours.ToList();
                var hour = DateTime.Now.Hour % 12;
                var hourString = m_timeList[hour];
                SelectedTime = hourString;

                AmpmList = m_amPm.ToList();
                var AMPMindex = DateTime.Now.Hour > 11 ? 1 : 0;
                var AMPMstring = m_ampmList[AMPMindex];
                SelectedAmpm = AMPMstring;

        }

        private int[] GetNumOfDayInMonth()
        {
            int[] tempResult = { 0 };

            switch (SelectedMonth)
            {
                case "Feb":

                    //leap year logic:
                    //The year is evenly divisible by 4;
                    //If the year can be evenly divided by 100, it is NOT a leap year, unless;
                    //The year is also evenly divisible by 400. Then it is a leap year.
                    int year;
                    if (int.TryParse(SelectedYear, out year))
                    {
                        if (year % 4 == 0)
                        {
                            if (year % 100 == 0)
                            {

                                tempResult = year % 400 == 0 ? m_twentyNineDayMonth : m_twentyEightDayMonth;
                                break;
                            }

                            tempResult = m_twentyNineDayMonth;
                            break;
                        }
                    }

                    tempResult = m_twentyEightDayMonth;

                    break;

                case "Jan":
                case "Mar":
                case "May":
                case "Jul":
                case "Aug":
                case "Oct":
                case "Dec":
                    tempResult = m_thirtyOneDayMonth;
                    break;

                case "Apr":
                case "Jun":
                case "Sep":
                case "Nov":
                    tempResult = m_thirtyDayMonth;

                    break;
            }

            return tempResult;

        }

        #endregion

        #region SELECTEDITEM (properties and private member)


       

        private string m_selectedMonth;
        public string SelectedMonth
        {
            get { return DatepickerModel.DateMonthWord; }
            set
            {
                DatepickerModel.DateMonthWord = value;
                RaisePropertyChanged("SelectedMonth");
            }
        }


        private string m_selectedYear;
        public string SelectedYear
        {
            get { return DatepickerModel.DateYearInt; }
            set
            {
                DatepickerModel.DateYearInt = value;
                RaisePropertyChanged("SelectedYear");
            }

        }

        private string m_selectedDay;
        public string SelectedDay
        {
            get { return DatepickerModel.DateDayInt; }
            set
            {
                DatepickerModel.DateDayInt = value;
                RaisePropertyChanged("SelectedDay");
            }

        }

        private string m_selectedTime;
        public string SelectedTime
        {
            get { return DatepickerModel.DateTimestring; }
            set
            {
                DatepickerModel.DateTimestring = value;
                RaisePropertyChanged("SelectedTime");
            }
        }

        private string m_selectedAmpm;
        public string SelectedAmpm
        {
            get { return DatepickerModel.dateAMPM; }
            set
            {
                DatepickerModel.dateAMPM = value;
                RaisePropertyChanged("SelectedAmpm");
            }
        }

        #endregion

        #region LIST (properties and member var)

        private ObservableCollection<string> m_monthList;
        public ObservableCollection<string> MonthList
        {
            get { return m_monthList; }
            set
            {
                m_monthList = value;
                RaisePropertyChanged("MonthList");

            }
        }

        private List<string> m_yearList;
        public List<string> YearList
        {
            get { return m_yearList; }
            set
            {
                m_yearList = value;
                RaisePropertyChanged("YearList");
            }
        }

        private List<string> m_dayOfMonthList;
        public List<string> DayOfMonthList
        {
            get { return m_dayOfMonthList; }
            set
            {
                m_dayOfMonthList = value;
                RaisePropertyChanged("DayOfMonthList");
            }
        }

        private List<string> m_timeList;
        public List<string> TimeList_
        {
            get { return m_timeList; }
            set
            {
                m_timeList = value;
                RaisePropertyChanged("TimeList_");
            }
        }

        private List<string> m_ampmList;
        public List<string> AmpmList
        {
            get { return m_ampmList; }
            set
            {
                m_ampmList = value;
                RaisePropertyChanged("AmpmList");
            }
        }

        #endregion

        #region EVENT (selectionchangedmvvm)

        public ICommand YearMonthSelectedChanged { get; private set; }
        public ICommand DateSelectedChanged { get; private set; }

        private void EventItemChanged()
        {
            YearMonthSelectedChanged = new DelegateCommand<string>(obj =>
            {      
                DayOfMonthList = GetNumOfDayInMonth().Select(i => i.ToString()).ToList();
                updateItemDateSelected();
            });

            DateSelectedChanged = new DelegateCommand<string>(obj =>
            {
                updateItemDateSelected();
            });
        }


        private DateTime GetSelectedDate()
        {
            DateTime tempResult;// = new DateTime();
            DateTime.TryParse(DatepickerModel.DateFullwTime, out tempResult);
            return tempResult;
            //DateTime 
        }

        private void updateItemDateSelected()
        {
            var x = ReportParameterViewModel.Instance;
            var i = GetSelectedDate();
            x.ClearSessionList();// = new ObservableCollection<Business.Session>();
             x.UpdateSessionList(i);
        
        }

       

        #endregion

        #region MEMBER VARIABLE (hardcoded)
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
    }
}


#region REF

//public void SetDateTime(int year, int month, int day, int hour)
//{
//    //foreach (int item in YearCombobox.Items)
//    //{
//    //    if (item == year)
//    //    {
//    //        YearCombobox.SelectedItem = item;
//    //        break;
//    //    }
//    //}

//    //MonthCombobox.SelectedIndex = month - 1;
//    //DayCombobox.SelectedIndex = day - 1;
//    //AmPmCombobox.SelectedIndex = hour > 11 ? 1 : 0;
//    //HourCombobox.SelectedIndex = hour % 12;
//}




//#region Events

///// <summary>
///// Occurs when [date time changed event].
///// </summary>
//public event EventHandler<EventArgs> DateTimeChangedEvent;

//#endregion

//#region

///// <summary>
///// Gets or sets a value indicating whether to show the time comboboxes.
///// </summary>
///// <value>
/////   <c>true</c> if [show time]; otherwise, <c>false</c>.
///// </value>

//#endregion


//#region Private Methods

///// <summary>
///// Handles the SelectionChanged event of the MonthYearCombobox control.
///// </summary>
///// <param name="sender">The source of the event.</param>
///// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
////private void MonthYearCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
////{
////    if (MonthCombobox.SelectedValue == null || YearCombobox.SelectedValue == null)
////    {
////        return;
////    }

////    switch (MonthCombobox.SelectedValue.ToString())
////    {
////        case "Feb":

////            //leap year logic:
////            //The year is evenly divisible by 4;
////            //If the year can be evenly divided by 100, it is NOT a leap year, unless;
////            //The year is also evenly divisible by 400. Then it is a leap year.
////            int year;
////            if (int.TryParse(YearCombobox.SelectedValue.ToString(), out year))
////            {
////                if (year % 4 == 0)
////                {
////                    if (year % 100 == 0)
////                    {
////                        DayCombobox.ItemsSource = year % 400 == 0 ? m_twentyNineDayMonth : m_twentyEightDayMonth;
////                        break;
////                    }

////                    DayCombobox.ItemsSource = m_twentyNineDayMonth;
////                    break;
////                }
////            }

////            DayCombobox.ItemsSource = m_twentyEightDayMonth;

////            break;

////        case "Jan":
////        case "Mar":
////        case "May":
////        case "Jul":
////        case "Aug":
////        case "Oct":
////        case "Dec":
////            DayCombobox.ItemsSource = m_thirtyOneDayMonth;
////            break;

////        case "Apr":
////        case "Jun":
////        case "Sep":
////        case "Nov":
////            DayCombobox.ItemsSource = m_thirtyDayMonth;

////            break;
////    }


////    OnDateTimeChangedEvent();
////}

///// <summary>
///// Handles the SelectionChanged event of the Combobox control.
///// </summary>
///// <param name="sender">The source of the event.</param>
///// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
////private void Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
////{
////    OnDateTimeChangedEvent();
////}

///// <summary>
///// Called when [date time changed event].
///// </summary>
//private void OnDateTimeChangedEvent()
//{
//    var handler = DateTimeChangedEvent;
//    if (handler != null)
//    {
//        handler(this, EventArgs.Empty);
//    }
//}

//#endregion


//#region Public Methods

///// <summary>
///// Gets the date time.
///// </summary>
///// <returns></returns>
////public DateTime GetDateTime()
////{
////    //var year = int.Parse(YearCombobox.SelectedValue.ToString());
////var month = MonthCombobox.SelectedIndex + 1;
////var day = DayCombobox.SelectedIndex + 1;
////var hour = HourCombobox.SelectedIndex;// +1;

////if (AmPmCombobox.SelectedIndex == 1)
////{
////    if (hour == 12)//there is no 24:00:00 hour
////    {
////        hour = 0;
////    }
////    else
////    {
////        hour += 12;
////    }
////}
////    return new DateTime(year, month, day, hour, 0, 0);
////}

///// <summary>
///// Sets the date time.
///// </summary>
///// <param name="year">The year.</param>
///// <param name="month">The month.</param>
///// <param name="day">The day.</param>
///// <param name="hour">The hour.</param>

//#endregion
//#endregion

#endregion