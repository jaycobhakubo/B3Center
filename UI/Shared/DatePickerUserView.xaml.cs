﻿#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System.Windows.Controls;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Shared;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;

namespace GameTech.Elite.Client.Modules.B3Center.UI.Shared
{
    /// <summary>
    ///     Interaction logic for DateTimePickerUserControl.xaml
    /// </summary>
    /// 
    public partial class DatePickerUserView
    {
        public DatePickerUserView()
        {
         
            InitializeComponent();
            DataContext = this;
        }


        private void DateList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var thisvm = (DatePickerVm)DataContext;
            thisvm.DateSelectedChanged();


            var reportMainVm = ReportsViewModel.Instance;
            reportMainVm.UpdateItemDateSelected(thisvm.GetSelectedDate());
        }

        private void YearMonthList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var thisvm = (DatePickerVm)DataContext;
            thisvm.YearMonthSelectedChanged();

            var reportMainVm = ReportsViewModel.Instance;
            reportMainVm.UpdateItemDateSelected(thisvm.GetSelectedDate());
        }
    }
}