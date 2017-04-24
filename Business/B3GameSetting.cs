using GameTech.Elite.Base;

namespace GameTech.Elite.Client.Modules.B3Center.Business
{
    public class B3IsGameEnabledSetting : Notifier
    {
        #region Fields

        private bool m_isEnabled;
        private bool m_isAllowed;
        #endregion

        #region Constructors
        /// <summary>
        /// Use to set value
        /// </summary>
        public B3IsGameEnabledSetting(B3GameType game, bool isEnabled, bool isAllowed)
        {
            GameType = game;
            IsEnabled = isEnabled;
            IsAllowed = isAllowed;
        }

        public B3IsGameEnabledSetting()
        {
        }
        #endregion

        #region Properties 

        public B3GameType GameType
        {
            get;
            set;
        }

        public bool IsEnabled
        {
            get
            {
                return m_isEnabled;
            }
            set
            {
                m_isEnabled = value;
                RaisePropertyChanged("IsEnabled");
            }
        }

        public bool IsAllowed
        {
            get
            {
                return m_isAllowed;
            }
            set
            {
                m_isAllowed = value;
                RaisePropertyChanged("IsAllowed");
            }
        }

        #endregion
    }


    public class B3SettingGlobal
    {
        #region Constructor

        public B3SettingGlobal(B3SettingType b3Setting, B3SettingCategory b3SettingCategoryType, B3GameType b3GameType, string b3SettingValue)
        {
            SettingType = b3Setting;
            B3SettingCategoryType = b3SettingCategoryType;
            GameType = b3GameType;
            B3SettingValue = b3SettingValue;
        }

        public B3SettingGlobal()
        {
        }

        #endregion
        #region Properties

        public B3SettingType SettingType
        {
            get;
            set;
        }

        public B3SettingCategory B3SettingCategoryType
        {
            get;
            set;
        }

        public B3GameType GameType
        {
            get;
            set;
        }

        public string B3SettingValue
        {
            get;
            set;
        }

        public string B3SettingDefaultValue
        {
            get;
            set;
        }
        
        public bool UIUpdateRequired {get; set;}
        public bool IsPayTableSettings { get; set; }
        public bool HasChanged { get; set; }
        public bool IsGameENabled { get; set; }

        #endregion
    }


    public class B3IconColor
    {
        #region Constructors

        public B3IconColor()
        {
        }

        public B3IconColor(int colorId, string colorValue)
        {
            ColorId = colorId;
            ColorValue = colorValue;
        }

        #endregion
        #region Properties

        public int ColorId
        {
            get;
            set;
        }

        public string ColorValue
        {
            get;
            set;
        }


        public override string ToString()
        {
            return ColorValue;
        }

        #endregion

    }

    public class B3MathGamePay
    {
        public int MathPackageId
        {
            get;
            set;
        }

        public B3GameType GameType
        {
            get;
            set;
        }

        public string PackageDesc
        {
            get;
            set;
        }

        public bool IsRng
        {
            get;
            set;
        }

        public bool IsThisGameEnable
        {
            get;
            set;
        }
    }
}
