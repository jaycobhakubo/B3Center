using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using System.Collections.ObjectModel;

namespace GameTech.Elite.Client.Modules.B3Center.Model
{
    class SetModelDefaultValue
    {
        public readonly ObservableCollection<B3GameSetting> B3SettingEnableDisablePreviousValue =  new ObservableCollection<B3GameSetting>();
        private readonly int m_gameCategoryId;

        public SetModelDefaultValue(object value, int settingCategoryId)
        {
            m_gameCategoryId = settingCategoryId;     
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

        private void B3SettingGameEnableDisable(ObservableCollection<B3GameSetting> collection)
        {
            foreach (var setting in collection)
            {
                var x = new B3GameSetting { GameType = setting.GameType, IsEnabled = setting.IsEnabled, IsAllowed = setting.IsAllowed };              
                B3SettingEnableDisablePreviousValue.Add(x);
            }
        }

    }
}
