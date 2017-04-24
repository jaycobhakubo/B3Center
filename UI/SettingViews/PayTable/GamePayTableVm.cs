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

        private List<B3MathGamePay> m_b3MathGamePayFullList;


        private List<B3MathGamePay> m_b3MathGamePayList;
      
        public  List<B3MathGamePay> B3MathGamePayList
        {
            get { return m_b3MathGamePayList; }
            set
            {
                m_b3MathGamePayList = value;
                RaisePropertyChanged("B3MathGamePayList");
            }
        }


        public string  GameName { get;
        }


        public GamePayTableVm( B3SettingGlobal b3SettingGlobal)
        {
            m_b3MathGamePayFullList = new List<B3MathGamePay>();
            m_b3MathGamePayFullList = SettingViewModel.Instance.GetB3MathGamePlay(b3SettingGlobal.GameType).ToList();
            var x = SettingViewModel.Instance.GetIsRngSetting();
            B3MathGamePayList = m_b3MathGamePayFullList.Where(l => l.IsRng == x).ToList();
            m_gamePayTableModel = new GamePayTableModel();
            GamePayTableModel.MathPayTable = GetB3MathGamePay(b3SettingGlobal.B3SettingValue);
            GameName = Business.Helpers.B3GameActualName[b3SettingGlobal.GameType];
        }

        public void UpdateMathPayTableUI(bool isRng)
        {
            B3MathGamePayList = m_b3MathGamePayFullList.Where(l => l.IsRng == isRng).ToList();
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
