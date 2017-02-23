using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GameTech.Elite.Client.Modules.B3Center.Business
{
    public class B3GameSetting
    {
        #region Constructors
        /// <summary>
        /// Use to set value
        /// </summary>
        public B3GameSetting(int gameID, bool isEnabled, bool isAllowed)
        {
            GameId = gameID;
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

        public B3SettingGlobal(int B3SettingID_, int B3SettingCategoryID_, int B3GameID_, string B3SettingValue_)
        {
            B3SettingID = B3SettingID_;
            B3SettingCategoryID = B3SettingCategoryID_;
            B3GameID = B3GameID_;
            B3SettingValue = B3SettingValue_;
        }

        public B3SettingGlobal()
        {
        }

        #endregion
        #region Properties

        public int B3SettingID
        {
            get;
            set;
        }

        public int B3SettingCategoryID
        {
            get;
            set;
        }

        public int B3GameID
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

        public B3IconColor(int colorID, string colorValue)
        {
            ColorID = colorID;
            ColorValue = colorValue;
        }

        #endregion
        #region Properties

        public int ColorID
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
        public B3MathGamePay()
        {
        }

        public int MathPackageID
        {
            get;
            set;
        }

        public int GameID
        {
            get;
            set;
        }

        public string PackageDesc
        {
            get;
            set;
        }

        public bool IsRNG
        {
            get;
            set;
        }
    }
}
