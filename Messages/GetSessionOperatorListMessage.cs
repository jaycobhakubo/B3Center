#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System.IO;
using System.Collections.ObjectModel;
using GameTech.Elite.Client.Modules.B3Center.Business;
using System.Linq;

namespace GameTech.Elite.Client.Modules.B3Center.Messages
{
    class GetSessionOperatorListMessage : ServerMessage
    {
        #region Member Variables
        private readonly int m_staffId;
        #endregion

        #region Constructors
        public GetSessionOperatorListMessage(int staffId)
        {
            m_staffId = staffId;
        }
        #endregion

        #region Member Methods
        protected override void PackRequest(BinaryWriter requestWriter)
        {
            requestWriter.Write(m_staffId);
        }

        protected override void UnpackResponse(BinaryReader responseReader)
        {
            base.UnpackResponse(responseReader);

           ushort opCount = responseReader.ReadUInt16();
            ObservableCollection<Operator> opList = new ObservableCollection<Operator>();

            for (ushort opIndex = 0; opIndex < opCount; ++opIndex)
            {
                int opId = responseReader.ReadInt32();
                string opName = new string(responseReader.ReadChars(responseReader.ReadUInt16()));
                string opNameDescr = new string(responseReader.ReadChars(responseReader.ReadUInt16()));
                string contactName = new string(responseReader.ReadChars(responseReader.ReadUInt16()));
                string address = new string(responseReader.ReadChars(responseReader.ReadUInt16()));
                string city = new string(responseReader.ReadChars(responseReader.ReadUInt16()));
                string state = new string(responseReader.ReadChars(responseReader.ReadUInt16()));
                string zipcode = new string(responseReader.ReadChars(responseReader.ReadUInt16()));
                string phonenum = new string(responseReader.ReadChars(responseReader.ReadUInt16()));
                string faxnum = new string(responseReader.ReadChars(responseReader.ReadUInt16()));
                int iconcolor = responseReader.ReadInt32();
                zipcode = new string(zipcode.ToList().Where(c => c != ' ').ToArray());
                Operator op = new Operator(opId, opName, opNameDescr, contactName, address, city, state, zipcode, phonenum, faxnum, iconcolor);
                opList.Add(op);
            }

            OperatorList = opList;
        }

        #endregion

        #region Member Properties

        public override int Id
        {
            get
            {
                return 39006;
            }
        }

        public override string Name
        {
            get
            {
                return "Session Operator List";
            }
        }

        public ObservableCollection<Operator> OperatorList
        {
            get;
            private set;
        }

        #endregion
    }
}
