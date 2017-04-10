#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System.IO;
using GameTech.Elite.Client.Modules.B3Center.Business;
using System.Collections.ObjectModel;

namespace GameTech.Elite.Client.Modules.B3Center.Messages
{
    internal class GetB3SettingsMessage : ServerMessage
    {
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
            B3SettingGlobal = new ObservableCollection<B3SettingGlobal>();
            if (ReturnCode == ServerReturnCode.Success)
            {

                int count = responseReader.ReadInt16();

                for (int i = 0; i < count; i++)
                {
                    B3SettingGlobal b3Settingglobal = new B3SettingGlobal
                    {
                        SettingType = (B3SettingType)responseReader.ReadInt32(),
                        B3SettingCategoryType = (B3SettingCategory)responseReader.ReadInt32(),
                        GameType = (B3GameType)responseReader.ReadInt32(),
                        B3SettingValue = new string(responseReader.ReadChars(responseReader.ReadInt16()))
                    };


                    switch (b3Settingglobal.SettingType)
                    {
                        case B3SettingType.MultiOperator:
                            {
                                IsMultiOperator = b3Settingglobal.ConvertB3StringValueToBool();
                                break;
                            }
                        case B3SettingType.CommonRngBallCall:
                            {
                                IsCommonRng = b3Settingglobal.ConvertB3StringValueToBool();
                            break;
                            }
                        case B3SettingType.EnforceMix:
                            {
                                EnforceMix = b3Settingglobal.ConvertB3StringValueToBool();
                                break;
                            }
                        case B3SettingType.AlowinSessionBall:
                            {
                                AllowInSessBallChange = b3Settingglobal.ConvertB3StringValueToBool();
                                break;
                            }
                        case B3SettingType.DualAccount:
                            {
                                IsDoubleAccount = b3Settingglobal.ConvertB3StringValueToBool();
                                break;
                            }
                    }

                    B3SettingGlobal.Add(b3Settingglobal);
                }        
            }
        }

        public override int Id
        {
            get
            {
                return 39004;
            }
        }

        public override string Name
        {
            get
            {
                return "Get B3 Settings";
            }
        }

        public bool IsMultiOperator { get; private set; }
        public bool IsCommonRng { get; private set; }
        public bool AllowInSessBallChange { get; private set; }
        public bool EnforceMix { get; private set; }
        public bool IsDoubleAccount { get; private set; }

        public ObservableCollection<B3SettingGlobal> B3SettingGlobal
        {
            get;
            set;
        }
       
        #endregion
    }
}
