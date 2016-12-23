﻿#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

//US4155: B3 Void Accounts

using System.IO;

namespace GameTech.Elite.Client.Modules.B3Center.Messages
{
    public class VoidB3SessionTicketsMessage : ServerMessage
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
            if (ReturnCode == ServerReturnCode.Success)
            {

            }
        }

        public override int Id
        {
            get
            {
                return 39067;
            }
        }

        public override string Name
        {
            get
            {
                return "Void B3 Session Tickets";
            }
        }
        #endregion
    }
}
