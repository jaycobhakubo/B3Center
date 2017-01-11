using GameTech.Elite.Client.Modules.B3Center.ViewModels.Reports;
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

namespace GameTech.Elite.Client.Modules.B3Center.UI.ReportViews
{
    /// <summary>
    /// Interaction logic for BingoCardView.xaml
    /// </summary>
    public partial class BingoCardView : UserControl
    {
        public BingoCardView(ballcallVm bcvm)
        {
            InitializeComponent();
            DataContext = bcvm;
        }
    }
}
