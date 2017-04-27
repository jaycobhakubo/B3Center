using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews.PayTable
{
    public class GamePayTableVm : ViewModelBase
    {
        private GamePayTableModel m_gamePayTableModel;
        private readonly List<B3MathGamePay> m_b3MathGamePayFullList;
        private List<B3MathGamePay> m_b3MathGamePayList;
        public string GameName { get; set; }
        public bool IsGameEnable { get; set; }
        private readonly B3SettingGlobal m_originalPayTableSettings;
  
        public GamePayTableVm( B3SettingGlobal b3SettingGlobal)
        {
            m_originalPayTableSettings = b3SettingGlobal;
            m_b3MathGamePayFullList = new List<B3MathGamePay>();
            m_b3MathGamePayFullList = SettingViewModel.Instance.GetB3MathGamePlay(b3SettingGlobal.GameType).ToList();
            var tempIsRng = SettingViewModel.Instance.GetIsRngSetting();
            m_b3MathGamePayList = new List<B3MathGamePay>();
            m_b3MathGamePayList = m_b3MathGamePayFullList.Where(l => l.IsRng == tempIsRng).ToList();
            m_gamePayTableModel = new GamePayTableModel();
            GamePayTableModel.MathPayTable = GetB3MathGamePay(b3SettingGlobal.B3SettingValue);
            GameName = Business.Helpers.B3GameActualName[b3SettingGlobal.GameType];
        }

        public void UpdateMathPayTableUI(bool isRng)
        {
           var x  = new List<B3MathGamePay>();
            x = m_b3MathGamePayFullList.Where(l => l.IsRng == isRng).ToList();
           x.Select(c => { c.NeedToReplace = false; return c; }).ToList();
           m_b3MathGamePayList = x;
            GamePayTableModel.MathPayTable = GetB3MathGamePay(m_originalPayTableSettings.B3SettingValue);         
        }
    
        private B3MathGamePay GetB3MathGamePay(string MathPackageId)
        {
            int mathPackageId;
            if (MathPackageId == null) return null;       //check for null                
            if (!int.TryParse(MathPackageId, out mathPackageId)) return null;////make sure we are able to parse an int
            if (B3MathGamePayList == null) return null;

            B3MathGamePay tempMathGamePaySetting = new B3MathGamePay();
            var tempList = m_b3MathGamePayList;
            var ItExists = B3MathGamePayList.Exists(l => l.MathPackageId == mathPackageId);

            if (ItExists == false)
            {
                tempMathGamePaySetting = m_b3MathGamePayFullList.Single(l => l.MathPackageId == mathPackageId);
                
                var newB3MathGamePay = new B3MathGamePay()
                {
                    MathPackageId = tempMathGamePaySetting.MathPackageId,
                    GameType = tempMathGamePaySetting.GameType,
                    PackageDesc = "Current math package is " + tempMathGamePaySetting.PackageDesc,
                    IsRng = tempMathGamePaySetting.IsRng,
                    NeedToReplace = true
                };
                tempList.Add(newB3MathGamePay);             
            }
            B3MathGamePayList = tempList;
            tempMathGamePaySetting = B3MathGamePayList.FirstOrDefault(l => l.MathPackageId == mathPackageId);
            return tempMathGamePaySetting;
        }

        private bool m_changeme;
        public bool changeme
        {
            get { return m_changeme; }
            set
            {
                m_changeme = value;
                RaisePropertyChanged("changeme");

            }
        }

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

        public List<B3MathGamePay> B3MathGamePayList
        {
            get { return m_b3MathGamePayList; }
            set
            {
                m_b3MathGamePayList = value;
                RaisePropertyChanged("B3MathGamePayList");
            }
        }
    }
}
