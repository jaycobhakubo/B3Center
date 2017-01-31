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

        //This selectionchanged is really not cool so much trigger on this event.
        private void lstbx_OperatorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OperatorViewModel ii = (OperatorViewModel)DataContext;
            var yy = ii.WorkInProgress;
            if (yy == false
                ) 
            {
                var currentOperatorIndex = ii.OperatorSelectedIndex;
                ii.SelectedItemChangevm(currentOperatorIndex);
                //var VmAccess = OperatorViewModel.Instance;
                //VmAccess.SelectedItemChangevm(currentOperatorIndex);
            }       
        }
    }
}
