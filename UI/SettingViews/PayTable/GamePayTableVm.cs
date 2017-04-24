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

     
     public List<B3MathGamePay> B3MathGamePayList { get; }
        


        public GamePayTableVm( GamePayTableModel gamePayTableModel, B3SettingGlobal b3SettingGlobal)
        {
            B3MathGamePayList = SettingViewModel.Instance.GetB3MathGamePlay(b3SettingGlobal.GameType).ToList();
            GamePayTableModel = gamePayTableModel;
            GamePayTableModel.MathPayTable = GetB3MathGamePay(b3SettingGlobal.B3SettingValue);        
        }

        private B3MathGamePay GetB3MathGamePay(string MathPackageId)
        {
            int mathPackageId;

            //check for null
            if (MathPackageId == null)
            {
                return null;
            }

            ////make sure we are able to parse an int
            if (!int.TryParse(MathPackageId, out mathPackageId))
            {
                return null;
            }

            //check setting for null or empty list
            if (B3MathGamePayList == null ||
            B3MathGamePayList.Count() == 0)
                {
                    return null;
                }

            return B3MathGamePayList.FirstOrDefault(l => l.MathPackageId == mathPackageId);
        }

    }
}
