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
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;

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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GamePayTableVm ii = (GamePayTableVm)DataContext;
            B3MathGamePay y = new B3MathGamePay();
            y = ii.GamePayTableModel.MathPayTable;
            if (ii.IsEnableGame != false)
            {
                if (y != null)
                {
                    if (ii.GamePayTableModel.MathPayTable.NeedToReplace == true)
                    {
                        if (ii.changeme != true)
                        {
                            ii.changeme = true;
                            SettingViewModel.Instance.PayTableSettingVm.ValidateUserInput();
                        }
                    }
                    else
                    {
                        if (ii.changeme != false)
                        {
                            ii.changeme = false;
                            SettingViewModel.Instance.PayTableSettingVm.ValidateUserInput();

                        }

                    }
                }
            }
            
        }   
    }
}
