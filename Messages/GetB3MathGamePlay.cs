using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
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
                    B3MathGamePay B3mathGamePlayData = new B3MathGamePay();

                    B3mathGamePlayData.MathPackageID = responseReader.ReadInt32();
                    B3mathGamePlayData.GameID = responseReader.ReadInt32();
                    B3mathGamePlayData.PackageDesc = new string(responseReader.ReadChars(responseReader.ReadInt16()));
                    B3mathGamePlayData.IsRNG = responseReader.ReadBoolean();
                    
                    ListB3MathGamePlay.Add(B3mathGamePlayData); 
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

