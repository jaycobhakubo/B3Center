﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews.PayTable
{
    /// <summary>
    /// Interaction logic for GamePayTable.xaml
    /// </summary>
    public partial class GamePayTable : UserControl
    {
        public GamePayTable()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void OnSelected(object sender, RoutedEventArgs e)
        {
            //ListBoxItem lbi = e.Source as ListBoxItem;

            //if (lbi != null)
            //{
            //    label1.Content = lbi.Content.ToString() + " is selected.";
            //}
        }

        private void cmbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cmbxitem = new ComboBoxItem();
            ComboBox cmbx = new ComboBox();
            cmbx = (ComboBox)sender;
            //cmbxitem = cmbx.SelectedItem;


        }
    }
}
