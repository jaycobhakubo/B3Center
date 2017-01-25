﻿//<UserControl x:Class="GameTech.Elite.Client.Modules.B3Center.UI.SettingViews.GameSettingCrazyBoutView"
//             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
//             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
//             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
//             xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
//             mc:Ignorable="d" 
//              xmlns:Converter ="clr-namespace:GameTech.Elite.Client.Modules.B3Center.Helpers"
//         >
//    <UserControl.Resources>
//        <Converter:ValueToBoolConverter x:Key="ValuetoBool"></Converter:ValueToBoolConverter>
//        <Converter:ValueDBToValueAppCallSpeed x:Key="ConvertDbToAppValueCallSpeed"></Converter:ValueDBToValueAppCallSpeed>
//    </UserControl.Resources>
//    <Border>

//            <Grid>
//                < Grid.ColumnDefinitions >
//                    < ColumnDefinition />
//                    < ColumnDefinition Width = "279" />
//                    < ColumnDefinition Width = "75" />
//                    < ColumnDefinition Width = "279" />
//                </ Grid.ColumnDefinitions >

//                < Grid.RowDefinitions >
//                    < RowDefinition Height = "Auto" />
//                </ Grid.RowDefinitions >

//                < StackPanel Grid.Column="1">
//                    <Label Content = "Game Pay Table" />
//                    < ComboBox Name= "cmbxGamePayTable" Tag= "58"
//                              ItemsSource= "{Binding Gamesetting_.LGamePayTable}"
//                              DisplayMemberPath= "PackageDesc"
//                              SelectedItem= "{Binding Gamesetting_.MathPayTableSetting}"
//                              />

//                    < Label Content = "Max Bet Level" />
//                    < ComboBox


//                Name= "cmbxMaxBetLevel"
//                        ItemsSource= "{Binding Gamesetting_.LMaxBetLevel}"
//                        SelectedItem= "{Binding Gamesetting_.MaxBetLevel}"
//               Tag= "9"
//                >
//                    </ ComboBox >

//                    < Label x:Name= "lblMaxCards"  Content= "Max Cards" />
//                < ComboBox
//                    ItemsSource = "{Binding Gamesetting_.LMaxCards}"
//                    SelectedItem= "{Binding Gamesetting_.MaxCards}"
//                x:Name= "cmbxMaxcards"
//                 Tag= "10" >
//                    </ ComboBox >

//                    < Label x:Name= "lblCallSpeedMin"  Content= "Call Speed Min" />
//                    < ComboBox
//                x:Name= "cmbxCallSpeedMin"
//                        ItemsSource= "{Binding Gamesetting_.LCallSpeed}"
//                        SelectedItem= "{Binding Gamesetting_.CallSpeed, Converter={StaticResource ConvertDbToAppValueCallSpeed}}"

//                 Tag= "59" />

//                    < Label x:Name= "lblCallSpeedOrMax"  Content= "Call Speed Max" />
//                < ComboBox ItemsSource = "{Binding Gamesetting_.LCallSpeedMin}"

//                x:Name= "cmbxCallSpeedOrMax" SelectedItem= "{Binding Gamesetting_.CallSpeedMin, Converter={StaticResource ConvertDbToAppValueCallSpeed}}"

//                 Tag= "11" >
//                    </ ComboBox >

//                 < Label x:Name= "lblCallSpeedMin"  Content= "Call Speed" />
//                < ComboBox
//                 ItemsSource= "{Binding Gamesetting_.LCallSpeed}"
//                        SelectedItem= "{Binding Gamesetting_.CallSpeed, Converter={StaticResource ConvertDbToAppValueCallSpeed}}"
//                x:Name= "cmbxCallSpeedMin"
//                 Tag= "59" />


//                    < Label Content = "Game Denominations" />
//                    < GroupBox  Background= "White"   BorderThickness= "2"  Margin= "-1,0,-1,0" Padding= "15,0,0,3" Style= "{DynamicResource GroupBoxStyle1}" >

