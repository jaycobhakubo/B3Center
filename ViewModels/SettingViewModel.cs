#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply: © 2015 GameTech International, Inc.
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using GameTech.Elite.Base;
using GameTech.Elite.UI;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Properties;
using GameTech.Elite.Client.Modules.B3Center.UI;
using GameTech.Elite.Client.Modules.B3Center.Messages;
using System.Collections.Specialized;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels
{
    class SettingViewModel : ViewModelBase
    {
        #region MEMBERS 



        #region SYSTEM SETTINGS
        private string m_siteName;
        private string m_handPayTrigger;
        private string m_vipPointMultiplier;
        private string m_magCardSentinelStart;
        private string m_magCardSentinelEnd;
        private string m_playerPinLength;
        private string m_rngBallCallTime;
        private string m_minimumPlayers;
        //See the diff between this 3
        private ObservableCollection<string> m_currency;
        private string m_currencySelected;
        private IEnumerable<string> m_autoSessionEnd;
        private List<string> m_SystemMainVol = new List<string>();
        private bool m_commonRNGBallCall;
        private bool m_dualAccount;
        private bool m_multiOperator;
        private bool m_enableUK;
        private bool m_northDakota;
        #endregion
        private static volatile SettingViewModel m_instance;
        private static readonly object m_syncRoot = new Object();
        private B3Controller m_controller;
        #endregion

        private bool m_canExecute;
        private CommandHandler m_savedCommand;
        private CommandHandler m_valueCollectionChanged;
        private List<B3SettingGlobal> m_systemSettings;//What is the best way to habndle collection of data?


  

        #region CONSTRUCTOR

        private SettingViewModel()
        {
            m_currency = new ObservableCollection<string>();
            Currency = new ObservableCollection<string>();
        }

    
        #endregion


        public void Initialize(B3Controller controller)
        {
            if (controller == null)
                throw new ArgumentNullException();

       
            m_controller = controller;
            m_systemSettings =  m_controller.Settings.B3SettingGlobal_.Where(l => l.B3SettingCategoryID == (int)B3SettingCategory.SystemSettings).ToList();
           
           // m_systemSettingsNew = new ObservableCollection<B3SettingGlobal>(m_controller.Settings.B3SettingGlobal_.Where(l => l.B3SettingCategoryID == (int)B3SettingCategory.SystemSettings));
          //  var check = m_systemSettings == m_systemSettingsNew;
            //m_systemSettings.CollectionChanged += new NotifyCollectionChangedEventHandler(HandleChange);
            LoadCurrencyList(); 
            GetCurrentMemberValues();//Setting Value to controls
            m_canExecute = true;

        }

        //private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    if (e.NewItems != null)
        //        foreach (var x in e.NewItems)
        //        {
        //            Console.WriteLine(x);
        //        }

        //    if (e.OldItems != null)
        //        foreach (var y in e.OldItems)
        //        {
        //            Console.WriteLine(y);
        //        }
        //}

        public static SettingViewModel Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_syncRoot)
                    {
                        if (m_instance == null)
                            m_instance = new SettingViewModel();
                    }
                }

                return m_instance;
            }
        }

        #region ICommand

        public CommandHandler SavedCommand
        {
            get
            {
                return m_savedCommand ?? (m_savedCommand = new CommandHandler(() => Saved(), m_canExecute));
            }
        }


       


        //Get all the information that want to be saved
        public void  Saved()
        {
            //m_savedCommand.isSuccess = true; //Success
            try
            {
                //IList<B3SettingGlobal> CurrentValueSystemSetting;
                //CurrentValueSystemSetting = m_systemSettings;
                SetNewMemberValues();
                List<SettingMember> m_lSystemSettings_SettingMember = new List<SettingMember>();   

                foreach (B3SettingGlobal sg in m_systemSettings)
                {
                    //string OldValue = m_systemSettings.Single(l => l.B3SettingID == sg.B3SettingID).B3SettingValue;

                    //if (sg.B3SettingValue != OldValue)
                    //{
                        SettingMember sm = new SettingMember();
                        sm.m_settingID = sg.B3SettingID;
                        sm.m_gameID = sg.B3GameID;
                        sm.m_value = sg.B3SettingValue;
                        sm.m_oldValue = "";
                        m_lSystemSettings_SettingMember.Add(sm);
                    //}             
                }

                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                SetB3SettingsMessage msg = new SetB3SettingsMessage(m_lSystemSettings_SettingMember);
                msg.Send();
                Mouse.OverrideCursor = null;
            }
            catch
            {
                //m_savedCommand.isSuccess = false;
            }

    
           
        }

       //public B3SettingGlobal(int B3SettingID_, int B3SettingCategoryID_, int B3GameID_, string B3SettingValue_)

        #endregion

        #region METHODS


        private void SetNewMemberValues()
        {
            //You have the new value for handpaytrigger how do want to handle it\
            //How do we saves the old value
            //set the new value
            //This will trigger the even collectionchanged
            m_systemSettings.Single(l => l.B3SettingID == (int)B3SettingDescription.HandPayTrigger).B3SettingValue = m_handPayTrigger;
        }

        //public class SettingMember
        //{
        //    public int m_settingID;
        //    public int m_gameID;
        //    public string m_value;
        //    public string m_oldValue;
        //}

      
        internal void LoadCurrencyList()
        {
            m_currency.Add("CREDIT");
            m_currency.Add("DOLLAR");
            m_currency.Add("PESO");
            m_currency.Add("POUND");
            CurrencySelected = m_currency.LastOrDefault();
        }

     


        private void GetCurrentMemberValues()
        {
                m_handPayTrigger = m_systemSettings.Single(l => l.B3SettingID == (int)B3SettingDescription.HandPayTrigger).B3SettingValue;
                m_minimumPlayers = m_systemSettings.Single(l => l.B3SettingID == (int)B3SettingDescription.MinimumPlayers).B3SettingValue;
                m_vipPointMultiplier = m_systemSettings.Single(l => l.B3SettingID == (int)B3SettingDescription.VIPPointMultiplier).B3SettingValue;
                m_magCardSentinelStart = m_systemSettings.Single(l => l.B3SettingID == (int)B3SettingDescription.MagCardSentinelStart).B3SettingValue;
                m_magCardSentinelEnd= m_systemSettings.Single(l => l.B3SettingID == (int)B3SettingDescription.MagCardSentinelEnd).B3SettingValue;
                //m_currency = m_systemSettings.Single(l => l.B3SettingID == (int)B3SettingDescription.Currency).B3SettingValue;
                m_rngBallCallTime = m_systemSettings.Single(l => l.B3SettingID == (int)B3SettingDescription.RNGBallCallTime).B3SettingValue;
                m_playerPinLength = m_systemSettings.Single(l => l.B3SettingID == (int)B3SettingDescription.PlayerPINLength).B3SettingValue;
                m_enableUK = ( m_systemSettings.Single(l => l.B3SettingID == (int)B3SettingDescription.EnableUK).B3SettingValue == "T")? true : false;
                m_dualAccount = (m_systemSettings.Single(l => l.B3SettingID == (int)B3SettingDescription.DualAccount).B3SettingValue == "T") ? true : false;
                m_multiOperator = (m_systemSettings.Single(l => l.B3SettingID == (int)B3SettingDescription.MultiOperator).B3SettingValue == "T") ? true : false;
                m_commonRNGBallCall = (m_systemSettings.Single(l => l.B3SettingID == (int)B3SettingDescription.CommonRNGBallCall).B3SettingValue == "T") ? true : false;
                m_northDakota = (m_systemSettings.Single(l => l.B3SettingID == (int)B3SettingDescription.NorthDakotaMode).B3SettingValue == "T") ? true : false;
                //m_autoSessionEnd= m_systemSettings.Single(l => l.B3SettingID == (int)B3SettingDescription.AutoSessionEnd).B3SettingValue;
                m_siteName = m_systemSettings.Single(l => l.B3SettingID == (int)B3SettingDescription.SiteName).B3SettingValue;
                //m_SystemMainVol = m_systemSettings.Single(l => l.B3SettingID == (int)B3SettingDescription.MainVolume).B3SettingValue;
        }

      



        #endregion


        #region PROPERTIES SYSTEM SETTINGS

        public List<B3SettingGlobal> SystemSetting
        {
            get { return m_systemSettings; }
        }


       // public string HandPayTrigger {get { return m_handPayTrigger; }set { m_handPayTrigger = value; }}


        //public string Denom1 { get { return m_denom1; } set { m_denom1 = value; } }
        //public string Denom5 { get { return m_denom5; } set { m_denom5 = value; } }
        //public string Denom10 { get { return m_denom10; } set { m_denom10 = value; } }
        //public string Denom25 { get { return m_denom25; } set { m_denom25 = value; } }
        //public string Denom50 { get { return m_denom50; } set { m_denom50 = value; } }
        //public string Denom100 { get { return m_denom100; } set { m_denom100 = value; } }
        //public string Denom200 { get { return m_denom200; } set { m_denom200 = value; } }
        //public string Denom500 { get { return m_denom500; } set { m_denom500 = value; } }
        //public string MaxBetLevel { get { return m_maxBetLevel; } set { m_maxBetLevel = value; } }
        //public string MaxCards { get { return m_maxCards; } set { m_maxCards = value; } }
        //public string CallSpeed { get { return m_callSpeed; } set { m_callSpeed = value; } }
        //public string AutoCall { get { return m_autoCall; } set { m_autoCall = value; } }
        //public string AutoPlay { get { return m_autoPlay; } set { m_autoPlay = value; } }
        //public string HideSerialNumber { get { return m_hideSerialNumber; } set { m_hideSerialNumber = value; } }
        //public string SingleOfferBonus { get { return m_singleOfferBonus; } set { m_singleOfferBonus = value; } }
        //public string CalibrateTouch { get { return m_calibrateTouch; } set { m_calibrateTouch = value; } }
        //public string PresstoCollect { get { return m_presstoCollect; } set { m_presstoCollect = value; } }
        //public string AnnounceCall { get { return m_announceCall; } set { m_announceCall = value; } }
        //public string ScreenCursor { get { return m_screenCursor; } set { m_screenCursor = value; } }
        //public string TimeToCollect { get { return m_timeToCollect; } set { m_timeToCollect = value; } }
        //public string Disclaimer { get { return m_disclaimer; } set { m_disclaimer = value; } }
        //public string DisclaimerTextID { get { return m_disclaimerTextID; } set { m_disclaimerTextID = value; } }
        //public string MainVolume { get { return m_mainVolume; } set { m_mainVolume = value; } }
        //public string ScreenCursor { get { return m_screenCursor; } set { m_screenCursor = value; } }
        //public string CalibrateTouch { get { return m_calibrateTouch; } set { m_calibrateTouch = value; } }
        //public string AutoPrintSessionReport { get { return m_autoPrintSessionReport; } set { m_autoPrintSessionReport = value; } }
        //public string PagePrinter { get { return m_pagePrinter; } set { m_pagePrinter = value; } }
        //public string QuickSales { get { return m_quickSales; } set { m_quickSales = value; } }
        //public string PrintLogo { get { return m_printLogo; } set { m_printLogo = value; } }
        //public string AllowinSessionBallChange { get { return m_allowinSessionBallChange; } set { m_allowinSessionBallChange = value; } }
        //public string LoggingEnable { get { return m_loggingEnable; } set { m_loggingEnable = value; } }
        //public string LogRecycleDays { get { return m_logRecycleDays; } set { m_logRecycleDays = value; } }
        //public string MainVolume { get { return m_mainVolume; } set { m_mainVolume = value; } }
        //public string MinPlayer { get { return m_minPlayer; } set { m_minPlayer = value; } }
        //public string GameStartDelay { get { return m_gameStartDelay; } set { m_gameStartDelay = value; } }
        //public string ConsolotionPrize { get { return m_consolotionPrize; } set { m_consolotionPrize = value; } }
        //public string GameRecallPassword { get { return m_gameRecallPassword; } set { m_gameRecallPassword = value; } }
        //public string WaitCountDown { get { return m_waitCountDown; } set { m_waitCountDown = value; } }
        //public string PayoutLimit { get { return m_payoutLimit; } set { m_payoutLimit = value; } }
        //public string JackpotLimit { get { return m_jackpotLimit; } set { m_jackpotLimit = value; } }
        //public string EnforceMix { get { return m_enforceMix; } set { m_enforceMix = value; } }
        //public string HandPayTrigger { get { return m_handPayTrigger; } set { m_handPayTrigger = value; } }
        //public string MinimumPlayers { get { return m_minimumPlayers; } set { m_minimumPlayers = value; } }
        //public string VIPPointMultiplier { get { return m_vIPPointMultiplier; } set { m_vIPPointMultiplier = value; } }
        //public string MagCardSentinelStart { get { return m_magCardSentinelStart; } set { m_magCardSentinelStart = value; } }
        //public string MagCardSentinelEnd { get { return m_magCardSentinelEnd; } set { m_magCardSentinelEnd = value; } }
        //public string Currency { get { return m_currency; } set { m_currency = value; } }
        //public string RNGBallCallTime { get { return m_rNGBallCallTime; } set { m_rNGBallCallTime = value; } }
        //public string PlayerPINLength { get { return m_playerPINLength; } set { m_playerPINLength = value; } }
        //public string EnableUK { get { return m_enableUK; } set { m_enableUK = value; } }
        //public string DualAccount { get { return m_dualAccount; } set { m_dualAccount = value; } }
        //public string MultiOperator { get { return m_multiOperator; } set { m_multiOperator = value; } }
        //public string CommonRNGBallCall { get { return m_commonRNGBallCall; } set { m_commonRNGBallCall = value; } }
        //public string NorthDakotaMode { get { return m_northDakotaMode; } set { m_northDakotaMode = value; } }
        //public string AutoSessionEnd { get { return m_autoSessionEnd; } set { m_autoSessionEnd = value; } }
        //public string SiteName { get { return m_siteName; } set { m_siteName = value; } }
        //public string MainVolume { get { return m_mainVolume; } set { m_mainVolume = value; } }
        //public string MathPayTableSetting { get { return m_mathPayTableSetting; } set { m_mathPayTableSetting = value; } }
        //public string CallSpeedMin { get { return m_callSpeedMin; } set { m_callSpeedMin = value; } }
        //public string CallSpeedBonus { get { return m_callSpeedBonus; } set { m_callSpeedBonus = value; } }







        public bool DualAccount
        {
            get { return m_dualAccount; }
            set
            {
                m_dualAccount = value;
                RaisePropertyChanged("DualAccount");
            }

        }

        public string CurrencySelected
        {
            get { return m_currencySelected; }
            set
            {
                m_currencySelected = value;
                RaisePropertyChanged("CurrencySelected");
            }
        }

        public ObservableCollection<string> Currency
        {
            get { return m_currency; }
            set
            {
                m_currency = value;
                RaisePropertyChanged("Currency");
            }
        }

    



        #endregion


        #region PROPERTIES

        public B3CenterSettings Settings
        {
            get
            {
                if (m_controller == null)
                {
                    return null;
                }

                return m_controller.Settings;
            }
        }

        public ObservableCollection<GameTech.Elite.Client.Modules.B3Center.Business.Operator> Operators
        {
            get
            {
                if (m_controller == null)
                {
                    return null;
                }

                return m_controller.Operators;
            }
            set
            {
                //m_controller.Operators
            }

        }

        public int StaffId
        {
            get
            {
                if (m_controller == null)
                {

                    return 0;
                }
                else
                {
                    return m_controller.Parent.StaffId;
                }
            }
        }

        public int OperatorId
        {
            get
            {
                if (m_controller == null)
                {

                    return 0;
                }
                else
                {
                    return m_controller.Parent.OperatorId;
                }
            }
        }

        public int MachineId
        {
            get
            {
                if (m_controller == null)
                {

                    return 0;
                }
                else
                {
                    return m_controller.Parent.MachineId;
                }
            }
        }

        public bool IsClassIIB3GameEnable
        {
            get
            {
                return m_controller.Parent.Settings.IsClassIIB3Enable;
            }
        }

        #endregion

  

    }
}
