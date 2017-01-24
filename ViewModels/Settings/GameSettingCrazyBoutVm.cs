using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
    public class GameSettingVmAllGame : GameSettingTemplateVm
    {
        public GameSettingVmAllGame(GameSetting _gameSetting, int GameId)
        {     
            Gamesetting_ = _gameSetting;       
        }


        private GameSetting m_gameSetting_;
        public GameSetting Gamesetting_
        {
            get { return m_gameSetting_; }
            set
            {
                m_gameSetting_ = value;
                RaisePropertyChanged("Gamesetting_");
            }
        }

        //public List<string> GameVolumeList
        //{
        //    get;
        //    set;
        //}

        //public List<string> GameMaxBetLevel
        //{
        //    get;
        //    set;
        //}

        //public List<string> GameMaxCard
        //{
        //    get;
        //    set;
        //}

        //public List<string> GameCallSpeedMin
        //{
        //    get;
        //    set;
        //}

        //public List<string> GameCallSpeedMax
        //{
        //    get;
        //    set;
        //}
    }
}
