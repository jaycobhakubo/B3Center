using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SettingViews.PayTable
{
    public class GamePayTableVm : ViewModelBase
    {
          
   
        private readonly B3SettingGlobal m_originalPayTableSettings;
        private readonly List<B3MathGamePay> m_b3MathGamePayFullList;
        private List<B3MathGamePay> m_b3MathGamePayList;
        private B3IsGameEnabledSetting m_gameEnable;
        private GamePayTableModel m_gamePayTableModel;
        private B3MathGamePay m_mathPayValue;
        private bool m_updateUIControl;
        private bool m_GameDisabled;
        private bool m_enforceMix;
        private bool m_originalSetting;
        private bool m_isGameEnable;

      
        public GamePayTableVm( B3SettingGlobal b3SettingGlobal)
        {
            m_originalPayTableSettings = b3SettingGlobal;
            m_gamePayTableModel = new GamePayTableModel();
            GameName = Business.Helpers.B3GameActualName[b3SettingGlobal.GameType];
            m_b3MathGamePayFullList = GetFullListPayTableSetting();    
            m_isGameEnable = SettingViewModel.Instance.GetEnableDisableSettingValue(m_originalPayTableSettings.GameType).IsEnabled;
            m_originalSetting = m_isGameEnable;
            UpdateMathPayTableUI();         
        }

        private List<B3MathGamePay> GetFullListPayTableSetting()
        {
            return  SettingViewModel.Instance.GetB3MathGamePlay(m_originalPayTableSettings.GameType).ToList();
        }

        private void GetListPayTableSetting()
        {
            var PayTableList = GetFullListPayTableSetting().Where(l => l.IsRng == !GetEnforceMixesSetting()).ToList();
        }

        private bool GetEnforceMixesSetting()
        {
           return SettingViewModel.Instance.GetEnforceMixSetting();
        }

        public void UpdateMathPayTableUI()
        {
           IsGameEnable = SettingViewModel.Instance.GetEnableDisableSettingValue(m_originalPayTableSettings.GameType).IsEnabled;
           m_enforceMix = GetEnforceMixesSetting();

            if (m_isGameEnable)//check if this game is enable
            {
              var tempPayTableList = m_b3MathGamePayFullList.Where(l => l.IsRng == !m_enforceMix).ToList();
                if (tempPayTableList.Count != 0)//If current setting result is not null.
                {
                    int tempMathPackageId = Convert.ToInt32(m_originalPayTableSettings.B3SettingValue);//Check if this has current paytablesetting.
                    if (tempMathPackageId != 0)
                    {
                        //Check if the current setting exists on the b3setting list.
                        var currentSettingExists = tempPayTableList.Exists(l => l.MathPackageId == tempMathPackageId);
                        if (currentSettingExists)
                        {
                            m_gamePayTableModel.MathPayValue = tempPayTableList.FirstOrDefault(l => l.MathPackageId == tempMathPackageId);
                        }
                        else//If it dont exists show current setting .
                        {
                            var c = m_b3MathGamePayFullList.Single(l => l.MathPackageId == tempMathPackageId);

                            var newB3MathGamePay = new B3MathGamePay()
                            {
                                MathPackageId = c.MathPackageId,
                                GameType = c.GameType,
                                PackageDesc = "Current math package is " + c.PackageDesc,
                                IsRng = c.IsRng,
                                ChangeForeground = true
                            };

                            tempPayTableList.Add(newB3MathGamePay);
                            m_gamePayTableModel.MathPayValue = tempPayTableList.FirstOrDefault(l => l.MathPackageId == tempMathPackageId);
                        }
                    }//If its not set then, just show the paytable setting.
                    else
                    {

                    }
                }
                else//If no available math package then disable this game.
                {
                    IsGameEnable = false;
                }
                B3MathGamePayList = tempPayTableList;
            }          
        }


        #region PROPERTIES

        public string GameName { get; set; }

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


        public bool UpdateUIControl
        {
            get { return m_updateUIControl; }
            set
            {
                m_updateUIControl = value;
                RaisePropertyChanged("UpdateUIControl");

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

        public bool IsGameEnable
        {
            get { return m_isGameEnable; }
            set
            {
                m_isGameEnable = value;
                RaisePropertyChanged("IsGameEnable");
            }
        }

        //public B3IsGameEnabledSetting GameEnable
        //{
        //    get
        //    {
        //        return SettingViewModel.Instance.GetEnableDisableSettingValue(m_originalPayTableSettings.GameType);
        //    }
        //    set
        //    {
        //        m_gameEnable = value;
        //        RaisePropertyChanged("GameEnable");
        //    }
        //}
        #endregion

    }
}
