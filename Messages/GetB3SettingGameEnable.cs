
#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System.Collections.Generic;
using System.IO;
using GameTech.Elite.Client.Modules.B3Center.Business;

namespace GameTech.Elite.Client.Modules.B3Center.Messages
{
    public class GetB3SettingGameEnable : ServerMessage
    {
        #region Constructors

        public GetB3SettingGameEnable()
        {
            ListB3GameSetting = new List<B3IsGameEnabledSetting>();
        }

        #endregion


        #region Member Methods

        /// <summary>
        /// packs the request to send to the server
        /// </summary>
        /// <param name="requestWriter"></param>
        protected override void PackRequest(BinaryWriter requestWriter)
        {
        }

        /// <summary>
        /// Parses the response received from the server.
        /// </summary>
        /// <param name="responseReader">The binary stream read that should
        /// be use to read any response data necessary.</param>
        protected override void UnpackResponse(BinaryReader responseReader)
        {
            if (ReturnCode == ServerReturnCode.Success)
            {
                int count = responseReader.ReadInt16();

                for (int i = 0; i < count; i++)
                {
                    int gameId = responseReader.ReadInt32();
                    bool isEnabled = responseReader.ReadBoolean();
                    bool isAllowed = responseReader.ReadBoolean();
                    B3IsGameEnabledSetting settings = new B3IsGameEnabledSetting((B3GameType)gameId, isEnabled, isAllowed);
                    ListB3GameSetting.Add(settings);
                }            
            }
        }

        public override int Id
        {
            get
            {
                return 39008;
            }
        }

        public override string Name
        {
            get
            {
                return "Get B3 Settings Game Enable";
            }
        }

        public List<B3IsGameEnabledSetting> ListB3GameSetting
        {
            get;
            set;
        }

        #endregion


    }
}
