using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using GameTech.Elite.Client.Modules.B3Center.Business;

namespace GameTech.Elite.Client.Modules.B3Center.Messages
{
    class SetB3OperatorMessage : ServerMessage
    {
        private Operator m_operator_;
        private int m_OperatorID;
        private int m_isDelete;

        public SetB3OperatorMessage(Operator operator_, int IsDelete)
        {
            m_operator_ = operator_;
            m_isDelete = IsDelete;
        }

        protected override void PackRequest(BinaryWriter requestWriter)
        {
            requestWriter.Write(m_operator_.OperatorId);//0 New operator; # Update operator

            requestWriter.Write((byte)m_isDelete);

            requestWriter.Write((ushort)m_operator_.OperatorName.Length);
            requestWriter.Write(m_operator_.OperatorName.ToCharArray());

            requestWriter.Write((ushort)m_operator_.OperatorNameDescription.Length);
            requestWriter.Write(m_operator_.OperatorNameDescription.ToCharArray());

            requestWriter.Write((ushort)m_operator_.ContactName.Length);
            requestWriter.Write(m_operator_.ContactName.ToCharArray());

            requestWriter.Write((ushort)m_operator_.Address.Length);
            requestWriter.Write(m_operator_.Address.ToCharArray());

            requestWriter.Write((ushort)m_operator_.City.Length);
            requestWriter.Write(m_operator_.City.ToCharArray());

            requestWriter.Write((ushort)m_operator_.State.Length);
            requestWriter.Write(m_operator_.State.ToCharArray());

            requestWriter.Write((ushort)m_operator_.ZipCode.Length);
            requestWriter.Write(m_operator_.ZipCode.ToCharArray());

            requestWriter.Write((ushort)m_operator_.PhoneNumber.Length);
            requestWriter.Write(m_operator_.PhoneNumber.ToCharArray());

            requestWriter.Write((ushort)m_operator_.FaxNumber.Length);
            requestWriter.Write(m_operator_.FaxNumber.ToCharArray());

            requestWriter.Write(m_operator_.IconColor);
        }

        protected override void UnpackResponse(BinaryReader responseReader)
        {
            if (ReturnCode == ServerReturnCode.Success)
            {
                m_OperatorID = responseReader.ReadInt16();                   
            }
                     
        }

        public int OperatorID
        {
            get { return m_OperatorID; }
   
        }

        public override int Id
        {
            get { return 39055; }
        }

        public override string Name
        {
            get { return "Set Operator Details"; }
        }
     
    }
}
