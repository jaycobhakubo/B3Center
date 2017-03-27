using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace GameTech.Elite.Client.Modules.B3Center.Model
{
    class SetModelDefaultValue
    {
        public readonly ObservableCollection<B3GameSetting> B3SettingEnableDisablePreviousValue =  new ObservableCollection<B3GameSetting>();
        private readonly int m_gameCategoryId;

        public SetModelDefaultValue(object value, int SettingCategoryId)
        {
            m_gameCategoryId = SettingCategoryId;     
           UnbindTheseCollection(value);                               
        }

        private void UnbindTheseCollection(object value)
        {
            switch (m_gameCategoryId)
            {
                case (int)B3SettingCategory.Player:
                    {                     
                        B3SettingGameEnableDisable((ObservableCollection<B3GameSetting>)value);                     
                        break;
                    }
            }
        }

        private void B3SettingGameEnableDisable(ObservableCollection<B3GameSetting> Collection_)
        {
            foreach (var setting in Collection_)
            {
                var x = new B3GameSetting() { GameId = setting.GameId, IsEnabled = setting.IsEnabled, IsAllowed = setting.IsAllowed };              
                B3SettingEnableDisablePreviousValue.Add(x);
            }
        }

    }
}
