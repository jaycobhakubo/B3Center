using System.IO;
using GameTech.Elite.Client.Modules.B3Center.Business;

namespace GameTech.Elite.Client.Modules.B3Center.Messages
{
    class SetB3OperatorMessage : ServerMessage
    {
        private readonly Operator m_operator;
        private int m_operatorId;
        private readonly int m_isDelete;

        public SetB3OperatorMessage(Operator currentOperator, int isDelete)
        {
            m_operator = currentOperator;
            m_isDelete = isDelete;
        }

        protected override void PackRequest(BinaryWriter requestWriter)
        {
            requestWriter.Write(m_operator.OperatorId);//0 New operator; # Update operator

            requestWriter.Write((byte)m_isDelete);

            requestWriter.Write((ushort)m_operator.OperatorName.Length);
            requestWriter.Write(m_operator.OperatorName.ToCharArray());

            requestWriter.Write((ushort)m_operator.OperatorNameDescription.Length);
            requestWriter.Write(m_operator.OperatorNameDescription.ToCharArray());

            requestWriter.Write((ushort)m_operator.ContactName.Length);
            requestWriter.Write(m_operator.ContactName.ToCharArray());

            requestWriter.Write((ushort)m_operator.Address.Length);
            requestWriter.Write(m_operator.Address.ToCharArray());

            requestWriter.Write((ushort)m_operator.City.Length);
            requestWriter.Write(m_operator.City.ToCharArray());

            requestWriter.Write((ushort)m_operator.State.Length);
            requestWriter.Write(m_operator.State.ToCharArray());

            requestWriter.Write((ushort)m_operator.ZipCode.Length);
            requestWriter.Write(m_operator.ZipCode.ToCharArray());

            requestWriter.Write((ushort)m_operator.PhoneNumber.Length);
            requestWriter.Write(m_operator.PhoneNumber.ToCharArray());

            requestWriter.Write((ushort)m_operator.FaxNumber.Length);
            requestWriter.Write(m_operator.FaxNumber.ToCharArray());

            requestWriter.Write(m_operator.IconColor);
        }

        protected override void UnpackResponse(BinaryReader responseReader)
        {
            if (ReturnCode == ServerReturnCode.Success)
            {
                m_operatorId = responseReader.ReadInt16();                   
            }
                     
        }

        public int OperatorId
        {
            get { return m_operatorId; }
   
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
