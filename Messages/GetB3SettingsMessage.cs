#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion
using System.Collections.Generic;
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
            b3SettingGlobal = new ObservableCollection<B3SettingGlobal>();
            if (ReturnCode == ServerReturnCode.Success)
            {

                int count = responseReader.ReadInt16();

                for (int i = 0; i < count; i++)
                {
                    B3SettingGlobal b3settingglobal = new B3SettingGlobal();
                    b3settingglobal.B3SettingID = responseReader.ReadInt32();
                    b3settingglobal.B3SettingCategoryID = responseReader.ReadInt32();
                    b3settingglobal.B3GameID = responseReader.ReadInt32();
                    b3settingglobal.B3SettingValue = new string(responseReader.ReadChars(responseReader.ReadInt16()));   
          
                    switch (b3settingglobal.B3SettingID)
                    {
                        case 52:
                            {
                                IsMultiOperator = (b3settingglobal.B3SettingValue == "T") ? true : false;
                                break;
                            }
                        case 53:
                            {
                                IsCommonRng = (b3settingglobal.B3SettingValue == "T") ? true : false;
                            break;
                            }
                        case 41:
                            {
                                EnforceMix = (b3settingglobal.B3SettingValue == "T") ? true : false;
                                break;
                            }
                        case 30:
                            {
                                AllowInSessBallChange = (b3settingglobal.B3SettingValue == "T") ? true : false;
                                break;
                            }
                        case 51:
                            {
                                IsDoubleAccount = (b3settingglobal.B3SettingValue == "T") ? true : false;
                                break;
                            }
                    }

                    b3SettingGlobal.Add(b3settingglobal);
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

        public ObservableCollection<B3SettingGlobal> b3SettingGlobal
        {
            get;
            set;
        }
       
        #endregion
    }
}
