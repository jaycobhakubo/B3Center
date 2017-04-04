namespace GameTech.Elite.Client.Modules.B3Center.Business
{
    public class B3GameSetting
    {
        #region Constructors
        /// <summary>
        /// Use to set value
        /// </summary>
        public B3GameSetting(int gameId, bool isEnabled, bool isAllowed)
        {
            GameId = gameId;
            IsEnabled = isEnabled;
            IsAllowed = isAllowed;
        }

        public B3GameSetting()
        {
        }
        #endregion
        #region Properties 

        public int GameId
        {
            get;
            set;
        }

        public bool IsEnabled
        {
            get;
            set;
        }

        public bool IsAllowed
        {
            get;
            set;
        }

        #endregion
    }


    public class B3SettingGlobal
    {
        #region Constructor

        public B3SettingGlobal(int b3SettingId, int b3SettingCategoryId, int b3GameId, string b3SettingValue)
        {
            B3SettingId = b3SettingId;
            B3SettingCategoryId = b3SettingCategoryId;
            B3GameId = b3GameId;
            B3SettingValue = b3SettingValue;
        }

        public B3SettingGlobal()
        {
        }

        #endregion
        #region Properties

        public int B3SettingId
        {
            get;
            set;
        }

        public int B3SettingCategoryId
        {
            get;
            set;
        }

        public int B3GameId
        {
            get;
            set;
        }

        public string B3SettingValue
        {
            get;
            set;
        }

        public string B3SettingdefaultValue
        {
            get;
            set;
        }

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

        public int GameId
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
    }
}
