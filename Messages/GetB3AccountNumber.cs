using System.Collections.ObjectModel;
using System.IO;

namespace GameTech.Elite.Client.Modules.B3Center.Messages
{

    public class GetB3AccountNumber : ServerMessage
    {

        private readonly int m_sessionNumber;

        public GetB3AccountNumber(int sessionNumber)
        {
            AccountNumberList = new ObservableCollection<string>();
            m_sessionNumber = sessionNumber;
        }

                /// <summary>
        /// packs the request to send to the server
        /// </summary>
        /// <param name="requestWriter"></param>
        protected override void PackRequest(BinaryWriter requestWriter)
        {
            requestWriter.Write(m_sessionNumber);
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
                    int accountNumber = responseReader.ReadInt32();
                    AccountNumberList.Add(accountNumber.ToString());                   
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

        public ObservableCollection<string> AccountNumberList
        {
            get;
            set;
        }
    }
}
