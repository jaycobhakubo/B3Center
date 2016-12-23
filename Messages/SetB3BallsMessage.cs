#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System;
using System.IO;

//US4299: B3 Set Balls

namespace GameTech.Elite.Client.Modules.B3Center.Messages
{
    public class SetB3BallsMessage : ServerMessage
    {
        #region Member Variables
        private readonly int[] m_balls;
        #endregion

                #region Constructors
        public SetB3BallsMessage(int[] ballList)
        {
            m_balls = ballList;
        }
        #endregion

        protected override void PackRequest(BinaryWriter requestWriter)
        {
            requestWriter.Write(Convert.ToInt16(m_balls.Length));

            foreach (var ball in m_balls)
            {
                requestWriter.Write(Convert.ToByte(ball));
            }
        }

        public override int Id
        {
            get { return 39052; }
        }

        public override string Name
        {
            get { return "Set B3 Balls"; }
        }
    }
}
