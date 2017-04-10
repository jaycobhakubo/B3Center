using System.Collections.Generic;
using System.IO;
using GameTech.Elite.Client.Modules.B3Center.Business;

namespace GameTech.Elite.Client.Modules.B3Center.Messages
{
    public class GetB3MathGamePlay : ServerMessage
    {
        public GetB3MathGamePlay()
        {
            ListB3MathGamePlay = new List<B3MathGamePay>();
        }

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
                int count = responseReader.ReadInt16();

                for (int i = 0; i < count; i++)
                {
                    B3MathGamePay b3MathGamePlayData = new B3MathGamePay
                    {
                        MathPackageId = responseReader.ReadInt32(),
                        GameType = (B3GameType)responseReader.ReadInt32(),
                        PackageDesc = new string(responseReader.ReadChars(responseReader.ReadInt16())),
                        IsRng = responseReader.ReadBoolean()
                    };


                    ListB3MathGamePlay.Add(b3MathGamePlayData); 
                }
            }
        }

        public override int Id
        {
            get
            {
                return 39057;
            }
        }

        public override string Name
        {
            get
            {
                return "Get B3 Math Game Play";
            }
        }

        public List<B3MathGamePay> ListB3MathGamePlay
        {
            get;
            set;
        } 
    }
}

