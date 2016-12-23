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
    public class GetB3SessionListMessage : ServerMessage
    {   
        #region Contructors
        public GetB3SessionListMessage()
        {
            SessionList = new List<Session>();
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
                var count = responseReader.ReadInt16();

                for (int i = 0; i < count; i++)
                {
                    var number = responseReader.ReadInt32();

                    var nameLength = responseReader.ReadInt16();

                    var name = responseReader.ReadChars(nameLength);

                    var startLength = responseReader.ReadInt16();

                    var startTime = responseReader.ReadChars(startLength);

                    var endLength = responseReader.ReadInt16();

                    var endTime = responseReader.ReadChars(startLength);

                    var active = responseReader.ReadBoolean();

                    var session = new Session(number, active, new string(name), new string(startTime), new string(endTime));

                    SessionList.Add(session);
                }
            }
        }

        public override int Id
        {
            get
            {
                return 39005;
            }
        }

        public override string Name
        {
            get
            {
                return "Get B3 Session List";
            }
        }

        public List<Session> SessionList
        {
            get; 
            private set;
        }

        #endregion
    }
}
