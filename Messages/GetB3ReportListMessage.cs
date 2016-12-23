#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System.Collections.Generic;
using System.IO;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Reports;

namespace GameTech.Elite.Client.Modules.B3Center.Messages
{
    public class GetB3ReportListMessage : ServerMessage
    {
        public GetB3ReportListMessage()
        {
            Reports = new List<B3Report>();
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
                    var id = responseReader.ReadInt32();

                    var nameLength = responseReader.ReadInt16();

                    var name = new string(responseReader.ReadChars(nameLength));

                    var md5Length = responseReader.ReadInt16();

                    var md5 = responseReader.ReadBytes(md5Length);

                    var report = new B3Report
                    {
                        Id = (ReportId)id,
                        Name = name,
                        Hash = md5
                    };

                    Reports.Add(report);
                }
            }
        }

        protected override void PackRequest(BinaryWriter requestWriter)
        {
        }

        public override int Id
        {
            get { return 39007; }
        }

        public override string Name
        {
            get
            {
                return "Get B3 Report List";
            }
        }

        public List<B3Report> Reports { get; set; } 
    }
}
