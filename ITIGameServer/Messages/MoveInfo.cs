using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;

namespace ITI.GameServer.Messages
{
    [MessagePackObject]
    public class MoveInfo : BaseMessage
    {
        [Key(1)]
        public int X { get; set; }

        [Key(2)]
        public int Y { get; set; }
    }
}
