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

        /// <summary>
        /// For every change of operator in the list. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstbx_OperatorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox x = (ListBox)sender;
            var z = x.SelectedItem;
            OperatorViewModel y = (OperatorViewModel)x.DataContext;

            Operator i = new Operator();

            i = y.SelectedOperator;
            //m_selectedOperator = SaveSettingOriginalValue(i);

            //var tt = x.SelectedItem;
            var VmAccess = OperatorViewModel.Instance;
            VmAccess.SelectedItemChangevm(i);//This i still bind to the collection     
            
        }

        private Operator m_selectedOperator; 
      //  //Unbind the collection
      //  private Operator SaveSettingOriginalValue(Operator c)
      //  {
      //      var g = new Operator();
      //      g.Address = c.Address;
      //      g.City = c.City;
      //      g.ContactName = c.ContactName;
      //      g.FaxNumber = c.FaxNumber;
      //      g.IconColor = c.IconColor;
      //      //g.IconColorValue = c.IconColorValue;
      //      g.OperatorId = c.OperatorId;
      //      g.OperatorName = c.OperatorName;
      //      g.OperatorNameDescription = c.OperatorNameDescription;
      //      g.PhoneNumber = c.PhoneNumber;
      //      g.State = c.State;
      //      g.ZipCode = c.ZipCode;
      //      return g;
      //  }

       
    }
}
