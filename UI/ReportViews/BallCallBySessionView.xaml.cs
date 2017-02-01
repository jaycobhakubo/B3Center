using System;
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
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Reports;

namespace GameTech.Elite.Client.Modules.B3Center.UI.ReportViews
{
    /// <summary>
    /// Interaction logic for BallCallBySessionView.xaml
    /// </summary>
    public partial class BallCallBySessionView : UserControl
    {
        public BallCallBySessionView(ReportBaseVm bvm)
        {
            InitializeComponent();
            DataContext = bvm;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }     
    }
}
