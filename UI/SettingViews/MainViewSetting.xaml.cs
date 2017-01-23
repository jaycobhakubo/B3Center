using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using GameTech.Elite.Client.Modules.B3Center.Business;
using System.Linq;
using System.Collections.ObjectModel;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews
{
    /// <summary>
    /// Interaction logic for SettingView.xaml
    /// </summary>
    public partial class SettingView //: UserControl
    {

        private readonly GameSettingView m_gamesView;
        private readonly SystemSettingView m_systemSettingView;
        private readonly PlayerSettingView m_playerSettingView;
        private readonly SalesSettingView m_salesSettingView;
        private readonly SessionSettingView m_sessionView;
        private readonly ServerGameSettingView m_serverGameView;
        //private readonly OperatorView m_operatorView;
        private readonly List<ToggleButton> m_menuItems;
        private B3Setting m_B3Setting;
        private Button m_btnSave;
        private Button m_btnBackOperatorSetting;
        private Button m_btnBackOperatorSettingFromGameSetting;
        private ToggleButton m_toggleButton;

        public Button btnbtnBackOperatorSetting
        {
            get { return m_btnBackOperatorSetting; }
            set { m_btnBackOperatorSetting = value; }
        }

        public Button btnBackOperatorSettingFromGameSetting
        {
            get { return m_btnBackOperatorSettingFromGameSetting; }
            set { m_btnBackOperatorSettingFromGameSetting = value; }
        }

        public B3Setting B3_Setting
        {
            get { return m_B3Setting; }
            set { m_B3Setting = value; }
        }

        public SettingView()
        {
            InitializeComponent();
          
        }


        public void ClearSelected()
        {
            foreach (var menuItem in m_menuItems)
            {
                if (menuItem.IsChecked != null && (bool)menuItem.IsChecked)
                {
                    menuItem.IsChecked = false;
                    //SettingTransitionControl.Content = null;
                }
            }
        }

        #region EVENTS



        void m_btnBackOperatorSetting_Click(object sender, RoutedEventArgs e)
        {
            brdrSettingMenuCol.Visibility = Visibility.Visible;
            ColumnSettings.Width = new GridLength(200, GridUnitType.Pixel);
            ClearSelected();
        }

     
        private void MenuToggleButton_Changed(object sender, RoutedEventArgs e)
        {
            var toggleButton = sender as ToggleButton;
            m_toggleButton = toggleButton;

            if (toggleButton == null)
            {
                return;
            }

            UserControl view = null;

            switch (toggleButton.Name)
            {
                case "GameSettingToggleButton":
                    {
                        m_gamesView.ClearSelected();
                        //m_btnSave = m_gamesView.btnSave;
                        //m_btnSave.Click += new RoutedEventHandler(m_btnSave_Click);
                        view = m_gamesView;
                        break;
                    }

                case "SystemSettingToggleButton":
                        {
                            m_systemSettingView.ReloadDataIntoControls();
                            m_btnSave = m_systemSettingView.btnSave;
                            //m_btnSave.Click += new RoutedEventHandler(m_btnSave_Click);
                            view = m_systemSettingView; 
                            break;
                        }
                case "PlayerSettingToggleButton":
                        {
                      
                            view = m_playerSettingView;
                            break;
                        }
                case "SalesSettingToggleButton":
                        {
                       
                            view = m_salesSettingView;
                            break;
                        }
                case "SessionSettingToggleButton":
                        {
                        
                            view = m_sessionView;
                            break;
                        }
                case "ServerGameSettingToggleButton":
                        {
  
                            view = m_serverGameView;
                            break;
                        }
                //case "OperatorSettingToggleButton":
                //        {
                //            m_operatorView.ClearSelected();
                //            view = m_operatorView;                                                
                //            break;
                //        }                    
            }


            if (toggleButton.IsChecked == true)
            {
                foreach (var menuItem in m_menuItems)
                {
                    if (Equals(menuItem, toggleButton))
                    {
                        continue;
                    }

                    if (menuItem.IsChecked != null && (bool)menuItem.IsChecked)
                    {
                        menuItem.IsChecked = false;
                    }
                }

                //SettingTransitionControl.Content = view;
            }
            else
            {
                //SettingTransitionControl.Content = null;
            }
        }

        private void OperatorSettingToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            brdrSettingMenuCol.Visibility = Visibility.Collapsed;
            ColumnSettings.Width = GridLength.Auto;
        }

        #endregion
       
    }


    public class B3Setting
    {
        private List<B3GameSetting> m_b3GameSetting;
        public List<B3GameSetting> B3GameSetting_
        {
            get { return m_b3GameSetting; }
            set { m_b3GameSetting = value; }
        }

        private List<B3SettingGlobal> m_b3SettingGlobal;
        public List<B3SettingGlobal> B3SettingGlobal_
        {
            get { return m_b3SettingGlobal; }
            set { m_b3SettingGlobal = value; }
        }

        private List<B3MathGamePay> m_listB3mathGamePlay;
        public List<B3MathGamePay> ListB3mathGamePlay_
        {
            get { return m_listB3mathGamePlay; }
            set { m_listB3mathGamePlay = value; }
        }
    }

    public class ConvertToString
    {
        public static string decimal_(string tempValue)
        {
            decimal result; string outputvalue = "";
            if (!Decimal.TryParse(tempValue, out result))
            {
                outputvalue = "00";
            }
            else if (result == 00.00M)
            {
                outputvalue = "00";
            }
            else
            {
                outputvalue = result.ToString();
            }
            return outputvalue;
        }
    }


}

