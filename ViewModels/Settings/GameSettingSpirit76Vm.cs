﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTech.Elite.Client.Modules.B3Center.Model.Setting;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Settings
{
    public class GameSettingSpirit76Vm : GameSettingVmAllGame
    {
        public GameSettingSpirit76Vm(GameSetting _gameSetting, int GameId) :
            base(_gameSetting, GameId)
        {
            GameViewModel = this;
        }
        public GameSettingVmAllGame GameViewModel { get; set; }
    }
}
