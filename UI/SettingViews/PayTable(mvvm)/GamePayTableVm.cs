using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings;
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
        private List<B3MathGamePay> m_b3MathGamePayList;
        private readonly B3SettingGlobal m_originalPayTableSettings;
        private readonly List<B3MathGamePay> m_b3MathGamePayFullList;
        
        public string GameName { get; set; }

        B3IsGameEnabledSetting m_isGameEnable = new B3IsGameEnabledSetting();
        public B3IsGameEnabledSetting IsGameEnable
        {
            get { return m_isGameEnable; }
            set
            {
                m_isGameEnable = value;
                RaisePropertyChanged("IsGameEnable");
            }
        }
        
        public GamePayTableVm( B3SettingGlobal b3SettingGlobal)
        {
            GameName = Business.Helpers.B3GameActualName[b3SettingGlobal.GameType];
            m_b3MathGamePayFullList = new List<B3MathGamePay>();
            m_b3MathGamePayList = new List<B3MathGamePay>();
            m_gamePayTableModel = new GamePayTableModel();
            var tempEnforceMix = SettingViewModel.Instance.GetEnforceMixSetting();
            m_b3MathGamePayFullList = SettingViewModel.Instance.GetB3MathGamePlay(b3SettingGlobal.GameType).ToList();     
            m_originalPayTableSettings = b3SettingGlobal;                 
            m_b3MathGamePayList = m_b3MathGamePayFullList.Where(l => l.IsRng == !tempEnforceMix).ToList();
            GamePayTableModel.MathPayValue = GetB3MathGamePay(m_originalPayTableSettings.B3SettingValue);
        
        }

        public void UpdatePayTableUIDisableGame(B3IsGameEnabledSetting isGameEnable)
        {
            //m_isGameEnable = isGameEnable;
            //if (!m_isGameEnable.IsEnabled || m_b3MathGamePayList.Count == 1)
            //{
            //    GameDisable.IsEnabled = true;
            //}
            //else
            //{
            //    GameDisable.IsEnabled = false;
            //}
        }

        public void UpdateMathPayTableUI(bool isRng)
        {
            var x  = new List<B3MathGamePay>();
            x = m_b3MathGamePayFullList.Where(l => l.IsRng == isRng).ToList();
            x.Select(c => { c.NeedToReplace = false; return c; }).ToList();
            m_b3MathGamePayList = x;
            GamePayTableModel.MathPayValue = GetB3MathGamePay(m_originalPayTableSettings.B3SettingValue);  
                   
        }
    
        private B3MathGamePay GetB3MathGamePay(string MathPackageId)
        {
            int mathPackageId;
            if (MathPackageId == null     
            ||!int.TryParse(MathPackageId, out mathPackageId)
          || m_b3MathGamePayList == null) return null;

            B3MathGamePay tempMathGamePaySetting = new B3MathGamePay();
            var tempList = m_b3MathGamePayList;
      

           if (tempList.Count == 0)//If selected setting dont support RNG or 55455 then lets disable this game
           {
               tempMathGamePaySetting = m_b3MathGamePayFullList.Single(l => l.MathPackageId == mathPackageId);

               var newB3MathGamePay = new B3MathGamePay()
               {
                   MathPackageId = 0,
                   GameType = tempMathGamePaySetting.GameType,
                   PackageDesc = "This setting is not applicable to this game. This game is now disabled.",
                   IsRng = tempMathGamePaySetting.IsRng,
                   NeedToReplace = false,
               };
               tempList.Add(newB3MathGamePay);
               mathPackageId = 0;
               IsGameEnable.IsEnabled = false;

           }
           else
           {

               var ItExists = m_b3MathGamePayList.Exists(l => l.MathPackageId == mathPackageId);
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
               IsGameEnable.IsEnabled = true;
           }

            B3MathGamePayList = tempList;
            tempMathGamePaySetting = B3MathGamePayList.FirstOrDefault(l => l.MathPackageId == mathPackageId);
            return tempMathGamePaySetting;
        }
        //If selected setting does not support RNG or 55455

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
