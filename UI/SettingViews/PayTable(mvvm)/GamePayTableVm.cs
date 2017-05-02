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
       
      
        private readonly B3SettingGlobal m_originalPayTableSettings;
        private readonly List<B3MathGamePay> m_b3MathGamePayFullList;
        private bool m_enforceMix;//are we going to use this again?
        private bool m_originalSetting;

        public string GameName { get; set; }
        
        public GamePayTableVm( B3SettingGlobal b3SettingGlobal)
        {
            m_originalPayTableSettings = b3SettingGlobal;
            m_gamePayTableModel = new GamePayTableModel();
            GameName = Business.Helpers.B3GameActualName[b3SettingGlobal.GameType];
            m_b3MathGamePayFullList = GetFullListPayTableSetting();       //This wont change all game has 55455 or rng setting

            m_isGameEnable = SettingViewModel.Instance.GetEnableDisableSettingValue(m_originalPayTableSettings.GameType).IsEnabled;
            m_originalSetting = m_isGameEnable;


            UpdateMathPayTableUI();
            //if (m_isGameEnable)//check if game is enable
            //{
            //    m_enforceMix = GetEnforceMixesSetting();
        

            //    var tempPayTableList = m_b3MathGamePayFullList.Where(l => l.IsRng == !m_enforceMix).ToList();
            //    if (tempPayTableList.Count != 0)//If current setting result is not null
            //    {
                    

            //        //Check if this has current paytablesetting
            //        int tempMathPackageId = Convert.ToInt32(m_originalPayTableSettings.B3SettingValue);
            //        if (tempMathPackageId != 0)
            //        {
            //            //check if the current setting exists on the b3setting list
            //            var currentSettingExists = tempPayTableList.Exists(l => l.MathPackageId == tempMathPackageId);
            //            if (currentSettingExists)
            //            {
            //                MathPayValue = tempPayTableList.FirstOrDefault(l => l.MathPackageId == tempMathPackageId);
            //            }
            //            else//If it dont exists show current setting 
            //            {
            //               var c = m_b3MathGamePayFullList.Single(l => l.MathPackageId == tempMathPackageId);

            //                var newB3MathGamePay = new B3MathGamePay()
            //                {
            //                    MathPackageId = c.MathPackageId,
            //                    GameType = c.GameType,
            //                    PackageDesc = "Current math package is " + c.PackageDesc,
            //                    IsRng = c.IsRng,
            //                    NeedToReplace = true
            //                };

            //                tempPayTableList.Add(newB3MathGamePay);
            //                MathPayValue = tempPayTableList.FirstOrDefault(l => l.MathPackageId == tempMathPackageId);
            //            }
            //        }//If its not set then just show the paytable setting
            //        else
            //        {

            //        }

            //        B3MathGamePayList = tempPayTableList;

            //    }
            //    else//If no available math package then disable this game
            //    {

            //    }
                
            //}
           
            //m_b3MathGamePayFullList = new List<B3MathGamePay>();
            //m_b3MathGamePayList = new List<B3MathGamePay>();
            //m_gamePayTableModel = new GamePayTableModel();
            //var tempEnforceMix = SettingViewModel.Instance.GetEnforceMixSetting();
            //m_b3MathGamePayFullList = SettingViewModel.Instance.GetB3MathGamePlay(b3SettingGlobal.GameType).ToList();
            //m_b3MathGamePayList = m_b3MathGamePayFullList.Where(l => l.IsRng == !tempEnforceMix).ToList();
            //GamePayTableModel.MathPayValue = GetB3MathGamePay(b3SettingGlobal.B3SettingValue);
            //GameName = Business.Helpers.B3GameActualName[b3SettingGlobal.GameType];
        }

        private B3MathGamePay m_mathPayValue;
        public B3MathGamePay MathPayValue
        {
            get
            {
                return m_mathPayValue;
            }
            set
            {
                m_mathPayValue = value;
                RaisePropertyChanged("MathPayValue");
            }
        }

        private void LoadGamePayTableSetting()
        {
           
        }

        private List<B3MathGamePay> m_b3MathGamePayList;
        public List<B3MathGamePay> B3MathGamePayList
        {
            get { return m_b3MathGamePayList; }
            set
            {
                m_b3MathGamePayList = value;
                RaisePropertyChanged("B3MathGamePayList");
            }
        }

        //Do we want to save it on the model?
        private bool m_isGameEnable;
        public bool IsGameEnable
        {
            get { return m_isGameEnable; }
            set
            {
                m_isGameEnable = value;
                RaisePropertyChanged("IsGameEnable");
            }
        }

        private B3IsGameEnabledSetting m_gameEnable;
        public B3IsGameEnabledSetting GameEnable
        {
            get
            {
                return SettingViewModel.Instance.GetEnableDisableSettingValue(m_originalPayTableSettings.GameType);
            }
            set
            {
                m_gameEnable = value;
                RaisePropertyChanged("GameEnable");
            }

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


        public void UpdateMathPayTableUI(/*bool isRng*/)
        {
           IsGameEnable = SettingViewModel.Instance.GetEnableDisableSettingValue(m_originalPayTableSettings.GameType).IsEnabled;
           m_enforceMix = GetEnforceMixesSetting();

            if (m_isGameEnable)//check if game is enable
            {
                //m_enforceMix = !isRng;
             

                var tempPayTableList = m_b3MathGamePayFullList.Where(l => l.IsRng == !m_enforceMix).ToList();
                if (tempPayTableList.Count != 0)//If current setting result is not null
                {


                    //Check if this has current paytablesetting
                    int tempMathPackageId = Convert.ToInt32(m_originalPayTableSettings.B3SettingValue);
                    if (tempMathPackageId != 0)
                    {
                        //check if the current setting exists on the b3setting list
                        var currentSettingExists = tempPayTableList.Exists(l => l.MathPackageId == tempMathPackageId);
                        if (currentSettingExists)
                        {
                            MathPayValue = tempPayTableList.FirstOrDefault(l => l.MathPackageId == tempMathPackageId);
                        }
                        else//If it dont exists show current setting 
                        {
                            var c = m_b3MathGamePayFullList.Single(l => l.MathPackageId == tempMathPackageId);

                            var newB3MathGamePay = new B3MathGamePay()
                            {
                                MathPackageId = c.MathPackageId,
                                GameType = c.GameType,
                                PackageDesc = "Current math package is " + c.PackageDesc,
                                IsRng = c.IsRng,
                                NeedToReplace = true
                            };

                            tempPayTableList.Add(newB3MathGamePay);
                            MathPayValue = tempPayTableList.FirstOrDefault(l => l.MathPackageId == tempMathPackageId);
                        }
                    }//If its not set then just show the paytable setting
                    else
                    {

                    }

                    //B3MathGamePayList = tempPayTableList;

                }
                else//If no available math package then disable this game
                {
                    IsGameEnable = false;
                }
                B3MathGamePayList = tempPayTableList;
            }
           
            //var x = new List<B3MathGamePay>();
            //x = m_b3MathGamePayFullList.Where(l => l.IsRng == isRng).ToList();
            //x.Select(c => { c.NeedToReplace = false; return c; }).ToList();
            //m_b3MathGamePayList = x;
            //GamePayTableModel.MathPayValue = GetB3MathGamePay(m_originalPayTableSettings.B3SettingValue);

        }
    
        private B3MathGamePay GetB3MathGamePay(string MathPackageId)
        {
          //  int mathPackageId;
          //  if (MathPackageId == null     
          //  ||!int.TryParse(MathPackageId, out mathPackageId)
          //|| m_b3MathGamePayList == null) return null;

         B3MathGamePay tempMathGamePaySetting = new B3MathGamePay();
          //  var tempList = m_b3MathGamePayList;
          //  if (GameDisabled != false) GameDisabled = false;

          // if (tempList.Count == 0)//If selected setting dont support RNG or 55455 then lets disable this game
          // {
          //     tempMathGamePaySetting = m_b3MathGamePayFullList.Single(l => l.MathPackageId == mathPackageId);

          //     var newB3MathGamePay = new B3MathGamePay()
          //     {
          //         MathPackageId = 0,
          //         GameType = tempMathGamePaySetting.GameType,
          //         PackageDesc = "This setting is not applicable to this game. This game is now disabled.",
          //         IsRng = tempMathGamePaySetting.IsRng,
          //         NeedToReplace = false,
          //     };
          //     tempList.Add(newB3MathGamePay);
          //     mathPackageId = 0;
          //     if (GameDisabled != true) GameDisabled = true;
          // }
          // else
          // {

          //     var ItExists = m_b3MathGamePayList.Exists(l => l.MathPackageId == mathPackageId);
          //     if (ItExists == false)
          //     {
          //         tempMathGamePaySetting = m_b3MathGamePayFullList.Single(l => l.MathPackageId == mathPackageId);

          //         var newB3MathGamePay = new B3MathGamePay()
          //         {
          //             MathPackageId = tempMathGamePaySetting.MathPackageId,
          //             GameType = tempMathGamePaySetting.GameType,
          //             PackageDesc = "Current math package is " + tempMathGamePaySetting.PackageDesc,
          //             IsRng = tempMathGamePaySetting.IsRng,
          //             NeedToReplace = true
          //         };
          //         tempList.Add(newB3MathGamePay);
          //     }
          // }

          //  B3MathGamePayList = tempList;
          //  tempMathGamePaySetting = B3MathGamePayList.FirstOrDefault(l => l.MathPackageId == mathPackageId);
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

        private bool m_GameDisabled;
        public bool GameDisabled
        {
            get { return m_GameDisabled; }
            set
            {
                m_GameDisabled = value;
                RaisePropertyChanged("GameDisabled");
           }
        }

    
    }
}