//                        < Grid >
//                            < Grid.Resources >
//                                < Style TargetType = "CheckBox" >
//                                    < Setter Property= "Margin" Value= "0,0,0,0" ></ Setter >
//                                    < Setter Property = "Height" Value= "18" />
//                                </ Style >
//                            </ Grid.Resources >
//                            < Grid.RowDefinitions >
//                                < RowDefinition Height = "Auto" />
//                                < RowDefinition Height= "30" />
//                                < RowDefinition Height = "30" />
//                            </ Grid.RowDefinitions >
//                            < Grid.ColumnDefinitions >
//                                < ColumnDefinition Width= "1*" />
//                                < ColumnDefinition Width = "1*" />
//                                < ColumnDefinition Width= "1*" />
//                                < ColumnDefinition Width = "1*" />
//                            </ Grid.ColumnDefinitions >


//                        < CheckBox Name= "chkbxDenom01" Content= "0.01" VerticalAlignment= "Bottom" HorizontalAlignment= "Left"  Grid.Row= "1" Grid.Column= "0"   TabIndex= "6" Tag= "1" IsChecked= "{Binding Gamesetting_.Denom1, Converter={StaticResource ValuetoBool}}" />
//                        < CheckBox Name = "chkbxDenom05" Content= "0.05" VerticalAlignment= "Bottom" HorizontalAlignment= "Left"   Grid.Row= "2" Grid.Column= "0" TabIndex= "7" Tag= "2" IsChecked= "{Binding Gamesetting_.Denom05, Converter={StaticResource ValuetoBool}}" />
//                        < CheckBox Name = "chkbxDenom10"  Content= "0.10" VerticalAlignment= "Bottom" HorizontalAlignment= "Left" Grid.Row= "1" Grid.Column= "1" TabIndex= "8" Tag= "3"  IsChecked= "{Binding Gamesetting_.Denom10, Converter={StaticResource ValuetoBool}}" />
//                        < CheckBox Name = "chkbxDenom25"  Content= "0.25" VerticalAlignment= "Bottom" HorizontalAlignment= "Left" Grid.Row= "2" Grid.Column= "1" TabIndex= "9" Tag= "4"  IsChecked= "{Binding Gamesetting_.Denom25, Converter={StaticResource ValuetoBool}}" />
//                        < CheckBox Name = "chkbxDenom50"  Content= "0.50" VerticalAlignment= "Bottom" HorizontalAlignment= "Left"  Grid.Row= "1" Grid.Column= "2" TabIndex= "10" Tag= "5" IsChecked= "{Binding Gamesetting_.Denom50, Converter={StaticResource ValuetoBool}}" />
//                        < CheckBox Name = "chkbxDenom100"  Content= "1.00" VerticalAlignment= "Bottom" HorizontalAlignment= "Left" Grid.Row= "2" Grid.Column= "2" TabIndex= "11" Tag= "6" IsChecked= "{Binding Gamesetting_.Denom100, Converter={StaticResource ValuetoBool}}" />
//                        < CheckBox Name = "chkbxDenom200"  Content= "2.00" VerticalAlignment= "Bottom" HorizontalAlignment= "Left" Grid.Row= "1" Grid.Column= "3" TabIndex= "12" Tag= "7" IsChecked= "{Binding Gamesetting_.Denom200, Converter={StaticResource ValuetoBool}}" />
//                        < CheckBox Name = "chkbxDenom500"  Content= "5.00" VerticalAlignment= "Bottom" HorizontalAlignment= "Left"  Grid.Row= "2" Grid.Column= "3" TabIndex= "13" Tag= "8" IsChecked= "{Binding Gamesetting_.Denom300, Converter={StaticResource ValuetoBool}}" />
//                    </ Grid >
//                    </ GroupBox >
//                </ StackPanel >
//                < StackPanel Grid.Column= "3" >
//                 < CheckBox Name = "chkbxAutoPlay"   Content= "Auto Play"  IsChecked= "{Binding Gamesetting_.AutoPlay, Converter={StaticResource ValuetoBool}}" />
//                < CheckBox  IsChecked= "{Binding Gamesetting_.HideSerialNumber, Converter={StaticResource ValuetoBool}}" Name= "chkbxHideSerialNumber" Content= "Hide Serial Number" />
//                < CheckBox  IsChecked= "{Binding Gamesetting_.SingleOfferBonus, Converter={StaticResource ValuetoBool}}" Name= "chkbxSingleOfferBonus" Content= "Single Offer Bonus" />
//                < CheckBox Name= "chkbxAutoCall" Visibility= "Collapsed"  Content= "Auto Call"  Tag= "12" />
//                </ StackPanel >
//            </ Grid >


//    </ Border >
//</ UserControl >
