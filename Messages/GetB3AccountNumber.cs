using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace GameTech.Elite.Client.Modules.B3Center.Messages
{

    public class GetB3AccountNumber : ServerMessage
    {

        private int m_SessionNumber;

        public GetB3AccountNumber(int sessionNumber)
        {
            AccountNumberList = new List<int>();
            m_SessionNumber = sessionNumber;
        }

                /// <summary>
        /// packs the request to send to the server
        /// </summary>
        /// <param name="requestWriter"></param>
        protected override void PackRequest(BinaryWriter requestWriter)
        {
            requestWriter.Write(m_SessionNumber);
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
                    int AccountNumber = responseReader.ReadInt32();
                    AccountNumberList.Add(AccountNumber);                   
                }
            }
        }

        public override int Id
        {
            get
            {
                return 39056;
            }
        }

        public override string Name
        {
            get
            {
                return "Get B3 Account Number";
            }
        }

        public  List<int>  AccountNumberList
        {
            get;
            set;
        }
    }
}
