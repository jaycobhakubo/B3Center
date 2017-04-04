using System.Collections.Generic;
using System.IO;
using GameTech.Elite.Client.Modules.B3Center.Business;

namespace GameTech.Elite.Client.Modules.B3Center.Messages
{
    public class GetB3ColorIcon : ServerMessage
    {

        public GetB3ColorIcon()
        {
            Listb3IconColor = new List<B3IconColor>();
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
            Listb3IconColor = new List<B3IconColor>();
            if (ReturnCode == ServerReturnCode.Success)
            {

                int count = responseReader.ReadInt16();

                for (int i = 0; i < count; i++)
                {
                    int colorId = responseReader.ReadInt32();
                    string colorDefinition = new string(responseReader.ReadChars(responseReader.ReadInt16()));
                    B3IconColor b3IconColor = new B3IconColor(colorId, colorDefinition);
                    Listb3IconColor.Add(b3IconColor);                   
                }
            }
        }

        public override int Id
        {
            get
            {
                return 39054;
            }
        }

        public override string Name
        {
            get
            {
                return "Get B3 Color ICon";
            }
        }

        public List<B3IconColor> Listb3IconColor
        {
            get;
            set;
        }

       
    }
}
