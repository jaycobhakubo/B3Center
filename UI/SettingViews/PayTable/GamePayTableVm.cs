using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews.PayTable
{
    public class GamePayTableVm : ViewModelBase
    {

        //private PayTableSetting
        private GamePayTableModel m_gamePayTableModel;
        public GamePayTableModel GamePayTableModel
        {
            get
            {
                return m_gamePayTableModel;
            }
            set
            {
                m_gamePayTableModel = value;
                RaisePropertyChanged("GamePayTableModel");
            }
        }




        public GamePayTableVm(B3GameType b3GameType, GamePayTableModel gamePayTableModel)
        {
            GamePayTableModel = gamePayTableModel;
            GamePayTableModel.GamePayTableList = SettingViewModel.Instance.GetB3MathGamePlay(b3GameType).ToList();
        }
    }  
}
