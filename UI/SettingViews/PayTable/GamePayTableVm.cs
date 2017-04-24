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




        private readonly List<B3MathGamePay> m_b3MathGamePayFullList;


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


        public string GameName
        {
            get;
            set;
        }

        public bool IsGameEnable { get; set; }

        public GamePayTableVm( B3SettingGlobal b3SettingGlobal)
        {
            m_originalPayTableSettings = b3SettingGlobal;
            m_b3MathGamePayFullList = new List<B3MathGamePay>();
            m_b3MathGamePayFullList = SettingViewModel.Instance.GetB3MathGamePlay(b3SettingGlobal.GameType).ToList();
            var x = SettingViewModel.Instance.GetIsRngSetting();
            B3MathGamePayList = m_b3MathGamePayFullList.Where(l => l.IsRng == x).ToList();
            m_gamePayTableModel = new GamePayTableModel();
            GamePayTableModel.MathPayTable = GetB3MathGamePay(b3SettingGlobal.B3SettingValue);
            GameName = Business.Helpers.B3GameActualName[b3SettingGlobal.GameType];
            m_harf = 0;
        }

        public void UpdateMathPayTableUI(bool isRng)
        {
            B3MathGamePayList = new List<B3MathGamePay>();
            B3MathGamePayList = m_b3MathGamePayFullList.Where(l => l.IsRng == isRng).ToList();
            GamePayTableModel.MathPayTable = GetB3MathGamePay(m_originalPayTableSettings.B3SettingValue);         
        }

        private readonly B3SettingGlobal m_originalPayTableSettings;
        public string GetMathNewPackageId()
        {
            m_originalPayTableSettings.B3SettingValue = GamePayTableModel.MathPayTable.MathPackageId.ToString();
            return m_originalPayTableSettings.B3SettingValue;
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
            //if (B3MathGamePayList == null ||
            //B3MathGamePayList.Count() == 0)
            //    {
            //        return null;
            //    }

            if (B3MathGamePayList == null )
            {
                return null;
            }

var x = B3MathGamePayList.FirstOrDefault(l => l.MathPackageId == mathPackageId);

        if (x == null)
        {
            x = m_b3MathGamePayFullList.Single(l => l.MathPackageId == mathPackageId);
            var newB3MathGamePay = new B3MathGamePay() 
            {
                MathPackageId = x .MathPackageId,
                GameType = x.GameType,
                PackageDesc = "Current math package is " + x.PackageDesc,
                IsRng = x.IsRng
            };

            //Harf = 1;
            Severity = 1;
           B3MathGamePayList.Add(newB3MathGamePay);
           x = B3MathGamePayList.FirstOrDefault(l => l.MathPackageId == mathPackageId);
        }

        return x;
        }

        private int m_harf;
        public int Severity
        {
            get { return m_harf; }
            set
            {
                m_harf = value;
                RaisePropertyChanged("Severity");
            }
        }

    }
}
