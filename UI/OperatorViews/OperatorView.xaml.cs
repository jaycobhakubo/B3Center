using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;

namespace GameTech.Elite.Client.Modules.B3Center.UI.OperatorViews
{
 

    /// <summary>
    /// Interaction logic for OperatorSettingView.xaml
    /// </summary>
    public partial class OperatorView : UserControl
    {
        public OperatorView()      
        {
            InitializeComponent();
            DataContext = this;      
        }
  
        #region METHODs

        /// <summary>
        /// Load all the available operator.
        /// </summary>
          private void FillOperatorList()
        {
            //if (stkpnlOperatorList.Children.Count > 0)
            //{
            //    stkpnlOperatorList.Children.Clear();
            //}

            //m_operators = SettingViewModel.Instance.Operators;     

            //if (m_operators != null)
            //{
            //    foreach (GameTech.Elite.Client.Modules.B3Center.Business.Operator  Operator_  in (m_operators.OrderBy(l => l.OperatorName).ToList()))
            //    {
                   
            //            ToggleButton tgb = new ToggleButton();
            //            tgb.SetResourceReference(Control.StyleProperty, "ToggleButtonDarkBlueStyle");
            //            tgb.Content = Operator_.OperatorName;
            //            tgb.Height = 50;
            //            tgb.Width = 200;
            //            tgb.FontSize = 15;
            //            tgb.HorizontalContentAlignment = HorizontalAlignment.Left;
            //            tgb.HorizontalAlignment = HorizontalAlignment.Left;
            //            tgb.Click += MenuToggleButton_Changed;
            //            tgb.Tag = Operator_.OperatorId;//This  is unique
            //            m_menuItems.Add(tgb);
            //            stkpnlOperatorList.Children.Add(tgb);

            //            if (m_currentOperatorSelected == Operator_.OperatorId)
            //            {
            //                tgb.IsChecked = true;
            //                view = m_charity;
            //                //CharityTransitionControl.Content = view;
            //                //m_charity.lblSavedNotification.Visibility = Visibility.Visible;
            //                m_charity.LoadDataIntoVar(Convert.ToInt32(m_currentOperatorSelected));
            //            }
                    
            //    }
            //}
        }

        /// <summary>
        /// Clear the selected operator.
        /// </summary>

          public void ClearSelected()
          {
              //foreach (var menuItem in m_menuItems)
              //{
              //    if (menuItem.IsChecked != null && (bool)menuItem.IsChecked)
              //    {
              //        menuItem.IsChecked = false;
              //        CharityTransitionControl.Content = null;

              //    }
              //}

              //if (CharityTransitionControl.Content != null)
              //{
              //    CharityTransitionControl.Content = null;
              //}

          }

        #endregion

        #region EVENTs

          /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
          private void MenuToggleButton_Changed(object sender, RoutedEventArgs e)
          {

         //     var toggleButton = sender as ToggleButton;



         //     if (toggleButton == null)
         //     {
         //         return;
         //     }

         //     view = null;

         //     if (toggleButton.IsChecked == false)
         //     {
         //         //CharityTransitionControl.Content = null;
         
         //         return;
         //     }

         ////Load data.
         //     m_charity.NewOperator = false;
         //     m_charity.ClearSavedNotification();
         //     m_charity.LoadDataIntoVar(Convert.ToInt32(toggleButton.Tag));
             
         //     view = m_charity;

         //     if (toggleButton.IsChecked == true)
         //     {
         //         foreach (var menuItem in m_menuItems)
         //         {
         //             if (Equals(menuItem, toggleButton))
         //             {
         //                 continue;
         //             }

         //             if (menuItem.IsChecked != null && (bool)menuItem.IsChecked)
         //             {
         //                 menuItem.IsChecked = false;
         //             }
         //         }

         //         //CharityTransitionControl.Content = view;
         //     }
         //     else
         //     {

         //             foreach (var menuItem in m_menuItems)
         //             {
         //                 if (Equals(menuItem, toggleButton))
         //                 {
         //                     menuItem.IsChecked = true;

         //                 }
         //             }
               
         //     }
          }


         
        /// <summary>
        /// Will go back to the main view and settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
          private void btnBackOperatorSettings_Click(object sender, RoutedEventArgs e)
          {

          }

        /// <summary>
        /// Initialize new usercontrol for adding new operator.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
          private void btnNewOperator_Click(object sender, RoutedEventArgs e)
          {
              //ClearSelected();
              //m_newOperator = new CharityView();
              //m_newOperator.NewOperator = true;
              //Button NewOperatorSaveBtn =  m_newOperator.BtnSave;
              //NewOperatorSaveBtn.Click += new RoutedEventHandler(m_nobtnSave_Click);
              ////CharityTransitionControl.Content = m_newOperator;
            
          }


        /// <summary>
        /// Update existing operator.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
          void m_btnSave_Click(object sender, RoutedEventArgs e)
          {
              //if (m_charity.IsOperatorRenamed)
              //{
              //    m_currentOperatorSelected = m_charity.CurrentOperatorId;
              //    FillOperatorList();
              //}            
          }

        /// <summary>
        /// Add new operator.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
          void m_nobtnSave_Click(object sender, RoutedEventArgs e)
          {
              //if (m_newOperator.NewOperator == true)
              //{
              //    m_currentOperatorSelected = m_newOperator.CurrentOperatorId;
              //    FillOperatorList();
              //}
          }

         
        /// <summary>
        /// Delete existing operator.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //m_currentOperatorSelected = m_charity.CurrentOperatorId;
            //FillOperatorList();
            //CharityTransitionControl.Content = null;
        }



        #endregion

        private void lstbx_OperatorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem test = (ListBoxItem)sender;
            
        }
    }
}
