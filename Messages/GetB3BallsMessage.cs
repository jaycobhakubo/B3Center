#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System.Collections.Generic;
using System.IO;

//US4299: B3 Set Balls

namespace GameTech.Elite.Client.Modules.B3Center.Messages
{
    public class GetB3BallsMessage : ServerMessage
    {
        public GetB3BallsMessage()
        {
            GameBallList = new List<int>();
        }

        /// <summary>
        /// Prepares the request to be sent to the server.
        /// </summary>
        /// <param name="requestWriter">The binary stream writer that should
        /// be used to write any request data necessary.</param>
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
                    var number = responseReader.ReadByte();
                    GameBallList.Add(number);
                }
            }
        }

        public override int Id
        {
            get { return 39053; }
        }

        public override string Name
        {
            get { return "Get B3 Balls"; }
        }

        public List<int> GameBallList { get; private set; }
    }
}
